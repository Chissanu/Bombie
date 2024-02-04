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
    public Button again;
    // Start is called before the first frame update
    void Start()
    {
        Button startBtn = start.GetComponent<Button>();
        Button againBtn = again.GetComponent<Button>();
        startBtn.onClick.AddListener(StartGame);
        againBtn.onClick.AddListener(StartGame);
    }

    // Update is called once per frame  
    void Update()
    {
        
    }

    public void StartGame()
    {
        charController.GetComponent<CharacterController>().StartGame();
        start.gameObject.SetActive(false);
    }

}
