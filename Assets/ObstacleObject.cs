using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObject 
{
    public static GameObject GenerateObstacleType0(Queue<float> obstacleData)
    {
        // XY-aligned cuboid
        float xmin = obstacleData.Dequeue();
        float xmax = obstacleData.Dequeue();
        float zmin = obstacleData.Dequeue();
        float zmax = obstacleData.Dequeue();
        
        GameObject obstacle = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obstacle.transform.position = new Vector3((xmin + xmax) / 2, 0, (zmin + zmax) / 2);
        obstacle.transform.localScale = new Vector3(xmax - xmin, 1, zmax - zmin);
        
        return obstacle;
    }

    public static GameObject GenerateObstacleType1(Queue<float> obstacleData)
    {
        // Cylinder
        float x = obstacleData.Dequeue();
        float z = obstacleData.Dequeue();
        float r = obstacleData.Dequeue();
        
        GameObject obstacle = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        obstacle.transform.position = new Vector3(x, 0, z);
        obstacle.transform.localScale = new Vector3(r, 1, r);
        
        return obstacle;
    }

    public static GameObject GenerateObstacleType2(Queue<float> obstacleData)
    {
        // non-XY-aligned cuboid
        float x = obstacleData.Dequeue();
        float z = obstacleData.Dequeue();
        float w = obstacleData.Dequeue();
        float h = obstacleData.Dequeue();
        float deg = obstacleData.Dequeue();
        
        GameObject obstacle = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obstacle.transform.position = new Vector3(x, 0, z);
        obstacle.transform.localScale = new Vector3(w, 1, h);
        obstacle.transform.Rotate(new Vector3(0, deg, 0));
        
        return obstacle;
    }
}
