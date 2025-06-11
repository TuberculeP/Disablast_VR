using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectDistributor : MonoBehaviour
{
    public GameObject objectToDistribute;
    public AudioClip debugSound1;
    public AudioClip debugSound2;
    

    private void OnTriggerEnter(Collider other)
    {
        var interactor = other.GetComponent<XRBaseInteractor>();
        if (interactor != null && interactor.interactablesSelected.Count == 0)
        {
            SpawnObjectInHand(interactor);
        }
    }

    void SpawnObjectInHand(XRBaseInteractor interactor)
    {
        // Instancie le fruit
        GameObject spawnedObject = Instantiate(objectToDistribute);

        // Place le fruit dans la main (au point d’attache de l’interactor)
        spawnedObject.transform.position = interactor.GetAttachTransform(null).position;
        spawnedObject.transform.rotation = interactor.GetAttachTransform(null).rotation;

        // Active le grab automatiquement
        var grabInteractable = spawnedObject.GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            interactor.interactionManager.SelectEnter(interactor, grabInteractable);
        }
    }
}