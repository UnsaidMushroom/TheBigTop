using UnityEngine;
using recruits;
using NUnit.Framework;
using System.Collections.Generic;

public class RecruitViewerWindow : MonoBehaviour
{
    public GameObject recruitTemplate;
    public GameObject content;
    public GameObject RecruitInspectionWindow;

    public int hOffset = 20;
    public int vOffset = 20;
    public int hPadding = 20;
    public int vPadding = 20;
    public int TemplateSize = 100;

    private List<GameObject> population;

    public static int selectedIndex;
    public Queue<GameObject> selecteds;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        population = new List<GameObject>();
        selecteds = new Queue<GameObject>();
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
        List<Recruit> recruits = RecruitManager.Instance.getSortedRecruits("rarity");
        recruits.Reverse();
        for (int i = 0; i < recruits.Count; i++) 
        {
            GameObject placed = Instantiate(recruitTemplate, content.transform);

            float xPos = hOffset + 0.5f * TemplateSize + (TemplateSize + hPadding) * (i % 3);
            float yPos = -(vOffset + 0.5f * TemplateSize + (TemplateSize + vPadding) * (i / 3));

            placed.GetComponent<RectTransform>().localPosition = new Vector3 (xPos, yPos, 0);
            //placed.GetComponent<RectTransform>().localPosition = new Vector3 (0, 0, 0);
            population.Add(placed);

            placed.GetComponent<RecruitButton>().setRecruit(recruits[i],i);
        }

        //modify content box
        float maxSize = 2*vOffset + (TemplateSize + vPadding) * ((recruits.Count / 3) + 1);
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(content.GetComponent<RectTransform>().sizeDelta.x, maxSize);



    }

    public void hide()
    {
        int max = population.Count;
        for (int i = 0; i < max; i++)
        {
            Destroy(population[i]);
        }
        population.Clear();
        gameObject.SetActive(false);
    }


    //public void SetDisplayRecruit(Recruit recruit) { displayedRecruit = recruit; }  
    public void displayRecruit()
    {
        Debug.Log("displaying a recruit: " + population[selectedIndex].GetComponent<RecruitButton>().myRecruit.name);
        GameObject prev;
        if (selecteds.TryDequeue(out prev)) { prev.GetComponent<RecruitButton>().UnselectMe(); }
        selecteds.Enqueue(population[selectedIndex]);
        population[selectedIndex].GetComponent<RecruitButton>().SelectMe();


        //open display window and give it the recruit
    }

}
