using BCW.ConsoleGame.Events;
using BCW.ConsoleGame.Models;
using BCW.ConsoleGame.Models.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Scenes.Add(new Scene
            (
                "Entry Way",
                "You're standing in a small empty room with a door on the north wall.",
                new MapPosition(9, 5),
                new List<ICommand>
                {
                    new NavigationCommand { Keys = "n", Description = "Go North", Direction = Direction.North }
                }
            ));

            Scenes.Add(new Scene
            (
                "Great Room",
                "You're standing in an ornate great room with doors on the south, west, and north walls.",
                new MapPosition(9, 4),
                new List<ICommand>
                {
                    new NavigationCommand { Keys = "s", Description = "Go South", Direction = Direction.South },
                    new NavigationCommand { Keys = "n", Description = "Go North", Direction = Direction.North },
                    new NavigationCommand { Keys = "w", Description = "Go West", Direction = Direction.West }
                }
            ));

            Scenes.Add(new Scene
            (
                "Dark Room",
                "You're standing in a dark bleak room with doors on the north, west, and east walls.",
                new MapPosition(8, 4),
                new List<ICommand>
                {
                    new NavigationCommand { Keys = "w", Description = "Go West", Direction = Direction.West },
                    new NavigationCommand { Keys = "n", Description = "Go North", Direction = Direction.North },
                    new NavigationCommand { Keys = "e", Description = "Go East", Direction = Direction.East }
                }
            ));

            Scenes.Add(new Scene
            (
                "Small Armory",
                "You're standing in the small armory with a doors east wall.",
                new MapPosition(7, 4),
                new List<ICommand>
                {
                    new NavigationCommand { Keys = "e", Description = "Go East", Direction = Direction.East }
                }
            ));

            Scenes.Add(new Scene
            (
                "Gallery",
                "You're standing in, what appears to be, a room with walls covered  floor-to-ceiling with golden framed art. Also there is a door on the south wall.",
                new MapPosition(9, 3),
                new List<ICommand>
                {
                    new NavigationCommand { Keys = "s", Description = "Go South", Direction = Direction.South }
                }
            ));

            Scenes.Add(new Scene
            (
                "Candle Room",
                "You're standing in a candle lit room with doors on the north, west, and south walls.",
                new MapPosition(8, 3),
                new List<ICommand>
                {
                    new NavigationCommand { Keys = "w", Description = "Go West", Direction = Direction.West },
                    new NavigationCommand { Keys = "n", Description = "Go North", Direction = Direction.North },
                    new NavigationCommand { Keys = "e", Description = "Go South", Direction = Direction.South }
                }
            ));

            foreach (var scene in Scenes)
            {
                scene.Navigated += sceneNavigated;
            }
        }
    }
}
