using System.Collections.Generic;

namespace JWA_Randomizer.Utilities.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Checks whether a List isn't instantiated or if the List
        /// doesn't contain anything
        /// </summary>
        /// <typeparam name="T">Generic type within the List</typeparam>
        /// <param name="list">List of objects to check</param>
        /// <returns>Returns true if the List is NULL or doesn't contain anything</returns>
        public static bool IsNullOrEmpty<T>(this List<T> list)
        {
            if (list == null)
                return true;

            return list.Count == 0;
        }
    }
}