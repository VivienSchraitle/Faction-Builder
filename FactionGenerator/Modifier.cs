using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactionGenerator
{
    public class Modifier
{
    public string Name { get; private set; } // e.g., "Magic"
    public int BaseValue { get; private set; } // Base value, passed from parent
    public int Adjustment { get; private set; } // Adjustment applied at the current level

    public Modifier(string name, int baseValue, int adjustment = 0)
    {
        Name = name;
        BaseValue = baseValue;
        Adjustment = adjustment;
    }

    // Final value after applying adjustment
    public int FinalValue => BaseValue + Adjustment;

    // Method to adjust the value
    public void SetAdjustment(int adjustment)
    {
        Adjustment = adjustment;
    }
    public void Adjust(Random rnd)
    {
        BaseValue = Math.Clamp(BaseValue + rnd.Next(-Adjustment, Adjustment),0,100000);
    }
    // Display the final value
    public override string ToString()
    {
        return $"{Name}: {FinalValue} (Base: {BaseValue}, Adjustment: {Adjustment})";
    }
}
}