﻿using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Player))]
    public class PlayerInput : MonoBehaviour
    {
        #region Variables
        public Player Player;
        public float DashCooldown;

        GameObject gameManager;
        GameManager gm;

        [SerializeField]
        public bool TakingHit;

        public GameObject ParticleSpawn;
        ParticleSystem ParticleSystem;

        private GameObject Trail;

        private TrailRenderer tr;

        #endregion

        #region Start
        public void Start()
        {
            Trail = GameObject.Find("FollowTrail");
            tr = Trail.GetComponent<TrailRenderer>();
            Player = GetComponent<Player>();
            ParticleSystem = ParticleSpawn.GetComponent<ParticleSystem>();
            DeactivateParticle();
            gameManager = GameObject.Find("Game Manager");
            gm = gameManager.GetComponent<GameManager>();
        }
        #endregion

        #region Update
        public void Update()
        {
            if (!gm.IsPaused())
            {
            SetDashCooldown();

            /*vector input stores players coordinates*/
            var directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                Player.CheckIfPlayerMoved(directionalInput);
            Player.SetDirectionalInput(directionalInput);

            

            if (!TakingHit)
            {         
                if (Input.GetKeyDown(KeyCode.Space))
                    Player.OnJumpInputDown();
                if (Input.GetKeyUp(KeyCode.Space))
                    Player.OnJumpInputUp();
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (DashCooldown == 0)
                    {
                        DashCooldown = 1.5f;
                        Player.OnDashInput();
                        ActivateParticle();
                        Invoke("DeactivateParticle", 0.32f);
                    }
                }            
            }
            print(tr);

            Player.TakingHit = TakingHit;
        }
}

        private void SetDashCooldown()
        {
            DashCooldown -= Time.deltaTime;
            if (DashCooldown < 0)
                DashCooldown = 0;
        }
        #endregion

        #region Hit
        public bool GetHit()
        {
            return TakingHit;
        }
        #endregion

        #region Paticles

        void ActivateParticle()
        {
            ParticleSystem.Play();
        }
        void DeactivateParticle()
        {
            ParticleSystem.Stop();
        }
        #endregion
    }
}
