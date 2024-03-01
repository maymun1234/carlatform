using UnityEngine;

public class MatchSpeed : MonoBehaviour
{
    public GameObject a1; // a1 nesnesi
    public string a2Tag = "YourTag"; // a2 nesnelerinin etiketi

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(a2Tag)) // Eğer etkileşime giren nesne belirli bir tag'e sahipse
        {
            MakeA1ChildOfA2(other.gameObject); // a1'i etkileşime giren nesnenin çocuğu yap
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(a2Tag)) // Eğer etkileşimden çıkan nesne belirli bir tag'e sahipse
        {
            // Eğer a1 a2Object'in bir çocuğu ise, a1'in parent'ını null yap
            if (a1.transform.parent == other.transform)
            {
                MakeA1Independent();
            }
        }
    }

    private void MakeA1ChildOfA2(GameObject a2Object)
    {
        // Eğer a1 veya a2 null ise işlemi gerçekleştirme
        if (a1 == null || a2Object == null)
        {
            Debug.LogWarning("a1 veya a2 nesneleri atanmamış!");
            return;
        }

        // a1 nesnesini a2Object'in çocuğu yap
        a1.transform.parent = a2Object.transform;
    }

    private void MakeA1Independent()
    {
        // Eğer a1 null değilse ve ebeveyni a2Object ise
        if (a1 != null && a1.transform.parent != null)
        {
            // a1 nesnesinin ebeveynini kaldır
            a1.transform.parent = null;
        }
    }
}
