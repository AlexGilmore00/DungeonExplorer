using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public abstract class LivingEntity
    // the master class for both the player and all enemies to inherit from
    {
        public string Name { get; protected set; }
        public int MaxHealth { get; protected set; }
        public int Health
        {
            get { return _health; }
            protected set
            {
                if (value < 0) { _health = 0; }
                else if (value > MaxHealth) { _health = MaxHealth; }
                else { _health = value; }
            }
        }
        private int _health;
        public int CurrentAtkDmg { get; protected set; }
        public int CurrentDefence { get; protected set; }

        public bool IsDead {  get; protected set; }

        public LivingEntity()
        {
            IsDead = false;
        }

        public void DealDamageTo (Player player, ParentEnemy enemy, bool playerAttackEnemy)
        // if playerAttackEnemy is true:
        //     the player damages the enemy
        // if playerAttackEnemy is false:
        //     the enemy deals damage to the player
        {
            if (playerAttackEnemy)
            {
                int attack = player.CurrentAtkDmg;
                int defence = enemy.CurrentDefence;

                int damage = attack - defence;
                // dont call to deal damage if no damage is dealt
                if (damage <= 0) { damage = 0; }
                else
                {
                    enemy.TakeDamage(damage);
                }
                Console.WriteLine($"{enemy.Name} took {damage} damage");
            }
            else
            {
                int attack = enemy.CurrentAtkDmg;
                int defence = player.CurrentDefence;

                int damage = attack - defence;
                // dont call to deal damage if no damage is dealt
                if (damage <= 0) { damage = 0; }
                else
                {
                    player.TakeDamage(damage);
                }
                Console.WriteLine($"{player.Name} took {damage} damage");
            }
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            // kill the entity if their health is 0 or below
            if (Health <= 0)
            {
                IsDead = true;
                Name += " (Dead)";
            }
        }
    }
}
