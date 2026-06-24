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
    public GameObject RotatingTemplate;


    public virtual void KnockOut(GameObject KOed)
    {
        Debug.Log(KOed.GetComponent<RotatingObject>().myRecruit.name + " was KOed!");
        Destroy(KOed);
    }

    public void PlaceRecruits()
    {
        float forwardAngle = (MaxActAngle + MinActAngle) / 2;
        float spacing = 36;
        for (int i = 0; i < 5; i++)
        {
            GameObject go = Instantiate(RotatingTemplate, wheelPosition, Quaternion.identity);
            RotatingObject r = go.GetComponent<RotatingObject>();
            float angle = forwardAngle + spacing + spacing*2*i;
            r.applyStartingStuff(wheelPosition,xRadius,yRadius,angle,MinActAngle,MaxActAngle);
            r.ApplyRecruit(recruitList[i]);

        }
    }
}
