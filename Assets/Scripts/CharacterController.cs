using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core;

public class CharacterController : MonoBehaviour
{
    public Rigidbody rb;
    public CharacterInputAction actionMap;
    public GameState gameState;
    private bool onFloor;
    
    [SerializeField] private float forcePower = 15;
    [SerializeField] private int jumpPower = 10;
    [SerializeField] private Vector2 directionValue;

    // Start is called before the first frame update
    void Start()
    {
        actionMap = new CharacterInputAction();
        actionMap.Enable();
        gameState = GameState.PreGame;
        if (gameState == GameState.PreGame)
        {
            
            this.rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.Gameplay)
        {
            // Unfreeze player
            this.rb.constraints = RigidbodyConstraints.None;
            this.rb.constraints = RigidbodyConstraints.FreezePositionZ;
            this.rb.constraints = RigidbodyConstraints.FreezeRotationX;
            this.rb.constraints = RigidbodyConstraints.FreezeRotationY;
            this.rb.constraints = RigidbodyConstraints.FreezeRotationZ;

            directionValue = actionMap.Player.Movement.ReadValue<Vector2>();
            // Check if player is jumping
            if (directionValue.y > 0.7)
            {
                directionValue.y = jumpPower;
            }

            // Check if player is on the floor
            if (onFloor == false)
            {
                directionValue.y = 0;
            }
        }   
    }

    private void FixedUpdate()
    {
        ApplyForce();
    }

    void ApplyForce()
    {
        Vector3 applyForce = directionValue * forcePower * Time.deltaTime;
        rb.AddForce(applyForce, ForceMode.Impulse);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            onFloor = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            onFloor = false;
        }
    }

    public void ChangeGameState()
    {
        gameState = GameState.Gameplay;
    }
}
