using System;
using System.Collections.Generic;
using System.Text;

namespace Disconecred
{
    public static class DataRowExtensions
    {
        public static DataRow NewRowWithData<T>(
            this DataTable table, T item)
        {
            var newRow = table.NewRow();
            foreach (var propertyInfo in item.GetType()
                .GetProperties())
            {
                newRow[propertyInfo.Name] = propertyInfo.GetValue(item);
            }
            return newRow;
        }
    }
    public static class ApplicationSettings
    {
        public static readonly string CONNECTION_STRING =
            ConfigurationManager.ConnectionStrings["DefaultConnectionString"]
            .ConnectionString;
    }


    // Data Trasfer Object
    public class LotItemDto
    {
        public string LotId { get; set; }
        public string LotName { get; set; }
        public string LotDescription { get; set; }
        public DateTime PublishedDate { get; set; }
        public decimal InitialCost { get; set; }
        public int CreatedByEmployeeId { get; set; }
    }

    public class AuctionManagement
    {
        public void OpenLot(LotItemDto dto)
        {
            DataSet disconnectedDataSet = new DataSet();
            using (SqlConnection sqlConnection = new SqlConnection(
                ApplicationSettings.CONNECTION_STRING))
            {
                sqlConnection.Open();
                string getLotItemsSql = "SELECT * FROM [dbo].[LotItems]";
                using (SqlDataAdapter adapter = new SqlDataAdapter(getLotItemsSql, sqlConnection))
                {
                    adapter.Fill(disconnectedDataSet, "LotItems");

                    SqlCommandBuilder sqlCommandBuilder =
                        new SqlCommandBuilder(adapter);

                    DataTable lotItemsTable = disconnectedDataSet
                        .Tables["LotItems"];

                    var row = lotItemsTable.NewRowWithData<LotItemDto>(dto);

                    lotItemsTable.Rows.Add(row);

                    // disconnectedDataSet.AcceptChanges();

                    adapter.Update(disconnectedDataSet, "LotItems");
                }
            }
        }
    }


    public class DisconnectedModel
    {
        public IEnumerable<string> GetOrganizationNames()
        {
            DataSet disconnectedDataSet = new DataSet();
            using (SqlConnection sqlConnection = new SqlConnection(
                ApplicationSettings.CONNECTION_STRING))
            {
                sqlConnection.Open();
                string getOrganizationsSql = "SELECT * FROM [dbo].[Organizations]";
                using (SqlDataAdapter adapter = new SqlDataAdapter(getOrganizationsSql, sqlConnection))
                {
                    adapter.Fill(disconnectedDataSet, "Organizations");

                    SqlCommandBuilder sqlCommandBuilder =
                        new SqlCommandBuilder(adapter);

                    DataTable organizationsTable = disconnectedDataSet
                        .Tables["Organizations"];

                    /*
                    DataRow newRow = organizationsTable.NewRow();
                    newRow["OrganizationId"] = Guid.NewGuid().ToString();
                    newRow["OrganizationName"] = "АО 'Air Astana'";
                    */

                    var firstRow = organizationsTable.Rows[0];

                    firstRow["OrganizationName"] = "АО 'Air Astana'";

                    // disconnectedDataSet.AcceptChanges();

                    adapter.Update(disconnectedDataSet, "Organizations");
                }
            }
            return null;
        }
    }
}
