using System;
using System.Collections.Generic;
using UnityEngine;

// 옛날에 만든거
// string으로 찾아야해서 불편함. 나중에 enum 같은걸로 바꿔야함!!
public class SoundManager : MonoBehaviour
{
    Dictionary<string, AudioClip> clips;
    
    AudioSource audioSource;
    AudioSource bgmAudioSource;

    private static SoundManager instance;
    public static SoundManager GetInstance()
    {
        if (!instance)
        {
            GameObject soundManager = new GameObject();
            DontDestroyOnLoad(soundManager);
            soundManager.AddComponent<SoundManager>();
            soundManager.name = "SoundManager";
            
            instance = soundManager.GetComponent<SoundManager>();
            
            instance.ReadSounds();
        }
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            GameObject soundManager = this.gameObject;
            DontDestroyOnLoad(soundManager);
            instance = this;
            
            instance.ReadSounds();
        }
    }


    void ReadSounds()
    {
        AudioClip[] aClips = Resources.LoadAll<AudioClip>("Sounds");
        clips = new Dictionary<string, AudioClip>();

        for (int i = 0; i < aClips.Length; i++)
        {
            clips.Add(aClips[i].name, aClips[i]);
        }
        
        GameObject bgmObj = new GameObject();
        DontDestroyOnLoad(bgmObj);
        bgmAudioSource = bgmObj.AddComponent<AudioSource>();
        
        GameObject audioObj = new GameObject();
        DontDestroyOnLoad(audioObj);
        audioSource = audioObj.AddComponent<AudioSource>();
        
    }
    
    
    public void PlayOnce(string _name, float _volume =1.0f)
    {
        if(clips.ContainsKey(_name))
            audioSource.PlayOneShot(clips[_name], _volume);;
    }
    
  

    public void PlayBgm(string _name, bool _loop, float _volume)
    {
        StopBGM();
        if (clips.ContainsKey(_name))
        {
            bgmAudioSource.loop = _loop;
            bgmAudioSource.clip = clips[_name];
            bgmAudioSource.volume = _volume;
            bgmAudioSource.Play();
        }
        
    }
    
    public void StopSE()
    {
        audioSource.Stop();
    }
    
    public void StopBGM()
    {
        bgmAudioSource?.Stop();
    }

    public void StopAll()
    {
        StopBGM();
        StopSE();
    }
}
