using System;
using System.Data;
using System.Data.SqlClient;

namespace SQLBulkCopy
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dtNorthWindOrders = new DataTable();
            using (SqlConnection northWindConnection = new SqlConnection(@"Data Source=CNTSNW10143079\SQL2008R2;Initial Catalog=Northwind;Integrated Security=True"))
            {
                using (SqlDataAdapter northWindAdapter = new SqlDataAdapter("SELECT ORDERID,CUSTOMERID,ORDERDATE,SHIPNAME FROM ORDERS", northWindConnection))
                {
                    northWindAdapter.Fill(dtNorthWindOrders);
                }
            }

            using (SqlConnection tempdbConnection = new SqlConnection(@"Data Source=CNTSNW10143079\SQL2008R2;Initial Catalog=DemoDB;Integrated Security=True"))
            {
                tempdbConnection.Open();

                using (SqlTransaction tran = tempdbConnection.BeginTransaction())
                {
                    SqlBulkCopy bulkCopyOrders = new SqlBulkCopy(tempdbConnection, SqlBulkCopyOptions.Default, tran);
                    bulkCopyOrders.DestinationTableName = "TEMP_ORDERS";

                    bulkCopyOrders.ColumnMappings.Add("ORDERID", "TEMP_ORDERID");
                    bulkCopyOrders.ColumnMappings.Add("CUSTOMERID", "TEMP_CUSTOMERID");
                    bulkCopyOrders.ColumnMappings.Add("ORDERDATE", "TEMP_ORDERDATE");
                    bulkCopyOrders.ColumnMappings.Add("SHIPNAME", "TEMP_SHIPNAME");

                    bulkCopyOrders.BulkCopyTimeout = 1000;

                    bulkCopyOrders.SqlRowsCopied += new SqlRowsCopiedEventHandler(onRowsCopy);
                    bulkCopyOrders.NotifyAfter = 10;

                    try
                    {
                        bulkCopyOrders.WriteToServer(dtNorthWindOrders);
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.ToString());
                    }
                    finally
                    {
                        dtNorthWindOrders = null;
                    }
                }
            }
        }

        private void onRowsCopy(object Sender, SqlRowsCopiedEventArgs args)
        {
            Response.Write("已复制：<font color=red>" + args.RowsCopied + "</font><br />");
        }
    }
}