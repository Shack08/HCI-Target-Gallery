using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform player;
    public GameObject prefab;

    [Range(0, 360)]
    public float arcSize;

    public bool shot = true;
    public float radius;
    public float height;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(shot)
        {
            shot = false;
            float theta = Random.Range(0,Mathf.Deg2Rad*arcSize);
            Instantiate(prefab, new Vector3(radius*Mathf.Sin(-theta/2), height, radius*Mathf.Cos(theta/2)), Quaternion.identity);
        }
           
    }
}
