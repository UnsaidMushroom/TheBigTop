using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public static EnemiesManager Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void KnockOut(GameObject KOed)
    {
        Debug.Log(KOed.GetComponent<RotatingObject>().myRecruit.name + " was KOed!");
    }
}
