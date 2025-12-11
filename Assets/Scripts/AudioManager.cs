using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public List<AudioClip> audioClips = new List<AudioClip>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this);
    }

    public void PlaySound(string audioName)
    {
        GameObject audioObject = new GameObject("audioObject");
        audioObject.transform.SetParent(transform, false);
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();

        AudioClip clip = audioClips.Find(x => x.name == audioName);
        audioSource.clip = clip;

        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioObject, clipLength);

    }

    public void PlaySoundAtPosition(string audioName, Vector3 position)
    {
        GameObject audioObject = new GameObject("audioObject");
        audioObject.transform.SetParent(transform, false);
        audioObject.transform.position = position;
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();

        AudioClip clip = audioClips.Find(x => x.name == audioName);
        audioSource.clip = clip;

        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioObject, clipLength);
    }

}
