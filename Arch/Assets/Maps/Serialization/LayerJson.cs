using Newtonsoft.Json;
using System;

namespace Arch.Assets.Maps.Serialization
{
    [JsonObject("layer")]
    public class LayerJson
    {
        [JsonProperty("name")]
        public String Name;
        
        [JsonProperty("type")]
        public String Type;
        
        [JsonProperty("id")]
        public Int32 ID;
        
        [JsonProperty("level")]
        public Int32 Level;
        
        [JsonProperty("width")]
        public UInt32 Width;
        
        [JsonProperty("height")]
        public UInt32 Height;
        
        [JsonProperty("tileset_name")]
        public String TilesetName;
        
        [JsonProperty("opacity")]
        public Single Opacity;
        
        [JsonProperty("repeat")]
        public Boolean Repeat;
        
        [JsonProperty("distance")]
        public Int32 Distance;
        
        [JsonProperty("y_distance")]
        public Int32 YDistance;
        
        [JsonProperty("tile_size")]
        public Int32 TileSize;
        
        [JsonProperty("move_speed")]
        public Vector2Json MoveSpeed;
        
        [JsonProperty("data")]
        public UInt16[,] Data;
    }
}
