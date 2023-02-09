using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public bool canMove;
    public float speed = 10.0f;
    public float currentSpeed;
    public float rotationSpeed = 10.0f;

    public float shoveStrength = 5.0f;
    public float currentShoveStrength;
    public float shoveCooldown = 1.0f;
    public float currentShoveCooldown;
    private bool canShove = true;
    public GameObject _shoveHitbox;
    public float shoveDuration = 0.1f;

    public Transform hands;
    public UIController uiController;
    
    private Transform pickedUpObject;

    private Vector2 _movementVector;
    private Rigidbody _rb;

    public Animator moleAnimator;
    public Color color;
    public Renderer meshRenderer;

    private MaterialPropertyBlock _mpb;
    private bool _goingBack;

    private void Awake() 
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        currentSpeed = speed;
        currentShoveStrength = shoveStrength;
        currentShoveCooldown = shoveCooldown;
    }    

    public void SetColor(Color c)
    {
        _mpb = new MaterialPropertyBlock();

        color = c;
        _mpb.SetColor("_MaskedColor", color);
        meshRenderer.SetPropertyBlock(_mpb);
    }

    private void OnMove(InputValue movementValue)
    {
       _movementVector = movementValue.Get<Vector2>();
    }

    private void OnShove(InputValue shoveValue)
    {
        if(shoveValue.isPressed && canShove)
        {
            StartCoroutine(DoShoveExecution());
        }
    }

    private void OnBack()
    {
        if(!_goingBack)
        {
            SceneManager.LoadScene(0);
        }
    }

    void FixedUpdate()
    {
        if(!CanMove)
        {
            return;
        }

        Vector3 movement = new Vector3(_movementVector.x, 0, _movementVector.y);

        if (movement.magnitude > 0.1f)
        {
            Quaternion rotation = Quaternion.LookRotation(movement);
            _rb.rotation = Quaternion.Slerp(_rb.rotation, rotation, Time.fixedDeltaTime * rotationSpeed);
        
            //transform.forward = Vector3.Slerp(transform.forward, movement, Time.fixedDeltaTime * rotationSpeed);
        }

        _rb.MovePosition(transform.position + movement * currentSpeed * Time.fixedDeltaTime);
        _rb.velocity = Vector3.zero;

        moleAnimator.SetBool("isWalking", _movementVector.magnitude > 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other != this && other.tag == "Player")
        {
            Debug.Log(other.name);
            Debug.Log("Hit another player");
            var rb = other.GetComponent<Rigidbody>();
            if(rb != null)
            {
                ApplyShove(rb, other.transform.position);
            }
            else
            {
                var rbp = other.GetComponentInParent<Rigidbody>();
                if(rbp != null)
                {
                    ApplyShove(rbp, other.transform.position);
                }
            }
        }
    }

    private IEnumerator DoShoveExecution()
    {
        canShove = false;
        _shoveHitbox.SetActive(true);
        yield return new WaitForSeconds(shoveDuration);
        _shoveHitbox.SetActive(false);

        yield return new WaitForSeconds(currentShoveCooldown - shoveDuration);

        canShove = true;
    }

    private void ApplyShove(Rigidbody rb, Vector3 otherPosition)
    {
        Debug.Log("Applying shove");
        var shoveDir = (otherPosition - transform.position).normalized;
        rb.velocity += shoveDir * currentShoveStrength;
        var pc = rb.GetComponent<PlayerController>();
        pc.canMove = false;
        StartCoroutine(SetMoveAgain(pc));
    }

    private IEnumerator SetMoveAgain(PlayerController pc)
    {
        yield return new WaitForSeconds(0.5f);
        pc.canMove = true;
    }

    public bool CanMove 
    { 
        get => canMove;
        set 
        {
            canMove = value;
        } 
    }
}
