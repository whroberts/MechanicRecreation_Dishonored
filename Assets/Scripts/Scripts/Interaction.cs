using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] PlayerHUD hud = null;
    PlayerMovementCC playerMovementCC;

    public void Grabbed()
    {
        hud.ShowText(false);
        Destroy(this, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        playerMovementCC = other.GetComponent<PlayerMovementCC>();

        if (playerMovementCC != null)
        {
            hud.ShowText(true);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Grabbed();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        playerMovementCC = other.GetComponent<PlayerMovementCC>();

        if (playerMovementCC != null)
        {
            hud.ShowText(true);
        }
    }
}
