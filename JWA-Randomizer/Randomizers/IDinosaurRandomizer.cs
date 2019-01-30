using System.Collections.Generic;
using JWA_Randomizer.Entities;

namespace JWA_Randomizer.Randomizers
{
    public interface IDinosaurRandomizer
    {
        /// <summary>
        /// Creates a randomized team of 8 dinosaurs.  Randomization type depends on 
        /// option selected by User
        /// </summary>
        /// <param name="dinosaurs">List of available dinosaurs</param>
        /// <returns>Returns a randomized team of dinosaurs</returns>
        List<Dinosaur> GetTeam(List<Dinosaur> dinosaurs);
    }
}