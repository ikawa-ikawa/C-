using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissileAlert : MonoBehaviour
{
    MissileDistance Sys;
    Image Img;

    float LocalTime;

    void Start()
    {
        Sys = transform.parent.Find("MissileDistance").GetComponent<MissileDistance>();
        Img = GetComponent<Image>();
    }

    void Update()
    {
        LocalTime = LocalTime + 8 * Time.deltaTime;

        if( Sys.getMissileFlag() == 1 )
        {
            Img.color = new Color(Img.color.r, Img.color.g, Img.color.b, Mathf.Sin(LocalTime) * 0.5f + 0.5f);
        }
        else
        {
            Img.color = new Color(Img.color.r, Img.color.g, Img.color.b, 0f);
        }
    }
}
