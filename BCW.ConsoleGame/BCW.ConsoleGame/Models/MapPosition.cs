using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BCW.ConsoleGame.Models
{
    [JsonObject(MemberSerialization.OptOut)]
    public class MapPosition
    {
        public int X { get; set; }
        public int Y { get; set; }

        public MapPosition(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
