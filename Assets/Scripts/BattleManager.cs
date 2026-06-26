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
    public List<RotatingObject> rotatingObjects;
    public GameObject RotatingTemplate;


    public virtual void KnockOut(GameObject KOed)
    {
        Debug.Log(KOed.GetComponent<RotatingObject>().myRecruit.name + " was KOed!");
        rotatingObjects.Remove(KOed.GetComponent<RotatingObject>());
        Destroy(KOed);
    }

    public void PlaceRecruits()
    {
        float forwardAngle = (MaxActAngle + MinActAngle) / 2;
        float spacing = 36;
        for (int i = 0; i < 5; i++)
        {
            GameObject go = Instantiate(RotatingTemplate, wheelPosition, Quaternion.identity);
            go.tag = gameObject.tag;
            RotatingObject r = go.GetComponent<RotatingObject>();
            float angle = forwardAngle + spacing + spacing*2*i;
            r.applyStartingStuff(wheelPosition,xRadius,yRadius,angle,MinActAngle,MaxActAngle);
            r.ApplyRecruit(recruitList[i]);

            rotatingObjects.Add(r);
            r.RotateAmount(0);

        }
    }

    public RotatingObject getActive(string LorR)
    {
        RotatingObject closestLeft = null;
        RotatingObject closestRight = null;
        foreach (RotatingObject ro in rotatingObjects)
        {
            if (ro.inActiveAngle())
            {
                if (ro.angle - MinActAngle < MaxActAngle - ro.angle)
                {
                    closestRight = ro;
                }
                else
                {
                    closestLeft = ro;
                }
            }
        }
        if (LorR == "left")
        {
            return (closestLeft != null) ? closestLeft : closestRight;
        }
        else if (LorR == "right")
        {
            return (closestRight != null) ? closestRight : closestLeft;
        }
        else
        {
            Debug.Log("getActive method takes 'left' or 'right' only as a param!!");
            return closestRight;
        }
    }
}
