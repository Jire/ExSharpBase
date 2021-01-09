using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace ExSharpBase.Modules
{
    internal static class PixelSearch
    {
        public static Point[] Search(Rectangle rect, Color pixelColor, int shadeVariation)
        {
            var points = new List<Point>();
            var getScreenShot = ScreenEx.GetScreenCapture(rect);
            var regionInBitmapData =
                getScreenShot.LockBits(new Rectangle(0, 0, getScreenShot.Width, getScreenShot.Height),
                    ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            var formattedColor = new int[] {pixelColor.B, pixelColor.G, pixelColor.R};

            unsafe
            {
                for (var y = 0; y < regionInBitmapData.Height; y++)
                {
                    var row = (byte*) regionInBitmapData.Scan0 + (y * regionInBitmapData.Stride);
                    for (var x = 0; x < regionInBitmapData.Width; x++)
                    {
                        if (!(row[x * 3] >= (formattedColor[0] - shadeVariation) &
                              row[x * 3] <= (formattedColor[0] + shadeVariation))) continue;
                        if (!(row[(x * 3) + 1] >= (formattedColor[1] - shadeVariation) &
                              row[(x * 3) + 1] <= (formattedColor[1] + shadeVariation))) continue;
                        if (row[(x * 3) + 2] >= (formattedColor[2] - shadeVariation) &
                            row[(x * 3) + 2] <= (formattedColor[2] + shadeVariation)) //red
                            points.Add(new Point(x + rect.X, y + rect.Y));
                    }
                }
            }

            getScreenShot.Dispose();
            return points.ToArray();
        }
    }
}