using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Cinemachine;

public class Vcam : MonoBehaviour
{
    private GameObject camObj;
    private CinemachineVirtualCamera vc;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            camObj = new GameObject();
            camObj = GameObject.FindWithTag("Player");
            vc = camObj.GetComponent<Cinemachine.CinemachineVirtualCamera>();
            vc.Follow = camObj.transform;
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
