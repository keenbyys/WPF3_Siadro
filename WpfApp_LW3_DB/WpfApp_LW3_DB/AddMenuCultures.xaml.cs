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
    /// Interaction logic for EditMenu.xaml
    /// </summary>
    public partial class AddMenuCultures : Window
    {
        public AddMenuCultures()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            string nameCultures = CulturesNameTextBox.Text;
            decimal avgYield = Convert.ToDecimal(AvgYieldTextBox.Text);
            decimal priceFirstClassProduct = Convert.ToDecimal(PriceFirstClassProductTextBox.Text);
            decimal priceSecondClassProduct = Convert.ToDecimal(PriceSecondClassProductTextBox.Text);
            decimal pricePremiumProduct = Convert.ToDecimal(PricePremiumProductTextBox.Text);

            string insert = $"INSERT INTO cultures (name_cultures, avg_yield, price_first_class_product, price_second_class_product, price_premium_product) " +
                           $"VALUES ('{nameCultures}', '{avgYield}', '{priceFirstClassProduct}', '{priceSecondClassProduct}', {pricePremiumProduct})";

            DatabaseHelper dataBase = new DatabaseHelper();
            dataBase.NoneQuery(insert);
            MessageBox.Show("Дані успішно додані!");
            dataBase.SelectQuery("SELECT * FROM agricultural_land.cultures;");
        }
    }
}
