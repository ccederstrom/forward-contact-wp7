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

namespace Forward_Contact.model
{
    public class DetailObject
    {
        public String IconSource { get; set; }
        public String Content { get; set; }
        public Boolean isSelected { get; set; }
        public DetailObject()
        {
            isSelected = false;
        }
    }
}
