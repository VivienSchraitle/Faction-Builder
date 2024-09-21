using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FactionGenerator.WorldStructure;

namespace FactionGenerator
{
public class World
{
    private Random random;
    public int continentStyle; //0 is pangea 100000 is only small islands
    public string Name { get; private set; }
    public List<Modifier> Modifiers { get; private set; }

    public List<Continent> Continents { get; private set; }
    WorldDataManager myWorldManager = new();

    public void init()
    {
        Name = GetWorldName();
        Continents = GetContinents();
        Modifiers.Add(new Modifier("Monsters", myWorldManager.Monsters));
        Modifiers.Add(new Modifier("Savegary", myWorldManager.Monsters));
        Modifiers.Add(new Modifier("NrCiv", myWorldManager.Monsters));

        // Pass down modifiers to each continent
        foreach (var continent in Continents)
        {
            continent.InheritModifiers(Modifiers, random);
            continent.ApplyAdjustments();
        }
    }

        private string GetWorldName()
        {
            throw new NotImplementedException(); //todo
        }

        private List<Continent> GetContinents()
        {
            throw new NotImplementedException();//todo
        }

        // Inherit base modifiers from the World Cluster
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