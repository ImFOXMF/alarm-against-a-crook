using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _soundSource;
    [SerializeField] private float _maxVolume = 1f;
    [SerializeField] private float _volumeChangeSpeed = 1f;

    private bool _isEnemyInside;
    private float _targetVolume;

    private void Start()
    {
        _soundSource.volume = 0;
        _soundSource.Play();
    }

    private void Update()
    {
        _targetVolume = _isEnemyInside ? _maxVolume : 0f;
        _soundSource.volume =
            Mathf.MoveTowards(_soundSource.volume, _targetVolume, _volumeChangeSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            _isEnemyInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            _isEnemyInside = false;
        }
    }
}