using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGameObject : MonoBehaviour
{
    public GameObject targetObject; // Le GameObject à désactiver et réactiver
    public float delay = 19f;       // Durée avant la réactivation (par défaut 19 secondes)

    private void Start()
    {
        if (targetObject == null)
        {
            targetObject = gameObject; // Utiliser cet objet si aucun n'est assigné
        }

        Debug.Log($"Initialisation : Désactivation de {targetObject.name} pour {delay} secondes.");

        // Lancer la coroutine pour désactiver et réactiver
        StartCoroutine(DisableAndReactivate());
    }

    private IEnumerator DisableAndReactivate()
    {
        // Désactiver le GameObject
        Debug.Log($"Désactivation de {targetObject.name}");
        targetObject.SetActive(false);

        // Attendre le délai spécifié
        yield return new WaitForSeconds(delay);

        // Réactiver le GameObject
        Debug.Log($"Réactivation de {targetObject.name}");
        targetObject.SetActive(true);
    }
}