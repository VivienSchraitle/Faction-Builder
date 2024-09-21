using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactionGenerator
{
public class VegetationZone
{
   private Random random;
    public string Name { get; private set; }
    public List<Modifier> Modifiers { get; private set; }

    public List<Biome> Biomes { get; private set; }

    public void init(string name, List<Biome> biomes)
    {
        Name = name;
        Biomes = biomes;


        foreach (var biome in Biomes)
        {
            biome.InheritModifiers(Modifiers, random);
            biome.ApplyAdjustments();
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