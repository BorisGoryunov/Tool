using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public static class StreamHelper
    {
        public static Stream BitmapToStream(Bitmap bitmap)
        {
            var stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Bmp);
            stream.Position = 0;
            byte[] image = new byte[stream.Length];
            stream.Read(image, 0, image.Length);
            return stream;
        }
    }
}
