using System;
using UnityEngine;

public class Tank : MonoBehaviour
{
    private Transform bodyTransform; //bodyオブジェクトのTransformコンポーネント情報
    private Transform tarrotTransform; //tarrotオブジェクトのTransformコンポーネント情報
    private Transform pointerPos; //pointerの3D座標把握用オブジェクトのTransform
    private Transform shotPoint; //弾発射座標用Transform
    public bool playerFlag = false; //プレイヤー操作を受け付けるか否か
    private GameObject[] bullet; //弾オブジェクト情報の格納
    private GameObject[] mine; //地雷プレハブオブジェクトの格納
    public GameObject bulletPrefab; //弾プレハブオブジェクトの格納
    public GameObject minePrefab; //地雷プレハブオブジェクトの格納
    private Bullet bulletScript; //弾のスクリプト情報
    public int bulletLimit; //発射可能弾数
    public int mineLimit; //地雷設置可能数
    public float bulletSpeed = 15f; //発射される弾のスピード
    public int ricochet; //跳弾回数
    private float t_radian; //現在のTarrotの向き

    void Start()
    {
        bullet = new GameObject[bulletLimit]; //指定個数分のインスタンスを生成
        mine = new GameObject[mineLimit]; //指定個数分のインスタンスを生成
        bodyTransform = this.transform.Find("Body").GetComponent<Transform>(); //BodyオブジェクトのTransformコンポーネント情報を取得
        tarrotTransform = this.transform.Find("Tarrot").GetComponent<Transform>(); //BatteryオブジェクトのTransformコンポーネント情報を取得
        shotPoint = this.transform.Find("Tarrot").Find("Canon").Find("ShotPoint").GetComponent<Transform>();
        if (playerFlag) pointerPos = GameObject.Find("PointerPosition3D").GetComponent<Transform>();
    }

    void Update()
    {
        if (playerFlag)
        {
            TarrotBetweenPointer();
            if (Input.GetMouseButtonDown(0)) //左クリック
                MakeBullet(shotPoint.position.x, shotPoint.position.z, bulletSpeed, t_radian, ricochet); //弾発射
            if (Input.GetMouseButtonDown(1)) //右クリック
                Debug.Log("Pressed secondary button."); //地雷設置関数
        }
    }

    void OnCollisionEnter(Collision collision) //他オブジェクトとの衝突をみる
    {
        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Blast") Destroy(this.gameObject); //破壊
    }

    void TarrotBetweenPointer() //TarrotとPointerとの角度を計算
    {
        t_radian = (float)Math.Atan2(pointerPos.position.z - tarrotTransform.position.z, pointerPos.position.x - tarrotTransform.position.x); //x-z平面のtanの値計算
        t_radian = t_radian * (float)(180 / Math.PI); //角度に変換
        TurnTarrot(t_radian);
    }
    void TurnTarrot(float radian) //角度（radian）を引数にTarrotを回転
    {
        tarrotTransform.localRotation = Quaternion.Euler(-90f, 0f, -radian + 90f); //回転処理
    }

    void MakeBullet(float x, float z, float speed, float radian, int rico) //弾を発射する
    {
        for (int i = 0; i < bulletLimit; i++)
        {
            if (bullet[i] == null) //弾の発射数が限度に達していないか確認
            {
                bullet[i] = Instantiate(bulletPrefab, new Vector3(x, 0, z), new Quaternion(0, 0, 0, 0)); //配列の空き番号に弾を格納
                bulletScript = bullet[i].GetComponent<Bullet>(); //発射した弾のスクリプト情報を取得
                bulletScript.Shot(speed, radian, rico); //発射関数実行
                Debug.Log("bullet " + i + " instantiated succesfully!"); //ログ
                break; //発射できた場合は，追加検索を行わない
            }
        }
    }

    void MakeMine(float x, float z)
    {
        for (int i = 0; i < mineLimit; i++)
        {
            if (mine[i] == null) //地雷の設置数が限度に達していないか確認
            {
                mine[i] = Instantiate(minePrefab, new Vector3(x, 0, z), new Quaternion(0, 0, 0, 0)); //配列の空き番号に地雷を格納
                //bulletScript = bullet[i].GetComponent<Bullet>(); //発射した弾のスクリプト情報を取得
                //bulletScript.Shot(speed, radian, rico); //発射関数実行
                Debug.Log("mine " + i + " instantiated succesfully!"); //ログ
                break; //設置できた場合は，追加検索を行わない
            }
        }
    }

}