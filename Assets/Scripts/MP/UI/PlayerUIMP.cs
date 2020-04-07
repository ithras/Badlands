using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIMP : NetworkBehaviour
{
    [SerializeField] private GameObject playerUI = null;
    public override void OnStartAuthority()
    {
        playerUI.SetActive(true);
    }
}
