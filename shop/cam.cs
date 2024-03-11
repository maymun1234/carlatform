using UnityEngine;

public class cam : MonoBehaviour
{
    public Transform target; // Hedef nesne

    public float rotationSpeed = 5.0f; // Döndürme hızı

    private void Update()
    {
        if (target != null)
        {
            // Hedef noktasını al
            Vector3 targetPosition = target.position;

            // Kameranın hedef noktasına bakmasını sağla
            transform.LookAt(targetPosition);

            // Y ekseni etrafında dönme
            transform.RotateAround(targetPosition, Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}
