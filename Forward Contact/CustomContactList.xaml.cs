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

namespace Forward_Contact
{
    public partial class CustomContactList : PhoneApplicationPage
    {
        public CustomContactList()
        {
            InitializeComponent();
            List<City> source = new List<City>();
            source.Add(new City() { Name = "Madrid", Country = "ES", Language = "Spanish" });
            source.Add(new City() { Name = "Barcelona", Country = "ES", Language = "Spanish" });
            source.Add(new City() { Name = "Mallorca", Country = "ES", Language = "Spanish" });
            source.Add(new City() { Name = "Las Vegas", Country = "US", Language = "English" });
            source.Add(new City() { Name = "Dalas", Country = "US", Language = "English" });
            source.Add(new City() { Name = "New York", Country = "US", Language = "English" });
            source.Add(new City() { Name = "London", Country = "UK", Language = "English" });
            source.Add(new City() { Name = "Mexico", Country = "MX", Language = "Spanish" });
            source.Add(new City() { Name = "Milan", Country = "IT", Language = "Italian" });
            source.Add(new City() { Name = "Roma", Country = "IT", Language = "Italian" });
            source.Add(new City() { Name = "Paris", Country = "FR", Language = "French" });


            var cityByCountry = from city in source
                                group city by city.Country into c
                                orderby c.Key
                                select new Group<City>(c.Key, c);

            this.citiesListGropus.ItemsSource = cityByCountry;
        }

    }
    public class City
    {
        public string Name
        {
            get;
            set;
        }

        public string Country
        {
            get;
            set;
        }

        public string Language
        {
            get;
            set;
        }
    }
    public class Group<T> : IEnumerable<T> 
        { 
            public Group(string name, IEnumerable<T> items) 
            { 
                this.Title = name; 
                this.Items = new List<T>(items); 
            } 
 
            public override bool Equals(object obj) 
            { 
                Group<T> that = obj as Group<T>; 
 
                return (that != null) && (this.Title.Equals(that.Title)); 
            } 
 
            public string Title 
            { 
                get; 
                set; 
            } 
 
            public IList<T> Items 
            { 
                get; 
                set; 
            } 
            #region IEnumerable<T> Members 
 
            public IEnumerator<T> GetEnumerator() 
            { 
                return this.Items.GetEnumerator(); 
            } 
            #endregion 
            #region IEnumerable Members 
 
            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() 
            { 
                return this.Items.GetEnumerator(); 
            } 
            #endregion 
        } 
}