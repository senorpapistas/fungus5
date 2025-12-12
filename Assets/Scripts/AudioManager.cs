using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public List<AudioClip> audioClips = new List<AudioClip>();

    private HashSet<string> toggleAudioObjects = new HashSet<string>();

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

    #region Public Audio Methods
    public void PlaySound(string audioName)
    {
        GameObject audioObject = new GameObject("audioObject: " + audioName);
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
        GameObject audioObject = new GameObject("audioObject: " + audioName);
        audioObject.transform.SetParent(transform, false);
        audioObject.transform.position = position;
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 1;

        AudioClip clip = audioClips.Find(x => x.name == audioName);
        audioSource.clip = clip;

        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioObject, clipLength);
    }

    //plays a continuous sound like walking. currently supports playing only 1 instance of a toggled sound
    public void ToggleSound(string audioName)
    {
        if (toggleAudioObjects.Contains(audioName))
        {
            return;
        }
        else
        {
            toggleAudioObjects.Add(audioName);
            GameObject audioObject = new GameObject("audioObject: " + audioName);
            audioObject.transform.SetParent(transform, false);
            AudioSource audioSource = audioObject.AddComponent<AudioSource>();

            AudioClip clip = audioClips.Find(x => x.name == audioName);
            audioSource.clip = clip;

            audioSource.Play();
            float clipLength = audioSource.clip.length;
            Destroy(audioObject, clipLength);
            StartCoroutine(RemoveFromSet(audioName, clipLength));
        }
    }

    #endregion

    private IEnumerator RemoveFromSet(string audioName, float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        toggleAudioObjects.Remove(audioName);
    }
}
