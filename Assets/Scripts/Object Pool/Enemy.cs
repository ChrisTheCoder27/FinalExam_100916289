using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace Chapter.ObjectPool
{
    public class Enemy : MonoBehaviour
    {
        public IObjectPool<Enemy> Pool { get; set; }

        [SerializeField] float maxHealth = 100f;
        float currentHealth;

        void OnEnable()
        {
            StartCoroutine(AutoDestroy());
        }

        void OnDisable()
        {
            ResetEnemy();
        }

        void Start()
        {
            currentHealth = maxHealth;
        }

        IEnumerator AutoDestroy()
        {
            yield return new WaitForSeconds(4f);
            TakeDamage(maxHealth);
        }

        void ReturnToPool()
        {
            Pool.Release(this);
        }

        void ResetEnemy()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                ReturnToPool();
            }
        }
    }
}