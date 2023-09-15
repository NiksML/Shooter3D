using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    [SerializeField] public GameObject camera;
    public PlayerController playerController;
    public MouseLook mouseLook;
    
    public void IsLocalPlayer()
    {
        playerController.enabled = true;
        mouseLook.enabled = true;
        camera.SetActive(true);
    }
}
