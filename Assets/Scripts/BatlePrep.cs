using recruits;
using UnityEngine;
using UnityEngine.UI;


public class BattlePrep : MonoBehaviour
{

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
        img1.sprite = null;
        img2.sprite = null;
        img3.sprite = null;
        img4.sprite = null;
        img5.sprite = null;
    }



}
