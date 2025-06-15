using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class InactivityReset : MonoBehaviour
{
    public float timeoutSeconds = 30f;

    private float timer;

    void Update()
    {
        bool isActive = false;

        // Check keyboard activity
        if (Keyboard.current != null && Keyboard.current.anyKey.isPressed)
            isActive = true;

        // Check mouse activity
        if (Mouse.current != null)
        {
            if (Mouse.current.delta.ReadValue().sqrMagnitude > 0)
                isActive = true;

            if (Mouse.current.leftButton.isPressed || Mouse.current.rightButton.isPressed || Mouse.current.middleButton.isPressed)
                isActive = true;

            if (Mouse.current.scroll.ReadValue().sqrMagnitude > 0)
                isActive = true;
        }

        // Check gamepad activity (fixing the error)
        if (Gamepad.current != null)
        {
            var controls = Gamepad.current.allControls;
            foreach (var control in controls)
            {
                if (control.IsPressed())
                {
                    isActive = true;
                    break;
                }
            }
        }

        // Check touchscreen activity
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
            isActive = true;

        if (isActive)
        {
            timer = 0f;
        }
        else
        {
            timer += Time.deltaTime;
            if (timer >= timeoutSeconds)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                timer = 0f; // reset timer
            }
        }
    }
}
