// DataManager.cs
using System;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;

public static class DataManager
{
    public static string[] Attribute;
    public static string[] MainName;
    public static string[] FactionDomainsMilitary;
    public static string[] FactionDomainsMundane;
    public static string[] FactionDomainsMagical;
    public static string[] FactionDomainsReligious;
    public static string[] FactionGoals;
    public static string[] Virtues;
    public static Dictionary<string, List<string>> SuperJobMappings;
    public static Dictionary<string, List<string>> HighJobMappings;
    public static Dictionary<string, List<string>> MidJobMappings;
    public static Dictionary<string, List<string>> LowJobMappings;
    public static string[] Doctrines;
    public static string[] FactionEssence;
    public static string[] SourceOfPower;
    public static string[] VotingType;
    public static string[] OliType;
    public static string[] OliDemoVotingResults;
    public static string[] AutocracyType;
    public static string[] JoinRitualSimple;
    public static string[] JoinRitualMedium;
    public static string[] JoinRitualHard;
    public static string[] Reputation;
    public static string[] Size;
    public static string[] Wealth;
    public static string[] MagicalInclination;
    public static string[] MilitaryInclination;
    public static string[] Intensity;

    public static string[] DefaultSkinColor;
    public static string[] DefaultUndertones;
    public static string[] DefaultHairColor;
    public static string[] DefaultHairstyles;
    public static string[] DefaultEyeColor;
    public static string[] AdjectiveSizes;
    public static string[] Classes;
    public static string[] Success;
    public static Random random;

    public static List<Ancestry> Ancestries = new();
    public static List<Heritage> Heritages = new();

    public struct Ancestry 
    {
        public string Name;
        public int LH;
        public float SizeAvg;
        public float SizeDev;
        public string[] SkinColor;
        public string[] Undertones;
        public string[] HairColor;
        public string[] Hairstyles;
        public string[] EyeColor;
        public string[][] OptionalSpecialTraits;
        public string[][] CertainSpecialTraits;
        public string[] Heritage;
        public int MaxAge;
        public int MatureAge;
    }

    public struct Heritage
    {
        public string Name;
        public int LH;
        public string[] SkinColor;
        public string[] Undertones;
        public string[] HairColor;
        public string[] Hairstyles;
        public string[] EyeColor;
        public string[][] OptionalSpecialTraits;
        public string[][] CertainSpecialTraits;
    }
#pragma warning disable
    static DataManager()
    {
        random = new();
        int seed = random.Next();
        LoadFactionData();
        LoadPeopleData();
        random = new Random(seed.GetHashCode());
    }

