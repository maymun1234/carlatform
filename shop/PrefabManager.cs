using UnityEngine;
using System;
using System.Collections.Generic;

// Prefab'ın bilgilerini saklayacak olan sınıf
[Serializable]
public class PrefabInfo
{
    public GameObject prefab; // Prefab GameObject referansı
    public Transform position1; // İlk pozisyon
    public Transform position2; // İkinci pozisyon
}

public class PrefabManager : MonoBehaviour
{
    // Prefab bilgilerini saklayacak olan liste
    public List<PrefabInfo> prefabList = new List<PrefabInfo>();

    // Test için başlangıçta birkaç prefab ekleyelim
    void Start()
    {
        // Örnek prefab'lar oluşturun
        GameObject prefab1 = Resources.Load<GameObject>("Prefab1");
        GameObject prefab2 = Resources.Load<GameObject>("Prefab2");

        // Prefab'ları ve pozisyonları bir PrefabInfo nesnesine ekleyin
        PrefabInfo info1 = new PrefabInfo();
        info1.prefab = prefab1;
        info1.position1 = prefab1.transform.GetChild(0); // Örnek bir pozisyon
        info1.position2 = prefab1.transform.GetChild(1); // Başka bir pozisyon

        PrefabInfo info2 = new PrefabInfo();
        info2.prefab = prefab2;
        info2.position1 = prefab2.transform.GetChild(0); // Örnek bir pozisyon
        info2.position2 = prefab2.transform.GetChild(1); // Başka bir pozisyon

        // Prefab'ları listeye ekleyin
        prefabList.Add(info1);
        prefabList.Add(info2);
    }
}
