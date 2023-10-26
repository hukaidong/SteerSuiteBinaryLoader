using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrajectoryObject : MonoBehaviour
{
    private bool active;
    public Vector3[] positionRecord;
    public float agentSize = 0.3f;

    public void setPositionRecord(Vector3[] record)
    {
        positionRecord = record;
    }
    
    public void tick(int frame)
    {
        if (positionRecord.Length <= frame)
        {
            active = false;
            transform.position = new Vector3(0, 0, 0);
        }
        else
        {
            active = true;
            transform.position = positionRecord.ElementAt(frame);
        }
    }
    
    public bool isActive()
    {
        return active;
    }
    
    // Start is called before the first frame update
    void Start()
    {
    }
}
