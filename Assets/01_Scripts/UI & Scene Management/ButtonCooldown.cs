using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonCooldown : MonoBehaviour
{
    public Button buttonToDisable;
    public float cooldownSeconds = 3f;

    public void TriggerActionWithCooldown()
    {
        if (!buttonToDisable.interactable) return;

        // Your database/API call here
        Debug.Log("Sending request...");

        // Start cooldown
        StartCoroutine(DisableButtonTemporarily());
    }

    private IEnumerator DisableButtonTemporarily()
    {
        buttonToDisable.interactable = false;
        yield return new WaitForSeconds(cooldownSeconds);
        buttonToDisable.interactable = true;
    }
}
