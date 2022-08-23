using Photon.Pun;
using UnityEngine;

namespace Entity
{
    public class AndrewTateInteractable : Interactable
    {
        public GameObject prefab;
        
        public override void InteractWithInteractable()
        {
            if (usable)
            {
                PhotonNetwork.InstantiateRoomObject(prefab.name, transform.position, Quaternion.identity);
                usable = false;
            }
        }
    }
}