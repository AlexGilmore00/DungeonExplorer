using System;

namespace DungeonExplorer
{
    public class DefaultRoom
    {
        public LivingEntity[] Enemies {  get; private set; }
        public ParentContainer[] Containers { get; private set; }
        private int _difficulty;
        private static Random rnd = new Random();

        public DefaultRoom(int difficulty)
        {
            // dificulty is determined by the level the player is on, level 1 has difficulty 0
            // level 2 has difficulty 1 and so on
            _difficulty = difficulty;
            // set the number of containers and enemies in the room depending on difficulty
            Enemies = new LivingEntity[rnd.Next(0, 2 + difficulty)];
            PopulateEnemies();
            Containers = new ParentContainer[rnd.Next(1, 4 -  (difficulty / 2))];
            PopulateContainers();
        }

        private void PopulateEnemies()
        {
            // fill the rooms Enemies param with random enemy classes
            for (int i = 0; i < Enemies.Length; i++)
            {
                Enemies[i] = LookUp.GenerateRandomEnemy();
            }
        }

        private void PopulateContainers()
        {
            // fill the rooms Containers param with random containers
            for (int i = 0;i < Containers.Length; i++)
            {
                Containers[i] = LookUp.GenerateRandomContainer();
            }
        }

        public string GetDescription()
        {
            return "not implemented yet";
        }
    }
}