using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
/// <summary>
/// 在unity中新建GameObject，绑定该脚本，并将sheep的预设绑定在Sheep属性中
/// </summary>
public class SheepGroup : MonoBehaviour
{
    public static SheepGroup Instance { get; private set; } = null; //单例

    public GameObject Sheep;

    public int shepheldNum = 0;    //牧羊犬数-注入
    public int holeNum = 0;    //破洞数-注入

    public int sheepNum = 0;    //羊数
    public int sheepFur = 0;    //羊毛数
    public double motionIndex = 50;    //心情值
    public double maxMotionIndex = 100;    //最大心情值

    Dictionary<int, GameObject> sheeps = new Dictionary<int, GameObject>();    //存放sheep的对象

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

    /// <summary>
    /// 初始化
    /// </summary>
    void Init()
    {
        //开局按sheepNum出羊
        while (sheepNum > sheeps.Count)
        {
            //TODO: Sheep的生成需要调整,目前在-10，10随机生成
            sheeps.Add(sheeps.Count, Instantiate(Sheep, new Vector3(Random.Range(-5, 1), Random.Range(-3, 1)), Quaternion.identity));
        }
    }

    /// <summary>
    /// motion的变动
    /// </summary>
    /// <param name="c">剪羊数</param>
    /// <param name="k">杀羊数</param>
    public void MotionIndexChange(int c, int k)
    {
        maxMotionIndex = 100 - 30 * (shepheldNum / 10);
        if (c == 0 && k == 0 && motionIndex < maxMotionIndex)
        {
            ++motionIndex;
        }
        else
        {
            motionIndex *= (1 - 0.3 * c / sheepNum);
            motionIndex *= (1 - 1.0 * k / sheepNum);
        }
    }

    /// <summary>
    /// 羊数的变动
    /// </summary>
    /// <param name="k">杀羊数</param>
    public void SheepNumChange(int c,int k)
    {
        //注入数值
        shepheldNum = ShepheldGroup.Instance.shepheldNum;
        holeNum = HoleGroup.Instance.HoleNum;

        sheepNum -= k;    //杀羊影响
        if (sheepNum < 0)
        {
            sheepNum = 0;
            //throw new Exception("超限<0");
        }
        int tmp1 = (int)(sheepNum * 0.003 * motionIndex);    //心情影响
        int tmp2 = (int)(-holeNum * 0.2 * sheepNum * (1 - 0.5 * shepheldNum / 10));    //漏洞与牧羊犬影响
        sheepNum = sheepNum + tmp1 + tmp2;

        FurNumChange(c, k);//羊毛结算

        if (sheepNum > 1000) sheepNum = 1000;    //上限，怕蹦
        //多了生羊
        while (sheepNum > sheeps.Count)
        {
            //TODO: Sheep的生成需要调整,目前在-10，10随机生成
            sheeps.Add(sheeps.Count, Instantiate(Sheep, new Vector3(Random.Range(-5, 1), Random.Range(-3, 1)), Quaternion.identity));
        }

        //少了减羊
        while (sheepNum < sheeps.Count)
        {
            Destroy(sheeps[sheeps.Count - 1]);
            sheeps.Remove(sheeps.Count - 1);
        }
    }

    public void FurNumChange(int c,int k)
    {
        sheepFur += sheepNum - sheeps.Count;
        sheepFur -= c;
        if(k>c)
        {
            sheepFur -= k - c;
        }
    }
}