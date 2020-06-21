using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LockOnSystem : MonoBehaviour
{


    //存在する敵のリスト
    GameObject[] ExistenceEnemys;

    //画面内の敵の位置情報（２０）までしか格納できない
    //Vector3[] Targets = new Vector3[200];
    List<Vector3> Targets = new List<Vector3>();
    int[] FlagList = new int[200];

    //ロックオンしている敵情報（こっちは配列なことに注意）
    int[] LockOnTargetsFlag = new int[10];

    List<Vector3> Concentrations = new List<Vector3>();
    int Concentration;
    int ConcentrationCountBank;//FlagListCircleの要素数が減ったことを観測するための保存用
    float ConcentrationCT;

    List<int> FlagListCircle = new List<int>();


    public float ScreenW = 1366;
    public float ScreenH = 768;
    public float LockOnCircle = 250;


    // Start is called before the first frame update
    void Start()
    {
        Concentration = 0;
        ConcentrationCT = 0;
        ConcentrationCountBank = 0;
    }

    // Update is called once per frame
    void Update()
    {

        

        //存在する敵のオブジェクトリスト
        ExistenceEnemys = GameObject.FindGameObjectsWithTag("Enemy");


        int i = 0;
 

        while( i < FlagList.Length )
        {
            FlagList[i] = 0;

            i = i + 1;
        }

        i = 0;

        while( i < LockOnTargetsFlag.Length )
        {
            LockOnTargetsFlag[i] = 0;

            i = i + 1;
        }

        i = 0;

        //敵のオブジェクトリストに対してアクセス
        while ( i < ExistenceEnemys.Length)
        {
            //当該の敵が画面内に居るのであれば
            if (ExistenceEnemys[i].GetComponent<EnemyManagement>().getIsRendered())
            {
                FlagList[i] = 1;
            }      
            i = i + 1;
        }


        i = 0;

        Targets.Clear();
        FlagListCircle.Clear();
        Concentrations.Clear();

        while (i < FlagList.Length)
        {
            //画面内に敵がいるフラグが立っているなら
            if(FlagList[i] == 1)
            {
                Targets.Add(ExistenceEnemys[i].GetComponent<Transform>().transform.position);
            }

            i = i + 1;
        }

        i = 0;

        //画面内に居る敵に対してその位置情報にアクセス
        while(i < Targets.Count)
        {

            //敵を画面のどの座標に表示しているかを取得
            Vector2 ScreenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, Targets[i]);

            //↓ここをScreenWとScreenHに変えて下さい
            ScreenPoint.x = ScreenPoint.x - (Screen.width / 2);
            ScreenPoint.y = ScreenPoint.y - (Screen.height / 2);

            //アクセスした敵がロックオンサークル内に敵がいるならば
            if (ScreenPoint.magnitude <= LockOnCircle)
            {
                LockOnTargetsFlag[i] = 1;

                Concentrations.Add(Targets[i]);
                FlagListCircle.Add(i);
            }
            else
            {
                LockOnTargetsFlag[i] = 0;
            }

            i = i + 1;
        }

        i = 0;

        //コンセントレーション処理
        ConcentrationCT = ConcentrationCT + 1;

        //FlagListCircleの要素数が直前より減った場合の処理
        if(FlagListCircle.Count < ConcentrationCountBank)
        {

            if(ConcentrationCountBank - Concentration <= FlagListCircle.Count)
            {
                Concentration = Concentration - (ConcentrationCountBank - FlagListCircle.Count);
            }

        }
        //逆に増えた場合
        if (FlagListCircle.Count > ConcentrationCountBank)
        {
            Concentration = Concentration + (FlagListCircle.Count - ConcentrationCountBank);
        }

        //ヌルポ回避．ありえない数値を排除
        if (Concentration >= FlagListCircle.Count || Concentration < 0)
        {
            Concentration = 0;
        }

        if (ConcentrationCT >= 2100000000)
        {
            ConcentrationCT = 0;
        }

        if (Input.GetKey(KeyCode.LeftShift) && ConcentrationCT > 200)
        {
            ConcentrationCT = 0;

        

            
            if (FlagListCircle.Count > 0)
            {
                
                if (FlagListCircle.Count - 1 == Concentration)
                {
                    Concentration = 0;
                }
                else
                {
                    Concentration = Concentration + 1;
                }
            }
            
            
        }

        //直前の情報を保存したい
        ConcentrationCountBank = FlagListCircle.Count;

    }


    
    public List<Vector3> getTarget()
    {
        return Targets;
    }

    public int[] LockOngetTargetsFlag()
    {
        return LockOnTargetsFlag;
    }

    public Vector3 getConcentration()
    {
        if(Concentrations.Count != 0)
        {
            return Concentrations[Concentration];
        }
        else
        {
            return new Vector3(0,0,0);
        }
    }

    public int getConcentrationsCount()
    {
        return Concentrations.Count;
    }

    public int getFlagListCircle()
    {
        if (FlagListCircle.Count == 0)
        {
            return 0;
        }
        else
        {
            return FlagListCircle[Concentration];
        }
    }

    //存在する敵のオブジェクトリストを返す
    public GameObject[] getExistenceEnemys()
    {
        return ExistenceEnemys;
    }
}

