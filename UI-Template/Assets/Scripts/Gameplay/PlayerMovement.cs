using System;
using UnityEngine;

namespace Gameplay
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;

        private void OnEnable()
        {
            GameManager.instance.playerInputs.Player.Movement.performed += OnMovement;
        }
        
        private void OnDisable()
        {
            GameManager.instance.playerInputs.Player.Movement.performed -= OnMovement;
        }
        
        private void OnMovement(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            var movement = obj.ReadValue<Vector2>();
            transform.position += new Vector3(movement.x, movement.y, 0) * (moveSpeed * Time.deltaTime);
        }
    }
}
