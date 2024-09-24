using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Avalonia.Utilities;

public class People
{
    private const int LowFinanceThreshold = 70;
    private const int MidFinanceThreshold = 200;
    private const int HighFinanceThreshold = 350;

    private DataManager.Ancestry _ancestry;
    private DataManager.Heritage _heritage;
    private Random _random = new Random();
    private Faction _localFaction;

    public int FinanceScore { get; private set; }
    public int ReputationScore { get; private set; }
    public int ReligionScore { get; private set; }

    public struct Person
    {
        public string Name;
        public float Size;
        public int Age;
        public string MyClass;
        public string MyJob;
        public string SkinColor;
        public string Undertones;
        public string HairColor;
        public string Hairstyle;
        public string EyeColor;
        public string[] SpecialTraits;
        public DataManager.Ancestry Ancestry;
        public DataManager.Heritage Heritage;
        public int FinanceScore;
        public int ReputationScore;
        public int ReligionScore;
        public Faction myFaction;
    }
    public void PopulateFaction(Faction myFaction) // starter for full faction population
    {
        _localFaction = myFaction;

        // Create the base faction folder
        string fullPath = Path.GetFullPath(@"Generated Factions");
        string factionDir = Directory.CreateDirectory(Path.Combine(fullPath, myFaction.FacName, "Members")).ToString();

        // List to store faction people
        List<Person> factionPeople = new();

        // Populate faction leaders if the faction is not "Anarchistic"
        if (_localFaction.Leadership != "Anarchistic")
        {
            factionPeople = GenerateFactionLeaders();

            // Create the leaders folder inside the faction folder
            string leadersDir = Directory.CreateDirectory(Path.Combine(fullPath, myFaction.FacName, "Leaders")).ToString();

            for (int i = 0; i < factionPeople.Count; i++) // Fixed the loop condition
            {
                Person leader = factionPeople.ElementAt(i);

                // Create a markdown file for each leader in the "Leaders" folder
                using StreamWriter outputFile = new StreamWriter(Path.Combine(leadersDir, leader.Name + ".md"));
                outputFile.WriteLine(leader.Name);
                outputFile.WriteLine("Age: " + leader.Age);
                outputFile.WriteLine("Height: " + leader.Size);
                outputFile.WriteLine("Job: " + leader.MyJob);
                outputFile.WriteLine("Class: " + (leader.MyClass ?? "None"));
                outputFile.WriteLine("Skin: " + leader.SkinColor + " with " + leader.Undertones);
                outputFile.WriteLine("Hair: " + leader.HairColor + " Hair in a " + leader.Hairstyle);
                outputFile.WriteLine("Eyes: " + leader.EyeColor);
                outputFile.WriteLine("Special Traits: " + string.Join(", ", leader.SpecialTraits));
            }
        }

        // Reset factionPeople list for regular members
        factionPeople = new List<Person>();

        // Populate faction members based on SizeScale
        for (int i = 0; i < _localFaction.SizeScale; i++) // Fixed the loop condition
        {
            Person member = factionPeople.ElementAt(i);

            // Create a markdown file for each member in the "Members" folder
            using StreamWriter outputFile = new StreamWriter(Path.Combine(factionDir, member.Name + ".md"));
            outputFile.WriteLine(member.Name);
            outputFile.WriteLine("Age: " + member.Age);
            outputFile.WriteLine("Height: " + member.Size);
            outputFile.WriteLine("Job: " + member.MyJob);
            outputFile.WriteLine("Class: " + (member.MyClass ?? "None"));
            outputFile.WriteLine("Skin: " + member.SkinColor + " with " + member.Undertones);
            outputFile.WriteLine("Hair: " + member.HairColor + " Hair in a " + member.Hairstyle);
            outputFile.WriteLine("Eyes: " + member.EyeColor);
            outputFile.WriteLine("Special Traits: " + string.Join(", ", member.SpecialTraits));
        }
    }

    public void AddSinglePerson()
    {
        //for this feature I need to read out the generated factions folder so let's worry about that later todo
    }


    // Generates multiple people based on faction type
    public List<Person> GenerateFactionLeaders()
    {
        string leadership = _localFaction.Leadership;
        List<Person> person = new();
        switch (leadership)
        {
            case "Democratic":
                person = HandleDemocraticFaction();
                break;
            case "Oligarchic":
                person.Add(GeneratePerson("Oligarch", _localFaction.PowerType));
                break;
            case "Autocratic":
                person.Add(GeneratePerson("Autocrat", _localFaction.PowerType));
                break;
            default:
                break;
        }
        return person;
    }

