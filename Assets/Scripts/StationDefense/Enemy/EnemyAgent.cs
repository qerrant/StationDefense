using StationDefense.Components;
using StationDefense.Game;
using StationDefense.Path;
using System.Collections.Generic;
using UnityEngine;

namespace StationDefense.Enemy
{
    public class EnemyAgent : Damageable
    {
        public AudioClip explosionSFX;
        public AudioClip detectedSFX;
        private List<Tile> _trackTiles = new List<Tile>();
        private int _targetIndex = 0;
        public GameObject enemyModel;
        public float speed = 5.0f;
        public float damage = 10.0f;
        public float maxHealth = 100.0f;
        public int loot = 10;
        public LayerMask layerMask;
        public GameObject[] prefabs;
        private Tile _startTile;

        public bool Ghost { get; set; }

        
        void Start()
        {
            SetMaxHealth(maxHealth);
            SetDamage(damage);
            UpdateTrack();
            CreateModel();
        }

        public void CreateModel()
        {
            int idx = Random.Range(0, prefabs.Length);
            GameObject go = Instantiate(prefabs[idx], transform.position, Quaternion.identity, transform);
            enemyModel = go;
            if (idx == 1) Ghost = true;

            go.transform.position += Vector3.up * 0.304f;
            go.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        public void UnGhost()
        {
            Destroy(enemyModel);
            GameObject go = Instantiate(prefabs[0]);
            go.transform.SetParent(transform);
            go.transform.localPosition = new Vector3(0, 0.304f, 0);
            go.transform.localRotation = Quaternion.Euler(0, 180, 0);
            enemyModel = go;
            Ghost = false;
            AudioSource sfx = GetComponent<AudioSource>();
            if (sfx == null) return;
            sfx.clip = detectedSFX;
            sfx.Play();
        }

        protected override void OnDead()
        {
            base.OnDead();
            Dead();
        }

        public void UpdateTrack()
        {
            int oldIndex = _targetIndex;
            Tile currentTile = GetUnderTile();

            if (_startTile == null) _startTile = currentTile;

            if (PathManager.instance.UpdatePath(currentTile.GetGridPosition()))
            {                
                _targetIndex = 1;
            }

            _trackTiles = new List<Tile>(PathManager.instance.GetPath(currentTile));
            if (_trackTiles.Count < 1)
            {
                _trackTiles = new List<Tile>(PathManager.instance.GetPath(_startTile));
                _targetIndex = oldIndex;
            }
            else
            {
                _startTile = currentTile;
                _targetIndex = 1;
            }
        }

        private Tile GetUnderTile()
        {             
            Vector3 shift = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            Ray ray = new Ray(shift, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(shift, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
            {
                return hit.collider.GetComponent<Tile>();
            }

            return null;
        }


        void Update()
        {
            if (GetHealth() > 0 && GameManager.instance.State == GameState.Game)
            {
                Move();
            }
        }

        private void Move()
        {
            if (_trackTiles == null || _trackTiles.Count < 1) return;

            transform.LookAt(_trackTiles[_targetIndex].transform.position);
            transform.position = Vector3.MoveTowards(transform.position, _trackTiles[_targetIndex].transform.position, Time.deltaTime * speed);

            if (transform.position == _trackTiles[_targetIndex].transform.position)
            {
                _targetIndex = Mathf.Clamp(++_targetIndex, 0, _trackTiles.Count - 1);
            }
        }


        private void Dead()
        {           
            enemyModel.SetActive(false);

            if (CurrencyManager.instanceExists)
            {
                CurrencyManager.instance.AddCurrency(loot);
            }

            WaveSpawner.instance.RemoveFromList(this);

            Destroy(gameObject, 1.0f);

            AudioSource sfx = GetComponent<AudioSource>();
            if (sfx == null) return;
            sfx.clip = explosionSFX;
            sfx.Play();
        }

        public void ScaleHealth(float scale)
        {
            maxHealth *= scale;
            SetMaxHealth(maxHealth);
        }
    }
}
