using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IStreamYouScream
{
    public class GhostSpawner : MonoBehaviour
    {
        public GhostController ghostPrefab;
        public GameObject spawnPoint;
        private bool spawningEnabled = true;

        public void Spawn()
        {
            if (!spawningEnabled)
            {
                return;
            }
            GhostController ghost = Instantiate(ghostPrefab, spawnPoint.transform.position, Quaternion.identity);
            spawningEnabled = false;
            ghost.OnDefeated.AddListener(EnableAgain);
        }

        private void EnableAgain()
        {
            spawningEnabled = true;
        }
    }
}
