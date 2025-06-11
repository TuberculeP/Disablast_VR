using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InfiniteGrab : MonoBehaviour
{
    public GameObject prefabToSpawn; // Ton objet à lancer
    void Update()
    {
        // au grab avec le controleur VR
        if (Input.GetButtonDown("Fire1")) // Assurez-vous que "Fire1" est mappé à l'action de grab dans les paramètres d'entrée
        {
            SpawnAndGrab();
        }
    }

    void SpawnAndGrab()
    {        // Crée une instance de l'objet à la position du contrôleur
        GameObject spawnedObject = Instantiate(prefabToSpawn, transform.position, transform.rotation);
        
        // Récupère le XR Grab Interactable du prefab
        XRGrabInteractable grabInteractable = spawnedObject.GetComponent<XRGrabInteractable>();

        if (grabInteractable != null)
        {
            // Simule le grab
            XRBaseInteractor interactor = FindObjectOfType<XRBaseInteractor>();
            if (interactor != null)
            {
                grabInteractable.interactionManager.SelectEnter(interactor, grabInteractable);
            }
            else
            {
                Debug.LogWarning("Aucun XRBaseInteractor trouvé dans la scène.");
            }
        }
        else
        {
            Debug.LogWarning("Le prefab n'a pas de XRGrabInteractable attaché.");
        }
    }
}
