using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Core;

public class GameManager : MonoBehaviour
{
    public GameObject charController;
    public GameObject star1;
    public GameObject star2;
    public GameState gameState;
    public Button start;
    public Button again;

    [SerializeField] public int maxStars = 5;
    // Start is called before the first frame update
    void Start()
    {
        Button startBtn = start.GetComponent<Button>();
        Button againBtn = again.GetComponent<Button>();
        startBtn.onClick.AddListener(StartGame);
        againBtn.onClick.AddListener(StartGame);

        for (int i = 0; i < maxStars; i++)
        {
            int val = Random.Range(0, 2);
            // Spawn stars
            if (val == 1)
            {
                Instantiate(star1, new Vector3(Random.Range(-10, -1), Random.Range(-2, 6), -13), Quaternion.Euler(0,90,90));
            } else
            {
                Instantiate(star2, new Vector3(Random.Range(-10, -1), Random.Range(-2, 6), -13), Quaternion.Euler(0, 90, 90));
            }
        }
    }

    // Update is called once per frame  
    void Update()
    {
        
    }

    public void StartGame()
    {
        charController.GetComponent<PlayerController>().StartGame();
        start.gameObject.SetActive(false);
    }

}