    public static void LoadFactionData()
    {
        try
        {
            string path = Directory.GetCurrentDirectory();
            path = Directory.GetParent(path).ToString();

            if (File.Exists(Path.Combine(path, "JSONs", "Faction", "FactionAttribute.JSON")))
                Attribute = JsonSerializer.Deserialize<string[]>(File.ReadAllText(Path.Combine(path, "JSONs", "Faction", "FactionAttribute.JSON")));
            if (File.Exists(Path.Combine(path, "JSONs", "Faction", "FactionName.JSON")))
                MainName = JsonSerializer.Deserialize<string[]>(File.ReadAllText(Path.Combine(path, "JSONs", "Faction", "FactionName.JSON")));
            if (File.Exists(Path.Combine(path, "JSONs", "Faction", "factionDomainsMilitary.JSON")))
                FactionDomainsMilitary = JsonSerializer.Deserialize<string[]>(File.ReadAllText(Path.Combine(path, "JSONs", "Faction", "factionDomainsMilitary.JSON")));
            if (File.Exists(Path.Combine(path, "JSONs", "Faction", "factionDomainsMundane.JSON")))
                FactionDomainsMundane = JsonSerializer.Deserialize<string[]>(File.ReadAllText(Path.Combine(path, "JSONs", "Faction", "factionDomainsMundane.JSON")));
            if (File.Exists(Path.Combine(path, "JSONs", "Faction", "factionDomainsReligious.JSON")))
                FactionDomainsReligious = JsonSerializer.Deserialize<string[]>(File.ReadAllText(Path.Combine(path, "JSONs", "Faction", "factionDomainsReligious.JSON")));
            if (File.Exists(Path.Combine(path, "JSONs", "Faction", "factionDomainsMagical.JSON")))
                FactionDomainsMagical = JsonSerializer.Deserialize<string[]>(File.ReadAllText(Path.Combine(path, "JSONs", "Faction", "factionDomainsMagical.JSON")));
            if (File.Exists(Path.Combine(path, "JSONs", "Faction", "Virtues.JSON")))
                Virtues = JsonSerializer.Deserialize<string[]>(File.ReadAllText(Path.Combine(path, "JSONs", "Faction", "Virtues.JSON")));
            if (File.Exists(Path.Combine(path, "JSONs", "Faction", "Goals.JSON")))
                FactionGoals = JsonSerializer.Deserialize<string[]>(File.ReadAllText(Path.Combine(path, "JSONs", "Faction", "Goals.JSON")));
            if (File.Exists(Path.Combine(path, "JSONs", "Faction", "InsaneFinances.JSON")))
                SuperJobMappings = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(File.ReadAllText(Path.Combine(path, "JSONs", "Faction", "InsaneFinances.JSON")));
            if (File.Exists(Path.Combine(path, "JSONs", "Faction", "HighFinances.JSON")))
                HighJobMappings = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(File.ReadAllText(Path.Combine(path, "JSONs", "Faction", "HighFinances.JSON")));
            if (File.Exists(Path.Combine(path, "JSONs", "Faction", "MidFinances.JSON")))
                MidJobMappings = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(File.ReadAllText(Path.Combine(path, "JSONs", "Faction", "MidFinances.JSON")));
            if (File.Exists(Path.Combine(path, "JSONs", "Faction", "LowFinances.JSON")))
                LowJobMappings = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(File.ReadAllText(Path.Combine(path, "JSONs", "Faction", "LowFinances.JSON")));
            if (File.Exists(Path.Combine(path, "JSONs", "Faction", "Doctrines.JSON")))
                Doctrines = JsonSerializer.Deserialize<string[]>(File.ReadAllText(Path.Combine(path, "JSONs", "Faction", "Doctrines.JSON")));
            if (File.Exists(Path.Combine(path, "JSONs", "Faction", "FactionEssence.JSON")))
                FactionEssence = JsonSerializer.Deserialize<string[]>(File.ReadAllText(Path.Combine(path, "JSONs", "Faction", "FactionEssence.JSON")));
            if (File.Exists(Path.Combine(path, "JSONs", "Faction", "SourceOfPower.JSON")))
                SourceOfPower = JsonSerializer.Deserialize<string[]>(File.ReadAllText(Path.Combine(path, "JSONs", "Faction", "SourceOfPower.JSON")));
            if (File.Exists(Path.Combine(path, "JSONs", "Faction", "VotingType.JSON")))
                VotingType = JsonSerializer.Deserialize<string[]>(File.ReadAllText(Path.Combine(path, "JSONs", "Faction", "VotingType.JSON")));
            if (File.Exists(Path.Combine(path, "JSONs", "Faction", "OliType.JSON")))
                OliType = JsonSerializer.Deserialize<string[]>(File.ReadAllText(Path.Combine(path, "JSONs", "Faction", "OliType.JSON")));
            if (File.Exists(Path.Combine(path, "JSONs", "Faction", "OliDemoVotingResults.JSON")))
                OliDemoVotingResults = JsonSerializer.Deserialize<string[]>(File.ReadAllText(Path.Combine(path, "JSONs", "Faction", "OliDemoVotingResults.JSON")));
            if (File.Exists(Path.Combine(path, "JSONs", "Faction", "AutocracyType.JSON")))
                AutocracyType = JsonSerializer.Deserialize<string[]>(File.ReadAllText(Path.Combine(path, "JSONs", "Faction", "AutocracyType.JSON")));
            if (File.Exists(Path.Combine(path, "JSONs", "Faction", "JoinRitualSimple.JSON")))
                JoinRitualSimple = JsonSerializer.Deserialize<string[]>(File.ReadAllText(Path.Combine(path, "JSONs", "Faction", "JoinRitualSimple.JSON")));
            if (File.Exists(Path.Combine(path, "JSONs", "Faction", "JoinRitualMedium.JSON")))
                JoinRitualMedium = JsonSerializer.Deserialize<string[]>(File.ReadAllText(Path.Combine(path, "JSONs", "Faction", "JoinRitualMedium.JSON")));
            if (File.Exists(Path.Combine(path, "JSONs", "Faction", "JoinRitualHard.JSON")))
                JoinRitualHard = JsonSerializer.Deserialize<string[]>(File.ReadAllText(Path.Combine(path, "JSONs", "Faction", "JoinRitualHard.JSON")));
            if (File.Exists(Path.Combine(path, "JSONs", "Faction", "Reputation.JSON")))
                Reputation = JsonSerializer.Deserialize<string[]>(File.ReadAllText(Path.Combine(path, "JSONs", "Faction", "Reputation.JSON")));
            if (File.Exists(Path.Combine(path, "JSONs", "Faction", "Size.JSON")))
                Size = JsonSerializer.Deserialize<string[]>(File.ReadAllText(Path.Combine(path, "JSONs", "Faction", "Size.JSON")));
            if (File.Exists(Path.Combine(path, "JSONs", "Faction", "Wealth.JSON")))
                Wealth = JsonSerializer.Deserialize<string[]>(File.ReadAllText(Path.Combine(path, "JSONs", "Faction", "Wealth.JSON")));
            if (File.Exists(Path.Combine(path, "JSONs", "Faction", "MagicalInclination.JSON")))
                MagicalInclination = JsonSerializer.Deserialize<string[]>(File.ReadAllText(Path.Combine(path, "JSONs", "Faction", "MagicalInclination.JSON")));
            if (File.Exists(Path.Combine(path, "JSONs", "Faction", "MilitaryInclination.JSON")))
                MilitaryInclination = JsonSerializer.Deserialize<string[]>(File.ReadAllText(Path.Combine(path, "JSONs", "Faction", "MilitaryInclination.JSON")));
            if (File.Exists(Path.Combine(path, "JSONs", "Faction", "Intensity.JSON")))
                Intensity = JsonSerializer.Deserialize<string[]>(File.ReadAllText(Path.Combine(path, "JSONs", "Faction", "Intensity.JSON")));
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

    public static void LoadPeopleData()
    {
        try
        {
        string path = Directory.GetCurrentDirectory();
        path = Directory.GetParent(path).ToString();
        if (File.Exists(Path.Combine(path, "JSONs", "People", "Ancestires.JSON")))
            LoadAncestries(Path.Combine(path, "JSONs", "People", "Ancestires.JSON"));
        if (File.Exists(Path.Combine(path, "JSONs", "People", "Heriatage.JSON")))
            LoadHeritages(Path.Combine(path, "JSONs", "People", "Heriatage.JSON"));
        if (File.Exists(Path.Combine(path, "JSONs", "People", "Classes.JSON")))
            Classes = JsonSerializer.Deserialize<string[]>(File.ReadAllText(Path.Combine(path, "JSONs", "People", "Classes.JSON")));
        if (File.Exists(Path.Combine(path, "JSONs", "People", "Success.JSON")))
            Success = JsonSerializer.Deserialize<string[]>(File.ReadAllText(Path.Combine(path, "JSONs", "People", "Success.JSON")));

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
        ConvertAncestriesToHeritages();
    }
    private static void LoadAncestries(string filePath)
    {
        if (File.Exists(filePath))
        {
            try
            {
                string jsonData = File.ReadAllText(filePath);

                var jsonObjects = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(jsonData);
                if (jsonObjects == null || jsonObjects.Count == 0)
                {
                    Console.WriteLine("Ancestry data is empty or invalid.");
                    return;
                }

                var defaultValues = jsonObjects[0];

                DefaultSkinColor = DeserializeJsonArray(defaultValues["DefaultSkinColor"]);
                DefaultUndertones = DeserializeJsonArray(defaultValues["DefaultUndertones"]);
                DefaultHairColor = DeserializeJsonArray(defaultValues["DefaultHairColor"]);
                DefaultHairstyles = DeserializeJsonArray(defaultValues["DefaultHairstyles"]);
                DefaultEyeColor = DeserializeJsonArray(defaultValues["DefaultEyeColor"]);
                AdjectiveSizes = DeserializeJsonArray(defaultValues["AdjectiveSizes"]);

                for (int i = 1; i < jsonObjects.Count; i++)
                {
                    var ancestryJson = jsonObjects[i];
                    var ancestry = new Ancestry
                    {
                        Name = ancestryJson.GetValueOrDefault("Name", "Unknown"),
                        LH = ancestryJson.GetValueOrDefault("LH", 0),
                        SizeAvg = ancestryJson.GetValueOrDefault("SizeAvg", 0.0f),
                        SizeDev = ancestryJson.GetValueOrDefault("SizeDev", 0.0f),
                        SkinColor = ResolveDefaults(DeserializeJsonArray(ancestryJson.GetValueOrDefault("SkinColor", new JsonElement())), DefaultSkinColor),
                        Undertones = ResolveDefaults(DeserializeJsonArray(ancestryJson.GetValueOrDefault("Undertones", new JsonElement())), DefaultUndertones),
                        HairColor = ResolveDefaults(DeserializeJsonArray(ancestryJson.GetValueOrDefault("HairColor", new JsonElement())), DefaultHairColor),
                        Hairstyles = ResolveDefaults(DeserializeJsonArray(ancestryJson.GetValueOrDefault("Hairstyles", new JsonElement())), DefaultHairstyles),
                        EyeColor = ResolveDefaults(DeserializeJsonArray(ancestryJson.GetValueOrDefault("EyeColor", new JsonElement())), DefaultEyeColor),
                        OptionalSpecialTraits = DeserializeSpecialTraits(ancestryJson.GetValueOrDefault("OptionalSpecialTraits", new JsonElement())),
                        CertainSpecialTraits = DeserializeSpecialTraits(ancestryJson.GetValueOrDefault("CertainSpecialTraits", new JsonElement())),
                        Heritage = DeserializeJsonArray(ancestryJson.GetValueOrDefault("Heritage", new JsonElement())),
                        MaxAge = ancestryJson.GetValueOrDefault("MaxAge", 0),
                        MatureAge = ancestryJson.GetValueOrDefault("MatureAge", 0)
                    };

                    ExpandSpecialTraits(ancestry.OptionalSpecialTraits);
                    ExpandSpecialTraits(ancestry.CertainSpecialTraits);

                    Ancestries.Add(ancestry);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading or deserializing the JSON file: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine($"File {filePath} not found.");
        }
    }

    private static void LoadHeritages(string filePath)
    {
        if (File.Exists(filePath))
        {
            try
            {
                string jsonData = File.ReadAllText(filePath);

                var jsonObjects = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(jsonData);
                if (jsonObjects == null || jsonObjects.Count == 0)
                {
                    Console.WriteLine("Heritage data is empty or invalid.");
                    return;
                }

                var defaultValues = jsonObjects[0];

                DefaultSkinColor = DeserializeJsonArray(defaultValues["DefaultSkinColor"]);
                DefaultUndertones = DeserializeJsonArray(defaultValues["DefaultUndertones"]);
                DefaultHairColor = DeserializeJsonArray(defaultValues["DefaultHairColor"]);
                DefaultHairstyles = DeserializeJsonArray(defaultValues["DefaultHairstyles"]);
                DefaultEyeColor = DeserializeJsonArray(defaultValues["DefaultEyeColor"]);
                AdjectiveSizes = DeserializeJsonArray(defaultValues["AdjectiveSizes"]);

                for (int i = 1; i < jsonObjects.Count; i++)
                {
                    var heritageJson = jsonObjects[i];
                    var heritage = new Heritage
                    {
                        Name = heritageJson.GetValueOrDefault("Name", "Unknown"),
                        LH = heritageJson.GetValueOrDefault("LH", 0),
                        SkinColor = ResolveDefaults(DeserializeJsonArray(heritageJson.GetValueOrDefault("SkinColor", new JsonElement())), DefaultSkinColor),
                        Undertones = ResolveDefaults(DeserializeJsonArray(heritageJson.GetValueOrDefault("Undertones", new JsonElement())), DefaultUndertones),
                        HairColor = ResolveDefaults(DeserializeJsonArray(heritageJson.GetValueOrDefault("HairColor", new JsonElement())), DefaultHairColor),
                        Hairstyles = ResolveDefaults(DeserializeJsonArray(heritageJson.GetValueOrDefault("Hairstyles", new JsonElement())), DefaultHairstyles),
                        EyeColor = ResolveDefaults(DeserializeJsonArray(heritageJson.GetValueOrDefault("EyeColor", new JsonElement())), DefaultEyeColor),
                        OptionalSpecialTraits = DeserializeSpecialTraits(heritageJson.GetValueOrDefault("OptionalSpecialTraits", new JsonElement())),
                        CertainSpecialTraits = DeserializeSpecialTraits(heritageJson.GetValueOrDefault("CertainSpecialTraits", new JsonElement()))
                    };

                    ExpandSpecialTraits(heritage.OptionalSpecialTraits);
                    ExpandSpecialTraits(heritage.CertainSpecialTraits);

                    Heritages.Add(heritage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading or deserializing the JSON file: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine($"File {filePath} not found.");
        }
    }

    private static void ConvertAncestriesToHeritages()
    {
        foreach(Ancestry ances in Ancestries)
        {
            Heritage helper = new Heritage();
            helper.Name = ances.Name;
            helper.LH = ances.LH;
            helper.CertainSpecialTraits = ances.CertainSpecialTraits;
            helper.EyeColor = ances.EyeColor;
            helper.HairColor = ances.HairColor;
            helper.Hairstyles = ances.Hairstyles;
            helper.OptionalSpecialTraits = ances.OptionalSpecialTraits;
            helper.SkinColor = ances.SkinColor;
            helper.Undertones = ances.Undertones;
            Heritages.Add(helper);
        }
    }
    
    // Helper methods for deserialization and defaults

    private static string[] DeserializeJsonArray(JsonElement jsonElement)
    {
        return jsonElement.ValueKind == JsonValueKind.Array
            ? jsonElement.EnumerateArray().Select(e => e.GetString()).Where(s => s != null).ToArray()
            : Array.Empty<string>();
    }

    private static string[][] DeserializeSpecialTraits(JsonElement jsonElement)
    {
        return jsonElement.ValueKind == JsonValueKind.Array
            ? jsonElement.EnumerateArray().Select(traitArray =>
                traitArray.ValueKind == JsonValueKind.Array
                    ? traitArray.EnumerateArray().Select(e => e.GetString()).Where(s => s != null).ToArray()
                    : Array.Empty<string>()
            ).ToArray()
            : Array.Empty<string[]>();
    }

    private static void ExpandSpecialTraits(string[][] specialTraits)
    {
        for (int j = 0; j < specialTraits.Length; j++)
        {
            for (int k = 0; k < specialTraits[j].Length; k++)
            {
                if (specialTraits[j][k].Contains("{AdjectiveSizes}"))
                {
                    specialTraits[j] = AdjectiveSizes
                        .Select(adj => specialTraits[j][k].Replace("{AdjectiveSizes}", adj))
                        .ToArray();
                }
            }
        }
    }

    private static string[] ResolveDefaults(string[] values, string[] defaults)
    {
        if (values[0].ToLower() == "default")
        {
            return defaults.Concat(values.Where(v => v.ToLower() != "default")).ToArray();
        }
        return values;
    }



}

public static class JsonElementExtensions
{
    public static T GetValueOrDefault<T>(this Dictionary<string, JsonElement> dict, string key, T defaultValue = default)
    {
        return dict.TryGetValue(key, out var value) && value.ValueKind != JsonValueKind.Undefined
            ? JsonSerializer.Deserialize<T>(value.GetRawText())
            : defaultValue;
    }
}