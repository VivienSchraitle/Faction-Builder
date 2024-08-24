using System;
using System.IO;
using System.Linq;

public class Main
{
    private static string name = "";
    private static string goals = "";
    private static string domains = "";
    private static string style = "";
    private static string parameters = "";
    private static string standing = "";
    private static string[] parts = Array.Empty<string>();
    private static Random rnd = new();
    private static int numParameters = 5;  // Can be modified as needed
    private static Faction myFaction = null!;

    public static void DoCode()
    {
        Console.Write($"Please enter the parameters ({numParameters} values, separated by spaces): ");
        string? input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            GenerateRandomValues();
        }
        else
        {
            if (!CheckString(input))
            {
                Environment.Exit(0);
            }
            parts = input.Split(' ');
            FillMissingParts(parts);
        }

        int[] inParameters = parts.Select(int.Parse).ToArray();
        myFaction = new Faction(inParameters[0], inParameters[1], inParameters[2], inParameters[3], inParameters[4]);

        name = myFaction.GetName();
        goals = myFaction.GetGoals();
        domains = myFaction.GetDomains();
        style = myFaction.GetStyle();
        parameters = myFaction.GetParameters();
        standing = myFaction.GetStandings();

        Console.WriteLine(name);
        Console.WriteLine("Standing");
        Console.WriteLine(standing);
        Console.WriteLine("Goals");
        Console.WriteLine(goals);
        Console.WriteLine("Domains");
        Console.WriteLine(domains);
        Console.WriteLine("Style");
        Console.WriteLine(style);
        Console.WriteLine("Parameters");
        Console.WriteLine(parameters);

        UserInputLoop();
    }

    private static void UserInputLoop()
    {
        while (true)
        {
            Console.WriteLine("Enter your option (1-9, save, or don't save):");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Invalid input. Please try again.");
                continue;
            }

            switch (input.ToLower())
            {
                case "1" or "name":
                    name = myFaction.GetName();
                    break;
                case "2" or "standing":
                    standing = myFaction.GetStandings();
                    break;
                case "3" or "goals":
                    goals = myFaction.GetGoals();
                    break;
                case "4" or "domains":
                    domains = myFaction.GetDomains();
                    break;
                case "5" or "style":
                    style = myFaction.GetStyle();
                    break;
                case "6" or "parameters":
                    parameters = myFaction.GetParameters();
                    break;
                case "9" or "everything":
                    parameters = myFaction.GetParameters();
                    break;
                case "10" or "save":
                    SaveData();
                    return;
                case "don't save":
                    return;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }

            Console.WriteLine(name);
            Console.WriteLine(standing);
            Console.WriteLine(goals);
            Console.WriteLine(domains);
            Console.WriteLine(style);
            Console.WriteLine(parameters);
        }
    }

    private static void SaveData()
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

    public static bool CheckString(string input)
    {
        string[] parts = input.Split(' ');

        if (parts.Length > numParameters)
        {
            Console.WriteLine("Failed to validate: Too many parts.");
            return false;
        }

        for (int i = 0; i < parts.Length; i++)
        {
            if (!int.TryParse(parts[i], out int value) || value < 0)
            {
                Console.WriteLine("Failed to validate: Invalid or negative value.");
                return false;
            }
        }

        bool isValid = parts.Take(4).All(v => int.Parse(v) >= 0 && int.Parse(v) <= 100)
                      && parts.Last() is { } lastValue && int.Parse(lastValue) >= 1 && int.Parse(lastValue) <= 5;

        if (!isValid)
        {
            Console.WriteLine("Failed to validate: Values out of acceptable range.");
        }

        return isValid;
    }

    private static void GenerateRandomValues()
    {
        parts = new string[numParameters];
        for (int i = 0; i < numParameters - 1; i++)
        {
            parts[i] = rnd.Next(0, 101).ToString();
        }
        parts[numParameters - 1] = rnd.Next(1, 6).ToString();
    }

    private static void FillMissingParts(string[] TEMPparts)
    {
        parts = new string[numParameters];
        for (int i = 0; i < numParameters; i++)
        {
            if (i < TEMPparts.Length && !string.IsNullOrWhiteSpace(TEMPparts[i]))
            {
                parts[i] = TEMPparts[i];
            }
            else
            {
                parts[i] = (i < numParameters - 1) ? rnd.Next(0, 101).ToString() : rnd.Next(1, 6).ToString();
            }
        }
    }
}
