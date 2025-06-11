using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectDistributor : XRGrabInteractable 
{
    public GameObject objectToDistribute;

    // private void OnTriggerEnter(Collider other)
    // {
    //     Debug.Log("OnTriggerEnter called with: " + other.name);
    //     var interactor = other.GetComponent<XRBaseInteractor>();
    //     if (interactor != null && interactor.interactablesSelected.Count == 0)
    //     {
    //         Debug.Log("Interactor found: " + interactor.name);
    //         SpawnObjectInHand(interactor);
    //     }
    // }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log("OnSelectEntered called with: " + args.interactorObject);
        base.OnSelectEntered(args);

        var interactor = args.interactorObject as XRBaseInteractor;
        Debug.Log("Interactor found: " + (interactor != null ? interactor.name : "None"));
        Debug.Log("Interactables selected count: " + (interactor != null ? interactor.interactablesSelected.Count : 0));
        if (interactor != null)
        {
            interactionManager.SelectExit(interactor, this);
            SpawnObjectInHand(interactor);
        }
    }

    void SpawnObjectInHand(XRBaseInteractor interactor)
    {
        Debug.Log("Spawning object in hand for interactor: " + interactor.name);
        // Instancie le fruit
        GameObject spawnedObject = Instantiate(objectToDistribute);

        // Place le fruit dans la main (au point d’attache de l’interactor)
        spawnedObject.transform.position = interactor.GetAttachTransform(null).position;
        spawnedObject.transform.rotation = interactor.GetAttachTransform(null).rotation;

        // Active le grab automatiquement
        var grabInteractable = spawnedObject.GetComponent<XRGrabInteractable>();
        Debug.Log("GrabInteractable found: " + (grabInteractable != null ? grabInteractable.name : "None"));
        if (grabInteractable != null)
        {
            interactor.interactionManager.SelectEnter(interactor, grabInteractable);
        }
    }
}