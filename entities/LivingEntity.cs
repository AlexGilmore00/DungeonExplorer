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
        public int CurrentAtkDmg;
        public int CurrentDefence;
        public bool IsDead { get; protected set; }
        public bool IsStunned;
        public List<Status> StatusEffects;

        public LivingEntity()
        {
            IsDead = false;
            StatusEffects = new List<Status>();
        }

        public void UpdateStatuses()
        // goes through all the enitties status effects and applies their
        // relevant condidions
        // removes the status effect from the list once its duration reaches zero
        {
            // keep track of which statuses need to be removed
            List<Status> removeList = new List<Status>();

            // apply status effects
            foreach (var status in StatusEffects)
            {
                switch (status.ID)
                {
                    case (int)StatusIds.Stun:
                        if (status.Duration > 0)
                        {
                            IsStunned = true;
                        }
                        else
                        {
                            IsStunned = false;
                            removeList.Add(status);
                        }
                        break;
                    case (int)StatusIds.Bleed:
                        if (status.Duration > 0)
                        {
                            Console.WriteLine($"{Name} took {status.Strength} damage" +
                                $"from bleeding");
                            Health -= status.Strength;
                        }
                        else
                        {
                            removeList.Add(status);
                        }
                        break;
                    default:
                        Console.WriteLine($"WARNING!!a status {status.Name} with an invalid " +
                            $"status id {status.ID} has been given to {Name}");
                        break;
                }

                status.Duration--;
            }

            // remove any nrcessary statuses
            foreach (var status in removeList)
            {
                StatusEffects.Remove(status);
            }
        }
    }
}
