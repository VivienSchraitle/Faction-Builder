using System;
using System.Collections.Generic;

namespace FactionGenerator.WorldStructure
{
    public class WorldDataManager
    {
        public struct WorldData
        {
            public string Name;
            public int Size;
            public int MagicLevel;
            public int TechLevel;
            public int ResourceLevel;
            public int GodsLevel;
            public int OuterBeingsLevel;
            public int Monsters;
            public int Savagery;
            public int CivilizationCount;
            public WorldTypeData WorldCharacteristics;
            public List<Continent> Continents;
        }

        public struct WorldTypeData
        {
            public double OceanPercent;
            public int ContinentStyle;
            public int MaxContinents;
            public int CivilizedLevel;
            public float SizeVariance;
            public int TemperatureRange;
        }

        public struct ContinentData
        {
            public string Name;
            public int Size;
            public int[][] Coordinates;
            public int CivilizedLevel;
            public int Savagery;
            public int Temperature;
            public int ResourceLevel;
            public List<VegetationZone> VegetationZones;
        }

        public struct VegetationZoneData
        {
            public string Name;
            public int Size;
            public int[][] Coordinates;
            public int Temperature;
            public int ResourceLevel;
            public List<Biome> Biomes;
        }

        public struct BiomeData
        {
            public string Name;
            public int Size;
            public int[][] Coordinates;
            public int ClimateType;
            public int ResourceLevel;
            public List<LocalRegion> LocalRegions;
        }

        public struct LocalRegionData
        {
            public string Name;
            public int Population;
            public int ResourceLevel;
            public int DangerLevel;
            public int CivilizationPresence;
        }
    }
}
