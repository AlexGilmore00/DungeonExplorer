using System;
using System.Linq;

namespace DungeonExplorer
{
    public class DefaultRoom : ParentRoom
    {
        private int _difficulty;

        private static Random rnd = new Random();

        public DefaultRoom(int difficulty)
        {
            // dificulty is determined by the level the player is on, level 1 has difficulty 0
            // level 2 has difficulty 1 and so on
            _difficulty = difficulty;
            // set the number of containers and enemies in the room depending on difficulty
            Enemies = new ParentEnemy[rnd.Next(0, 2 + _difficulty)];
            PopulateEnemies();
            Containers = new ParentContainer[rnd.Next(1, 4 -  (_difficulty / 2))];
            PopulateContainers();
            // get the flavour text description of the room, this holds no gameplay value
            // describes the general look and feel of the room
            _flavourText = LookUp.GetRandomFlavourText(_difficulty);
        }

        private void PopulateEnemies()
        // fill the rooms Enemies param with random enemy classes
        {
            for (int i = 0; i < Enemies.Length; i++)
            {
                Enemies[i] = LookUp.GenerateRandomEnemy();
            }
        }

        private void PopulateContainers()
        // fill the rooms Containers param with random containers
        {
            for (int i = 0;i < Containers.Length; i++)
            {
                Containers[i] = LookUp.GenerateRandomContainer();
            }
        }
    }
}