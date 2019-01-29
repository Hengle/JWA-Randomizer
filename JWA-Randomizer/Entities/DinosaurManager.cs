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
        private readonly List<Dinosaur> _dinosaurs;

        public int DinosaurCount => _dinosaurs.Count;

        public DinosaurManager(string fileName)
        {
            _fileName = fileName;
            _dinosaurs = new List<Dinosaur>();
        }

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

        public void LoadDinosaurs()
        {
            var doc = new XmlDocument();
            doc.Load(_fileName);

            if (doc.DocumentElement == null)
            {
                return;
            }

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if (node == null || !node.Name.Equals("dinosaur", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                string name = null;
                var level = -1;
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    if (childNode == null)
                    {
                        continue;
                    }

                    if (childNode.Name.Equals("name", StringComparison.OrdinalIgnoreCase))
                    {
                        name = childNode.InnerText;
                    } else if (childNode.Name.Equals("level", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!int.TryParse(childNode.InnerText, out level))
                        {
                            level = -1;
                        }
                    }
                }

                if (string.IsNullOrEmpty(name) || level <= 0)
                {
                    continue;
                }

                var dinosaur = new Dinosaur(Utils.GetDinosaurName(name), level);

                _dinosaurs.Add(dinosaur);
            }
        }

        public List<Dinosaur> GetTeam(RandomizerType type)
        {
            var randomizer = GetRandomizer(type);
            var team = randomizer?.GetTeam(_dinosaurs);

            return !team.IsNullOrEmpty() ? team?.OrderByDescending(t => t.Level).ToList() : null;
        }
    }
}
