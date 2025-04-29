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
            Test.ExecMainTests();
            try
            {
                Game game = new Game();
                game.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine($"AN UNEXPECTED ERROR HAS OCCURED: {e}, {e.Message}");
            }
            finally
            {
                Console.WriteLine("end of game, press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
