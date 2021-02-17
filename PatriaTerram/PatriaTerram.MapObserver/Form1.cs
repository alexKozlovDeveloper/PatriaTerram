using Newtonsoft.Json;
using PatriaTerram.Core.BuildingConditions;
using PatriaTerram.Core.Configurations;
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
using AStarAlgorithm;

namespace PatriaTerram.MapObserver
{
    public partial class Form1 : Form
    {
        private int[][] _matrix;

        public Form1()
        {
            InitializeComponent();

            foreach (var item in Configs.PaletteConfigs)
            {
                configsNamesComboBox.Items.Add(item.Key);
            }

            configsNamesComboBox.SelectedIndex = 0;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            int size = int.Parse(sizeComboBox.SelectedItem as string);
            int smoothingSize = int.Parse(SmoothingSizeTextBox.Text as string);
            int seed = int.Parse(seedTextBox.Text as string);

            var generator = new PerlinNoiseGenerator(seed, 256);

            _matrix = generator.GetPerlinNoiseMatrix(size, smoothingSize);

            _matrix = _matrix.StretchOnMaximumAndMinimumValue(0, 255).Smoothing();

            UpdateImage(_matrix);
        }

        private void exponentiationButton_Click(object sender, EventArgs e)
        {
            _matrix = _matrix.StretchOnMaximumAndMinimumValue(0, 255).Smoothing();
            _matrix = _matrix.Exponentiation(256);

            UpdateImage(_matrix);
        }

        private void UpdateImage(int[][] matrix)
        {
            if (matrix == null) { return; }

            Bitmap image = new Bitmap(matrix.Length, matrix.Length);

            for (int x = 0; x < matrix.Length; x++)
            {
                for (int y = 0; y < matrix[0].Length; y++)
                {
                    image.SetPixel(x, y, System.Drawing.Color.FromArgb(matrix[x][y], matrix[x][y], matrix[x][y]));
                }
            }

            mapPictureBox.Image = image;
        }

        private void clearLowValueButton_Click(object sender, EventArgs e)
        {
            if (_matrix == null) { return; }

            int lowEdge = int.Parse(lowEdgeTextBox.Text);

            _matrix = _matrix.ClearBottomValue(lowEdge);

            UpdateImage(_matrix);
        }

        private void clearHighValueButton_Click(object sender, EventArgs e)
        {
            if (_matrix == null) { return; }

            int highEdge = int.Parse(highEdgeTextBox.Text);

            _matrix = _matrix.ClearTopValue(highEdge);

            UpdateImage(_matrix);
        }

