using UnityEngine;
using TMPro;

public class TMP_TextUpdater : MonoBehaviour
{
    public TMP_Text textToUpdate; // Güncellenecek TMP_Text nesnesi

    // Her frame güncelleme yapmak için Update kullanılır
    void Update()
    {
        if (textToUpdate != null)
        {
            TMP_Text sourceText = GetComponent<TMP_Text>(); // Scriptin olduğu TMP_Text nesnesini al
            if (sourceText != null)
            {
                textToUpdate.text = sourceText.text; // Güncellenecek TMP_Text nesnesinin metnini, scriptin olduğu nesnenin metni ile güncelle
            }
            else
            {
                Debug.LogWarning("Kaynak metin bulunamadı!");
            }
        }
        else
        {
            Debug.LogWarning("Güncellenecek metin atanmamış!");
        }
    }
}
