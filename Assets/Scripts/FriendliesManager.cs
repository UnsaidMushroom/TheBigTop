using recruits;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// a battle manager that handles the friendly side
/// </summary>
public class FriendliesManager : BattleManager
{
    public static FriendliesManager Instance;
    public float scrollSensitivity = 10;

    public GameObject FightScreen;

    //public InputActionAsset actions;
    public InputAction scroll;

    public AudioSource lossSound;

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

    /// <summary>
    /// reads and applies the scroll rotation.
    /// </summary>
    public void applyScroll()
    {
        float scrollAmt = scroll.ReadValue<Vector2>().y;
        //Debug.Log("Scrolled: " +  scrollAmt + " units");

        foreach ( RotatingObject ro in rotatingObjects)
        {
            ro.RotateAmount(scrollAmt * scrollSensitivity * 0.1f);
        }
    }

    /// <summary>
    /// called when an friend has been knocked out
    /// </summary>
    /// <param name="KOed"></param> the friend knocked out
    public override void KnockOut(GameObject KOed)
    {
        base.KnockOut(KOed);
    }

    /// <summary>
    /// checks if your side has been fully knocked out
    /// if so, displays the loss screen
    /// </summary>
    public override void checkEliminated()
    {
        if (rotatingObjects.Count <= 0)
        {
            Debug.Log("You have lost !!!");
            BattleActive = false;
            ILostScreen.SetActive(true);
            lossSound.Play();
        }
    }

    /// <summary>
    /// returns player to the main menu screen, resets everything.
    /// called by a button on the loss screen
    /// </summary>
    public void ReturnToMainMenu()
    {
        MoneyManager.Restart();
        RecruitManager.Instance.Restart();
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// gathers and places the recruits that were selected for combat
    /// </summary>
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

    /// <summary>
    /// at start of battle, briefly display fight screen
    /// </summary>
    /// <returns></returns>
    public IEnumerator DisplayFightScreen()
    {
        FightScreen.SetActive(true);

        yield return new WaitForSeconds(.5f);

        FightScreen.SetActive(false);
    }


}
