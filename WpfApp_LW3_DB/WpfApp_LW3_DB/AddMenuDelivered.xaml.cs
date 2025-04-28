using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp_LW3_DB
{
    /// <summary>
    /// Interaction logic for AddMenuDelivered.xaml
    /// </summary>
    public partial class AddMenuDelivered : Window
    {
        public AddMenuDelivered()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            int farmsId = Convert.ToInt32(FarmIDTextBox.Text);
            int culturesID = Convert.ToInt32(CulturesIDTextBox.Text);
            int orderWeight = Convert.ToInt32(OrderWeightTextBox.Text);
            int deliveredWeight = Convert.ToInt32(DeliveredWeightTextBox.Text);
            decimal yieldFarm = Convert.ToDecimal(YieldFarmTextBox.Text);
            string productQuality = (ProductQualityComboBox.SelectedItem as ComboBoxItem).Content.ToString();
            DateTime deliveryDate = DeliveryDateDatePicker.SelectedDate.Value;
            string formattedDate = deliveryDate.ToString("yyyy-MM-dd HH:mm:ss");

            string insert = $"INSERT INTO delivered (farms_id_farm, cultures_id_cultures, order_weight, delivered_weight, yield_farm, product_quality, delivery_date) " +
                           $"VALUES ({farmsId}, {culturesID}, {orderWeight}, {deliveredWeight}, {yieldFarm}, '{productQuality}', '{formattedDate}')";

            DatabaseHelper dataBase = new DatabaseHelper();
            dataBase.NoneQuery(insert);
            MessageBox.Show("Дані успішно додані!");
            dataBase.SelectQuery("SELECT * FROM agricultural_land.delivered;");
        }
    }
}
