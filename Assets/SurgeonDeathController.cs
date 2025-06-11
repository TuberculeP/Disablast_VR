using UnityEngine;

public class SurgeonDeathController : MonoBehaviour
{
    public WaveManager waveManager;

    private System.Action deathCallback;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grenade"))
        {
            deathCallback?.Invoke();
            Destroy(other.gameObject);
        }
    }

    public void SetOnDeathCallback(System.Action callback)
    {
        // This method can be used to set a callback for when the surgeon dies.
        // The callback can be invoked in the OnTriggerEnter method if needed.
        // For now, we will not implement this as it is not required by the current logic.
        deathCallback = callback;
    }
}
