using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } = null; //单例

    public Text SheepNum,FurNum, HoleNum, ShepHeldNum, goldNum, motion;
    public Text cutN, sellN, repairN, dogN;
    public Text goldChange;
    public Button turnButton,cutButton,sellButton,repairButton,dogButton,cutMinus,sellMinus,repairMinus,dogMinus;


    //回合输入
    public int kill, cut, repair, buyShepheld;
    public int goldcost;
    public int failTurnNum = 0;    //回合数超过该值盼负
    public int turnNum = 0;    //回合数
    public int posTurnNum = 0;    //达到目标的回合数，超过一定值游戏胜利
    public int gold = 0;    //金钱


    void Start()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Instance.Init();
            Destroy(gameObject);
        }

        Init();
        DontDestroyOnLoad(gameObject);    //防止restart时自身被销毁
    }

    void Update()
    {
        SheepNum.text = SheepGroup.Instance.sheepNum.ToString();
        HoleNum.text = HoleGroup.Instance.HoleNum.ToString();
        ShepHeldNum.text = ShepheldGroup.Instance.shepheldNum.ToString();
        goldNum.text = gold.ToString();
        motion.text = SheepGroup.Instance.motionIndex.ToString();
        FurNum.text = SheepGroup.Instance.sheepFur.ToString();
        goldcost = 30 * kill + 3 * cut - 50 * repair - 20 * buyShepheld;
        goldChange.text = (goldcost).ToString();

        /*cutN.text =cut.ToString();
        sellN.text = kill.ToString();
        repairN.text = repair.ToString();
        dogN.text = buyShepheld.ToString();*/

    }

    void Init()
    {
        //TODO：加载游戏组件，初始化值
        turnButton.onClick.AddListener(TurnProcess);
        cutButton.onClick.AddListener(CutAdd);
        sellButton.onClick.AddListener(SellAdd);
        repairButton.onClick.AddListener(RepairAdd);
        dogButton.onClick.AddListener(BuyShepheldAdd);
        cutMinus.onClick.AddListener(CutMinus);
        sellMinus.onClick.AddListener(SellMinus);
        repairMinus.onClick.AddListener(RepairMinus);
        dogMinus.onClick.AddListener(BuyShepheldMinus);
    }

    void CutAdd()
    {
        if(cut< SheepGroup.Instance.sheepFur)
        {
            cut++;
            cutN.text = cut.ToString();
        }
    }

    void RepairAdd()
    {
        if((50<=(gold + goldcost))&&repair<HoleGroup.Instance.HoleNum)
        {
            repair++;
            repairN.text = repair.ToString();
        }
        else if(50>(gold+goldcost))
        {
            AudioManager.Instance.NoticePlay(0);
        }
        else if ((repair>=HoleGroup.Instance.HoleNum))
        {
            AudioManager.Instance.NoticePlay(1);
        }
    }

    void SellAdd()
    {
        if(kill< SheepGroup.Instance.sheepNum)
        kill++;
        sellN.text = kill.ToString();
    }

    void BuyShepheldAdd()
    {
       if((gold+goldcost)>50)
       {
          buyShepheld++;
          dogN.text = buyShepheld.ToString();
       }

        if((gold+goldcost)<=50)
        {
            AudioManager.Instance.NoticePlay(0);
        }
    }

    void CutMinus()
    {
        if(cut>0)
        {
            cut--;
            cutN.text = cut.ToString();
        }
    }

    void RepairMinus()
    {
        if(repair>0)
        {
            repair--;
            repairN.text = repair.ToString();
        }
    }

    void SellMinus()
    {
        if(kill>0)
        {
            kill--;
            sellN.text = kill.ToString();
        }
    }

    void BuyShepheldMinus()
    {
        if(buyShepheld>0)
        {
            buyShepheld--;
            dogN.text = buyShepheld.ToString();
        }
    }

    /// <summary>
    /// 一个回合的过程,可以用来响应按钮事件，参数需要从游戏过程读取
    /// </summary>
    /// <param name="kill">杀羊数</param>
    /// <param name="cut">剪羊数</param>
    /// <param name="repair">补洞数</param>
    /// <param name="buyShepheld">买牧羊犬数</param>
    void TurnProcess()
    {
        //TODO：ui中读取用户选择，响应按钮事件
        gold += 30 * kill + 3 * cut - 50 * repair - 20 * buyShepheld;    //金钱变动
        HoleGroup.Instance.HoleNumChange(repair);    //洞状态修正
        ShepheldGroup.Instance.ShepheldNumChange(buyShepheld);    //牧羊犬群状态修正
        SheepGroup.Instance.MotionIndexChange(cut, kill);    //羊群心情修正
        SheepGroup.Instance.SheepNumChange(cut,kill);    //羊群、羊毛数量修正
        Judge();
        kill = 0;cut = 0;repair = 0;buyShepheld = 0;
        cutN.text = cut.ToString();
        sellN.text = kill.ToString();
        repairN.text = repair.ToString();
        dogN.text = buyShepheld.ToString();
    }


    /// <summary>
    /// 判断胜负
    /// </summary>
    void Judge()
    {
        ++turnNum;

        if (SheepGroup.Instance.sheepNum <= 0 || turnNum > failTurnNum)
        {
            GameFinish(false);
            return;
        }

        if (SheepGroup.Instance.sheepNum > 100)
        {
            ++posTurnNum;
        }
        else
        {
            posTurnNum = 0;
        }

        if (posTurnNum > 5)
        {
            GameFinish(true);
        }
    }

    /// <summary>
    /// 游戏结束的回调
    /// </summary>
    /// <param name="success">游戏是否胜利</param>
    void GameFinish(bool success)
    {

    }
}