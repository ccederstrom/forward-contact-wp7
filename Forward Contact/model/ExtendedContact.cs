using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using Microsoft.Phone.UserData;

namespace Forward_Contact.model
{
    public class ExtendedContact
    {
        public BitmapImage pic { get; set; }
        public String name { get; set; }
        public Contact contact { get; set; }
        public ExtendedContact(Contact c)
        {
            contact = c;
            pic = new BitmapImage();
            if (c.GetPicture() != null)
            {
                pic.SetSource(c.GetPicture());
            }
            else
            {
                pic = new BitmapImage(new Uri("/Images/emptyAvatar.png", UriKind.RelativeOrAbsolute));
            }
            name = c.DisplayName;
        }
    }
}
