using System.IO;
using System.Drawing;
using System;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Interop;
using System.Windows.Media;

namespace OthelloAlainGabriel
{
    //Classe définissant un token
    public class Token
    {
        //public string PathToToken { get; }
        public ImageBrush ImgBrush { get; }
        
        public Token(string path)
        {
            //PathToToken = path;
            Console.WriteLine(path);
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
