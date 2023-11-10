using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointHandlerScript : MonoBehaviour
{

    public Sprite normalSprite;
    public Sprite clickedSprite;
    public Text numberText;
    public LogicScript logic;


    private Image buttonImage;
    private Animator textAnimator;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        textAnimator = GetComponent<Animator>();
        logic = GameObject.Find("Logic Manager").GetComponent<LogicScript>();

    }

    public void OnButtonClick()
    {
        if (IsCorrectButton()) 
        {
                
            ChangeSpriteToBlue();
            TriggerTextDisappearAnimation();

            logic.increaseExpectedNumber();

        }
    }

    public void ChangeSpriteToBlue()
    {
        if (buttonImage.sprite == normalSprite)
        {
            buttonImage.sprite = clickedSprite;
        }
    }

    private void TriggerTextDisappearAnimation()
    {
        textAnimator.SetTrigger("TextDisappear");
    }

    private bool IsCorrectButton() {
        return int.Parse(numberText.text) == logic.getExpectedNumber();
    }
}
