using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collisions : MonoBehaviour{
public Candle candle; //obje tanımlanıyor
    public Text finish; //UI için
    void Awake(){
    finish.enabled = false;  
    }
    void Start(){
        Candle candle = gameObject.GetComponentInParent(typeof(Candle)) as Candle; //child objeye erişim
    }
    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Gold"){ //Gold'a çarpma durumu
        candle.addGold(1);
        Destroy(other.gameObject);
        }else if(other.gameObject.tag == "WaterSlime"){ //WaterSlime'a çarpma durumu. Parlaklığı düşürüyor
            Destroy(other.gameObject);
            candle.decBrightness(1f);
        }else if(other.gameObject.tag == "FireSlime"){ //FireSlime'a çarpma durumu. Parlaklığı arttırıyor fakat boyutu küçültüyor
            Destroy(other.gameObject);
            candle.incBrightness(0.5f);
            candle.decScale(1f);
        }else if(other.gameObject.tag == "Turtle"){ //Turtle'a çarpma durumu. Boyutu yarı yarıya düşürüyor
            Destroy(other.gameObject);
            candle.decScale(candle.scaleCandle/2);
        }else if(other.gameObject.tag == "Candle"){ //Candle'a çarpma durumu. Boyutu arttırıyor
            candle.incScale();
            Destroy(other.gameObject);
        }else if(other.gameObject.tag == "Finish"){ //Bitişte ekrana yazı yazdırır
            finish.enabled = true;
        }
    }
    void OnTriggerStay(Collider other){
        if(other.gameObject.tag == "Lava"){ //Lava üstündeyken parlaklık artıyor fakat boyut küçülüyor. Ayrıca hızı yavaşlıyor
        candle.speed = 3f;
        candle.incBrightness(0.01f);
        candle.decScale(0.01f);
        }
        if(other.gameObject.tag == "Water"){ //Water üstündeyken parlaklık azalıyor ve hızı yavaşlıyor
        candle.speed = 3f;
        candle.decBrightness(0.01f);
        }
    }
}