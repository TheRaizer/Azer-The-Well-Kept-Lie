using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Azer.EntityComponents;
using Azer.UtilityComponents;

namespace Azer.Player
{
    public class PlayerRespawn : MonoBehaviour
    {
        public SetCheckPoint CheckPoint { get; set; }

        private PlayerController player;
        private PlayerPsychokinesis playerKinesis;
        private SpriteRenderer playerSprite;
        private HealthManagerTemplate playerHealth;

        [SerializeField] private CameraController cameraController = null;
        [SerializeField] private float respawnTime = 0;

        private WaitForSeconds respawnWait;

        private void Awake()
        {
            respawnWait = new WaitForSeconds(respawnTime);
            player = FindObjectOfType<PlayerController>();
            playerHealth = player.gameObject.GetComponent<HealthManagerTemplate>();
            playerSprite = player.gameObject.GetComponent<SpriteRenderer>();
            playerKinesis = player.gameObject.GetComponent<PlayerPsychokinesis>();
        }

        private void DisablePlayer()
        {
            player.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            playerKinesis.CanUse = false;
            cameraController.IsFollowing = false;
            playerSprite.enabled = false;
            player.enabled = false;
        }

        private void EnablePlayer()
        {
            playerHealth.MaxHeal();
            cameraController.IsFollowing = true;
            playerSprite.enabled = true;
            playerKinesis.CanUse = true;
            player.enabled = true;
            SetCameraBounds();
        }

        private IEnumerator RespawnPlayerCo()
        {
            DisablePlayer();
            MovePlayerToCheckPoint();

            yield return respawnWait;

            EnablePlayer();
        }

        private void MovePlayerToCheckPoint()
        {
            player.transform.position = CheckPoint.transform.position;
        }

        private void SetCameraBounds()
        {
            cameraController.SetBounds(CheckPoint.CameraAreaTrigger.NextMinX, 
                                        CheckPoint.CameraAreaTrigger.NextMinY, 
                                        CheckPoint.CameraAreaTrigger.NextMaxX, 
                                        CheckPoint.CameraAreaTrigger.NextMaxY);

            cameraController.SetAnchor(CheckPoint.CameraAreaTrigger.NextAnchor);
            cameraController.SetLerpSpeed(CheckPoint.CameraAreaTrigger.nextSmoothedSpeed);
            cameraController.SetOffsets(CheckPoint.CameraAreaTrigger.NextXOffset, CheckPoint.CameraAreaTrigger.NextYOffset);
        }

        public void RespawnPlayer()
        {
            StartCoroutine("RespawnPlayerCo");
        }
    }
}
