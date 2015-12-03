    Apache Bench

使用ab.exe监测100个并发/100次请求情况下同步/异步访问数据库的性能差异

ab.exe介绍
   ab.exe是apache server的一个组件，用于监测并发请求，并显示监测数据

本文的目的
   通过webapi接口模拟100个并发请求下，同步和异步访问数据库的性能差异
  
创建数据库及数据
    --创建表结构
    CREATE TABLE dbo.[Cars] (
        Id INT IDENTITY(1000,1) NOT NULL,
        Model NVARCHAR(50) NULL,
        Make NVARCHAR(50) NULL,
        [Year] INT NOT NULL,
        Price REAL NOT NULL,
        CONSTRAINT [PK_Cars] PRIMARY KEY CLUSTERED (Id) ON [PRIMARY]
    ) ON [PRIMARY];
    GO
    --创建存储过程
    CREATE PROCEDURE [dbo].[sp$GetCars]
    AS
    -- 存储过程执行过程中等待一秒
    WAITFOR DELAY '00:00:01';
    SELECT * FROM Cars;
    GO
    --初始化数据 
    INSERT INTO dbo.Cars VALUES('Car1', 'Model1', 2006, 24950);
    INSERT INTO dbo.Cars VALUES('Car2', 'Model1', 2003, 56829);
    INSERT INTO dbo.Cars VALUES('Car3', 'Model2', 2006, 17382);
    INSERT INTO dbo.Cars VALUES('Car4', 'Model3', 2002, 72733);
 
编写webapi程序
1、数据访问类（包含同步/异步访问数据库的方法）
 public class GalleryContext
    {
        readonly string _spName = "sp$GetCars";
        readonly string _connectionString =
        ConfigurationManager.ConnectionStrings["TestDBConnStr"].ConnectionString;
 
        /// <summary>
        /// 同步获取数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Car> GetCarsViaSP()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = _spName;
                    cmd.CommandType = CommandType.StoredProcedure;
 
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        return reader.Select(r => carBuilder(r)).ToList();
                    }
                }
            }
        }
 
        /// <summary>
        /// 异步获取数据 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Car>> GetCarsViaSPAsync()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = _spName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (var reader = await cmd.ExecuteReaderAsync())  //调用异步方法
                    {
                        return reader.Select(r => carBuilder(r)).ToList();
                    }
                }
            }
        }
 
        //private helpers
        private Car carBuilder(SqlDataReader reader)
        {
            return new Car
            {
                Id = int.Parse(reader["Id"].ToString()),
                Make = reader["Make"] is DBNull ? null : reader["Make"].ToString(),
                Model = reader["Model"] is DBNull ? null : reader["Model"].ToString(),
                Year = int.Parse(reader["Year"].ToString()),
                Price = float.Parse(reader["Price"].ToString()),
            };
        }
    }  
 
2、数据库对应实体类及辅助扩展方法
  public class Car
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public float Price { get; set; }
    } 
 
 public static class Extensions
    {
        public static IEnumerable<T> Select<T>(this SqlDataReader reader, Func<SqlDataReader, T> projection)
        {
            while (reader.Read())
            {
                yield return projection(reader);
            }
        }
    } 
 
3、webapi Controller调用方法
  //同步调用接口
    public class SPCarsSyncController : ApiController
    {
        readonly GalleryContext galleryContext = new GalleryContext();
        public IEnumerable<Car> Get()
        {
            return galleryContext.GetCarsViaSP();
        }
    }  
 
 //异步调用接口
    public class SPCarsAsyncController : ApiController
    {
        readonly GalleryContext galleryContext = new GalleryContext();
        public async Task<IEnumerable<Car>> Get()
        {
            return await galleryContext.GetCarsViaSPAsync();
        }
    } 
 
4、数据库连接配置文件
<connectionStrings>
    <add name="TestDBConnStr"
    connectionString="Server=你的数据库地址;Database=TestDB;User Id=sa;Password=123;Integrated Security=false;Max Pool Size=500;Asynchronous Processing=True;"
    providerName="System.Data.SqlClient" />
  </connectionStrings> 
其中：
    Max Pool Size：表示连接池的最大数量
    Asynchronous Processing=True;表示数据库支持异步处理
    Integrated Security=false;此处设为true或不设置会抛出异常
    Login failed for user 'NT AUTHORITY\SYSTEM'
 
使用ab.exe模拟并发请求
模拟请求同步服务：
    D:\>ab -n 100 -c 100 http://localhost/api/SPCarsSync
模拟请求异步服务：
    D:\>ab -n 100 -c 100 http://localhost/api/SPCarsAsync
    
结论：
    访问相同的数据表及数据，
    使用同步方式共用时20秒，平均每秒处理5个请求；
    使用异步方式共用时10秒，平均每秒处理10个请求；

 