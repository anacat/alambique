using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PickUp))]
public class RoottenBeer : MonoBehaviour
{
    private PickUp _pickUp;
    [SerializeField]
    private float _lockOnAngle = 60.0f;
    [SerializeField]
    private float _lockOnDistance = 5.0f;
    [SerializeField]
    private float _throwSpeed = 50.0f;
    private Transform _targetTransform;

    private void Start()
    {
        _pickUp = GetComponent<PickUp>();
    }

    public void ThrowAtPlayer()
    {
        var ownerLookDirection = _pickUp.pickUpController.transform.forward;
        var otherPlayerTransform = transform;
        if (MultiplayerController.instance.Player1().transform == _pickUp.pickUpController.transform)
        {
            otherPlayerTransform = MultiplayerController.instance.Player2().transform;
        }
        else
        {
            otherPlayerTransform = MultiplayerController.instance.Player1().transform;
        }

        var distance = Vector3.Distance(_pickUp.pickUpController.transform.position, otherPlayerTransform.position);
        var directionBetweenPlayers = otherPlayerTransform.position - _pickUp.pickUpController.transform.position;
        var angle = Vector3.Angle(ownerLookDirection, directionBetweenPlayers);

        _targetTransform = otherPlayerTransform;

        if (distance <= _lockOnDistance && angle <= _lockOnAngle)
        {
            StartCoroutine(MoveTowardsPlayer());
        }
        else
        {
            StartCoroutine(MoveInDirection(ownerLookDirection));
        }
        var playerController = _pickUp.pickUpController.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.moleAnimator.SetTrigger("throw");
        }
        _pickUp.pickUpController.NullPickUp();
    }

    private IEnumerator MoveTowardsPlayer()
    {
        while(true)
        {
            yield return new WaitForEndOfFrame();
            transform.position = Vector3.MoveTowards(transform.position, _targetTransform.position, _throwSpeed * Time.deltaTime);
            var distance = Vector3.Distance(transform.position, _targetTransform.position);
            Debug.Log(distance);
            if (distance <= 0.1f)
            {
                DoOnHit();
                yield break;
            }
        }
    }

    private IEnumerator MoveInDirection(Vector3 direction)
    {
        Invoke(nameof(DestroyThisShit), 20.0f);
        while(true)
        {
            yield return new WaitForEndOfFrame();
            transform.position += direction.normalized * Time.deltaTime * _throwSpeed;
            var hits = Physics.OverlapSphere(transform.position, 0.3f);
            for (var i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform == _targetTransform)
                {
                    DoOnHit();
                    yield break;
                }
            }
        }
    }

    private void DoOnHit()
    {
        var effectManager = _targetTransform.GetComponent<EffectManager>();
        if(effectManager != null)
        {
            effectManager.ApplySlow();
        }
        Destroy(gameObject);
    }

    private void DestroyThisShit()
    {
        _pickUp.DestroyThiShit(false);
    }
}
