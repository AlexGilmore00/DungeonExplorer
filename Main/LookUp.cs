using DungeonExplorer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class LookUp
    {
        // this class will be used for static function calls when looking up things
        // such as an items type/damage/effects or looking up a random enemy type 
        // or room description

        private static Random rnd = new Random();
        public LookUp() { }

        //
        //
        // for room generation:
        //
        //

        public static LivingEntity GenerateRandomEnemy()
        // not fully implemented yet but will eventually return a random enemy class
        // taht is a child of LivingEntity
        {
            return new TestEnemy();
        }

        public static ParentContainer GenerateRandomContainer()
        // not fully implemented yet but will eventually return a random container class
        // taht is a child of ParentContainer
        {
            return new TestContainer();
        }

        public static string GetRandomFlavourText(int difficulty)
        // returns a random description of a room depending on the difficulty of said room
        // a room on level 1 will have difficulty 0 and so on
        {
            // the list descriptions contains lists with descriptions of a room of a given difficulty
            // the index of the nested list is the difficulty of the room descriptions within it
            // so access using descriptions[difficulty][x]
            List<List<string>> descriptions = new List<List<string>>();

            // level one
            descriptions.Add(new List<string> 
            {
                "the walls are cold and damp, the cracks in the stone brick let a light breeze through. " +
                "you cant tell whether the sounds you hear are feint screams or wind rushing through " +
                "distant halls."
            });

            return descriptions[difficulty][rnd.Next(0, descriptions[difficulty].Count)];
        }
    }
}
