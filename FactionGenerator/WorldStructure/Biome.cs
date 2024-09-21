using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactionGenerator
{
public class Biome
{
   private Random random;
    public string Name { get; private set; }
    public List<Modifier> Modifiers { get; private set; }

    public List<LocalRegion> LocalRegions { get; private set; }

    public void init(string name, List<LocalRegion> localRegions)
    {
        Name = name;
        LocalRegions = localRegions;

        foreach (var region in LocalRegions)
        {
            region.InheritModifiers(Modifiers, random);
            region.ApplyAdjustments();
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