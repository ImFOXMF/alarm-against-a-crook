using UnityEngine;
using System.Collections;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _soundSource;
    [SerializeField] private HouseTrigger _houseTrigger;
    [SerializeField] private float _maxVolume = 1f;
    [SerializeField] private float _volumeChangeSpeed = 1f;

    private bool _isEnemyInside = false;
    private Coroutine _volumeChangeCoroutine;

    private void Awake()
    {
        _soundSource.volume = 0f;
    }

    private void OnEnable()
    {
        _houseTrigger.FindEnemyPosition += ChangeEnemyPosition;
    }
    
    private void OnDisable()
    {
        _houseTrigger.FindEnemyPosition -= ChangeEnemyPosition;
    }

    private void ChangeEnemyPosition()
    {
        _isEnemyInside = !_isEnemyInside;

        ToogleAudioPlayback();
    }

    private void ToogleAudioPlayback()
    {
        if (_volumeChangeCoroutine != null)
        {
            StopCoroutine(_volumeChangeCoroutine);
        }

        _volumeChangeCoroutine = StartCoroutine(ChangeVolumeCoroutine(GetTargetVolume()));
    }

    private float GetTargetVolume()
    {
        return _isEnemyInside ? _maxVolume : 0f;
    }

    private IEnumerator ChangeVolumeCoroutine(float currentTargetVolume)
    {
        if (_isEnemyInside && !_soundSource.isPlaying)
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