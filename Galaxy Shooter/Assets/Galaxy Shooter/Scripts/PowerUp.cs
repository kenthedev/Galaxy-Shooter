using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int powerupID; //0 = triple shot, 1 = speed boost, 2 = shields
	// Use this for initialization
	
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with" + other.name);
        if (other.tag == "Player")
        {
            //access the player
            //turn the triple shoot bool to true
            
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                //enable triple shot
                //if powerupid == 0
                if (powerupID == 0)
                {
                    player.TripleShotPowerupOn();
                }
                //enable speed boost here
                else if (powerupID == 1)
                {
                    player.SpeedBoostPowerupOn();
                }
                //enable shields
                else if (powerupID == 2)
                {
                    player.ShieldPowerupOn();
                }
            }          
            //destroy yourself
            Destroy(this.gameObject);
        }
    }
}
