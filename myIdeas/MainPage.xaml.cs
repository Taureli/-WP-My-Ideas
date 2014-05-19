using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using myIdeas.Resources;
using myIdeas.ViewModels;

namespace myIdeas
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            using(IdeasContext ctx = new IdeasContext(IdeasContext.ConnectionString))
            {
                ctx.CreateIfNotExists();

                ctx.LogDebug = true;

                CategoriesList.ItemsSource = ctx.Categories.ToList();
            }
        }

        private void Category_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}