using recruits;
using System.Drawing;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class RotatingObject : Abstr_Damagable
{
    public InputActionAsset actions;
    public InputAction scroll;

    public float rotateSpeed = 2f;

    public Vector3 centerPos;
    //private Vector3 destPos;

    public float xRad; // this is the distance from the center pos to the furthest right/left of the ellipse
    public float yRad; // ""...                                                  ...top/bottom...       ..."
    public float angle; //this is the angle in the counter clockwise direction, starting from the positive x axis.
    //^ the angle will be spilt into 360/5 by whatever creates these.


    //the angles between which the object is active 
    public float minActiveAngle;
    public float maxActiveAngle;


    public Recruit myRecruit;

    /*
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //centerPos.x = -1;
        //centerPos.y = -5;
        //xRad = transform.position.x - centerPos.x;
        //yRad = transform.position.y - centerPos.y;

    }
    */

    public void applyStartingStuff(Vector3 centerPos, float xRad, float yRad, float angle, float minActAng, float maxActAng)
    {
        this.centerPos = centerPos;
        this.xRad = xRad;
        this.yRad = yRad;
        this.angle = angle;
        this.minActiveAngle = minActAng;
        this.maxActiveAngle = maxActAng;
    }

    // Update is called once per frame
    void Update()
    {

        //for now, set rotation
        RotateAmount(rotateSpeed * Time.deltaTime);
    }

    public void RotateAmount(float amount)
    {
        angle += amount;
        angle = angle % 360;
        //destPos.y = Mathf.Sin(Time.deltaTime) * xRad + centerPos.y;
        //destPos.x = Mathf.Cos(Time.deltaTime) * yRad + centerPos.x;
        //transform.position = Vector2.MoveTowards(this.transform.position, destPos, rotateSpeed * Time.deltaTime);
        float xPos = centerPos.x + xRad * Mathf.Cos(angle * Mathf.Deg2Rad);
        float yPos = centerPos.y + yRad * Mathf.Sin(angle * Mathf.Deg2Rad);
        transform.position = new Vector3(xPos, yPos, 0);
    }

    public override void Damage()
    {
        myRecruit.remainingHP -= 5;//really this should be taken from the attack dealing it
        if (myRecruit.remainingHP <= 0)
        {
            if (friendliesTags.Contains(myTag))
            {
                FriendliesManager.Instance.KnockOut(gameObject);
            }
            else if (enemiesTags.Contains(myTag))
            {
                EnemiesManager.Instance.KnockOut(gameObject);
            }
        }
       
    }




    public void ApplyRecruit(Recruit rec)
    {
        myRecruit = rec;
        //Debug.Log("Recieved recruit: " + rec.name);
        gameObject.GetComponent<SpriteRenderer>().sprite = myRecruit.sprite;
        Debug.Log("Recieved recruit: " + rec.name);

    }

}
