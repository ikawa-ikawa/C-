using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagement : MonoBehaviour
{
    public GameObject DamageUIPrefab;
    public GameObject ExplosionPrefab;
    public int HP = 5000;

    //メインカメラに付いているタグ名
    private const string MAIN_CAMERA_TAG_NAME = "MainCamera";

    //カメラに表示されているか
    private bool isRendered = false;

    //何かにぶつかった時に呼ばれる
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            //弾の攻撃力とか取るためのやつ
            Bullet2 BulletObje = other.gameObject.GetComponent<Bullet2>();

            // ここでの位置は指定しても意味ない
            GameObject DamageUI = (GameObject)Instantiate(DamageUIPrefab, new Vector3(50f, 50f, 0f), Quaternion.Euler(0, 0, 0));

            //画面にマーカーを表示する位置を計算
            Vector2 Position = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);

            // 表示する場所を決定する（Y軸乱数）
            Vector3 Tmp = new Vector3(Position.x, Position.y + Random.Range(0f, 40f), 0f);

            // キャンバス下に設定するために取得
            GameObject canvas = GameObject.Find("Canvas");

            //キャンバスの子に設定
            DamageUI.transform.SetParent(canvas.transform, false);

            //ポジション変更
            DamageUI.transform.position = Tmp;

            //ダメージ量を決定
            int Damage = BulletObje.getAttackPower() + BulletObje.getErrorRange();

            // 弾の攻撃力と誤差を取得してダメージUIに渡す
            DamageUI.GetComponent<DamageUI>().setDamageNum(Damage);

            // HPからダメージを減らす
            HP = HP - Damage;

            // 1秒後に消す
            Destroy(DamageUI, 1.0f);

            // 弾を消す
            Destroy(other.gameObject);

        }

        if(other.gameObject.CompareTag("Missile"))
        {
            //ミサイルの攻撃力とか取るためのやつ
            Missile2 MissileObje = other.gameObject.GetComponent<Missile2>();

            // ここでの位置は指定しても意味ない
            GameObject DamageUI = (GameObject)Instantiate(DamageUIPrefab, new Vector3 ( 50f , 50f, 0f ), Quaternion.Euler(0, 0, 0));

            //画面にマーカーを表示する位置を計算
            Vector2 Position = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);
            
            // 表示する場所を決定する（Y軸乱数）
            Vector3 Tmp = new Vector3(Position.x, Position.y + Random.Range(0f, 40f), 0f);

            // キャンバス下に設定するために取得
            GameObject canvas = GameObject.Find("Canvas");

            //キャンバスの子に設定
            DamageUI.transform.SetParent(canvas.transform, false);

            //ポジション変更
            DamageUI.transform.position = Tmp;

            //ダメージ量を決定
            int Damage = MissileObje.getAttackPower() + MissileObje.getErrorRange();

            // 弾の攻撃力と誤差を取得してダメージUIに渡す
            DamageUI.GetComponent<DamageUI>().setDamageNum(Damage);

            // HPからダメージを減らす
            HP = HP - Damage;

            // 1秒後に消す
            Destroy(DamageUI, 1.0f);
        }


        Debug.Log("ヒット判定：（EnemyManagement）：" + other.gameObject.tag);
        
    }

    void Start()
    {
        isRendered = false;
    }

    void Update()
    {
        // HPが無くなった時の処理
        if(HP < 0)
        {
            //爆発エフェクトを生成
            Instantiate(ExplosionPrefab, this.transform.position, Quaternion.identity);
            //自分を削除
            Destroy(this.gameObject);
        }
    }

    public bool getIsRendered()
    {
        return isRendered;
    }

    //カメラに映ってる間に呼び出される
    private void OnWillRenderObject()
    {
        //メインカメラに映った時だけ_isRenderedを有効に
        if (Camera.current.tag == MAIN_CAMERA_TAG_NAME)
        {
            isRendered = true;
        }
    }

    //カメラから見えなくなった瞬間に呼び出される
    private void OnBecameInvisible()
    {
        isRendered = false;   
    }
}
