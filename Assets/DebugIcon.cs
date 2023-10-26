using UnityEngine;

public class DebugIcon : MonoBehaviour
{
    public string herePath = "here-white.png";
    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position + Vector3.up * 0.6f, herePath, true);
    }
}