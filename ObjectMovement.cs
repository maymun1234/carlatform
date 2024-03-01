using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public float minSpeed = 3f; // Minimum hareket hızı
    public float maxSpeed = 7f; // Maksimum hareket hızı
    public float minInitialDirectionDelay = 1f; // Başlangıçtaki rastgele yönün minimum gecikme süresi
    public float maxInitialDirectionDelay = 5f; // Başlangıçtaki rastgele yönün maksimum gecikme süresi
    public Transform object1; // İlk obje
    public Transform object2; // İkinci obje
    private bool direction = true; // true: +x yönü, false: -x yönü
    private float currentSpeed; // Mevcut hız
    private float initialDirectionDelay; // Başlangıçta rasstgele yön için gecikme süresi

    public float x1;

    public float x2;



    void Start()
    {
        // Başlangıçta rastgele bir yön ataması yap
        initialDirectionDelay = Random.Range(minInitialDirectionDelay, maxInitialDirectionDelay);
        Invoke("ChangeDirection", initialDirectionDelay);

        // Başlangıçta bir hız ataması yap
        currentSpeed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        // Hareket yönünü belirle
        Vector3 moveDirection = direction ? Vector3.right : Vector3.left;

        // Hareketi uygula
        transform.Translate(moveDirection * currentSpeed * Time.deltaTime);

        // Eğer obje1 veya obje2 ile temas edilirse yönü değiştir
        if (object1 != null && object2 != null)
        {
            if (transform.position.x >= object2.position.x+x1 || transform.position.x <= object1.position.x+x2)
            {
                direction = !direction;
                // Hızı tekrar rastgele belirle
                currentSpeed = Random.Range(minSpeed, maxSpeed);
            }
        }
    }

    // Yönü değiştirme metodu
    void ChangeDirection()
    {
        direction = !direction;
    }
}
