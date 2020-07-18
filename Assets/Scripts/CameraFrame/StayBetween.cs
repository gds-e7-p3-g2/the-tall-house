using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IStreamYouScream
{
    public class StayBetween : MonoBehaviour
    {
        [SerializeField] GameObject Player;
        [SerializeField] GameObject CameraFrame;
        [SerializeField] float MaxHorizontalDistance = 3f;
        [SerializeField] float MaxVerticalDistance = 20f;
        void Start()
        {
            FixPosition();
        }

        void FixedUpdate()
        {
            FixPosition();
        }

        private void FixPosition()
        {
            Vector3 NewPosCandidate = (Player.transform.position + CameraFrame.transform.position) / 2f;

            float xt = Player.transform.position.x;
            float yt = Player.transform.position.y;

            float x = NewPosCandidate.x;
            float y = NewPosCandidate.y;
            float z = transform.position.z;

            float newX = Mathf.Max(Mathf.Min(x, xt + MaxHorizontalDistance), xt - MaxHorizontalDistance);
            float newY = Mathf.Max(Mathf.Min(y, yt + MaxVerticalDistance), yt - MaxVerticalDistance);

            transform.position = new Vector3(newX, newY, z);
        }
    }
}