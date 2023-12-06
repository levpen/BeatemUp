using Beatemup.Enemy;
using Beatemup.Weapon;
using UnityEngine;

namespace Beatemup
{
    public class ProjectileScaling : Projectile
    {
        [SerializeField] private float scaleSpeed = 1;
        private Vector3 right;
        void Start() {
            base.Start();
            right = transform.right;
        }
        private void FixedUpdate()
        {
            transform.position += right * (speed * Time.deltaTime);
            // transform.Translate(new Vector3(speed * Time.fixedDeltaTime, 0, 0), Space.World);
            transform.Rotate(new Vector3(0, 0, 10));
            var curScale = transform.localScale;
            transform.localScale = new Vector3(curScale.x+scaleSpeed*Time.fixedDeltaTime, curScale.y+scaleSpeed*Time.fixedDeltaTime, 0);
        }
    }
}