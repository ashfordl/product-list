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
using ProductList.Database;

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
                    // DatabaseOperations.CreateDatabase(dialog.FileName);
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
                    // TODO Load file
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
    }
}
