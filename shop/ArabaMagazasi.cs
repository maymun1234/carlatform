using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;
using Newtonsoft.Json;
using TMPro;


public class ArabaMagazasi : MonoBehaviour
{
    private List<GameObject> arabalar; // Arabaları saklayacağımız liste
    private int currentCarIndex = 0; // Şu anda seçili olan arabanın index'i
    public TextMeshProUGUI aracAdiText;

    void Start()
    {
        ArabaPrefabListesiniDoldur();
        // İlk araba spawn edilsin
        SpawnAraba(currentCarIndex);
    }

    void Update()
    {
        // Sol ve sağ ok tuşlarına basıldığında araba değiştir
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ShowPreviousCar();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ShowNextCar();
        }
    }

    void ArabaPrefabListesiniDoldur()
    {
        arabalar = new List<GameObject>();

        // Resources klasörü altındaki "Vehicles" klasöründeki tüm prefabları al
        GameObject[] arabalarArray = Resources.LoadAll<GameObject>("Vehicles");

        foreach (GameObject arabaPrefab in arabalarArray)
        {
            // Prefab ile aynı ismi taşıyan JSON dosyasını oku
            string dosyaYolu = Path.Combine(Application.dataPath, "Resources/Vehicles/" + arabaPrefab.name + ".json");

            if (File.Exists(dosyaYolu))
            {
                string jsonVeri = File.ReadAllText(dosyaYolu);
                AracVerisi aracVerisi = JsonConvert.DeserializeObject<AracVerisi>(jsonVeri);

                // JSON dosyasında isActive true ise, arabayı listeye ekle
                if (aracVerisi.isActive)
                {
                    arabalar.Add(arabaPrefab);
                    aracAdiText.text = aracVerisi.aracAdi;
                }
            }
        }

        // Listede arabalar var mı kontrol et
        if (arabalar.Count > 0)
        {
            Debug.Log("Toplam " + arabalar.Count + " araba bulundu.");
        }
        else
        {
            Debug.Log("Hiç araba bulunamadı!");
        }
    }

    public void ShowNextCar()
    {
        currentCarIndex++;
        if (currentCarIndex >= arabalar.Count)
        {
            currentCarIndex = 0;
        }
        SpawnAraba(currentCarIndex);
         AracAdiniGuncelle(currentCarIndex);
    }

    public void ShowPreviousCar()
    {
        currentCarIndex--;
        if (currentCarIndex < 0)
        {
            currentCarIndex = arabalar.Count - 1;
        }
        SpawnAraba(currentCarIndex);
         AracAdiniGuncelle(currentCarIndex);
    }

    void SpawnAraba(int index)
    {
        // Eğer araba varsa sil
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Yeni araba spawn et
        GameObject newCar = Instantiate(arabalar[index], transform.position, Quaternion.identity);
        newCar.transform.parent = transform;
        Camera[] kameralar = newCar.GetComponentsInChildren<Camera>();
    foreach (Camera camera in kameralar)
    {
        camera.enabled = false;
    }

     Canvas[] kamealar = newCar.GetComponentsInChildren<Canvas>();
    foreach (Canvas camea in kamealar)
    {
        camea.enabled = false;
    }
    }

    void AracAdiniGuncelle(int index)
{
    string dosyaYolu = Path.Combine(Application.dataPath, "Resources/Vehicles/" + arabalar[index].name + ".json");
    Debug.LogWarning("mhjmgjjgm");

    if (File.Exists(dosyaYolu))
    {
        Debug.LogWarning("dbdbgdgn");
        string jsonVeri = File.ReadAllText(dosyaYolu);
        AracVerisi aracVerisi = JsonUtility.FromJson<AracVerisi>(jsonVeri);
        Debug.LogWarning("vericekildi");
        aracAdiText.text = aracVerisi.aracAdi;

        if (aracVerisi.isActive)
        {
            Debug.LogWarning("vericekildi1");
            // Arac adını güncelle
            
            Debug.LogWarning(aracVerisi.aracAdi);

        }
    }
}

    // Araba için JSON dosyasındaki veri yapısı
    private class AracVerisi
    {
        public string aracAdi;
        public bool isActive;
    }
}
