using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "AudioEvent/Simple")]
public class SimpleAudioEvent : AudioEvent {
    private int _times = 0;

    public override void Play(AudioSource source) {
        if (clips.Length == 0)
            return;

        source.clip = clips[Random.Range(0, clips.Length)];

        source.volume = Random.Range(volume.min, volume.max);
        source.pitch = Random.Range(pitch.min, pitch.max);

        source.Play();
    }

    public override void PlayEveryTimes(AudioSource source, int times) {
        _times++;

        if (_times % times == 0) {
            Play(source);
        }
    }
}
