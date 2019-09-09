using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    //public or private identify
    //data type (int, floats, bool, strings)
    // every variable has a NAME
    // option value assigned

    public int _life = 3;

    //these variables determine if powerup is collected or not
    public bool canTripleShot = false;
    public bool isSpeedBoostActive = false;
    public bool isShieldActive = false;

    [SerializeField]
    private GameObject _ExplosionPrefab;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shieldGameObject;


    //firerate is 0.25f
    //canFire -- has the amount of time between firing passed?
    //Time.Time

    [SerializeField]
    private float _fireRate = 0.25f;

    private float _canFire = 0.0f;

    [SerializeField] // we use this to make a private variable appear in the inspector
    private float _speed = 5.0f; // the f at the end signifies that its a decimal

    private UIManager _uiManager;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;

    // Use this for initialization
    void Start () {
        //current pos = new position
        transform.position = new Vector3(0, 0, 0);
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager != null)
        {
            _uiManager.UpdateLives(_life);
        }
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager != null)
        {
            _spawnManager.StartSpawnRoutines();
        }
    }
	
	// Update is called once per frame, its our game logic loop
	void Update () {
        Movement();
        //if space key or left mouse button is pressed
        //spawn laser at player position
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
     
    }

    private void Shoot()
    {
        if (Time.time > _canFire)
        {
            if (canTripleShot)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);                         
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);   
            }
            _canFire = Time.time + _fireRate;
        }        
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //if speed boost enabled
        //move 1.5x the normal speed
        //else move normal speed
        if (isSpeedBoostActive == true)
        {
            transform.Translate(Vector3.right * _speed * 1.5f * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * 1.5f * verticalInput * Time.deltaTime);
        }
        else
        {
            // new Vector3(1,0,0) * 5 * 0   Time.deltaTime incorporates the realtime
            transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
        }
        


        //if player on the y is greater than 0
        //set player position on the y to 0

        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        //if player position on the x is greater than 9.5
        //position on the x needs to be -9.5

        if (transform.position.x > 9.5f)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.5f)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
    }
    public void TripleShotPowerupOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }
    public void SpeedBoostPowerupOn()
    {
        isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }
    public IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isSpeedBoostActive = false;
    }
    public void ShieldPowerupOn()
    {
        isShieldActive = true;
        _shieldGameObject.SetActive(true);

    }
    public IEnumerator ShieldPowerupDownRoutine()
    {
        yield return new WaitForSeconds(10.0f);
        isShieldActive = false;
        _shieldGameObject.SetActive(false);
    }

    public void Damage()
    {

        if (isShieldActive)
        {
            isShieldActive = false;
            _shieldGameObject.SetActive(false);
            return;
        }

        _life--;
        _uiManager.UpdateLives(_life);

        if (_life < 1)
        {
            Instantiate(_ExplosionPrefab, transform.position, Quaternion.identity);
            _gameManager.gameOver = true;
            _uiManager.ShowTitleScreen();
            Destroy(this.gameObject);
        }
  
    }

}
