using System;
using System.IO;
using System.Linq;

public class FactionBuilder
{
    private string name = "";
    private string goals = "";
    private string domains = "";
    private string style = "";
    private string parameters = "";
    private string standing = "";
    private string[] parts = Array.Empty<string>();
    private Random rnd = new();
    private int numParameters = 6;
    private Faction myFaction = null!;

    public void GenerateFaction(string[] inputParts)
    {
        if (inputParts == null || inputParts.Length != numParameters)
        {
            GenerateRandomValues();
        }
        for (int i = 0; i < inputParts.Length; i++)
        {
            if (string.IsNullOrEmpty(inputParts[i]))
            {
                inputParts[i] = GetRandomValueForParameter(i);
            }
        }

        if (ValidateParameters(inputParts))
        {
            parts = inputParts;
        }
        else
        {
            throw new ArgumentException("Invalid parameters provided.");
        }


        int[] inParameters = parts.Select(int.Parse).ToArray();
        myFaction = new Faction(inParameters[0], inParameters[1], inParameters[2], inParameters[3], inParameters[4], inParameters[5]);

        UpdateFactionData();
    }

    public string GetFactionDetails()
    {
        return $"Name: {name}\nStanding: {standing}\nGoals: {goals}\nDomains: {domains}\nStyle: {style}\nParameters: {parameters}";
    }

    public void SaveFaction()
    {
        string fullPath = Path.GetFullPath(@"Generated Factions");
        string dir = Directory.CreateDirectory(fullPath + "/" + name).ToString();
        using StreamWriter outputFile = new StreamWriter(Path.Combine(dir, name + ".md"));
        outputFile.WriteLine("\n" + standing + "\n");
        outputFile.WriteLine(goals + "\n");
        outputFile.WriteLine(domains + "\n");
        outputFile.WriteLine(style + "\n");
        outputFile.WriteLine(parameters + "\n");
    }

    private bool ValidateParameters(string[] inputParts)
    {
        for (int i = 0; i < inputParts.Length; i++)
        {
            if (!int.TryParse(inputParts[i], out int value))
            {
                return false;
            }

            if (value < 0 || value > 100)
            {
                return false;
            }
        }

        return true;
    }

    private void GenerateRandomValues()
    {
        parts = new string[numParameters];
        for (int i = 0; i < numParameters - 1; i++)
        {
            parts[i] = rnd.Next(0, 101).ToString();
        }
        parts[numParameters - 1] = rnd.Next(1, 6).ToString();
    }
private string GetRandomValueForParameter(int index)
{
    switch (index)
    {
        case 0 or 1 or 2 or 3 or 4: return rnd.Next(0, 101).ToString();
        case 5: return rnd.Next(0, 6).ToString();
        default: throw new ArgumentOutOfRangeException("Invalid parameter index.");
    }
}
    private void UpdateFactionData()
    {
        name = myFaction.GetName();
        goals = myFaction.GetGoals();
        domains = myFaction.GetDomains();
        style = myFaction.GetStyle();
        parameters = myFaction.GetParameters();
        standing = myFaction.GetStandings();
    }
}
