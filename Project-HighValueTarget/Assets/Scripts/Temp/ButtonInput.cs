using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonInput : MonoBehaviour
{
    public bool RInput = false;
    public bool access = false;
    public Image recticle;

    private float RayDistance=20f;
    private RaycastHit UIHit;

    private float RangeRayDistance = 1500f;
    private RaycastHit OJHit;
    public Text RangeT;

    private float LoadingTime=2.0f;
    private float RemainTime = 0f;
    private float RecticleAngle;
    private float Range=0f;

    private void Start()
    {
        SightOut();
        recticle.fillAmount = 0;
    }
    private void Update()
    {
        Ray UIRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Ray OJRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if(Physics.Raycast(OJRay,out OJHit,RangeRayDistance))
        {
            if (OJHit.transform)
            {
                RayDistanceCheck();
            }
        }
        else
        {
            Range = 0f;
        }

        if(RInput==true)
        {
            RemainTime += Time.deltaTime;
            RecticleAngle = RemainTime / LoadingTime;
            recticle.fillAmount = RecticleAngle;
            if (RecticleAngle >= 1 || Input.GetButtonDown("Fire1"))
            {
                if (Physics.Raycast(UIRay, out UIHit, RayDistance))
                {
                    if(UIHit.transform.CompareTag("UI"))
                    { 
                        if(UIHit.transform.gameObject.name=="ChangeScene")
                        {
                            ChangeScene();
                        }
                    }
                }
                //작업완료시 실행할것
                SightOut();

            }
        }
        
    }
    void RayDistanceCheck()
    {
        Vector3 PTrans = this.transform.position;
        Vector3 OJHitTrans = OJHit.point;
        Range = Vector3.Distance(PTrans, OJHitTrans);
        Range = Mathf.Round(Range * 10) / 10;
        RangeT.text = Range.ToString();
    }


    public void SightEnter()
    {

        RemainTime = 0;
        RInput = true;  
    }

    public void SightOut()
    {
        RInput = false;
        RemainTime = 0;
        recticle.fillAmount = 0;   
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene(1);
    }

}
