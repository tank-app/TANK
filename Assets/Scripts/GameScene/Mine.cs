using System;
using System.Threading.Tasks;
using UnityEngine;

public class Mine : MonoBehaviour {
    private Renderer rend;
    void Start () {
        //rend = GameObject.Find ("Big Explosion").GetComponent<Renderer> ();
        Gobomb (); //GoBomb関数実行
    }

    void Update () {

    }

    async void Gobomb () {
        Prediction (); //爆発カウントダウン実行
        await Task.Delay (5000); //5秒待機
        Explosion (); //爆発関数実行
    }

    void Prediction () {
        Debug.Log ("Predict!"); //ログ出力

    }

    void Explosion () {
        Debug.Log ("Bomb!"); //ログ出力
    }

}