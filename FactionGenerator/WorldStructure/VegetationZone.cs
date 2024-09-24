using System.Collections.Generic;
using FactionGenerator.WorldStructure;

namespace FactionGenerator
{
    public class VegetationZone : WorldEntity
    {
        public List<Biome> Biomes { get; private set; }

        public void Init(string name, List<Biome> biomes)
        {
            Name = name;
            Biomes = biomes;

            foreach (var biome in Biomes)
            {
                biome.InheritModifiers(Modifiers);
                biome.ApplyAdjustments();
            }
        }
    }
}
