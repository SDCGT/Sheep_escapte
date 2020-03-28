using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class HoleGroup : MonoBehaviour
{
    public static HoleGroup Instance { get; private set; } = null; //单例
    
    public GameObject Hole;
    public int HoleNum = 0;
    
    Dictionary<int, GameObject> fenches = new Dictionary<int, GameObject>();    //存放fenches的对象
    
    void Start()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            //Instance.Init();
            Destroy(gameObject);
        }
        
        //Init();
        //DontDestroyOnLoad(gameObject);    //防止restart时自身被销毁
    }

    /// <summary>
    /// 洞的变动
    /// </summary>
    /// <param name="n">补洞数</param>
    public void HoleNumChange(int n)
    {
        HoleNum += (int)(Math.Floor(0.01 * SheepGroup.Instance.sheepNum));
        HoleNum -= n;
        Debug.Log(HoleNum);
        //tmp为正，增洞
    }
}
