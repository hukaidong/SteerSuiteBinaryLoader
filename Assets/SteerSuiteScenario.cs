using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SteerSuiteScenario : MonoBehaviour
{
    private int toc = 0;
    public int tickLimit = 0;
    public GameObject world;
    public TrajectoryObject addTrajectory(Vector3[] trajectory)
    {
        GameObject trajectoryObject = new GameObject("Trajectory");
        trajectoryObject.transform.parent = this.gameObject.transform;

        var trajectoryGizmo = trajectoryObject.AddComponent<DebugIcon>();
        trajectoryGizmo.herePath = "here-blue.png";
        
        var trajectoryObjectComponent = trajectoryObject.AddComponent<TrajectoryObject>();
        trajectoryObjectComponent.setPositionRecord(trajectory);
        trajectoryObjectComponent.tick(0);

        tickLimit = Mathf.Max(tickLimit, trajectory.Length);
        return trajectoryObjectComponent;
    }
    
    public void addObstacle(int obstacleType, Queue<float> obstacleData)
    {
        GameObject obstacleObject;
        switch (obstacleType)
        {
            case 0: obstacleObject = ObstacleObject.GenerateObstacleType0(obstacleData); break;
            case 1: obstacleObject = ObstacleObject.GenerateObstacleType1(obstacleData); break;
            case 2: obstacleObject = ObstacleObject.GenerateObstacleType2(obstacleData); break;
            default: throw new System.Exception("Unknown obstacle type!");
        }
        obstacleObject.name = "Generated Obstacle";
        obstacleObject.transform.parent = world.transform;
        
        var trajectoryGizmo = obstacleObject.AddComponent<DebugIcon>();
        trajectoryGizmo.herePath = "here-orange.png";
        
        var meshRenderer = obstacleObject.GetComponent<MeshRenderer>();
        // make it dark red
        meshRenderer.material.color = new Color(0.2f, 0.1f, 0.1f);
    }
    
    public void reset()
    {
        List<GameObject> toDestroy = new List<GameObject>();
        
        toDestroy.AddRange(from Transform child in world.transform select child.gameObject);
        toDestroy.AddRange(from Transform child in transform select child.gameObject);

        foreach (GameObject child in toDestroy)
        {
            GameObject.DestroyImmediate(child);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        toc = 0;
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (var trajectoryObject in GetComponentsInChildren<TrajectoryObject>())
        {
            trajectoryObject.tick(toc);
        }
        toc = (toc + 1) % tickLimit;
    }
}
