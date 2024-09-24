using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FactionGenerator.WorldStructure;

namespace FactionGenerator
{
    public class LocalRegion : WorldEntity
    {
        public string Name { get; private set; }
        public List<Modifier> Modifiers { get; private set; }

        public LocalRegion(string name)
        {
            Name = name;
        }

    }
}