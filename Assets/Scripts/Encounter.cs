using UnityEngine;
using recruits;
using System.Collections.Generic;

public class Encounter
{
    public string name;
    //public Sprite character; //the character that you are fighting
    public int level = 1;
    public List<string> encounterRecs;
    //public enemyStrategy
    
    public Encounter(string name, int level, List<string> recruits)
    {
        this.name = name;
        this.level = level;
        encounterRecs = recruits;
    }

    

    


}
