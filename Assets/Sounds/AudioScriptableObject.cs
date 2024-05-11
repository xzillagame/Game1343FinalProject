using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio Sources", menuName = "SciptableObjects/AudioPlayer")]
public class AudioScriptableObject : ScriptableObject
{
    [Serializable]
    public class ShieldAudioClips
    {
        public AudioClip shieldGain;
        public AudioClip shieldBroke;
        public AudioClip shieldFall;
    }
    public ShieldAudioClips shieldAudioClips;


    [Serializable]
    public class ShrinkAudioClips
    {
        public AudioClip shrinking;
        public AudioClip growing;
    }
    public ShrinkAudioClips shrinkAudioClips;


    [Serializable]
    public class HitSFX
    {
        public AudioClip playerHitSFX;
    }

    public HitSFX hitSFX;





}
