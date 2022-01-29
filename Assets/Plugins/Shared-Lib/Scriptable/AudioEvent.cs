using SukharevShared;
using UnityEngine;

public abstract class AudioEvent : ScriptableObject {
    public AudioClip[] clips;

    public RangedFloat volume;

    public RangedFloat pitch;

    public abstract void Play(AudioSource source);

    public abstract void PlayEveryTimes(AudioSource source, int times);

    public void Stop(AudioSource source) {
        source.Stop();
    }

    public void PlayLoop(AudioSource source) {
        Play(source);

        source.loop = true;
    }
}
