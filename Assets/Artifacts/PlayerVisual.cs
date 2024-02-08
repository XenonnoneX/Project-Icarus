using UnityEngine;

public class PlayerVisual
{
    public GameObject gameObject;
    public Animator frontAnimator;
    public Animator rightAnimator;
    public Animator backAnimator;
    public Animator leftAnimator;
    
    public PlayerVisual(GameObject gameObject, Color visualColor)
    {
        this.gameObject = gameObject;

        Animator[] visualAnimators = gameObject.GetComponentsInChildren<Animator>(true);

        foreach (Animator visualAnimator in visualAnimators)
        {
            if (visualAnimator.gameObject.name == "Character_front") frontAnimator = visualAnimator;
            else if (visualAnimator.gameObject.name == "Character_right") rightAnimator = visualAnimator;
            else if (visualAnimator.gameObject.name == "Character_back") backAnimator = visualAnimator;
            else if (visualAnimator.gameObject.name == "Character_left") leftAnimator = visualAnimator;
        }

        SetSpriteColors(visualColor);
    }

    internal void SetSpriteColors(Color visualColor)
    {
        SpriteRenderer[] playerSpriteRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>(true);

        foreach (SpriteRenderer renderer in playerSpriteRenderers)
        {
            renderer.color = visualColor;
        }
    }

    internal void SetVisualDirection(Vector2 dir)
    {
        if (dir == Vector2.up) SetVisualDirection("Character_back");
        else if (dir == Vector2.down) SetVisualDirection("Character_front");
        else if (dir == Vector2.left) SetVisualDirection("Character_left");
        else if (dir == Vector2.right) SetVisualDirection("Character_right");
    }

    internal void SetVisualDirection(string activeSpriteName)
    {
        frontAnimator.gameObject.SetActive(activeSpriteName == "Character_front");
        rightAnimator.gameObject.SetActive(activeSpriteName == "Character_right");
        backAnimator.gameObject.SetActive(activeSpriteName == "Character_back");
        leftAnimator.gameObject.SetActive(activeSpriteName == "Character_left");
    }

    internal void SetIsWalking(bool value)
    {
        frontAnimator.SetBool("isWalking", value);
        rightAnimator.SetBool("isWalking", value);
        backAnimator.SetBool("isWalking", value);
        leftAnimator.SetBool("isWalking", value);
    }
}