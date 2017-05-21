using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.UserData;
using Microsoft.Phone.Tasks;

namespace Forward_Contact
{
    public partial class ImportContact : PhoneApplicationPage
    {
        Contact newContact;
        SaveContactTask savecontactTask;

        public ImportContact()
        {
            InitializeComponent();
            savecontactTask = new SaveContactTask();
            savecontactTask.Completed += new EventHandler<SaveContactResult>(saveContactTask_Completed);
        }        
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            this.mImportTxt.Text = "Name: Hung Nguyen;\r\n Mobile Phone: 3236101307;\r\n";
        }
        private void import_Click(object sender, RoutedEventArgs e)
        {
            if (mImportTxt.Text == "")
            {
                MessageBox.Show("Please paste the contact information");
            }
            else
            {
                addInformationToContact(mImportTxt.Text);
                savecontactTask.Show();
            }
        }
        private void addInformationToContact(String mInfo)
        {
            Dictionary<String, String> dict = new Dictionary<String, String>();
            String[] delimiter = { ";\r\n" };
            String[] rows = mInfo.Split(delimiter, StringSplitOptions.None);
            foreach(String row in rows)
            {
                if (row != null && row != "")
                {
                    String[] temp = row.Split(new char[] { ':' });
                    if (temp.Length == 2)
                    {
                        dict.Add(temp[0].Trim(), temp[1].Trim());
                    }
                }
            }
           
            savecontactTask.FirstName = dict["Name"];
            savecontactTask.MobilePhone = dict["Mobile Phone"];
        }

        void saveContactTask_Completed(object sender, SaveContactResult e)
        {
            switch (e.TaskResult)
            {
                //Logic for when the contact was saved successfully
                case TaskResult.OK:
                    MessageBox.Show("Contact saved.");
                    break;

                //Logic for when the task was cancelled by the user
                case TaskResult.Cancel:
                    MessageBox.Show("Save cancelled.");
                    break;

                //Logic for when the contact could not be saved
                case TaskResult.None:
                    MessageBox.Show("Contact could not be saved.");
                    break;
            }
        }
    }
}