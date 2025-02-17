using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _pathContainer;

    private Transform[] _waypoints;
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
        _waypoints = new Transform[_pathContainer.childCount];

        for (int i = 0; i < _waypoints.Length; i++)
        {
            _waypoints[i] = _pathContainer.GetChild(i);
        }
    }

    private void GoToCurrentWaypoint()
    {
        Transform currentWaypoint = _waypoints[_currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position,
            _speed * Time.deltaTime);
    }

    private bool HasReachedCurrentWaypoint()
    {
        return transform.position == _waypoints[_currentWaypointIndex].position;
    }

    private void UpdateTargetWaypoint()
    {
        _currentWaypointIndex = ++_currentWaypointIndex % _waypoints.Length;

        Vector3 nextWaypointDirection = _waypoints[_currentWaypointIndex].position - transform.position;
        transform.forward = nextWaypointDirection;
    }
}