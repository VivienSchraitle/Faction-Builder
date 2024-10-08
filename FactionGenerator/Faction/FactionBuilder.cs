using System;
using System.IO;
using System.Linq;

public class FactionBuilder
{
    private string primeHeritage = "";
    private string secondHeritage = "";
    private string name = "";
    private string goals = "";
    private string domains = "";
    private string style = "";
    private string parameters = "";
    private string standing = "";
    private string[] parts = Array.Empty<string>();
    private Random rnd = new();
    private int numParameters = 9;
    private Faction myFaction = null!;

    public void GenerateFaction(string[] inputParts)
    {
        primeHeritage = inputParts[inputParts.Length - 1];
        inputParts = inputParts.Take(inputParts.Count() - 1).ToArray();
        if(primeHeritage == "Mixed" || primeHeritage == "Random")
        {
            primeHeritage = "Human";
        }

        secondHeritage = inputParts[inputParts.Length - 1];
        inputParts = inputParts.Take(inputParts.Count() - 1).ToArray();

        if(secondHeritage == "Mixed" || secondHeritage == "Random")
        {
            secondHeritage = "Human";
        }
        if (inputParts == null || inputParts.Length != numParameters)
        {
            GenerateRandomValues();
        }
#pragma warning disable // Dereference of a possibly null reference.
        for (int i = 0; i < inputParts.Length; i++)
        {
            if (string.IsNullOrEmpty(inputParts[i]))
            {
                inputParts[i] = GetRandomValueForParameter(i);
            }
        }
#pragma warning restore // Dereference of a possibly null reference.

        if (ValidateParameters(inputParts))
        {
            parts = inputParts;
        }
        else
        {
            throw new ArgumentException("Invalid parameters provided.");
        }


        int[] inParameters = parts.Select(int.Parse).ToArray();
        myFaction = new Faction(inParameters[0], inParameters[1], inParameters[2], inParameters[3], inParameters[4], inParameters[5], inParameters[6], inParameters[7], inParameters[8], primeHeritage, secondHeritage);

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
        People populator = new();
        populator.PopulateFaction(myFaction);
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
    parts[numParameters - 1] = rnd.Next(1, 7).ToString(); // Generate between 1 and 6
}
    private string GetRandomValueForParameter(int index)
    {
        if (index < numParameters - 1)
        {
            return rnd.Next(1, 101).ToString();
        }
        else if (index == numParameters - 1)
        {
            return rnd.Next(0, 6).ToString();
        }
        else
        {
            throw new ArgumentOutOfRangeException("Invalid parameter index.");
        }
    }
    private void UpdateFactionData()
{
    name = myFaction.GetName() ?? "Unknown Name";
    goals = myFaction.GetGoals() ?? "Unknown Goals";
    domains = myFaction.GetDomains() ?? "Unknown Domains";
    style = myFaction.GetStyle() ?? "Unknown Style";
    parameters = myFaction.GetParameters() ?? "Unknown Parameters";
    standing = myFaction.GetStandings() ?? "Unknown Standing";
}
}
