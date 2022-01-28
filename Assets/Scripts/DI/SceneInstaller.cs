using Controls;
using UnityEngine.Serialization;
using Zenject;

namespace DI
{
    public class SceneInstaller : MonoInstaller
    {
        [FormerlySerializedAs("PlayerControl")] public JoystickControlInfo joystickControlInfo;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<JoystickControlInfo>().FromInstance(joystickControlInfo).AsSingle();
        }
    }
}