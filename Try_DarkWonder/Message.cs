using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Message : MonoBehaviour{

    private static Message instance;
    public Text text;

    public static Message GetInstance()
    {
        return instance;
    }

    public void Awake()
    {
        instance = this;
        text.gameObject.SetActive(false);
    }

    public void ShowMessage(string message)
    {
        text.text = message;
        text.gameObject.SetActive(true);
    }

    public void ShowMessageTime(string message, float time)
    {
        ShowMessage(message);
        StartCoroutine(WaitForExitMessage(time));
    }

    public void ExitMessage()
    {
        text.gameObject.SetActive(false);
    }

    IEnumerator WaitForExitMessage(float time)
    {
        yield return new WaitForSeconds(time);
        text.gameObject.SetActive(false);
    }
}
