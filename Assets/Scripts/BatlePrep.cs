using recruits;
using UnityEngine;
using UnityEngine.UI;


public class BattlePrep : MonoBehaviour
{
    public Sprite blankSprite;
    public Image img1;
    public Image img2;
    public Image img3;
    public Image img4;
    public Image img5;

    public void applyRecruit(Recruit r)
    {
        img1.sprite = img2.sprite;
        img2.sprite = img3.sprite;
        img3.sprite = img4.sprite;
        img4.sprite = img5.sprite;
        img5.sprite = r.sprite;
    }

    public void clearRecruits()
    {
        img1.sprite = blankSprite;
        img2.sprite = blankSprite;
        img3.sprite = blankSprite;
        img4.sprite = blankSprite;
        img5.sprite = blankSprite;
    }



}
