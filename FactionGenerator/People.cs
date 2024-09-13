// People.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Utilities;


public class People
{
    private DataManager.Ancestry MyAnces;
    private DataManager.Heritage MyHeria;

    public int FinanceScore { get; private set; }
    public int ReputationScore { get; private set; }
    public int ReligionScore { get; private set; }
    Random random = new Random();
    private Faction localFaction;

    public struct Person
    {
        public string Name;
        public float Size;
        public int Age;
        public string MyClass;
        public string MyJob;
        public string MyApperance;
        public string SkinColor;
        public string Undertones;
        public string HairColor;
        public string Hairstyle;
        public string EyeColor;
        public string[] SpecialTraits;
        public string Heritage;
        Faction myFaction;
    }
    public void generatePeople(Faction myFaction = null)
    {
        if (myFaction != null)
        {
            localFaction = myFaction;
            int? leaders = myFaction.assignedLeaders;
            string? leadership = myFaction.Leadership;
            if (leadership == "Anarchistic")
            {
                //intentionally empty
            }
            else if (leadership == "Democratic")
            {
                if (myFaction.VotingSystem.Contains("non elected leader"))
                {
                    generatePerson("Non Elected Leader");
                }
                else if (myFaction.VotingSystem.Contains("elected leader"))
                {
                    generatePerson("Elected Leader");
                }
                if (myFaction.VotingSystem.Contains("council"))
                {
                    for (int i = myFaction.assignedLeaders; i > 0; i--)
                    {
                        generatePerson("Council Member");
                    }
                }
            }
            else if (leadership == "Oligarchic")
            {

                for (int i = myFaction.assignedLeaders; i > 0; i--)
                {
                    generatePerson("Oligarch", myFaction.PowerType);
                }

            }
            else if (leadership == "Autocratic")
            {
                generatePerson("Autocrat", myFaction.PowerType);
            }
        }
    }
    public Person generatePerson(string position = null, string type = null)
    {
        Person thisPerson = new();
       if (position != null && type != null)
       {
            thisPerson.MyJob = $"{position} chosen through {type}";
       }
       else if (position != null)
       {
            thisPerson.MyJob = position;
       }
       else if (localFaction != null)
       {
            int rndScore = random.Next(101);
            FinanceScore = Math.Clamp(localFaction.FinanceScore + random.Next(-localFaction.FinanceScore/3,localFaction.FinanceScore/3),0,100); //max is 400
            int finances = rndScore + 3 * FinanceScore; //max is 400

            if (finances < 70)
            {
                if (random.Next(101) < 81 && localFaction.MoneySources["Low"].Count > 0)
                {
                string tempjob = localFaction.MoneySources["Low"][random.Next(localFaction.MoneySources["Low"].Count)];
                thisPerson.MyJob = DataManager.LowJobMappings[tempjob][random.Next(DataManager.LowJobMappings[tempjob].Count)];
                }
                else
                {
                    int index = random.Next(DataManager.LowJobMappings.Keys.Count);
                    string tempjob = DataManager.LowJobMappings.Keys.ElementAt(index);
                    thisPerson.MyJob = DataManager.LowJobMappings[tempjob][random.Next(DataManager.LowJobMappings[tempjob].Count)];
                }
            }
            else if (finances < 200)
            {
                if (random.Next(101) < 81 && localFaction.MoneySources["Mid"].Count > 0)
                {
                string tempjob = localFaction.MoneySources["Mid"][random.Next(localFaction.MoneySources["Mid"].Count)];
                thisPerson.MyJob = DataManager.MidJobMappings[tempjob][random.Next(DataManager.MidJobMappings[tempjob].Count)];
                }
                else
                {
                    int index = random.Next(DataManager.MidJobMappings.Keys.Count);
                    string tempjob = DataManager.MidJobMappings.Keys.ElementAt(index);
                    thisPerson.MyJob = DataManager.MidJobMappings[tempjob][random.Next(DataManager.MidJobMappings[tempjob].Count)];
                }
            }
            else if (finances < 350)
            {
                if (random.Next(101) < 81 && localFaction.MoneySources["High"].Count > 0)
                {
                string tempjob = localFaction.MoneySources["High"][random.Next(localFaction.MoneySources["High"].Count)];
                thisPerson.MyJob = DataManager.HighJobMappings[tempjob][random.Next(DataManager.HighJobMappings[tempjob].Count)];
                }
                else
                {
                    int index = random.Next(DataManager.HighJobMappings.Keys.Count);
                    string tempjob = DataManager.HighJobMappings.Keys.ElementAt(index);
                    thisPerson.MyJob = DataManager.HighJobMappings[tempjob][random.Next(DataManager.HighJobMappings[tempjob].Count)];
                }
            }
            else
            {
                if (random.Next(101) < 81 && localFaction.MoneySources["Insane"].Count > 0)
                {
                string tempjob = localFaction.MoneySources["Insane"][random.Next(localFaction.MoneySources["Insane"].Count)];
                thisPerson.MyJob = DataManager.SuperJobMappings[tempjob][random.Next(DataManager.SuperJobMappings[tempjob].Count)];
                }
                else
                {
                    int index = random.Next(DataManager.SuperJobMappings.Keys.Count);
                    string tempjob = DataManager.SuperJobMappings.Keys.ElementAt(index);
                    thisPerson.MyJob = DataManager.SuperJobMappings[tempjob][random.Next(DataManager.SuperJobMappings[tempjob].Count)];
                }
            }
       }
       return thisPerson;
    }

    public DataManager.Ancestry getAncestry(string[] args)
    {
        int totalLikelihoodAnces = DataManager.Ancestries.Sum(a => a.LH);

        int randomValue = random.Next(totalLikelihoodAnces);

        DataManager.Ancestry selectedAncestry = new();
        foreach (var ancestry in DataManager.Ancestries)
        {
            randomValue -= ancestry.LH;
            if (randomValue < 0)
            {
                selectedAncestry = ancestry;
                break;
            }
        }
        MyAnces = selectedAncestry;
        return selectedAncestry;
    }

    public DataManager.Heritage getHeritage(string[] args)
    {
        int totalLikelihoodHerria = DataManager.Heritages.Sum(h => h.LH);
        int randomValueH = random.Next(totalLikelihoodHerria);

        DataManager.Heritage selectedHeritage = new();
        foreach (var heritage in DataManager.Heritages)
        {
            randomValueH -= heritage.LH;
            if (randomValueH < 0)
            {
                selectedHeritage = heritage;
                break;
            }
        }
        MyHeria = selectedHeritage;
        return selectedHeritage;
    }
    public string getClass()
    {
        int baseNumber = random.Next(DataManager.Classes.Length - 4);
        baseNumber += random.Next(-1, 3);
        baseNumber = Math.Clamp(baseNumber, 0, DataManager.Classes.Length - 1);
        return DataManager.Classes[baseNumber];
    }
    public string getJob(Faction myFaction = null)
    {
        return "";
    }
    public string getApperance()
    {
        return "";
    }
    private string addTraits()
    {
        return "";
    }
}

