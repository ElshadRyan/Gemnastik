using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitAnimation : MonoBehaviour
{
    public bool wait;
    public bool notDone = false;

    public void AnimationWait()
    {
        notDone = true;
        wait = true;

    }
    public void OffAnimationWait()
    {
        notDone = false;
        wait = false;

    }
}
