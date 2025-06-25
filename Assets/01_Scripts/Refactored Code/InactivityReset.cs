using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InactivityReset : MonoBehaviour
{
    public float timeoutSeconds = 30f;
    public GameObject inactivityWindow; // Assign your UI panel here

    private float timer;
    private bool windowShown = false;

    void Update()
    {
        if (windowShown) return; // Don't track input while window is open

        bool isActive = false;

        if (Keyboard.current != null && Keyboard.current.anyKey.isPressed)
            isActive = true;

        if (Mouse.current != null)
        {
            if (Mouse.current.delta.ReadValue().sqrMagnitude > 0 ||
                Mouse.current.leftButton.isPressed || Mouse.current.rightButton.isPressed ||
                Mouse.current.middleButton.isPressed ||
                Mouse.current.scroll.ReadValue().sqrMagnitude > 0)
                isActive = true;
        }

        if (Gamepad.current != null)
        {
            foreach (var control in Gamepad.current.allControls)
            {
                if (control.IsPressed())
                {
                    isActive = true;
                    break;
                }
            }
        }

        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
            isActive = true;

        if (isActive)
        {
            timer = 0f;
        }
        else
        {
            timer += Time.deltaTime;

            if (timer >= timeoutSeconds && !windowShown)
            {
                ShowInactivityWindow();
            }
        }
    }

    void ShowInactivityWindow()
    {
        if (inactivityWindow != null)
        {
            inactivityWindow.SetActive(true);
            windowShown = true;
        }
    }

    // Call this from the "Yes" button
    public void ContinueSession()
    {
        if (inactivityWindow != null)
        {
            inactivityWindow.SetActive(false);
        }
        windowShown = false;
        timer = 0f;
    }

    // Call this from the "No" button
    public void RestartSession()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
