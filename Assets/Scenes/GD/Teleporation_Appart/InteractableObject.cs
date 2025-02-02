using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InteractableObject : MonoBehaviour
{
    public string sceneToLoad; // Nom de la scène à charger
    private bool isPlayerNearby = false; // Pour vérifier si le joueur est proche

    void Update()
    {
        // Vérifie si le joueur est proche et appuie sur la touche F
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            // Charge la scène spécifiée
            if (!string.IsNullOrEmpty(sceneToLoad))
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si c'est le joueur qui entre dans la zone
        if (other.CompareTag("Untagged"))
        {
            isPlayerNearby = true;
            // Affiche un message ou une UI indiquant que F est disponible
            Debug.Log("Appuyez sur F pour interagir.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Vérifie si c'est le joueur qui sort de la zone
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            // Cache l'indication d'interaction
            Debug.Log("Vous êtes trop loin pour interagir.");
        }
    }
}
