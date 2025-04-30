using System;
using UnityEngine;

public class HouseTrigger : MonoBehaviour
{
    public event Action EnemyEntered;
    public event Action EnemyExited;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            EnemyEntered?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            EnemyExited?.Invoke();
        }
    }
}
