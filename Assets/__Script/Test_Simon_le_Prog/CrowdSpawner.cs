using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdSpawner : MonoBehaviour
{
    public Vector3 zoneSize = new Vector3(10f, 0f, 10f); // Taille de la zone
    public int numberOfInstances = 10; // Nombre de FBX � instancier
    public GameObject fbxPrefab; // Le prefab � instancier

    public List<GameObject> spawnedObjects = new List<GameObject>(); // Objets instanci�s

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, zoneSize); // Zone visible dans la sc�ne
    }

    public void SpawnCrowd()
    {
        if (fbxPrefab == null)
        {
            Debug.LogError("Aucun prefab assign� !");
            return;
        }

        for (int i = 0; i < numberOfInstances; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-zoneSize.x / 2, zoneSize.x / 2),
                1f, // Hauteur visible
                Random.Range(-zoneSize.z / 2, zoneSize.z / 2)
            );

            GameObject instance = Instantiate(fbxPrefab, transform.position + randomPosition, Quaternion.identity);
            spawnedObjects.Add(instance);

            Debug.Log($"Objet {i + 1} cr�� � : {transform.position + randomPosition}");
        }

        Debug.Log($"Nombre total d'objets cr��s : {spawnedObjects.Count}");
    }

    [ContextMenu("Spawn Crowd")]
    public void TestSpawnCrowd()
    {
        SpawnCrowd();
    }
}