using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SpotlightViewer.ViewModels;
using SpotlightViewer.Model;
using Microsoft.Win32;

namespace SpotlightViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SpotlightVM _viewModel;


        public MainWindow()
        {
            _setupVM();
            InitializeComponent();
            this.DataContext = _viewModel;
        }



        private void _setupVM()
        {
            _viewModel = new SpotlightVM();
            _viewModel.ProgressStarted += _viewModel_ProgressStarted;
            _viewModel.ProgressComplete += _viewModel_ProgressComplete;
            _viewModel.PropertyChanged += _viewModel_PropertyChanged;
        }

        private void _viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case "TotalFileCount":
                case "ProcessedFileCount":
                    break;
                default:
                    break;
            }
        }

        private void _viewModel_ProgressComplete(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void _viewModel_ProgressStarted(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void ThumbnailViewer_Selected(object sender, RoutedEventArgs e)
        {
            _viewModel.SelectedImage = (SpotlightImageFile)this.ThumbnailViewer.SelectedItem;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel.GenerateTemporaryImages();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "JPG (*.jpg)|*.jpg";
            sfd.DefaultExt = "jpg";
            sfd.AddExtension = true;
            var res = sfd.ShowDialog();
            if (res == true)
            {
                _viewModel.SaveSelectedImage(sfd.FileName);
            }
        }
    }
}
