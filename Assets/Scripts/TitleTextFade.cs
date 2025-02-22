using System.Collections;
using UnityEngine;
using TMPro;

public class TitleTextFade : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float fadeDuration = 2f;

    public void FadeOut()
    {
        StartCoroutine(FadeOutText());
    }

    IEnumerator FadeOutText()
    {
        Color textColor = textComponent.color;
        float startAlpha = textColor.a;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeDuration);
            textComponent.color = new Color(textColor.r, textColor.g, textColor.b, newAlpha);
            yield return null;
        }

        textComponent.gameObject.SetActive(false); // Disable text after fading out
    }
}

