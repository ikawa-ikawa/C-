using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageUI : MonoBehaviour
{
    //　フェードアウトするスピード
    private float FadeOutSpeed = 1f;

    // テキストコンポーネント
    private Text DamageText;

    // 実際のダメージ数
    private int DamageNum;

    // スピード乱数
    private float RandomSpeed;

    void Start()
    {
        DamageText = GetComponentInChildren<Text>();

        RandomSpeed = Random.Range(20f, 50f);
    }

    void Update()
    {
        
        transform.position = transform.position + Vector3.up * RandomSpeed * Time.deltaTime;

        DamageText.color = Color.Lerp(DamageText.color, new Color(0f, 0f, 0f, 0f), FadeOutSpeed * Time.deltaTime);

        DamageText.text = "" + DamageNum;
    }


    public void setDamageNum(int Damage)
    {
        DamageNum = Damage;
    }
}
