using Unity.VisualScripting;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player;
    public float sensitivity = 33;
    public Transform orient;
    float x;
    float y;
    float hor;
    float vert;

   
    // Update is called once per frame
    private void Update()
    {

        PlayerInput();

        // Поворачиваем ориентацию (не ржать)
        Vector3 ViewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z) ;
        orient.forward = ViewDir.normalized;

        //Поворачиваем игрока
        hor = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");
        Vector3 inputDir = orient.forward * vert + orient.right * hor;

        if (inputDir != Vector3.zero)
        {
            //Слёрпаем
            player.forward = Vector3.Slerp(player.forward, inputDir.normalized, Time.deltaTime * sensitivity);
        }

    }

    // Закрепление курсора
    private void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
