using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 在unity中新建GameObject，绑定该脚本，并将shepheld的预设绑定在Shepheld属性中
/// </summary>
public class ShepheldGroup : MonoBehaviour
{
    public static ShepheldGroup Instance { get; private set; } = null; //单例
    
    public GameObject Shepheld;
    public int shepheldNum = 0;
    
    Dictionary<int, GameObject> shephelds = new Dictionary<int, GameObject>();    //存放shepheld的对象
    
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

    public void ShepheldNumChange(int n)
    {
        int res = shepheldNum + n;
        if(res > 9 || res < 0)
            throw new Exception("超限0-9");
        //n为正，则购入牧羊犬
        while (n > 0)
        {
            //TODO: Shepheld的生成需要调整,目前在-10，10随机生成
            shephelds.Add(shephelds.Count, Instantiate(Shepheld, new Vector3(Random.Range(-5, 5), Random.Range(-5, 5)), Quaternion.identity));
            --n;
        }
        //n为负
        while (n < 0)
        {
            Destroy(shephelds[shephelds.Count - 1]);
            shephelds.Remove(shephelds.Count - 1);
            ++n;
        }
        shepheldNum = res;
    }
}