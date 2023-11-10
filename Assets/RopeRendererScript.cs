using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RopeRendererScript : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public JsonParserScript parserScript;
    public LogicScript logic;
    public LevelManagerScript levelManager;

    private List<JsonParserScript.Point> pointList;
    private Vector3[] linePoints;
    private int pointsCount;

    void Start()
    {
        logic = GameObject.Find("Logic Manager").GetComponent<LogicScript>();
        StartCoroutine(SetParserOnLevelSelect());
    }


    public void InitializeOnParserSet(JsonParserScript newParser)
    {
        parserScript = newParser;
        InitializeRenderer();
    }
    private IEnumerator SetParserOnLevelSelect()
    {
        yield return new WaitUntil(() => levelManager.getCurrentLevel() > 0);
        InitializeOnParserSet(levelManager.jsonParser);
    }

    public void InitializeRenderer()

    {
        pointList = parserScript.getPointList();
        lineRenderer = GetComponent<LineRenderer>();

        pointsCount = pointList.Count;
        linePoints = new Vector3[pointsCount + 1];

        for (int i = 0; i < pointsCount; i++)
        {

            float x = pointList[i].x;
            float y = pointList[i].y;

            linePoints[i] = new Vector3(CameraScaleX(x), CameraScaleY(y), 0);
        }
        linePoints[pointsCount] = linePoints[0];

        lineRenderer.positionCount = pointsCount + 1;
        lineRenderer.SetPositions(linePoints);
        StartCoroutine(AnimateLine());
    }

    private IEnumerator AnimateLine()
    {
        lineRenderer.enabled = false;
        float segmentDuration = 1f;
        
        int expectedNumber = logic.getExpectedNumber();

        if (expectedNumber <= 1)
        {
            
            yield return new WaitUntil(() => logic.getExpectedNumber() > 2);
            lineRenderer.enabled = true;
        }

            for (int i = 0; i < pointsCount; i++)
            {
                float startTime = Time.time;

                Vector3 startPosition = linePoints[i];
                Vector3 endPosition = linePoints[i + 1];

                Vector3 pos = startPosition;
            bool paused = false;

            while (pos != endPosition)
                {
                float t = (Time.time - startTime) / segmentDuration;
                pos = Vector3.Lerp(startPosition, endPosition, t);

                if (!paused) {
                    int currentPoint = i + 1;
                    int difference = expectedNumber - currentPoint;

                    if (difference < 2)
                    {
                        if (currentPoint == pointsCount)
                        {

                        }
                        else
                        {
                            paused = true;
                            yield return new WaitUntil(() => logic.getExpectedNumber() - currentPoint >= 2);
                        }
                    }
                    
                }

                    for (int j = i + 1; j < pointsCount + 1; j++)
                        lineRenderer.SetPosition(j, pos);

                    yield return null;
                }
            }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public float CameraScaleX(float x)
    {
        Camera mainCamera = Camera.main;
        float cameraWidth = mainCamera.orthographicSize * 2 * mainCamera.aspect;
        return (x / 1000) * cameraWidth - (cameraWidth / 2);
    }

    public float CameraScaleY(float y)
    {
        Camera mainCamera = Camera.main;
        float cameraHeight = mainCamera.orthographicSize * 2;
        return (1 - (y / 1000)) * cameraHeight - (cameraHeight / 2);
    }
}
