using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject prefab; //跳弾エフェクトprefab情報を格納(Unity側で代入済み)
    private int ricochet = 10; //跳弾可能回数 要初期化
    private Rigidbody rb; //物理演算情報RigidBody
    //private GameObject cloneObject; //跳弾エフェクトprefab情報をクローンして格納
    private Renderer[] rend; //エフェクトレンダラー情報

    void Start()
    {
    }

    void OnCollisionEnter(Collision collision) //物体の衝突を見る関数(非貫通)
    {
        float speed_x; //x軸方向移動速度
        float speed_z; //z軸方向移動速度
        float radian; //反射後の角度を計算

        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "WeakWall") //壁と衝突した場合
        {
            if (ricochet == 0) //跳弾可能回数が0のとき
            {
                Destroy(this.gameObject); //消滅
                Debug.Log("Broke!");
                //発射タンク情報へのアクセス
            }
            else //それ以外のとき
            {
                ricochet--; //跳弾可能回数を1減らす
                //SoundEffect(1); //外部スクリプトによる命令に変更(消滅すると鳴らせない)
                Debug.Log("Wall!");
                speed_x = rb.velocity.x; //x軸方向速度の取得
                speed_z = rb.velocity.z; //z軸方向速度の取得
                radian = (float)Math.Atan2(speed_z, speed_x); //x-z平面のtanの値計算
                radian = radian * (float)(180 / Math.PI); //角度に変換
                this.transform.rotation = Quaternion.Euler(0, -radian + 90f, 0); //回転処理（進行方向）
                Instantiate(prefab, this.transform.position, Quaternion.identity); //跳弾エフェクトのprefabを作成
            }
        }
        if (collision.gameObject.tag == "Tank") //タンクと衝突した場合
        {
            Debug.Log("Hit!");
            //SoundEffect(1); //外部スクリプトによる命令に変更(消滅すると鳴らせない)
            Destroy(this.gameObject); //消滅
            //発射タンク情報へのアクセス(不要?)
        }
        if (collision.gameObject.tag == "Bullet") //弾同士で衝突した場合
        {
            Debug.Log("Bullet!");
            Instantiate(prefab, this.transform.position, Quaternion.identity);
            //SoundEffect(1); //外部スクリプトによる命令に変更(消滅すると鳴らせない)
            Destroy(this.gameObject); //消滅
            //発射タンク情報へのアクセス(不要?)
        }
        if (collision.gameObject.tag == "Mine" || collision.gameObject.tag == "Blast") //タンク・他の弾・地雷・爆風との接触
        {
            Destroy(this.gameObject); //消滅
            //発射タンク情報へのアクセス(不要?)
        }
    }
    public void Shot(float forceIndex, float init_radian, int rico) //発射速度，発射角度，跳弾回数
    {
        float force_x; //x方向の力
        float force_z; //z方向の力
        //float forceIndex = 30f; //射出力基準値 要初期化 低速15f 高速30f
        //float init_radian = 30f; //初期進行角度 要初期化

        ricochet = rico; //反射回数の代入
        rend = new Renderer[3]; //配列の長さは3
        rb = GetComponent<Rigidbody>(); //Rigidbody情報の取得
        rend[0] = transform.Find("BPS").gameObject.GetComponent<Renderer>(); //Particle System情報の取得 通常弾エフェクト
        rend[1] = transform.Find("BPS2").gameObject.GetComponent<Renderer>(); //高速弾エフェクト
        rend[2] = transform.Find("BPS3").gameObject.GetComponent<Renderer>(); //反射弾エフェクト
        //rend[3] = GameObject.Find("Burst").GetComponent<Renderer>(); //未定

        this.transform.rotation = Quaternion.Euler(0, -init_radian + 90f, 0); //回転処理（進行方向）
        init_radian = init_radian * (float)(Math.PI / 180); //ラジアン(pi)に変換
        force_x = forceIndex * (float)Math.Cos(init_radian); //x軸方向の力を計算
        force_z = forceIndex * (float)Math.Sin(init_radian); //z軸方向の力を計算
        rb.AddForce(force_x, 0, force_z, ForceMode.VelocityChange); //瞬間的に弾に力を加える(質量無視)

        if (forceIndex > 20f) rend[1].enabled = true; //速度が20以上の弾エフェクト

        if (ricochet > 2) rend[2].enabled = true; //跳弾数が2以上の弾
        else rend[0].enabled = true; //跳弾数が1以下の弾
    }
}
//Destroyは専用関数用意？