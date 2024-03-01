using UnityEngine;

public class MatchSpee: MonoBehaviour
{
    public string targetTag = "YourTargetTag"; // Eşleştirilecek objelerin etiketi

    private void OnTriggerEnter(Collider other)
    {
        // Trigger, belirlenen etikete sahip bir objeye temas ettiğinde
        if (other.CompareTag(targetTag))
        {
            Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();
            Rigidbody carRigidbody = GetComponentInParent<Rigidbody>();

            if (otherRigidbody != null && carRigidbody != null)
            {
                // Aracın x eksenindeki hızını, objenin x eksenindeki hızına eşitle
                Vector3 targetVelocity = otherRigidbody.velocity;
                targetVelocity.y = 0f; // Sadece yatay hızı eşleştireceğiz, dikey hızı önemsemiyoruz
                carRigidbody.velocity = targetVelocity;
            }
        }
    }
}
