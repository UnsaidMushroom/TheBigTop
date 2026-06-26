using UnityEngine;

public class DoNothingAndDie : Abstr_Projectile
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        Debug.Log("Did nothing and died -- invalid attack");
        Destroy(gameObject);
    }

}
