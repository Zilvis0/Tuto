using System.ComponentModel;
using UnityEngine;

public class LevelManagerScript : MonoBehaviour
{

    public JsonParserScript jsonParser;
    public GameObject levelSelectOverlay;
    public LogicScript logic;
    public GameObject buttonSpawner;

    public int currentLevel = 0;

    void Start()
    {
        buttonSpawner = GameObject.FindGameObjectWithTag("buttons");
    }


    public int getCurrentLevel() { return currentLevel; }

    public void SetCurrentLevel(int newLevelIndex)
    {
        if (newLevelIndex >= 0 && newLevelIndex <= jsonParser.levelList.levels.Length)
        {
            currentLevel = newLevelIndex;
            levelSelectOverlay.SetActive(false);
            buttonSpawner.SetActive(false);

        }
        else
        {
            Debug.LogError("Invalid level index.");
        }
    }
}
