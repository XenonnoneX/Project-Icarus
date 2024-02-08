using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyShowObject : MonoBehaviour
{
    [SerializeField] Image arrowImage;
    [SerializeField] Sprite arrowSpriteUp;
    [SerializeField] Sprite arrowSpriteRight;
    [SerializeField] Sprite arrowSpriteDown;
    [SerializeField] Sprite arrowSpriteLeft;

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
        if (key == "D")
        {
            arrowImage.sprite = arrowSpriteDown;
        }
        else if (key == "U")
        {
            arrowImage.sprite = arrowSpriteUp;
        }
        else if (key == "L")
        {
            arrowImage.sprite = arrowSpriteLeft;
        }
        else if (key == "R")
        {
            arrowImage.sprite = arrowSpriteRight;
        }
        arrowImage.color = Color.white;
    }

    public void ShowCorrect()
    {
        arrowImage.color = Color.green;
    }
}