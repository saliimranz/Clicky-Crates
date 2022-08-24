using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    public ParticleSystem enemyExplosion;
    private float minSpeed = 12.0f;
    private float maxSpeed = 16.0f;

    private float maxTorque = 10;
    private float xSpawnRange = 4;
    private float ySpawnPos = -6;

    public int pointValue;
    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(),RandomTorque(),RandomTorque(), ForceMode.Impulse);

        transform.position = RandomSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 RandomForce(){
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }
    float RandomTorque(){
        return Random.Range(-maxTorque,maxTorque);
    }
    Vector3 RandomSpawnPos(){
        return new Vector3(Random.Range(-xSpawnRange,xSpawnRange), ySpawnPos);
    }

    private void OnMouseDown(){
        if(gameManager.isGameActive) {
        Destroy(gameObject);
        Instantiate(enemyExplosion , transform.position, enemyExplosion.transform.rotation);
        gameManager.UpdateScore(pointValue);}
    }
    private void OnTriggerEnter(Collider other){
        Destroy(gameObject);
        if(!gameObject.CompareTag("Bad")){
            gameManager.GameOver();
        }
    }
}
