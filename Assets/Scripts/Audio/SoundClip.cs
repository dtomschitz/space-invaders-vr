using UnityEngine;

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