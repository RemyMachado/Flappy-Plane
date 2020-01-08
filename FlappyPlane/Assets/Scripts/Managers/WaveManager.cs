using System;
using System.Collections.Generic;
using Controllers;
using UnityEngine;

namespace Managers
{
    public class WaveManager : MonoBehaviour
    {
        public event Action<uint> OnNewWave;    
        public uint wave = 0;
        public uint enemyCount;
        public AudioClip newWaveSound;
        private uint _checkEndWaveFrameInterval = 30;
        private GameObject _player;

        private void Start()
        {
            _player = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            if (Time.frameCount % _checkEndWaveFrameInterval == 0)
            {
                enemyCount = (uint) GameObject.FindGameObjectsWithTag("Enemy").Length;

                if (enemyCount == 0)
                {
                    IncrementWave();
                }
            }
        }

        public uint GetWave()
        {
            return wave;
        }

        private void IncrementWave()
        {
            if (_player)
            {
                ++wave;
                GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>().PlayOneShot(newWaveSound, 0.2f);
                OnNewWave?.Invoke(wave);
            }
        }
    }
}