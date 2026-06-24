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
        if (RecruitManager.Instance == null) //catch edge case
        {
            FindFirstObjectByType<RecruitManager>().Start(); // force recruit manager to load first
        }

        int r = Random.Range(0, encounters.Count);
        activeEncounter = encounters[r];
        recruitList = new List<Recruit>();
        foreach(string rec in activeEncounter.encounterRecs)
        {
            recruitList.Add(RecruitManager.Instance.GetNewRecruit(rec));
        }
        PlaceRecruits();
    }
}
