using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour
{
    public enum SceneType { MainMenu, GameScene }
        
    public void LoadScene(SceneType scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    //public void LoadScene(int sceneNumber)
    //{
    //    SceneManager.LoadScene(sceneNumber);
    //}

    public void Quit()
    {
        Application.Quit();
    }
}
