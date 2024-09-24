// Faction.cs
using System;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

public class Faction
{
    /// <summary>
    /// defining scores for faction generation
    /// </summary>
    public int MagicScore { get; private set; }
    public int IntensityScore { get; private set; }
    public int SizeScale { get; private set; }
    public int MilitaristicScore { get; private set; }
    public int FinanceScore { get; private set; }
    public int ReputationScore { get; private set; }
    public int ReligionScore { get; private set; }



    /// <summary>
    /// definign scores for person generaton
    /// </summary>

    public int primPercent;
    public int secPercent;
    public DataManager.Heritage primHeritage;
    public DataManager.Ancestry primAncestry;

    public int assignedLeaders;
    public DataManager.Heritage secHeritage;
    public DataManager.Ancestry secAncestry;

    /// <summary>
    ///generated values for faction design
    /// </summary>
    public string FacName = ""; // Initialize to empty string
    public string[] GoalsArray = Array.Empty<string>(); // Initialize to empty array
    public string[] DomainsArray = Array.Empty<string>(); // Initialize to empty array
    public string Leadership = ""; // Initialize to empty string
    public string JoinRitual = "";
    public string PowerParameters = "";
    public string[] Values = Array.Empty<string>();
    public Dictionary<string, List<string>> MoneySources = new(); // Initialize to an empty dictionary
    public string[] Doctrines = Array.Empty<string>();

    // Standings as an array of strings
    public string[] StandingsArray { get; private set; }

    public string PowerType;
    public string VotingSystem;

    /// <summary>
    /// a random for some spice in generation add seed here
    /// </summary>
    private static Random rnd = new();

    public Faction(int scale, int money, int magic, int military, int religious, int reputation, int pPercent, int sPercent, int intensity, string primHeri, string secoHeri)
{
    MagicScore = magic;
    MilitaristicScore = military;
    FinanceScore = money;
    SizeScale = scale;
    ReputationScore = reputation;
    primPercent = pPercent;
    secPercent = sPercent;
    IntensityScore = intensity;
    ReligionScore = religious;

    // Handle missing ancestry or heritage
    if (DataManager.Ancestries.Exists(a => a.Name.Equals(primHeri)))
    {
        primAncestry = DataManager.Ancestries.First(h => h.Name.Equals(primHeri));
    }
    else if (DataManager.Heritages.Exists(a => a.Name.Equals(primHeri)))
    {
        primHeritage = DataManager.Heritages.First(h => h.Name.Equals(primHeri));
    }
    else
    {
        throw new ArgumentException($"Primary heritage or ancestry '{primHeri}' not found.");
    }

    if (DataManager.Ancestries.Exists(a => a.Name.Equals(secoHeri)))
    {
        secAncestry = DataManager.Ancestries.First(h => h.Name.Equals(secoHeri));
    }
    else if (DataManager.Heritages.Exists(a => a.Name.Equals(secoHeri)))
    {
        secHeritage = DataManager.Heritages.First(h => h.Name.Equals(secoHeri));
    }
    else
    {
        throw new ArgumentException($"Secondary heritage or ancestry '{secoHeri}' not found.");
    }
}


    public string GetName()
    {
        string name = "";
        long helper = rnd.NextInt64(100);
        if (helper > 70)
        {
            name = "The ";
        }

        name += DataManager.Attribute[rnd.NextInt64(DataManager.Attribute.Length)] + " ";
        name += DataManager.MainName[rnd.NextInt64(DataManager.MainName.Length)];
        FacName = name;
        return name;
    }

