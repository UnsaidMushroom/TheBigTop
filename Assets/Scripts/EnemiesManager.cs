using UnityEngine;
using System.Collections.Generic;
using recruits;
using System.Collections;

public class EnemiesManager : BattleManager
{
    public static EnemiesManager Instance;

    public static List<Encounter> encounters;
    public Encounter activeEncounter;

    public float halftimePerAttack = 0.2f;

    public int layersDeep = 0;
    public const float fifthWheel = 360 / 5f;
    public const float rotDegPerSecond = 50;

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
        BeginNextMove();
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

        //prepare AttackPatterns for the encounters
        //options are: leftAttack,rightAttack,randAttack,
        //fastLeftAttack, slowLeftAttack, and the same for right and rand
        //shortWait,longWait,
        //spinLeft,spinRight, spinVeryLeft, spinVeryRight, spinSlightLeft, spinSlightRight
        //left and right are realtive to the center of the circle looking forward
        AttackPattern simplePattern = new AttackPattern("leftAttack","shortWait","rightAttack","spinLeft");

        //for now, only placing one encounter, really will have more
        encounters.Add(new Encounter("default", -1, new List<string>() { "simpleCommon1", "simpleRare1", "simpleEpic1", "simpleCommon2", "simpleCommon1" }, simplePattern));

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

    //call this at the beginning of battle and whenever the previous finishes. 
    public void BeginNextMove()
    {
        if (layersDeep == 0)
        {
            StartCoroutine(activeEncounter.getNextMove());
        }

    }


    


    public IEnumerator leftAttack()
    {
        layersDeep++;

        yield return new WaitForSeconds(halftimePerAttack);
        getActive("left").Attack();
        yield return new WaitForSeconds(halftimePerAttack);

        layersDeep--;
        BeginNextMove();

        yield return null;
    }

    public IEnumerator rightAttack()
    {
        layersDeep++;

        yield return new WaitForSeconds(halftimePerAttack);
        getActive("right").Attack();
        yield return new WaitForSeconds(halftimePerAttack);

        layersDeep--;
        BeginNextMove();

        yield return null;
    }
    public IEnumerator randAttack()
    {
        layersDeep++;

        yield return new WaitForSeconds(halftimePerAttack);
        getActive("rand").Attack();
        yield return new WaitForSeconds(halftimePerAttack);

        layersDeep--;
        BeginNextMove();

        yield return null;
    }

    public IEnumerator fastLeftAttack()
    {
        layersDeep++;

        yield return new WaitForSeconds(halftimePerAttack/2);
        getActive("left").Attack();
        yield return new WaitForSeconds(halftimePerAttack/2);

        layersDeep--;
        BeginNextMove();

        yield return null;
    }

    public IEnumerator fastRightAttack()
    {
        layersDeep++;

        yield return new WaitForSeconds(halftimePerAttack / 2);
        getActive("right").Attack();
        yield return new WaitForSeconds(halftimePerAttack / 2);

        layersDeep--;
        BeginNextMove();

        yield return null;
    }
    public IEnumerator fastRandAttack()
    {
        layersDeep++;

        yield return new WaitForSeconds(halftimePerAttack / 2);
        getActive("rand").Attack();
        yield return new WaitForSeconds(halftimePerAttack / 2);

        layersDeep--;
        BeginNextMove();

        yield return null;
    }

    public IEnumerator slowLeftAttack()
    {
        layersDeep++;

        yield return new WaitForSeconds(halftimePerAttack * 2);
        getActive("left").Attack();
        yield return new WaitForSeconds(halftimePerAttack * 2);

        layersDeep--;
        BeginNextMove();

        yield return null;
    }

    public IEnumerator slowRightAttack()
    {
        layersDeep++;

        yield return new WaitForSeconds(halftimePerAttack * 2);
        getActive("right").Attack();
        yield return new WaitForSeconds(halftimePerAttack * 2);

        layersDeep--;
        BeginNextMove();

        yield return null;
    }
    public IEnumerator slowRandAttack()
    {
        layersDeep++;

        yield return new WaitForSeconds(halftimePerAttack * 2);
        getActive("rand").Attack();
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
