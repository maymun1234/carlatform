using UnityEngine;
using UnityEngine.SceneManagement;



public class CollisionHandler : MonoBehaviour
{
    public string collisionTag = "YourCollisionTag"; // Temas edilecek objelerin etiketi
    public GameObject gsameoverscreen;
  

    // Temas algılandığında çağrılan metot
    void OnCollisionEnter(Collision collision)
    {
        // Temas edilen objenin etiketini kontrol et
        if (collision.collider.CompareTag(collisionTag))
        {
            // Oyun zamanını sıfırla
            //ResetGameTime();
            // Oyun sahnesini sıfırla
            gsameoverscreen.SetActive(true);
            ResetGameTime();
            
        }
    }


    public void ResetGameTime()
    {
        Time.timeScale = 0f;
    }

    // Oyun sahnesini sıfırla
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
