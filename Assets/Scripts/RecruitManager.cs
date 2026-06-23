using System.Collections.Generic;
using UnityEngine;

namespace recruits {
    public enum Rarity
    {
        COMMON,
        RARE,
        EPIC,
        LEGENDARY
    }

    


    public class RecruitManager : MonoBehaviour
    {

        public static RecruitManager Instance;
        public List<Recruit> recruitList;
        public List<Recruit> battleRecruits;

        public Dictionary<string, Recruit> masterRecruitDict;

        //these are lists for the sake of indexing, there should not be repeats
        public List<string> commonRecruits;
        public List<string> rareRecruits;
        public List<string> epicRecruits;
        public List<string> legendaryRecruits;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);

                initializeRecruits();

            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void initializeRecruits()
        {
            //initialize collections
            masterRecruitDict = new Dictionary<string, Recruit>();
            commonRecruits = new List<string>();
            rareRecruits = new List<string>();
            epicRecruits = new List<string>();
            legendaryRecruits = new List<string>();
            recruitList = new List<Recruit> ();


            //prepare all recruits!
            //put real recruits here

            //this is dummy data! when we have real stuff, remove this!

            masterRecruitDict.Add("simpleCommon1", new Recruit("simpleCommon1", Rarity.COMMON, 5, "simple fire attack", 30));
            masterRecruitDict.Add("simpleCommon2", new Recruit("simpleCommon2", Rarity.COMMON, 5, "simple water attack", 30));
            masterRecruitDict.Add("simpleCommon3", new Recruit("simpleCommon3", Rarity.COMMON, 5, "simple air attack", 30));
            masterRecruitDict.Add("simpleCommon4", new Recruit("simpleCommon4", Rarity.COMMON, 5, "simple ice attack", 30));
            masterRecruitDict.Add("err 1", new Recruit("err 1", Rarity.COMMON, 5, "simple ice attack", 30));


            masterRecruitDict.Add("simpleRare1", new Recruit("simpleRare1", Rarity.RARE, 8, "rare fire attack", 40));
            masterRecruitDict.Add("simpleRare2", new Recruit("simpleRare2", Rarity.RARE, 8, "rare water attack", 40));
            masterRecruitDict.Add("simpleRare3", new Recruit("simpleRare3", Rarity.RARE, 8, "rare air attack", 40));
            masterRecruitDict.Add("simpleRare4", new Recruit("simpleRare4", Rarity.RARE, 8, "rare ice attack", 40));

            masterRecruitDict.Add("simpleEpic1", new Recruit("simpleEpic1", Rarity.EPIC, 12, "epic fire attack", 50));
            masterRecruitDict.Add("simpleEpic2", new Recruit("simpleEpic2", Rarity.EPIC, 12, "epic water attack", 50));
            masterRecruitDict.Add("simpleEpic3", new Recruit("simpleEpic3", Rarity.EPIC, 12, "epic air attack", 50));
            masterRecruitDict.Add("simpleEpic4", new Recruit("simpleEpic4", Rarity.EPIC, 12, "epic ice attack", 50));

            masterRecruitDict.Add("simpleLegendary1", new Recruit("simpleLegendary1", Rarity.LEGENDARY, 15, "legendary fire attack", 60));
            masterRecruitDict.Add("simpleLegendary2", new Recruit("simpleLegendary2", Rarity.LEGENDARY, 15, "legendary water attack", 60));

            //figure out rarity sets
            foreach (string name in masterRecruitDict.Keys)
            {
                if (masterRecruitDict[name].rarity == Rarity.COMMON)
                {
                    commonRecruits.Add(name);
                }
                else if (masterRecruitDict[name].rarity == Rarity.RARE)
                {
                    rareRecruits.Add(name);
                }
                else if (masterRecruitDict[name].rarity == Rarity.EPIC)
                {
                    epicRecruits.Add(name);
                }
                else if (masterRecruitDict[name].rarity == Rarity.LEGENDARY)
                {
                    legendaryRecruits.Add(name);
                }
            }

            
            setStartingRecruits();

        }

        public void setStartingRecruits()
        {
            
            //assign starting recruits --//may consider having extra default recruits to start
            for (int i = 0; i < 5; i++) //arbitrary, start with 5 random common recruits
            {
                recruitList.Add(getRandomRecruit(Rarity.COMMON));
            }

            string debugMsg = "initial recruits: \n";
            foreach (Recruit recruit in recruitList)
            {
                battleRecruits.Add(recruit);
                debugMsg += recruit.name + "\n";
            }
            Debug.Log(debugMsg);
        }

        public Recruit getRandomRecruit(Rarity rarity)
        {
            string temp = "error";
            if (rarity == Rarity.COMMON)
            {
                temp = commonRecruits[Random.Range(0, commonRecruits.Count)];
            }
            else if (rarity == Rarity.RARE)
            {
                temp = rareRecruits[Random.Range(0, rareRecruits.Count)];
            }
            else if (rarity == Rarity.EPIC)
            {
                temp = epicRecruits[Random.Range(0, epicRecruits.Count)];
            }
            else if (rarity == Rarity.LEGENDARY)
            {
                temp = legendaryRecruits[Random.Range(0,legendaryRecruits.Count)];
            }

            //return a copy, so the original in the list is not modified
            return masterRecruitDict[temp].getCopy();

        }

        public static Recruit stcRandRecruit(Rarity rarity)
        {
            return Instance.getRandomRecruit(rarity);
        }


        public void Restart()
        {
            recruitList.Clear();
            setStartingRecruits();
        }



        public List<Recruit> getSortedRecruits(string sortmode)
        {
            Recruit.sortMode = sortmode;
            recruitList.Sort();
            return recruitList;
        }


       


    }


    
}
