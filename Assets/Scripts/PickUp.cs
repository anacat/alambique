using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUp : MonoBehaviour
{
    public UnityEvent grabbed;
    public UnityEvent used;

    public bool destroyedOnLand;

    public float growthDuration = 1f;
    public Vector3 startScale = Vector3.one;
    public Vector3 endScale = Vector3.one * 2f;

    Rigidbody rb;

    public enum Type
    {
        Gota,
        Buff,
        Debuff
    }

    public Type type;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        StartCoroutine(LerpScale());
    }

    public void Grab()
    {
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
            //colocar aqui animação

            DestroyThiShit();
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

        Fall();
    }

    void Fall()
    {
        rb.isKinematic = false;
    }

    public void DestroyThiShit()
    {
        Destroy(gameObject);
    }
}
