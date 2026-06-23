using System.Collections.Generic;
using recruits;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public FriendliesManager friends;
    public EnemiesManager enemies;

    public static BattleManager Instance;

    public List<Recruit> recruitList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
