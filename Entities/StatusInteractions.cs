using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DungeonExplorer
{
    public abstract class StatusInteractions
    {
        public StatusInteractions() { }

        public static void ApplyStatusTo(LivingEntity entity, StatusIds statusId, int duration)
        // entity is the entity the status will be applied to
        // status is the number corresponding to that status effect as shown
        // in StatusIds enum
        {
            switch (statusId)
            {
                case (int)StatusIds.Stun:
                    if (!AlreadyApplied(entity, statusId, duration))
                    {
                        entity.StatusEffects.Add(new Status("Stun", (int)StatusIds.Stun, duration));
                        Console.WriteLine($"{entity.Name} has been stunned for {duration} turns!");
                    }
                    break;
                case StatusIds.Bleed:
                    if (!AlreadyApplied(entity, statusId, duration))
                    {
                        entity.StatusEffects.Add(new Status("Bleed", (int)StatusIds.Bleed, duration));
                        Console.WriteLine($"{entity.Name} has been afflicted with bleed for {duration} turns");
                    }
                    break;
                case StatusIds.Strength:
                    if (!AlreadyApplied(entity, statusId, duration))
                    {
                        entity.StatusEffects.Add(new Status("Strength", (int)StatusIds.Strength, duration));
                        Console.WriteLine($"{entity.Name} has been strengthened for {duration} turns");
                    }
                    break;
                case StatusIds.Poison:
                    if (!AlreadyApplied(entity, statusId, duration))
                    {
                        entity.StatusEffects.Add(new Status("Poison", (int)StatusIds.Poison, duration));
                        Console.WriteLine($"{entity.Name} has been afflicted with poison for {duration} turns");
                    }
                    break;
            }
        }

        private static bool AlreadyApplied(LivingEntity entity, StatusIds statusId, int duration)
        // checks if an entity has a specific status effect already applied
        // if so, return true
        // else return false
        // if an incoming status has a greater duration than an already applied status
        // refresh the duration of the old status
        {
            foreach (var status in entity.StatusEffects)
            {
                if ((int)statusId == status.ID && duration <= status.Duration)
                {
                    return true;
                }
                else if ((int)statusId == status.ID && duration > status.Duration)
                {
                    status.Duration = duration;
                    return true;
                }
            }

            return false;
        }

        public static void UpdateStatuses(LivingEntity entity)
        // goes through all the enitties status effects and applies their
        // relevant conditions
        // removes the status effect from the list once its duration reaches zero
        {
            // keep track of which statuses need to be removed
            List<Status> removeList = new List<Status>();

            // apply status effects
            foreach (var status in entity.StatusEffects)
            {
                switch (status.ID)
                {
                    case (int)StatusIds.Stun:
                        if (status.Duration > 0)
                        {
                            entity.IsStunned = true;
                        }
                        else
                        {
                            entity.IsStunned = false;
                            removeList.Add(status);
                        }
                        break;
                    case (int)StatusIds.Bleed:
                        if (status.Duration > 0)
                        {
                            Console.WriteLine($"{entity.Name} took {status.Strength} damage " +
                                $"from bleeding");
                            ApplyDamage(entity, status.Strength);
                        }
                        else
                        {
                            removeList.Add(status);
                        }
                        break;
                    case (int)StatusIds.Strength:
                        if (status.Duration > 0 && !status.HasBeenApplied)
                        {
                            entity.CurrentAtkDmg += 5;
                            status.HasBeenApplied = true;
                        }
                        if (status.Duration <= 0)
                        {
                            Console.WriteLine($"{entity.Name}'s strength has ran out");
                            removeList.Add(status);
                        }
                        break;
                    case (int)StatusIds.Poison:
                        if (status.Duration > 0)
                        {
                            Console.WriteLine($"{entity.Name} took {status.Strength} damage " +
                                $"from poison");
                            ApplyDamage(entity, status.Strength);
                        }
                        else
                        {
                            removeList.Add(status);
                        }
                        break;
                    default:
                        Console.WriteLine($"WARNING!!a status {status.Name} with an invalid " +
                            $"status id {status.ID} has been given to {entity.Name}");
                        break;
                }

                status.Duration--;
            }

            // remove any nrcessary statuses
            foreach (var status in removeList)
            {
                entity.StatusEffects.Remove(status);
            }
        }

        public static void ApplyDamage(LivingEntity entity, int damage)
        {
            if (entity is Player)
            {
                Player player = (Player)entity;
                player.TakeDamage(damage);
            }
            else if (entity is ParentEnemy)
            {
                ParentEnemy enemy = (ParentEnemy)entity;
                enemy.TakeDamage(damage);
            }
            else
            {
                Console.WriteLine($"WARNING!! a LivingEntity of not type Player or ParentEnemy" +
                    $"{entity.Name} was passed into applyDamage in StatusInteractions. they" +
                    $"could not take any damage");
            }
        }
    }
}
