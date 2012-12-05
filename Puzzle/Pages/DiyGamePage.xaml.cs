using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using Puzzle.Vo;
using System.IO;
using Puzzle.Utils;
using System.Windows.Media.Imaging;

namespace Puzzle.Pages {
    public partial class DiyGamePage : PhoneApplicationPage {
        const int MARGIN = 2;
        const string TILE_PAGE_URI = "/Pages/TilePage.xaml";

        private PhotoChooserTask photoChooser = new PhotoChooserTask();
        private TileSizeVo size = new TileSizeVo(3, 3);

        public DiyGamePage() {
            InitializeComponent();

            photoChooser.Completed += OnPhotoChooserTaskCompleted;
        }

        private void OnPhotoChooserTaskCompleted(object sender, PhotoResult e) {
            if (e.Error == null && e.ChosenPhoto != null) {
                SetTilePhoto(e.ChosenPhoto);
            }
        }

        private void OnChoosePhotoBtnClick(object sender, RoutedEventArgs e) {
            SetPhotoChooser(photoChooser, false);
            photoChooser.Show();
        }

        private void OnTakePhotoBtnClick(object sender, RoutedEventArgs e) {
            SetPhotoChooser(photoChooser, true);
            photoChooser.Show();
        }

        private void SetPhotoChooser(PhotoChooserTask photoChooser, bool showCamera) {
            TileSizeVo size = GetTileSize();
            photoChooser.PixelHeight = size.Horz;
            photoChooser.PixelWidth = size.Vert;
            photoChooser.ShowCamera = showCamera;
        }

        private TileSizeVo GetTileSize() {
            //int tileSize = (int)Math.Min(playPanel.ActualWidth / this.size.Horz, playPanel.ActualHeight / this.size.Vert) - 2 * MARGIN;
            double actualWidth = 420, actualHeight = 575;
            int tileSize = (int)Math.Min(actualWidth / this.size.Horz, actualHeight / this.size.Vert) - 2 * MARGIN;
            return new TileSizeVo(tileSize * this.size.Vert, tileSize * this.size.Horz);
        }

        private void SetTilePhoto(Stream stream) {
            WriteableBitmap originalBitmap = ImageUtils.toWriteableBitmap(stream);
            (Application.Current as App).CurrentPhoto = originalBitmap;

            this.tiles33Btn.IsEnabled = true;
            this.tiles44Btn.IsEnabled = true;
        }

        private void OnTiles33BtnClick(object sender, RoutedEventArgs e) {
            (Application.Current as App).TileSizeVo = new TileSizeVo(3, 3);
            NavigationToTilePage();
        }

        private void OnTiles44BtnClick(object sender, RoutedEventArgs e) {
            (Application.Current as App).TileSizeVo = new TileSizeVo(4, 4);
            NavigationToTilePage();
        }

        void NavigationToTilePage() {
            this.NavigationService.Navigate(new Uri(TILE_PAGE_URI, UriKind.Relative));
        }
    }
}