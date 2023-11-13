using Beatemup.Weapon;
using UnityEngine;

namespace Beatemup
{
    public class ProjectileCircle : Projectile
    {
        [SerializeField] private SpriteRenderer circle;

        private void FixedUpdate()
        {
            var curScale = transform.localScale;
            transform.localScale = new Vector3(curScale.x+Time.fixedDeltaTime, curScale.y+Time.fixedDeltaTime, 0);
        }
        
        
        // Code for line renderer
        // [SerializeField] private LineRenderer circleRenderer;
        // void Start()
        // {
        //     Draw(100, 10f);
        // }
        //
        // private void Draw(int steps, float radius)
        // {
        //     circleRenderer.positionCount = steps+1;
        //     for (int i = 0; i < circleRenderer.positionCount; ++i)
        //     {
        //         float circleProgress = (float)i / steps;
        //
        //         float curRadian = circleProgress * 2 * Mathf.PI;
        //         float x = Mathf.Cos(curRadian)*radius;
        //         float y = Mathf.Sin(curRadian)*radius;
        //         
        //         circleRenderer.SetPosition(i, new Vector3(x, y, 0));
        //     }
        // }
    }
}
