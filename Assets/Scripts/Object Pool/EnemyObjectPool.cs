using UnityEngine;
using UnityEngine.Pool;

namespace Chapter.ObjectPool
{
    public class EnemyObjectPool : MonoBehaviour
    {
        [SerializeField] int maxPoolSize = 10;
        [SerializeField] int stackDefaultCapacity = 10;
        [SerializeField] GameObject enemyPrefab;

        IObjectPool<Enemy> pool;

        public IObjectPool<Enemy> Pool
        {
            get
            {
                if (pool == null)
                {
                    pool = new ObjectPool<Enemy>(CreatedPooledItem, OnTakeFromPool, OnReturnedToPool,
                    OnDestroyPoolObject, true, stackDefaultCapacity, maxPoolSize);
                }
                return pool;
            }
        }

        Enemy CreatedPooledItem()
        {
            GameObject go = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            Enemy enemy = go.AddComponent<Enemy>();
            enemy.Pool = Pool;
            return enemy;
        }

        void OnReturnedToPool(Enemy enemy)
        {
            enemy.gameObject.SetActive(false);
        }

        void OnTakeFromPool(Enemy enemy)
        {
            enemy.gameObject.SetActive(true);
        }

        void OnDestroyPoolObject(Enemy enemy)
        {
            Destroy(enemy.gameObject);
        }

        public void Spawn()
        {
            var amount = Random.Range(1, 5);
            for (int i = 0; i < amount; i++)
            {
                var enemy = Pool.Get();
                enemy.transform.position = Random.insideUnitCircle * 6;
            }
        }
    }
}