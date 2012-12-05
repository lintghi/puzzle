using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Media.Imaging;

namespace Puzzle.Utils {
    public class ImageUtils {
        public static WriteableBitmap toWriteableBitmap(Stream stream) {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.SetSource(stream);
            return new WriteableBitmap(bitmapImage);
        }

        public static WriteableBitmap toWriteableBitmap(Stream stream, int pixelWidth, int pixelHeight) {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.SetSource(stream);

            WriteableBitmap bitmap = new WriteableBitmap(pixelWidth, pixelWidth);
            bitmap.SetSource(stream);
            return bitmap;
        }
    }
}
