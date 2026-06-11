using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Анимации + контроллер (теперь с ориентацией)
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform orient;
    //Показатели
    [SerializeField] public float Speed = 3f;
    [SerializeField] public float Gravity = -9.81f;
    [SerializeField] private float JumpHeight = 3f;
    //Векторы
    private Vector3 jumpvelocity;
    [SerializeField] private Vector3 moveDirection;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    private void Update()
    {
        
        moveDirection = orient.forward * Input.GetAxis("Vertical") + orient.right * Input.GetAxis("Horizontal");
        characterController.Move(moveDirection * Time.deltaTime * Speed);

        //Прыжок
        if (characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        //Гравитация

        jumpvelocity.y += Gravity * Time.deltaTime;

        characterController.Move(jumpvelocity * Time.deltaTime);
        //Анимации бега
        if (moveDirection == Vector3.zero)
        {
            animator.SetFloat("Speed", 0f);
        }
        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetFloat("Speed", 0.8f);
        }
        else
        {
            animator.SetFloat("Speed", 1.3f);
        }


        

    }

    public void Jump()
    {
        //Прыжки

        jumpvelocity.y = Mathf.Sqrt(JumpHeight * -2.0f * Gravity);
        if (characterController.isGrounded && jumpvelocity.y < 0)
        {
            jumpvelocity.y = 0f;
        }
        else if (jumpvelocity.y == 0 && characterController.isGrounded)
        {
            animator.SetBool("Jump", false);
        }
        else if (jumpvelocity.y > 0)
        {
            animator.SetBool("Jump", true);
        }
        
    }
}
