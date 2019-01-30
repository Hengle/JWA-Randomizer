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
        /// <summary>
        /// Creates a team of 8 dinosaurs, attempting to include at least one dinosaur
        /// from each DinosaurType
        /// </summary>
        /// <param name="dinosaurs">List of available dinosaurs</param>
        /// <returns>Returns a randomized, but balanced, team of dinosaurs</returns>
        public List<Dinosaur> GetTeam(List<Dinosaur> dinosaurs)
        {
            // If there are no dinosaurs or less than 4, we can't create a team
            if (dinosaurs.IsNullOrEmpty() || dinosaurs.Count < 4)
            {
                throw new Exception("You do not have enough dinosaurs to create a team.");
            }

            // If there are 8 or less dinosaurs available, no point in randomizing
            if (dinosaurs.Count <= 8)
            {
                return dinosaurs;
            }

            var team = new List<Dinosaur>();
            var dinosaurClones = new List<Dinosaur>(dinosaurs);

            // Attempt to add at least one dinosaur from each "type" of dinosaur
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

        /// <summary>
        /// Given a list of dinosaurs, selects one from the list at random
        /// </summary>
        /// <param name="dinosaurs">List of available dinosaurs</param>
        /// <returns>Returns a randomly selected dinosaur from the list</returns>
        private static Dinosaur GetRandom(IReadOnlyList<Dinosaur> dinosaurs)
        {
            var random = new Random();
            var maxRandom = dinosaurs.Count - 1;

            var randomIndex = random.Next(maxRandom);
            return dinosaurs[randomIndex];
        }

        /// <summary>
        /// Given a list of dinosaurs and a "type" to look for, selects one from the list
        /// matching that type at random
        /// </summary>
        /// <param name="dinosaurs">List of available dinosaurs</param>
        /// <param name="type">The "type" of dinosaur to look for</param>
        /// <returns>Returns a random dinosaur that matches the "type" specified from the
        /// list.  If none match, it returns a completely random dinosaur from the list</returns>
        private static Dinosaur GetType(List<Dinosaur> dinosaurs, DinosaurType type)
        {
            // Filter the available list to just those of the requested "type"
            var options = dinosaurs.Where(d => d.IsOfType(type)).ToList();

            // If no dinosaurs are found matching the requested "type", simply return a
            // completely random dinosaur from the list instead
            var dino = !options.IsNullOrEmpty() ? GetRandom(options) : GetRandom(dinosaurs);

            dinosaurs.Remove(dino);
            return dino;
        }
    }
}