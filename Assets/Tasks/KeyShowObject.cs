using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyShowObject : MonoBehaviour
{
    [SerializeField] TMP_Text keyText;
    [SerializeField] Image bgImage;

    public void SetKey(KeyCode key)
    {
        if (key == KeyCode.DownArrow) SetKey("D");
        else if (key == KeyCode.UpArrow) SetKey("U");
        else if (key == KeyCode.LeftArrow) SetKey("L");
        else if (key == KeyCode.RightArrow) SetKey("R");
        else SetKey(key.ToString());
    }
    void SetKey(string key)
    {
        keyText.text = key;
        bgImage.color = Color.white;
    }

    public void ShowCorrect()
    {
        SetKey("");
        bgImage.color = Color.green;
    }
}