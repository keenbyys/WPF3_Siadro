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
    /// Interaction logic for AddMenu.xaml
    /// </summary>
    public partial class AddMenuFarm : Window
    {
        public AddMenuFarm()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            string nameFarm = FarmNameTextBox.Text;
            string regionFarm = RegionTextBox.Text;
            string numPhone = NumPhoneTextBox.Text;
            string managerFarm = ManagerFarmTextBox.Text;
            int sownArea = Convert.ToInt32(SownTextBox.Text);
            int totalArea = Convert.ToInt32(TotalAreaTextBox.Text);

            string insert = $"INSERT INTO farms (name_farm, region, num_phone, manager_farm, sown_area, total_area) " +
                           $"VALUES ('{nameFarm}', '{regionFarm}', '{numPhone}', '{managerFarm}', {sownArea}, {totalArea})";

            DatabaseHelper dataBase = new DatabaseHelper();
            dataBase.NoneQuery(insert);
            MessageBox.Show("Дані успішно додані!");
            dataBase.SelectQuery("SELECT * FROM agricultural_land.farms;");
        }
    }
}
