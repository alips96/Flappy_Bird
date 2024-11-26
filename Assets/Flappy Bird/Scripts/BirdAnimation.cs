using Unity.Netcode;
using UnityEngine;

public class BirdAnimation : NetworkBehaviour
{
    [SerializeField] private RuntimeAnimatorController playerAnimatorController;

    private void Start()
    {
        if(IsOwner)
            GetComponent<Animator>().runtimeAnimatorController = playerAnimatorController;
    }
}
