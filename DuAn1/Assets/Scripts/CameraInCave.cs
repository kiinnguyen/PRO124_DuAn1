using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInCave : MonoBehaviour
{
    public Camera baseCamera;


    void Start()
    {
        baseCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        if (baseCamera != null)
        {
            ActivateCamera(baseCamera);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ActivateCamera(GetComponent<Camera>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ActivateCamera(baseCamera);
        }
    }

    private void ActivateCamera(Camera cameraToActivate)
    {
        baseCamera.gameObject.SetActive(false);
        cameraToActivate.gameObject.SetActive(true);
    }
}
