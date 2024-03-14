using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.UI;

public class ArabaMagazasi : MonoBehaviour
{
    private List<GameObject> arabalar = new List<GameObject>();
    private int currentCarIndex = 0;
    
    private GameObject currentVehicle; // Şu anki aracı temsil eden değişken
    
    public TextMeshProUGUI aracAdiText;
    public GameObject button;
    public GameObject buttontext;

    public Color buyrenk;
    public Color userenk;

    void Start()
    {
        ArabaPrefabListesiniDoldur();
        SpawnAraba(currentCarIndex);
        ShowCar(0); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            ShowCar(-1);
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            ShowCar(1);
    }

    void ArabaPrefabListesiniDoldur()
    {
        GameObject[] arabalarArray = Resources.LoadAll<GameObject>("Vehicles");

        foreach (GameObject arabaPrefab in arabalarArray)
        {
            string dosyaYolu = Path.Combine(Application.dataPath, "Resources/Vehicles", $"{arabaPrefab.name}.json");

            if (File.Exists(dosyaYolu))
            {
                string jsonVeri = File.ReadAllText(dosyaYolu);
                AracVerisi aracVerisi = JsonConvert.DeserializeObject<AracVerisi>(jsonVeri);

                
                if (aracVerisi.isown)
                    arabalar.Insert(0, arabaPrefab); 
                else
                    arabalar.Add(arabaPrefab); 
            }
        }

        Debug.Log(arabalar.Count > 0 ? $"Toplam {arabalar.Count} araba bulundu." : "Hiç araba bulunamadı!");
    }


    public void ShowCar(int direction)
    {
        currentCarIndex = (currentCarIndex + direction + arabalar.Count) % arabalar.Count;
        SpawnAraba(currentCarIndex);
        AracAdiniGuncelle(currentCarIndex);
        UpdateButtonAndText(); // Buton ve yazıyı güncelle
    }

    void SpawnAraba(int index)
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        GameObject newCar = Instantiate(arabalar[index], transform.position, Quaternion.identity);
        newCar.transform.parent = transform;

        foreach (Camera camera in newCar.GetComponentsInChildren<Camera>())
            camera.enabled = false;

        foreach (Canvas camea in newCar.GetComponentsInChildren<Canvas>())
            camea.enabled = false;
    }

    void AracAdiniGuncelle(int index)
    {
        string dosyaYolu = Path.Combine(Application.dataPath, "Resources/Vehicles", $"{arabalar[index].name}.json");

        if (File.Exists(dosyaYolu))
        {
            string jsonVeri = File.ReadAllText(dosyaYolu);
            AracVerisi aracVerisi = JsonUtility.FromJson<AracVerisi>(jsonVeri);
            aracAdiText.text = aracVerisi.aracAdi;
        }
    }

    public void buttonclick1()
    {
        Debug.LogWarning("1");
    }

    public void buttonclick()
{
    string dosyaYolu = Path.Combine(Application.dataPath, "Resources/Vehicles", $"{arabalar[currentCarIndex].name}.json");

    if (File.Exists(dosyaYolu))
    {
        string jsonVeri = File.ReadAllText(dosyaYolu);
        AracVerisi aracVerisi = JsonConvert.DeserializeObject<AracVerisi>(jsonVeri);

        if (!aracVerisi.isown)
        {
            int aracCoin = aracVerisi.coin;
            int playerCoin = PlayerPrefs.GetInt("Coin", 0); // coin adlı PlayerPrefs'ten değeri al, eğer yoksa 0 al

            // Player'ın coin miktarı aracın coin miktarından büyükse aracı alabilmesi için isown'u true yap
            if (playerCoin >= aracCoin)
            {
                aracVerisi.isown = true;
                PlayerPrefs.SetInt("Coin", playerCoin - aracCoin); // Aracı aldığı için coin'i düşür
                PlayerPrefs.Save(); // PlayerPrefs değişikliklerini kaydet

                // Aracın isown durumunu güncelledik, şimdi bu değişikliği JSON dosyasına yazabiliriz
                string updatedJson = JsonConvert.SerializeObject(aracVerisi);
                File.WriteAllText(dosyaYolu, updatedJson);

                Debug.LogWarning("Araba satın alındı ve sahibi yapıldı.");
            }
            else
            {
                Debug.LogWarning("Yetersiz coin, araba satın alınamadı.");
            }
        }
        else
        {
            currentVehicle = arabalar[currentCarIndex]; // Şu anki aracı güncelle
            PlayerPrefs.SetString("CurrentVehicle", currentVehicle.name); // Şu anki aracın ismini kaydet
            PlayerPrefs.Save();
            Debug.LogWarning("CurrentVehicle değişti: " + currentVehicle.name);
        }
    }

    UpdateButtonAndText(); // Buton ve yazıyı güncelle
}


    // Buton ve yazıyı güncelleyen metod
    void UpdateButtonAndText()
    {
        AracVerisi aracVerisi = GetSelectedCarData(); // Seçili aracın verilerini al

        if (aracVerisi != null)
        {
            int araccoin = aracVerisi.coin;

            if (aracVerisi.isown)
            {
                button.GetComponent<Image>().color = buyrenk;
                buttontext.GetComponent<TextMeshProUGUI>().color = userenk;
                buttontext.GetComponent<TextMeshProUGUI>().text = "USE";
            }
            else
            {
                button.GetComponent<Image>().color = userenk;
                buttontext.GetComponent<TextMeshProUGUI>().color = buyrenk;
                buttontext.GetComponent<TextMeshProUGUI>().text = "BUY(" + araccoin + ")";
            }
        }
    }

    // Seçili aracın verilerini döndüren metod
    AracVerisi GetSelectedCarData()
    {
        if (currentCarIndex >= 0 && currentCarIndex < arabalar.Count)
        {
            string dosyaYolu = Path.Combine(Application.dataPath, "Resources/Vehicles", $"{arabalar[currentCarIndex].name}.json");

            if (File.Exists(dosyaYolu))
            {
                string jsonVeri = File.ReadAllText(dosyaYolu);
                return JsonConvert.DeserializeObject<AracVerisi>(jsonVeri);
            }
        }
        return null;
    }

    // Araba için JSON dosyasındaki veri yapısı
    private class AracVerisi
    {
        public string aracAdi;
        public bool isActive;
        public bool isown;
        public int coin;
    }
}
