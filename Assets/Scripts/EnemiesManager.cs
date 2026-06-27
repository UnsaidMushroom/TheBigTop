using UnityEngine;
using System.Collections.Generic;
using recruits;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemiesManager : BattleManager
{
    public static EnemiesManager Instance;

    public static List<Encounter> encounters;
    public Encounter activeEncounter;

    public float halftimePerAttack = 0.2f;

    public int layersDeep = 0;
    public const float fifthWheel = 360 / 5f;
    public const float rotDegPerSecond = 50;

    public int combatReward = 0;
    public TextMeshProUGUI rewardtext1;
    public TextMeshProUGUI rewardtext2;

    public static int battlesFought;
    public const int battleMax = 7;
    public static Encounter FinalEncounter;


    public GameObject FinalWinScreen;

    public AudioSource winSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
       
        if (encounters == null)
        {
            LoadEncounters();
        }
        getNewEncounter();
        layersDeep = 0;
        BattleActive = true;
        combatReward = 0;
        ILostScreen.SetActive(false);

        StartCoroutine(longWait());
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


    public override void checkEliminated()
    {
        if (rotatingObjects.Count <= 0)
        {
            winSound.Play();
            if (battlesFought == battleMax)
            {
                FinalWinScreen.SetActive(true);
            }

            foreach (Recruit r in recruitList)
            {
                combatReward += r.sellValue();
            }
            MoneyManager.addFundsS(combatReward);

            rewardtext1.text = "Reward - $" + combatReward + "\nLevelUp!";
            rewardtext2.text = rewardtext1.text;

            Debug.Log("you have beaten the opponent!!!");
            BattleActive = false;

            if (battlesFought != battleMax)
            {
                ILostScreen.SetActive(true);
            }
            foreach (Recruit rec in RecruitManager.Instance.battleRecruits)
            {
                rec.levelUp(1);
            }
        }

    }

    public void ProceedToCarousel()
    {
        SceneManager.LoadScene("Carousel");
    }

    public void LoadEncounters()
    {
        encounters = new List<Encounter>();

        //prepare AttackPatterns for the encounters
        //options are: leftAttack,rightAttack,randAttack,
        //fastLeftAttack, slowLeftAttack, and the same for right and rand
        //shortWait, mediumWait, longWait,
        //spinLeft,spinRight, spinVeryLeft, spinVeryRight, spinSlightLeft, spinSlightRight
        //left and right are realtive to the center of the circle looking forward
        AttackPattern simplePattern = new AttackPattern("leftAttack","shortWait","rightAttack","spinLeft");
        AttackPattern simplePattern2 = new AttackPattern("rightAttack", "longWait", "leftAttack", "spinVeryLeft");
        AttackPattern simplePattern3 = new AttackPattern("spinVeryRight", "mediumWait", "rightAttack", "spinLeft");
        AttackPattern simplePattern4 = new AttackPattern("spinSlightLeft", "mediumWait", "rightAttack", "longWait", "rightAttack");
        AttackPattern simplePattern5 = new AttackPattern("spinVeryRight", "longWait", "rightAttack", "spinVeryLeft", "rightAttack");
        AttackPattern simplePattern6 = new AttackPattern("spinSlightRight", "mediumWait", "leftAttack", "spinLeft", "rightAttack");
        AttackPattern simplePattern7 = new AttackPattern("spinVeryRight", "shortWait", "rightAttack", "spinSlightRight", "rightAttack");



        AttackPattern RingMasterPattern = new AttackPattern("fastLeftAttack", "spinSlightRight", "fastRightAttack", "spinVeryRight", "shortWait", "leftAttack", "spinVeryLeft");

        //for now, only placing one encounter, really will have more
        encounters.Add(new Encounter("default", 1, RecruitManager.getRandomBattleRecruits(Rarity.COMMON, Rarity.COMMON, Rarity.RARE, Rarity.COMMON, Rarity.COMMON), simplePattern));
        encounters.Add(new Encounter("these names never show up anywhere", 1, RecruitManager.getRandomBattleRecruits(Rarity.COMMON, Rarity.COMMON, Rarity.RARE, Rarity.COMMON, Rarity.RARE), simplePattern2));
        encounters.Add(new Encounter("these names never show up anywhere", 1, RecruitManager.getRandomBattleRecruits(Rarity.EPIC, Rarity.COMMON, Rarity.RARE, Rarity.COMMON, Rarity.COMMON), simplePattern3));
        encounters.Add(new Encounter("these names never show up anywhere", 1, RecruitManager.getRandomBattleRecruits(Rarity.EPIC, Rarity.COMMON, Rarity.RARE, Rarity.RARE, Rarity.RARE), simplePattern4));
        encounters.Add(new Encounter("these names never show up anywhere", 1, RecruitManager.getRandomBattleRecruits(Rarity.EPIC, Rarity.COMMON, Rarity.RARE, Rarity.EPIC, Rarity.RARE), simplePattern5));
        encounters.Add(new Encounter("these names never show up anywhere", 1, RecruitManager.getRandomBattleRecruits(Rarity.EPIC, Rarity.COMMON, Rarity.RARE, Rarity.RARE, Rarity.EPIC), simplePattern6));
        encounters.Add(new Encounter("these names never show up anywhere", 1, RecruitManager.getRandomBattleRecruits(Rarity.EPIC, Rarity.COMMON, Rarity.RARE, Rarity.COMMON, Rarity.RARE), simplePattern7));
        encounters.Add(new Encounter("these names never show up anywhere", 1, RecruitManager.getRandomBattleRecruits(Rarity.RARE, Rarity.COMMON, Rarity.RARE, Rarity.COMMON, Rarity.COMMON), simplePattern5));
        encounters.Add(new Encounter("these names never show up anywhere", 1, RecruitManager.getRandomBattleRecruits(Rarity.EPIC, Rarity.COMMON, Rarity.RARE, Rarity.RARE, Rarity.EPIC), simplePattern6));
        encounters.Add(new Encounter("these names never show up anywhere", 1, RecruitManager.getRandomBattleRecruits(Rarity.COMMON, Rarity.EPIC, Rarity.RARE, Rarity.RARE, Rarity.COMMON), simplePattern6));
        encounters.Add(new Encounter("these names never show up anywhere", 1, RecruitManager.getRandomBattleRecruits(Rarity.COMMON, Rarity.COMMON, Rarity.RARE, Rarity.RARE, Rarity.COMMON), simplePattern4));



        //final encounter is special, occurs as the 6th fight.
        FinalEncounter = new Encounter("The Ringmaster", 3, RecruitManager.getRandomBattleRecruits(Rarity.LEGENDARY, Rarity.LEGENDARY, Rarity.LEGENDARY, Rarity.LEGENDARY, Rarity.LEGENDARY), RingMasterPattern);

    }

    public void getNewEncounter()
    {
        battlesFought++;

        if (RecruitManager.Instance == null) //catch edge case
        {
            FindFirstObjectByType<RecruitManager>().Start(); // force recruit manager to load first
        }

        if (battlesFought == battleMax)
        {
            activeEncounter = FinalEncounter;
        }
        else
        {
            int rand = Random.Range(0, encounters.Count);
            activeEncounter = encounters[rand];
        }
        recruitList = new List<Recruit>();
        foreach(string rec in activeEncounter.encounterRecs)
        {
            Recruit r = RecruitManager.Instance.GetNewRecruit(rec);
            r.levelUp(battlesFought);
            recruitList.Add(r);

        }
        PlaceRecruits();
        

        
    }

    //call this at the beginning of battle and whenever the previous finishes. 
    public void BeginNextMove()
    {
        if (layersDeep == 0 && BattleActive)
        {
            StartCoroutine(activeEncounter.getNextMove());
        }

    }






    


    public IEnumerator leftAttack()
    {
        layersDeep++;

        yield return new WaitForSeconds(halftimePerAttack);
        getActive("left")?.Attack();
        yield return new WaitForSeconds(halftimePerAttack);

        layersDeep--;
        BeginNextMove();

        yield return null;
    }

    public IEnumerator rightAttack()
    {
        layersDeep++;

        yield return new WaitForSeconds(halftimePerAttack);
        getActive("right")?.Attack();
        yield return new WaitForSeconds(halftimePerAttack);

        layersDeep--;
        BeginNextMove();

        yield return null;
    }
    public IEnumerator randAttack()
    {
        layersDeep++;

        yield return new WaitForSeconds(halftimePerAttack);
        getActive("rand")?.Attack();
        yield return new WaitForSeconds(halftimePerAttack);

        layersDeep--;
        BeginNextMove();

        yield return null;
    }

    public IEnumerator fastLeftAttack()
    {
        layersDeep++;

        yield return new WaitForSeconds(halftimePerAttack/2);
        getActive("left")?.Attack();
        yield return new WaitForSeconds(halftimePerAttack/2);

        layersDeep--;
        BeginNextMove();

        yield return null;
    }

    public IEnumerator fastRightAttack()
    {
        layersDeep++;

        yield return new WaitForSeconds(halftimePerAttack / 2);
        getActive("right")?.Attack();
        yield return new WaitForSeconds(halftimePerAttack / 2);

        layersDeep--;
        BeginNextMove();

        yield return null;
    }
    public IEnumerator fastRandAttack()
    {
        layersDeep++;

        yield return new WaitForSeconds(halftimePerAttack / 2);
        getActive("rand")?.Attack();
        yield return new WaitForSeconds(halftimePerAttack / 2);

        layersDeep--;
        BeginNextMove();

        yield return null;
    }

    public IEnumerator slowLeftAttack()
    {
        layersDeep++;

        yield return new WaitForSeconds(halftimePerAttack * 2);
        getActive("left")?.Attack();
        yield return new WaitForSeconds(halftimePerAttack * 2);

        layersDeep--;
        BeginNextMove();

        yield return null;
    }

    public IEnumerator slowRightAttack()
    {
        layersDeep++;

        yield return new WaitForSeconds(halftimePerAttack * 2);
        getActive("right")?.Attack();
        yield return new WaitForSeconds(halftimePerAttack * 2);

        layersDeep--;
        BeginNextMove();

        yield return null;
    }
    public IEnumerator slowRandAttack()
    {
        layersDeep++;

        yield return new WaitForSeconds(halftimePerAttack * 2);
        getActive("rand")?.Attack();
        yield return new WaitForSeconds(halftimePerAttack * 2);

        layersDeep--;
        BeginNextMove();

        yield return null;
    }

    public IEnumerator shortWait()
    {
        layersDeep++;

        yield return new WaitForSeconds(halftimePerAttack/2);

        layersDeep--;
        BeginNextMove();

        yield return null;
    }

    public IEnumerator mediumWait()
    {
        layersDeep++;

        yield return new WaitForSeconds(halftimePerAttack);

        layersDeep--;
        BeginNextMove();

        yield return null;
    }
    public IEnumerator longWait()
    {
        layersDeep++;

        yield return new WaitForSeconds(halftimePerAttack * 2);

        layersDeep--;
        BeginNextMove();

        yield return null;
    }

    public IEnumerator spinLeft()
    {
        layersDeep++;

        //float debugTimer = Time.time;
        for (float i = 0; i < fifthWheel;)
        {
            float temp = rotDegPerSecond * Time.deltaTime;
            //debugTimer += Time.deltaTime;
            foreach (RotatingObject ro in rotatingObjects)
            {
                ro.RotateAmount(temp);
            }
            i += temp;
            //Debug.Log("degrees current spin " + i + "; time elapsed " + (Time.time - debugTimer));
            yield return null;

        }

        if (getActive() == null)
        {
            StartCoroutine(spinSlightLeft());
        }
        layersDeep--;
        BeginNextMove();

        yield return null;

    }
    public IEnumerator spinSlightLeft()
    {
        layersDeep++;


        for (float i = 0; i < fifthWheel/2; i++)
        {
            float temp = rotDegPerSecond * Time.deltaTime;
            //debugTimer += Time.deltaTime;
            foreach (RotatingObject ro in rotatingObjects)
            {
                ro.RotateAmount(temp);
            }
            i += temp;
            //Debug.Log("degrees current spin " + i + "; time elapsed " + (Time.time - debugTimer));
            yield return null;

        }

        if (getActive() == null)
        {
            StartCoroutine(spinSlightLeft());
        }
        layersDeep--;
        BeginNextMove();

        yield return null;

    }
    public IEnumerator spinVeryLeft()
    {
        layersDeep++;


        for (float i = 0; i < fifthWheel * 2; i++)
        {
            float temp = rotDegPerSecond * Time.deltaTime;
            //debugTimer += Time.deltaTime;
            foreach (RotatingObject ro in rotatingObjects)
            {
                ro.RotateAmount(temp);
            }
            i += temp;
            //Debug.Log("degrees current spin " + i + "; time elapsed " + (Time.time - debugTimer));
            yield return null;


        }

        if (getActive() == null)
        {
            StartCoroutine(spinLeft());
        }
        layersDeep--;
        BeginNextMove();

        yield return null;

    }

    public IEnumerator spinRight()
    {
        layersDeep++;


        for (float i = 0; i < fifthWheel; i++)
        {
            float temp = rotDegPerSecond * Time.deltaTime;
            //debugTimer += Time.deltaTime;
            foreach (RotatingObject ro in rotatingObjects)
            {
                ro.RotateAmount(temp);
            }
            i += temp;
            //Debug.Log("degrees current spin " + i + "; time elapsed " + (Time.time - debugTimer));
            yield return null;


        }

        if (getActive() == null)
        {
            StartCoroutine(spinSlightRight());
        }
        layersDeep--;
        BeginNextMove();

        yield return null;

    }
    public IEnumerator spinSlightRight()
    {
        layersDeep++;


        for (float i = 0; i < fifthWheel / 2; i++)
        {
            float temp = rotDegPerSecond * Time.deltaTime;
            //debugTimer += Time.deltaTime;
            foreach (RotatingObject ro in rotatingObjects)
            {
                ro.RotateAmount(temp);
            }
            i += temp;
            //Debug.Log("degrees current spin " + i + "; time elapsed " + (Time.time - debugTimer));
            yield return null;


        }

        if (getActive() == null)
        {
            StartCoroutine(spinSlightRight());
        }
        layersDeep--;
        BeginNextMove();

        yield return null;

    }
    public IEnumerator spinVeryRight()
    {
        layersDeep++;


        for (float i = 0; i < fifthWheel * 2; i++)
        {
            float temp = rotDegPerSecond * Time.deltaTime;
            //debugTimer += Time.deltaTime;
            foreach (RotatingObject ro in rotatingObjects)
            {
                ro.RotateAmount(temp);
            }
            i += temp;
            //Debug.Log("degrees current spin " + i + "; time elapsed " + (Time.time - debugTimer));
            yield return null;


        }

        if (getActive() == null)
        {
            StartCoroutine(spinRight());
        }
        layersDeep--;
        BeginNextMove();

        yield return null;

    }








}
