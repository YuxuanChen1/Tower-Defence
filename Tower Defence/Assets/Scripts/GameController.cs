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
    public Difficulity difficulity { get; set; }
    public float masterVolume { get; set; }
    public float musicVolume { get; set; }
    public float effectVolume { get; set; }


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }



}
