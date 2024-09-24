using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactionGenerator
{
    public class Worlds
    {
        public string Name { get; private set; }
        public List<Modifier> Modifiers { get; private set; }
        public List<World> MyWorlds { get; private set; }

        public void Init(string name, int size, int magicLevel, int techLevel, int resourceLevel, int godsLevel, int outerBeingsLevel, int amountWorlds)
        {
            Name = name;
            Modifiers = new List<Modifier>();

            // Add base-level modifiers for the entire world set
            Modifiers.Add(new Modifier("Size", size));
            Modifiers.Add(new Modifier("Magic", magicLevel, (int)(magicLevel * 0.2f)));
            Modifiers.Add(new Modifier("Technology", techLevel, (int)(techLevel * 0.2f)));
            Modifiers.Add(new Modifier("Resources", resourceLevel, (int)(resourceLevel * 0.2f)));
            Modifiers.Add(new Modifier("Deities", godsLevel));
            Modifiers.Add(new Modifier("OuterBeings", outerBeingsLevel));

            // Initialize the list of worlds
            MyWorlds = new List<World>();

            for (int i = 0; i < amountWorlds; i++)
            {
                var world = new World();
                world.InheritModifiers(Modifiers);  // Each world inherits the base modifiers
                world.Init();                       // Now, the world generates its data based on the inherited modifiers
                world.ApplyAdjustments();           // Apply final adjustments after initialization
                MyWorlds.Add(world);                // Add the world to the list
            }
        }
    }
}
