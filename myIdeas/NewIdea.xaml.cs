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

        }

    }
}