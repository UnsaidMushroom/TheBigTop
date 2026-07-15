using TMPro;
using UnityEngine;

/// <summary>
/// manages the money the player has and earns
/// </summary>
public class MoneyManager : MonoBehaviour
{
    
    public static MoneyManager instance;


    public static int funds = -1;
    public TextMeshProUGUI moneyText;

    /// <summary>
    /// initialize instance
    /// </summary>
    public void Start()
    {

        
        instance = this;
        //DontDestroyOnLoad(gameObject);
        if (funds == -1) { funds = 12; }
        updateMoneyText();
        
        
    }

    /// <summary>
    /// adds money.
    /// used at the carousel.
    /// </summary>
    /// <param name="amount"></param> the amount of money earned
    public void addFunds(int amount)
    {
        funds += amount;
        updateMoneyText();
    }

    /// <summary>
    /// statically adds money.
    /// used anywhere other than the carousel
    /// </summary>
    /// <param name="amount"></param>
    public static void addFundsS(int amount)
    {
        if (funds == -1) { funds = 12; }
        funds += amount;
    }

    /// <summary>
    /// spends amount of money, if able.
    /// </summary>
    /// <param name="amount"></param> the money attempted to be spent
    /// <returns></returns> true if enough money to be spent
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

    /// <summary>
    /// updates the money display at the carousel.
    /// </summary>
    public void updateMoneyText()
    {
        moneyText.text = "$" + funds;
    }

    /// <summary>
    /// resets values. 
    /// called between runs
    /// </summary>
    public static void Restart()
    {
        funds = -1;
        instance = null;
    }

}
