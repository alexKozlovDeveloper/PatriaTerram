﻿namespace PatriaTerram.Core.Interfaces
{
    public interface ILayer
    {
        string Name { get; }

        event ILayerHelper.AddItemHandler UpdateItemValueEvent;
    }

    public static class ILayerHelper 
    {
        public delegate void AddItemHandler(string layer, string descriptor, int value);
    }
}
