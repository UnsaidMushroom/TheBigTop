using recruits;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Part of the UI
/// Used when the player is selecting their recruits for the upcoming battle
/// </summary>
public class BattlePrep : MonoBehaviour
{
    
    public Sprite blankSprite;

    public Image img1;
    public Image img2;
    public Image img3;
    public Image img4;
    public Image img5;

    /// <summary>
    /// select a recruit for battle, update images
    /// </summary>
    /// <param name="r"></param>
    public void applyRecruit(Recruit r)
    {
        img1.sprite = img2.sprite;
        img2.sprite = img3.sprite;
        img3.sprite = img4.sprite;
        img4.sprite = img5.sprite;
        img5.sprite = r.sprite;
    }

    /// <summary>
    /// clear all recruits from the selection
    /// </summary>
    public void clearRecruits()
    {
        img1.sprite = blankSprite;
        img2.sprite = blankSprite;
        img3.sprite = blankSprite;
        img4.sprite = blankSprite;
        img5.sprite = blankSprite;
    }



}
