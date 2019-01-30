using System;
using System.Linq;

namespace JWA_Randomizer.Utilities.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Converts a raw Enum value into a string with spaces between capital letters.
        /// For example, DinosaurName.TyrannosaurusRexGen2.ToSpacedString() = "Tyrannosaurus Rex Gen 2"
        /// </summary>
        /// <param name="enumerable">The Enum to space out into a string</param>
        /// <returns>Returns a string representation of the Enum with spaces between capital letters</returns>
        public static string ToSpacedString(this Enum enumerable)
        {
            // Insert spaces right before each capital letter and number (i.e.  "TestValue2" becomes "Test Value 2")
            return
                string.Concat(
                        enumerable.ToString().Select(x => char.IsUpper(x) || char.IsNumber(x) ? " " + x : x.ToString()))
                    .TrimStart(' ');
        }
    }
}