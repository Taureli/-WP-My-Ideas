using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;

namespace myIdeas
{
    public partial class NewCat : PhoneApplicationPage
    {
        public NewCat()
        {
            InitializeComponent();
        }

        private void NewCatName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (NewCatName.Text == "New category")
            {
                NewCatName.Text = "";
                SolidColorBrush Brush1 = new SolidColorBrush();
                Brush1.Color = Colors.Black;
                NewCatName.Foreground = Brush1;
            }
        }

        private void NewCatName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (NewCatName.Text == String.Empty)
            {
                NewCatName.Text = "New category";
                SolidColorBrush Brush2 = new SolidColorBrush();
                Brush2.Color = Colors.Gray;
                NewCatName.Foreground = Brush2;
            }
        }

        private void NewCatName_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                NewCatButton.Focus();
        }

        private void NewCatButton_Click(object sender, RoutedEventArgs e)
        {
            if (NewCatName.Text != "")
            {
                using (IdeasContext ctx = new IdeasContext(IdeasContext.ConnectionString))
                {
                    ctx.CreateIfNotExists();
                }
            }
        }
    }
}