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
    public partial class Idea : PhoneApplicationPage
    {

        int IdeaId;

        public Idea()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (NavigationContext.QueryString.ContainsKey("SelectedIdea"))
            {
                IdeaId = Convert.ToInt16(NavigationContext.QueryString["SelectedIdea"]);
            }

            using (IdeasContext ctx = new IdeasContext(IdeasContext.ConnectionString))
            {
                ctx.CreateIfNotExists();

                ctx.LogDebug = true;

                PageTitle.DataContext = (from p in ctx.Ideas where p.Id == IdeaId select p.Title).Single();
                IdeaContent.DataContext = (from p in ctx.Ideas where p.Id == IdeaId select p.Content).Single();

            }

        }

    }
}