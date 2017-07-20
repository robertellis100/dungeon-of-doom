using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCW.ConsoleGame.Models.Characters;

namespace BCW.ConsoleGame.Models
{
    class SlimeySlug : Monster
    {
        public SlimeySlug()
        {
            items = new List<IComposite>();
        }
    }
}
