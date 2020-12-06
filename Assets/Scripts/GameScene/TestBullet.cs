using System;
using System.Threading.Tasks;
using UnityEngine;

public class TestBullet : MonoBehaviour
{
    private GameObject[] bullet; //弾オブジェクト情報の格納
    public GameObject prefab; //弾プレハブオブジェクトの格納
    private Bullet bulletScript; //弾のスクリプト情報
    public int limit; //発射可能弾数
    async void Start ()
    {
        bullet = new GameObject[limit]; //指定個数分のインスタンスを生成
        for(int i = 0; i < 10; i++)
        {
            await Task.Delay(500);
            MakeBullet(10f, 10f, 30f, 0f, 3); //Temporary
        }

    }

    void MakeBullet (float x, float z, float speed, float radian, int rico)
    {
        for (int i = 0; i < limit; i++)
        {
            if (bullet[i] == null) //弾の発射数が限度に達していないか確認
            {
                bullet[i] = Instantiate (prefab, new Vector3 (x, 0, z), new Quaternion(0, 0, 0, 0) ); //配列の空き番号に弾を格納
                bulletScript = bullet[i].GetComponent<Bullet>(); //発射した弾のスクリプト情報を取得
                bulletScript.Shot(speed, radian, rico); //発射関数実行
                Debug.Log("bullet " + i + " instantiated succesfully!"); //ログ
                break; //発射できた場合は，追加検索を行わない
            }
        }
    }
}