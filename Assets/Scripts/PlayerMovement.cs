using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour{
    float speed = 5;
    bool movementEnabled = true;
    Vector2 velocity = new Vector2();
    InputAction moveInput;
    Rigidbody2D body;
    Player player;
    void Start(){
        /*moveInput = InputSystem.actions.FindAction("Move");
        moveInput.Enable();*/
        player = GetComponent<Player>();
        moveInput = player.playerInput.actions.FindAction("Move");
        body = gameObject.GetComponent<Rigidbody2D>();
        speed = gameObject.GetComponent<Player>().stats.speed;
    }

    // Update is called once per frame
    void Update(){
        Movement();
        body.linearVelocity = velocity;
    }
    private void Movement(){
        if(movementEnabled){
            velocity.x = moveInput.ReadValue<Vector2>().x * speed;
            velocity.y = moveInput.ReadValue<Vector2>().y * speed;
        }else{
            velocity.x=0;
            velocity.y=0;
        }
    }
}
