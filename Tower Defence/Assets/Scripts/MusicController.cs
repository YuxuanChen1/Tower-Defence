using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    [Header("��Ϸ������")]
    [SerializeField] private GameController gameController;
    [Header("���ֲ�����")]
    [SerializeField] private AudioSource audioSource;
    [Header("�����Ч")]
    [SerializeField] private AudioSource clickAudioSource;
    [Header("����Ƭ��")]
    [SerializeField] private List<AudioClip> audioClips;


    public float masterVolume { get; set; }
    public float musicVolume { get; set; }
    public float effectVolume { get; set; }

    private void Start()
    {
        gameController = GetComponent<GameController>();
        if (audioSource == null)
        {
            Debug.LogError("�����ֲ�����");
            return;
        }
        if (clickAudioSource == null)
        {
            Debug.LogError("�޵����Ч������");
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
        //��ȡ������������
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
        Debug.Log("<color=green> ��ǰ�������У����� </color>" + allObj.Length + "<color=green> ����Button��� </color>" + btnCount + "<color=green> �� </color>");
    }
    public void ClickEffect()
    {
        clickAudioSource.Play();
    }

}
