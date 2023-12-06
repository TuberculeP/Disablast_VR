using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleBallTouch : MonoBehaviour
{

    [SerializeField]
    private Material matRouge;
    [SerializeField]
    private Material matVert;
    [SerializeField]
    private GameObject door;
    
    private Renderer _cubeRenderer;
    private MeshCollider _doorMeshCollider;
    private Transform _doorTransform;
    private Quaternion _doorOpenedRotation;
    private Quaternion _doorClosedRotation;

    void Start()
    {
        _cubeRenderer = gameObject.GetComponent<Renderer>();
        _cubeRenderer.material = matRouge;

        _doorMeshCollider = door.GetComponent<MeshCollider>();
        _doorTransform = door.transform;
        
        _doorOpenedRotation = Quaternion.Euler(0, -90, 0);
        _doorClosedRotation = Quaternion.Euler(0, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Ball")) return;
        
        _cubeRenderer.material = matVert;
        _doorTransform.rotation = _doorOpenedRotation;
        _doorMeshCollider.enabled = false;
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Ball")) return;
        
        _cubeRenderer.material = matRouge;
        _doorTransform.rotation = _doorClosedRotation;
        _doorMeshCollider.enabled = true;
    }
}
