using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New SoundAsset", menuName = "Audio/SoundAsset")]
public class SoundAsset : ScriptableObject
{
    public Sound sound;
    public SoundClip[] clips;
}
