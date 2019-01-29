using System.Collections.Generic;
using JWA_Randomizer.Entities;

namespace JWA_Randomizer.Randomizers
{
    public interface IDinosaurRandomizer
    {
        List<Dinosaur> GetTeam(List<Dinosaur> dinosaurs);
    }
}
