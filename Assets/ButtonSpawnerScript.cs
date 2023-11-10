using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSpawnerScript : MonoBehaviour
{

    public GameObject buttonPrefab;
    public JsonParserScript parserScript;
    public LevelManagerScript levelManager;
    public int lvlIndex;

    void Start()
    {       
        for (int i = 0; i < parserScript.levelList.levels.Length; i++)
        {
            GameObject button = Instantiate(buttonPrefab, transform);
            button.transform.localPosition = new Vector3(i * 200, 400, 0);


            ButtonClickHandler clickHandler = button.GetComponent<ButtonClickHandler>();
            if (clickHandler != null)
            {
                clickHandler.levelManager = levelManager;
                clickHandler.level = i + 1;
            }

            Text buttonText = button.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.text = (i + 1).ToString();
            }
            else
            {
                Debug.LogError("ButtonPrefab does not contain a Text component.");

            }
        }
    }
}
