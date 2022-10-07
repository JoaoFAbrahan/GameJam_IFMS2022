using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CHARACTERS
{
    public class CODE_PlayerSpawn : MonoBehaviour
    {
        private static Transform[] spawnPoints;
        private int spawnIndex;

        private void Awake()
        {
            // Gera uma Array contendo todos os SpawnPoints
            spawnPoints = new Transform[transform.childCount];

            for (int i = 0; i < spawnPoints.Length; i++)
            {
                spawnPoints[i] = transform.GetChild(i);
            }

            spawnIndex = RandomizeSpawnPosition();
        }

        /// <summary>
        /// Randomiza uma index do Array
        /// </summary>
        /// <returns></returns>
        private int RandomizeSpawnPosition()
        {
            // Randomiza um numero para usar como index do array de _spawnPoints
            int seedRandom;

            seedRandom = Random.Range(0, spawnPoints.Length);
            return seedRandom;
        }

        /// <summary>
        /// Retorna a Transform Position do ponto de spawn escolhido
        /// </summary>
        /// <returns></returns>
        public Transform GetSpawnPosition()
        {
            return spawnPoints[spawnIndex];
        }
    }
}