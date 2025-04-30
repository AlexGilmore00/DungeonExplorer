using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class BossRoom : ParentRoom
    {
        private int _difficulty;
        public BossRoom(int difficulty)
        {
            _difficulty = difficulty;
            // add ability for player to go down to the next level from the
            // boss room
            _validConnections.Add('D');
            // generate the boss
            // NOTE: an offset of +10 if given when generating a boss
            Enemies = new ParentEnemy[1];
            Enemies[0] = LookUp.GenerateRandomEnemy(_difficulty + 10);
            // no containers for now
            Containers = new ParentContainer[0];
            // update flaviour text to be unique for boss rooms in the future
            _flavourText = LookUp.GetRandomFlavourText(_difficulty);
        }
    }
}
