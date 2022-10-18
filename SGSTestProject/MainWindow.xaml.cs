using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SGSTestProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<(int, string, int)> WorkShops { get; set; } = new List<(int, string, int)>();
        public List<(int, string)> Employees { get; set; } = new List<(int, string)>();
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {
            var culture = new CultureInfo("ru-RU"); //for datetime format
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            City.SelectedValuePath = "Key";
            City.DisplayMemberPath = "Value";
            City.Items.Add(new KeyValuePair<int, string>(10, "Кондитерский цех"));
            City.Items.Add(new KeyValuePair<int, string>(30, "Цех обработки сырья"));
            City.Items.Add(new KeyValuePair<int, string>(55, "Меховой цех"));
            WorkShops.Add((10, "Almaty", 1));
            WorkShops.Add((10, "Astana", 2));
            WorkShops.Add((30, "Taraz", 3));
            WorkShops.Add((30, "Shimkent", 4));
            WorkShops.Add((55, "Aktobe", 5));
            WorkShops.Add((55, "Pavlodar", 6));
            City.SelectedValuePath = "Key";
            WorkShop.DisplayMemberPath = "Value";
            Employees.Add((1, "Сейдахметов С"));
            Employees.Add((1, "Герцен Е"));
            Employees.Add((2, "Остапин А"));
            Employees.Add((2, "Гаврилов Д"));
            Employees.Add((3, "Пашин Б"));
            Employees.Add((3, "Машина Е"));
            Employees.Add((4, "Павлов М"));
            Employees.Add((4, "Ким Е"));
            Employees.Add((5, "Быков А"));
            Employees.Add((6, "Налимов К"));
            Smen.Items.Add("Смена 1");
            Smen.Items.Add("Смена 2");
            if (DateTime.Now.Hour > 8 && DateTime.Now.Hour < 20)
            {
                Brig.Text = "Дневная бригада";
            }
            else
            {
                Brig.Text = "Ночная бригада";
            }
        }

        private void City_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = ((KeyValuePair<int, string>)e.AddedItems[0]).Key;
            WorkShop.Items.Clear();
            foreach (var item in WorkShops.Where(z => z.Item1 == selected))
            {
                WorkShop.Items.Add(new KeyValuePair<int, string>(item.Item3, item.Item2));
            }
        }

        private void WorkShop_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = ((KeyValuePair<int, string>)e.AddedItems[0]).Key;
            Employee.Items.Clear();
            foreach (var item in Employees.Where(z => z.Item1 == selected))
            {
                Employee.Items.Add(item.Item2);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (City.SelectedIndex != -1 && WorkShop.SelectedIndex != -1 && Employee.SelectedIndex != -1 && Smen.SelectedIndex != -1)
            {
                var res = "{" + $"\"City\": {City.SelectedValue}, \"WorkShop\": {((KeyValuePair<int, string>)WorkShop.SelectedValue).Key}, \"Employee\":{Employee.SelectedValue}, \"Brigade\":{Brig.Text}, \"Smena\":\"{Smen.SelectedValue}\"" + "}";
                //https://jsonformatter.curiousconcept.com/# VALID (RFC 8259))
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text File | *.txt";
                Res.Text = res;
                if (saveFileDialog.ShowDialog() == true)
                    File.WriteAllText(saveFileDialog.FileName, res);
            }
            else
            {
                MessageBox.Show("Не выбраны необходимые значения");
            }
        }
    }
}
