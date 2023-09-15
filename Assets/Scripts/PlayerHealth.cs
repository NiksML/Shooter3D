using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Photon.Pun;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;

    [PunRPC]
    public void TakingDmg(int deltaDmg)
    {
        health -= deltaDmg;
        Debug.Log("taking dmg " + health);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
