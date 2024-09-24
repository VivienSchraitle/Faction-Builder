using System.Collections.Generic;
using FactionGenerator.WorldStructure;

namespace FactionGenerator
{
    public class Biome : WorldEntity
    {
        public WorldDataManager.BiomeData biomeData;
        public List<LocalRegion> LocalRegions { get; private set; }

        public void Init(string name, List<LocalRegion> localRegions)
        {
            Name = name;
            LocalRegions = localRegions;

            foreach (var region in LocalRegions)
            {
                region.InheritModifiers(Modifiers);
                region.ApplyAdjustments();
            }
        }
    }
}
