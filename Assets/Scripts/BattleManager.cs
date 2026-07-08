using System.Collections.Generic;
using recruits;
using UnityEngine;

/// <summary>
/// A BattleManager manages part of the battle, either friendly or enemy
/// </summary>
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

    public static bool BattleActive = false;

    public GameObject ILostScreen;

    public AudioSource KOSound;

    /// <summary>
    /// Removes a rotating object when it is knocked out,
    /// called by the rotating object when HP <= 0.
    /// </summary>
    /// <param name="KOed"></param> the gameObject that was knocked out
    public virtual void KnockOut(GameObject KOed)
    {
        Debug.Log(KOed.GetComponent<RotatingObject>().myRecruit.name + " was KOed!");
        rotatingObjects.Remove(KOed.GetComponent<RotatingObject>());
        Destroy(KOed);
        KOSound.Play();
        checkEliminated();
    }

    /// <summary>
    /// checks if all recruits on this side have been KO'ed.
    /// this should maybe be abstract...
    /// </summary>
    public virtual void checkEliminated()
    {
        Debug.Log("battleManager called, but needed friendly or enemy");
    }

    /// <summary>
    /// Places all recruits as GameObjects on this side.
    /// </summary>
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

    /// <summary>
    /// Gets the rotating object that is active
    /// </summary>
    /// <param name="LorR"></param> the "left" or "right" or random of the actives to be gotten
    /// <returns></returns> the corresponding active rotating object
    public RotatingObject getActive(string LorR)
    {
        RotatingObject closestLeft = null;
        RotatingObject closestRight = null;
        if (LorR != "left" &&  LorR != "right")
        {
            int r = Mathf.FloorToInt(Random.value * 2);
            LorR = (r < 1) ? "left" : "right";
        }
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
            Debug.Log("something went wrong with the getActive Method");
            return null;
        }
    }

    /// <summary>
    /// gets a random active rotating object
    /// </summary>
    /// <returns></returns> either of the active rotating objects
    public RotatingObject getActive()
    {
        return getActive("rand");
    }
}
