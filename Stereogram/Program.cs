using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Stereogram
{
    class Program
    {
        static void Main(string[] args)
        {
            int rows = 600;
            int cols = 800;
            int width = 80;
            int realCols = cols - 2 * width;

            Bitmap origBmp = new Bitmap(realCols, rows);
            int[,] orig = new int[rows, realCols];
            for (int row = 0; row < rows; row++)
                for (int col = 0; col < realCols; col++)
                {
                    double radius = Math.Sqrt((rows / 2 - row) * (rows / 2 - row) + (realCols / 2 - col) * (realCols / 2 - col));

                    int v = (int)Math.Round(100 * (Math.Cos(radius * Math.PI / 50) + 1) * Math.Exp(-radius / 300));

                    orig[row, col] = v;

                    origBmp.SetPixel(col, row, Color.FromArgb(v, v, v));
                }
            origBmp.Save(@"E:\Veggie\orig.bmp");

            Random rand = new Random();
            Color[,] chunk = new Color[rows, width];
            for (int row = 0; row < rows; row++)
                for (int col = 0; col < width; col++)
                {
                    int r = rand.Next(256);
                    int g = rand.Next(256);
                    int b = rand.Next(256);

                    chunk[row, col] = Color.FromArgb(r, g, b);
                }

            Color[,] image = new Color[rows, cols];
            Color[,] image2 = new Color[rows, cols];
            for (int row = 0; row < rows; row++)
                for (int col = 0; col < cols; col++)
                {
                    image[row, col] = chunk[row, col % width];
                    image2[row, col] = chunk[row, col % width];
                }

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < realCols; col++)
                {
                    image2[row, col + width + orig[row, col]] = image2[row, col + width - orig[row, col]];
                }
            }

            Bitmap bmp = new Bitmap(cols, rows);
            for (int row = 0; row < rows; row++)
                for (int col = 0; col < cols; col++)
                {
                    bmp.SetPixel(col, row, image2[row, col]);
                }

            bmp.Save(@"E:\Veggie\test.bmp");
        }
    }
}
