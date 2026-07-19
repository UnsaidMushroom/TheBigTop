using recruits;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// how a recruit battles.
/// the object that is used for the actual battling and spinning, etc.
/// </summary>
public class RotatingObject : Abstr_Damagable
{
    

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

    public Color activeColor = Color.white;
    public Color inactiveColor = Color.darkGray;
    public SpriteRenderer myRenderer;

    public Collider2D myCollider;

    public Recruit myRecruit;

    public static InputAction MouseClick;

    public bool MouseHeld = false;

    public SpriteRenderer HealthBar;
    public const float HPtoWidth = 1 / 50f;

    public float coolDownTimer;
    public float coolDownSecs = 0.25f;

    public AudioSource shootSound;
    public AudioSource damageSound;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        if (MouseClick == null)
        {
            MouseClick = InputSystem.actions.FindAction("Player/Attack");
        }
        myCollider = gameObject.GetComponent<Collider2D>();
        HealthBar.size = new Vector2(myRecruit.remainingHP * HPtoWidth, 0.1f);
        coolDownSecs = 0.25f;
    }

    /// <summary>
    /// initializes a rotating object.
    /// </summary>
    /// <param name="centerPos"></param> the center of the disk this rotates around
    /// <param name="xRad"></param> the distance from center to right/leftmost edge of disk arc
    /// <param name="yRad"></param> the distance from center to top/bottom edge of disk arc
    /// <param name="angle"></param> the angle this disk starts from, counter-clockwise from the positive x axis.
    /// <param name="minActAng"></param> the minimum angle for this to be considered in the active zone
    /// <param name="maxActAng"></param> the maximum angle for this to be considered in the active zone
    public void applyStartingStuff(Vector3 centerPos, float xRad, float yRad, float angle, float minActAng, float maxActAng)
    {
        
        this.centerPos = centerPos;
        this.xRad = xRad;
        this.yRad = yRad;
        this.angle = angle;
        this.minActiveAngle = minActAng;
        this.maxActiveAngle = maxActAng;

        myRenderer = gameObject.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (coolDownTimer > 0)
        {
            coolDownTimer -= Time.deltaTime;

        }
        //Debug.Log(MouseClick.ReadValue<float>());
        if (myTag == "Friendly")
        {
            if (MouseClick.ReadValue<float>() == 1f  ) 
            {
                if (!MouseHeld)
                {
                    //Debug.Log(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
                    if (myCollider.OverlapPoint(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()))){
                        Attack();
                    }
                    MouseHeld = true;
                }
            }
            else
            {
                MouseHeld = false;
            }
            //Debug.Log(MouseClick.ReadValue<float>() + 0.5f);
        }

    }

    /// <summary>
    /// rotates around the disk a given amount.
    /// </summary>
    /// <param name="amount"></param> the amount, in degrees, to rotate.
    public void RotateAmount(float amount)
    {
        angle += rotateSpeed * amount * Time.deltaTime;
        angle = angle % 360;
        angle = (angle < 0) ? angle + 360 : angle;
        //destPos.y = Mathf.Sin(Time.deltaTime) * xRad + centerPos.y;
        //destPos.x = Mathf.Cos(Time.deltaTime) * yRad + centerPos.x;
        //transform.position = Vector2.MoveTowards(this.transform.position, destPos, rotateSpeed * Time.deltaTime);
        float xPos = centerPos.x + xRad * Mathf.Cos(angle * Mathf.Deg2Rad);
        float yPos = centerPos.y + yRad * Mathf.Sin(angle * Mathf.Deg2Rad);
        transform.position = new Vector3(xPos, yPos, 0);
        applyColor();
    }

    /// <summary>
    /// applies color tint depending on status.
    /// </summary>
    public void applyColor()
    {
        myRenderer.color = (inActiveAngle()) ? activeColor : inactiveColor;
    }


    /// <summary>
    /// if no params, suffer some damage on contact.
    /// </summary>
    public override void Damage()
    {
        Damage(5);
    }

    /// <summary>
    /// suffers the recieved damage to this object
    /// </summary>
    /// <param name="dmg"></param> the damage suffered
    public override void Damage(int dmg)
    {
        if (inActiveAngle())
        {
            myRecruit.remainingHP -= dmg;//really this should be taken from the attack dealing it
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
            else
            {
                //apply damaged effects
                HealthBar.size = new Vector2(myRecruit.remainingHP * HPtoWidth, 0.1f);
                damageSound.Play();
            }
            
        }
    }

    /// <summary>
    /// determines if this is active or not.
    /// </summary>
    /// <returns></returns> true if this in in the active zone
    public bool inActiveAngle() => (angle < maxActiveAngle && angle > minActiveAngle);

    /// <summary>
    /// assigns this rotating object its recruit
    /// </summary>
    /// <param name="rec"></param> the recruit this is responsible for representing
    public void ApplyRecruit(Recruit rec)
    {
        myRecruit = rec;
        //Debug.Log("Recieved recruit: " + rec.name);
        gameObject.GetComponent<SpriteRenderer>().sprite = myRecruit.sprite;
        Debug.Log("Recieved recruit: " + rec.name);

    }

    /// <summary>
    /// this object fires an attack.
    /// </summary>
    public void Attack()
    {
        if (inActiveAngle() && coolDownTimer <= 0)
        {
            Debug.Log("Attacked");
            GameObject go = Instantiate(myRecruit.getAttack(), transform.position, Quaternion.identity);
            Abstr_Projectile ap = go.GetComponent<Abstr_Projectile>();
            ap.setDamage(myRecruit.damage);
            ap.setTag(myTag);
            shootSound.Play();
            coolDownTimer = coolDownSecs;
        }
    }

    
    
        
    
    

}
