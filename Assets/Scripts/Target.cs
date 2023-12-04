using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Target : MonoBehaviour
{   
    public Transform player;
    public bool isTargetPractice;
    public float minHeight;
    public float maxHeight;
    public float arcSize;
    public float radius;
    public float health = 10f;
    [SerializeField] private AudioSource hitAudioSource;
    public float defaultHealth;    

    public static event Action<Transform> OnTargetSpawned;
    // Start is called before the first frame update
    void Start()
    {
        defaultHealth = health;
        float height = Random.Range(minHeight, maxHeight);
        float theta = Random.Range(0, Mathf.Deg2Rad*arcSize);
        transform.position = new Vector3(radius*Mathf.Sin(-theta/2)+player.position.x, height, radius*Mathf.Cos(theta/2)+player.position.z);
        transform.LookAt(player);
        transform.forward *=-1;

            // Trigger the OnTargetSpawned event
        OnTargetSpawned?.Invoke(transform);
    }

    // Update is called once per frame
    public void TakeDamage(float amount)
    {
        health -= amount;
        if(health <= 0)
            Die();
    }
    void Die()
    {
        if(isTargetPractice){
            health = defaultHealth;
            float height = Random.Range(minHeight, maxHeight);
            float theta = Random.Range(0, Mathf.Deg2Rad*arcSize);
            gameObject.transform.position = new Vector3(radius*Mathf.Sin(-theta/2)+player.position.x, height, radius*Mathf.Cos(theta/2)+player.position.z);
            gameObject.transform.LookAt(player);
            transform.forward *=-1;
            // Trigger the OnTargetSpawned event
        OnTargetSpawned?.Invoke(transform);
        }
        else{
            Destroy(gameObject);
        }
        hitAudioSource.Play();
        Debug.Log("Target Broken");
    }
}
