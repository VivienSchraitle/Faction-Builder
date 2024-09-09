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
    private string MyClass;
    private string MyJob;
    private string MyApperance;
    Random random = new Random();
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
        int baseNumber = random.Next(DataManager.Classes.Length-4);
        baseNumber += random.Next(-1, 3);
        baseNumber = Math.Clamp(baseNumber, 0, DataManager.Classes.Length - 1);
        return DataManager.Classes[baseNumber];
    }
    public string getJob(int setJob) 
    {
        switch(setJob)
        {
            default:
            return "";
        }
        
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

