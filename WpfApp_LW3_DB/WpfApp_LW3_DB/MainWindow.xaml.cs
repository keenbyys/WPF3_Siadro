using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp_LW3_DB;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{

    public MainWindow()
    {
        InitializeComponent();
    }

    private void ConnectButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            DatabaseHelper connectionDataBase = new DatabaseHelper();
            MessageBox.Show("Підключення до MySQL успішне!");
            LoadButton.IsEnabled = true;
            AddButton.IsEnabled = true;
            DeleteButton.IsEnabled = true;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Помилка підключення: " + ex.Message);
        }
    }

    private void LoadButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            DatabaseHelper helperDataBase = new DatabaseHelper();
            if (ComboBox.Text == "Farms")
            {
                DataGrid.ItemsSource = null;
                var table = helperDataBase.SelectQuery("SELECT * FROM agricultural_land.farms;");
                DataGrid.ItemsSource = table.DefaultView;
                
            }
            if (ComboBox.Text == "Culture")
            {
                DataGrid.ItemsSource = null;
                var table = helperDataBase.SelectQuery("SELECT * FROM agricultural_land.cultures;");
                DataGrid.ItemsSource = table.DefaultView;
            }
            if (ComboBox.Text == "Delivered")
            {
                DataGrid.ItemsSource = null;
                var table = helperDataBase.SelectQuery("SELECT * FROM agricultural_land.delivered;");
                DataGrid.ItemsSource = table.DefaultView;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Помилка вибірки: " + ex.Message);
        }
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (ComboBox.Text == "Farms")
            {
                AddMenuFarm addMenuFarm = new AddMenuFarm();
                addMenuFarm.ShowDialog();
            } 
            if (ComboBox.Text == "Culture")
            {
                AddMenuCultures addMenuCultures = new AddMenuCultures();
                addMenuCultures.ShowDialog();
            }
            if (ComboBox.Text == "Delivered")
            {
                AddMenuDelivered addMenuDelivered = new AddMenuDelivered();
                addMenuDelivered.ShowDialog();
            }
            else
            {
                MessageBox.Show("boba iba");
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show("Помилка: " + ex.Message);
        }
    }

    private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
    {
        var editedRow = e.Row.Item as DataRowView;
        if (editedRow != null)
        {
            var newValue = (e.EditingElement as TextBox).Text;
            string columnName = e.Column.Header.ToString();

            editedRow[columnName] = newValue;

            if (ComboBox.Text == "Farms")
            {
                UpdateDatabaseFarm(editedRow.Row, editedRow["id_farm"].ToString());
            }
            else if (ComboBox.Text == "Culture")
            {
                UpdateDatabaseCultures(editedRow.Row, editedRow["id_cultures"].ToString());
            }
            else if (ComboBox.Text == "Delivered")
            {
                UpdateDatabaseDelivered(editedRow.Row, editedRow["farms_id_farm"].ToString(), editedRow["cultures_id_cultures"].ToString());
            }
        }
    }

    private void UpdateDatabaseFarm(DataRow updatedRow, string id_farm)
    {
        try
        {
            string query = @"UPDATE farms
                         SET name_farm = @name_farm, 
                             region = @region, 
                             num_phone = @num_phone, 
                             manager_farm = @manager_farm, 
                             sown_area = @sown_area, 
                             total_area = @total_area
                         WHERE id_farm = @id_farm";

            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=agricultural_land;Uid=root;Pwd=50026022SVK-23;"))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name_farm", updatedRow["name_farm"]);
                    cmd.Parameters.AddWithValue("@region", updatedRow["region"]);
                    cmd.Parameters.AddWithValue("@num_phone", updatedRow["num_phone"]);
                    cmd.Parameters.AddWithValue("@manager_farm", updatedRow["manager_farm"]);
                    cmd.Parameters.AddWithValue("@sown_area", updatedRow["sown_area"]);
                    cmd.Parameters.AddWithValue("@total_area", updatedRow["total_area"]);
                    cmd.Parameters.AddWithValue("@id_farm", id_farm);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Данные успешно обновлены!");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка при обновлении данных: " + ex.Message);
        }
    }

    private void UpdateDatabaseCultures(DataRow updatedRow, string id_culture)
    {
        try
        {
            string query = @"UPDATE cultures
                         SET name_cultures = @name_cultures, 
                             avg_yield = @avg_yield, 
                             price_first_class_product = @price_first_class_product, 
                             price_second_class_product = @price_second_class_product, 
                             price_premium_product = @price_premium_product
                         WHERE id_cultures = @id_cultures";

            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=agricultural_land;Uid=root;Pwd=50026022SVK-23;"))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    
                    cmd.Parameters.AddWithValue("@name_cultures", updatedRow["name_cultures"]);
                    cmd.Parameters.AddWithValue("@avg_yield", updatedRow["avg_yield"]);
                    cmd.Parameters.AddWithValue("@price_first_class_product", updatedRow["price_first_class_product"]);
                    cmd.Parameters.AddWithValue("@price_second_class_product", updatedRow["price_second_class_product"]);
                    cmd.Parameters.AddWithValue("@price_premium_product", updatedRow["price_premium_product"]);
                    cmd.Parameters.AddWithValue("@id_cultures", id_culture);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Данные успешно обновлены!");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка при обновлении данных: " + ex.Message);
        }
    }

    private void UpdateDatabaseDelivered(DataRow updatedRow, string farms_id_farm, string cultures_id_cultures)
    {
        try
        {
            string query = @"UPDATE delivered
                         SET order_weight = @order_weight, 
                             delivered_weight = @delivered_weight, 
                             yield_farm = @yield_farm, 
                             product_quality = @product_quality, 
                             delivery_date = @delivery_date
                         WHERE farms_id_farm = @farms_id_farm AND cultures_id_cultures = @cultures_id_cultures";

            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=agricultural_land;Uid=root;Pwd=50026022SVK-23;"))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    
                    cmd.Parameters.AddWithValue("@order_weight", updatedRow["order_weight"]);
                    cmd.Parameters.AddWithValue("@delivered_weight", updatedRow["delivered_weight"]);
                    cmd.Parameters.AddWithValue("@yield_farm", updatedRow["yield_farm"]);
                    cmd.Parameters.AddWithValue("@product_quality", updatedRow["product_quality"]);
                    cmd.Parameters.AddWithValue("@delivery_date", updatedRow["delivery_date"]);
                    cmd.Parameters.AddWithValue("@farms_id_farm", farms_id_farm);
                    cmd.Parameters.AddWithValue("@cultures_id_cultures", cultures_id_cultures);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Данные успешно обновлены!");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка при обновлении данных: " + ex.Message);
        }
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        DatabaseHelper delete = new DatabaseHelper();

        if (DataGrid.SelectedItem is DataRowView selectedRow)
        {
            if (ComboBox.Text == "Farms")
            {
                int idFarm = Convert.ToInt32(selectedRow["id_farm"]);
                string query = $"DELETE FROM farms WHERE id_farm = {idFarm}";
                delete.NoneQuery(query);
                LoadButton_Click(sender, e);
            }
            else if (ComboBox.Text == "Culture")
            {
                int idCulture = Convert.ToInt32(selectedRow["id_cultures"]);
                string query = $"DELETE FROM cultures WHERE id_cultures = {idCulture}";
                delete.NoneQuery(query);
                LoadButton_Click(sender, e);
            }
            else if (ComboBox.Text == "Delivered")
            {
                int idDelivered = Convert.ToInt32(selectedRow["id_delivered"]);
                string query = $"DELETE FROM delivered WHERE id_delivered = {idDelivered}";
                delete.NoneQuery(query);
                LoadButton_Click(sender, e);
            }
        }
        else
        {
            MessageBox.Show("Выберите строку для удаления.");
        }
    }
}