using UnityEngine;
using System.Collections.Generic;
using recruits;

public class FriendliesManager : BattleManager
{
    public static FriendliesManager Instance;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        loadFriendlies();
    }

    // Update is called once per frame
    void Update()
    {
        //rotate the wheel!
    }

    public override void KnockOut(GameObject KOed)
    {
        base.KnockOut(KOed);
    }

    public void loadFriendlies()
    {
        recruitList = RecruitManager.Instance.battleRecruits;
        //place the recruits

    }


}
