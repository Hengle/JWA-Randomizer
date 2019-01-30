using System;
using System.Collections.Generic;
using JWA_Randomizer.Entities;
using JWA_Randomizer.Utilities.Extensions;

namespace JWA_Randomizer.Randomizers
{
    public class DinosaurWeightedRandomizer : IDinosaurRandomizer
    {
        /// <summary>
        /// Creates a randomized team of 8 dinosaurs, favoring those of higher level
        /// </summary>
        /// <param name="dinosaurs">List of dinosaurs available</param>
        /// <returns>Returns a randomized, but weighted, team of dinosaurs</returns>
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
            var dinosaurClones = new List<Dinosaur>();

            // For each dinosaur available, add a number of clones of that dinosaur equal to its
            // level to the list of dinosaurs to then choose from (i.e. a level 30 dinosaur will
            // then have 30 entries to be chosen from compared to a level 1 dinosaur, which will
            // only have 1)
            dinosaurs.ForEach(d =>
            {
                for (var x = 0; x < d.Level; x++)
                {
                    dinosaurClones.Add(d.Clone());
                }
            });

            var random = new Random();
            var maxRandom = dinosaurClones.Count - 1;

            // While we don't have a team of 8 dinosaurs, select one at random from the weighted
            // list of those available
            while (team.Count < 8)
            {
                var randomIndex = random.Next(maxRandom);
                var dino = dinosaurClones[randomIndex];
                team.Add(dino);

                // When we add a dinosaur to the team, remove all its cloned entries from the
                // list of available dinosaurs to prevent duplicates from joining the team
                dinosaurClones.RemoveAll(d => d.DinosaurName == dino.DinosaurName);
                maxRandom = dinosaurClones.Count - 1;
            }

            return team;
        }
    }
}