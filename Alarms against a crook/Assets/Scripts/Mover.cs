using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Path _pathContainer;

    private Transform[] _path;
    private int _currentWaypointIndex;

    private void Awake()
    {
        InitializeWaypoints();
    }

    private void Update()
    {
        GoToCurrentWaypoint();

        if (HasReachedCurrentWaypoint())
            UpdateTargetWaypoint();
    }

    private void InitializeWaypoints()
    {
        _path = new Transform[_pathContainer.Waypoints.Length];

        for (int i = 0; i < _path.Length; i++)
        {
            _path[i] = _pathContainer.Waypoints[i];
        }
    }

    private void GoToCurrentWaypoint()
    {
        Transform currentWaypoint = _path[_currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position,
            _speed * Time.deltaTime);
    }

    private bool HasReachedCurrentWaypoint()
    {
        return transform.position == _path[_currentWaypointIndex].position;
    }

    private void UpdateTargetWaypoint()
    {
        _currentWaypointIndex = ++_currentWaypointIndex % _path.Length;

        Vector3 nextWaypointDirection = _path[_currentWaypointIndex].position - transform.position;
        transform.forward = nextWaypointDirection;
    }
}