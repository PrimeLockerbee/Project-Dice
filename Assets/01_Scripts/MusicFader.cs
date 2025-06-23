using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MusicFader : MonoBehaviour
{
    public AudioSource audioSource; //Sleep hier je AudioSource naartoe in de Inspector
    public float fadeDuration = 1.5f; //Tijd in seconden voor de fade in/out

    private Coroutine currentFade; //Houdt bij of er al een fade bezig is

    void Awake()
    {
        //Zorgt dat er altijd een AudioSource is gekoppeld
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    //Start een fade naar een nieuwe audioclip
    public void FadeToNewTrack(AudioClip newClip)
    {
        if (currentFade != null)
            StopCoroutine(currentFade); //Stop eerdere fade
        currentFade = StartCoroutine(FadeOutIn(newClip)); //Start nieuwe fade
    }

    //Fade muziek uit en stop afspelen
    public void FadeOut()
    {
        if (currentFade != null)
            StopCoroutine(currentFade);
        currentFade = StartCoroutine(FadeOutOnly());
    }

    //Eerst uitfaden, dan nieuwe clip afspelen en infaden
    private IEnumerator FadeOutIn(AudioClip newClip)
    {
        yield return StartCoroutine(FadeVolume(1f, 0f)); //Fade out
        audioSource.clip = newClip;
        audioSource.Play(); // Start nieuwe clip
        yield return StartCoroutine(FadeVolume(0f, 1f)); //Fade in
    }

    //Alleen uitfaden en stoppen
    private IEnumerator FadeOutOnly()
    {
        yield return StartCoroutine(FadeVolume(1f, 0f));
        audioSource.Stop();
    }

    //Interpoleert het volume tussen twee waarden over de ingestelde duur
    private IEnumerator FadeVolume(float from, float to)
    {
        float time = 0f;
        while (time < fadeDuration)
        {
            audioSource.volume = Mathf.Lerp(from, to, time / fadeDuration);
            time += Time.deltaTime;
            yield return null; //Wacht één frame
        }
        audioSource.volume = to; //Zorg dat de eindwaarde precies klopt
    }
}