    // Generates a person based on faction or default criteria
    public Person GeneratePerson(string position = null, string type = null)
    {
        Person person = new();
        person.myFaction = _localFaction;



        if (!string.IsNullOrEmpty(position) && !string.IsNullOrEmpty(type))
        {
            person.MyJob = $"{position} chosen through {type}";
            person.FinanceScore = _random.Next(_localFaction.FinanceScore, 100);
            person.ReligionScore = _random.Next(0, 100);
            person.ReputationScore = _random.Next(0, 100);
        }
        else if (!string.IsNullOrEmpty(position))
        {
            person.MyJob = position;
            person.FinanceScore = Math.Clamp(_localFaction.FinanceScore + _random.Next(-10, 10), 0, 100);
            person.ReligionScore = Math.Clamp(_localFaction.ReligionScore + _random.Next(-10, 10), 0, 100);
            person.ReputationScore = Math.Clamp(_localFaction.ReputationScore + _random.Next(-10, 10), 0, 100);
        }
        else if (_localFaction != null)
        {
            person.FinanceScore = _random.Next(_localFaction.FinanceScore);
            person.ReligionScore = _random.Next(0, 100);
            person.ReputationScore = _random.Next(_localFaction.ReputationScore);
            AssignFactionBasedJob(person);

        }
        else
        {
            person.FinanceScore = Math.Clamp(_localFaction.FinanceScore + _random.Next(-30, 30), 0, 100);
            person.ReligionScore = _random.Next(0, 100);
            person.ReputationScore = _random.Next(0, 100);
            GetNonFactionJob(person);

        }
        if (_localFaction == null)
        {
            GetAncestry(person);
            GetHeritage(person);
        }
        return person;
    }
    public Person GenerateUnafiliatedPerson()
    {
        Person person = new();
        person.FinanceScore = _random.Next(0, 100);
        person.ReligionScore = _random.Next(0, 100);
        person.ReputationScore = _random.Next(0, 100);
        GetNonFactionJob(person);
        GetAncestry(person);
        GetHeritage(person);
        GetName(person);
        SetAppearance(person);
        return person;

    }

    private List<Person> HandleDemocraticFaction()
    {
        
        List<Person> Leaders = new();
        if (_localFaction == null) 
        throw new ArgumentNullException("_localFaction", "Faction cannot be null.");
        if (_localFaction.VotingSystem.Contains("non elected leader"))
        {
            Leaders.Add(GeneratePerson("Non Elected Leader"));
        }
        else if (_localFaction.VotingSystem.Contains("elected leader"))
        {
            Leaders.Add(GeneratePerson("Elected Leader"));
        }

        if (_localFaction.VotingSystem.Contains("council"))
        {
            for (int i = 0; i < _localFaction.assignedLeaders; i++)
            {
                Leaders.Add(GeneratePerson("Council Member"));
            }
        }
        return Leaders;
    }


    private void AssignFactionBasedJob(Person person)
    {
        if (_localFaction == null) 
        throw new ArgumentNullException("_localFaction", "Faction cannot be null.");
        int randomScore = _random.Next(101);
        person.FinanceScore = Math.Clamp(_localFaction.FinanceScore + _random.Next(-_localFaction.FinanceScore / 3, _localFaction.FinanceScore / 3), 0, 100);
        int totalScore = randomScore + 3 * FinanceScore;

        if (totalScore < LowFinanceThreshold)
        {
            person.MyJob = GetJobFromMapping("Low", DataManager.LowJobMappings, 20, person);
        }
        else if (totalScore < MidFinanceThreshold)
        {
            person.MyJob = GetJobFromMapping("Mid", DataManager.MidJobMappings, 40, person);
        }
        else if (totalScore < HighFinanceThreshold)
        {
            person.MyJob = GetJobFromMapping("High", DataManager.HighJobMappings, 60, person);
        }
        else
        {
            person.MyJob = GetJobFromMapping("Insane", DataManager.SuperJobMappings, 80, person);
        }
    }

    private string GetJobFromMapping(string moneySourceKey, Dictionary<string, List<string>> jobMappings, int SuccessRateThreshold, Person pepe)
    {
        if (_localFaction == null) 
        throw new ArgumentNullException("_localFaction", "Faction cannot be null.");
        if (_random.Next(101) > SuccessRateThreshold && _localFaction.MoneySources[moneySourceKey].Count > 0)
        {
            string source = _localFaction.MoneySources[moneySourceKey][_random.Next(_localFaction.MoneySources[moneySourceKey].Count)];
            return jobMappings[source][_random.Next(jobMappings[source].Count)];
        }
        return GetNonFactionJob(pepe);
    }

    // Retrieves Ancestry
    public DataManager.Ancestry GetAncestry(Person person)
    {
        int totalLikelihood = DataManager.Ancestries.Sum(a => a.LH);
        int randomValue = _random.Next(totalLikelihood);

        foreach (var ancestry in DataManager.Ancestries)
        {
            randomValue -= ancestry.LH;
            if (randomValue < 0)
            {
                _ancestry = ancestry;
                break;
            }
        }
        person.Ancestry = _ancestry;

        return _ancestry;
    }

    // Retrieves Heritage
    public DataManager.Heritage GetHeritage(Person person)
    {

        int totalLikelihood = DataManager.Heritages.Sum(h => h.LH);
        int randomValue = _random.Next(totalLikelihood);
        if (_random.Next(101) < 50)
        {
            person.Heritage = _heritage = DataManager.Heritages.Where(a => a.Name == person.Ancestry.Name).First();
        }
        else
        {
            foreach (var heritage in DataManager.Heritages)
            {
                randomValue -= heritage.LH;
                if (randomValue < 0)
                {
                    _heritage = heritage;
                    break;
                }
            }
        }
        return _heritage;
    }

