using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool _canTripleFire = false;
    public bool _moreSpeed = false;
    public bool _shield = false;
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 5.0f;
    
    [SerializeField]
    public int _lives = 3;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _animationExplosion;
    [SerializeField]
    private GameObject _animationShield;
    [SerializeField]
    private GameObject[] _engines;

    //fireRate is 0.25f
    //caFire -- has the amount of tuime between firing passed?
    //Time.time
    [SerializeField]
    private float _fireRate = 0.25f;
    
    private float _canFire = 0.0f;
    //public float horizontalInput;
    private UIManager _uiManager;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;
    private AudioSource _audioSource;

    private int hitCount;
    void Start()
    {
       transform.position = new Vector3(0,0,0);
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _audioSource = GetComponent<AudioSource>();

        if (_uiManager != null)
        {
            _uiManager.UpdateLives(_lives);
        }

        if(_spawnManager != null)
        {
            _spawnManager.StartSpawnRoutines();
        }
        hitCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
            Shoot();
        }

        //if (_shield)
        //{
        //    Instantiate(_animationShield, transform.position, Quaternion.identity);
        //}
    }

    private void Shoot(){
        if(Time.time > _canFire){
            _audioSource.Play();
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);

            if(_canTripleFire){
                Instantiate(_laserPrefab, transform.position + new Vector3(0.59f, 0.011f, 0), Quaternion.identity);
                Instantiate(_laserPrefab, transform.position + new Vector3(-0.59f, 0.011f, 0), Quaternion.identity);
            }

            _canFire = Time.time + _fireRate;

        } 
    }

    private void Movement(){

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float mySpeed =  _moreSpeed ? (_speed * 2.5f) : _speed;

        transform.Translate(Vector3.right * mySpeed * horizontalInput * Time.deltaTime);
        transform.Translate(Vector3.up * mySpeed * verticalInput * Time.deltaTime);

        if( transform.position.y > 0 ) {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if( transform.position.y < -4.2f){
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        if( transform.position.x > 9.50f ){
            transform.position = new Vector3( -9.50f, transform.position.y, 0);
        } else if ( transform.position.x < -9.50f ) {
            transform.position = new Vector3( 9.50f, transform.position.y, 0);
        }
    }

    public void PowerUpOn(int powerID)
    {

        enabledPower(powerID, true);
        StartCoroutine(PowerDownRoutine(powerID));
    }

    public IEnumerator PowerDownRoutine(int powerID)
    {

        yield return new WaitForSeconds(5.0f);
        enabledPower(powerID, false);
        
    }

    public void Damage()
    {
        if (_shield == true)
        {
            enabledPower(2, false);
            return;
        }

        hitCount++;
        if(hitCount == 1)
        {
            _engines[0].SetActive(true);
        }else if(hitCount == 2)
        {
            _engines[1].SetActive(true);

        }

        _lives--;
        _uiManager.UpdateLives(_lives);
        
        if(_lives == 0)
        {
            Instantiate(_animationExplosion, transform.position, Quaternion.identity);
            _gameManager.gameOver = true;
            _uiManager.ShowTitleScreen(true);
            Destroy(this.gameObject);
        }
    }

    private void enabledPower(int powerID, bool enabled)
    {
        switch (powerID)
        {
            case 0:
                {
                    _canTripleFire = enabled;
                    break;
                }
            case 1:
                { // enable speed
                    _moreSpeed = enabled;
                    break;
                }
            case 2:
                { // enable shields
                    _shield = enabled;
                    _animationShield.SetActive(enabled);
                    break;
                }
        }
    }

}

