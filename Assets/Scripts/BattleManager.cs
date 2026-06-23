using System.Collections.Generic;
using recruits;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public Vector3 wheelPosition;
    public float xRadius;
    public float yRadius;
    public float MinActAngle;
    public float MaxActAngle;


    public List<Recruit> recruitList;


    public virtual void KnockOut(GameObject KOed)
    {
        Debug.Log(KOed.GetComponent<RotatingObject>().myRecruit.name + " was KOed!");
        Destroy(KOed);
    }
}