    public string GetDomains()
    {
        string domains = "Their fields of concern: ";
        int amount = (SizeScale / 10) + IntensityScore;
        int rndIndex = rnd.Next(50, 100);
        int distributer = Math.Max(1, rndIndex + MagicScore + MilitaristicScore + ReligionScore); // Ensure it's >= 1
        int magicAmount = (int)Math.Round((double)amount / distributer);
        int militaryAmount = (int)Math.Round((double)amount / distributer);
        int religionAmount = (int)Math.Round((double)ReligionScore / distributer);

        DomainsArray = new string[amount];

        for (int i = 0; i < amount - magicAmount - militaryAmount - religionAmount; i++)
        {
            DomainsArray[i] = DataManager.FactionDomainsMundane[rnd.NextInt64(DataManager.FactionDomainsMundane.Length)];
            domains += DomainsArray[i] + ", ";
        }
        for (int i = amount - magicAmount - militaryAmount - religionAmount; i < amount - militaryAmount - religionAmount; i++)
        {
            DomainsArray[i] = DataManager.FactionDomainsMagical[rnd.NextInt64(DataManager.FactionDomainsMagical.Length)];
            domains += DomainsArray[i] + ", ";
        }
        for (int i = amount - militaryAmount - religionAmount; i < amount - religionAmount; i++)
        {
            DomainsArray[i] = DataManager.FactionDomainsMilitary[rnd.NextInt64(DataManager.FactionDomainsMilitary.Length)];
            domains += DomainsArray[i] + ", ";
        }
        for (int i = amount - religionAmount; i < amount; i++)
        {
            DomainsArray[i] = DataManager.FactionDomainsReligious[rnd.NextInt64(DataManager.FactionDomainsReligious.Length)];
            domains += DomainsArray[i] + ", ";
        }
        domains += "and they are defined by their craving for: " + DataManager.FactionEssence[rnd.NextInt64(DataManager.FactionEssence.Length)];

        return domains;
    }

    public string GetGoals()
    {
        int amount = (SizeScale / 10) + IntensityScore;
        GoalsArray = new string[amount];
        string goals = "Their main goals are: \n";
        for (int i = 0; i < amount; i++)
        {
            GoalsArray[i] = DataManager.FactionGoals[rnd.NextInt64(DataManager.FactionGoals.Length)];
            goals += GoalsArray[i] + ", ";
        }
        return goals;
    }

    public string GetStyle()
    {
        string styleDescription = "Their organization style is: ";

        // Set Leadership style
        Leadership = DetermineLeadership();

        // Set Join Ritual
        JoinRitual = DetermineJoinRitual();

        // Set Power Parameters
        PowerParameters = DeterminePowerParameters();

        return styleDescription + Leadership + "\n" + JoinRitual + "\n" + PowerParameters;
    }

    private string DetermineLeadership()
    {
        int rndScore = (int)rnd.NextInt64(100);
        if (IntensityScore == 1)
        {
            if (rndScore < 25)
                return DataManager.SourceOfPower[0]; // Anarchistic
            if (rndScore <= 58)
            {
                assignedLeaders = rnd.Next(5, SizeScale * 4 + 5);
                return DataManager.SourceOfPower[1]; // Democratic
            }
            if (rndScore <= 90)
            {
                assignedLeaders = rnd.Next(3, SizeScale / 5 +3);
                return DataManager.SourceOfPower[2];
            } // Oligarchic
            assignedLeaders = 1;
            return DataManager.SourceOfPower[3];     // Autocratic
        }
        else if (IntensityScore == 2)
        {
            if (rndScore < 10)
                return DataManager.SourceOfPower[0]; // Anarchistic
            if (rndScore <= 40)
            {
                assignedLeaders = rnd.Next(5, SizeScale * 4 + 5);
                return DataManager.SourceOfPower[1]; // Democratic
            }// Democratic
            if (rndScore <= 80)
            {
                assignedLeaders = rnd.Next(3, SizeScale / 5 + 3);
                return DataManager.SourceOfPower[2];
            }// Oligarchic
            assignedLeaders = 1;
            return DataManager.SourceOfPower[3];     // Autocratic
        }
        else
        {
            if (rndScore < 5)
                return DataManager.SourceOfPower[0]; // Anarchistic
            if (rndScore <= 30)
            {
                assignedLeaders = rnd.Next(5, SizeScale * 4 + 5);
                return DataManager.SourceOfPower[1]; // Democratic
            } // Democratic
            if (rndScore <= 60)
            {
                assignedLeaders = rnd.Next(3, SizeScale / 5 +3);
                return DataManager.SourceOfPower[2];
            } // Oligarchic
            assignedLeaders = 1;
            return DataManager.SourceOfPower[3];     // Autocratic
        }
    }

