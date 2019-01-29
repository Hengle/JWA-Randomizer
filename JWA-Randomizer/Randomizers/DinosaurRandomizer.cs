using System;
using System.Collections.Generic;
using JWA_Randomizer.Entities;
using JWA_Randomizer.Utilities.Extensions;

namespace JWA_Randomizer.Randomizers
{
    public class DinosaurRandomizer : IDinosaurRandomizer
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
            var random = new Random();
            var maxRandom = dinosaurClones.Count - 1;
            while (team.Count < 8)
            {
                var randomIndex = random.Next(maxRandom);
                team.Add(dinosaurClones[randomIndex]);

                dinosaurClones.RemoveAt(randomIndex);
                maxRandom--;
            }

            return team;
        }
    }
}
