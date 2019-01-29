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

        public Dinosaur(DinosaurName name, int level)
        {
            DinosaurName = name;
            Level = level;

            Rarity = Utils.GetRarity(DinosaurName);
            DinosaurType = Utils.GetDinosaurType(DinosaurName);
        }

        public bool IsOfType(DinosaurType type)
        {
            return DinosaurType.HasFlag(type);
        }

        public Dinosaur Clone()
        {
            return new Dinosaur(DinosaurName, Level);
        }

        public override string ToString()
        {
            return $"Level {Level} {Name} ({Rarity.ToSpacedString()}) *{DinosaurType.ToSpacedString()}*";
        }
    }
}
