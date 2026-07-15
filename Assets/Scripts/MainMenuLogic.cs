using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// handles logic in the main menu
/// </summary>
public class MainMenuLogic : MonoBehaviour
{
    /// <summary>
    /// starts the game.
    /// called by a button in the main menu
    /// </summary>
    public void Begin()
    {
        SceneManager.LoadScene("Battle");
    }
}
