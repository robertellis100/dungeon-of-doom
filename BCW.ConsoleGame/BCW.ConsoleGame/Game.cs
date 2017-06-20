using BCW.ConsoleGame.Events;
using BCW.ConsoleGame.Models;
using BCW.ConsoleGame.Models.Scenes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BCW.ConsoleGame
{
    public class Game
    {
        public List<IScene> Scenes { get; set; }

        public Game()
        {
            Scenes = new List<IScene>();
            loadScenes();

            gotoPosition(new MapPosition(9, 5));
        }

        void gotoPosition(MapPosition position)
        {
            var scene = Scenes.FirstOrDefault(s => s.MapPosition.X == position.X && s.MapPosition.Y == position.Y);

            scene?.Enter();
        }

        private void sceneNavigated(object sender, NavigationEventArgs args)
        {
            var toPosition = new MapPosition(args.Scene.MapPosition.X, args.Scene.MapPosition.Y);

            switch (args.Direction)
            {
                case Direction.North:
                    toPosition.Y -= 1;
                    break;

                case Direction.South:
                    toPosition.Y += 1;
                    break;

                case Direction.East:
                    toPosition.X += 1;
                    break;

                case Direction.West:
                    toPosition.X -= 1;
                    break;
            }

            var nextScene = Scenes.FirstOrDefault(s => s.MapPosition.X == toPosition.X && s.MapPosition.Y == toPosition.Y);

            nextScene?.Enter();
        }

        private void loadScenes()
        {
            //Scenes.Add(new Scene
            //(
            //    "Entry Way",
            //    "You're standing in a small empty room with a door on the north wall.",
            //    new MapPosition(9, 5),
            //    new List<ICommand>
            //    {
            //        new NavigationCommand { Keys = "n", Description = "Go North", Direction = Direction.North }
            //    }
            //));


            Scenes = loadData();
           

            foreach (var scene in Scenes)
            {
                scene.Navigated += sceneNavigated;
            }
        }

        private List<IScene> loadData()
        {
            //read scene data from json file
            //and deserialize it to a Scene object
            //and 
            var scenes = new List<IScene>();
            var dataFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Scenes.json");
            using (StreamReader reader = File.OpenText(dataFilePath))
            {
                var sceneData = (JObject) JToken.ReadFrom(new JsonTextReader(reader));
                var scenesJson = (JArray) sceneData.GetValue("Scenes");
                foreach (JToken sceneJson in scenesJson)
                {
                    var scene = JsonConvert.DeserializeObject<Scene>(sceneJson.ToString());
                    scenes.Add(scene);
                }
                
            }
            return scenes;

        }
    }
}
