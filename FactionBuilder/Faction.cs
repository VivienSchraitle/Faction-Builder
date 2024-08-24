// Faction.cs
using System;

public class Faction
{
    public int MagicScore { get; private set; }
    public int IntensityScore { get; private set; }
    public int SizeScale { get; private set; }
    public int MilitaristicScore { get; private set; }
    public int FinanceScore { get; private set; }
    public int ReputationScore { get; private set; }

    public string[]? GoalsArray { get; private set; }
    public string[]? DomainsArray { get; private set; }
    // Leadership style as a single string
    public string? Leadership { get; private set; }

    // Join ritual as a single string
    public string? JoinRitual { get; private set; }

    // Power parameters as an array of strings
    public string[]? PowerParameters { get; private set; }

    // Values as an array of strings
    public string[]? Values { get; private set; }

    // Money sources as an array of strings
    public string[]? MoneySources { get; private set; }

    // Doctrines as an array of strings
    public string[]? Doctrines { get; private set; }

    // Standings as an array of strings
    public string[]? StandingsArray { get; private set; }

    private Random rnd = new();

    public Faction(int magic, int military, int money, int scale, int reputation)
    {
        MagicScore = magic;
        MilitaristicScore = military;
        FinanceScore = money;
        SizeScale = scale;
        ReputationScore = reputation;
        IntensityScore = rnd.Next(1, 6);
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

        return name;
    }

    public string GetDomains()
    {
        string domains = "Their fields of concern: ";
        int amount = (SizeScale / 10) + IntensityScore;
        int rndIndex = rnd.Next(50, 100);
        int distributer = rndIndex + MagicScore + MilitaristicScore;
        int magicAmount = (int)Math.Round((double)amount / distributer);
        int militaryAmount = (int)Math.Round((double)amount / distributer);

        DomainsArray = new string[amount];

        for (int i = 0; i < amount - magicAmount - militaryAmount; i++)
        {
            DomainsArray[i] = DataManager.FactionDomainsMundane[rnd.NextInt64(DataManager.FactionDomainsMundane.Length)];
            domains += DomainsArray[i] + ", ";
        }
        for (int i = amount - magicAmount - militaryAmount; i < amount - militaryAmount; i++)
        {
            DomainsArray[i] = DataManager.FactionDomainsMagical[rnd.NextInt64(DataManager.FactionDomainsMagical.Length)];
            domains += DomainsArray[i] + ", ";
        }
        for (int i = amount - militaryAmount; i < amount; i++)
        {
            DomainsArray[i] = DataManager.FactionDomainsMilitary[rnd.NextInt64(DataManager.FactionDomainsMilitary.Length)];
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
            goals += GoalsArray[i] + "\n";
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

        return styleDescription + Leadership + "\n" + JoinRitual + "\n" + string.Join(", ", PowerParameters);
    }

    private string DetermineLeadership()
    {
        int rndScore = (int)rnd.NextInt64(100);
        if (IntensityScore == 1)
        {
            if (rndScore < 25)
                return DataManager.SourceOfPower[0]; // Anarchistic
            if (rndScore <= 58)
                return DataManager.SourceOfPower[1]; // Democratic
            if (rndScore <= 90)
                return DataManager.SourceOfPower[2]; // Oligarchic
            return DataManager.SourceOfPower[3];     // Autocratic
        }
        else if (IntensityScore == 2)
        {
            if (rndScore < 10)
                return DataManager.SourceOfPower[0]; // Anarchistic
            if (rndScore <= 40)
                return DataManager.SourceOfPower[1]; // Democratic
            if (rndScore <= 80)
                return DataManager.SourceOfPower[2]; // Oligarchic
            return DataManager.SourceOfPower[3];     // Autocratic
        }
        else
        {
            if (rndScore < 5)
                return DataManager.SourceOfPower[0]; // Anarchistic
            if (rndScore <= 30)
                return DataManager.SourceOfPower[1]; // Democratic
            if (rndScore <= 60)
                return DataManager.SourceOfPower[2]; // Oligarchic
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
        if (dedicationValue < 900)
            return DataManager.JoinRitualHard[rnd.NextInt64(DataManager.JoinRitualHard.Length)];
        
        Console.WriteLine("UNDEFINED CONTROL SEQUENCE");
        Environment.Exit(0);
        return null; // Just to satisfy the compiler; it will never reach here.
    }

    private string[] DeterminePowerParameters()
    {
        string[] powerParameters = new string[3];
        int helper = Leadership switch
        {
            "Anarchistic" => -1,
            "Democratic" => 0,
            "Oligarchic" => 1,
            "Autocratic" => 2,
            _ => -300,
        };

        if (helper == 0)
            powerParameters[0] = "The faction is led by a democratic voting system in a " + DataManager.VotingType[rnd.NextInt64(DataManager.VotingType.Length)] + " manner.";
        else if (helper == 1)
            powerParameters[0] = "The faction is led by an oligarchy, granting voting rights to the " + DataManager.OliType[rnd.NextInt64(DataManager.OliType.Length)] + ".";
        else if (helper == 2)
            powerParameters[0] = "The faction is led by an autocracy, and leadership is determined by " + DataManager.AutocracyType[rnd.NextInt64(DataManager.AutocracyType.Length)] + ".";
        else if (helper == -1)
            powerParameters[0] = "The faction is an Anarchistic communion.";

        // Add further detail to the power structure, if needed
        powerParameters[1] = DataManager.OliDemoVotingResults[rnd.NextInt64(DataManager.OliDemoVotingResults.Length)];

        return powerParameters;
    }

    public string GetParameters()
    {
        int valuesCount = SizeScale / 10 + IntensityScore;
        int moneyCount = (SizeScale + FinanceScore) / 10;
        int doctrineCount = IntensityScore;

        Values = new string[valuesCount];
        MoneySources = new string[moneyCount];
        Doctrines = new string[doctrineCount];

        string values = "Their values are: \n";
        string money = "They finance themselves by: \n";
        string doctrine = "They teach: \n";

        for (int i = 0; i < valuesCount; i++)
        {
            Values[i] = DataManager.Virtues[rnd.NextInt64(DataManager.Virtues.Length)];
            values += Values[i] + ", ";
        }
        for (int i = 0; i < moneyCount; i++)
        {
            int rndScore = rnd.Next(101);
            int finances = rndScore + 3 * FinanceScore - i * 10;
            if (finances < 70)
                MoneySources[i] = DataManager.LowFinances[rnd.NextInt64(DataManager.LowFinances.Length)];
            else if (finances < 270)
                MoneySources[i] = DataManager.MidFinances[rnd.NextInt64(DataManager.MidFinances.Length)];
            else
                MoneySources[i] = DataManager.HighFinances[rnd.NextInt64(DataManager.HighFinances.Length)];
            money += MoneySources[i] + "\n";
        }
        for (int i = 0; i < doctrineCount; i++)
        {
            Doctrines[i] = DataManager.Doctrines[rnd.NextInt64(DataManager.Doctrines.Length)];
            doctrine += Doctrines[i] + ", ";
        }
        return values + "\n" + money + "\n" + doctrine;
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
        StandingsArray[0] = DataManager.Wealth[financeIndex];
        StandingsArray[1] = DataManager.MagicalInclination[magicIndex];
        StandingsArray[2] = DataManager.MilitaryInclination[militaryIndex];
        StandingsArray[3] = DataManager.Intensity[intensityIndex];
        StandingsArray[4] = DataManager.Size[scaleIndex];
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
}
