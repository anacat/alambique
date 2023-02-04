using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUp : MonoBehaviour
{
    public enum Type
    {
        Gota,
        Buff,
        Debuff
    }

    public Type type;

    public float growthDuration = 1f;
    public Vector3 startScale = Vector3.one;
    public Vector3 endScale = Vector3.one * 2f;
    public float lifeTime;

    [HideInInspector]
    public PickUpController pickUpController;
    //Rigidbody rb;

    public float floorHeight;
    public float fallSpeed;
    bool falling;

    public UnityEvent grabbed;
    public UnityEvent used;
    public UnityEvent destroyed;
    public UnityEvent hitFloor;

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

            if (transform.position.y <= floorHeight)
            {
                falling = false;

                transform.position = new Vector3(transform.position.x,
                floorHeight,
                transform.position.z);


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
        if(collision.gameObject.tag == "Floor")
        {
            hitFloor.Invoke();
            if (type == Type.Gota)
            {
                //colocar aqui animação

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
        DestroyThiShit();
    }

    public void DestroyThiShit()
    {
        destroyed.Invoke();

        Destroy(gameObject);
    }
}
