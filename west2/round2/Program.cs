using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class Program
    {
        static void Main(string[] args)
        {
 
            Item.LoadJson();
            Gamemodebase game=new Gamemodebase();
            game.Beginplay();
          
        }
    }
}
