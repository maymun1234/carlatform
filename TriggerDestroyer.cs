using UnityEngine;
using UnityEngine.Events;

public class TriggerDestroyer : MonoBehaviour
{
    // Tetikleyiciye çarptığında yok edilecek objelerin etiketi
    public string tagToDestroy;

     [System.Serializable]
    public class ResetMethods
    {
        public UnityEvent resetEvent;
        // Diğer parametreleri buraya ekleyebilirsiniz.
    }

    public ResetMethods resetMethod;

    // Tetikleyici nesneyle başka bir obje temas ettiğinde tetiklenen metot
    private void OnTriggerEnter(Collider other)
    {
        // Temas eden objenin etiketini kontrol et ve belirlenen etiketle eşleşiyor mu diye bak
        if (other.CompareTag(tagToDestroy))
        {
            // Eğer temas eden objenin etiketi belirlenen etiketle eşleşiyorsa, objeyi yok et
            Destroy(other.gameObject);
            resetMethod.resetEvent.Invoke();
            Debug.Log("hnnhhg");
        }
    }
}
