using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="AudioClipGroup")]
public class AudioClipGroup : ScriptableObject
{
    [Range(0f, 1f)]
    public float VolumeMin = 1f;
    [Range(0f, 1f)]
    public float VolumeMax = 1f;

    [Range(0f, 2f)]
    public float PitchMin = 1f;
    [Range(0f, 2f)]
    public float PitchMax = 1f;

    public float Cooldown = 0.1f;
    private float timestamp;
    public List<AudioClip> Clips;


    private void OnEnable()
    {
        timestamp = 0;
    }
    
    public void Play()
    {
        if (AudioSourcePool.Instance == null)
            return;
        
        Play(AudioSourcePool.Instance.GetSource());
    }

    public void Play(AudioSource source)
    {
        if (timestamp > Time.time)
            return;
        if (Clips.Count <= 0)
            return;

        timestamp = Time.time + Cooldown;
        source.volume = Random.Range(VolumeMin, VolumeMax);
        source.pitch = Random.Range(PitchMin, PitchMax);
        source.clip = Clips[Random.Range(0, Clips.Count)];

        source.Play();
    }

}
