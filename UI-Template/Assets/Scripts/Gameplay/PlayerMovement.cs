using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Transform player;
        [SerializeField] private Material playerMaterial;

        [Header("Settings")]
        [SerializeField] private float moveSpeed = 5f;
        
        private Vector2 movement;

        private void Start()
        {
            RecalculateData();
            GameManager.instance.ApplySettings();
            GameManager.OnSettingsChanged += RecalculateData;
        }

        private void RecalculateData()
        {
            float scale = Random.Range(GameData.cubeScaleMin, GameData.cubeScaleMax);
            player.localScale = new Vector3(scale, scale, 1);
            
            playerMaterial.color = GameData.cubeColor;
        }
        
        private void Update()
        {
            player.position += new Vector3(movement.x, movement.y, 0) * (moveSpeed * Time.deltaTime);
        }

        private void OnEnable()
        {
            GameManager.instance.playerInputs.Player.Movement.performed += OnMovementPerfomed;
            GameManager.instance.playerInputs.Player.Movement.canceled += OnMovementCanceled;
        }
        
        private void OnDisable()
        {
            GameManager.instance.playerInputs.Player.Movement.performed -= OnMovementPerfomed;
            GameManager.instance.playerInputs.Player.Movement.canceled -= OnMovementCanceled;
        }
        
        private void OnMovement(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            var movement = obj.ReadValue<Vector2>();
            player.position += new Vector3(movement.x, movement.y, 0) * (moveSpeed * Time.deltaTime);
        }
        
        private void OnMovementPerfomed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            movement = obj.ReadValue<Vector2>();
        }
        
        private void OnMovementCanceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            movement = Vector2.zero;
        }
    }
}
