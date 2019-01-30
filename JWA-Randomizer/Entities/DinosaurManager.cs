using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using JWA_Randomizer.Enums;
using JWA_Randomizer.Randomizers;
using JWA_Randomizer.Utilities.Extensions;
using JWA_Randomizer.Utilities;

namespace JWA_Randomizer.Entities
{
    public class DinosaurManager
    {
        private readonly string _fileName;
        //private readonly List<Dinosaur> _dinosaurs;

        private readonly Dictionary<DinosaurName, Dinosaur> _dinosaurs;

        public int DinosaurCount => _dinosaurs.Count;

        /// <summary>
        /// Constructor for the DinosaurManager
        /// </summary>
        /// <param name="fileName">Filename/location for the XML dinosaur list</param>
        public DinosaurManager(string fileName)
        {
            _fileName = fileName;
            //_dinosaurs = new List<Dinosaur>();

            _dinosaurs = new Dictionary<DinosaurName, Dinosaur>();
        }

        /// <summary>
        /// Instantiates a DinosaurRandomizer based on the randomizer type selected
        /// </summary>
        /// <param name="randomizerType">Randomizer type selected</param>
        /// <returns>Returns the requested DinosaurRandomizer</returns>
        private static IDinosaurRandomizer GetRandomizer(RandomizerType randomizerType)
        {
            switch (randomizerType)
            {
                case RandomizerType.Invalid:
                    return null;
                case RandomizerType.Standard:
                    return new DinosaurRandomizer();
                case RandomizerType.Weighted:
                    return new DinosaurWeightedRandomizer();
                case RandomizerType.Balanced:
                    return new DinosaurBalancedRandomizer();
                default:
                    return null;
            }
        }

        /// <summary>
        /// Reads the list of dinosaurs from the configured XML file, populating a list of all
        /// available dinosaurs to choose from
        /// </summary>
        public void LoadDinosaurs()
        {
            // Prepare the XML document
            var doc = new XmlDocument();
            doc.Load(_fileName);

            // If there isn't an element, don't do anything
            if (doc.DocumentElement == null)
            {
                return;
            }

            // Iterate through the child nodes, only caring about those labeled "dinosaur"
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                // Ignore if there isn't a node or if it isn't of type "dinosaur"
                if (node == null || !node.Name.Equals("dinosaur", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                string name = null;
                var level = -1;

                // Iterate through the "dinosaur" child nodes to find its "name" and "level"
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    // Don't do anything if there isn't a child node
                    if (childNode == null)
                    {
                        continue;
                    }

                    // Found the "name"
                    if (childNode.Name.Equals("name", StringComparison.OrdinalIgnoreCase))
                    {
                        name = childNode.InnerText;
                    }
                    // Found the "level"
                    else if (childNode.Name.Equals("level", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!int.TryParse(childNode.InnerText, out level))
                        {
                            level = -1;
                        }
                    }
                }

                // If we don't have a name or a valid level (greater than 0), no dinosaur to add here
                if (string.IsNullOrEmpty(name) || level <= 0)
                {
                    continue;
                }

                // Attempt to get the DinosaurName for the dinosaur
                var dinosaurName = Utils.GetDinosaurName(name);

                // If we couldn't get the DinosaurName, or we already added a dinosaur with this
                // DinosaurName, don't add the dinosaur now
                if (dinosaurName == DinosaurName.Invalid || _dinosaurs.ContainsKey(dinosaurName))
                {
                    continue;
                }

                // Add the dinosaur
                var dinosaur = new Dinosaur(dinosaurName, level);
                _dinosaurs.Add(dinosaurName, dinosaur);
            }
        }

        /// <summary>
        /// Given a randomizer type, generate a team of dinosaurs from those available
        /// </summary>
        /// <param name="type">Randomizer type to use</param>
        /// <returns>Returns 8 dinosaurs to use in a team</returns>
        public List<Dinosaur> GetTeam(RandomizerType type)
        {
            // Get the selected randomizer
            var randomizer = GetRandomizer(type);

            // If we couldn't get the randomizer, abort
            if (randomizer == null)
            {
                throw new Exception("Unable to get randomizer.");
            }

            // Get the randomized team of dinosaurs from those available
            var team = randomizer.GetTeam(_dinosaurs.Values.ToList());

            // If we got no team, abort
            if (team.IsNullOrEmpty())
            {
                throw new Exception("Unable to get team.");
            }

            // Return the team of dinosaurs, ordered by highest level
            return team.OrderByDescending(t => t.Level).ToList();
        }
    }
}