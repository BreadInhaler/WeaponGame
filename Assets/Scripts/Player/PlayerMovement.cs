using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour{
    float speed = 5;
    bool movementEnabled = true;
    Vector2 velocity = new Vector2();
    InputAction moveInput;
    Rigidbody2D body;
    Player player;
    public SpriteRenderer sprite;
    void Start(){
        /*moveInput = InputSystem.actions.FindAction("Move");
        moveInput.Enable();*/
        player = GetComponent<Player>();
        moveInput = player.playerInput.actions.FindAction("Move");
        moveInput.Enable();
        body = gameObject.GetComponent<Rigidbody2D>();
        speed = gameObject.GetComponent<Player>().stats.speed;
    }

    // Update is called once per frame
    void Update(){
        bool isWalking = velocity.x!=0 || velocity.y!=0;
        sprite.flipX = velocity.x>=0;
        player.animator.SetBool("isWalking",isWalking);
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
