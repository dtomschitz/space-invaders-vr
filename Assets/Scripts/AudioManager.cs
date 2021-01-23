using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Sound
{
    ButtonClick,
    Shoot
}

/// <summary>
/// Class <c>SoundClip</c> is used to combine a <see cref="Sound"/> with an existing
/// <see cref="AudioClip"/>. The class can also store some important configuration
/// paramaters such as the volume, the pitch or the <see cref="maxTimer"/> which
/// is used to set the amount of time it takes before the sound can get played
/// again.
/// </summary>
[System.Serializable]
public class SoundClip
{
    public Sound sound;
    public AudioClip clip;
    public float maxTimer;

    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;

    public bool loop;

    [Range(0, 1)]
    public float spatialBlend = 1f;
    public float maxDistance = 100f;
}

/// <summary>
/// Class <c>AudioManager</c> is used play back a specific <see cref="SoundClip"/>
/// for once.
/// </summary>
public class AudioManager : MonoBehaviour
{
    #region Singelton

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    #endregion;

    public SoundClip[] soundClips;
    private Dictionary<Sound, float> soundTimer;

    private GameObject oneShotGameObject;
    private AudioSource oneShotAudioSource;

    void Start()
    {
        soundTimer = new Dictionary<Sound, float>();
    }

    /// <summary>
    /// Plays the specific <see cref="SoundClip"/> at the given position.
    /// The Method will determine whether the sound can be played currently or
    /// not. If it can a new game object with the <see cref="AudioSource"/>
    /// component attached will be created. Based on the given <see cref="Sound"/>
    /// the method will find the associated <see cref="SoundClip"/>. 
    /// The created game object will get destoryed after the sound ended.
    /// </summary>
    /// <param name="sound">The sound which should get played.</param>
    /// <param name="position">The position where the sound should be played.</param>
    public void PlaySound(Sound sound, Vector3 position)
    {
        if (CanPlay(sound))
        {
            GameObject soundGameObject = new GameObject("Sound");
            soundGameObject.transform.position = position;

            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            SoundClip soundClip = GetSoundClip(sound);

            SetAudioSourceConfig(audioSource, soundClip);
            audioSource.Play();

            Destroy(soundGameObject, audioSource.clip.length);
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
    /// <param name="position">The position where the sound should be played.</param>
    public void PlaySound(Sound sound)
    {
        if (CanPlay(sound))
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
    /// <param name="sound"></param>
    /// <returns></returns>
    private SoundClip GetSoundClip(Sound sound)
    {
        SoundClip[] clips = soundClips.Where(soundClip => soundClip.sound == sound).ToArray();
        if (clips == null || clips.Length == 0)
        {
            return null;
        }

        if (clips.Length == 1)
        {
            return clips[0];
        }

        return clips[Random.Range(0, clips.Length)];
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
