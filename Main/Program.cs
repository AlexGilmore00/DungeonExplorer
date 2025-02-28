using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Game game = new Game();
                game.Start();
            }
            catch
            {
                Console.WriteLine("AN UNEXPECTED ERROR HAS OCCURED");
            }
            finally
            {
                Console.WriteLine("end of game, press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
