using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Move : MonoBehaviour
{
    #region Components
    Vector3 direction;
    Transform cameraTransform;
    CharacterController controller;
    
    InputAction move;
    #endregion
    Vector3 input = new Vector3(0, 0, 0);
    bool flying = false;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
        controller = GetComponent<CharacterController>();
        move = InputSystem.actions.FindAction("Move");
    }
    public void MoveCharacter(float speed)
    {
        input.x = move.ReadValue<Vector2>().x;
        input.z = move.ReadValue<Vector2>().y;
        direction = SetDirection();

        direction.x *= speed;
        direction.z *= speed;

        if (!flying)
        {
            if (!controller.isGrounded)
            {
                direction.y -= 9.81f * Time.deltaTime;
            }
            else
            {
                direction.y = 0;
            }

        }
        //   Debug.Log(controller.isGrounded);
        Turn(input);
        controller.Move(direction * Time.deltaTime);
        
    }
    Vector3 SetDirection()
    {
        Vector3 inputDirection = new Vector3(input.x, 0, input.z).normalized;
        if (cameraTransform != null)
        {
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;
            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();
            return forward * inputDirection.z + right * inputDirection.x;
        }
        else
        {
            return inputDirection;
        }
    }
    public bool IsMoving()
    {
        return input.x != 0 || input.z != 0;
    }
    void Turn(Vector3 input)
    {    
        if (input.z != 0 || input.x != 0) 
        {
          transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 8);
        }    
    }
   public void TurnToMouse() 
    {
        Vector3 lookDirection = GetLookDirection();    
        transform.rotation = Quaternion.LookRotation(lookDirection);
    }
    public Vector3 GetLookDirection() 
    {
        Vector3 objective = MousePosition();
        objective.y = transform.position.y;
        return (objective - transform.position).normalized;
    }
    public  Vector3 MousePosition()
    {
        Vector3 mousePos = new Vector2(0, 0);
        Vector3 screenPos;
        if (Mouse.current != null)
        {
            screenPos = Mouse.current.position.ReadValue();
        }
        else
        {
            
            screenPos = UnityEngine.Input.mousePosition;
            Debug.Log("Using legacy input for mouse position");
        }
       
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(screenPos.x, screenPos.y,0 ));
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            mousePos = raycastHit.point;
            
        }
        
        return mousePos;
    }
    
}
