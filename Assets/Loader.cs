using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Object = UnityEngine.Object;

public class Loader : MonoBehaviour
{
    public bool VariableAgentSize = false;
    public int AgentSizeIndex = 1;
    public int AgentSizeOffset = 4;
    public SteerSuiteScenario target;
    public string filePath = "Assets/trajectory.txt";
    // Start is called before the first frame update
    void Start()
    {
        target = Object.FindObjectOfType<SteerSuiteScenario>();
    }


    public void loadFile(string filePath)
    {
        reset();
        
        FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read);
        int[] objectType = readSection<int>(fileStream);
        float[] objectInfo = readSection<float>(fileStream);
        processObstacles(objectType, objectInfo);
        
        float[] _parameter = readSection<float>(fileStream);
        int agentSizeIndex = AgentSizeIndex;
        
        while (fileStream.Position < fileStream.Length)
        {
            float[] agentPosition = readSection<float>(fileStream);
            var trajectory = processAgentPosition(agentPosition);
            var trajectoryObject = target.addTrajectory(trajectory);

            if (VariableAgentSize)
            {
                Debug.Log("Agent Size: " + _parameter[agentSizeIndex] + " at index " + agentSizeIndex + " of " + _parameter.Length + " parameters");
                trajectoryObject.agentSize = (0.3f * _parameter[agentSizeIndex]) + 0.3f;
                agentSizeIndex += AgentSizeOffset;
            }
            else
            {
                trajectoryObject.agentSize = 0.3f;
            }
        }
        
        fileStream.Close();
    }
    
    public void reset()
    {
        target.reset();
    }

    void processObstacles(int[] objectType, float[] objectInfo)
    {
        Queue<float> objectData = new Queue<float>();
        foreach (float data in objectInfo)
        {
            objectData.Enqueue(data);
        }
        
        foreach (int type in objectType)
        {
            target.addObstacle(type, objectData);
        }
    }
    
    Vector3[] processAgentPosition(float[] agentPosition)
    {
            Vector3[] trajectory = new Vector3[agentPosition.Length / 2]; 
            for (int i = 0; i < agentPosition.Length; i += 2)
            {
                float x = agentPosition[i];
                float z = agentPosition[i + 1];
                trajectory[i / 2] = new Vector3(x, 0, z);
            }

            return trajectory;
    }
    
    T[] readSection<T>(FileStream fileStream)
    {
        byte[] sizeBuffer = new byte[4];
        fileStream.Read(sizeBuffer);
        int sectionSize = BitConverter.ToInt32(sizeBuffer);
        T[] result = new T[sectionSize];
        byte[] resultBuffer = new byte[sectionSize * 4];
        fileStream.Read(resultBuffer);
        Buffer.BlockCopy(resultBuffer, 0, result, 0, resultBuffer.Length);
        
        return result;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
