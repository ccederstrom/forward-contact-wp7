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
using System.Diagnostics;
using System.Windows.Media.Imaging;
using Forward_Contact.model;

namespace Forward_Contact
{
    public partial class CustomContactChooserTask : PhoneApplicationPage
    {
        public CustomContactChooserTask()
        {
            InitializeComponent();
            longlist.SelectionChanged += new SelectionChangedEventHandler(longlist_SelectionChanged);
            if(App.contactList!=null)longlist.ItemsSource = App.contactList;           
        }       
        private void longlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ExtendedContact a = (ExtendedContact)longlist.SelectedItem;
            if (a != null)
            {
                App.selectedContact = a.contact;
                NavigationService.GoBack();
            }
        }
    }   
    
}