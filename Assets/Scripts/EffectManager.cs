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

    #region Strength effect variables
    [SerializeField]
    private float _strengthUpAmount;
    [SerializeField]
    private float _strengthUpDuration;
    private float _remainingStrengthUpTime;
    private Coroutine _elapsedStrengthUpCoroutine;
    private bool _isStrong;
    #endregion

    #region Confusion effect variables
    [SerializeField]
    private float _confusionDuration;
    private float _remainingConfusionTime;
    private Coroutine _elapsedConfusionCoroutine;
    private bool _isConfused;
    #endregion

    #region Events
    public UnityEvent OnStartSlow;
    public UnityEvent OnEndSlow;
    public UnityEvent OnStartSpeedUp;
    public UnityEvent OnEndSpeedUp;
    public UnityEvent OnStartStrengthUp;
    public UnityEvent OnEndStrengthUp;
    public UnityEvent OnStartConfusion;
    public UnityEvent OnEndConfusion;
    #endregion

    private void Update()
    {
        var speedRate = 1.0f;
        var strengthRate = 1.0f;
        if(_slowed)
        {
            speedRate -= _slowAmount;
        }
        if(_speeding)
        {
            speedRate += _speedUpAmount;
        }
        if(_isConfused)
        {
            speedRate *= -1.0f;
        }
        if(_isStrong)
        {
            strengthRate += strengthRate;
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("Applying confusion");
            ApplyConfusion();
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Applying strength up");
            ApplyStrengthUp();
        }
        owner.currentSpeed = owner.speed * speedRate;
        owner.currentShoveStrength = owner.shoveStrength * strengthRate;
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

    #region Strength up methods
    public void ApplyStrengthUp()
    {
        _isStrong = true;
        _remainingStrengthUpTime += _strengthUpDuration;
        if(_elapsedStrengthUpCoroutine == null)
        {
            OnStartStrengthUp.Invoke();
            _elapsedStrengthUpCoroutine = StartCoroutine(ElapseStrengthUp());
        }
    }

    private IEnumerator ElapseStrengthUp()
    {
        while(true)
        {
            yield return new WaitForEndOfFrame();
            _remainingSpeedUpTime -= Time.deltaTime;

            if(_remainingStrengthUpTime <= 0)
            {
                _remainingStrengthUpTime = 0.0f;
                _elapsedStrengthUpCoroutine = null;
                _isStrong = false;
                OnEndStrengthUp.Invoke();
                yield break;
            }
        }
    }
    #endregion

    #region Confusion methods
    public void ApplyConfusion()
    {
        _isConfused = true;
        _remainingConfusionTime += _confusionDuration;
        if(_elapsedConfusionCoroutine == null)
        {
            OnStartConfusion.Invoke();
            _elapsedConfusionCoroutine = StartCoroutine(ElapseConfusion());
        }
    }

    private IEnumerator ElapseConfusion()
    {
        while(true)
        {
            yield return new WaitForEndOfFrame();
            _remainingConfusionTime -= Time.deltaTime;

            if(_remainingConfusionTime <= 0)
            {
                _remainingConfusionTime = 0.0f;
                _elapsedConfusionCoroutine = null;
                _isConfused = false;
                OnEndConfusion.Invoke();
                yield break;
            }
        }
    }
    #endregion
}
