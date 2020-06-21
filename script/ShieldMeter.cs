using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldMeter : MonoBehaviour
{
    JET Sys;
    Image Img;

    void Start()
    {
        /*JET*/
        Sys = GameObject.FindWithTag("Player").GetComponent<JET>();

        Img = this.GetComponent<Image>();
    }

    void Update()
    {

        Img.fillAmount = (float)Sys.getHP() / (float)Sys.getMaxHP();

    }
}
