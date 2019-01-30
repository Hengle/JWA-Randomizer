using System;
using System.Collections.Generic;
using JWA_Randomizer.Entities;
using JWA_Randomizer.Utilities.Extensions;

namespace JWA_Randomizer.Randomizers
{
    public class DinosaurRandomizer : IDinosaurRandomizer
    {
        /// <summary>
        /// Creates a completely random team of 8 dinosaurs
        /// </summary>
        /// <param name="dinosaurs">List of dinosaurs available</param>
        /// <returns>Returns a randomized team of dinosaurs</returns>
        public List<Dinosaur> GetTeam(List<Dinosaur> dinosaurs)
        {
            // If there aren't any dinosaurs, or less than 4, we can't do anything
            if (dinosaurs.IsNullOrEmpty() || dinosaurs.Count < 4)
            {
                throw new Exception("You do not have enough dinosaurs to create a team.");
            }

            // If there are only 8 or less dinosaurs, no point in randomization
            if (dinosaurs.Count <= 8)
            {
                return dinosaurs;
            }

            var team = new List<Dinosaur>();
            var dinosaurClones = new List<Dinosaur>(dinosaurs);
            var random = new Random();
            var maxRandom = dinosaurClones.Count - 1;

            // While we don't have a team of 8 dinosaurs, grab one at random
            // from those available
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