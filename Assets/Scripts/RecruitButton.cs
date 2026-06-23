using recruits;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;

public class RecruitButton : Button
{
    public Recruit myRecruit = null;
    
    public int myIndex = -1;

    public GameObject selectedBox;
    

    public void setRecruit(Recruit recruit, int index)
    {
        myRecruit = recruit;
        myIndex = index;
        gameObject.GetComponent<Image>().sprite = recruit.sprite;
        Image[] possibleSelectBoxes = gameObject.GetComponentsInChildren<Image>();
        foreach (Image possibleSelectBox in possibleSelectBoxes)
        {
            if (possibleSelectBox.gameObject != gameObject) { selectedBox = possibleSelectBox.gameObject; break; }
        }


        Debug.Log("populating button: " +  recruit.name);
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = recruit.name;
        selectedBox.SetActive(false);

        //Button btn = this.GetComponent<Button>();
        //btn.onClick.AddListener(chooseMe);
        //gameObject.GetComponent<Image>().sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/" + "missing_sprite" + ".png", typeof(Sprite));

    }

    public void chooseMe()
    {
        RecruitViewerWindow.selectedIndex = myIndex;
        Debug.Log("you have clicked a recruit!");
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        chooseMe();
        base.OnPointerClick(eventData);
    }

    public void SelectMe()
    {
        selectedBox.SetActive(true);
    }

    public void UnselectMe()
    {
        selectedBox.SetActive(false);
    }

}
