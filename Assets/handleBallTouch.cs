using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handleBallTouch : MonoBehaviour
{

    private Renderer cubeRenderer;
    public Material matRouge;
    public Material matVert;
    public GameObject Door;
    private Quaternion DoorOpen;
    private Quaternion DoorClosed;

    void Start()
    {
        Door = GameObject.Find("Door");
        cubeRenderer = gameObject.GetComponent<Renderer>();
        cubeRenderer.material = matRouge;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Ball")
          {
              Debug.Log("DoorOpen");
              cubeRenderer.material = matVert;
              DoorOpen = Door.transform.rotation = Quaternion.Euler(0, -90, 0);
            }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            Debug.Log("DoorClosed");
            cubeRenderer.material = matRouge;
            Door.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
