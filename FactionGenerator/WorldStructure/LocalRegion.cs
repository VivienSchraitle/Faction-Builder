using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactionGenerator
{
public class LocalRegion
{
   private Random random;
    public string Name { get; private set; }
    public List<Modifier> Modifiers { get; private set; }

    public LocalRegion(string name)
    {
        Name = name;
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