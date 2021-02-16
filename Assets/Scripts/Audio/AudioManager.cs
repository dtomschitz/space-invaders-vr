using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Class <c>AudioManager</c> is used play back a specific <see cref="Sound"/> 
/// for once or in a loop.
/// </summary>
public class AudioManager : MonoBehaviour
{
    #region Singelton

    public static AudioManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion;

    SoundAsset[] soundAssets;

    GameObject oneShotGameObject;
    AudioSource oneShotAudioSource;

    Dictionary<Sound, float> soundTimer;
    Dictionary<Sound, GameObject> loopingAudio;

    void Start()
    {
        soundAssets = Resources.LoadAll<SoundAsset>("SoundAssets");
        soundTimer = new Dictionary<Sound, float>();
        loopingAudio = new Dictionary<Sound, GameObject>();
    }

    /// <summary>
    /// Plays the specific <see cref="Sound"/> at the given position. The Method
    /// will determine whether the sound can be played currently or not. If it 
    /// can a new game object with the <see cref="AudioSource"/> component 
    /// attached will be created. Based on the given <see cref="Sound"/> the 
    /// method will find the associated <see cref="SoundClip"/>. If the resolved
    /// <see cref="SoundClip"/> is marked as a looping one the created game 
    /// object will not get destoryed after the sound ended.
    /// </summary>
    /// <param name="sound">The sound which should get played.</param>
    /// <param name="position">The position where the sound should be played.</param>
    public void PlaySound(Sound sound, Vector3 position)
    {
        if (CanPlay(sound) && !loopingAudio.ContainsKey(sound))
        {
            GameObject soundGameObject = new GameObject("Sound");
            soundGameObject.transform.position = position;

            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            SoundClip soundClip = GetSoundClip(sound);

            SetAudioSourceConfig(audioSource, soundClip);
            audioSource.Play();

            if (soundClip.loop) {
                loopingAudio.Add(sound, soundGameObject);
            } else {
                Destroy(soundGameObject, audioSource.clip.length);
            }
        }
    }


    /// <summary>
    /// Plays the specific <see cref="SoundClip"/>.
    /// The Method will determine whether the sound can be played currently or
    /// not. If it can a new game object with the <see cref="AudioSource"/>
    /// component attached will be created. Based on the given <see cref="Sound"/>
    /// the method will find the associated <see cref="SoundClip"/>.
    /// </summary>
    /// <param name="sound">The sound which should get played.</param>
    public void PlaySound(Sound sound)
    {
        if (CanPlay(sound) && !loopingAudio.ContainsKey(sound))
        {
            if (oneShotGameObject == null)
            {
                oneShotGameObject = new GameObject("One Shot Sound");
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
            }

            SoundClip soundClip = GetSoundClip(sound);
            if (soundClip != null)
            {
                SetAudioSourceConfig(oneShotAudioSource, soundClip);
                oneShotAudioSource.PlayOneShot(oneShotAudioSource.clip);
            }
 
        }
    }

    /// <summary>
    /// Stops the specific looping <see cref="Sound"/>.
    /// </summary>
    /// <param name="sound">The sound which should get stopped.</param>
    public void StopSound(Sound sound)
    {
        if (loopingAudio.ContainsKey(sound))
        {
            GameObject soundGameObject = loopingAudio[sound];
            loopingAudio.Remove(sound);
            Destroy(soundGameObject);
        }
    }

    /// <summary>
    /// Loads the paramters of the <see cref="SoundClip"/> into the <see cref="AudioSource"/>
    /// component.
    /// </summary>
    /// <param name="audioSource">The created <see cref="AudioSource"/>.</param>
    /// <param name="soundClip">The <see cref="SoundClip"/> which gets played.</param>
    private void SetAudioSourceConfig(AudioSource audioSource, SoundClip soundClip)
    {
        audioSource.clip = soundClip.clip;
        audioSource.volume = soundClip.volume;
        audioSource.maxDistance = soundClip.maxDistance;
        audioSource.spatialBlend = soundClip.spatialBlend;
        audioSource.loop = soundClip.loop;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        audioSource.dopplerLevel = 0f;
    }

    /// <summary>
    /// Tries to find the assoicated <see cref="SoundClip"/> to the given
    /// <see cref="Sound"/> and returns it. If no <see cref="SoundClip"/> could
    /// be found the method will return null.
    /// </summary>
    /// <param name="sound">
    /// The sound for which the <see cref="SoundClip"/> should be found.
    /// </param>
    /// <returns></returns>
    private SoundClip GetSoundClip(Sound sound)
    {
        SoundClip[] clips = Array.Find(soundAssets, (soundAsset) => soundAsset.sound == sound).clips.ToArray();
        if (clips == null || clips.Length == 0) return null;
        if (clips.Length == 1) return clips[0];

        return clips[UnityEngine.Random.Range(0, clips.Length)];
    }

    /// <summary>
    /// Checks whether the sound can be played again or not.This method is used
    /// to ensure that sounds like the movement sound could not played overlapping.
    /// </summary>
    /// <param name="sound">The sound which should get played.</param>
    /// <returns>True if the sound can be played; otherwise false.</returns>
    private bool CanPlay(Sound sound)
    {
        foreach (KeyValuePair<Sound, float> soundTime in soundTimer)
        {
            if (soundTime.Key == sound)
            {
                float lastTimePlayed = soundTimer[sound];
                float maxTimer = GetSoundClip(sound).maxTimer;

                if (lastTimePlayed + maxTimer < Time.time)
                {
                    soundTimer[sound] = Time.time;
                    return true;
                }
                return false;
            }
        }

        return true;
    }
}
