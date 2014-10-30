using Microsoft.Win32;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
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

        /// <summary>
        /// Loads the database to the GUI.
        /// </summary>
        /// <param name="filepath">The path of the database file.</param>
        /// <param name="connection">An optional existing SQLiteConnection to use. This will override the filepath.</param>
        private void AttemptLoadDatabase(string filepath, SQLiteConnection connection = null)
        {
            SQLiteConnection con = connection ?? DatabaseConnection.Connect(filepath);

            // Select all categories, with their products
            var categories = Category.SelectAll(con).ToList();

            // Create a pseudo-category for products with no actual category
            Category psuedo = new Category() { Name = "No category", Products = Product.SelectAllWhereNoCategory(con).ToList() };
            categories.Add(psuedo);

            products.ItemsSource = categories;
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
                catch (Exception) // In case anything went wrong
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
            string uri = (string)e.Parameter;

            // TODO Validate uri as a URI.
            // Could be a security vulnerability as it could launch any application

            // Opens the default browser
            Process.Start(uri);
        }
    }
}
