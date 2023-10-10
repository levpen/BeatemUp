using System.Collections;
using Beatemup.Enemy;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        [SerializeField] Transform firePoint;
        
        //Experience props
        public float magnetSpeed = 1;
        private bool canMove = true;

        private bool dead;


        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            Cursor.visible = false;
        }

        public void Update()
        {
            if (canMove)
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
                ChangeFireDirection(movement.x, 0);
            }
        }
        private void MouseMove()
        {
            var aim = camera.ScreenToWorldPoint(Input.mousePosition);
            aim.z = 0;
            ChangeFireDirection(aim.x, transform.position.x);
            
            crossHair.transform.position = aim;
        }

        private void ChangeFireDirection(float x, float border)
        {
            if (x < border)
            {
                spriteRenderer.flipX = true;
                firePoint.localPosition = new Vector2(-0.255f, -0.255f);
            }
            else if(x > border)
            {
                spriteRenderer.flipX = false;
                firePoint.localPosition = new Vector2(0.255f, -0.255f);
            }
        }

        private void FixedUpdate()
        {
            //movement logic
            if(!dead)
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

        private void ReactToHit(float damage)
        {
            // Debug.Log("Player damaged");
            if (!dead)
            {
                health -= damage;
                spriteRenderer.color = Color.red;
                hud.ChangeHp(health);
                Invoke(nameof(ResetColor), 0.3f);
                // Debug.Log(health);
                if (health <= 0)
                {
                    //TODO
                    dead = true;
                    Destroy(rb);
                    // Debug.Log("dead");
                    Defeated();
                }
            }
        }

        void ResetColor()
        {
            spriteRenderer.color = Color.white;
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
            hud.AddXp(xpToAdd);
        }
        
        private void Defeated()
        {
            canMove = false;
            animator.SetTrigger("isDefeated");
        }

        private void Die()
        {
            Destroy(this.GameObject());
            //Time.timeScale = 0;
            Cursor.visible = true;
            SceneManager.LoadScene("MainMenu");
        }
    }
}