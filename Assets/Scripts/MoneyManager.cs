using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    
    public static MoneyManager instance;


    public static int funds = -1;
    public TextMeshProUGUI moneyText;

    public void Start()
    {

        
        instance = this;
        //DontDestroyOnLoad(gameObject);
        if (funds == -1) { funds = 12; }
        updateMoneyText();
        
        
    }

    public void addFunds(int amount)
    {
        funds += amount;
        updateMoneyText();
    }


    public static void addFundsS(int amount)
    {
        if (funds == -1) { funds = 12; }
        funds += amount;
    }


    public bool SpendFunds(int amount)
    {
        if (funds < amount)
        {
            return false;
        }
        else
        {
            funds -= amount;
            updateMoneyText();
            return true;
        }
    }

    public void updateMoneyText()
    {
        moneyText.text = "$" + funds;
    }

    public static void Restart()
    {
        funds = -1;
        instance = null;
    }

}
