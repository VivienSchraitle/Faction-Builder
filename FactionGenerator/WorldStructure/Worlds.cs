using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactionGenerator
{
    public class Worlds
    {
        public int seed;
        public string Name { get; private set; }
        public List<Modifier> Modifiers { get; private set; }

        public List<World> MyWorlds { get; private set; }
        new Random random;

        public void init(string name, int size, int magicLevel, int techLevel, int resourceLevel, int godsLevel, int outerLevel, List<World> worlds)
        {
            Name = name;
            Modifiers.Add(new Modifier("Size", size));
            Modifiers.Add(new Modifier("Magic", magicLevel, (int)(magicLevel * 0.2f)));
            Modifiers.Add(new Modifier("Technology", techLevel, (int)(techLevel * 0.2f)));
            Modifiers.Add(new Modifier("Resources", resourceLevel, (int)(resourceLevel * 0.2f)));
            Modifiers.Add(new Modifier("Deities", godsLevel));
            Modifiers.Add(new Modifier("OuterBeings", outerLevel));
            MyWorlds = worlds;

            // Pass down base modifiers to each World
            foreach (var world in MyWorlds)
            {
                world.InheritModifiers(Modifiers, random);
                world.ApplyAdjustments();
                world.init();
            }
        }
        public void init(int seed)
        {
            random = new Random(seed.GetHashCode());
        }
    }
}

