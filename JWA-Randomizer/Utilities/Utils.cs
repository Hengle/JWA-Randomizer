using System;
using System.Linq;
using JWA_Randomizer.Enums;
using JWA_Randomizer.Utilities.Extensions;

namespace JWA_Randomizer.Utilities
{
    public static class Utils
    {
        public static Rarity GetRarity(DinosaurName dinosaurName)
        {
            switch (dinosaurName)
            {
                case DinosaurName.Invalid:
                    return Rarity.Invalid;
                case DinosaurName.Allosaurus:
                case DinosaurName.Apatosaurus:
                case DinosaurName.Deinocheirus:
                case DinosaurName.DimetrodonGen2:
                case DinosaurName.Dimorphodon:
                case DinosaurName.DracorexGen2:
                case DinosaurName.Einosaurus:
                case DinosaurName.Euoplocephalus:
                case DinosaurName.Gallimimus:
                case DinosaurName.Iguanodon:
                case DinosaurName.IrritatorGen2:
                case DinosaurName.Majungasaurus:
                case DinosaurName.Miragaia:
                case DinosaurName.MonolophosaurusGen2:
                case DinosaurName.Nundasuchus:
                case DinosaurName.Parasaurolophus:
                case DinosaurName.Sarcosuchus:
                case DinosaurName.Stegosaurus:
                case DinosaurName.Suchomimus:
                case DinosaurName.Tanycolagreus:
                case DinosaurName.Tarbosaurus:
                case DinosaurName.TriceratopsGen2:
                case DinosaurName.Velociraptor:
                    return Rarity.Common;
                case DinosaurName.Amargasaurus:
                case DinosaurName.BaronyxGen2:
                case DinosaurName.Carnataurus:
                case DinosaurName.ErlikosaurusGen2:
                case DinosaurName.Giraffatitan:
                case DinosaurName.Kaprosuchus:
                case DinosaurName.Nodosaurus:
                case DinosaurName.Ornithomimus:
                case DinosaurName.Proceratosaurus:
                case DinosaurName.Tenontosaurus:
                case DinosaurName.TyrannosaurusRexGen2:
                case DinosaurName.Utahraptor:
                    return Rarity.Rare;
                case DinosaurName.Einiasuchus:
                case DinosaurName.Suchotator:
                    return Rarity.RareHybrid;
                default:
                    return Rarity.Invalid;
            }
        }

        public static DinosaurType GetDinosaurType(DinosaurName dinosaurName)
        {
            switch (dinosaurName)
            {
                // Pure Chomper
                case DinosaurName.Allosaurus:
                case DinosaurName.Tarbosaurus:
                case DinosaurName.TyrannosaurusRexGen2:
                    return DinosaurType.Chomper;

                // Pure Tank
                case DinosaurName.Euoplocephalus:
                    return DinosaurType.Tank;

                // Pure Raptor
                case DinosaurName.Proceratosaurus:
                case DinosaurName.Utahraptor:
                case DinosaurName.Velociraptor:
                    return DinosaurType.Raptor;

                // Pure Dotter

                // Pure Wild Card
                case DinosaurName.DracorexGen2:
                    return DinosaurType.WildCard;
                
                // Pure Anti Raptor
                case DinosaurName.Stegosaurus:
                    return DinosaurType.AntiRaptor;

                // Pure Counter Attack

                // Pure Stunner
                case DinosaurName.Einiasuchus:
                case DinosaurName.Einosaurus:
                    return DinosaurType.Stunner;

                // Chomper & Dotter
                case DinosaurName.Suchotator:
                    return DinosaurType.Chomper | DinosaurType.Dotter;

                // Tank & Anti Raptor
                case DinosaurName.Amargasaurus:
                case DinosaurName.Apatosaurus:
                    return DinosaurType.Tank | DinosaurType.AntiRaptor;
                
                // Tank & Counter Attack
                case DinosaurName.Carnataurus:
                case DinosaurName.Miragaia:
                    return DinosaurType.Tank | DinosaurType.CounterAttack;
                
                // Not specified yet
                default:
                    return DinosaurType.Invalid;
            }
        }

        public static DinosaurName GetDinosaurName(string name)
        {
            var dinosaurName = (from DinosaurName dn in Enum.GetValues(typeof(DinosaurName))
                where
                dn != DinosaurName.Invalid &&
                (name.Equals(dn.ToString(), StringComparison.OrdinalIgnoreCase) ||
                 name.Equals(dn.ToSpacedString(), StringComparison.OrdinalIgnoreCase))
                select dn).FirstOrDefault();

            return dinosaurName;
        }

        public static RandomizerType GetRandomizerType(string type)
        {
            int typeInt;
            var randomizerType = (from RandomizerType rt in Enum.GetValues(typeof(RandomizerType))
                where
                rt != RandomizerType.Invalid &&
                (type.Equals(rt.ToString(), StringComparison.OrdinalIgnoreCase) ||
                 type.Equals(rt.ToSpacedString(), StringComparison.OrdinalIgnoreCase) ||
                 (int.TryParse(type, out typeInt) && typeInt == (int) rt))
                select rt).FirstOrDefault();

            return randomizerType;
        }
    }
}
