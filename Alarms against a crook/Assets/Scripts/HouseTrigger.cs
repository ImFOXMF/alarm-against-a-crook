using System;
using UnityEngine;

public class HouseTrigger : MonoBehaviour
{
    public event Action FindEnemyPosition;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            FindEnemyPosition?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            FindEnemyPosition?.Invoke();
        }
    }
}
