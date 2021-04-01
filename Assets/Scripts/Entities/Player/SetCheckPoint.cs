using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Azer.UtilityComponents;

namespace Azer.Player
{
    public class SetCheckPoint : MonoBehaviour
    {
        private PlayerRespawn playerRespawn;

        [field: SerializeField]
        public ChangeCameraOptions CameraAreaTrigger { get; private set; }

        private void Awake()
        {
            playerRespawn = FindObjectOfType<PlayerRespawn>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                playerRespawn.CheckPoint = this;
            }
        }
    }
}
