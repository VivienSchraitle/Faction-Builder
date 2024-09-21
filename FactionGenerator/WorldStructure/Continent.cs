using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactionGenerator
{
public class Continent
{
   private Random random;
    public string Name { get; private set; }
    public List<Modifier> Modifiers { get; private set; }

    public List<VegetationZone> VegetationZones { get; private set; }

    public void init(string name, List<VegetationZone> vegetationZones)
    {
        Name = name;
        VegetationZones = vegetationZones;


        foreach (var zone in VegetationZones)
        {
            zone.InheritModifiers(Modifiers, random);
            zone.ApplyAdjustments();
        }
    }

    public void InheritModifiers(List<Modifier> upperModifiers, Random rnd)
    {
        random = rnd;
        Modifiers = upperModifiers;
    }
    public void ApplyAdjustments()
    {
        foreach(Modifier mod in Modifiers)
        {
            mod.Adjust(random);
        }
    }
}
}