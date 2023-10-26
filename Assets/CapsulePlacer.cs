using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsulePlacer : MonoBehaviour
{
    public SteerSuiteScenario scenario;
    public GameObject world;
    public GameObject capsulePrefab;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in scenario.transform)
        {
            GameObject capsule = Instantiate(capsulePrefab, child.position, Quaternion.identity);
            capsule.name = "Generated Capsule";
            capsule.transform.parent = world.transform;
            
            var tracker = capsule.GetComponent<TrajectoryTracker>();
            tracker.target = child.GetComponent<TrajectoryObject>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
