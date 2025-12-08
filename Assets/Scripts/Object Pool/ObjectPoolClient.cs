using UnityEngine;

namespace Chapter.ObjectPool
{
    public class ObjectPoolClient : MonoBehaviour
    {
        EnemyObjectPool pool;

        void Start()
        {
            pool = GetComponent<EnemyObjectPool>();

            InvokeRepeating("SpawnEnemy", 2f, 3f);
        }

        void SpawnEnemy()
        {
            pool.Spawn();
        }
    }
}