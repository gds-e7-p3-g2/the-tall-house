using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IStreamYouScream
{
    public class ActionTeleport : Action
    {
        private GameObject Player;
        [SerializeField] GameObject Destination;

        void Start()
        {
            Player = GameObject.FindWithTag("Player");
        }

        public override void PerformAction()
        {

            StartCoroutine(MoveAnimation());
        }

        IEnumerator MoveAnimation()
        {
            Camera.main.GetComponent<FadeToBlack>().FadeOut();

            yield return new WaitForSeconds(1.1f);

            Player.transform.position = new Vector3(Destination.transform.position.x, Destination.transform.position.y, Player.transform.position.z);

            yield return new WaitForSeconds(.4f);

            Camera.main.GetComponent<FadeToBlack>().FadeIn();

            yield return new WaitForSeconds(.5f);
        }
    }
}