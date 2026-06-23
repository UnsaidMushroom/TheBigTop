using UnityEngine;
using System.Collections.Generic;
using recruits;

public class EnemiesManager : BattleManager
{
    public static EnemiesManager Instance;

    public static List<Encounter> encounters;
    public Encounter activeEncounter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        if (encounters == null)
        {
            LoadEncounters();
        }
        getNewEncounter();
    }

    // Update is called once per frame
    void Update()
    {
        //run active encounter logic
    }
    public override void KnockOut(GameObject KOed)
    {
        base.KnockOut(KOed);
    }

    public void LoadEncounters()
    {
        encounters = new List<Encounter>();

        //for now, only placing one encounter, really will have more
        encounters.Add(new Encounter("default", -1, new List<string>() { "simpleCommon1", "simpleRare1", "simpleEpic1", "simpleCommon2", "simpleCommon1" }));

    }

    public void getNewEncounter()
    {
        int r = Random.Range(0, encounters.Count);
        activeEncounter = encounters[r];
        for (int i = 0; i < 5;  i++)
        {
            //place the five recruits from encounters[r] in enemy positions
        }
    }
}
