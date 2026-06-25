using TMPro;
using UnityEngine;
using UnityEngine.UI;
using recruits;

public class DetailView : MonoBehaviour
{
    public Image recruitImage;
    public TextMeshProUGUI NameField;
    public TextMeshProUGUI MaxHPField;
    public TextMeshProUGUI DamageField;
    public TextMeshProUGUI DescriptionField;
    public GameObject SellButton;
    public TextMeshProUGUI SellText;


    public void applyRecruit(Recruit r)
    {
        recruitImage.sprite = r.sprite;
        NameField.text = r.name + "\nLv."+ r.level;
        MaxHPField.text = "MaxHP:" + r.maxHP;
        DamageField.text = "Damage: " + r.damage;
        DescriptionField.text = r.attackDescription;
        SellText.text = "Sell - $" + r.sellValue();
        if (RecruitManager.Instance.recruitList.Count > 5)
        {
            SellButton.SetActive(true);
        }
        else
        {
            SellButton.SetActive(false);
        }
    }
}
