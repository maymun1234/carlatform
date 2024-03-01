using UnityEngine;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // Klonlamak istediğiniz GameObject
    public int numberOfClones = 20; // Klon sayısı
    public float offset = 6f; // Y eksenindeki ofset miktarı

    private List<GameObject> spawnedObjects = new List<GameObject>(); // Oluşturulan objelerin listesi
    private Vector3 lastSpawnPosition; // Son spawnlanan objenin pozisyonu

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        // İlk objeyi spawnlamak için pozisyon belirleyelim
        Vector3 spawnPosition = transform.position + new Vector3(0f, 0f, 12f);
        lastSpawnPosition = spawnPosition;

        // numberOfClones sayısı kadar döngü yapalım
        for (int i = 0; i < numberOfClones; i++)
        {
            // Yeni bir obje oluşturup spawnPosition'a klonlayalım
            GameObject newObject = Instantiate(objectToSpawn, lastSpawnPosition, Quaternion.identity);
            // Oluşturulan objeyi listeye ekleyelim
            spawnedObjects.Add(newObject);

            // Yeni spawnPosition'u güncelleyelim
            lastSpawnPosition.z += offset;
        }
    }

    // Örneğin bir obje yok edildiğinde bu metot çağırılabilir
    public void ObjectDestroyed(GameObject destroyedObject)
    {
        // Eğer listede yoksa (yani zaten yok edilmişse) yeniden oluşturalım
        if (!spawnedObjects.Contains(destroyedObject))
        {
            // Yeni bir obje oluşturup lastSpawnPosition'a klonlayalım
            GameObject newObject = Instantiate(objectToSpawn, lastSpawnPosition, Quaternion.identity);
            // Oluşturulan objeyi listeye ekleyelim
            spawnedObjects.Add(newObject);

            // Yeni spawnPosition'u güncelleyelim
            lastSpawnPosition.z += offset;
        }
    }
}
