using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryTracker : MonoBehaviour
{
    public TrajectoryObject target;
    private MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        transform.position = target.transform.position;
        transform.localScale = new Vector3(target.agentSize, 1f, target.agentSize);
    }

    // Update is called once per frame
    void Update()
    {
        if (target.isActive())
        {
            meshRenderer.enabled = true;
            transform.position = target.transform.position;
        }
        else
        {
            meshRenderer.enabled = false;
        }
    }
}
