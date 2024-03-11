using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startposition : MonoBehaviour
{
    public Vector3 baslangicKonumu;

    void Start()
    {
        // Şu anki sahnenin indeksini al
        int sahneIndeksi = SceneManager.GetActiveScene().buildIndex;

        // Eğer bu 0 nolu sahne değilse, fonksiyondan çık
        if (sahneIndeksi != 0)
        {
            Debug.Log("Bu script sadece 0 nolu sahnede çalışır.");
            return;
        }

        // GameObject'in başlangıç konumunu kaydet
        transform.position = baslangicKonumu;
        
        // Baslangıç konumunu konsola yazdır
        Debug.Log("Başlangıç Konumu: " + baslangicKonumu);
    }
}
