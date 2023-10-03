using UnityEngine;

namespace Beatemup.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed = 50f;
        [SerializeField] private CharacterController controller;
        //[SerializeField] private float mouseSpeed = 50f;
        [SerializeField] private GameObject crossHair;

        [SerializeField] private new Camera camera;

        private void Awake()
        {
            Cursor.visible = false;
        }

        public void Update()
        {
            var horizInp = Input.GetAxis("Horizontal");
            var vertInp = Input.GetAxis("Vertical");
            MouseMove();

            Vector3 movement = new Vector2(horizInp, vertInp);
            movement = transform.TransformDirection(movement) * (speed * Time.deltaTime);

            movement = Vector3.ClampMagnitude(movement, speed);
            

            controller.Move(movement);
        }

        private void MouseMove()
        {
            var aim = camera.ScreenToWorldPoint(Input.mousePosition);
            aim.z = 0;
            crossHair.transform.position = aim;
        }
    }
}