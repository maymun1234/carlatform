using UnityEngine;

public class ZAxisSynchronizer : MonoBehaviour
{
    public Transform targetObject; // Eş zamanlı hareket edecek hedef obje

    void Update()
    {
        // Eğer hedef obje mevcut değilse işlemi durdur
        if (targetObject == null)
            return;

        // Hedef objenin mevcut pozisyonunu al
        Vector3 targetPosition = targetObject.position;

        // Hedef objenin z konumunu, bu objenin z konumuyla eşitle
        targetPosition.z = transform.position.z;

        // Hedef objenin pozisyonunu güncelle
        targetObject.position = targetPosition;
    
    }
}
