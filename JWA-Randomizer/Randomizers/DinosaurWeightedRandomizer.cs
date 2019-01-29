using System;
using System.Collections.Generic;
using JWA_Randomizer.Entities;
using JWA_Randomizer.Utilities.Extensions;

namespace JWA_Randomizer.Randomizers
{
    public class DinosaurWeightedRandomizer : IDinosaurRandomizer
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
            var dinosaurClones = new List<Dinosaur>();

            dinosaurs.ForEach(d =>
            {
                for (var x = 0; x < d.Level; x++)
                {
                    dinosaurClones.Add(d.Clone());
                }
            });

            var random = new Random();
            var maxRandom = dinosaurClones.Count - 1;
            while (team.Count < 8)
            {
                var randomIndex = random.Next(maxRandom);
                var dino = dinosaurClones[randomIndex];
                team.Add(dino);

                dinosaurClones.RemoveAll(d => d.DinosaurName == dino.DinosaurName);
                maxRandom = dinosaurClones.Count - 1;
            }

            return team;
        }
    }
}
