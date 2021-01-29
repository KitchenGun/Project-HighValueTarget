using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool GameStart=false;
    public bool WeaponFire = false;
    public int Mission;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        instance = this;
        this.transform.position = new Vector3(0, 0, 0);//
        Mission = SceneManager.sceneCount;
        
    }

    private void Update()
    {
        if(Mission==1)
        {
            GameStart = true;
        }
        else
        {
            GameStart = false;
        }
    }

}
