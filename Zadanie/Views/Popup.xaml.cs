using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Zadanie.Views
{
    /// <summary>
    /// Logika interakcji dla klasy Popup.xaml
    /// </summary>
    public partial class Popup : Window
    {
        public Popup(string Title,string Text)
        {
            InitializeComponent();
            this.Title = Title;
            PopupText.Text = Text;
            this.Show();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
