using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using System.IO;

public class GunShoot : MonoBehaviour
{
    public bool aimAssist;
    [SerializeField] private float assistRadius;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float impactForce = 100f;
    [SerializeField] private float range = 100f;
    [SerializeField] private float firingRate = 15f;
    [SerializeField] private int trialCount;
    [SerializeField] private int maxBlocks;
    public Camera FPSCam;

    private float nextTimetoFire = 0f;
    private int missCount;
    private int hitCount;
    private int trialsNum;
    private int blockCount;
    private float accuracy;
    private float aimDuration;
    private float startTime;
    private bool isBreak;
    private string path;

    [SerializeField] private AudioSource fireAudioSource;

    [SerializeField] private GameObject breakUI;
    [SerializeField] private MouseLook mouseLook;
    [SerializeField] private TextMeshProUGUI blockText;
    [SerializeField] private TextMeshProUGUI trialText;

    public MouseLook mouseLookScript;
    public AnalogJoystickController analogJoystickControllerScript;
    public GyroAim gyroAimScript;
    public GyroAImNoAimAssist gyroAimNoAimAssistScript;
    private Vector3 initialAimPoint;
    private float distanceBetweenReticleAndNewTarget;
    private MonoBehaviour[] inputComponents;
    void Start()
    {
            // Get the path of the file
        string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
        string directoryPath = Path.Combine(Application.dataPath, "data");
        path = Path.Combine(directoryPath, $"saveData_{timestamp}.csv");

        // Ensure the directory exists
        Directory.CreateDirectory(directoryPath);
        // Define your headers
        string headers = "InputType,BlockCount,TrialsNum,HitCount,MissCount,Accuracy,DistanceBetweenReticleAndNewTarget,AimDuration\n";

        // Write the headers to the file
        File.WriteAllText(path, headers);

        inputComponents = new MonoBehaviour[] {  mouseLookScript, analogJoystickControllerScript, gyroAimScript, gyroAimNoAimAssistScript};
        trialText.text = UpdateText("trials",trialsNum, trialCount);
        blockText.text = UpdateText("blocks",blockCount, maxBlocks);
        startTime = Time.time;
    }
    private void Update()
    {

            // Cast a ray from the camera's position in the direction the camera is facing
        RaycastHit hit;
        if (Physics.Raycast(FPSCam.transform.position, FPSCam.transform.forward, out hit, range))
        {
            // If the ray hits an object, store the hit point as the initial aim point
            initialAimPoint = hit.point;
        }
        else
        {
            // If the ray doesn't hit an object, store the point at the end of the ray
            initialAimPoint = FPSCam.transform.position + FPSCam.transform.forward * range;
        }

        // Debug.Log(blockCount);
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimetoFire)
        {
            nextTimetoFire = Time.time + 1f / firingRate;
            if(!isBreak)
                Shoot();
                // If the firing sound is already playing, stop it
            if (fireAudioSource.isPlaying)
            {
                fireAudioSource.Stop();
            }

            // Play the firing sound
            fireAudioSource.Play();
        }
    }

    public string UpdateText(string prompt, int currentCount, int totalCount)
    {
        return prompt + " " + currentCount + "/" + totalCount;
    }

    public void CheckForBreak()
    {
        hitCount+=1;
        trialsNum+=1;
        startTime = Time.time;
       int controllerIndex = PlayerPrefs.GetInt("ControllerIndex", 0);
       string inputType = inputComponents[controllerIndex].GetType().Name;

        // Create a string with the data you want to save
        string data = ""+inputType+","+(blockCount+1) + "," + trialsNum + "," + hitCount + "," + missCount + "," + accuracy + "," + distanceBetweenReticleAndNewTarget+","+aimDuration+"\n";
        Debug.Log(data);
        SaveData(data);
        missCount = 0;
        if(hitCount>=trialCount)
        {
            blockCount+=1;
            breakUI.SetActive(true);
            mouseLook.VisualizeCursor(true);
            hitCount = 0;
            Time.timeScale = 0;
            isBreak = true;
            if (blockCount>=maxBlocks)
            {
                    // Get the path of the file
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                string directoryPath = Path.Combine(Application.dataPath, "data");
                path = Path.Combine(directoryPath, $"saveData_{timestamp}.csv");

                // Ensure the directory exists
                Directory.CreateDirectory(directoryPath);
                AfterBreak();
                gameObject.GetComponent<ChangeScene>().LoadScene();
            }
        }
        trialText.text = UpdateText("trials",trialsNum, trialCount);
        blockText.text = UpdateText("blocks",blockCount, maxBlocks);
    }

    public void SaveData(string data)
    {
         //Write the data to the file
        File.AppendAllText(path, data);
    }

    public void AfterBreak()
    {
        Debug.Log("AfterBreak method called");
        startTime = Time.time;
        Time.timeScale = 1.0f;
        isBreak = false;
        missCount = 0;
        trialsNum = 0;
        trialText.text = UpdateText("trials",trialsNum, trialCount);
        
    }
    void Shoot()
{
    RaycastHit hit;
    int controllerIndex = PlayerPrefs.GetInt("ControllerIndex", 0);
    string inputType = inputComponents[controllerIndex].GetType().Name;

    if (aimAssist && inputType == "GyroAim")
    {
        if(Physics.SphereCast(FPSCam.transform.position, assistRadius, FPSCam.transform.forward, out hit, range))
        {
            
            Vector3 distanceVec = hit.transform.position - hit.point;
            if(distanceVec.magnitude > assistRadius)
                accuracy = distanceVec.magnitude - assistRadius;
            else
                accuracy = 0;
            Hit(hit);
            CheckForBreak();
        }
        else
        {
            missCount+=1;
        }
    }
    else if (Physics.Raycast(FPSCam.transform.position, FPSCam.transform.forward, out hit, range))
    {
        
        Vector3 distanceVec = hit.transform.position - hit.point;
        accuracy = distanceVec.magnitude;
        Hit(hit);
        CheckForBreak();
    }
    else
    {
        missCount+=1;
    }
}

    void Hit(RaycastHit hit)
    {
        aimDuration = Time.time - startTime;
        // Debug.Log(hit.transform.name);

        if (hit.rigidbody != null)
        {
            hit.rigidbody.AddForce(-hit.normal * impactForce);
        }
        Target target = hit.transform.GetComponent<Target>();
        if(target != null){
            target.TakeDamage(damage);
        }
    }

        void OnEnable()
    {
        Target.OnTargetSpawned += HandleTargetSpawned;
    }

    void OnDisable()
    {
        Target.OnTargetSpawned -= HandleTargetSpawned;
    }

    void HandleTargetSpawned(Transform targetTransform)
    {
        // Calculate the distance between the initial aim point and the new target's position
        distanceBetweenReticleAndNewTarget = Vector3.Distance(initialAimPoint, targetTransform.position);
        Debug.Log("Distance between initial aim point and new target: " + distanceBetweenReticleAndNewTarget);
    }
}
