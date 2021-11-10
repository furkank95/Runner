using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldRotate : MonoBehaviour{
    float  speed = 100f;
    void Update(){
        transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0)); //obje döndürmek için
    }
}
