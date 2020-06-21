using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissileMerker : MonoBehaviour
{
    Missile Sys;
    Image Img;

    void Start()
    {
        /*ミサイルシステム*/
        Sys = GameObject.Find("Missile(Right)").GetComponent<Missile>();

        Img = this.GetComponent<Image>();
    }

    void Update()
    {
        Img.fillAmount = Sys.getInterval();
    }
}
