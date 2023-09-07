using UnityEngine;

public class GravityComponent : MonoBehaviour
{
    private const float gravity = -9.81f;

    [SerializeField] private float gravityScale = 3f;

    private Vector3 gravityVelocity;
    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        gravityVelocity.y += gravity * gravityScale * Time.deltaTime;
        characterController.Move(gravityVelocity);
    }
}
