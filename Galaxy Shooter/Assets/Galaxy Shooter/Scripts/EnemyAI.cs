using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    //variable for your speed
    private float _speed = 5.0f;
    private int _life = 1;
    private int _score = 0;
    [SerializeField]
    private GameObject _EnemyExplosionPrefab;
    private UIManager _uiManager;

    // Use this for initialization
    void Start () {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }
	
	// Update is called once per frame
	void Update () {
        //move down
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        //when off the screen on the bottom 
        if (transform.position.y < -7)
        {
            //respawn back on top with a new x position between the bounds of the screen
            float rndXPos = Random.Range(-7f, 7f);
            transform.position = new Vector3(rndXPos, 7, 0);            
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with" + other.name);
        if (other.tag == "Laser")
        {
            if (other.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);
            }
            Destroy(other.gameObject);
            Instantiate(_EnemyExplosionPrefab, transform.position, Quaternion.identity);
            _uiManager.UpdateScore();
            Destroy(this.gameObject);
        }
        else if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
                Debug.Log("Damaging Player");
            }
            Instantiate(_EnemyExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        
    }


}
