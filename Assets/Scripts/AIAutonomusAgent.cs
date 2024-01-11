using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAutonomusAgent : Agent
{
    public AIPerception perception = null;

    private void Update()
    {
        var gameObjects = perception.GetGameObjects();
        foreach(var go in gameObjects)
        {
            Debug.DrawLine(transform.position, go.transform.position, Color.red);
        }
    }
}
