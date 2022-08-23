using Photon.Pun;
using UnityEngine;

public class Interactable : MonoBehaviourPunCallbacks
{
    public float interactableArea = 5f;
    public bool usable = true;

    public virtual void InteractWithInteractable() {}
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position,interactableArea);
    }
}
