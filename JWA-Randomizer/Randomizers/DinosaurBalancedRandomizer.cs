using System;
using System.Collections.Generic;
using System.Linq;
using JWA_Randomizer.Entities;
using JWA_Randomizer.Enums;
using JWA_Randomizer.Utilities.Extensions;

namespace JWA_Randomizer.Randomizers
{
    public class DinosaurBalancedRandomizer : IDinosaurRandomizer
    {
        public List<Dinosaur> GetTeam(List<Dinosaur> dinosaurs)
        {
            if (dinosaurs.IsNullOrEmpty() || dinosaurs.Count < 4)
            {
                return null;
            }

            if (dinosaurs.Count <= 8)
            {
                return dinosaurs;
            }

            var team = new List<Dinosaur>();
            var dinosaurClones = new List<Dinosaur>(dinosaurs);

            team.Add(GetType(dinosaurClones, DinosaurType.Chomper));
            team.Add(GetType(dinosaurClones, DinosaurType.Tank));
            team.Add(GetType(dinosaurClones, DinosaurType.Raptor));
            team.Add(GetType(dinosaurClones, DinosaurType.Dotter));
            team.Add(GetType(dinosaurClones, DinosaurType.WildCard));
            team.Add(GetType(dinosaurClones, DinosaurType.AntiRaptor));
            team.Add(GetType(dinosaurClones, DinosaurType.CounterAttack));
            team.Add(GetType(dinosaurClones, DinosaurType.Stunner));

            return team;
        }

        private static Dinosaur GetRandom(IReadOnlyList<Dinosaur> dinosaurs)
        {
            var random = new Random();
            var maxRandom = dinosaurs.Count - 1;

            var randomIndex = random.Next(maxRandom);
            return dinosaurs[randomIndex];
        }

        private static Dinosaur GetType(List<Dinosaur> dinosaurs, DinosaurType type)
        {
            var options = dinosaurs.Where(d => d.IsOfType(type)).ToList();
            var dino = !options.IsNullOrEmpty() ? GetRandom(options) : GetRandom(dinosaurs);

            dinosaurs.Remove(dino);
            return dino;
        }
    }
}