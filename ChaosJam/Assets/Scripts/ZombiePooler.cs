using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePooler : MonoBehaviour
{

    public static ZombiePooler Instance;

    #region Singleton
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> zombieDictonary;

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    // Start is called before the first frame update
    void Start()
    {
        zombieDictonary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> zombiePool = new Queue<GameObject>();

            for(int i=0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                zombiePool.Enqueue(obj);
            }

            zombieDictonary.Add(pool.tag, zombiePool);

        }

    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {

        if(!zombieDictonary.ContainsKey(tag))
        {
            Debug.LogWarning("Tag Doesnt Exist");
            return null;
        }

        GameObject objToSpawn = zombieDictonary[tag].Dequeue();

        objToSpawn.SetActive(true);
        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;

        IPooledObject pooledObj = objToSpawn.GetComponent<IPooledObject>();

        if(pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        zombieDictonary[tag].Enqueue(objToSpawn);

        return objToSpawn;
    }
}