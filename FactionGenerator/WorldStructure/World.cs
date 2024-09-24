using System;
using System.Collections.Generic;
using FactionGenerator.WorldStructure;

namespace FactionGenerator
{
    public class World : WorldEntity
    {
        public WorldDataManager.WorldData thisWorld; // World data object
        public List<Continent> Continents { get; private set; }

        public void Init()
        {
            // Generate dynamic values based on modifiers or create them
            int size = GetModifierValue("Size", 50000); // Example: default 50000 if no modifier
            int magicLevel = GetModifierValue("Magic", 1000);
            int techLevel = GetModifierValue("Technology", 1000);
            int resourceLevel = GetModifierValue("Resources", 1000);
            int godsLevel = GetModifierValue("Deities", 1000);
            int outerBeingsLevel = GetModifierValue("OuterBeings", 1000);

            // Initialize World Data dynamically from modifier values
            thisWorld = new WorldDataManager.WorldData
            {
                Name = GetWorldName(), // Generate or fetch world name
                Size = size,
                MagicLevel = magicLevel,
                TechLevel = techLevel,
                ResourceLevel = resourceLevel,
                GodsLevel = godsLevel,
                OuterBeingsLevel = outerBeingsLevel,
                WorldCharacteristics = getWorldType(),
                Continents = new List<Continent>()
            };

            // Generate continents and pass down modifiers
            Continents = GenerateContinents();
        }

        private WorldDataManager.WorldTypeData getWorldType()
        {
            throw new NotImplementedException();
        }

        // Helper method to extract values from modifiers or use a default
        private int GetModifierValue(string modifierName, int defaultValue)
        {
            var mod = Modifiers.Find(m => m.Name == modifierName);
            return mod != null ? mod.FinalValue : defaultValue;
        }

        // Generates continents and applies modifiers to each
        private List<Continent> GenerateContinents()
        {
            int continentCount = thisWorld.WorldCharacteristics.MaxContinents;
            var continents = new List<Continent>();

            for (int i = 0; i < continentCount; i++)
            {
                var continent = new Continent();
                continent.Init(); // Initialize continent with its name
                continent.InheritModifiers(Modifiers); // Pass down the world-level modifiers to the continent
                continent.ApplyAdjustments();
                continents.Add(continent);
            }

            return continents;
        }

        private string GetWorldName()
        {
            // Replace this with a name generator or fetch from data
            return "GeneratedWorld_" + Guid.NewGuid().ToString().Substring(0, 8);
        }
    }
}
