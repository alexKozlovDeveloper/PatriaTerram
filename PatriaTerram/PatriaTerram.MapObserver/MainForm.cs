using Newtonsoft.Json;
using PatriaTerram.Core.Configurations;
using PatriaTerram.Core.Factoryes;
using PatriaTerram.Core.Helpers;
using PatriaTerram.Core.Models;
using PerlinNoise;
using System;
using System.Drawing;
using System.Windows.Forms;
using AStarAlgorithm;

namespace PatriaTerram.MapObserver
{
    public partial class MainForm : Form
    {
        private int[][] _matrix;

        public MainForm()
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
            var filePath = @"TestMatrixes\matrix10x10.json";
            var json = System.IO.File.ReadAllText(filePath);

            return JsonConvert.DeserializeObject<int[][]>(json);
        }
    }
}
