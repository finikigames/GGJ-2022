using Photon.Pun;
using UnityEngine;

namespace GGJ2022.Source.Scripts.Controls
{
    public class PlayerSoundController : MonoBehaviour
    {
        public AudioSource AudioSource;
        public SimpleAudioEvent HitDevilEvent;
        public SimpleAudioEvent HitAngelEvent;
        public SimpleAudioEvent ShootAngelEvent;
        public SimpleAudioEvent ShootDevilEvent;
        public SimpleAudioEvent SwtichStateEvent;
        public SimpleAudioEvent HitWallEvent;

        [PunRPC]
        private void PlayDevilShootSound()
        {
            ShootDevilEvent.Play(AudioSource);
        } 
        
        [PunRPC]
        private void PlayAngelShootSound()
        {
            ShootAngelEvent.Play(AudioSource);
        }
        
        [PunRPC]
        private void PlayDevilHitSound()
        {
            HitDevilEvent.Play(AudioSource);
        } 
        
        [PunRPC]
        private void PlayAngelHitSound()
        {
            HitAngelEvent.Play(AudioSource);
        }
        
        [PunRPC]
        private void PlaySwitchStateSound()
        {
            SwtichStateEvent.Play(AudioSource);
        } 
        
        [PunRPC]
        private void PlayWallHitSound()
        {
            HitWallEvent.Play(AudioSource);
        }
    }
}