using System.Collections.Generic;

namespace JWA_Randomizer.Utilities.Extensions
{
    public static class ListExtensions
    {
        public static bool IsNullOrEmpty<T>(this List<T> list)
        {
            if (list == null)
                return true;

            if (list.Count == 0)
                return true;

            return false;
        }
    }
}
