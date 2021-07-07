using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulity
{
    easy,
    normal,
    hard,
}
public class GameController : MonoBehaviour
{
    public Difficulity difficulity;

    private int lastSceneIndex;//���ڼ�¼��һ��

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public int GetSceneIndex()
    {
        return lastSceneIndex;
    }

    public void SetSceneIndex(int indexOfScene)
    {
        lastSceneIndex = indexOfScene;
    }

    public void GetButton()
    {
        GetComponent<MusicController>().GetAllButtonAddListener();
    }
}
