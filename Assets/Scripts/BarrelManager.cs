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
    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _playerController.uiController.SetProgress(0);
    }

    public void AddGotas(int toAdd)
    {
        if(!_playerController.CanMove)
        {
            return;
        }

        currentGotas += toAdd;

        float scaleModifier = (float)currentGotas / (float)capGotas;
        _playerController.uiController.SetProgress(scaleModifier);

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
