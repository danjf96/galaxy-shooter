using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.5f;
        
    [SerializeField]
    private int powerupID;// 0 = triple shot 1 = speed boost 2 = shield
                          // Update is called once per frame

    [SerializeField]
    private AudioClip _clip;
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y < -7 )
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Collided with: "+ other.name);

        if(other.tag == "Player"){
            Player player  = other.GetComponent<Player>();
            
            if(player != null){
                //enabled power
                player.PowerUpOn(powerupID);
                AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
            }

            //detroy ourself
            Destroy(this.gameObject);

        }
    }
}
