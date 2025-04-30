using UnityEngine;
using System.Collections;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _soundSource;
    [SerializeField] private HouseTrigger _houseTrigger;
    [SerializeField] private float _maxVolume = 1f;
    [SerializeField] private float _volumeChangeSpeed = 1f;

    private int _enemiesInsideCount = 0;
    private Coroutine _volumeChangeCoroutine;

    private void Awake()
    {
        _soundSource.volume = 0f;
    }

    private void OnEnable()
    {
        _houseTrigger.EnemyEntered += OnEnemyEntered;
        _houseTrigger.EnemyExited += OnEnemyExited;
    }
    
    private void OnDisable()
    {
        _houseTrigger.EnemyEntered -= OnEnemyEntered;
        _houseTrigger.EnemyExited -= OnEnemyExited;
    }

    private void OnEnemyEntered()
    {
        _enemiesInsideCount++;
        
        UpdateSoundState();
    }

    private void OnEnemyExited()
    {
        if (_enemiesInsideCount > 0)
        {
            _enemiesInsideCount--;
        }
        
        UpdateSoundState();
    }

    private void UpdateSoundState()
    {
        if (_volumeChangeCoroutine != null)
        {
            StopCoroutine(_volumeChangeCoroutine);
        }

        _volumeChangeCoroutine = StartCoroutine(ChangeVolumeCoroutine(GetTargetVolume()));
    }

    private float GetTargetVolume()
    {
        return _enemiesInsideCount > 0 ? _maxVolume : 0f;
    }

    private IEnumerator ChangeVolumeCoroutine(float currentTargetVolume)
    {
        if (_enemiesInsideCount > 0 && !_soundSource.isPlaying)
        {
            _soundSource.Play();
        }

        while (_soundSource.volume != currentTargetVolume)
        {
            _soundSource.volume = Mathf.MoveTowards(
                _soundSource.volume,
                currentTargetVolume,
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