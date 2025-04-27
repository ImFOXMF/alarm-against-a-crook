using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Path _pathContainer;

    private int _currentWaypointIndex;

    private void Update()
    {
        GoToCurrentWaypoint();

        if (transform.position == _pathContainer.GetWaypoint(_currentWaypointIndex).position)
            UpdateTargetWaypoint();
    }

    private void GoToCurrentWaypoint()
    {
        Transform currentWaypoint = _pathContainer.GetWaypoint(_currentWaypointIndex);
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position,
            _speed * Time.deltaTime);
    }

    private void UpdateTargetWaypoint()
    {
        _currentWaypointIndex = ++_currentWaypointIndex % _pathContainer.WaypointCount;

        Vector3 nextWaypointDirection = _pathContainer.GetWaypoint(_currentWaypointIndex).position - transform.position;
        transform.forward = nextWaypointDirection;
    }
}