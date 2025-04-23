using UnityEngine;

public class HouseTrigger : MonoBehaviour
{
    [SerializeField] private Alarm _alarm;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            _alarm.ChangeEnemyPlace();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            _alarm.ChangeEnemyPlace();
        }
    }
}
