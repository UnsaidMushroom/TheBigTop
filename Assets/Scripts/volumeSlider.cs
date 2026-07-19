using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// controls the master volume.
/// hypothetically, we could make sublasses of audiosource each for BGM,SFX,etc and then have separate sliders each adjust a variable for that class.
/// </summary>
public class volumeSlider : MonoBehaviour
{

    public Slider volumeSlide;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            volumeSlide.value = PlayerPrefs.GetFloat("Volume");
        }
        else
        {
            PlayerPrefs.SetFloat("Volume", 1);
            volumeSlide.value = PlayerPrefs.GetFloat("Volume");
        }
        changeVol();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// updates the volume.
    /// </summary>
    public void changeVol()
    {
        AudioListener.volume = volumeSlide.value;
        PlayerPrefs.SetFloat("Volume", volumeSlide.value);
    }
}
