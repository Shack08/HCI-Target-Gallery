using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public bool isTargetPractice;
    public float health = 10f;
    public float defaulthealth;

    private void Start()
    {
        defaulthealth = health;
    }

    public void TakeDamage(float amount) 
    {
        health -= amount;
        if (health <= 0) {
            Die();
        }
    }

    void Die() 
    {
        if (isTargetPractice)
        {
            health = defaulthealth;
            gameObject.transform.position = new Vector3(Random.Range(-7.5f,7), 7, Random.Range(-9, 7.7f));
        }
        else {
            Destroy(gameObject);
        }
        
        Debug.Log("Target Broken");
    }
}
