using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    public GameObject Player, Arena;
    private float ArenaSize = 2f, CameraX;
    public float TranslationRatio = 15f;
    private float HorMove, XAngle, ZPos;
    public float MaxAngle = 4;
    public float First, Second, Third;

    // Start is called before the first frame update
    void Start()
    {
        HorMove = ArenaSize / TranslationRatio;
        XAngle = transform.eulerAngles.x;
        ZPos = transform.position.z;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        // Move camera opposite to the centre of the players
        CameraX = -Player.transform.position.x * HorMove;
        transform.position = new Vector3( CameraX, transform.position.y,
            ZPos - Mathf.Abs( Third * Mathf.Pow(CameraX,3) + Second * Mathf.Pow(CameraX,2) + First * CameraX ) );

        // Rotate camera to account for translation
        transform.eulerAngles = new Vector3( 0f, 180f, 0f );
        transform.Rotate( Vector3.down, MaxAngle * Player.transform.position.x );
        transform.Rotate( Vector3.right, XAngle );

    }
}
