using UnityEngine;
using UnityEngine.InputSystem;

public class QuitHandler : MonoBehaviour
{
    private Keyboard keyboard;

    private void OnEnable()
    {
        keyboard = Keyboard.current;
    }

    private void Update()
    {
        if (keyboard != null && keyboard.escapeKey.wasPressedThisFrame)
        {
            QuitApplication();
        }
    }

    public void QuitApplication()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
