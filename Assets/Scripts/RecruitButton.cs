using recruits;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;

/// <summary>
/// a special button for selecting recruits.
/// used to hold information and send it to the recruit viewer since the methods called by buttons can't have params
/// </summary>
public class RecruitButton : Button
{
    public Recruit myRecruit = null;
    
    public int myIndex = -1;

    public GameObject selectedBox;
    
    /// <summary>
    /// sets the recruit this button is responsible for
    /// </summary>
    /// <param name="recruit"></param> the recruit this button displays
    /// <param name="index"></param> the index in the recruit list this corresponds to
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
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = recruit.name + "\nlv" + recruit.level;
        selectedBox.SetActive(false);

        //Button btn = this.GetComponent<Button>();
        //btn.onClick.AddListener(chooseMe);
        //gameObject.GetComponent<Image>().sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/" + "missing_sprite" + ".png", typeof(Sprite));

    }

    /// <summary>
    /// informs recruitviewer this button's recruit is chosen
    /// </summary>
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

    /// <summary>
    /// show that this has been selected
    /// </summary>
    public void SelectMe()
    {
        selectedBox.SetActive(true);
    }

    /// <summary>
    /// show that this is no longer selected
    /// </summary>
    public void UnselectMe()
    {
        selectedBox.SetActive(false);
    }

}
