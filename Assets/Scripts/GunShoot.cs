using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    public bool aimAssist;
    public float assistRadius;

    public float damage = 10f;
    public float impactForce = 100f;
    public float range = 100f;
    public float firingRate = 15f;

    public Camera FPSCam;

    private float nextTimetoFire = 0f;

    private int missCount;
    private float accuracy;
    private float aimDuration;
    private float startTime;


    void Start()
    {
        startTime = Time.time;
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimetoFire)
        {
            nextTimetoFire = Time.time + 1f / firingRate;
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (aimAssist)
        {
            if(Physics.SphereCast(FPSCam.transform.position, assistRadius, FPSCam.transform.forward, out hit, range))
            {
                Hit(hit);
                Vector3 distance = hit.transform.position - FPSCam.transform.position;
                accuracy = distance.magnitude - assistRadius;
                

            }
        }
        else if (Physics.Raycast(FPSCam.transform.position, FPSCam.transform.forward, out hit, range))
        {
            Hit(hit);
            Vector3 distance = hit.transform.position - FPSCam.transform.position
            accuracy = distance.magnitude;
        }
        else
        {
            missCount+=1;
        }
    }

    void Hit(RaycastHit hit)
    {
        aimDuration = Time.time - startTime;
        Debug.Log(hit.transform.name);

        if (hit.rigidbody != null)
        {
            hit.rigidbody.AddForce(-hit.normal * impactForce);
        }
        Target target = hit.transform.GetComponent<Target>();
        if(target != null){
            target.TakeDamage(damage);
        }
    }
}
