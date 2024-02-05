using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Core;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public GameObject charController;
    public GameObject star1;
    public GameObject star2;
    public GameState gameState;
    public GameObject platform;
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
                Instantiate(star1, new Vector3(Random.Range(-10, 0), Random.Range(-2, 0), -13), Quaternion.Euler(0,90,90));
            } else
            {
                Instantiate(star2, new Vector3(Random.Range(-10, 0), Random.Range(-2, 0), -13), Quaternion.Euler(0, 90, 90));
            }
        }
    }

    // Update is called once per frame  
    void Update()
    {
        if (charController.GetComponent<PlayerController>().GetGameState() != GameState.PreGame)
        {
            StartCoroutine(MovePlatformCoroutine());
        }
    }

    public void StartGame()
    {
        charController.GetComponent<PlayerController>().StartGame();
        start.gameObject.SetActive(false);
    }

    private IEnumerator MovePlatformCoroutine()
    {
        float posx = platform.transform.position.x;
        posx -= 0.05f;
        yield return new WaitForSeconds(0.01f);
        if (posx <= -48)
        {
            posx = 35;
        }
        platform.transform.position = new Vector3(posx, platform.transform.position.y, platform.transform.position.z);
    }

}
