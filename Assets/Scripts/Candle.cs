using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Candle : MonoBehaviour
{
    public float speed = 0f; //hız değeri
    float sagSolHizi= 5f; //karakterin sağ ve sol hareket hızı
    float sagSol = 0; //karakterin sağ ve sol hareket koordinatları için
    int goldCount = 0; //toplanılan altın
    public float scaleCandle = 1f; //mumun boyutu
    public Light myLight; //fire objesinin Light componenti
    public GameObject childCandle; //child obje
    public Text gold; //UI için
     public Text died; //UI için
    void Awake(){
        died.enabled = false;  
    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.LeftShift)){  //shifte basıldığı zaman hızı artıyor fakat boyutu, parlaklığı (intensity ve range) azalıyor
            Debug.Log("hiz");
            speed = 10f;
            decScale(0.00015f);
            decBrightness(0.01f);
        }else if (Input.GetKeyUp(KeyCode.LeftShift)){ //normal zamandaki hızı
            speed = 5f;
        }
        sagSol = Input.GetAxis("Horizontal");
        transform.Translate(0, 0, Time.deltaTime* speed); //ileri yöndeki hareket
        transform.Translate(Time.deltaTime * sagSol * sagSolHizi, 0, 0); //sağ ve sol hareket
        decScale(0.0002f); //yürürken mumun erimesi için

        if(childCandle.transform.position.y <= -0.1f){  //düşme kontrolü         
            Debug.LogError("Düştü");
            string temp = "Oyun bitti düştünüz.";
            died.text = temp;
            died.enabled = true;  
        }
        myLight.transform.position = new Vector3(childCandle.transform.position.x, scaleCandle + 0.2f, childCandle.transform.position.z); //fire objesinin pozisyonu mumun üstüne gelecek şekilde ayarlanıyor
        }
    public void addGold(int amount){ //altın toplama ve UI için
        goldCount = goldCount + amount;
        string temp = "Gold: " + goldCount.ToString();
        gold.text = temp;
    }
    public void incScale(){    //mumun boyutunu artırıyor ve fire objesinin pozisyonu ayarlanıyor
        scaleCandle = scaleCandle + 0.3f;
        myLight.transform.position = new Vector3(childCandle.transform.position.x, scaleCandle, childCandle.transform.position.z);
        transform.localScale = new Vector3(1,scaleCandle ,1);
    }
    public void decScale(float amount){ //mumun boyutunu azaltıyor ve fire objesinin pozisyonu ayarlanıyor
        scaleCandle = scaleCandle - amount;
        myLight.transform.position = new Vector3(childCandle.transform.position.x, scaleCandle, childCandle.transform.position.z); //fire objesinin pozisyonu mumun üstüne gelecek şekilde ayarlanıyor. Child objenin konumuna göre
        transform.localScale = new Vector3(1,scaleCandle ,1);
        
        if(myLight.transform.position.y <= 0){ //fire objesinin pozisyonuna göre oyun sonu kontrolü
            Debug.LogError("Mum eridi");
            string temp = "Oyun bitti mum eridi.";
            died.text = temp;
            died.enabled = true;  
        }
    }
    public void incBrightness(float amount){ //parlaklığı artıyor, intensity ve range
        myLight.intensity = myLight.intensity + amount;
        myLight.range = myLight.range + amount * 10;
    }
    public void decBrightness(float amount){   //parlaklığı azaltıyor, intensity ve range
        myLight.intensity = myLight.intensity - amount;
        myLight.range = myLight.range - amount * 10;
        
        if(myLight.intensity <= 0){ //oyun sonu kontrolü 
            Debug.LogError("Işık söndü");
            string temp = "Oyun bitti ışık söndü.";
            died.text = temp;
            died.enabled = true;  
        }
    }
}