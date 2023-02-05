using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelManager : MonoBehaviour
{
    public int capGotas;
    public int currentGotas;

    //public GameObject barrelBar;

    public Vector3 zeroScale;
    public Vector3 maxScale;

    public Animator gageAnimator;

    public int beers;

    private void Start()
    {
        //barrelBar.transform.localScale = Vector3.Lerp(zeroScale, maxScale, 0);
    }

    // Start is called before the first frame update
    public void AddGotas(int toAdd)
    {
        currentGotas += toAdd;

        float scaleModifier = (float)currentGotas / (float)capGotas;

        //barrelBar.transform.localScale = Vector3.Lerp(zeroScale, maxScale, scaleModifier);

        if(currentGotas >= 10)
        {
            gageAnimator.SetTrigger("Fill3");

            currentGotas -= 10;

            ScoreBeer();
        }
        else if(currentGotas >= 6)
        {
            gageAnimator.SetTrigger("Fill2");
        }
        else if(currentGotas >= 3)
        {
            gageAnimator.SetTrigger("Fill1");
        }

    }

    void ScoreBeer()
    {
        beers++;
    }
}
