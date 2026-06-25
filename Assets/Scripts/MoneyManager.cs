using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    
    public static MoneyManager instance;


    public int funds = 0;
    public TextMeshProUGUI moneyText;

    public void Start()
    {

        if (instance == null)
        {
            instance = this;
            funds = 12;
            updateMoneyText();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        
    }

    public void addFunds(int amount)
    {
        funds += amount;
        updateMoneyText();
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

}
