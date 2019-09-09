using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Sprite[] _lives;
    public Image _livesImageDisplay;
    public GameObject _titleScreen;

    public Text _scoreText;
    public int _score;
    //update our display
    public void UpdateLives(int currentLives)
    {
        Debug.Log("Player lives: " + currentLives);
        //swap out current img src for one within the _lives array
        _livesImageDisplay.sprite = _lives[currentLives];
    }
    public void UpdateScore()
    {
        _score += 100;
        _scoreText.text = "Score: " + _score;
    }
    public void ShowTitleScreen()
    {
        _titleScreen.SetActive(true);
    }
    public void HideTitleScreen()
    {
        _titleScreen.SetActive(false);
        _scoreText.text = "Score: ";
    }

}
