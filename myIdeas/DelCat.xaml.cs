using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace myIdeas
{
    public partial class DelCat : PhoneApplicationPage
    {
        public DelCat()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            using (IdeasContext ctx = new IdeasContext(IdeasContext.ConnectionString))
            {
                ctx.CreateIfNotExists();

                DeleteCatList.ItemsSource = ctx.Categories.Where(d => d.Isdefault == 0).Where(d => d.Ideas.Count == 0).ToList();

            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            using (IdeasContext ctx = new IdeasContext(IdeasContext.ConnectionString))
            {
                Button _button = (Button)sender;

                var bla = (from p in ctx.Categories where p.Id == (int)_button.Tag select p).Single();

                ctx.Categories.DeleteOnSubmit(bla);
                ctx.SubmitChanges();

                DeleteCatList.ItemsSource = ctx.Categories.Where(d => d.Isdefault == 0).Where(d => d.Ideas.Count == 0).ToList();
            }
        }
    }
}