using UnityEngine;
using System.Collections;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _soundSource;
    [SerializeField] private float _maxVolume = 1f;
    [SerializeField] private float _volumeChangeSpeed = 1f;

    private bool _isEnemyInside = false;
    private Coroutine _volumeChangeCoroutine;

    private void Start()
    {
        _soundSource.volume = 0f;
    }

    public void ChangeEnemyPlace()
    {
        _isEnemyInside = !_isEnemyInside;
        
        if (_volumeChangeCoroutine != null)
        {
            StopCoroutine(_volumeChangeCoroutine);
        }
        
        _volumeChangeCoroutine = StartCoroutine(ChangeVolumeCoroutine());
    }

    private IEnumerator ChangeVolumeCoroutine()
    {
        float targetVolume = _isEnemyInside ? _maxVolume : 0f;
        
        if (_isEnemyInside && !_soundSource.isPlaying)
        {
            _soundSource.Play();
        }
        
        while (_soundSource.volume != targetVolume)
        {
            _soundSource.volume = Mathf.MoveTowards(
                _soundSource.volume, 
                targetVolume, 
                _volumeChangeSpeed * Time.deltaTime
            );
            
            yield return null;
        }
        
        if (_soundSource.volume == 0f && _soundSource.isPlaying)
        {
            _soundSource.Stop();
        }
    }
}