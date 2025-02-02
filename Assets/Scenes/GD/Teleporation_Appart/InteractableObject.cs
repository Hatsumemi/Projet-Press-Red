using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InteractableObject : MonoBehaviour
{
    public string sceneToLoad; // Nom de la sc�ne � charger
    private bool isPlayerNearby = false; // Pour v�rifier si le joueur est proche

    void Update()
    {
        // V�rifie si le joueur est proche et appuie sur la touche F
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            // Charge la sc�ne sp�cifi�e
            if (!string.IsNullOrEmpty(sceneToLoad))
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // V�rifie si c'est le joueur qui entre dans la zone
        if (other.CompareTag("Untagged"))
        {
            isPlayerNearby = true;
            // Affiche un message ou une UI indiquant que F est disponible
            Debug.Log("Appuyez sur F pour interagir.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // V�rifie si c'est le joueur qui sort de la zone
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            // Cache l'indication d'interaction
            Debug.Log("Vous �tes trop loin pour interagir.");
        }
    }
}
