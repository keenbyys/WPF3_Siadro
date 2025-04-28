using MySql.Data.MySqlClient;
using System.Data;

namespace WpfApp_LW3_DB
{
    public class DatabaseHelper
    {
        private string connectionString = "Server=localhost;Database=agricultural_land;Uid=root;Pwd=50026022SVK-23;";

        public DataTable SelectQuery (string query)
        {
            DataTable dataTable = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand commandSelect = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapterSelect = new MySqlDataAdapter(commandSelect);
                    adapterSelect.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"Помилка при з'єднанні з БД: {ex.Message}");
                }
            }

            return dataTable;
        }

        public void NoneQuery(string query)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand commandUpdateInsertDelete = new MySqlCommand(query, connection);
                    commandUpdateInsertDelete.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"Помилка при з'єднанні з БД: {ex.Message}");
                }
            }
        }

    }
}
