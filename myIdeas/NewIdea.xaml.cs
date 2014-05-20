using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Diagnostics;
using System.Windows.Media;

namespace myIdeas
{
    public partial class NewIdea : PhoneApplicationPage
    {
        public NewIdea()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            using (IdeasContext ctx = new IdeasContext(IdeasContext.ConnectionString))
            {
                ctx.CreateIfNotExists();

                ctx.LogDebug = true;

                this.lpkCategories.ItemsSource = ctx.Categories.OrderBy(d => d.Name).Select(d => d.Name).ToList();
            }

        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            CreateIdea();
        }

        private void IdeaTitle_GotFocus(object sender, RoutedEventArgs e)
        {
            if (IdeaTitle.Text == "Title")
            {
                IdeaTitle.Text = "";
                SolidColorBrush Brush1 = new SolidColorBrush();
                Brush1.Color = Colors.Black;
                IdeaTitle.Foreground = Brush1;
            }
        }

        private void IdeaTitle_LostFocus(object sender, RoutedEventArgs e)
        {
            if (IdeaTitle.Text == String.Empty)
            {
                IdeaTitle.Text = "Title";
                SolidColorBrush Brush2 = new SolidColorBrush();
                Brush2.Color = Colors.Gray;
                IdeaTitle.Foreground = Brush2;
            }
        }

        private void IdeaContent_GotFocus(object sender, RoutedEventArgs e)
        {
            if (IdeaContent.Text == "My Idea")
            {
                IdeaContent.Text = "";
                SolidColorBrush Brush1 = new SolidColorBrush();
                Brush1.Color = Colors.Black;
                IdeaContent.Foreground = Brush1;
            }
        }

        private void IdeaContent_LostFocus(object sender, RoutedEventArgs e)
        {
            if (IdeaContent.Text == String.Empty)
            {
                IdeaContent.Text = "My Idea";
                SolidColorBrush Brush2 = new SolidColorBrush();
                Brush2.Color = Colors.Gray;
                IdeaContent.Foreground = Brush2;
            }
        }

        private void CreateIdea()
        {

            if (IdeaTitle.Text.Length > 250)
            {
                MessageBox.Show("Idea title is too long!");
            }
            else if (IdeaContent.Text.Length > 3990)
            {
                MessageBox.Show("Sorry, idea content is too long!");
            }
            else if (IdeaTitle.Text.Length > 0 && IdeaContent.Text.Length > 0)
            {
                using (IdeasContext ctx = new IdeasContext(IdeasContext.ConnectionString))
                {
                    ctx.CreateIfNotExists();

                    int CatId = (from p in ctx.Categories where p.Name == lpkCategories.SelectedItem.ToString() select p.Id).Single();

                    var idea = new Ideas() { Title = IdeaTitle.Text, Cat_id = CatId, Content = IdeaContent.Text };

                    ctx.Ideas.InsertOnSubmit(idea);
                    ctx.SubmitChanges();

                    NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                }
            }
        }

        private void IdeaTitle_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                Focus();
            }
        }

    }
}