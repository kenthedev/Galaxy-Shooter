using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private GameObject _PlayerPrefab;
    public bool gameOver = true;

    private UIManager _uiManager;

    //if gameover is true
    //if space key pressed
    //spawn player
    //gameOver is false
    //hide title screen

    
	// Use this for initialization
	void Start () {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                //spawn player
                Instantiate(_PlayerPrefab, new Vector3(0, -0.94f, 0), Quaternion.identity);
                gameOver = false;
                _uiManager.HideTitleScreen();
            }
        }
	}
}
