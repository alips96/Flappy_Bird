using Unity.Netcode;
using UnityEngine;

public class BirdAnimation : NetworkBehaviour
{
    [SerializeField] private RuntimeAnimatorController playerAnimatorController;

    void Start()
    {
        if(IsOwner)
            GetComponent<Animator>().runtimeAnimatorController = playerAnimatorController;
    }
}
