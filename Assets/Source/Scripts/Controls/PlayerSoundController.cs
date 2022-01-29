using Photon.Pun;
using UnityEngine;

namespace GGJ2022.Source.Scripts.Controls
{
    public class PlayerSoundController : MonoBehaviour
    {
        public AudioSource AudioSource;
        public SimpleAudioEvent HitEvent;
        public SimpleAudioEvent ShootEvent;

        [PunRPC]
        private void PlayShootSound()
        {
            ShootEvent.Play(AudioSource);
        }

        [PunRPC]
        private void PlayHitSound()
        {
            HitEvent.Play(AudioSource);
        }
    }
}