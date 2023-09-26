using UnityEngine;

namespace Beatemup
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed = 50f;
        [SerializeField] private CharacterController controller;

        public void Update()
        {
            var horizInp = Input.GetAxis("Horizontal");
            var vertInp = Input.GetAxis("Vertical");


            Vector3 movement = new Vector2(horizInp, vertInp);
            movement = transform.TransformDirection(movement) * (speed * Time.deltaTime);

            //movement = Vector3.ClampMagnitude(movement, speed);

            controller.Move(movement);
        }
    }
}