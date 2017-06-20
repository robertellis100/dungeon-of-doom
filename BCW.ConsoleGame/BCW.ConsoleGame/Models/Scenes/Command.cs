﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BCW.ConsoleGame.Models.Scenes
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Command : ICommand
    {
        public string Keys { get; set; }
        public string Description { get; set; }
        public Action Action { get; set; }
    }
}