    private string DetermineJoinRitual()
    {
        int rndScore = (int)rnd.NextInt64(400);
        int helper = Leadership switch
        {
            "Anarchistic" => -1,
            "Democratic" => 0,
            "Oligarchic" => 1,
            "Autocratic" => 2,
            _ => -300,
        };

        int dedicationValue = rndScore + IntensityScore * 100 + helper * 100;
        if (dedicationValue < 300)
            return DataManager.JoinRitualSimple[rnd.NextInt64(DataManager.JoinRitualSimple.Length)];
        if (dedicationValue < 600)
            return DataManager.JoinRitualMedium[rnd.NextInt64(DataManager.JoinRitualMedium.Length)];
        if (dedicationValue < 1101)
            return DataManager.JoinRitualHard[rnd.NextInt64(DataManager.JoinRitualHard.Length)];

        Console.WriteLine($"UNDEFINED CONTROL SEQUENCE JOIN RITUAL VALUE = {dedicationValue} = {rndScore} + {IntensityScore} * 100 + {helper} * 100");
        Environment.Exit(0);
        return null; // Just to satisfy the compiler; it will never reach here.
    }

    private string DeterminePowerParameters()
    {
        string powerParameters = "";
        int helper = Leadership switch
        {
            "Anarchistic" => -1,
            "Democratic" => 0,
            "Oligarchic" => 1,
            "Autocratic" => 2,
            _ => -300,
        };

        if (helper == 0)
        {
            PowerType = DataManager.VotingType[rnd.NextInt64(DataManager.VotingType.Length)];
            VotingSystem = DataManager.OliDemoVotingResults[rnd.NextInt64(DataManager.OliDemoVotingResults.Length)];
            powerParameters = "The faction is led by a democratic voting system in a " + PowerType + " manner. The democracy functions over" + VotingSystem + ".";
        }
        else if (helper == 1)
        {
            PowerType = DataManager.OliType[rnd.NextInt64(DataManager.OliType.Length)];
            VotingSystem = DataManager.OliDemoVotingResults[rnd.NextInt64(DataManager.OliDemoVotingResults.Length)];
            powerParameters = "The faction is led by an oligarchy, granting voting rights to the " + PowerType + ". The oligarvchy functions over" + VotingSystem + ".";
        }
        else if (helper == 2)
        {
            PowerType = DataManager.AutocracyType[rnd.NextInt64(DataManager.AutocracyType.Length)];
            powerParameters = "The faction is led by an autocracy, and leader is put in power by" + PowerType + ".";
        }
        else if (helper == -1)
        { powerParameters = "The faction is an Anarchistic communion."; }

        // Add further detail to the power structure, if needed

        return powerParameters;
    }

    public string GetParameters()
    {
        int valuesCount = Math.Clamp(SizeScale / 10 + IntensityScore, 1, 7);
        int moneyCount = Math.Clamp((SizeScale + FinanceScore) / 20, 1, 7);
        int doctrineCount = IntensityScore;

        Values = new string[valuesCount];
        MoneySources = new();
        Doctrines = new string[Math.Clamp(doctrineCount,1,5)];

        MoneySources.Add("Low", new List<string>());
        MoneySources.Add("Mid", new List<string>());
        MoneySources.Add("High", new List<string>());
        MoneySources.Add("Insane", new List<string>());

        string values = "Their values are: \n";
        string money = "They finance themselves by: \n";
        string doctrine = "They teach: \n";

        for (int i = 0; i < valuesCount; i++)
        {
            Values[i] = DataManager.Virtues[rnd.NextInt64(DataManager.Virtues.Length)];
            values += Values[i] + ", ";
        }
        int reducer = 0;
        List<string> list = new();
        for (int i = 0; i < moneyCount; i++)
        {
            int rndScore = rnd.Next(101);
            int finances = rndScore + 3 * FinanceScore - reducer * 7; //max is 400

            string randomJob;
            string jobCategory;
            if (finances < 70)
            {
                jobCategory = "Low";
                randomJob = GetWeightedRandomGeneralJob(DataManager.LowJobMappings);
            }
            else if (finances < 200)
            {
                jobCategory = "Mid";
                randomJob = GetWeightedRandomGeneralJob(DataManager.MidJobMappings);
            }
            else if (finances < 350)
            {
                jobCategory = "High";
                randomJob = GetWeightedRandomGeneralJob(DataManager.HighJobMappings);
                reducer += 1;
            }
            else
            {
                jobCategory = "Insane";
                randomJob = GetWeightedRandomGeneralJob(DataManager.SuperJobMappings);
                reducer += 5;
                i = i + 2;
            }

            // Add the job to the value array for the corresponding jobCategory key in MoneySources
            MoneySources[jobCategory].Add(randomJob);
            list.Add(randomJob);
        }


        for (int i = 0; i < doctrineCount; i++)
        {
            Doctrines[i] = DataManager.Doctrines[rnd.NextInt64(DataManager.Doctrines.Length)];
            doctrine += Doctrines[i] + ", ";
        }
        return values + "\n" + money + string.Join(", ", list) +  "\n" + doctrine;
    }

