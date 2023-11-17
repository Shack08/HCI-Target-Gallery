using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{   
    public bool isTargetPractice;
    public float health = 10f;
    public float defaultHealth;    
    // Start is called before the first frame update
    void Start()
    {
        defaultHealth = health;
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
            gameObject.transform.position = new Vector3(Random.Range(-8,7), 5, Random.Range(4,10));
        }
        else{
            Destroy(gameObject);
        }
        Debug.Log("Target Broken");
    }
}
