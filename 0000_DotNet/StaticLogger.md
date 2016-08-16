    *** Static Logger ***
    
using System;
using System.IO;
using System.Configuration;

namespace KS.Global
{
    /// <summary>
    /// Logger 的摘要说明。
    /// 仅可使用DB、Comm和默认log实例
    /// </summary>
    public class Logger
    {
        //定义不同类型日志对象
        //主业务流程日志
        private static Logger Steplogger = null;
        //控件日志
        private static Logger Controllogger = null;
        //通信日志
        private static Logger Commlogger = null;
        //业务组件日志:记录功能函数实现情况
        private static Logger Codelogger = null;
        //后台数据日志
        private static Logger Datalogger = null;
        //监控业务日志
        private static Logger Monitorlogger = null;
        //流水日志
        private static Logger Journallogger = null;
        //脚本日志
        private static Logger Scriptlogger = null;
        private static Logger logger = null;
        
        private string filePath = ""; //用来记录当前日志文件
        private StreamWriter sw = null;
        private bool recordLog = false;
        private string datetime = null;
        private string logname = "";

        private Logger() //禁止直接创建实例
        {
        }

        public void close() //关闭日志，释放资源
        {
            try
            {//如果前面的日志还没关闭，则关闭
                this.sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        private const int DEBUG = 0;
        private const int WARNING = 1;
        private const int ERROR = 2;
        private const int INFO = 3;
        
        /**
         * 记录日志级别
         * 0　DEBUG
         * 1　WARNING
         * 2　ERROR
         * 3　INFO
         * 如果需要更改记录日志的级别，只需要修改本字段的取值即可
         **/
         
        private int level = 0;
        
        /**
         * 获取日志类实例
         **/
        public static Logger getLogger()
        {
            if (logger == null)
            {
                logger = new Logger();
                //                string recordlogvalue =ConfigurationSettings.AppSettings["RecordLog"];
                //                if (recordlogvalue.ToUpper().Equals("YES")) 
                //                {
                //                    logger.recordLog = true;
                //                }
                //                else 
                //                {
                //                    logger.recordLog = false;
                //                }

                logger.recordLog = true;
                logger.datetime = DateTime.Now.ToString("u").Substring(0, 10);
                string sDir = UtilTool.GetExecutePath();
                logger.config(sDir + "LOG\\" + logger.datetime + ".log");
            }
            return logger;
        }
        
        /// <summary>
        /// 日志分类型,分别new不同类型对象
        /// </summary>
        /// <param name="LogType"></param>
        /// <returns></returns>
        public static Logger getLogger(string LogType)
        {
            switch (LogType)
            {

                case "Control":
                    {
                        if (Controllogger == null)
                        {
                            Controllogger = new Logger();
                            Controllogger.recordLog = true;
                            Controllogger.datetime = DateTime.Now.ToString("u").Substring(0, 10);
                            string sDir = UtilTool.GetExecutePath();
                            string path = sDir + "LOG\\" + LogType;
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            Controllogger.config(path + "\\" + Controllogger.datetime + ".log");
                        }
                        return Controllogger;
                    }
                case "Step":
                    {
                        if (Steplogger == null)
                        {
                            Steplogger = new Logger();
                            Steplogger.recordLog = true;
                            Steplogger.datetime = DateTime.Now.ToString("u").Substring(0, 10);
                            string sDir = UtilTool.GetExecutePath();
                            string path = sDir + "LOG\\" + LogType;
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            Steplogger.config(path + "\\" + Steplogger.datetime + ".log");
                        }
                        return Steplogger;
                    }

                case "Script":
                    {
                        if (Scriptlogger == null)
                        {
                            Scriptlogger = new Logger();
                            Scriptlogger.recordLog = true;
                            Scriptlogger.datetime = DateTime.Now.ToString("u").Substring(0, 10);
                            string sDir = UtilTool.GetExecutePath();
                            string path = sDir + "LOG\\" + LogType;
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            Scriptlogger.config(path + "\\" + Scriptlogger.datetime + ".log");
                        }
                        return Scriptlogger;
                    }
                case "Code":
                    {
                        if (Codelogger == null)
                        {
                            Codelogger = new Logger();
                            Codelogger.recordLog = true;
                            Codelogger.datetime = DateTime.Now.ToString("u").Substring(0, 10);
                            string sDir = UtilTool.GetExecutePath();
                            string path = sDir + "LOG\\" + LogType;
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            Codelogger.config(path + "\\" + Codelogger.datetime + ".log");
                        }
                        return Codelogger;
                    }
                case "DB":
                    {
                        if (Datalogger == null)
                        {
                            Datalogger = new Logger();
                            Datalogger.recordLog = true;
                            Datalogger.datetime = DateTime.Now.ToString("u").Substring(0, 10);
                            string sDir = UtilTool.GetExecutePath();
                            string path = sDir + "LOG\\" + LogType;
                            
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            Datalogger.config(path + "\\" + Datalogger.datetime + ".log");
                        }
                        return Datalogger;
                    }
                case "Monitor":
                    {
                        if (Monitorlogger == null)
                        {
                            Monitorlogger = new Logger();
                            Monitorlogger.recordLog = true;
                            Monitorlogger.datetime = DateTime.Now.ToString("u").Substring(0, 10);
                            string sDir = UtilTool.GetExecutePath();
                            string path = sDir + "LOG\\" + LogType;
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            Monitorlogger.config(path + "\\" + Monitorlogger.datetime + ".log");
                        }
                        return Monitorlogger;
                    }
                case "Journal":
                    {
                        if (Journallogger == null)
                        {
                            Journallogger = new Logger();
                            Journallogger.recordLog = true;
                            Journallogger.datetime = DateTime.Now.ToString("u").Substring(0, 10);
                            string sDir = UtilTool.GetExecutePath();
                            string path = sDir + "LOG\\" + LogType;
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            Journallogger.config(path + "\\" + Journallogger.datetime + ".log");
                        }
                        return Journallogger;
                    }
                case "Comm":
                    {
                        if (Commlogger == null)
                        {
                            Commlogger = new Logger();
                            Commlogger.recordLog = true;
                            Commlogger.datetime = DateTime.Now.ToString("u").Substring(0, 10);
                            string sDir = UtilTool.GetExecutePath();
                            string path = sDir + "LOG\\" + LogType;
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            Commlogger.config(path + "\\" + Commlogger.datetime + ".log");
                        }
                        return Commlogger;
                    }
                default:
                    {
                        getLogger();
                        return logger;
                    }
            }
        }

        /**
         * 配置日志路径及级别
         * 由本方法关闭前面的日志并打开新日志
         **/
        public void config(string filePath)
        {
            try
            {//如果前面的日志还没关闭，则关闭
                if(sw!= null)
                    this.sw.Close();
            }
            catch (Exception e)
            {
            }

            this.level = level;
            this.filePath = filePath;

            try
            {
                this.sw = new StreamWriter(this.filePath, true, System.Text.Encoding.Default);
                this.sw.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //日志文件打开失败，放弃日志
            }
        }

        /**
         * 写日志的基础方法
         * 如果要改为写数据库或者其它流，只需更改本方法
         **/
        private void log(string strLog)
        {
            if (this.sw != null)
            {
                string now = DateTime.Now.ToString("u").Substring(0, 10);
                if (!now.Equals(datetime))
                {
                    ///日期发生变化，重新打开新文件，记录日志
                    ///
                    datetime = now;
                    string sDir = UtilTool.GetExecutePath();
                    if (logger != null)
                        logger.config(sDir + "LOG\\" + datetime + ".log");
                    if (Datalogger != null)
                        Datalogger.config(sDir + "LOG\\DB\\" + datetime + ".log");
                    if (Commlogger != null)
                        Commlogger.config(sDir + "LOG\\Comm\\" + datetime + ".log");
                }
                this.sw.WriteLine(DateTime.Now.ToString() + " - " + strLog);
                this.sw.Flush();
            }
        }

        /**
         * 写日志
         **/
        public void debug(string strLog)
        {
            if (this.level <= Logger.DEBUG && this.recordLog)
            {
                this.log("debug: " + strLog);
            }
        }
        public void error(string strLog)
        {
            if (this.level <= Logger.ERROR && this.recordLog)
            {
                this.log("error: " + strLog);
            }
        }
        public void info(string strLog)
        {
            if (this.level <= Logger.INFO && this.recordLog)
            {
                this.log("infor: " + strLog);
            }
        }
        public void warning(string strLog)
        {
            if (this.level <= Logger.WARNING && this.recordLog)
            {
                this.log("warning: " + strLog);
            }
        }

        public void debugUploadError(string strLog)
        {
            // System.Windows.Forms.MessageBox.Show(strLog);
            if (this.level <= Logger.DEBUG && this.recordLog)
            {
                this.log("debug: " + strLog);
            }
        }
    }
}

调用示例:

        private void btn_db_Click(object sender, EventArgs e)
        {
            Logger.getLogger("DB").debug("this is a test click");
        }

        private void btn_com_Click(object sender, EventArgs e)
        {
            Logger.getLogger("Comm").debug("this is a test click");
        }

        private void btn_log_Click(object sender, EventArgs e)
        {
            Logger.getLogger().debug("this is a test click");
        }