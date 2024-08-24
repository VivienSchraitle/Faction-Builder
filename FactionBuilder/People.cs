// People.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


    public class People
    {
        static People()
        {
            // No need to load ancestries and heritages here, as DataManager handles it
        }

        public static string getPerson(string[] args)
        {
            string person = "";

            int totalLikelihoodAnces = DataManager.Ancestries.Sum(a => a.LH);
            int totalLikelihoodHerria = DataManager.Heritages.Sum(h => h.LH);

            Random random = new Random();
            int randomValue = random.Next(totalLikelihoodAnces);
            int randomValueH = random.Next(totalLikelihoodHerria);

            string selectedAncestry = "";
            foreach (var ancestry in DataManager.Ancestries)
            {
                randomValue -= ancestry.LH;
                if (randomValue < 0)
                {
                    selectedAncestry = ancestry.Name;
                    break;
                }
            }

            string selectedHeritage = "";
            foreach (var heritage in DataManager.Heritages)
            {
                randomValueH -= heritage.LH;
                if (randomValueH < 0)
                {
                    selectedHeritage = heritage.Name;
                    break;
                }
            }

            Console.WriteLine("Random Ancestry: " + selectedAncestry + " " + selectedHeritage);
            return person;
        }

        private static string addTraits()
        {
            return "";
        }
    }

