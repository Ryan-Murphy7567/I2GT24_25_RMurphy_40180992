using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class dialoguewindow : MonoBehaviour
{ const string KalphaCode = "<color=#00000000>";
    public TMP_Text Text;
    private string currentText;
    private const float MaxTextTime = 0.1f;
    public static int TextSpeed = 2;

    private CanvasGroup Group;
    // Start is called before the first frame update
    void Start()
    {
Group = GetComponent<CanvasGroup>();
Group.alpha = 0;
    }

    public void show(string text)
    {
        Group.alpha = 1;
        currentText = text;
        StartCoroutine(DisplayText());
    }

    public void close()
    { StopAllCoroutines();
        Group.alpha = 0;
    }

    private IEnumerator DisplayText()
    {
        Text.text = "";
        string originalText = currentText;
        string displayedText = "";
        int alphaIndex = 0;
        foreach (char c in currentText.ToCharArray())
        { alphaIndex++;
            Text.text = originalText;
            displayedText = Text.text.Insert(alphaIndex,KalphaCode );
            Text.text = displayedText;
            yield return new WaitForSecondsRealtime(MaxTextTime/ TextSpeed);
        }
        yield return null;
    }
}
