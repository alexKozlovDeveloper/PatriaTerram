using System;
using System.Collections.Generic;
using System.Linq;

namespace PerlinNoise
{
    public class PerlinNoiseGenerator
    {
        private readonly Random _random;

        private readonly int _maxValue;

        public PerlinNoiseGenerator(int seed, int maxValue)
        {
            _random = new Random(seed);

            _maxValue = maxValue;
        }

        public int[][] GetPerlinNoiseMatrix(int width, int height)
        {
            var matrixes = new List<int[][]>();

            for (int i = 2; i < width; i *= 2)
            {
                var m = GetRandomMatrix(i, i);

                m = IncreaseOctave(m, width / i);

                matrixes.Add(m);
            }

            var sum = matrixes.Average();

            return sum.Smoothing().Smoothing();
        }

        public int[][] GetRandomMatrix(int width, int height)
        {
            var matrix = new int[width][];

            for (int i = 0; i < width; i++)
            {
                matrix[i] = new int[height];
            }

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    matrix[x][y] = _random.Next(0, _maxValue);
                }
            }

            return matrix;
        }

        public int[][] IncreaseOctave(int[][] matrix, int multiplayer)
        {
            var result = new int[matrix.Length * multiplayer][];

            for (int x = 0; x < matrix.Length * multiplayer; x++)
            {
                result[x] = new int[matrix[0].Length * multiplayer];
            }

            for (int x = 0; x < matrix.Length; x++)
            {
                for (int y = 0; y < matrix[0].Length; y++)
                {
                    for (double a = 0; a < multiplayer; a++)
                    {
                        for (double b = 0; b < multiplayer; b++)
                        {
                            if(a == 0 && b == 0) 
                            {
                                result[x * multiplayer][y * multiplayer] = matrix[x][y];
                                continue; 
                            }

                            var point00 = matrix[x][y];
                            var point01 = matrix[x][(y == matrix[0].Length - 1 ? -1 : y) + 1];
                            var point10 = matrix[(x == matrix.Length - 1 ? -1 : x) + 1][y];
                            var point11 = matrix[(x == matrix.Length - 1 ? -1 : x) + 1][(y == matrix[0].Length - 1 ? -1 : y) + 1];

                            var t00 = 1 / Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
                            var t01 = 1 / Math.Sqrt(Math.Pow(a, 2) + Math.Pow(multiplayer - b, 2));
                            var t10 = 1 / Math.Sqrt(Math.Pow(multiplayer - a, 2) + Math.Pow(b, 2));
                            var t11 = 1 / Math.Sqrt(Math.Pow(multiplayer - a, 2) + Math.Pow(multiplayer - b, 2));

                            var value = (point00 * t00 + point01 * t01 + point10 * t10 + point11 * t11) / (t00 + t01 + t10 + t11);

                            result[x * multiplayer + (int)a][y * multiplayer + (int)b] = (int)value;
                        }
                    }
                }
            }

            return result;
        }

        public int Lerp(int a, int b, int t)
        {
            return a + (b - a) * t;
        }        
    }
}
