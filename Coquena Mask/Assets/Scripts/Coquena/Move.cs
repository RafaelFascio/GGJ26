
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    #region Components
    Vector3 direction;
    Transform cameraTransform;
    CharacterController controller;
    
    InputAction move;
    #endregion
    
    Vector3 input = new Vector3(0, 0, 0);
    public LayerMask groundMask;
    public bool flying;
    public float gravity;
    float targetHeight;
    float verticalVelocity;
    float currentHeight = 0f;
    float timeFlying = 0f;


    private void Awake()
    {
        gravity = 10f;
        flying = false;
        cameraTransform = Camera.main.transform;
        controller = GetComponent<CharacterController>();
        move = InputSystem.actions.FindAction("Move");
        verticalVelocity = 31f;     
        targetHeight = 7f;
    }
    public void MoveCharacter(float speed)
    {
        
        input.x = move.ReadValue<Vector2>().x;
        input.z = move.ReadValue<Vector2>().y;
        Debug.Log(move.ReadValue<Vector2>());
        direction = SetDirection();

        direction.x *= speed;
        direction.z *= speed;

        if (!flying)
        {
            if (!controller.isGrounded)
            {
                direction.y -= gravity * (Time.deltaTime + timeFlying);
                timeFlying += Time.deltaTime;
            }
            else
            {
                direction.y = 0;
                timeFlying = 0f;
            }
        }
        else {  
               
                currentHeight = GetCurrentHeight();
                
                float heightDifference = targetHeight - currentHeight;
                
                direction.y = heightDifference * verticalVelocity *Time.deltaTime;
             
        }
        
        Turn(input);
        controller.Move(direction * Time.deltaTime);
        
    }

    float GetCurrentHeight()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if(Physics.Raycast(ray, out RaycastHit hitInfo, 100,groundMask))
        {
            return hitInfo.distance;
        }
        else { Debug.Log("no choco con nada"); }
            return 100;
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
          transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 30);
        }    
    }
    
    public void TurnToMouse()
    {
        Vector3 lookDirection = GetLookDirection();
       
        Vector3 flatLook = new Vector3(lookDirection.x, 0f, lookDirection.z);
        if (flatLook.sqrMagnitude > 0.0001f)
        {
            
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(flatLook), Time.deltaTime * 30f);
        }
    }
    public Vector3 GetLookDirection()
    {
        Vector3 objective = MousePosition();

       
        objective.y = transform.position.y;

        Vector3 dir = (objective - transform.position);
        dir.y = 0f; 
        if (dir.sqrMagnitude < 0.0001f)
        {
            
            return transform.forward;
        }
        return dir.normalized;
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
