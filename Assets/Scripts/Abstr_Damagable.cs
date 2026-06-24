using UnityEngine;
using System.Collections.Generic;
public abstract class Abstr_Damagable : MonoBehaviour
{


    public static HashSet<string> friendliesTags;
    public static HashSet<string> enemiesTags;

    protected string myTag = null;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myTag = gameObject.tag;
        if (friendliesTags == null || enemiesTags == null)
        {
            initializeSets();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public abstract void Damage();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (myTag == null) { myTag = gameObject.tag; }

        string otherTag = collision.gameObject.tag;
        Debug.Log("collision! me: " + myTag + "; other: " + otherTag);

        if (!friendliesTags.Contains(myTag) && friendliesTags.Contains(otherTag))
        {
            Damage();
        }
        if (!enemiesTags.Contains(myTag) && enemiesTags.Contains(otherTag))
        {
            Damage();
        }
    }

    public static void initializeSets()
    {
        friendliesTags = new HashSet<string>();
        friendliesTags.Add("Friendly");

        enemiesTags = new HashSet<string>();
        enemiesTags.Add("Enemy");


    }
}

