using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Core;

public class GameManager : MonoBehaviour
{
    public GameObject charController;
    public GameState gameState;
    public Button start;
    // Start is called before the first frame update
    void Start()
    {
        Button startBtn = start.GetComponent<Button>();
        startBtn.onClick.AddListener(StartGame);

    }

    // Update is called once per frame  
    void Update()
    {
        
    }

    public void StartGame()
    {
        charController.GetComponent<CharacterController>().ChangeGameState();
    }

}
