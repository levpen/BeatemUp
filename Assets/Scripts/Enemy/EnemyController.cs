using Unity.VisualScripting;
using UnityEngine;

namespace Beatemup.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        private static Transform player;
        public float moveSpeed;
        public float health;
        public float damage;
        
        public GameObject xpPrefab;
        
        private void Awake()
        {
            player = GameObject.FindWithTag("Player").transform;
        }

        private void Update()
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, Time.deltaTime * moveSpeed);
        }


        public void ReactToHit(float dmg)
        {
            //Debug.Log("Hit");
            health -= dmg;
            //Debug.Log(health);
            if (health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Instantiate(xpPrefab, transform.position, transform.rotation);
            Destroy(this.GameObject());
        }
    }
}