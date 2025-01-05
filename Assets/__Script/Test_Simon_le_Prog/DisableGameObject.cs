using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGameObject : MonoBehaviour
{
    public GameObject targetObject; // Le GameObject � d�sactiver et r�activer
    public float delay = 19f;       // Dur�e avant la r�activation (par d�faut 19 secondes)

    private void Start()
    {
        if (targetObject == null)
        {
            targetObject = gameObject; // Utiliser cet objet si aucun n'est assign�
        }

        Debug.Log($"Initialisation : D�sactivation de {targetObject.name} pour {delay} secondes.");

        // Lancer la coroutine pour d�sactiver et r�activer
        StartCoroutine(DisableAndReactivate());
    }

    private IEnumerator DisableAndReactivate()
    {
        // D�sactiver le GameObject
        Debug.Log($"D�sactivation de {targetObject.name}");
        targetObject.SetActive(false);

        // Attendre le d�lai sp�cifi�
        yield return new WaitForSeconds(delay);

        // R�activer le GameObject
        Debug.Log($"R�activation de {targetObject.name}");
        targetObject.SetActive(true);
    }
}