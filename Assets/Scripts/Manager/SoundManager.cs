using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    Dictionary<string, AudioClip> clips;
    
    AudioSource audioSource;
    AudioSource bgmAudioSource;

    public static SoundManager instance;
    public static SoundManager GetInstance()
    {
        if (!instance)
        {
            GameObject aManager = new GameObject();
            DontDestroyOnLoad(aManager);
            aManager.AddComponent<SoundManager>();
            aManager.name = "AudioManager";
            
            instance = aManager.GetComponent<SoundManager>();
            
            instance.ReadSounds();
        }
        return instance;
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
            bgmAudioSource.PlayOneShot(clips[_name], _volume);    
        }
        
    }
    
    public void StopSE()
    {
        audioSource.Stop();
    }
    
    public void StopBGM()
    {
        bgmAudioSource.Stop();
    }

    public void StopAll()
    {
        StopBGM();
        StopSE();
    }
}
