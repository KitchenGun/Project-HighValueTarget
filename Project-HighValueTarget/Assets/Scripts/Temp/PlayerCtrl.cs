using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float PForce = 4000f;
    public float PMass = 2f;
    public Vector3 PSpeed;//속도
    public float PGravity = 0.1f;

    private CharacterController Pcontroller;//플레이어 컨트롤러
    private Quaternion PAngles;//쿼터니언값
    private Vector3 PAnglesV=Vector3.zero;//백터값
    // Start is called before the first frame update
    void Start()
    {
        PAngles = this.transform.rotation;
        Pcontroller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        PAngles = this.transform.rotation;
        SpeedChanger();
        PlayerMovement();
    }
    
    void SpeedChanger()
    { 
        PSpeed = new Vector3(Mathf.Sin(PAngles.eulerAngles.x),Mathf.Sin(PAngles.eulerAngles.y) * Mathf.Cos(PAngles.eulerAngles.x),
                               Mathf.Cos(PAngles.eulerAngles.y) * Mathf.Cos(PAngles.eulerAngles.x));
        PSpeed = (PForce / PMass) * PSpeed;
    }
 
    void PlayerMovement()
    {
        
        Vector3 Direction =PSpeed;
        Vector3 Velocity = Direction *Time.deltaTime;
        Velocity.y -= PGravity;
        Velocity = Camera.main.transform.TransformDirection(Velocity);
        Pcontroller.Move(Velocity * Time.deltaTime);
    }
}
