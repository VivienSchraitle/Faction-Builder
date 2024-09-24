using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FactionGenerator.WorldStructure;

namespace FactionGenerator
{
    public class Continent : WorldEntity
    {

        public string Name { get; private set; }
        public List<Modifier> Modifiers { get; private set; }

        public List<VegetationZone> VegetationZones { get; private set; }

        public void Init()
        {
            foreach (var zone in VegetationZones)
            {
                zone.InheritModifiers(Modifiers);
                zone.ApplyAdjustments();
            }
        }
    }
}