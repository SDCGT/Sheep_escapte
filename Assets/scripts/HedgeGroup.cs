using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 篱笆集合,也可以用tilemap，就需要重写
/// </summary>
public class HedgeGroup : MonoBehaviour
{
    public static HedgeGroup Instance { get; private set; } = null; //单例
    
    public GameObject Hedge;
    Dictionary<int, GameObject> Hedges = new Dictionary<int, GameObject>();    //存放hedge的对象
    List<int> broken = new List<int>();    //存放被破坏的值
    
    void Start()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            //Instance.Init();
            Destroy(gameObject);
        }
        
        Init();
        //DontDestroyOnLoad(gameObject);    //防止restart时自身被销毁
    }

    void Init()
    {   //定义边界

        //初始化篱笆地图
        //挨着边界生成
        //Hedges.Add(Hedges.Count, Instantiate());
    }
        
}
