using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IStreamYouScream
{
    class PlayerStunnedState : PlayerState
    {
        public PlayerStunnedState(PlayerController playerController) : base(playerController) { }

        // TODO go to Idle after X seconds
    }
}