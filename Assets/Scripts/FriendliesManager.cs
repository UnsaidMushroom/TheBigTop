using recruits;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FriendliesManager : BattleManager
{
    public static FriendliesManager Instance;
    public float scrollSensitivity = 10;


    //public InputActionAsset actions;
    public InputAction scroll;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        LoadFriendlies();
        scroll = InputSystem.actions.FindAction("Player/Scroll");
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
        Debug.Log("Scrolled: " +  scrollAmt + " units");

        foreach ( RotatingObject ro in rotatingObjects)
        {
            ro.RotateAmount(scrollAmt * scrollSensitivity * 0.1f);
        }
    }

    public override void KnockOut(GameObject KOed)
    {
        base.KnockOut(KOed);
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


}
