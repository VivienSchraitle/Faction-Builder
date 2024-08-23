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
        }
        static List<Ancestry> ancestries = new List<Ancestry>();
        static List<Herriatage> herriatages = new List<Herriatage>();

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

            public int MaxAge;
            public int MatureAge;
        }
        struct Herriatage
        {
            public string Name;
            public int LH;
            public float SizeAvg;
            public float SizeDev;
            public string[] SkinColor;
            public string[] HairColor;
            public string[] EyeColor;
            public string[] SpecialTraits;
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
        public static string getPerson(string[] args)
        {
            string person = "";
            // Create a dictionary to store ancestries and their likelihoods
            ancestries = new List<Ancestry>
            {
                new Ancestry {Name = "Human", LH = 100, SizeAvg = 4.3f, SizeDev = 2f, MatureAge = 18, MaxAge = 90},
                new Ancestry {Name = "Dwarf", LH = 100, SizeAvg = 3.7f, SizeDev = 1f, MatureAge = 25, MaxAge = 350},
                new Ancestry {Name = "Elf", LH = 40, SizeAvg = 5.4f, SizeDev = 2f, MatureAge = 50, MaxAge = 600},
                new Ancestry {Name = "Gnome", LH = 100, SizeAvg = 3f, SizeDev = 0.4f, MatureAge = 18, MaxAge = 350},
                new Ancestry {Name = "Goblin", LH = 20, SizeAvg = 3.1f, SizeDev = 0.4f, MatureAge = 5, MaxAge = 60},
                new Ancestry {Name = "Halfling", LH = 100, SizeAvg = 2.7f, SizeDev = 0.7f, MatureAge = 18, MaxAge = 350},
                new Ancestry {Name = "Orc", LH = 50, SizeAvg = 5.2f, SizeDev = 2.3f, MatureAge = 17, MaxAge = 100},
                new Ancestry {Name = "Leshy", LH = 10, SizeAvg = 2.5f, SizeDev = 1f, MatureAge = 0, MaxAge = 10000},
                new Ancestry {Name = "Athamaru", LH = 10, SizeAvg = 4.3f, SizeDev = 2f, MatureAge = 20, MaxAge = 260},
                //??? ocean creature ppl  Frills, barbels, and crested fins add to their flamboyant appearance. 
                //The sheer variety of crest shapes, scale patterns, and fin styles make individuals distinct, even as communities share features.
                //Athamarus value natural adornments that blur the lines between body modifications and jewelry. 
                //In areas where coral grows, tending reefs and their symbiotic species are highly valued tasks. 
                //Some fashion still-living specimens into earrings or cuffs, then gently guide the coral as it continues to grow.
                //These pieces often stay in circulation for generations and are valued as living community history. 
                //Other uses of coral include integrating pieces in with the wearer's body in symbiosis,
                // the coral providing nutrients and the athamaru ensuring access to sunlight and quality water.                 
                new Ancestry {Name = "Azarketi", LH = 5, SizeAvg = 5f, SizeDev = 1f, MatureAge = 20, MaxAge = 120,
                SkinColor = (string[])DefaultSkinColor.Concat(new string[]{"Pearlescent White", "Pinkish White", "Light Coral Pink", "Red", "Peach", "Pale Pink", "Blush Pink", "Light Greenish", "Seafoam Green", "Pale Olive", "Light Brown", "Sandy Brown", "Coral Brown", "Rose Beige", "Warm Beige", "Ivory"})
                ,EyeColor = (string[])DefaultEyeColor.Concat(new string[]{"Violet", "Violet", "Violet", "Violet", "Violet", "Violet",})
                , Hairstyles = (string[])DefaultHairstyles.Concat(new string[]{"Single Dorsal Fin", "Double Dorsal Fins", "Side Fins", "Triple Fin Crest", "V-shaped Fin Ridge", "Scaled Ridge with Spikes", "Curved Ridge", "Jagged Ridge", "Smooth Ridge", "Webbed Fin Ridge", "Short Scaled Ridge", "Long Flowing Fin", "Frilled Side Fins", "Layered Scaled Ridge", "Banded Ridge", "Spiked Fin Crest", "Rippled Fin", "Segmented Ridge", "Fan-shaped Fin", "Crown-shaped Ridge", "Twisted Fin", "Horned Ridge", "Double Webbed Fins", "Scalloped Ridge", "Pinnate Fin", "Interlocking Ridges", "Flared Fin Ridge", "Clustered Fins", "Radiant Ridge", "Spiral Fin Crest", "Single Dorsal Fin", "Double Dorsal Fins", "Side Fins", "Triple Fin Crest", "V-shaped Fin Ridge", "Scaled Ridge with Spikes", "Curved Ridge", "Jagged Ridge", "Smooth Ridge", "Webbed Fin Ridge", "Short Scaled Ridge", "Long Flowing Fin", "Frilled Side Fins", "Layered Scaled Ridge", "Banded Ridge", "Spiked Fin Crest", "Rippled Fin", "Segmented Ridge", "Fan-shaped Fin", "Crown-shaped Ridge", "Twisted Fin", "Horned Ridge", "Double Webbed Fins", "Scalloped Ridge", "Pinnate Fin", "Interlocking Ridges", "Flared Fin Ridge", "Clustered Fins", "Radiant Ridge", "Spiral Fin Crest","Single Dorsal Fin", "Double Dorsal Fins", "Side Fins", "Triple Fin Crest", "V-shaped Fin Ridge", "Scaled Ridge with Spikes", "Curved Ridge", "Jagged Ridge", "Smooth Ridge", "Webbed Fin Ridge", "Short Scaled Ridge", "Long Flowing Fin", "Frilled Side Fins", "Layered Scaled Ridge", "Banded Ridge", "Spiked Fin Crest", "Rippled Fin", "Segmented Ridge", "Fan-shaped Fin", "Crown-shaped Ridge", "Twisted Fin", "Horned Ridge", "Double Webbed Fins", "Scalloped Ridge", "Pinnate Fin", "Interlocking Ridges", "Flared Fin Ridge", "Clustered Fins", "Radiant Ridge", "Spiral Fin Crest"})
               // , SpecialTraits = new string[]{"Webbed Feet", "Webbed Hands", "Gills on Neck", "Gills on Torso", "Waterproof Scales", "Retractable Fins", "Transparent Eyelids", "Tail Fin", "Barbed Fingernails", "Slimy Skin", "Sharp Teeth", "Color-Changing Scales", "Curved Ridge", "Gill Covers", "Light-Sensitive Eyes", "Nictitating Membrane", "Camouflage Skin", "Prehensile Tail", "Ridge-Lined Skull", "Luminous Eyes", "Coral-Like Armor Plates", "Suction Cup Pads", "Shell-Like Exoskeleton", "Dermal Denticles", "Hardened Scales", "Enlarged Chest Cavity", "Strong Jaw Muscles", "Wide Peripheral Vision", "Barbed Tongue", "Bioluminescent Markings", "Subdermal Air Sacs", "Flesh-Colored Fins", "Adaptable Skin Texture", "Semi-Permeable Skin", "Reflective Eyes", "Buoyant Body Structure", "Secondary Set of Fins", "Antennae-Like Protrusions", "Expandable Throat Pouch", "Padded Foot Soles", "Heat-Resistant Scales", "Razor-Sharp Ridge", "Secondary Jaw", "Slime Production", "Webbed Neck Folds", "Wing-Like Fins"}
                }, 
                // red. Azarketis appear as regal, athletic humans.
                // Skin is pearlescent white to pinkish, greenish, or brown tones reminiscent of coral. 
                // rarely hair, fins or scaled ridges on their heads instead.
                // Like the Azlanti people from which they descend, they often have violet eyes.
                // Sets of three gills on either side of their necks, as well as their webbed hands and feet. 
                // Age, height book estimate
                new Ancestry {Name = "Catfolk", LH = 60, SizeAvg = 4.5f, SizeDev = 2f, MatureAge = 15, MaxAge = 80,
                SkinColor = new string[]{}},
                //Short claws, soft fur, long tail, large ears, vertical pupils, furr patterns liek cats
                //age from book, height from book estimate
                new Ancestry {Name = "Centaur", LH = 2, SizeAvg = 6, SizeDev = 1, MatureAge = 15, MaxAge = 290}, 
                //any horse options. upper half has any human options.
               new Ancestry {Name = "Fetchlings", LH =2 , SizeAvg = 5.2f, SizeDev = 1.2f, MatureAge = 20, MaxAge = 140}, 
                // any human, Fetchlings' reflective, pupilless eyes can pierce darkness (also glow)
                // skin tones fall on a monochromatic scale from stark white to deep black, and all the shades of gray between
               new Ancestry {Name = "Gnoll", LH = 2, SizeAvg = 6, SizeDev = 1, MatureAge = 15, MaxAge = 110},
                //Gnolls are large, hyena-like humanoids with short muzzles, sharp teeth, and large and expressive round ears. 
                //Their bodies are covered in shaggy fur, rougher on the back and softer and lighter on the stomach and throat,
                // usually in an off-white, tan, or brown shade—spots and stripes are both common
               new Ancestry {Name = "Grippli", LH = 2, SizeAvg = 1.5f, SizeDev = 1, MatureAge = 12, MaxAge = 100},
                //Gripplis resemble humanoid tree frogs, with oversized eyes, wide mouths, and gangly physique
                //their slight frames and large toes afford excellent grip while climbing, while their colorful skin provides reliable camouflage that varies by their home environment— green and brown for jungle-dwelling groups, blue and orange for riparian communities, and many other colors between
                // jades break down of above: skin color can range wildly, anything to help them blend in with where they live.
               new Ancestry {Name = "Hobgoblin", LH = 2, SizeAvg = 4, SizeDev = 2.6f, MatureAge = 14, MaxAge = 70},
                // Hobgoblins stand nearly as tall as humans, though they tend to have shorter legs and longer arms and torsos. They have the bald, wide heads and beady eyes, and gray skin that becomes steely blue when tanned
                //Their eyes burn fiery orange or red
               new Ancestry {Name = "Kitsune", LH =2 , SizeAvg = 4.5f, SizeDev = 1.8f, MatureAge = 15, MaxAge = 1500},
                // its kitsune, im not gonan bother putting any text in so you can do it all how you like miss fox :3

               new Ancestry {Name = "Kobold", LH = 2, SizeAvg = 2.4f, SizeDev = .10f, MatureAge = 12, MaxAge = 60},
                // The color of a kobold’s scales can vary widely. Most often, they mimic the hues of chromatic or metallic dragons, with a mix of slightly darker or lighter scales that create a mottled appearance. The scales of newly hatched kobolds often reflect the community’s draconic exemplar, whether that’s the dragon they currently serve or the dragon type from which they’re descended.
                //Kobolds are short (about 3 feet tall) reptilian humanoids with slender bodies and long tails 
                //They often boast distant draconic ancestry, and every kobold displays one or more draconic features, such as stout horns, razor-sharp teeth, or—more rarely—vestigial wings or draconic breath
               new Ancestry {Name = "Lizardfolk", LH = 2, SizeAvg = 6, SizeDev = 1, MatureAge = 15, MaxAge = 120},
                // note: height was only from a blurb saying "the average lizardfolk stands 6 to 7 feet tall, but grows throughout their lifetime, gaining strength and size with age", so maybe a 2? or 3? rather then the 1 i set.
                //Lizardfolk vary depending on their environment, but share toothy snouts and long and powerful tails. Those from temperate or desert regions tend toward gray, green, or brown scales that aid in camouflage, while those from tropical climes are brightly colored. Many sport dorsal spikes or garish neck frills that hint at their clan lineage
               new Ancestry {Name = "Merfolk", LH = 2 , SizeAvg = 5.8f, SizeDev = 2, MatureAge = 15, MaxAge = 75},
                //Merfolk are among the most distinct ancestries on Golarion; one can't really mistake a merfolk for anything else. From the waist up, merfolk have the bodies of humanoids with powerful bodies and sharp, aquadynamic features. Their eyes are a little larger than those of a human, and many have slightly pointed ears, similar to those of aiuvarins. Below the waist, merfolk have the bodies of great fish, each with a long, scaled tail ending in a fin or pair of fins.
                //Merfolk come in every color of skin and scale imaginable. Most often, their human bodies have skin tones similar to those of humans or elves living in the same area—the merfolk of the Shackles, for instance, bear a certain similarity to the Lirgeni, while those who live in the Steaming Sea somewhat resemble their Ulfen neighbors. Merfolk who dwell farther from land take on colorations closer to fish tones of blue, gray, or even green. Deep-sea abyssal merfolk have dark gray, midnight blue, or even translucent skin.
                //Merfolk fish tails are colored similarly to local fish populations. In the temperate zones where most merfolk live, they have iridescent, silvery bodies with traces of gray or blue; tropical merfolk might display patterns of brilliant colors, while those born in the depths might have translucent scales or faintly bioluminescent stripes.
               new Ancestry {Name = "Minotaur", LH =2 , SizeAvg = 5.4f, SizeDev = 1, MatureAge = 17, MaxAge = 150},
                // height, and age is from e5. height for sure didn't make sense even with my very over the top rounding.
                //Minotaurs are tall, bulky humanoids with the bovine features such as horns, hooves, and elongated faces. Their fur patterns are frequently monotone in deep browns or blacks, though white or gray aren't uncommon. Though the large size of a minotaur might cause one think they are clumsy, the truth is quite the opposite. Minotaur hooves rest on a delicate balance point, making their footfalls quiet and precise. However, when there is a need to be heard, the steps of a minotaur can fall like thunder.
                // they also like horn addons or tattos near theb ase if em if they smoll horns

            };
            List<Herriatage> herriatages = new List<Herriatage>();

            foreach (Ancestry ancestry in ancestries)
            {
                herriatages.Add(new Herriatage
                {
                    Name = ancestry.Name,
                    LH = ancestry.LH,
                    SizeAvg = ancestry.SizeAvg,
                    SizeDev = ancestry.SizeDev,
                    SkinColor = ancestry.SkinColor,
                    HairColor = ancestry.HairColor,
                    EyeColor = ancestry.EyeColor,
                    // Add more properties as needed
                });
            }

            int totalLikelihoodAnces = 0;
            int totalLikelihoodHerria = 0;
            // Calculate the total likelihood
            foreach (Ancestry race in ancestries)
            {
                totalLikelihoodAnces += race.LH;
            }
            foreach (Herriatage race in herriatages)
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
            foreach (var herritage in herriatages)
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