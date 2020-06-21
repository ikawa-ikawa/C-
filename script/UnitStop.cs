using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UnitStop : MonoBehaviour
{
    JET Sys;
    Image Img;

    void Start()
    {
        /*JET*/
        Sys = GameObject.FindWithTag("Player").GetComponent<JET>();

    }

    void Update()
    {

        if(Sys.getBstop() == 1)
        {
            this.gameObject.GetComponent<Image>().color = new Color(0f, 1f, 1f, 1f);
        }
        else
        {
            this.gameObject.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
        }

    }
}
