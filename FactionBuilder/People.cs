using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;

namespace FactionBuilder
{

    public class People
    {
        static People()
        {
            GetAncestries();
            GetHeriatages();
        }
        static List<Ancestry> ancestries = new List<Ancestry>();
        static List<Heriatage> heriatages = new List<Heriatage>();

        public static string[] DefaultSkinColor;
        public static string[] DefaultUndertones;
        public static string[] DefaultHairColor;
        public static string[] DefaultHairstyles;
        public static string[] DefaultEyeColor;
        public static string[] AdjectivesSize;
        struct Ancestry
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
            public string[] Heriatage;

            public int MaxAge;
            public int MatureAge;
        }
        struct Heriatage
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
        public static void GetAncestries()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();
            string filePath = Path.Combine(path, "JSONs", "Ancestires.JSON");

            if (File.Exists(filePath))
            {
                try
                {
                    string jsonData = File.ReadAllText(filePath);

                    // Deserialize the JSON data into a list of objects
                    var jsonObjects = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(jsonData);

                    // Extract default values from the first JSON object
                    var defaultValues = jsonObjects[0];

                    DefaultSkinColor = JsonSerializer.Deserialize<string[]>(defaultValues["DefaultSkinColor"]);
                    DefaultUndertones = JsonSerializer.Deserialize<string[]>(defaultValues["DefaultUndertones"]);
                    DefaultHairColor = JsonSerializer.Deserialize<string[]>(defaultValues["DefaultHairColor"]);
                    DefaultHairstyles = JsonSerializer.Deserialize<string[]>(defaultValues["DefaultHairstyles"]);
                    DefaultEyeColor = JsonSerializer.Deserialize<string[]>(defaultValues["DefaultEyeColor"]);
                    AdjectivesSize = JsonSerializer.Deserialize<string[]>(defaultValues["AdjectivesSize"]);

                    // Deserialize the rest of the JSON into Ancestry objects
                    for (int i = 1; i < jsonObjects.Count; i++)
                    {
                        var ancestryJson = jsonObjects[i];
                        var ancestry = JsonSerializer.Deserialize<Ancestry>(ancestryJson.ToString());

                        // Assign default values if properties are empty
                        ancestry.SkinColor = ancestry.SkinColor[0].ToLower().Equals("default") ? (string[])DefaultSkinColor.Concat(ancestry.SkinColor) : ancestry.SkinColor;
                        ancestry.Undertones = ancestry.Undertones[0].ToLower().Equals("default") ? (string[])DefaultUndertones.Concat(ancestry.Undertones) : ancestry.Undertones;
                        ancestry.HairColor = ancestry.HairColor[0].ToLower().Equals("default") ? (string[])DefaultHairColor.Concat(ancestry.HairColor) : ancestry.HairColor;
                        ancestry.Hairstyles = ancestry.Hairstyles[0].ToLower().Equals("default") ? (string[])DefaultHairstyles.Concat(ancestry.Hairstyles) : ancestry.Hairstyles;
                        ancestry.EyeColor = ancestry.EyeColor[0].ToLower().Equals("default") ? (string[])DefaultEyeColor.Concat(ancestry.EyeColor) : ancestry.EyeColor;

                        if (ancestry.OptionalSpecialTraits == null) ancestry.OptionalSpecialTraits = new string[][] { };
                        if (ancestry.CertainSpecialTraits == null) ancestry.CertainSpecialTraits = new string[][] { };
                        for (int j = 0; j < ancestry.CertainSpecialTraits.Length; j++)
                        {
                            for (int k = 0; k < ancestry.CertainSpecialTraits[j].Length; k++)
                            {
                                if (ancestry.CertainSpecialTraits[j][k].Contains("{AdjectivesSize}"))
                                {
                                    ancestry.CertainSpecialTraits[j] = AdjectivesSize
                                        .Select(adj => ancestry.CertainSpecialTraits[j][k].Replace("{AdjectivesSize}", adj))
                                        .ToArray();
                                }
                            }
                        }
                        for (int j = 0; j < ancestry.OptionalSpecialTraits.Length; j++)
                        {
                            for (int k = 0; k < ancestry.OptionalSpecialTraits[j].Length; k++)
                            {
                                if (ancestry.OptionalSpecialTraits[j][k].Contains("{AdjectivesSize}"))
                                {
                                    ancestry.OptionalSpecialTraits[j] = AdjectivesSize
                                        .Select(adj => ancestry.OptionalSpecialTraits[j][k].Replace("{AdjectivesSize}", adj))
                                        .ToArray();
                                }
                            }
                        }

                        ancestries.Add(ancestry);
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
        public static void GetHeriatages()
        {
            foreach (Ancestry ances in ancestries)
            {
                heriatages.Add(AncesConverter(ances));
            }
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();
            string filePath = Path.Combine(path, "JSONs", "Heriatage.JSON");

            if (File.Exists(filePath))
            {
                try
                {
                    string jsonData = File.ReadAllText(filePath);

                    // Deserialize the JSON data into a list of objects
                    var jsonObjects = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(jsonData);

                    // Extract default values from the first JSON object
                    var defaultValues = jsonObjects[0];

                    DefaultSkinColor = JsonSerializer.Deserialize<string[]>(defaultValues["DefaultSkinColor"]);
                    DefaultUndertones = JsonSerializer.Deserialize<string[]>(defaultValues["DefaultUndertones"]);
                    DefaultHairColor = JsonSerializer.Deserialize<string[]>(defaultValues["DefaultHairColor"]);
                    DefaultHairstyles = JsonSerializer.Deserialize<string[]>(defaultValues["DefaultHairstyles"]);
                    DefaultEyeColor = JsonSerializer.Deserialize<string[]>(defaultValues["DefaultEyeColor"]);
                    AdjectivesSize = JsonSerializer.Deserialize<string[]>(defaultValues["AdjectivesSize"]);

                    // Deserialize the rest of the JSON into Ancestry objects
                    for (int i = 1; i < jsonObjects.Count; i++)
                    {
                        var ancestryJson = jsonObjects[i];
                        var ancestry = JsonSerializer.Deserialize<Ancestry>(ancestryJson.ToString());

                        // Assign default values if properties are empty
                        ancestry.SkinColor = ancestry.SkinColor[0].ToLower().Equals("default") ? (string[])DefaultSkinColor.Concat(ancestry.SkinColor) : ancestry.SkinColor;
                        ancestry.Undertones = ancestry.Undertones[0].ToLower().Equals("default") ? (string[])DefaultUndertones.Concat(ancestry.Undertones) : ancestry.Undertones;
                        ancestry.HairColor = ancestry.HairColor[0].ToLower().Equals("default") ? (string[])DefaultHairColor.Concat(ancestry.HairColor) : ancestry.HairColor;
                        ancestry.Hairstyles = ancestry.Hairstyles[0].ToLower().Equals("default") ? (string[])DefaultHairstyles.Concat(ancestry.Hairstyles) : ancestry.Hairstyles;
                        ancestry.EyeColor = ancestry.EyeColor[0].ToLower().Equals("default") ? (string[])DefaultEyeColor.Concat(ancestry.EyeColor) : ancestry.EyeColor;

                        if (ancestry.OptionalSpecialTraits == null) ancestry.OptionalSpecialTraits = new string[][] { };
                        if (ancestry.CertainSpecialTraits == null) ancestry.CertainSpecialTraits = new string[][] { };
                        for (int j = 0; j < ancestry.CertainSpecialTraits.Length; j++)
                        {
                            for (int k = 0; k < ancestry.CertainSpecialTraits[j].Length; k++)
                            {
                                if (ancestry.CertainSpecialTraits[j][k].Contains("{AdjectivesSize}"))
                                {
                                    ancestry.CertainSpecialTraits[j] = AdjectivesSize
                                        .Select(adj => ancestry.CertainSpecialTraits[j][k].Replace("{AdjectivesSize}", adj))
                                        .ToArray();
                                }
                            }
                        }
                        for (int j = 0; j < ancestry.OptionalSpecialTraits.Length; j++)
                        {
                            for (int k = 0; k < ancestry.OptionalSpecialTraits[j].Length; k++)
                            {
                                if (ancestry.OptionalSpecialTraits[j][k].Contains("{AdjectivesSize}"))
                                {
                                    ancestry.OptionalSpecialTraits[j] = AdjectivesSize
                                        .Select(adj => ancestry.OptionalSpecialTraits[j][k].Replace("{AdjectivesSize}", adj))
                                        .ToArray();
                                }
                            }
                        }

                        ancestries.Add(ancestry);
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
        private static Heriatage AncesConverter(Ancestry ances)
        {
            return new Heriatage { Name = ances.Name, LH = ances.LH, SkinColor = ances.SkinColor, Undertones = ances.Undertones, HairColor = ances.HairColor, CertainSpecialTraits = ances.CertainSpecialTraits, EyeColor = ances.EyeColor, Hairstyles = ances.Hairstyles, OptionalSpecialTraits = ances.OptionalSpecialTraits };
        }
        public static string getPerson(string[] args)
        {
            string person = "";
            // Create a dictionary to store ancestries and their likelihoods


            int totalLikelihoodAnces = 0;
            int totalLikelihoodHerria = 0;
            // Calculate the total likelihood
            foreach (Ancestry race in ancestries)
            {
                totalLikelihoodAnces += race.LH;
            }
            foreach (Heriatage race in heriatages)
            {
                totalLikelihoodHerria += race.LH;
            }

            // Generate a random number between 0 and the total likelihood
            Random random = new Random();
            int randomValue = random.Next(totalLikelihoodAnces);
            int randomValueH = random.Next(totalLikelihoodHerria);

            // Select the ancestry
            string selectedAncestry = "";
            foreach (var ancestry in ancestries)
            {
                randomValue -= ancestry.LH;
                if (randomValue < 0)
                {
                    selectedAncestry = ancestry.Name;
                    break;
                }
            }
            string selectedHerritage = "";
            foreach (var herritage in heriatages)
            {
                randomValueH -= herritage.LH;
                if (randomValueH < 0)
                {
                    selectedHerritage = herritage.Name;
                    break;
                }
            }

            Console.WriteLine("Random Ancestry: " + selectedAncestry + " " + selectedHerritage);
            return person;
        }
        private static string addTraits()
        {
            return "";
        }
    }

}