using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

class Program
{
    //static List<DataRowState> list = new List<DataRowState>();

    static void Main()
    {
        AdapterInsert(XmlToDataTableByFile());
        SqlDataAdapterMain();
    }

    //Step 1. Initialize data table from xml source.
    static DataTable XmlToDataTableByFile()
    {
        string fileName = "xmlsample.xml";
        XmlDocument doc = new XmlDocument();
        doc.Load(fileName);

        DataTable dt = new DataTable("song");
        XmlNode songNode = doc.SelectSingleNode("/music/song[1]");
        string colName = null;
        if (songNode != null)
        {
            for (int i = 0; i < songNode.ChildNodes.Count; i++)
            {
                var xmlNode = songNode.ChildNodes.Item(i);
                if (xmlNode != null)
                {
                    colName = xmlNode.Name;
                }
                dt.Columns.Add(colName);
            }
        }
        DataSet ds = new DataSet("music");
        ds.Tables.Add(dt);

        ds.ReadXml(fileName);
        return dt;
    }

    static void AdapterInsert(DataTable dt)
    {
        using (SqlConnection conn = new SqlConnection(@"Server=CNTSNW10143079\SQL2008R2;DataBase=DemoDB;Integrated Security=True;"))
        {
            SqlCommand com = conn.CreateCommand();
            com.CommandText = "Insert Into Song (id,artist,genre,album,year) Values (@id,@artist,@genre,@album,@year)";
            com.Parameters.Add("@id", SqlDbType.VarChar, 20, "id");
            com.Parameters.Add("@artist", SqlDbType.VarChar, 20, "artist");
            com.Parameters.Add("@genre", SqlDbType.VarChar, 20, "genre");
            com.Parameters.Add("@album", SqlDbType.VarChar, 20, "album");
            com.Parameters.Add("@year", SqlDbType.VarChar, 20, "year");
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction("Song");
            com.Transaction = tran;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.InsertCommand = com;

            try
            {
                adapter.Update(dt);
                tran.Commit();
            }
            catch (Exception e)
            {
                tran.Rollback("Song");
                Console.WriteLine(e.Message);
            }
        }
    }

    //Step 2. Update data table.
    static void SqlDataAdapterMain()
    {
        SqlConnection connection = new SqlConnection(@"Server=CNTSNW10143079\SQL2008R2;DataBase=DemoDB;Integrated Security=True;");
        SqlDataAdapter adapter = CreateSqlDataAdapter(connection);
        DataTable dt = DataAdapterFill(connection, adapter);
        UpdateRows(connection, adapter, dt);
    }

    static SqlDataAdapter CreateSqlDataAdapter(SqlConnection connection)
    {
        SqlCommand com = connection.CreateCommand();
        com.CommandText = "Select id,artist,genre,album,year From Song Where year >= @pyear";
        com.Parameters.Add("@pyear",SqlDbType.VarChar).Value = "2004";

        SqlDataAdapter adapter = new SqlDataAdapter(com);
        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
        string text = builder.GetUpdateCommand().CommandText;

        return adapter;
    }

    static DataTable DataAdapterFill(SqlConnection connection, SqlDataAdapter adapter)
    {
        DataTable dt = new DataTable();
        try
        {
            adapter.Fill(dt);
        }
        catch (Exception)
        {
            connection.Close();
            connection.Dispose();
        }
        return dt;
    }

    static void UpdateRows(SqlConnection connection, SqlDataAdapter adapter, DataTable dataTable)
    {
        dataTable.Rows[0]["genre"] = "New Age";
        try
        {
            adapter.Update(dataTable);
        }
        catch (Exception)
        {

        }
        finally
        {
            connection.Close();
            connection.Dispose();
        }
    }
}

