using UnityEngine;
using System.Timers;
using System;
using System.Collections.Generic;

[Serializable]
public class ClipData
{
    public string name;
    public int id;
    public AudioClip clip;
}

public class AudioMgr : MonoBehaviour
{
    public static AudioMgr instance;
    private AudioSource audioSource;
    private AudioSource walkSource;
    [Header("通用声音")]
    public List<ClipData> clipList;
    [Header("走路声音")]
    public List<AudioClip> walkList;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
        audioSource = GetComponent<AudioSource>();
        walkSource = transform.Find("WalkUse").GetComponent<AudioSource>();
    }

    //播放音效
    public void PlayWalk()
    {
        var index = UnityEngine.Random.Range(0, walkList.Count - 1);
        //PlayAudio(walkList[index]);
        walkSource.PlayOneShot(walkList[index]);
    }
    public void PlayAudio(int id)
    {
        PlayAudio(FindClip(id));
    }
    public void PlayAudio(string name)
    {
        PlayAudio(FindClip(name));
    }
    //播放音乐
    public void PlayMusic(int id)
    {
        PlayMusic(FindClip(id));
    }
    public void PlayMusic(string name)
    {
        PlayMusic(FindClip(name));
    }
    //延迟播放音效
    public TimeCounter PlayeAudioWait(int id, float time)
    {
        return PlayeAudioWait(FindClip(id), time);
    }
    public TimeCounter PlayeAudioWait(string name, float time)
    {
        return PlayeAudioWait(FindClip(name), time);
    }
    //播放数次音效
    public TimeCounter PlayAudioCount(int id, float time, int num)
    {
        return PlayAudioCount(FindClip(id), time, num, 1);
    }
    public TimeCounter PlayAudioCount(string name, float time, int num)
    {
        return PlayAudioCount(FindClip(name), time, num, 1);
    }
    //循环播放音效
    public TimeCounter PlayAudioLoop(int id, float time)
    {
        return PlayAudioLoop(FindClip(id), time);
    }
    public TimeCounter PlayAudioLoop(string name, float time)
    {
        return PlayAudioLoop(FindClip(name), time, 1);
    }
    //停止音效的循环
    public void EndAudioTimer(TimeCounter counter)
    {
        counter.Kill();
    }
    //其他
    private AudioClip FindClip(int id)
    {
        foreach (var data in clipList)
        {
            if (data.id == id)
            {
                return data.clip;
            }
        }
        return null;
    }
    private AudioClip FindClip(string name)
    {
        foreach (var data in clipList)
        {
            if (data.name == name)
            {
                return data.clip;
            }
        }
        return null;
    }
    private void PlayAudio(AudioClip clip)
    {
        PlayAudio(clip, 1);
    }
    private void PlayAudio(AudioClip clip, float volumeScale)
    {
        if (!clip)
        {
            Debug.LogError("音效不存在！");
            return;
        }
        audioSource.PlayOneShot(clip, volumeScale);
    }
    private void PlayMusic(AudioClip clip)
    {
        PlayMusic(clip, 1);
    }
    private void PlayMusic(AudioClip clip, float volumeScale)
    {
        if (!clip)
        {
            Debug.LogError("音乐不存在！");
            return;
        }
        audioSource.PlayOneShot(clip, audioSource.volume * volumeScale);
    }
    private TimeCounter PlayeAudioWait(AudioClip clip, float time)
    {
        return PlayeAudioWait(clip, time, 1);
    }
    private TimeCounter PlayeAudioWait(AudioClip clip, float time, float volumeScale)
    {
        return MyTimer.instance.WaitTimer(time, () =>
        {
            PlayAudio(clip, volumeScale);
        });
    }
    private TimeCounter PlayAudioCount(AudioClip audioClip, float time, int num)
    {
        return PlayAudioCount(audioClip, time, num, 1);
    }
    private TimeCounter PlayAudioCount(AudioClip audioClip, float time, int num, float volumeScale)
    {
        return MyTimer.instance.StartTimer(time, num, () =>
        {
            PlayAudio(audioClip, volumeScale);
        });
    }
    private TimeCounter PlayAudioLoop(AudioClip audioClip, float time)
    {
        return PlayAudioCount(audioClip, time, 1);
    }
    private TimeCounter PlayAudioLoop(AudioClip audioClip, float time, float volumeScale)
    {
        return MyTimer.instance.StartTimer(time, () =>
        {
            PlayAudio(audioClip, volumeScale);
        });
    }
}