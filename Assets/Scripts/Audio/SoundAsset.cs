using UnityEngine;

/// <summary>
/// Class <c>SoundAsset</c> is used to store a list of <see cref="SoundClips"/> 
/// which are associated with the specified <see cref="Sound"/>.
/// </summary>
[System.Serializable]
[CreateAssetMenu(fileName = "New SoundAsset", menuName = "Audio/SoundAsset")]
public class SoundAsset : ScriptableObject
{
    public Sound sound;
    public SoundClip[] clips;
}
