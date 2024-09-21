using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia.Utilities;

public class People
{
    private const int LowFinanceThreshold = 70;
    private const int MidFinanceThreshold = 200;
    private const int HighFinanceThreshold = 350;
    private const int SuccessRateThreshold = 81;

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
        public string MyApperance;
        public string SkinColor;
        public string Undertones;
        public string HairColor;
        public string Hairstyle;
        public string EyeColor;
        public string[] SpecialTraits;
        public string Heritage;
        public int FinanceScore;
        public int ReputationScore;
        public int ReligionScore;
        Faction myFaction;
    }
    public void PopulateFaction(Faction myFaction)
    {
        string fullPath = Path.GetFullPath(@"Generated Factions");
        string dir = Directory.CreateDirectory(Path.Combine(fullPath + myFaction.FacName + "Members")).ToString();
        //using StreamWriter outputFile = new StreamWriter(Path.Combine(dir, myFaction.FacName, + ".md"));
        //todo
    }
    public void AddSinglePerson()
    {
        //for this feature I need to read out the generated factions folder so let's worry about that later todo
    }


    // Generates multiple people based on faction type
    public void GenerateFactionLeaders(Faction myFaction)
    {
  

        _localFaction = myFaction;
        string leadership = myFaction.Leadership;

        switch (leadership)
        {
            case "Anarchistic":
                // Intentionally left empty
                break;
            case "Democratic":
                HandleDemocraticFaction(myFaction);
                break;
            case "Oligarchic":
                GeneratePerson("Oligarch", myFaction.PowerType);
                break;
            case "Autocratic":
                GeneratePerson("Autocrat", myFaction.PowerType);
                break;
        }
    }

    // Generates a person based on faction or default criteria
    public Person GeneratePerson(string position = null, string type = null)
    {
        Person person = new();

        if (!string.IsNullOrEmpty(position) && !string.IsNullOrEmpty(type))
        {
            person.MyJob = $"{position} chosen through {type}";
        }
        else if (!string.IsNullOrEmpty(position))
        {
            person.MyJob = position;
        }
        else if (_localFaction != null)
        {
            AssignFactionBasedJob(person, _localFaction);
        }
        else
        {
            GetNonFactionJob(person);
        }

        return person;
    }

    private void HandleDemocraticFaction(Faction faction)
    {
        if (faction.VotingSystem.Contains("non elected leader"))
        {
            GeneratePerson("Non Elected Leader");
        }
        else if (faction.VotingSystem.Contains("elected leader"))
        {
            GeneratePerson("Elected Leader");
        }

        if (faction.VotingSystem.Contains("council"))
        {
            for (int i = 0; i < faction.assignedLeaders; i++)
            {
                GeneratePerson("Council Member");
            }
        }
    }


    private void AssignFactionBasedJob(Person person, Faction localFaction)
    {
        int randomScore = _random.Next(101);
        person.FinanceScore = Math.Clamp(localFaction.FinanceScore + _random.Next(-localFaction.FinanceScore / 3, localFaction.FinanceScore / 3), 0, 100);
        int totalScore = randomScore + 3 * FinanceScore;

        if (totalScore < LowFinanceThreshold)
        {
            person.MyJob = GetJobFromMapping(localFaction, "Low", DataManager.LowJobMappings);
        }
        else if (totalScore < MidFinanceThreshold)
        {
            person.MyJob = GetJobFromMapping(localFaction, "Mid", DataManager.MidJobMappings);
        }
        else if (totalScore < HighFinanceThreshold)
        {
            person.MyJob = GetJobFromMapping(localFaction, "High", DataManager.HighJobMappings);
        }
        else
        {
            person.MyJob = GetJobFromMapping(localFaction, "Insane", DataManager.SuperJobMappings);
        }
    }

    private string GetJobFromMapping(Faction localFaction, string moneySourceKey, Dictionary<string, List<string>> jobMappings)
    {
        if (_random.Next(101) < SuccessRateThreshold && localFaction.MoneySources[moneySourceKey].Count > 0)
        {
            string source = localFaction.MoneySources[moneySourceKey][_random.Next(localFaction.MoneySources[moneySourceKey].Count)];
            return jobMappings[source][_random.Next(jobMappings[source].Count)];
        }

        int randomIndex = _random.Next(jobMappings.Keys.Count);
        string randomKey = jobMappings.Keys.ElementAt(randomIndex);
        return jobMappings[randomKey][_random.Next(jobMappings[randomKey].Count)];
    }

    // Retrieves Ancestry
    public DataManager.Ancestry GetAncestry(string[] args)
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

        return _ancestry;
    }

    // Retrieves Heritage
    public DataManager.Heritage GetHeritage(string[] args)
    {
        int totalLikelihood = DataManager.Heritages.Sum(h => h.LH);
        int randomValue = _random.Next(totalLikelihood);

        foreach (var heritage in DataManager.Heritages)
        {
            randomValue -= heritage.LH;
            if (randomValue < 0)
            {
                _heritage = heritage;
                break;
            }
        }

        return _heritage;
    }

    // Generates a class based on random selection and adjustments
    public string GetClass()
    {
        int baseNumber = _random.Next(DataManager.Classes.Length - 4);
        baseNumber = Math.Clamp(baseNumber + _random.Next(-1, 3), 0, DataManager.Classes.Length - 1);
        return DataManager.Classes[baseNumber];
    }

    public string GetNonFactionJob(Person person)
    {
        return ""; // Placeholder for future logic
    }

    public string GetAppearance()
    {
        return ""; // Placeholder for future logic
    }

    private string AddTraits()
    {
        return ""; // Placeholder for future logic
    }
}
