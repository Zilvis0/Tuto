using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonParserScript : MonoBehaviour
{

    public TextAsset jsonFile;
    public LevelList levelList;
    public LevelManagerScript levelManager;
    public int currentLevelIndex;
 

    [System.Serializable]
    public class Level
    {
        public float[] level_data;
    }

    public class LevelList
    {
        public Level[] levels;
    }

    public class Point
    {
        public float x;
        public float y;
    }

    public List<Point> pointsList = new List<Point>();

    void Start()
    {
        levelList = JsonUtility.FromJson<LevelList>(jsonFile.text);
    }

    public LevelList GetLevelLists() { 
        return levelList;
    }

    public List<Point> getPointList()
    {
        pointsList.Clear();
        currentLevelIndex = levelManager.getCurrentLevel() - 1;
    
            if (currentLevelIndex >= 0 && currentLevelIndex < levelList.levels.Length)
            {
                Level currentLevelData = levelList.levels[currentLevelIndex];

                for (int i = 0; i < currentLevelData.level_data.Length; i += 2)
                {
                    Point point = new Point();
                    point.x = currentLevelData.level_data[i];
                    point.y = currentLevelData.level_data[i + 1];
                    pointsList.Add(point);
                }
            }

        return pointsList;

    }
}
