using PatriaTerram.Core.BuildingConditions;
using PatriaTerram.Core.Factoryes;
using PatriaTerram.Core.Helpers;
using PatriaTerram.Core.Models;
using PerlinNoise;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatriaTerram.MapObserver
{
    public partial class Form1 : Form
    {
        private int[][] _matrix;
        private PerlinNoiseGenerator _generator;

        public Form1()
        {
            InitializeComponent();

            _generator = new PerlinNoiseGenerator(seed: 666, maxValue: 256);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            int size = int.Parse(sizeComboBox.SelectedItem as string);
            int smoothingSize = int.Parse(SmoothingSizeTextBox.Text as string);

            _matrix = _generator.GetPerlinNoiseMatrix(size, smoothingSize);

            _matrix = _matrix.StretchOnMaximumAndMinimumValue(10, 250).Smoothing();

            UpdateImage();
        }

        private void exponentiationButton_Click(object sender, EventArgs e)
        {
            if(_matrix == null) { return; }

            _matrix = _matrix.Exponentiation(256);
            
            UpdateImage();
        }

        private void UpdateImage()
        {
            if (_matrix == null) { return; }

            Bitmap image = new Bitmap(_matrix.Length, _matrix.Length);

            for (int x = 0; x < _matrix.Length; x++)
            {
                for (int y = 0; y < _matrix[0].Length; y++)
                {
                    image.SetPixel(x, y, Color.FromArgb(_matrix[x][y], _matrix[x][y], _matrix[x][y]));
                }
            }

            mapPictureBox.Image = image;
        }

        private void clearLowValueButton_Click(object sender, EventArgs e)
        {
            if (_matrix == null) { return; }

            int lowEdge = int.Parse(lowEdgeTextBox.Text);

            _matrix = _matrix.ClearBottomValue(lowEdge);

            UpdateImage();
        }

        private void clearHighValueButton_Click(object sender, EventArgs e)
        {
            if (_matrix == null) { return; }

            int highEdge = int.Parse(highEdgeTextBox.Text);

            _matrix = _matrix.ClearTopValue(highEdge);

            UpdateImage();
        }

        private void paletteButton_Click(object sender, EventArgs e)
        {
            int size = int.Parse(sizeComboBox.SelectedItem as string);
            int seed = int.Parse(seedTextBox.Text as string);
            int oceanEdge = int.Parse(oceanEdgeTextBox.Text as string);
            int mountainsEdge = int.Parse(mountainsTextBox.Text as string);

            int fertileSoilBottomEdge = int.Parse(fertileSoilBottomEdgeTextBox.Text as string);
            int fertileSoilTopEdge = int.Parse(fertileSoilTopEdgeTextBox.Text as string);

            int woodBottomEdge = int.Parse(woodBottomEdgeTextBox.Text as string);
            int woodTopEdge = int.Parse(woodTopEdgeTextBox.Text as string);

            int stoneBottomEdge = int.Parse(stoneBottomEdgeTextBox.Text as string);
            int stoneTopEdge = int.Parse(stoneTopEdgeTextBox.Text as string);

            int lakeBottomEdge = int.Parse(lakeBottomEdgeTextBox.Text as string);
            int lakeTopEdge = int.Parse(lakeTopEdgeTextBox.Text as string);

            int beachSize = int.Parse(beachSizeTextBox.Text as string);

            int pixelSize = int.Parse(pixelSizeTextBox.Text as string);
            int smoothingSize = int.Parse(SmoothingSizeTextBox.Text as string);

            var factory = new TerrainPaletteFactory(size, size, 
                seed,
                oceanEdge, 
                mountainsEdge,
                fertileSoilBottomEdge,
                fertileSoilTopEdge,
                woodBottomEdge,
                woodTopEdge,
                stoneBottomEdge,
                stoneTopEdge,
                lakeBottomEdge,
                lakeTopEdge,
                beachSize
                );

            var palette = factory.GetPalette();

            mapPictureBox.Image = GetBitmap(palette, pixelSize);
        }

        private Bitmap GetBitmap(Palette palette, int multiplayer)
        {
            var image = new Bitmap(palette.Width * multiplayer, palette.Height * multiplayer);

            for (int x = 0; x < palette.Width; x++)
            {
                for (int y = 0; y < palette.Height; y++)
                {
                    var point = palette[x, y];
                    point.GetPointColor(out int r, out int g, out int b);

                    for (int i = 0; i < multiplayer; i++)
                    {
                        for (int j = 0; j < multiplayer; j++)
                        {
                            image.SetPixel(x * multiplayer + i, y * multiplayer + j, Color.FromArgb(r, g, b));
                        }
                    }                 
                }
            }

            return image;
        }
    }
}
