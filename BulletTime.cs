using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTime : MonoBehaviour
{
    private Camera cam;
    private float minZoom=10;
    private float slowMotion = 0.01f;
    private float slowMotionLength = 1f;

    void Start()
    {
        cam = GetComponent<Camera>();
        
    }
    void Update()
    {
        Time.timeScale += (1f / slowMotionLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }

    public void bulletTime()
    {
        
        cam.fieldOfView = minZoom;
        //Time.timeScale = slowMotion;
        //Time.fixedDeltaTime = Time.timeScale * .02f;
        
    }

}
