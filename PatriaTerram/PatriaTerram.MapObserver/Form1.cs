using PatriaTerram.Core.Factoryes;
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

            _matrix = _generator.GetPerlinNoiseMatrix(size, size);

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

            var factory = new TerrainPaletteFactory(size, size, 
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

            Bitmap image = new Bitmap(palette.Width * 2, palette.Height * 2);

            for (int x = 0; x < palette.Width; x++)
            {
                for (int y = 0; y < palette.Height; y++)
                {
                    var point = palette[x, y];

                    point.GetPointColor(out int r, out int g, out int b);

                    image.SetPixel(x * 2, y * 2, Color.FromArgb(r, g, b));
                    image.SetPixel(x * 2, y * 2 + 1, Color.FromArgb(r, g, b));
                    image.SetPixel(x * 2 + 1, y * 2, Color.FromArgb(r, g, b));
                    image.SetPixel(x * 2 + 1, y * 2 + 1, Color.FromArgb(r, g, b));
                }
            }

            mapPictureBox.Image = image;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
