using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    public void Begin()
    {
        SceneManager.LoadScene("Battle");
    }
}
