using recruits;
using UnityEngine;
using UnityEngine.UI;



public class Slots : MonoBehaviour
{

    public Image leftImage;
    public Image centerImage;
    public Image rightImage;


    public Sprite CherrySprite;
    public Sprite BellSprite;
    public Sprite SevenSprite;

    public int costPerSpin = 7;

    public enum rollOption
    {
        CHERRY,
        BELL,
        SEVEN
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void spinMachine()
    {

        if (!MoneyManager.instance.SpendFunds(costPerSpin)) { return; } //abort if not enough funds

        rollOption[] rolls = new rollOption[3];


        rolls[0] = getRandomRoll();
        rolls[1] = getRandomRoll();
        rolls[2] = getRandomRoll();




        assignSprite(leftImage, rolls[0]);
        assignSprite(centerImage, rolls[1]);
        assignSprite(rightImage, rolls[2]);



        Recruit returned = null;
        if (rolls[0] == rolls[1] && rolls[1] == rolls[2])
        {
            if (rolls[0] == rollOption.CHERRY)
            {
                Debug.Log("rolled 3 Cherries!, rare result!");
                //choose from rare options
                returned = RecruitManager.stcRandRecruit(Rarity.RARE);
            }
            else if (rolls[0] == rollOption.BELL)
            {
                Debug.Log("rolled 3 Bells!, epic result!");
                //choose from epic options
                returned = RecruitManager.stcRandRecruit(Rarity.EPIC);

            }
            else if (rolls[0] == rollOption.SEVEN)
            {
                Debug.Log("Rolled 3 Sevens!, Legendary result!");
                //choose from legendary options
                returned = RecruitManager.stcRandRecruit(Rarity.LEGENDARY);

            }
        }
        else
        {
            //all different
            Debug.Log("rolled different items, common result");
            //choose from common options
            returned = RecruitManager.stcRandRecruit(Rarity.COMMON);

        }
        Debug.Log("slots gave you a recruit: " + returned.name);
        RecruitManager.Instance.recruitList.Add(returned);



    }


    public rollOption getRandomRoll()
    {
        int MAX = 10;
        int rand = Random.Range(0, MAX);

        if (rand <= 5)
        {
            return rollOption.CHERRY;
        }
        else if (rand <= 8)
        {
            return rollOption.BELL;
        }
        else
        {
            return rollOption.SEVEN;
        }

    }

    public void assignSprite(Image image, rollOption option)
    {
        if (option  == rollOption.CHERRY)
        {
            image.sprite = CherrySprite;
        }
        else if (option == rollOption.BELL)
        {
            image.sprite = BellSprite;
        }
        else if (option == rollOption.SEVEN)
        {
            image.sprite = SevenSprite;
        }
    }

}
