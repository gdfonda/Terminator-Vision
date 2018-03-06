using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextTyper : MonoBehaviour
{
    // Time taken for each letter to appear (The lower it is, the faster each letter appear)
    public float LetterDelay = 0.01f;
    // Message that will displays till the end that will come out letter by letter
    public string Message;
    // Text for the message to display
    private Text TextControl;

    private bool _isTyping = false;

    // Use this for initialization
    void Start()
    {
        TextControl = GetComponent<Text>();
    }

    public void TypeText(string text, bool refresh = true)
    {
        if (!_isTyping)
        {
            _isTyping = true;
            Message = text;
            if (refresh) TextControl.text = "";
            StartCoroutine(TypeText());
        }
    }

    IEnumerator TypeText()
    {
        foreach (char letter in Message.ToCharArray())
        {
            TextControl.text += letter;
            yield return 0;
            yield return new WaitForSeconds(LetterDelay);
        }
        _isTyping = false;
    }
}