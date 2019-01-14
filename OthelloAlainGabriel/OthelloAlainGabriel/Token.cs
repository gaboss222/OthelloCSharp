using System.Drawing;
using System;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Interop;
using System.Windows.Media;

namespace OthelloAlainGabriel
{
    /// <summary>
    /// Token class
    /// Token = path = imgBrushes
    /// </summary>
    public class Token
    {
        public ImageBrush ImgBrush { get; }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="path">Path to token</param>
        public Token(string path)
        {
            Image img = Image.FromFile(path);
            Bitmap bmp = new Bitmap(img);
            var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(bmp.GetHbitmap(),
                                                                            IntPtr.Zero,
                                                                            Int32Rect.Empty,
                                                                            BitmapSizeOptions.FromEmptyOptions()
            );
            bmp.Dispose();
            ImgBrush = new ImageBrush(bitmapSource);
        }
    }
}