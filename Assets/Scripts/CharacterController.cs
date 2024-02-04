using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core;
using Unity.VisualScripting;

public class CharacterController : MonoBehaviour
{
    public Rigidbody rb;
    public CharacterInputAction actionMap;
    public GameState gameState;
    public GameObject bomb;
    public List<GameObject> bombList = new List<GameObject>();
    public int maxBombs = 5;
    public GameObject gameOver;

    private float isAttacking = 0f;
    private int currentBomb = 0;
    private int currentBombPointer = 0;
    private bool canPress = true;
    
    [SerializeField] private float forcePower = 15;
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
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezePositionZ;
            rb.constraints = RigidbodyConstraints.FreezeRotationX;
            rb.constraints = RigidbodyConstraints.FreezeRotationY;
            rb.constraints = RigidbodyConstraints.FreezeRotationZ;

            directionValue = actionMap.Player.Movement.ReadValue<Vector2>();
            isAttacking = actionMap.Player.Attack.ReadValue<float>();

            if (this.transform.position.x <= -20)
            {
                rb.velocity = Vector3.zero;
                this.gameObject.transform.SetPositionAndRotation(new Vector3(-20,this.transform.position.y,this.transform.position.z), Quaternion.identity);
            }
            if (this.transform.position.y >= 6.5)
            {
                rb.velocity = Vector3.zero;
                this.gameObject.transform.SetPositionAndRotation(new Vector3(this.transform.position.x, 6.5f, this.transform.position.z), Quaternion.identity);
            }

            if (isAttacking >= 0.1 && canPress)
            {
                Attack();
            }
        }
    }

    private void FixedUpdate()
    {
        ApplyForce();
    }

    void ApplyForce()
    {
        // Reset Plane rotation
        if (directionValue.x == 0 && directionValue.y == 0)
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        // Plane looking up/down
        if (directionValue.y >= 0.1)
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 10);
        } else if (directionValue.y <= -0.1)
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, -10);
        }
        // Plane rotate wings
        if (directionValue.x >= 0.1 && directionValue.y >= 0.1)
        {
            this.gameObject.transform.rotation = Quaternion.Euler(-40, 0, 10);
        }
        else if (directionValue.x >= 0.1 && directionValue.y <= -0.1)
        {
            this.gameObject.transform.rotation = Quaternion.Euler(-40, 0, 10);
        }

        // Apply force
        Vector3 applyForce = directionValue * forcePower * Time.deltaTime;
        rb.AddForce(applyForce, ForceMode.Impulse);
    }

    void Attack()
    {
        StartCoroutine(ButtonDelayCoroutine());
        if (currentBomb != maxBombs)
        {
            Vector3 currentPos = this.transform.position;
            currentPos.x += 0.1f;
            currentPos.y -= 0.3f;
            var newBomb = Instantiate(bomb, currentPos, this.transform.rotation);
            bombList.Add(newBomb);
            currentBomb += 1;
        } else
        {
            if (currentBombPointer <= maxBombs - 1)
            {
                Debug.Log("Cur");
                Debug.Log(currentBombPointer);
                bombList[currentBombPointer].GetComponent<Rigidbody>().velocity = Vector3.zero;
                bombList[currentBombPointer].transform.position = this.transform.position;
                currentBombPointer++;
            } else
            {
                currentBombPointer = 0;
            }
        }
        
    }
    public void StartGame()
    {
        this.gameObject.transform.position = new Vector3(-16.09f, -0.27f, -12.55f);
        this.rb.useGravity = false;
        gameState = GameState.Gameplay;
        gameOver.SetActive(false);
        this.rb.velocity = Vector3.zero;

        currentBomb = 0;
        currentBombPointer = 0;
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            StartCoroutine(DelayDeathSceneCoroutine());
        }
    }

    private IEnumerator ButtonDelayCoroutine()
    {
        // Disable button press
        canPress = false;

        // Wait for the delay (adjust the time accordingly)
        yield return new WaitForSeconds(0.5f); // Adjust the delay time as needed

        // Enable button press after the delay
        canPress = true;
    }

    private IEnumerator DelayDeathSceneCoroutine()
    {
        gameState = GameState.Postgame;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(0, -20, 0, ForceMode.Force);
        yield return new WaitForSeconds(2f);
        GameOver();
    }
}
