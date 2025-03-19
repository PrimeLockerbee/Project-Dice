using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour
{
    //public enum SceneType { MainMenu, GameScene }

    //public void LoadScene(SceneType scene)
    //{
    //    SceneManager.LoadScene(scene.ToString());
    //}

    [SerializeField] private GameObject _pauseMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //TEMP PAUSE

            PauseGame();
        }
    }

    public void LoadScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0f;

        _pauseMenu.SetActive(true);
    }

    public void UnPause()
    {
        Time.timeScale = 1.0f;

        _pauseMenu.SetActive(false);
    }

}
