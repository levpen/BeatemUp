using System.Collections;
using Beatemup.Enemy;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Beatemup.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed = 50f;
        [SerializeField] private GameObject crossHair;
        [SerializeField] private float health = 100;

        [SerializeField] private new Camera camera;
        [SerializeField] private HudController hud;
        private Vector2 movement;

        private Rigidbody2D rb;
        private Animator animator;
        private SpriteRenderer spriteRenderer;
        
        //Experience props
        private float xp;
        private int curLevel = 1;
        [SerializeField] private float nextLevelXp = 100;
        public float magnetSpeed = 1;
        [SerializeField] private float levelMultiplier = 1.2f;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            Cursor.visible = false;
        }

        public void Update()
        {
            MouseMove();

            movement.x = Input.GetAxis("Horizontal");
            movement.y = Input.GetAxis("Vertical");
            
            //Animation
            if (movement.x != 0.0f || movement.y != 0.0f)
            {
                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
            
            //Sprite direction
            if (movement.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if(movement.x > 0)
            {
                spriteRenderer.flipX = false;
            }
        }

        private void FixedUpdate()
        {
            //movement logic
            rb.MovePosition(rb.position + movement * (speed * Time.deltaTime));
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var target = other.gameObject.GetComponent<EnemyController>();
            if (target != null)
            {
                ReactToHit(target.damage);
            }
        }

        private void MouseMove()
        {
            var aim = camera.ScreenToWorldPoint(Input.mousePosition);
            aim.z = 0;
            if (aim.x < transform.position.x)
            {
                spriteRenderer.flipX = true;
            }

            else if (aim.x > transform.position.x)
            {
                spriteRenderer.flipX = false;
            }
            crossHair.transform.position = aim;
        }


        private void ReactToHit(float damage)
        {
            // Debug.Log("Player damaged");
            
            health -= damage;
            hud.ChangeHp(health);
            
            // Debug.Log(health);
            if (health <= 0)
            {
                //TODO
                Debug.Log("dead");
                //Die();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Debug.Log("trigger");
            var target = other.gameObject.GetComponent<Experience>();
            if (target != null && !target.magnetizing)
            {
                target.magnetizing = true;
                StartCoroutine(Magnetize(target));
            }
        }

        IEnumerator Magnetize(Experience target)
        {
            while (Vector2.Distance(target.transform.position, transform.position) > .5f)
            {
                target.transform.position = Vector2.MoveTowards(target.transform.position, transform.position,
                    Time.deltaTime * magnetSpeed);
                yield return null;
            }

            target.Die(this);
        }
        
        public void AddXp(float xpToAdd)
        {
            xp += xpToAdd;
            while (xp >= nextLevelXp)
            {
                xp -= nextLevelXp;
                curLevel++;
                nextLevelXp *= levelMultiplier;
                hud.LevelUp(curLevel);
            }
        }

        private void Die()
        {
            Destroy(this.GameObject());
        }
    }
}