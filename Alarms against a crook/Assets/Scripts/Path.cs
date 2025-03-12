using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;

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

    public Transform[] Waypoints => _waypoints;
}