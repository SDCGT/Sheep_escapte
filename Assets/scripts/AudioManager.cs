using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource noticeAudioSource;
    public AudioClip needGold,noHole;
    Dictionary<int, AudioClip> NoticeAudio = new Dictionary<int, AudioClip>();
    public static AudioManager Instance { get; private set; } = null; //单例

    private void Start()
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

    void Init()
    {
        NoticeAudio.Add(0, needGold);
        NoticeAudio.Add(1, noHole);
    }

    /// <summary>
    /// 提示框显示文本
    /// </summary>
    /// <param name="n">提示信息代码</param>
    public void NoticePlay(int n)
    {
        AudioSource.PlayClipAtPoint(NoticeAudio[n], new Vector3(1, 1, 1));
    }

}
