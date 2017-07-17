using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCW.ConsoleGame.Events;
using BCW.ConsoleGame.Data;
using BCW.ConsoleGame.Models;
using BCW.ConsoleGame.Models.Scenes;
using BCW.ConsoleGame.Models.Commands;
using BCW.ConsoleGame.JsonDataProvider;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace BCW.ConsoleGame.JsonDataProvider
{
    public class DataProvider
    {
        private List<IScene> loadData(DataProvider dataProvider)
        {
            var scenes = new List<IScene>();
            var dataFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Scenes.json");

            using (StreamReader reader = File.OpenText(dataFilePath))
            {
                var sceneData = (JObject)JToken.ReadFrom(new JsonTextReader(reader));

                var scenesJson = (JArray)sceneData.GetValue("Scenes");

                scenes = scenesJson.Select(s => new Scene
                (
                    (string)s["Title"],
                    (string)s["Description"],
                    new MapPosition((int)s["MapPosition"]["X"], (int)s["MapPosition"]["Y"]),
                    (s["NavigationCommands"] as JArray).Select(c => new NavigationCommand
                    {
                        Keys = (string)c["Keys"],
                        Description = (string)c["Description"],
                        Direction = (Direction)Enum.Parse(typeof(Direction), (string)c["Direction"])
                    }).ToList<ICommand>(),
                    new List<ICommand> { new GameCommand { Keys = "X", Description = "Exit The Game" } }
                )).ToList<IScene>();
            }

            return scenes;
        }
    }
}
