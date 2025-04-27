using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    
    public Transform[] Waypoints => _waypoints;
    public int WaypointCount => _waypoints.Length;
    
    public Transform GetWaypoint(int index)
    {
        return Waypoints[index];
    }

#if UNITY_EDITOR
    [ContextMenu("Refresh Child Array")]
    private void RefreshChildArray()
    {
        int pointCount = transform.childCount;
        _waypoints = new Transform[pointCount];

        for (int i = 0; i < pointCount; i++)
            _waypoints[i] = transform.GetChild(i);
    }
#endif
}