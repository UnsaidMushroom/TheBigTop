using recruits;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class FriendliesManager : BattleManager
{
    public static FriendliesManager Instance;
    public float scrollSensitivity = 10;

    public GameObject FightScreen;

    //public InputActionAsset actions;
    public InputAction scroll;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        LoadFriendlies();
        ILostScreen.SetActive(false);
        scroll = InputSystem.actions.FindAction("Player/Scroll");
        BattleActive = true;
        StartCoroutine(DisplayFightScreen());
    }

    // Update is called once per frame
    void Update()
    {
        //rotate the wheel!
        applyScroll();
    }

    public void applyScroll()
    {
        float scrollAmt = scroll.ReadValue<Vector2>().y;
        //Debug.Log("Scrolled: " +  scrollAmt + " units");

        foreach ( RotatingObject ro in rotatingObjects)
        {
            ro.RotateAmount(scrollAmt * scrollSensitivity * 0.1f);
        }
    }

    public override void KnockOut(GameObject KOed)
    {
        base.KnockOut(KOed);
    }

    public override void checkEliminated()
    {
        if (rotatingObjects.Count <= 0)
        {
            Debug.Log("You have lost !!!");
            BattleActive = false;
            ILostScreen.SetActive(true);

        }
    }


    public void ReturnToMainMenu()
    {
        MoneyManager.Restart();
        RecruitManager.Instance.Restart();
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadFriendlies()
    { 
        if (RecruitManager.Instance == null) //catch edge case
        {
            FindFirstObjectByType<RecruitManager>().Start(); // force recruit manager to load first
        }

        recruitList = RecruitManager.Instance.battleRecruits;

        //place the rotating objects
        PlaceRecruits();
        
    }


    public IEnumerator DisplayFightScreen()
    {
        FightScreen.SetActive(true);

        yield return new WaitForSeconds(.5f);

        FightScreen.SetActive(false);
    }


}
