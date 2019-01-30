using JWA_Randomizer.Utilities.Extensions;
using JWA_Randomizer.Enums;
using JWA_Randomizer.Utilities;

namespace JWA_Randomizer.Entities
{
    public class Dinosaur
    {
        public DinosaurName DinosaurName { get; }
        public int Level { get; }
        public Rarity Rarity { get; }
        public DinosaurType DinosaurType { get; }

        public string Name => DinosaurName.ToSpacedString();

        /// <summary>
        /// Constructor for the Dinosaur class
        /// </summary>
        /// <param name="name">DinosaurName for the dinosaur</param>
        /// <param name="level">The dinosaur's level</param>
        public Dinosaur(DinosaurName name, int level)
        {
            DinosaurName = name;
            Level = level;

            // Using the DinosaurName, get the rarity and dinosaur type
            Rarity = Utils.GetRarity(DinosaurName);
            DinosaurType = Utils.GetDinosaurType(DinosaurName);
        }

        /// <summary>
        /// Checks if the dinosaur is of a particular dinosaur type
        /// </summary>
        /// <param name="type">DinosaurType to check</param>
        /// <returns>Returns true if the dinosaur is of the particular type</returns>
        public bool IsOfType(DinosaurType type)
        {
            return DinosaurType.HasFlag(type);
        }

        /// <summary>
        /// Duplicates the dinosaur into a fresh object with the same properties
        /// </summary>
        /// <returns>Returns a new, but identical, dinosaur object</returns>
        public Dinosaur Clone()
        {
            return new Dinosaur(DinosaurName, Level);
        }

        /// <summary>
        /// Override that will display the dinosaur's level, name, rarity, and type(s)
        /// </summary>
        /// <returns>Returns a formatted string of various dinosaur characteristics</returns>
        public override string ToString()
        {
            return $"Level {Level} {Name} ({Rarity.ToSpacedString()}) *{DinosaurType.ToSpacedString()}*";
        }
    }
}