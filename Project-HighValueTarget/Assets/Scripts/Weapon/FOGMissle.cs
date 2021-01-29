using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(CharacterController))]//이스크립트가 붙은 오브젝트는 필수적으로 캐릭터컨트롤러를 가진다.
public class FOGMissle : Missle
{
    
    private GameObject FOGMisslePrefeb;

    protected override void Awake()
    {
        base.Awake();
        
        base.MissleForce = 7000f;//fog 미사일 만의 힘값
        base.MissleMass=2f;//fog미사일의 중량
        StartCoroutine(StateMain(ThisWeaponState));//무기상태관리시작

    }
    public void Update()
    {
        Debug.Log(GameManager.instance.WeaponFire);
        Debug.Log(ThisWeaponState);
        Debug.Log(Input.touchCount);
        if (Input.touchCount>=1||Input.GetMouseButtonDown(0))
        {
            Debug.Log(ThisWeaponState);
            GameManager.instance.WeaponFire = true;
        }
    }


}
