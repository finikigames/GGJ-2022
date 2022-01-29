using GGJ2022.Source.Scripts.Game.Players.Base;
using UnityEngine;

namespace GGJ2022.Source.Scripts.Game.ECS.Components
{
    public struct PlayerRequestData
    {
        public GameObject PlayerObject;
        public ObjectState InitialState;
    }
}