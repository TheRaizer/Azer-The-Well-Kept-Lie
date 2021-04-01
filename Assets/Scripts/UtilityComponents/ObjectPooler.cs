using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Azer.UtilityComponents
{
    public class ObjectPooler : MonoBehaviour
    {
        [System.Serializable]
        public class Pool
        {
            public int amount = 0;
            public string id = null;
            public GameObject prefab = null;

            public Pool(int amt, string id, GameObject prefab)
            {
                this.id = id;
                this.prefab = prefab;
                amount = amt;
            }
        }



        [SerializeField] private List<Pool> pools = null;

        private Dictionary<string, Queue<GameObject>> poolDictionary = null;

        // Start is called before the first frame update
        void Awake()
        {
            poolDictionary = new Dictionary<string, Queue<GameObject>>();

            foreach (Pool pool in pools)
            {
                Queue<GameObject> objQueue = new Queue<GameObject>();
                for (int i = 0; i < pool.amount; i++)
                {
                    GameObject poolObj = Instantiate(pool.prefab);
                    poolObj.SetActive(false);
                    objQueue.Enqueue(poolObj);
                }
                poolDictionary.Add(pool.id, objQueue);
            }
        }

        public GameObject SpawnObjectFromPool(string tag, Vector2 pos, Quaternion rot)
        {
            if (!poolDictionary.ContainsKey(tag))
                Debug.LogError("No such pool with tag " + tag);

            GameObject objToSpawn = poolDictionary[tag].Dequeue();

            objToSpawn.transform.position = pos;
            objToSpawn.transform.rotation = rot;
            objToSpawn.SetActive(true);

            poolDictionary[tag].Enqueue(objToSpawn);

            return objToSpawn;
        }
    }
}
