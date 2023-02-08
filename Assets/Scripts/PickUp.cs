using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUp : MonoBehaviour
{
    public enum Type
    {
        Gota,
        Slow,
        Speed,
        Strength,
        Confuse
    }

    public Type type;

    public float growthDuration = 1f;
    public Vector3 startScale = Vector3.one;
    public Vector3 endScale = Vector3.one * 2f;
    public float lifeTime;
    public GameObject spawnIndicator;

    [HideInInspector]
    public Coroutine DyingRoutine;
    public bool IsPicked = false;

    [HideInInspector]
    public PickUpController pickUpController;
    //Rigidbody rb;

    public float floorHeight;
    public float fallSpeed;
    bool falling;

    public UnityEvent fall;
    public UnityEvent grabbed;
    public UnityEvent used;
    public UnityEvent destroyed;
    public UnityEvent hitFloor;

    public int value;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //rb.isKinematic = true;

        StartCoroutine(LerpScale());
    }

    private void FixedUpdate()
    {
        if (falling)
        {
            transform.position = new Vector3(transform.position.x,
                transform.position.y - fallSpeed * Time.deltaTime,
                transform.position.z);

            if (transform.position.y < floorHeight)
            {
                falling = false;

                StartCoroutine(StartDying());

                transform.position = new Vector3(transform.position.x,
                floorHeight,
                transform.position.z);
                hitFloor.Invoke();

            }
        }
    }

    public void Grab()
    {
        transform.tag = "Untagged";

        falling = false;

        //rb.isKinematic = true;
        grabbed.Invoke();
    }

    public void Use()
    {
        used.Invoke();
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision!");
        if (collision.gameObject.tag == "Floor")
        {
            hitFloor.Invoke();
            if (type == Type.Gota)
            {
                //colocar aqui anima��o

               // DestroyThiShit();
            }
            else if (type != Type.Gota)
            {
                StartCoroutine(StartDying());
            }
        }
    }

    private IEnumerator LerpScale()
    {
        float startTime = Time.time;
        float endTime = startTime + growthDuration;

        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / growthDuration;
            transform.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
        }

        transform.localScale = endScale;

        falling = true;
        fall.Invoke();
    }

    private IEnumerator StartDying()
    {
        float currentLife = 0;

        while (currentLife < lifeTime)
        {
            currentLife += Time.deltaTime;
            yield return null;
        }

        if(transform.parent == null)
            DestroyThiShit(true);
    }

    public void DestroyThiShit(bool invoke)
    {
        if(invoke)
        {
            destroyed.Invoke();
        }

        Destroy(gameObject);
    }
}
