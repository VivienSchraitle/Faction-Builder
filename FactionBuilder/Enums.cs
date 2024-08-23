using System;
using System.IO;
using System.Text.Json;

public class Enums(int magic, int military, int money, int scale,int reputation, int intensity)
{

    // Properties to expose private fields for serialization/deserialization
    public int MagicScore { get; private set; } = magic; //0-100
    public int IntensityScore { get; private set; } = intensity; //1-5
    public int SizeScale { get; private set; } = scale; //0-100
    public int MilitaristicScore { get; private set; } = military; //0-100
    public int FinanceScore { get; private set; } = money; //0-100
    public int ReputationScore { get; private set; } = reputation; //0-100
    public string MyLeadership { get; private set; } = "";

    private static string[] Attribute;
    private static string[] MainName;
    private static string[] FactionDomainsMilitary;
    private static string[] FactionDomainsMundane;
    private static string[] FactionDomainsMagical;
    private static string[] FactionGoals;
    private static string[] Virtues;
    private static string[] HighFinances;
    private static string[] MidFinances;
    private static string[] LowFinances;
    private static string[] Doctrines;

    private Random rnd = new();

    static Enums()
    {
        Console.WriteLine("got here");
        // Handle deserialization with try-catch and check file existence
        try
        {
            string path = System.IO.Directory.GetCurrentDirectory();
            path = System.IO.Directory.GetParent(path).ToString();
            Console.WriteLine(path);
            if (File.Exists(path + "/JSONs/FactionAttribute.JSON"))
                Attribute = JsonSerializer.Deserialize<string[]>(File.ReadAllText(path + "/JSONs/FactionAttribute.JSON"));
            if (File.Exists(path + "/JSONs/FactionName.JSON"))
                MainName = JsonSerializer.Deserialize<string[]>(File.ReadAllText(path + "/JSONs/FactionName.JSON"));
            if (File.Exists(path + "/JSONs/factionDomainsMilitary.JSON"))
                FactionDomainsMilitary = JsonSerializer.Deserialize<string[]>(File.ReadAllText(path + "/JSONs/factionDomainsMilitary.JSON"));
            if (File.Exists(path + "/JSONs/factionDomainsMundane.JSON"))
                FactionDomainsMundane = JsonSerializer.Deserialize<string[]>(File.ReadAllText(path + "/JSONs/factionDomainsMundane.JSON"));
            if (File.Exists(path + "/JSONs/factionDomainsMagical.JSON"))
                FactionDomainsMagical = JsonSerializer.Deserialize<string[]>(File.ReadAllText(path + "/JSONs/factionDomainsMagical.JSON"));
            if (File.Exists(path + "/JSONs/Virtues.JSON"))
                Virtues = JsonSerializer.Deserialize<string[]>(File.ReadAllText(path + "/JSONs/Virtues.JSON"));
            if (File.Exists(path + "/JSONs/Goals.JSON"))
                FactionGoals = JsonSerializer.Deserialize<string[]>(File.ReadAllText(path + "/JSONs/Goals.JSON"));
            if (File.Exists(path + "/JSONs/HighFinances.JSON"))
                HighFinances = JsonSerializer.Deserialize<string[]>(File.ReadAllText(path + "/JSONs/HighFinances.JSON"));
            if (File.Exists(path + "/JSONs/MidFinances.JSON"))
                MidFinances = JsonSerializer.Deserialize<string[]>(File.ReadAllText(path + "/JSONs/MidFinances.JSON"));
            if (File.Exists(path + "/JSONs/LowFinances.JSON"))
                LowFinances = JsonSerializer.Deserialize<string[]>(File.ReadAllText(path + "/JSONs/LowFinances.JSON"));
            if (File.Exists(path + "/JSONs/Doctrines.JSON"))
                Doctrines = JsonSerializer.Deserialize<string[]>(File.ReadAllText(path + "/JSONs/Doctrines.JSON"));
        }
        catch (JsonException ex)
        {
            Console.WriteLine("Error during JSON deserialization: " + ex.Message);
            Environment.Exit(0);
        }
        catch (IOException ex)
        {
            Console.WriteLine("File I/O error: " + ex.Message);
            Environment.Exit(0);
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

        name += Attribute[rnd.NextInt64(Attribute.Length)] + " ";
        name += MainName[rnd.NextInt64(MainName.Length)];

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
        for (int i = 0; i < amount - magicAmount - militaryAmount; i++)
        {
            domains += FactionDomainsMundane[rnd.NextInt64(FactionDomainsMundane.Length)] + ", ";
        }
        for (int i = 0; i < magicAmount; i++)
        {
            domains += FactionDomainsMagical[rnd.NextInt64(FactionDomainsMagical.Length)] + ", ";
        }
        for (int i = 0; i < militaryAmount; i++)
        {
            domains += FactionDomainsMilitary[rnd.NextInt64(FactionDomainsMilitary.Length)] + ", ";
        }
        domains += "and they are defined by their craving for: " + factionEssence[rnd.NextInt64(factionEssence.Length)];

        return domains;
    }

    public string GetGoals()
    {
        string goals = "Their main goals are: \n";
        int amount = (SizeScale / 10) + IntensityScore;
        for (int i = 0; i < amount; i++)
        {
            goals += FactionGoals[rnd.NextInt64(FactionGoals.Length)] + "\n";
        }
        return goals;
    }

    public string GetStyle()
    {
        string goalsStart = "Their organization style is: ";
        string goals = "";
        int rndScore = (int)rnd.NextInt64(100);
        if (IntensityScore == 1)
        {
            if (rndScore < 25)
            {
                goals += SourceOfPower[0];
            }
            else if (rndScore <= 58)
            {
                goals += SourceOfPower[1];
            }
            else if (rndScore <= 90)
            {
                goals += SourceOfPower[2];
            }
            else
            {
                goals += SourceOfPower[3];
            }
        }
        else if (IntensityScore == 2)
        {
            if (rndScore < 10)
            {
                goals += SourceOfPower[0];
            }
            else if (rndScore <= 40)
            {
                goals += SourceOfPower[1];
            }
            else if (rndScore <= 80)
            {
                goals += SourceOfPower[2];
            }
            else
            {
                goals += SourceOfPower[3];
            }
        }
        else
        {
            if (rndScore < 5)
            {
                goals += SourceOfPower[0];
            }
            else if (rndScore <= 30)
            {
                goals += SourceOfPower[1];
            }
            else if (rndScore <= 60)
            {
                goals += SourceOfPower[2];
            }
            else
            {
                goals += SourceOfPower[3];
            }
        }
        MyLeadership = goals;
        string initiation = "The method of initiation is: ";
        rndScore = (int)rnd.NextInt64(400);
        int helper;
        if (goals.Equals("Anarchistic")) { helper = -1; }
        else if (goals.Equals("Democratic")) { helper = 0; }
        else if (goals.Equals("Oligarchic")) { helper = 1; }
        else if (goals.Equals("Autocratic")) { helper = 2; }
        else { helper = -300; }

        int dedicationValue = rndScore + IntensityScore * 100 + helper * 100;
        if (dedicationValue < 300) { initiation += JoinRitualSimple[rnd.NextInt64(JoinRitualSimple.Length)]; }
        else if (dedicationValue < 600) { initiation += JoinRitualMedium[rnd.NextInt64(JoinRitualMedium.Length)]; }
        else if (dedicationValue < 900) { initiation += JoinRitualHard[rnd.NextInt64(JoinRitualHard.Length)]; }
        else { Console.WriteLine("UNDEFINED CONTROL SEQUENCE"); Environment.Exit(0); }

        string powerParameter = "";
        if (helper == 0) { powerParameter = "The faction is led by a democratic voting system in a " + VotingType[rnd.NextInt64(VotingType.Length)] + " manner, where " + OliDemoVotingResults[rnd.NextInt64(OliDemoVotingResults.Length)] + " are voted on."; }
        else if (helper == 1) { powerParameter = "The faction is led by an oligarchy, granting voting rights to the " + OliType[rnd.NextInt64(OliType.Length)] + ", where " + OliDemoVotingResults[rnd.NextInt64(OliDemoVotingResults.Length)] + " are voted on."; }
        else if (helper == 2) { powerParameter = "The faction is led by an autocracy, and leadership is determined by " + AutocracyType[rnd.NextInt64(AutocracyType.Length)] + "."; }
        else if (helper == -1) { powerParameter = "The faction is an Anarchistic communion."; }
        else { Console.WriteLine("UNDEFINED CONTROL SEQUENCE"); Environment.Exit(0); }

        return goalsStart + goals + "\n" + initiation + "\n" + powerParameter;
    }

    public string GetParameters()
    {
        string values = "Their values are: \n";
        string money = "They finance themselves by: \n";
        string doctrine = "They teach: \n";



        for (int i = 0; i < SizeScale / 10 + IntensityScore; i++)
        {
            values += Virtues[rnd.NextInt64(Virtues.Length)] + ", ";
        }
        int allocated = 0;
        for (int i = 0; i < (SizeScale + FinanceScore) / 10; i++)
        {
            int rndScore = rnd.Next(101);

            int finances = rndScore + 3 * FinanceScore - allocated;//0 - 400 
            if (finances < 70) { money += LowFinances[rnd.NextInt64(LowFinances.Length)]; }
            else if (finances < 270) { money += MidFinances[rnd.NextInt64(MidFinances.Length)]; }
            else { money += HighFinances[rnd.NextInt64(HighFinances.Length)]; allocated += 10; }
            money += "\n";
        }
        for (int i = 0; i < IntensityScore; i++)
        {
            doctrine += Doctrines[rnd.NextInt64(Doctrines.Length)] + ", ";
        }
        return values + "\n" + money + "\n" + doctrine;
    }
    public string GetStandings()
    {
        AdjustScores();
        // Determine index based on input values and randomness
        int magicIndex = (int)Math.Round((double)MagicScore / 10);
        int intensityIndex = IntensityScore - 1;
        int scaleIndex = (int)Math.Round((double)SizeScale / 5);
        int militaryIndex = (int)Math.Round((double)MilitaristicScore / 10);
        int financeIndex = (int)Math.Round((double)FinanceScore / 10);
        int reputationIndex =(int)Math.Round((double)ReputationScore / 10);

        // Add a bit of randomness to the indices
        magicIndex += rnd.Next(-1, 2);
        intensityIndex += rnd.Next(-1, 2);
        scaleIndex += rnd.Next(-1, 2);
        militaryIndex += rnd.Next(-1, 2);
        financeIndex += rnd.Next(-1, 2);
        reputationIndex += rnd.Next(-1,2);

        // Ensure indices are within bounds
        magicIndex = Math.Clamp(magicIndex, 0, Wealth.Length - 1);
        intensityIndex = Math.Clamp(intensityIndex, 0, Intensity.Length - 1);
        scaleIndex = Math.Clamp(scaleIndex, 0, Size.Length - 1);
        militaryIndex = Math.Clamp(militaryIndex, 0, MilitaryInclination.Length - 1);
        financeIndex = Math.Clamp(financeIndex, 0, Wealth.Length - 1);
        reputationIndex = Math.Clamp(reputationIndex, 0, Reputation.Length - 1);

        // Generate strings based on indices
        string wealthString = Wealth[financeIndex];
        string magicalInclinationString = MagicalInclination[magicIndex];
        string militaryInclinationString = MilitaryInclination[militaryIndex];
        string intensityString = Intensity[intensityIndex];
        string sizeString = Size[scaleIndex];
        string repString = Reputation[reputationIndex];

        // Use the generated strings as needed
        return (wealthString + "\n" + magicalInclinationString + "\n" + militaryInclinationString + "\n" + intensityString + "\n" + sizeString + "\n" + repString);
    }


    public string GetPeople()
    {
        return "";
    }



private void AdjustScores()
{
    // Determine the highest score
    int highestScore = Math.Max(MagicScore, Math.Max(SizeScale, Math.Max(MilitaristicScore, FinanceScore)));

    // Adjust other scores based on the highest score
    if (highestScore >= 70) // Adjust if the highest score is 90 or higher
    {
        // Only increase the scores if they are not already the highest
        if (MagicScore != highestScore && highestScore-30 >MagicScore)
            MagicScore += 15; // Example: Increase by 5 points
        
        if (SizeScale != highestScore && highestScore-30 >SizeScale)
            SizeScale += 15; // Example: Increase by 10 points
        
        if (MilitaristicScore != highestScore && highestScore-20 > MilitaristicScore)
            MilitaristicScore += 15; // Example: Increase by 5 points
        
        if (FinanceScore != highestScore && highestScore -20 >FinanceScore)
            FinanceScore += 15; // Example: Increase by 10 points

        // Ensure scores don't exceed their maximum values
        MagicScore = Math.Min(MagicScore, 100);
        SizeScale = Math.Min(SizeScale, 100);
        MilitaristicScore = Math.Min(MilitaristicScore, 100);
        FinanceScore = Math.Min(FinanceScore, 100);
    }
}

    #region Data
    private static readonly string[] factionEssence =
    {
        "Power",
        "Prestige",
        "Faith",
        "Knowledge",
        "Order",
        "Freedom",
        "Unity",
        "Purpose",
        "Savety",
        "Reighteousness",
        "Fame",
        "Beneficence",
        "Vengence"
    };

    private static readonly string[] SourceOfPower =
    {
        "Anarchistic",
        "Democratic",
        "Oligarchic",
        "Autocratic"
    };

    private static readonly string[] VotingType =
    {
        "Direct",
        "Representative",
        "Demarchic"
    };

    private static readonly string[] OliType =
    {
        "Nobility",
        "Wealth",
        "Power",
        "Intelligence",
        "Lineage",
        "Age"
    };

    private static readonly string[] OliDemoVotingResults =
    {
        "council",
        "direct laws and commandments",
        "council with direct laws and commandments",
        "direct vote",
        "council",
        "direct laws and commandments",
        "council with direct laws and commandments",
        "direct vote",
        "elected leader with direct laws and commandments",
        "elected leader with council and direct laws and commandments",
        "parliamentary non-elected leader with direct laws and commandments",
        "parliamentary non-elected leader with council and direct laws and commandments"
    };

    private static readonly string[] AutocracyType =
    {
        "a higher power",
        "through lineage",
        "through economic control"
    };

    private static readonly string[] JoinRitualSimple =
    {
        "Demonstrate alignment",
        "Perform a service",
        "Pay a fee",
        "Be recruited",
        "Share common interests",
        "Contribute resources",
        "Participate in activities",
        "Express support publicly",
        "Be invited by a member",
        "Show potential for value",
        "Complete a minor task",
        "Donate to the cause",
        "Attend public events",
        "Be in the right place at the right time",
        "Share similar beliefs",
        "Possess a desired skill",
        "Offer unique knowledge",
        "Display loyalty implicitly",
        "Be a charismatic individual"
    };

    private static readonly string[] JoinRitualMedium =
    {
        "Undergo a trial",
        "Prove loyalty",
        "Receive an invitation",
        "Complete a rite of passage",
        "Acquire necessary skills",
        "Pass a rigorous evaluation",
        "Demonstrate exceptional ability",
        "Swear an oath of allegiance",
        "Undergo physical conditioning",
        "Master specific knowledge",
        "Complete a dangerous mission",
        "Prove worth in combat",
        "Sacrifice personal possessions",
        "Undergo a spiritual transformation",
        "Demonstrate unwavering dedication",
        "Pass a psychological evaluation",
        "Be recommended by current members",
        "Complete a period of servitude",
        "Prove oneself in a leadership role",
        "Undergo a magical transformation"
    };

    private static readonly string[] JoinRitualHard =
    {
        "Be born into it",
        "Sacrifice personal freedom",
        "Undergo a transformation",
        "Prove worth through combat",
        "Renounce former affiliations",
        "Embrace a new identity",
        "Surrender personal possessions",
        "Accept a new name or title",
        "Undergo a painful ritual",
        "Live in seclusion or isolation",
        "Break ties with family and friends",
        "Forsake personal desires",
        "Embrace a strict code of conduct",
        "Undergo a complete lifestyle change",
        "Become a living symbol of the faction",
        "Sacrifice one's life for the faction",
        "Become a martyr for the cause",
        "Undergo a permanent physical alteration",
        "Lose all sense of individuality",
        "Completely immerse oneself in the faction's culture"
    };
private static readonly string[] Reputation =
{
    "Renowned and revered, considered a paragon of their field",
    "Exemplary and highly respected, held in the highest regard",
    "Well-respected and admired, with a strong positive reputation",
    "Generally respected and well-regarded, with a favorable reputation",
    "Respected and well-liked, with a positive reputation",
    "Mostly well-regarded, with a positive reputation but some minor criticisms",
    "Mostly well-regarded, with a positive reputation but some minor criticisms",
    "Neutral reputation, neither particularly positive nor negative",
    "Neutral reputation, neither particularly positive nor negative",
    "Mostly negatively perceived, with some positive aspects",
    "Mostly negatively perceived, with some positive aspects",
    "Negatively perceived, with a reputation for negative behavior",
    "Negatively perceived, with a reputation for negative behavior",
    "Strongly negatively perceived, with a very poor reputation",
    "Despised and reviled, with a reputation for harmful actions",
    "Infamous and notorious, known for their scandalous or criminal actions",
    "Actively hunted and pursued, considered a dangerous threat",
    "Unknown or secret, with no public reputation",
    "Unknown or secret, with no public reputation",
    "Forgotten and obscure, with a reputation that has faded into obscurity",
};

    private static readonly string[] Size =
    {
    "A group amde up out of very few individuals",
    "A small group of individuals",
    "A small group of individuals",
    "A handful of people, the followers could populate a few houses",
    "A handful of people, the followers could populate a few houses",
    "A reltivly small group, the followers could populate a fraction of a small village",
    "A small group, the followers could populate a small village",
    "A small group, the followers could populate a small village",
    "A modest operation with a few locations, the followers could populate a small town",
    "A modest operation with a few locations, the followers could populate a small town",
    "A modest operation with a few locations, the followers could populate a large town",
    "A medium-sized operation with several locations, the followers could populate a small-sized city",
    "A medium-sized operation with several locations, the followers could populate a small-sized city",
    "A medium-sized operation with several locations, the followers could populate a mid-sized city",
    "A medium-sized operation with several locations, the followers could populate a mid-sized city",
    "A large operation with numerous locations, the followers could populate a large city",
    "A large operation with numerous locations, the followers could populate a large city",
    "A large operation with numerous locations, the followers could populate a major city",
    "A very large operation with widespread influence, the followers could populate multiple cities",
    "A very large operation with widespread influence, the followers could populate multiple cities",
    "A multinational operation, the followers could populate a nation",
    "A global operation with a vast network, the followers could populate multiple nations",
    };
    private static readonly string[] Wealth =
       {
    "Negligible resources, completely reliant on external support",
    "Minimal resources, struggling to meet basic needs",
    "Small financial base, relying on local funding and fees",
    "Limited financial resources, with difficulty expanding or growing",
    "Moderate financial resources, with a mix of local, regional, and possibly national funding",
    "Moderate financial resources, with a mix of local, regional, and possibly national funding",
    "Sufficient financial resources, able to sustain operations and growth",
    "Significant financial resources, with a diverse portfolio of investments and funding sources",
    "Substantial financial resources, capable of major initiatives and expansion",
    "Vast financial resources, with global influence and significant wealth accumulation",
    "Immense financial resources, able to shape industries and economies",
    "Essentially unlimited resources, able to shape the world in their image"
    };
    private static readonly string[] MagicalInclination =
    {
    "Negligible magical ability, completely reliant on mundane skills",
    "Minimal magical ability, capable of basic, unreliable magic",
    "Limited magical ability, capable of basic spells or rituals with limitations",
    "Moderate magical ability, proficient in a range of spells and rituals",
    "Sufficient magical ability, able to cast complex spells and rituals",
    "Sufficient magical ability, able to cast complex spells and rituals",
    "Significant magical ability, capable of powerful spells and magical feats",
    "Significant magical ability, capable of powerful spells and magical feats",
    "Substantial magical ability, considered a master of a specific magical discipline",
    "Vast magical ability, capable of extraordinary feats and magical mastery",
    "Immense magical ability, considered a legendary figure in magical history",
    "Unparalleled magical ability, capable of feats beyond human comprehension"
    };
    private static readonly string[] MilitaryInclination =
    {
    "Non-violent or pacifist, completely avoiding conflict",
    "Minimal military capability, relying on defensive tactics and deterrence",
    "Limited military capability, capable of basic defense and limited offensive actions",
    "Moderate military capability, able to defend territory and engage in minor conflicts",
    "Moderate military capability, able to defend territory and engage in minor conflicts",
    "Sufficient military capability, able to sustain operations and protect interests",
    "Significant military capability, capable of major military campaigns and regional dominance",
    "Significant military capability, capable of major military campaigns and regional dominance",
    "Substantial military capability, capable of global influence and military projection",
    "Vast military capability, considered a military superpower with unmatched resources",
    "Immense military capability, able to dominate entire regions and shape global affairs",
    "Unparalleled military capability, capable of feats of military might beyond imagination"
};
    private static readonly string[] Intensity =
{
    "Minimal or no impact on personal life, primarily a hobby or interest",
    "Minor impact on personal life, requiring occasional sacrifices or adjustments",
    "Moderate impact on personal life, requiring significant sacrifices or changes",
    "Major impact on personal life, defining a significant portion of one's identity",
    "Exceptional impact on personal life, involving extraordinary sacrifices and a reimagining of oneself"
};

    #endregion
}
