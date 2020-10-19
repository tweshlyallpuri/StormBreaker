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

namespace StormBreaker.Views
{
    /// <summary>
    /// Interaction logic for SplashWindow.xaml
    /// </summary>
    public partial class SplashWindow : Window
    {
        public SplashWindow()
        {
            InitializeComponent();
            SplashMediaElement.Source = new Uri(@"D:\Dev\GitHub\StormBreaker\StormBreaker\Views\Images\StormBreaker.gif");
            
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            SplashMediaElement.Play();
        }

        private void SplashMediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            SplashMediaElement.Position = new TimeSpan(0, 0, 1);
            SplashMediaElement.Play();
        }
    }
}
