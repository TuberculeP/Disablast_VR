using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleRespawn : MonoBehaviour
{
    public Transform respawnTransform;
    private Rigidbody _rigidBody;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        Invoke(nameof(Respawn), 0.1f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Limit")) Respawn();
    }

    public void Respawn()
    {
        _rigidBody.constraints = RigidbodyConstraints.FreezeAll;
        gameObject.transform.position = respawnTransform.position;
        Invoke(nameof(ReleaseConstraints), 0.1f);
    }

    private void ReleaseConstraints()
    {
        _rigidBody.constraints = RigidbodyConstraints.None;
    }
}
