using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public string targetTag = "YourTargetTag"; // Hedef objelerin etiketi
    public int scorePerHit = 1; // Her vuruşta eklenecek skor miktarı
    public TMP_Text scoreText; // Skoru gösterecek TMPro nesnesi
    public TMP_Text highScoreText; // Yüksek skoru gösterecek TMPro nesnesi

    private int score = 0; // Oyuncunun skoru
    public int highScore = 0; // Yüksek skor

    private const string highScoreKey = "HighScore"; // PlayerPrefs anahtarı

    void Start()
    {
        highScore = PlayerPrefs.GetInt(highScoreKey, 0); // Yüksek skoru al, eğer kayıtlı değilse 0 olarak ayarla
    UpdateScoreText();
    UpdateHighScoreText(); // Yüksek skoru gösteren metni güncelle

    }

    // Hedef objeye çarpıldığında çağrılacak olan metot
    public void HitTarget()
    {
        score += scorePerHit; // Skora puan ekle
        if (score > highScore) // Eğer mevcut skor yüksek skoru aşıyorsa
        {
            highScore = score; // Yüksek skoru güncelle
            PlayerPrefs.SetInt(highScoreKey, highScore); // Yüksek skoru kaydet
            UpdateHighScoreText(); // Yüksek skor metnini güncelle
        }
        UpdateScoreText(); // Skor metnini güncelle
    }

    // Skor metnini güncelleyen metot
    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "" + score.ToString(); // Skor metnini güncelle
        }
    }

    // Yüksek skor metnini güncelleyen metot
    void UpdateHighScoreText()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "" + highScore.ToString(); // Yüksek skor metnini güncelle
        }
    }

    // Hedef objeye çarpıldığında kontrol etmek için
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            HitTarget(); // Skoru arttır
        }
    }
}
