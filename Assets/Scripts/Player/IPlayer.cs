using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
    void Move(float distance);
    void Walk();
    void Run();
    void EnterHideout();
    void LeaveHideout();
    void PickItem();
    void UseItem();
    void StartRecording();
    void StopRecording();
    void FlashAttack();
    void EmergencyFlashAttack();
    void BeStuned();
    void Lose();
    bool GetIsHiding();
    void LookAtCamera();
    void Face();
}