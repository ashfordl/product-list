using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using ProductList.Database;
using ProductList.Models;
using SQLite.Net;

namespace ProductList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private bool AttemptLoadDatabase(string filepath, SQLiteConnection connection = null)
        {
            try
            {
                SQLiteConnection con = connection ?? DatabaseConnection.Connect(filepath);

                // Select all categories, with their products
                var categories = Category.SelectAll(con).ToList();

                // Create a pseudo-category for products with no actual category
                Category psuedo = new Category() { Name = "No category", Products = Product.SelectAllWhereNoCategory(con).ToList() };
                categories.Add(psuedo);

                products.ItemsSource = categories;
            }
            catch (SQLiteException sqlex)
            {
                return false;
            }
            
            return true;
        }

        private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "New Product List";
            dialog.FileName = "List1.db";
            dialog.Filter = "Database File (*.db)|*.db|All Files(*.*)|*.*";

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    DatabaseConnection.CreateDatabase(dialog.FileName);
                }
                catch (Exception ex) // In case anything went wrong
                {
                    MessageBox.Show("An error occurred and the list was not created.\n\n" + ex.ToString(),
                                    "Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                }

                // TODO Load file
            }
        }

        private void OpenCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "New Product List";
            dialog.FileName = "List1.db";
            dialog.Filter = "Database File (*.db)|*.db|All Files(*.*)|*.*";

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    AttemptLoadDatabase(dialog.FileName);
                }
                catch (Exception ex) // In case anything went wrong
                {
                    MessageBox.Show("An error occurred and the list was not created.",
                                    "Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                }
            }
        }

        private void NavigateUri_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void NavigateUri_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var send = (Hyperlink)sender;

            Process.Start(send.NavigateUri.ToString());
        }
    }
}
