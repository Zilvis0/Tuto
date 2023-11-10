using UnityEngine;

public class LogicScript : MonoBehaviour
{

    public int expectedNumber = 1;

    public int getExpectedNumber() { return expectedNumber; }
    public void increaseExpectedNumber() { expectedNumber += 1; }
}