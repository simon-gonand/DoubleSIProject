using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveClearDisplay : MonoBehaviour
{
    public static WaveClearDisplay instance;

    [SerializeField]
    private Image panel;
    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private float timeDisplay;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void Display()
    {
        Timer.instance.StopTimer();
        StartCoroutine(DisplayLerp(150f/256f));
    }

    private IEnumerator DisplayLerp(float maxAlphaPanel)
    {
        float t = 0.0f;

        Color startColorPanel = panel.color;
        Color startColorText = text.color;

        while (t < 1.0f)
        {
            t += Time.deltaTime * 2.0f;

            panel.color = Color.Lerp(startColorPanel, new Color(panel.color.r, panel.color.g, panel.color.b, maxAlphaPanel), t);
            text.color = Color.Lerp(startColorText, new Color(text.color.r, text.color.g, text.color.b, 1), t);

            yield return null;
        }

        t = 0.0f;
        yield return new WaitForSeconds(timeDisplay);

        while (t < 1.0f)
        {
            t += Time.deltaTime * 2.0f;

            Color panelColor = panel.color;
            Color textColor = text.color;

            panelColor.a = Mathf.Lerp(maxAlphaPanel, 0, t);
            textColor.a = Mathf.Lerp(1, 0, t);

            panel.color = panelColor;
            text.color = textColor;

            yield return null;
        }

        DungeonManager.instance.NextWave();
    }
}
