using UnityEngine;

public class ButtonClickHandler : MonoBehaviour
{

    public LevelManagerScript levelManager;
    public int level;
    public PointSpawnerScript pointSpawner;


    public void OnButtonClick()
    {
        levelManager.SetCurrentLevel(level);
        pointSpawner.SetJsonParser(levelManager.jsonParser);
    }
}
