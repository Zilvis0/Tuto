using UnityEngine;
using UnityEngine.UI;

public class PointSpawnerScript : MonoBehaviour
{
    public GameObject pointPrefab;
    public JsonParserScript jsonParser;
    public GameObject pointSpawner;
    public GameObject container;


    private void Start()
    {
        container = GameObject.FindGameObjectWithTag("container");
    }

    public void SetJsonParser(JsonParserScript newParser)
    {
            jsonParser = newParser;  
            RenderObjects();
    }

    public void RenderObjects()
    {
            int pointNumber = 1;

            foreach (JsonParserScript.Point pointData in jsonParser.getPointList())
            {
                Vector3 spawnPosition = new Vector3(GameScaleX(pointData.x), GameScaleY(pointData.y), 0);
                GameObject pointObject = Instantiate(pointPrefab, spawnPosition, Quaternion.identity, container.transform);

                Text pointText = pointObject.GetComponentInChildren<Text>();

                if (pointText != null)
                {
                    pointText.text = pointNumber.ToString();
                }
                pointNumber++;
            }
    }

    public float GameScaleX(float x)
    {
        return x * Screen.width / 1000;
    }

    public float GameScaleY(float y)
    {
        return (1000 - y) * Screen.height / 1000;
    }
}


