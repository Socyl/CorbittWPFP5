//Byron Corbitt Bcorbitt@cnm.edu


using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

namespace CorbittWPFP5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SprocketOrderForm : Window
    {

        private Address address;
        private Sprocket newSprocket;

        //make a list of sprockets
        private List<Sprocket> sprockets = new List<Sprocket>();


        public SprocketOrderForm()
        {
            InitializeComponent();
            address = new Address();
            newSprocket = new SteelSprocket();
            //Databind the listbox to the List
            lbOrders.ItemsSource = sprockets;

        }

        private void cbLocalPickupChecked(object sender, RoutedEventArgs e)
        {
            //hide the address fields when local delivery and reset them if already filled out (user changes mind)
            txbStreet.Text = "";
            txbCity.Text = "";
            txbState.Text = "";
            txbZipCode.Text = "";
            canvasAddress.Visibility = Visibility.Hidden;
        }

        private void cbLocalPuckupUnchecked(object sender, RoutedEventArgs e)
        {
            //show the address fields when not a local delivery 
            canvasAddress.Visibility = Visibility.Visible;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {


            SprocketForm window = new SprocketForm();
            window.ShowDialog();

            if (window.DialogResult == true)
            {
                //got a good sprocket back from SprocketForm so add it to the sprockets list and refresh the listbox
                sprockets.Add(window.Sprocket);
                lbOrders.Items.Refresh();
            }
            else
            {
                //cancel button hit
                MessageBox.Show("Sprocket Form Dialog Cancelled!");
            }

        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            //Get the index of the selected item in the listbox
            int selectionIndex = lbOrders.SelectedIndex;
            //if that selection is valid
            if (selectionIndex >= 0)
            {
                //prompt for confirmation
                MessageBoxResult theResult = MessageBox.Show(owner: this,
                                             messageBoxText: "Are you sure?",
                                             caption: "Confirm",
                                             button: MessageBoxButton.YesNo,
                                             icon: MessageBoxImage.Question,
                                             MessageBoxResult.Yes);
                if (theResult == MessageBoxResult.Yes)
                {
                    //if yes, remove it from the list and refresh the listbox
                    sprockets.RemoveAt(selectionIndex);
                    lbOrders.Items.Refresh();
                }
                else
                {
                    return;
                }
            }
            else
            {
                //nothing selected when remove button clicked
                MessageBox.Show("You must select an item to remove!");
            }
        }

        private void CheckForBlanksOnSprocketOrderForm()
        {
            //handle blank Customer Name on the SprocketOrderForm
            if (txbCustomerName.Text.Equals(""))
            {
                throw new ArgumentException("Customer Name must be filled in!");
            }

            //handle blank address fields on the SprocketOrderForm
            if (cbLocalPickup.IsChecked == false && (txbStreet.Text.Equals("") || txbState.Text.Equals("") || txbCity.Text.Equals("") || txbZipCode.Text.Equals("")))
            {
                throw new ArgumentException("All address fields must be filled in.");
            }
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            //make sure fields arent blank
            try
            {
                CheckForBlanksOnSprocketOrderForm();
            }
            catch (ArgumentException exp)
            {
                MessageBox.Show(exp.Message);
                return;
            }
            //init a new SprocketOrder and populate fields
            SprocketOrder order = new SprocketOrder();
            order.Items = sprockets;
            order.CustomerName = txbCustomerName.Text;
            Address address = new Address();
            //handle local/non local deliveries
            if (cbLocalPickup.IsChecked == true)
            {
                address.Street = "Local pickup";
            }
            else
            {
                address.Street = txbStreet.Text;
                address.State = txbState.Text;
                address.City = txbCity.Text;
                address.ZipCode = txbZipCode.Text;
            }
            order.Address = address;
            //and save the file out to disk
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Text Files|*.txt";

            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                string path = dialog.FileName;
                File.WriteAllText(path, order.ToString());
            }

        }
    }
}
