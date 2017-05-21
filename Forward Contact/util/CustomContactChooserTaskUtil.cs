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
using System.Threading;
using Microsoft.Phone.Shell;


namespace Forward_Contact.util
{
    public class CustomContactChooserTaskUtil
    {
        private static PhoneApplicationPage callerPage; 
        public static void initialContactList(PhoneApplicationPage page)
        {
            doIntialContactList(page);
        }
        private static void doIntialContactList(PhoneApplicationPage page)
        {
            if (App.contactList == null)
            {
                callerPage = page;
                Contacts contacts = new Contacts();
                contacts.SearchCompleted += new EventHandler<ContactsSearchEventArgs>(Allcontacts_SearchCompleted);
                contacts.SearchAsync(string.Empty, FilterKind.None, null);
            }
            else
            {
                Debug.WriteLine("Load from App.cs");
            }
        }
        private static void Allcontacts_SearchCompleted(object sender, ContactsSearchEventArgs e)
        {
            List<ExtendedContact> extcontacts = new List<ExtendedContact>();
            foreach (Contact ext in e.Results)
            {
                extcontacts.Add(new ExtendedContact(ext));
            }
            var items = from extcontact in extcontacts
                        group extcontact by getGroupName(extcontact.contact.DisplayName) into c
                        orderby c.Key
                        select new Group<ExtendedContact>(c.Key, c);
            App.contactList = items;
            if (callerPage is MainPage) 
            { 
                ((MainPage)callerPage).tbLoading.Visibility = Visibility.Collapsed;
                ((MainPage)callerPage).btnChooseCOntact.IsEnabled = true;
                //((MainPage)callerPage).ApplicationBar.IsMenuEnabled = true;
            }
        }
        private static String getGroupName(String displayName)
        {
            Char initialCharacter = displayName.ElementAt(0);
            if (Char.IsLetter(initialCharacter) == false)
            {
                return "#";
            }
            else
            {
                return initialCharacter.ToString().ToLower();
            }
        }
    }
}
