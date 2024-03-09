using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public string targetTag = "YourTargetTag"; // Hedef objelerin etiketi
    public int scorePerHit = 1; // Her vuruşta eklenecek skor miktarı
    public int coinPerHit = 10; // Her vuruşta eklenecek skor miktarı

    public TMP_Text scoreText; // Skoru gösterecek TMPro nesnesi
    public TMP_Text highScoreText; // Yüksek skoru gösterecek TMPro nesnesi
    public TMP_Text coinText; // Para miktarını gösterecek TMPro nesnesi

    private int score = 0; // Oyuncunun skoru
    private int highScore = 0; // Yüksek skor
    private int coin = 0; // Oyuncunun parası
    

    private const string highScoreKey = "HighScore"; // PlayerPrefs anahtarı
    private const string coinKey = "Coin";

    void Start()
    {
        // Skoru sıfırla
        score = 0;
        UpdateScoreText();
        scoreText.text = score.ToString();

        // Highscore'u ve coini veritabanından çek
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        coin = PlayerPrefs.GetInt(coinKey, 0);

        // Metin alanlarını güncelle
        UpdateHighScoreText();
        UpdateCoinText();
    }

    void Update()
    {
        // Her güncellemede highscore'u kontrol et
        
    }

    // Hedef objeye çarpıldığında çağrılacak olan metot
    public void HitTarget()
    {
        score += scorePerHit; // Skora puan ekle
        coin += coinPerHit; // Para miktarını güncelle
        UpdateCoinText();
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(highScoreKey, highScore);
            UpdateHighScoreText();
        }

        // Metin alanlarını güncelle
        UpdateScoreText();
        
    }

    // Skor metnini güncelleyen metot
    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString(); // Skor metnini güncelle
        }
    }

    // Yüksek skor metnini güncelleyen metot
    void UpdateHighScoreText()
    {
        if (highScoreText != null)
        {
            highScoreText.text = highScore.ToString(); // Yüksek skor metnini güncelle
        }
    }

    // Para metnini güncelleyen metot
    void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = coin.ToString(); // Para miktarı metnini güncelle
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
