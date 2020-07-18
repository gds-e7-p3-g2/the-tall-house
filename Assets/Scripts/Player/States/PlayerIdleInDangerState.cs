using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IStreamYouScream
{
    class PlayerIdleInDangerState : PlayerIdleState
    {
        public PlayerIdleInDangerState(PlayerController playerController) : base(playerController) { }
        public override void ToggleHiding() { }

    }
}