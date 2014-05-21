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
using Microsoft.Advertising.Mobile.UI;

namespace myIdeas
{
    public partial class MainPage : PhoneApplicationPage
    {

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            if (App.IsTrial)
            {
                AdControl.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                AdControl.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            using(IdeasContext ctx = new IdeasContext(IdeasContext.ConnectionString))
            {
                ctx.CreateIfNotExists();

                CategoriesList.ItemsSource = ctx.Categories.OrderBy(d => d.Name).ToList();
            }

        }

        private void Category_Click(object sender, RoutedEventArgs e)
        {
            Button _button = (Button)sender;
            String selectedCat = _button.Tag.ToString();

            NavigationService.Navigate(new Uri("/Category.xaml?SelectedCat=" + selectedCat, UriKind.Relative));
        }

        private void AddCategoryButton(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/NewCat.xaml", UriKind.Relative));
        }

        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/About.xaml", UriKind.Relative));
        }

        private void AddIdeaClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/NewIdea.xaml", UriKind.Relative));
        }

        private void RemoveCategory_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/DelCat.xaml", UriKind.Relative));
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/NewIdea.xaml", UriKind.Relative));
        }

        private void ApplicationBarMenuItem_Click_1(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("http://windowsphone.com/s?appId=8e0ecdc7-7f86-4cba-a9c2-9246b794798e"));
        }

    }
}