using System;
using System.Collections.Generic;
using System.Linq;

namespace FactionGenerator.WorldStructure
{
    public abstract class WorldEntity
    {
        public string Name { get; protected set; }
        public List<Modifier> Modifiers { get; protected set; } = new List<Modifier>();

        public void InheritModifiers(List<Modifier> parentModifiers)
        {
            foreach (var parentMod in parentModifiers)
            {
                var existingMod = Modifiers.FirstOrDefault(m => m.Name == parentMod.Name);
                if (existingMod != null)
                {
                    existingMod.SetAdjustment(parentMod.FinalValue); // Adjust based on parent's final value
                }
                else
                {
                    Modifiers.Add(new Modifier(parentMod.Name, parentMod.FinalValue));
                }
            }
        }

        public void ApplyAdjustments()
        {
            foreach (Modifier mod in Modifiers)
            {
                mod.Adjust(DataManager.random); // Adjust based on RNG or a set rule
            }
        }
    }
}
