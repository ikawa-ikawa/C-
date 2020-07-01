using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissileAlert : MonoBehaviour
{
    MissileDistance Sys;
    Image Img;

    float LocalTime;
    float AudioTime;
    int LocalFlag;
    

    AudioSource Audio;

    void Start()
    {
        Sys = transform.parent.Find("MissileDistance").GetComponent<MissileDistance>();
        Img = GetComponent<Image>();
        Audio = GetComponent<AudioSource>();
        LocalFlag = 0;
        AudioTime = 0;
    }

    void Update()
    {

        if ( Sys.getMissileFlag() == 1 )
        {
            float LocalSin = Mathf.Sin(LocalTime);
            LocalTime = LocalTime + 8 * Time.deltaTime;
            Img.color = new Color(Img.color.r, Img.color.g, Img.color.b, LocalSin * 0.5f + 0.5f);

            if( LocalFlag == 0 )
            {
                Audio.PlayOneShot(Audio.clip);
                LocalFlag = 1;
            }
            if( LocalFlag == 1 )
            {
                AudioTime = AudioTime + 1 * Time.deltaTime; ;
            }
            if( AudioTime >= 0.75f )
            {
                AudioTime = 0;
                LocalFlag = 0;
            }

        }
        else
        {
            LocalTime = 0;
            Img.color = new Color(Img.color.r, Img.color.g, Img.color.b, 0f);
            LocalFlag = 0;
            Audio.Stop();
            AudioTime = 0;
        }
    }
}
