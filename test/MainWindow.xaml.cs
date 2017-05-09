using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
using diagramlib;

namespace test
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Customer[] Customers { get; }

        public MainWindow()
        {
            Customers = new[]
            {
                new Customer("Nab", 40),
                new Customer("Pobedy", 90),
                new Customer("Lenina", 200),
                new Customer("Petuha", 300) 
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ////Plotter.Items = Customers;
            //Plotter.ConstructPiePieces();
        }
    }
}
