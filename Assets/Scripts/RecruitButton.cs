using recruits;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;

public class RecruitButton : Button
{
    Recruit myRecruit = null;


    public void setRecruit(Recruit recruit)
    {
        myRecruit = recruit;
        gameObject.GetComponent<Image>().sprite = recruit.sprite;
        Debug.Log("populating button: " +  recruit.name);
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = recruit.name;
        //Button btn = this.GetComponent<Button>();
        //btn.onClick.AddListener(chooseMe);
        //gameObject.GetComponent<Image>().sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/" + "missing_sprite" + ".png", typeof(Sprite));

    }

    public void chooseMe()
    {
        RecruitViewerWindow.displayedRecruit = myRecruit;
        Debug.Log("you have clicked a recruit!");
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        chooseMe();
        base.OnPointerClick(eventData);
    }

}
