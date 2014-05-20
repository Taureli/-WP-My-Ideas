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
    public partial class Category : PhoneApplicationPage
    {

        int CatId;

        public Category()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (NavigationContext.QueryString.ContainsKey("SelectedCat"))
            {
                CatId = Convert.ToInt16(NavigationContext.QueryString["SelectedCat"]);
            }

            using (IdeasContext ctx = new IdeasContext(IdeasContext.ConnectionString))
            {
                ctx.CreateIfNotExists();

                ctx.LogDebug = true;

                this.IdeasList.ItemsSource = ctx.Ideas.Where(d => d.Cat_id == CatId).ToList();

                PageTitle.DataContext = (from p in ctx.Categories where p.Id == CatId select p.Name).Single();
            }

        }

        private void IdeaButton_Click(object sender, RoutedEventArgs e)
        {
            Button _button = (Button)sender;
            String selectedIdea = _button.Tag.ToString();

            NavigationService.Navigate(new Uri("/Idea.xaml?SelectedIdea=" + selectedIdea, UriKind.Relative));
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {

        }
    }
}