    // Generates a class based on random selection and adjustments
    public string GetClass(Person person)
    {
        int baseNumber = _random.Next(DataManager.Classes.Length - 4);
        baseNumber = Math.Clamp(baseNumber + _random.Next(-1, 3), 0, DataManager.Classes.Length - 1);
        person.MyClass = DataManager.Classes[baseNumber];
        return DataManager.Classes[baseNumber];
    }

    public string GetNonFactionJob(Person person)
    {
        if (person.FinanceScore < LowFinanceThreshold)
        {
            string randomKey = DataManager.LowJobMappings.Keys.ElementAt(DataManager.LowJobMappings.Keys.Count);
            person.MyJob = DataManager.LowJobMappings[randomKey][_random.Next(DataManager.LowJobMappings[randomKey].Count)];
        }
        else if (person.FinanceScore < MidFinanceThreshold)
        {
            string randomKey = DataManager.MidJobMappings.Keys.ElementAt(DataManager.MidJobMappings.Keys.Count);
            person.MyJob = DataManager.MidJobMappings[randomKey][_random.Next(DataManager.MidJobMappings[randomKey].Count)];
        }
        else if (person.FinanceScore < HighFinanceThreshold)
        {
            string randomKey = DataManager.HighJobMappings.Keys.ElementAt(DataManager.HighJobMappings.Keys.Count);
            person.MyJob = DataManager.HighJobMappings[randomKey][_random.Next(DataManager.HighJobMappings[randomKey].Count)];
        }
        else
        {
            string randomKey = DataManager.SuperJobMappings.Keys.ElementAt(DataManager.SuperJobMappings.Keys.Count);
            person.MyJob = DataManager.SuperJobMappings[randomKey][_random.Next(DataManager.SuperJobMappings[randomKey].Count)];
        }
        return person.MyJob;
    }

    public void SetAppearance(Person person)
    {
        person.EyeColor = (_random.Next(101) > 50) ? person.Ancestry.EyeColor.ElementAt(_random.Next(person.Ancestry.EyeColor.Length)) : person.Heritage.EyeColor.ElementAt(_random.Next(person.Heritage.EyeColor.Length));
        person.HairColor = (_random.Next(101) > 50) ? person.Ancestry.HairColor.ElementAt(_random.Next(person.Ancestry.HairColor.Length)) : person.Heritage.HairColor.ElementAt(_random.Next(person.Heritage.HairColor.Length));
        person.Hairstyle = (_random.Next(101) > 50) ? person.Ancestry.Hairstyles.ElementAt(_random.Next(person.Ancestry.Hairstyles.Length)) : person.Heritage.Hairstyles.ElementAt(_random.Next(person.Heritage.Hairstyles.Length));
        person.SkinColor = (_random.Next(101) > 50) ? person.Ancestry.SkinColor.ElementAt(_random.Next(person.Ancestry.SkinColor.Length)) : person.Heritage.SkinColor.ElementAt(_random.Next(person.Heritage.SkinColor.Length));
        List<string> traitList = new();
        traitList.AddRange(GetSingleElementFromEachArray(person.Ancestry.CertainSpecialTraits).ToList());
        traitList.AddRange(GetSingleElementFromEachArray(person.Heritage.CertainSpecialTraits).ToList());
        traitList.AddRange(GetSingleElementFromEachArrayOptional(person.Ancestry.OptionalSpecialTraits).ToList());
        traitList.AddRange(GetSingleElementFromEachArrayOptional(person.Heritage.OptionalSpecialTraits).ToList());
        person.SpecialTraits = traitList.ToArray();
        person.Age = _random.Next(person.Ancestry.MatureAge, person.Ancestry.MaxAge);
        person.Size = person.Ancestry.SizeAvg + _random.Next((int)(person.Ancestry.SizeDev * 10)) / 10;
    }
    public string[] GetSingleElementFromEachArray(string[][] certainSpecialTraits)
    {
        return certainSpecialTraits
            .Where(traitArray => traitArray.Length > 0)  // Ensure non-empty arrays
            .Select(traitArray => traitArray[_random.Next(traitArray.Length)])  // Select a random element from each array
            .ToArray();
    }
    public string[] GetSingleElementFromEachArrayOptional(string[][] optionalSpecialTraits)
    {
        return optionalSpecialTraits
            .Where(traitArray => traitArray.Length > 0 && _random.Next(101) > 30)  // Ensure non-empty arrays
            .Select(traitArray => traitArray[_random.Next(traitArray.Length)])  // Select a random element from each array
            .ToArray();
    }
    public string GetName(Person pepe)
    {
        pepe.Name = Guid.NewGuid().ToString() + Guid.NewGuid().ToString() + Guid.NewGuid().ToString();
        return pepe.Name;// todo actually get names lmaooo
    }
}
