using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class coinmanager : MonoBehaviour
{
    // Start is called before the first frame update
     public TextMeshProUGUI textMeshProText;

    void Start()
    {
        // PlayerPrefs'tan 'skor' anahtarına karşılık gelen değeri al
        int skor = PlayerPrefs.GetInt("Coin", 0);

        // TMPro Text nesnesine skor değerini yazdır
        textMeshProText.text = "" + skor.ToString();
    }
}
