using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Loader))]
public class LoaderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Loader loader = (Loader)target;
        if (GUILayout.Button("Load"))
        {
            loader.loadFile(loader.filePath);
        }
        
        if (GUILayout.Button("Reset"))
        {
            loader.reset();
        }
    }
    
}