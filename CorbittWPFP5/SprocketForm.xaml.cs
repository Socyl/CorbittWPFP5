using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace CorbittWPFP5
{
    /// <summary>
    /// Interaction logic for SprocketForm.xaml
    /// </summary>
    public partial class SprocketForm : Window
    {

        private bool isValid;
        private Sprocket sprocket;

        public Sprocket Sprocket
        {
            get
            { 
                return sprocket;
            }
        }

        public SprocketForm()
        {
            InitializeComponent();
            //add the items to the listbox
            cbMaterials.Items.Add("Steel");
            cbMaterials.Items.Add("Aluminum");
            cbMaterials.Items.Add("Plastic");
            //set the "default" item in the listbox
            cbMaterials.SelectedItem="Steel";
           
        }

        private void CheckFieldsForBlanks()
        {
            if (txbItemID.Text.Equals("")||txbNumTeeth.Text.Equals("")||txbNumItems.Text.Equals(""))
            {
                throw new ArgumentException("All Fields must be filled in!");
            }
         
        }
        private void ValidateFieldsData()
        {

            //BC This implementation seems complicated, but it allows the SprocketForm to stay open
            //when errors are made in data entry.  Other methods closed the window as soon as i set DialogResult to false.
            //this method avoids that issue, and lets me play with try/catch blocks on individual fields in the form.

            //TODO this is not very DRY....could probably write a function for some of the duplicated code here.


            //ItemID field
            try
            {
                //check field, if it would parse, isValid
                txbItemID.Background = Brushes.White;
                txbError.Visibility = Visibility.Hidden;
                int.Parse(txbItemID.Text);
                isValid = true;
            }
            catch(FormatException exp)
            {   //if not, set red and show error
                txbItemID.Background = Brushes.Red;
                txbError.Text = "ERROR: " + exp.Message;
                txbError.Visibility = Visibility.Visible;
                isValid = false;
                return;
            }
            //NumTeeth field
            try
            {
                //check field, if it would parse, isValid
                txbNumTeeth.Background = Brushes.White;
                txbError.Visibility = Visibility.Hidden;
                int.Parse(txbNumTeeth.Text);
                isValid = true;
            }
            catch (FormatException exp)
            {
                //if not, set red and show error
                txbNumTeeth.Background = Brushes.Red;
                txbError.Text = "ERROR: " + exp.Message;
                txbError.Visibility = Visibility.Visible;
                isValid = false;
                return;
            }
            //NumItems field
            try
            {
                //check field, if it would parse, isValid
                txbNumItems.Background = Brushes.White;
                txbError.Visibility = Visibility.Hidden;
                int.Parse(txbNumItems.Text);
                isValid = true;
            }
            catch (FormatException exp)
            {
                //if not, set red and show error
                txbNumItems.Background = Brushes.Red;
                txbError.Text = "ERROR: " + exp.Message;
                txbError.Visibility = Visibility.Visible;
                isValid = false;
                return;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            
            DialogResult = false;
            Close();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
           //make sure fields arent blank
            try
            {
                CheckFieldsForBlanks();
            }
            catch(ArgumentException exp)
            {
                MessageBox.Show(exp.Message);
                return; 
            }
            //init the correct type sprocket
            if (cbMaterials.SelectedItem.Equals("Steel"))
            {
                sprocket = new SteelSprocket();
            }
            else if (cbMaterials.SelectedItem.Equals("Aluminum"))
            {
                sprocket = new AluminumSprocket();
            }
            else if (cbMaterials.SelectedItem.Equals("Plastic")) 
            {
                sprocket = new PlasticSprocket();
            }
            //validate the data in the fields will parse correctly
            ValidateFieldsData();

            if (isValid)
            { 
                //all will parse ok, so parse them
                sprocket.ItemID = int.Parse(txbItemID.Text);
                sprocket.NumItems = int.Parse(txbNumItems.Text);
                sprocket.NumTeeth = int.Parse(txbNumTeeth.Text);
                DialogResult = true;
                
            }
        }
    }
}
