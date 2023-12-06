using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handleRespawn : MonoBehaviour
{
    public Transform respawnTransform;
    Rigidbody m_Rigidbody;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        Invoke("Respawn", 0.1f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Limit") Respawn();
    }

    public void Respawn()
    {
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        gameObject.transform.position = respawnTransform.position;
        m_Rigidbody.constraints = RigidbodyConstraints.None;
    }
}
