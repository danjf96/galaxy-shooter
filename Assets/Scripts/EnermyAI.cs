using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnermyAI : MonoBehaviour
{
    //variable for your speed
    [SerializeField]
    private float _speed = 2.5f;
    private float eixoY = 6.37f;

    [SerializeField]
    private GameObject _enemyExplosion;
    
    [SerializeField]
    private AudioClip _clip;
    
    private UIManager _uiManager;
    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //move down    
        Movement();
        //when off the screen on the bottom

        //respawm back on top with a new x position between the bounds of the screen
    }

    private void Movement()
    {
        float mySpeed = _speed;

        transform.Translate(Vector3.down * mySpeed * Time.deltaTime);

        if (transform.position.y <= ( eixoY * -1 ) )
        {
            //newPosition();
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
                {
                    Player player = collision.GetComponent<Player>();
                    player.Damage();
                    Instantiate(_enemyExplosion, transform.position, Quaternion.identity);
                    //newPosition();
                    AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
                    Destroy(this.gameObject);
                    break;
                }
            case "Laser":
                {
                    Destroy(collision.gameObject);
                    Instantiate(_enemyExplosion, transform.position, Quaternion.identity);
                    //newPosition();
                    _uiManager.UpdateScore();
                    AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
                    Destroy(this.gameObject);
                    break;
                }
        }
    }

    private void newPosition()
    {
        float randomX = Random.Range(-7.78f, 7.78f);
        transform.position = new Vector3(randomX, eixoY, 0);
    }

}
