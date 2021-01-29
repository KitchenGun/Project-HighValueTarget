using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Missle : Weapon
{
    public float MissleForce;//미사일힘
    public float MissleMass;//미사일중량
    public Vector3 MissleSpeed;//미사일속도
    public float Gravity = 1f;//중력

    public CharacterController MissleController;//플레이어 컨트롤러
    public Quaternion MissleAngles;//쿼터니언값
    public Vector3 MissleAnglesV = Vector3.zero;//백터값

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        ThisWeaponState = WeaponState.LOCK;
        MissleController = this.gameObject.GetComponent<CharacterController>();
    }


    protected override IEnumerator FIRE()
    {
        base.FIRE();
        Debug.Log("cp");
        while (!isWeaponStateChange)
        {
            if (GameManager.instance.WeaponFire)
            {
                MissleMovement();
            }
            else
            {
                SetState(WeaponState.WAIT);
                yield return null;
            }//추후에 발사 제어 필요 
        }
    }

    public void SpeedChanger()//속도를 리얼하게 변환
    {
        MissleAngles = this.transform.rotation;//호출 오브젝트의 각도
        //미사일 각도에 따른 움직임 식
        MissleSpeed = new Vector3(Mathf.Sin(MissleAngles.eulerAngles.x), Mathf.Sin(MissleAngles.eulerAngles.y) * Mathf.Cos(MissleAngles.eulerAngles.x),
                               Mathf.Cos(MissleAngles.eulerAngles.y) * Mathf.Cos(MissleAngles.eulerAngles.x));
        MissleSpeed = (MissleForce / MissleMass) * MissleSpeed;//각도에 따라 변하는 미사일의 속도
    }

    public virtual void MissleMovement()
    {
        SpeedChanger();
        Vector3 Direction = MissleSpeed;
        Vector3 Velocity = Direction * Time.deltaTime;
        Velocity.y -= Gravity;
        Velocity = Camera.main.transform.TransformDirection(Velocity);
        MissleController.Move(Velocity * Time.deltaTime);
    }
}
