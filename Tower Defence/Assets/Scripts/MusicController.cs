using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    [Header("游戏控制器")]
    [SerializeField] private GameController gameController;
    [Header("音乐播放器")]
    [SerializeField] private AudioSource audioSource;
    [Header("点击音效")]
    [SerializeField] private AudioSource clickAudioSource;
    [Header("音乐片段")]
    [SerializeField] private List<AudioClip> audioClips;


    public float masterVolume { get; set; }
    public float musicVolume { get; set; }
    public float effectVolume { get; set; }

    private void Start()
    {
        gameController = GetComponent<GameController>();
        if (audioSource == null)
        {
            Debug.LogError("无音乐播放器");
            return;
        }
        if (clickAudioSource == null)
        {
            Debug.LogError("无点击音效播放器");
            return;
        }

        masterVolume = musicVolume = effectVolume = 1f;

        int index = UnityEngine.Random.Range(0, audioClips.Count);
        audioSource.clip = audioClips[index];
        audioSource.Play();

    }

    private void Update()
    {
        audioSource.volume = masterVolume * musicVolume;

        if (!audioSource.isPlaying)
        {
            int index = UnityEngine.Random.Range(0, audioClips.Count);
            audioSource.clip = audioClips[index];
            audioSource.Play();
        }
    }

    public void GetAllButtonAddListener()
    {
        //获取场景所有物体
        GameObject[] allObj = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
        int btnCount = 0;
        Button tmpButton;
        for (int i = 0; i < allObj.Length; i++)
        {
            tmpButton = allObj[i].GetComponent<Button>();
            if (tmpButton != null)
            {
                btnCount++;
                tmpButton.onClick.AddListener(() => ClickEffect());
            }
        }
        Debug.Log("<color=green> 当前场景共有：物体 </color>" + allObj.Length + "<color=green> 个，Button组件 </color>" + btnCount + "<color=green> 个 </color>");
    }
    public void ClickEffect()
    {
        clickAudioSource.Play();
    }

}
