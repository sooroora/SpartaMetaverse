using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AnimationSoundEvent : MonoBehaviour
{
    SoundManager soundManager;

    private void Start()
    {
        soundManager = SoundManager.GetInstance();
    }

    public void PlayStepSound()
    {
        soundManager.PlayOnce("step_1");
    }
    
}