        private void paletteButton_Click(object sender, EventArgs e)
        {
            int pixelSize = int.Parse(pixelSizeTextBox.Text as string);

            var config = new PaletteConfiguration
            {
                Width = int.Parse(sizeComboBox.SelectedItem as string),
                Height = int.Parse(sizeComboBox.SelectedItem as string),
                Seed = int.Parse(seedTextBox.Text as string),
                OceanEdge = int.Parse(oceanEdgeTextBox.Text as string),
                MountainsEdge = int.Parse(mountainsTextBox.Text as string),
                FertileSoilRange = new Range
                {
                    Bottom = int.Parse(fertileSoilBottomEdgeTextBox.Text as string),
                    Top = int.Parse(fertileSoilTopEdgeTextBox.Text as string)
                },
                WoodRange = new Range
                {
                    Bottom = int.Parse(woodBottomEdgeTextBox.Text as string),
                    Top = int.Parse(woodTopEdgeTextBox.Text as string)
                },
                StoneRange = new Range
                {
                    Bottom = int.Parse(stoneBottomEdgeTextBox.Text as string),
                    Top = int.Parse(stoneTopEdgeTextBox.Text as string)
                },
                LakeRange = new Range
                {
                    Bottom = int.Parse(lakeBottomEdgeTextBox.Text as string),
                    Top = int.Parse(lakeTopEdgeTextBox.Text as string),
                },
                BeachSize = int.Parse(beachSizeTextBox.Text as string),
                MaxAltitudeValue = 256,
                SmoothingSize = int.Parse(SmoothingSizeTextBox.Text as string)
            };

            var factory = new TerrainPaletteFactory(config);

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
                            image.SetPixel(x * multiplayer + i, y * multiplayer + j, System.Drawing.Color.FromArgb(r, g, b));
                        }
                    }
                }
            }

            return image;
        }

        private void loadConfigbutton_Click(object sender, EventArgs e)
        {
            string configName = configsNamesComboBox.SelectedItem.ToString();

            var config = Configs.PaletteConfigs[configName];

            sizeComboBox.SelectedItem = config.Width;
            seedTextBox.Text = config.Seed.ToString();

            oceanEdgeTextBox.Text = config.OceanEdge.ToString();
            mountainsTextBox.Text = config.MountainsEdge.ToString();

            fertileSoilBottomEdgeTextBox.Text = config.FertileSoilRange.Bottom.ToString();
            fertileSoilTopEdgeTextBox.Text = config.FertileSoilRange.Top.ToString();

            woodBottomEdgeTextBox.Text = config.WoodRange.Bottom.ToString();
            woodTopEdgeTextBox.Text = config.WoodRange.Top.ToString();

            stoneBottomEdgeTextBox.Text = config.StoneRange.Bottom.ToString();
            stoneTopEdgeTextBox.Text = config.StoneRange.Top.ToString();

            lakeBottomEdgeTextBox.Text = config.LakeRange.Bottom.ToString();
            lakeTopEdgeTextBox.Text = config.LakeRange.Top.ToString();

            beachSizeTextBox.Text = config.BeachSize.ToString();

            SmoothingSizeTextBox.Text = config.SmoothingSize.ToString();
        }

        private void astartTestbutton_Click(object sender, EventArgs e)
        {
            var matrix = GetMatrix();

            var start = new AStarAlgorithm.Entityes.Coord(0, 0);
            var finish = new AStarAlgorithm.Entityes.Coord(6, 2);

            var resolver = new AStarResolver(matrix);

            var path = resolver.GetPath(start, finish);

            foreach (var item in path)
            {
                matrix[item.X][item.Y] = 666;
            }

            var bitmap = GetBitmap(matrix, 10);

            mapPictureBox.Image = bitmap;
        }

        private Bitmap GetBitmap(int[][] matrix, int multiplayer)
        {
            var width = matrix.Length;
            var height = matrix[0].Length;

            var image = new Bitmap(width * multiplayer, height * multiplayer);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    //var point = palette[x, y];
                    //point.GetPointColor(out int r, out int g, out int b);

                    var color = System.Drawing.Color.FromArgb(0, 0, 0);

                    if(matrix[x][y] == 1)
                    {
                        color = System.Drawing.Color.White;
                    }

                    if (matrix[x][y] == 999)
                    {
                        color = System.Drawing.Color.Black;
                    }

                    if (matrix[x][y] == 666)
                    {
                        color = System.Drawing.Color.Yellow;
                    }

                    for (int i = 0; i < multiplayer; i++)
                    {
                        for (int j = 0; j < multiplayer; j++)
                        {
                            image.SetPixel(x * multiplayer + i, y * multiplayer + j, color);
                        }
                    }
                }
            }

            return image;
        }

        private int[][] GetMatrix()
        {
            var width = 10;
            var height = 10;

            int[][] matrix = new int[width][];

            for (int i = 0; i < height; i++)
            {
                matrix[i] = new int[height];
            }

            matrix[0][0] = 1;
            matrix[0][1] = 1;
            matrix[0][2] = 1;
            matrix[0][3] = 1;
            matrix[0][4] = 1;
            matrix[0][5] = 1;
            matrix[0][6] = 1;
            matrix[0][7] = 1;
            matrix[0][8] = 1;
            matrix[0][9] = 1;

            matrix[1][0] = 1;
            matrix[1][1] = 999;
            matrix[1][2] = 999;
            matrix[1][3] = 999;
            matrix[1][4] = 999;
            matrix[1][5] = 999;
            matrix[1][6] = 999;
            matrix[1][7] = 999;
            matrix[1][8] = 999;
            matrix[1][9] = 1;

            matrix[2][0] = 1;
            matrix[2][1] = 999;
            matrix[2][2] = 1;
            matrix[2][3] = 1;
            matrix[2][4] = 1;
            matrix[2][5] = 1;
            matrix[2][6] = 1;
            matrix[2][7] = 1;
            matrix[2][8] = 1;
            matrix[2][9] = 1;

            matrix[3][0] = 1;
            matrix[3][1] = 999;
            matrix[3][2] = 1;
            matrix[3][3] = 999;
            matrix[3][4] = 999;
            matrix[3][5] = 999;
            matrix[3][6] = 1;
            matrix[3][7] = 999;
            matrix[3][8] = 999;
            matrix[3][9] = 1;

            matrix[4][0] = 1;
            matrix[4][1] = 999;
            matrix[4][2] = 1;
            matrix[4][3] = 999;
            matrix[4][4] = 1;
            matrix[4][5] = 999;
            matrix[4][6] = 1;
            matrix[4][7] = 1;
            matrix[4][8] = 999;
            matrix[4][9] = 1;

            matrix[5][0] = 1;
            matrix[5][1] = 999;
            matrix[5][2] = 999;
            matrix[5][3] = 999;
            matrix[5][4] = 1;
            matrix[5][5] = 999;
            matrix[5][6] = 999;
            matrix[5][7] = 1;
            matrix[5][8] = 999;
            matrix[5][9] = 1;

            matrix[6][0] = 1;
            matrix[6][1] = 999;
            matrix[6][2] = 1;
            matrix[6][3] = 999;
            matrix[6][4] = 1;
            matrix[6][5] = 1;
            matrix[6][6] = 1;
            matrix[6][7] = 1;
            matrix[6][8] = 999;
            matrix[6][9] = 1;

            matrix[7][0] = 1;
            matrix[7][1] = 999;
            matrix[7][2] = 1;
            matrix[7][3] = 1;
            matrix[7][4] = 1;
            matrix[7][5] = 999;
            matrix[7][6] = 999;
            matrix[7][7] = 1;
            matrix[7][8] = 999;
            matrix[7][9] = 1;

            matrix[8][0] = 1;
            matrix[8][1] = 999;
            matrix[8][2] = 999;
            matrix[8][3] = 999;
            matrix[8][4] = 999;
            matrix[8][5] = 999;
            matrix[8][6] = 999;
            matrix[8][7] = 999;
            matrix[8][8] = 999;
            matrix[8][9] = 1;

            matrix[9][0] = 1;
            matrix[9][1] = 1;
            matrix[9][2] = 1;
            matrix[9][3] = 1;
            matrix[9][4] = 1;
            matrix[9][5] = 1;
            matrix[9][6] = 1;
            matrix[9][7] = 1;
            matrix[9][8] = 1;
            matrix[9][9] = 1;

            return matrix;
        }
    }
}
