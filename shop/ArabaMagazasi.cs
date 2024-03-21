
using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class ArabaMagazasi : MonoBehaviour
{
    private List<GameObject> arabalar = new List<GameObject>();

    private int currentCarIndex = 0;

    public TextMeshProUGUI aracAdiText;
    public GameObject button;
    public GameObject buttontext;

    public Color buyrenk;
    public Color userenk;

    void Start()
    {
        ArabaPrefabListesiniDoldur();
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
            TextAsset jsonVeri = Resources.Load<TextAsset>($"Vehicles/{arabaPrefab.name}");
            if (jsonVeri != null)
            {
                AracVerisi aracVerisi = JsonConvert.DeserializeObject<AracVerisi>(jsonVeri.text);
                aracVerisi.arabaPrefab = arabaPrefab; // arabaPrefab özelliğini ayarla


                if (aracVerisi.isown)
                    arabalar.Insert(0, arabaPrefab); 
                else
                    arabalar.Add(arabaPrefab);
            }
        }

        Debug.Log(arabalar.Count > 0 ? $"Toplam {arabalar.Count} araba bulundu." : "HiÃ§ araba bulunamadÄ±!");
    }

    public void ShowCar(int direction)
    {
        currentCarIndex = (currentCarIndex + direction + arabalar.Count) % arabalar.Count;
        SpawnAraba(currentCarIndex);
        AracAdiniGuncelle(currentCarIndex);
        UpdateButtonAndText();
    }

    void SpawnAraba(int index)
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        GameObject newCar = Instantiate(arabalar[index], transform.position, Quaternion.identity);
        newCar.transform.parent = transform;

        foreach (Camera camera in newCar.GetComponentsInChildren<Camera>())
            camera.enabled = false;

        foreach (Canvas canvas in newCar.GetComponentsInChildren<Canvas>())
            canvas.enabled = false;
    }

    void AracAdiniGuncelle(int index)
    {
        TextAsset jsonVeri = Resources.Load<TextAsset>($"Vehicles/{arabalar[index].name}");
        if (jsonVeri != null)
        {
            AracVerisi aracVerisi = JsonConvert.DeserializeObject<AracVerisi>(jsonVeri.text);
            aracAdiText.text = aracVerisi.aracAdi;
        }
    }


    public void ResetAllIsOwn()
    {
        // Loop through all AracVerisi objects
        foreach (GameObject arabaPrefab in arabalar)
        {
            TextAsset jsonVeri = Resources.Load<TextAsset>($"Vehicles/{arabaPrefab.name}");
            if (jsonVeri != null)
            {
                AracVerisi aracVerisi = JsonConvert.DeserializeObject<AracVerisi>(jsonVeri.text);

                // Set all isown to false except for the first car (index 0)
                aracVerisi.isown = arabaPrefab.name.Equals(arabalar[0].name);

                // Write the updated data back to the JSON file
                string jsonData = JsonConvert.SerializeObject(aracVerisi);
                File.WriteAllText($"Assets/Resources/Vehicles/{arabaPrefab.name}.json", jsonData);
            }
        }

        // Update the in-memory list of car data (optional)
        for (int i = 0; i < arabalar.Count; i++)
        {
            TextAsset jsonVeri = Resources.Load<TextAsset>($"Vehicles/{arabalar[i].name}");
            if (jsonVeri != null)
            {
                arabalar[i].GetComponent<AracVerisi>().isown = arabalar[i].name.Equals(arabalar[0].name); // Assuming AracVerisi component exists on each prefab
            }
        }
    }

    public void buttonclick()
    {
      AracVerisi aracVerisi = GetSelectedCarData();
    if (aracVerisi != null)
    {
        if (aracVerisi.isown)
        {
            //PlayerPrefs.SetString("currentvehicle", aracVerisi.arabaPrefab.name);
           PlayerPrefs.SetString("currentvehicle", aracVerisi.aracid);
            Debug.LogWarning("KAYDEDİLDİ");
        }
        else
        {
            int storedCoin = PlayerPrefs.GetInt("Coin", 0); // PlayerPrefs'ten mevcut coin miktarını al
            if (storedCoin >= aracVerisi.coin)
            {
                Debug.LogWarning(aracVerisi.isown);
                aracVerisi.isown = true;
                // Aracı satın al ve Coin miktarını güncelle
                PlayerPrefs.SetString("currentvehicle", aracVerisi.aracAdi);
                PlayerPrefs.SetInt("Coin", storedCoin - aracVerisi.coin);
                //aracverisi
                Debug.LogWarning("satıldı");
                 
                string jsonData = JsonConvert.SerializeObject(aracVerisi);
                File.WriteAllText($"Assets/Resources/Vehicles/{arabalar[currentCarIndex].name}.json", jsonData);

                


                PlayerPrefs.Save();
            }
            else
            {
                // Yeterli coin yoksa bir şey yapma
                Debug.LogWarning("Yetersiz coin, araç satın alınamadı."+storedCoin);
            }
        }
        UpdateButtonAndText(); // Buton ve metinleri güncelle
    }}

    void UpdateButtonAndText()
    {
        AracVerisi aracVerisi = GetSelectedCarData();
        if (aracVerisi != null)
        {
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
                buttontext.GetComponent<TextMeshProUGUI>().text = "BUY(" + aracVerisi.coin + ")";
            }
        }
    }

    AracVerisi GetSelectedCarData()
    {
        if (currentCarIndex >= 0 && currentCarIndex < arabalar.Count)
        {
            TextAsset jsonVeri = Resources.Load<TextAsset>($"Vehicles/{arabalar[currentCarIndex].name}");
            if (jsonVeri != null)
            {
                return JsonConvert.DeserializeObject<AracVerisi>(jsonVeri.text);
            }
        }
        return null;
    }

   
    

    private class AracVerisi
    {
        public string aracAdi;
        public bool isActive;
        public bool isown;
        public int coin;
        public GameObject arabaPrefab; // Aracın prefabını tutacak özellik
        public string aracid;

    }
