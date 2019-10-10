
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button playButton;

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
