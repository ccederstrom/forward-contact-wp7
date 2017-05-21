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
using Microsoft.Phone.Tasks;
using Microsoft.Phone.UserData;
using System.Diagnostics;
using Forward_Contact.model;
using System.Threading;
using Forward_Contact.util;
using System.Windows.Media.Imaging;
using Information_Page;
using System.Windows.Threading;

namespace Forward_Contact
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Reference: http://www.windowsphonegeek.com/articles/How-to-choose-a-Contact-and-get-Contact-details-in-a-WP7-app
        private string displayName = "Andrew Hill";
        List<DetailObject> detail = new List<DetailObject>();
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            #region Paid version Check
            if (App.isPaidVersion == true)
            {
                // disables ads
                adControl.Visibility = System.Windows.Visibility.Collapsed;
                //adControl.IsAutoRefreshEnabled = false;
                adControl.IsEnabled = false;
                adControl.IsAutoCollapseEnabled = false;
                listDetail.Height = 410;
                //pivotControl.Margin = new Thickness(0, 0, 0, 0);
                // direction.Margin = new Thickness(0, 0, 0, 0);
            }
            else
            {
                // enable ads
                adControl.Visibility = System.Windows.Visibility.Visible;
                //adControl.IsAutoRefreshEnabled = true;
                adControl.IsEnabled = true;
                adControl.IsAutoCollapseEnabled = false;
                listDetail.Height = 390;
                //Ads Timer
                mTimer.Interval = new TimeSpan(0, 0, 0, 0, 300000); // 5 mins
                mTimer.Tick += new EventHandler(mTimer_Tick);
            }
            #endregion
        }
       
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            Debug.WriteLine("ONNAVIGATE");
            CustomContactChooserTaskUtil.initialContactList(this);
            //ApplicationBar.IsMenuEnabled = false;
            if (App.selectedContact != null)
            {
                BitmapImage pic = new BitmapImage();
                if (App.selectedContact.GetPicture() != null)
                {
                    pic.SetSource(App.selectedContact.GetPicture());
                }
                else
                {
                    pic = new BitmapImage(new Uri("/Images/emptyAvatar.png", UriKind.RelativeOrAbsolute));
                }
                imgAvatar.Source = pic;
                imgAvatar.Visibility = Visibility.Visible;
                tbName.Text = App.selectedContact.DisplayName;
                tbName.Visibility = Visibility.Visible;
                loadDetailtoList();
                tbTap.Visibility = Visibility.Visible;
            }
        }
        private void loadDetailtoList()
        {
            detail = new List<DetailObject>();
            Contact selected = App.selectedContact;
            String path ;
            if (isLightTheme())
            {
                path = ForwardContact_Constant.PATH_ICON_LIGHT;
            }
            else
            {
                path = ForwardContact_Constant.PATH_ICON;
            }
            //DisplayName
            if (selected.DisplayName != null && selected.DisplayName != "")
            {
                DetailObject temp = new DetailObject();
                temp.IconSource = path+ForwardContact_Constant.ICON_DISPLAYNAME;
                temp.Content = "Name: " + selected.DisplayName;
                detail.Add(temp);
            }

            //Phone
            if (selected.PhoneNumbers != null && selected.PhoneNumbers.Count() != 0)
            {
                foreach (ContactPhoneNumber acct in selected.PhoneNumbers)
                {
                    DetailObject temp = new DetailObject();
                    temp.IconSource = path + ForwardContact_Constant.ICON_PHONE;
                    temp.Content = "Phone (" + acct.Kind + "): " + acct.PhoneNumber;
                    detail.Add(temp);
                }
            }

            //Email
            if (selected.EmailAddresses != null && selected.EmailAddresses.Count() != 0)
            {
                foreach (ContactEmailAddress acct in selected.EmailAddresses)
                {
                    DetailObject temp = new DetailObject();
                    temp.IconSource = path + ForwardContact_Constant.ICON_EMAIL;
                    temp.Content = "Email (" + acct.Kind + "): " + acct.EmailAddress;
                    detail.Add(temp);
                }
            }

            //Account
            if (selected.Accounts != null && selected.Accounts.Count() != 0)
            {
                foreach(Account acct in selected.Accounts)
                {                    
                    DetailObject temp = new DetailObject();
                    temp.IconSource = path + ForwardContact_Constant.ICON_ACCOUNT;
                    temp.Content = "Account: "+acct.Name;
                    detail.Add(temp);
                }
            }

            //Address
            if (selected.Addresses != null && selected.Addresses.Count() != 0)
            {
                foreach (ContactAddress acct in selected.Addresses)
                {
                    DetailObject temp = new DetailObject();
                    temp.IconSource = path + ForwardContact_Constant.ICON_ADDRESS;
                    temp.Content ="Address "+"("+acct.Kind+"): " + acct.PhysicalAddress.AddressLine1 + " " + acct.PhysicalAddress.AddressLine2 + " " + acct.PhysicalAddress.City + " "
                        + acct.PhysicalAddress.StateProvince + " " + acct.PhysicalAddress.CountryRegion + " " + acct.PhysicalAddress.PostalCode;
                    detail.Add(temp);
                }
            }

            //Birthday
            if (selected.Birthdays != null && selected.Birthdays.Count() != 0)
            {
                foreach (DateTime acct in selected.Birthdays)
                {
                    DetailObject temp = new DetailObject();
                    temp.IconSource = path + ForwardContact_Constant.ICON_BIRTHDAY;
                    temp.Content = "Birthday: " + acct.ToLongDateString();
                    detail.Add(temp);
                }
            }
                        
            //Company
            if (selected.Companies != null && selected.Companies.Count() != 0)
            {
                foreach (ContactCompanyInformation acct in selected.Companies)
                {
                    DetailObject temp = new DetailObject();
                    temp.IconSource = path + ForwardContact_Constant.ICON_COMPANY;
                    temp.Content = "Company: " + acct.CompanyName ;
                    if (acct.JobTitle != "" && acct.JobTitle!=null) temp.Content += " (" + acct.JobTitle + ")";
                    detail.Add(temp);
                }
            }

            //Websites
            if (selected.Websites != null && selected.Websites.Count() != 0)
            {
                foreach (String acct in selected.Websites)
                {
                    DetailObject temp = new DetailObject();
                    temp.IconSource = path + ForwardContact_Constant.ICON_SIGNNIFICANT;
                    temp.Content = "Website: " + acct;
                    detail.Add(temp);
                }
            }

            //Children
            if (selected.Children != null && selected.Children.Count() != 0)
            {
                foreach (String acct in selected.Children)
                {
                    DetailObject temp = new DetailObject();
                    temp.IconSource = path + ForwardContact_Constant.ICON_BIRTHDAY;
                    temp.Content = "Children: " + acct;
                    detail.Add(temp);
                }
            }

            //SignificantOthers
            if (selected.SignificantOthers != null && selected.SignificantOthers.Count() != 0)
            {
                foreach (String acct in selected.SignificantOthers)
                {
                    DetailObject temp = new DetailObject();
                    temp.IconSource = path + ForwardContact_Constant.ICON_SIGNNIFICANT;
                    temp.Content = "Significant Others: " + acct;
                    detail.Add(temp);
                }
            }

            //Notes
            if (selected.Notes != null && selected.Notes.Count() != 0)
            {
                foreach (String acct in selected.Notes)
                {
                    DetailObject temp = new DetailObject();
                    temp.IconSource = path + ForwardContact_Constant.ICON_NOTES;
                    temp.Content = "Note: " + acct;
                    detail.Add(temp);
                }
            }
           
            listDetail.ItemsSource = detail;
        }
        private void btnContacts_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/CustomContactChooserTask.xaml", UriKind.Relative));
        }

       
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //Contacts contacts = new Contacts();
            //contacts.SearchCompleted += new EventHandler<ContactsSearchEventArgs>(contacts_SearchCompleted);
            //contacts.SearchAsync(displayName, FilterKind.DisplayName, null);

            //search for all contacts
            //contacts.SearchAsync(string.Empty, FilterKind.None, null);

        }

       
        private void SendSMS_Click(object sender, EventArgs e)
        {
            SmsComposeTask sms = new SmsComposeTask();
            String smsContent = "";
            foreach (DetailObject obj in listDetail.SelectedItems)
            {
                smsContent += obj.Content + ";\r\n";
            }
            sms.Body = smsContent;
            sms.Show();
        }

        private void SendEmail_Click(object sender, EventArgs e)
        {
            EmailComposeTask email = new EmailComposeTask();
            email.Subject = "Contact Info of "+ this.tbName.Text;
            String emailContent = "";
            foreach (DetailObject obj in listDetail.SelectedItems)
            {
                emailContent += obj.Content + ";\r\n";
            }
            email.Body = emailContent;
            email.Show();
        }
             
        
        private void Import_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/ImportContact.xaml", UriKind.Relative));
        }

        private Boolean isLightTheme()
        {
            var v = (Visibility)Resources["PhoneLightThemeVisibility"];
            if (v == Visibility.Visible) return true;
            else return false;
        }

        private void About_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AppInfoPage.xaml", UriKind.Relative));
        }
        #region Advertisement
        DispatcherTimer mTimer = new System.Windows.Threading.DispatcherTimer();
        private void AdControl_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            AdControlPanel.Visibility = Visibility.Collapsed;
            listDetail.Height = 410;
            mTimer.Start();
        }

        void mTimer_Tick(object sender, EventArgs e)
        {
            AdControlPanel.Visibility = Visibility.Visible;
            listDetail.Height = 390;
        }
        #endregion
    }
}