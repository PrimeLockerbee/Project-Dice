using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashScreenController : MonoBehaviour
{
    public SpriteRenderer emblemSprite;
    public SpriteRenderer copyrightSymbol;
    public Animator emblemAnimator;
    public AudioSource splashAudio;
    public float fadeDuration = 1f;
    public float waitAfterAnimation = 0.5f;
    public float soundDelay = 0.3f; // Delay before sound starts
    public string animationStateName = "SplashScreenAnim"; // replace with your actual state name
    public int nextSceneID = 1;

    void Start()
    {
        SetSpriteAlpha(0f);
        StartCoroutine(SplashSequence());
    }

    IEnumerator SplashSequence()
    {
        // Fade In
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            SetSpriteAlpha(alpha);
            yield return null;
        }
        SetSpriteAlpha(1f);

        // Trigger animation
        emblemAnimator.SetTrigger("Play");

        // Optional delay before sound plays
        yield return new WaitForSeconds(soundDelay);
        if (splashAudio != null)
            splashAudio.Play();

        // Wait until the animator enters the animation state
        yield return new WaitUntil(() =>
            emblemAnimator.GetCurrentAnimatorStateInfo(0).IsName(animationStateName)
        );

        // Then wait for the animation to finish
        float animLength = emblemAnimator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animLength + waitAfterAnimation);

        // Fade Out
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            SetSpriteAlpha(alpha);
            yield return null;
        }
        SetSpriteAlpha(0f);

        SceneManager.LoadScene(nextSceneID);
    }

    void SetSpriteAlpha(float alpha)
    {
        if (emblemSprite != null)
        {
            Color c = emblemSprite.color;
            c.a = alpha;
            emblemSprite.color = c;
        }
    }

    void SetCopyRightAlpha(float alpha)
    {
        if (copyrightSymbol != null)
        {
            Color c = copyrightSymbol.color;
            c.a = alpha;
            copyrightSymbol.color = c;
        }
    }
}
