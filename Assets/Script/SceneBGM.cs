using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBGM : MonoBehaviour
{
    public AudioClip bgmClip;

    private void Start()
    {
        SongHandler.instance.PlayBGM(bgmClip);
    }
}
