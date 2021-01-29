using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected enum WeaponState
    {
        LOCK = 0,     //잠금상태  
        READY,      //준비상태
        FIRE,       //발사중
        WAIT       //대기중
    }

    public float Damge;//무기데미지
    public float DamgeDistance;//무기피해범위

    public int Ammo;//현재탄약
    public int Ammo_Max;//최대탄약
    //게임매니저제작하여 싱글톤으로 관리예정
    public bool isWeaponStateChange;//무기상태변화체크
    
    protected WeaponState ThisWeaponState;

    protected IEnumerator StateMain(WeaponState state)
    {
        while (true)//스크립트 실행하고 계속 수행
        {
            isWeaponStateChange = false;//무기상태변환x로 전환
            ThisWeaponState = state;//함수와 같이 호출된 함수 이것으로 변환
            yield return StartCoroutine(state.ToString());//상태와 같은 이름의 함수 실행 각 자식 스크립트에서 수정가능하게 제작
        }
    }

    protected virtual void SetState(WeaponState state)
    {//상태 변환함수
        Debug.Log("set");
        isWeaponStateChange = true;//무기상태변환o상태로 변환
        ThisWeaponState = state;//상태지정
    }

    protected virtual IEnumerator LOCK()
    {//잠금 상태
        while (!isWeaponStateChange)
        {//무기교체가 아닐경우 실행
            
            if (GameManager.instance.GameStart)
            {//미션이 시작할경우
                SetState(WeaponState.READY);
                yield return null;
            }//상태는 준비상태
        }
        yield return null;
    }
    protected virtual IEnumerator READY()
    {//준비 상태

        isWeaponStateChange = false;
        while (!isWeaponStateChange)
        {//무기교체 아닐경우 실행
            if(GameManager.instance.WeaponFire)
            {
                Debug.Log("instance fire");
                SetState(WeaponState.FIRE);
                yield return null;
            }
        }
    }

    protected virtual IEnumerator FIRE()
    {//발사는 무기별로 다름으로 각 무기 스크립트에서 제어
        isWeaponStateChange = false;
        yield return null;
    }

    protected virtual IEnumerator WAIT()
    {//대기상태 무기 재장전 잔탄부족 과열 과 같은 미션중이지만 공격불가능 상황
        yield return null;
    }

}