    public string GetStandings()
    {
        AdjustScores();
        int magicIndex = (int)Math.Round((double)MagicScore / 10);
        int intensityIndex = IntensityScore - 1;
        int scaleIndex = (int)Math.Round((double)SizeScale / 5);
        int militaryIndex = (int)Math.Round((double)MilitaristicScore / 10);
        int financeIndex = (int)Math.Round((double)FinanceScore / 10);
        int reputationIndex = (int)Math.Round((double)ReputationScore / 10);

        magicIndex += rnd.Next(-1, 2);
        intensityIndex += rnd.Next(-1, 2);
        scaleIndex += rnd.Next(-1, 2);
        militaryIndex += rnd.Next(-1, 2);
        financeIndex += rnd.Next(-1, 2);
        reputationIndex += rnd.Next(-1, 2);

        magicIndex = Math.Clamp(magicIndex, 0, DataManager.Wealth.Length - 1);
        intensityIndex = Math.Clamp(intensityIndex, 0, DataManager.Intensity.Length - 1);
        scaleIndex = Math.Clamp(scaleIndex, 0, DataManager.Size.Length - 1);
        militaryIndex = Math.Clamp(militaryIndex, 0, DataManager.MilitaryInclination.Length - 1);
        financeIndex = Math.Clamp(financeIndex, 0, DataManager.Wealth.Length - 1);
        reputationIndex = Math.Clamp(reputationIndex, 0, DataManager.Reputation.Length - 1);

        StandingsArray = new string[6];
        StandingsArray[2] = DataManager.Wealth[financeIndex];
        StandingsArray[0] = DataManager.MagicalInclination[magicIndex];
        StandingsArray[1] = DataManager.MilitaryInclination[militaryIndex];
        StandingsArray[4] = DataManager.Intensity[intensityIndex];
        StandingsArray[3] = DataManager.Size[scaleIndex];
        StandingsArray[5] = DataManager.Reputation[reputationIndex];

        return string.Join("\n", StandingsArray);
    }

    private void AdjustScores()
    {
        int highestScore = Math.Max(MagicScore, Math.Max(SizeScale, Math.Max(MilitaristicScore, FinanceScore)));

        if (highestScore >= 70)
        {
            if (MagicScore != highestScore && highestScore - 30 > MagicScore)
                MagicScore += 15;

            if (SizeScale != highestScore && highestScore - 30 > SizeScale)
                SizeScale += 15;

            if (MilitaristicScore != highestScore && highestScore - 20 > MilitaristicScore)
                MilitaristicScore += 15;

            if (FinanceScore != highestScore && highestScore - 20 > FinanceScore)
                FinanceScore += 15;

            MagicScore = Math.Min(MagicScore, 100);
            SizeScale = Math.Min(SizeScale, 100);
            MilitaristicScore = Math.Min(MilitaristicScore, 100);
            FinanceScore = Math.Min(FinanceScore, 100);
        }
    }
    public string GetWeightedRandomGeneralJob(Dictionary<string, List<string>> baseDir)
    {
        List<string> generalJobs = new List<string>(baseDir.Keys); //todo FIX ERROR

        // Calculate the total weight (total number of specific jobs across all general jobs)
        int totalWeight = 0;
        foreach (var job in generalJobs)
        {
            totalWeight += baseDir[job].Count;  // Weight is the number of specific jobs
        }

        // Generate a random number between 0 and totalWeight
        int randomWeight = rnd.Next(totalWeight);

        // Select the general job based on the random weight
        int cumulativeWeight = 0;
        foreach (var job in generalJobs)
        {
            cumulativeWeight += baseDir[job].Count;
            if (randomWeight < cumulativeWeight)
            {
                return job;  // Return the general job selected based on its weight
            }
        }

        return null;  // Default return value (should not reach here)
    }
}
