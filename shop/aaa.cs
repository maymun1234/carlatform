using UnityEngine;
using UnityEngine.EventSystems;

public class aaa : MonoBehaviour
{
     public Transform target; // Hedef nesne
    public float rotationSpeed = 5.0f; // Döndürme hızı

    void Update()
    {
        if (!IsPointerOverUIObject())
        {
            float horizontalInput = Input.GetAxis("Mouse X");
            transform.RotateAround(target.position, Vector3.up, horizontalInput * rotationSpeed);
        }
    }

    bool IsPointerOverUIObject()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
