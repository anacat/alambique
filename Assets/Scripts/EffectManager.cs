using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EffectManager : MonoBehaviour
{
    public PlayerController owner;

    #region Slow effect variables
    [SerializeField]
    private float _slowAmount;
    [SerializeField]
    private float _slowDuration;
    private float _remainingSlowTime;
    private Coroutine _elapseSlowCoroutine;
    private bool _slowed;
    #endregion

    #region Speed up effect variables
    [SerializeField]
    private float _speedUpAmount;
    [SerializeField]
    private float _speedUpDuration;
    private float _remainingSpeedUpTime;
    private Coroutine _elapseSpeedUpCoroutine;
    private bool _speeding;
    #endregion

    #region Events
    public UnityEvent OnStartSlow;
    public UnityEvent OnEndSlow;
    public UnityEvent OnStartSpeedUp;
    public UnityEvent OnEndSpeedUp;
    #endregion

    private void Update()
    {
        var speedRate = 1.0f;
        if(_slowed)
        {
            speedRate -= _slowAmount;
        }
        if(_speeding)
        {
            speedRate += _speedUpAmount;
        }

        owner.currentSpeed = owner.speed * speedRate;
    }

    #region Slow methods
    public void ApplySlow()
    {
        _slowed = true;
        _remainingSlowTime += _slowDuration;
        if (_elapseSlowCoroutine == null)
        {
            OnStartSlow.Invoke();
            _elapseSlowCoroutine = StartCoroutine(ElapseSlow());
        }
    }

    private IEnumerator ElapseSlow()
    {
        while(true)
        {
            yield return new WaitForEndOfFrame();
            _remainingSlowTime -= Time.deltaTime;

            if(_remainingSlowTime <= 0)
            {
                _remainingSlowTime = 0.0f;
                _elapseSlowCoroutine = null;
                _slowed = false;
                OnEndSlow.Invoke();
                yield break;
            }
        }
    }
    #endregion

    #region Speed up methods
    public void ApplySpeedUp()
    {
        _speeding = true;
        _remainingSpeedUpTime += _speedUpDuration;
        if (_elapseSpeedUpCoroutine == null)
        {
            OnStartSpeedUp.Invoke();
            _elapseSpeedUpCoroutine = StartCoroutine(ElapseSpeedUp());
        }
    }

    private IEnumerator ElapseSpeedUp()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            _remainingSpeedUpTime -= Time.deltaTime;

            if (_remainingSpeedUpTime <= 0)
            {
                _remainingSpeedUpTime = 0.0f;
                _elapseSpeedUpCoroutine = null;
                _speeding = false;
                OnEndSpeedUp.Invoke();
                yield break;
            }
        }
    }
    #endregion
}
