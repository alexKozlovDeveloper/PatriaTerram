namespace PatriaTerram.Core.Models
{
    public class PalettePointTerrain
    {
        public Terrain Terrain { get; set; }
        public int Value { get; set; }
        public Color Color
        {
            get
            {
                return new Color
                {
                    R = (int)(Terrain.Color.R * (Value / 255.0)),
                    G = (int)(Terrain.Color.G * (Value / 255.0)),
                    B = (int)(Terrain.Color.B * (Value / 255.0))
                };
            }
        }       
    }
}
