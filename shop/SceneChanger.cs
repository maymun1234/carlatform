using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Gitmek istediğiniz sahnenin indeksi
    

    // Butona tıklandığında çağrılacak olan metot
    public void ChangeScene(int sceneIndex)
    {
        // Belirtilen sahneye git
        SceneManager.LoadScene(sceneIndex);
    }
}
