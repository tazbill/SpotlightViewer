using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Drawing;
using SpotlightViewer.Model;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace SpotlightViewer.ViewModels
{
    public class SpotlightVM : INotifyPropertyChanged
    {
        private const string SPOTLIGHT_PATH = @"Packages\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\LocalState\Assets";
        private string _localApplicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private string _temporaryImagePath = Path.Combine( Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"SpotlightViewer\Images");
        private string _temporaryThumbPath = Path.Combine( Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"SpotlightViewer\Thumbs");
        private string _wallpaperPath = ConfigurationManager.AppSettings["WallpaperDirectory"];
        private int _totalFileCount = 0;
        private int _currentFileCount = 0;
        private ObservableCollection<SpotlightImageFile> _spotlightImages;
        private SpotlightImageFile _selectedFile;


        public event EventHandler ProgressStarted;
        public event EventHandler ProgressComplete; 


        public int TotalFileCount
        {
            get { return this._totalFileCount; }
            set
            {
                this._totalFileCount = value;
                OnPropertyChanged("TotalFileCount");
            }
        }

        public int ProcessedFileCount
        {
            get { return this._currentFileCount; }
            set
            {
                this._currentFileCount = value;
                OnPropertyChanged("ProcessedFileCount");
            }
        }

        public ObservableCollection<SpotlightImageFile> SpotlightImages
        {
            get { return this._spotlightImages; }
            set
            {
                this._spotlightImages = value;
                OnPropertyChanged("SpotlightImages");
            }
        }

        public SpotlightImageFile SelectedImage
        {
            get { return this._selectedFile; }
            set
            {
                this._selectedFile = value;
                OnPropertyChanged("SelectedImage");
                OnPropertyChanged("ShowSaveButton");
            }
        }

        public bool ShowSaveButton
        {
            get { return this._selectedFile != null; }
        }



        public SpotlightVM()
        {
            _spotlightImages = new ObservableCollection<SpotlightImageFile>();
            _Bootstrap();
        }



        public void ResetProcessingProgress()
        {
            this.TotalFileCount = 0;
            this.ProcessedFileCount = 0;
        }

        public void GenerateTemporaryImages()
        {
            _GenerateTemporaryImages();
        }

        public void SaveSelectedImage(string filepath)
        {
            _SaveImageToDisk(this.SelectedImage.FullImage, filepath);
        }





        private void _GenerateTemporaryImages()
        {

            var spotlightDirectory = new DirectoryInfo(Path.Combine(_localApplicationDataPath, SPOTLIGHT_PATH));
            var spotlightFiles = spotlightDirectory.GetFiles().Where(f => f.CreationTime > _GetDateToStartFrom()).ToList();

            _ClearTemporaryStorage();

            if ( spotlightFiles.Any() )
            {
                this.TotalFileCount = spotlightFiles.Count;
                if (ProgressStarted != null) ProgressStarted(this, new EventArgs());

                foreach (var file in spotlightFiles)
                {
                    Image img = null;
                    bool isLandscape = false;
                    var filename = Path.Combine(_temporaryImagePath, file.Name + ".jpg");
                    var thumbnailName = Path.Combine(_temporaryThumbPath, file.Name + ".jpg");

                    try { img = Image.FromFile(file.FullName); }
                    catch (Exception ex ) { img = null; }
                    
                    if (img != null)
                    {
                        isLandscape = (img.Width > img.Height);

                        if (isLandscape)
                        {
                            var thumbnailWidth = 200;
                            var thumbnailHeight = (double)(thumbnailWidth * img.Height / img.Width);
                            Image thumbnail = (Image)(new Bitmap(img, new Size(thumbnailWidth, (int)thumbnailHeight)));

                            SpotlightImages.Add(new SpotlightImageFile()
                            {
                                ImageFileName = file.Name,
                                FullImage = img,
                                Thumbnail = thumbnail
                            });

                            img.Save(filename);
                            thumbnail.Save(thumbnailName);
                            this.ProcessedFileCount++;
                        }
                    }
                }

                if (ProgressComplete != null) ProgressComplete(this, new EventArgs());
            }
            ResetProcessingProgress();
        }

        private DateTime _GetDateToStartFrom()
        {
            //var currentWallpaperDir = new DirectoryInfo(_wallpaperPath);
            //var currentWallpaperFiles = currentWallpaperDir.GetFiles();
            //return currentWallpaperFiles.Max(f => f.CreationTime);
            int daysToLookBack = Convert.ToInt32(ConfigurationManager.AppSettings["DaysToLookBack"]) * -1;

            return DateTime.Today.AddDays(daysToLookBack);
        }

        private void _ClearTemporaryStorage()
        {
            var tempImageDir = new DirectoryInfo(_temporaryImagePath);
            var tempThumbDir = new DirectoryInfo(_temporaryThumbPath);

            List<FileInfo> tempFiles = new List<FileInfo>();
            tempFiles.AddRange(tempImageDir.GetFiles());
            tempFiles.AddRange(tempThumbDir.GetFiles());

            this.TotalFileCount = tempFiles.Count;
            if (ProgressStarted != null) ProgressStarted(this, new EventArgs());

            foreach(var file in tempFiles)
            {
                file.Delete();
                this.ProcessedFileCount++;
            }

            if (ProgressComplete != null) ProgressComplete(this, new EventArgs());
        }

        private void _SaveImageToDisk( Image img, string path )
        {
            img.Save(path);

        }

        private void _Bootstrap()
        {
            if (!Directory.Exists(_temporaryImagePath))
                Directory.CreateDirectory(_temporaryImagePath);
            if (!Directory.Exists(_temporaryThumbPath))
                Directory.CreateDirectory(_temporaryThumbPath);
        }



        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
