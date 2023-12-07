using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ForceRespawn : MonoBehaviour
{
    [SerializeField] private GameObject objectToRespawn;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Test");
        if (!other.gameObject.CompareTag("Hand")) return;
        objectToRespawn.GetComponent<HandleRespawn>().Respawn();
    }
}
