using UnityEngine;
using recruits;

public class RecruitViewerWindow : MonoBehaviour
{
    public GameObject recruitTemplate;
    public GameObject content;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void populate()
    {
        gameObject.SetActive(true);

        //probably actually want to index so as to track distance...
        foreach (Recruit recruit in RecruitManager.Instance.getSortedRecruits("rarity"))
        {
            Instantiate(recruitTemplate, content.transform);
            //recruitTemplate.GetComponent<RectTransform>().position = 
        }


    }
}
