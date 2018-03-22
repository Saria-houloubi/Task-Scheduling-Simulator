using System;
using System.IO;
using System.Windows;

namespace TishreenUniversity.ParallelPro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Set the video path by Getting current working folder and attach video path to it
            var backgroundPath = $"{Directory.GetCurrentDirectory()}/Images/BackgroundVideo.wmv";
            //Set the source of the media item
            mediaItem.Source = new Uri(backgroundPath);
        }
    }
}
