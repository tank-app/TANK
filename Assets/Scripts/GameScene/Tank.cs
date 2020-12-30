using System;
using UnityEngine;

public class Tank : MonoBehaviour
{
    private Transform bodyTransform; //bodyオブジェクトのTransformコンポーネント情報
    private Transform tarrotTransform; //tarrotオブジェクトのTransformコンポーネント情報
    private Transform pointerPos; //pointerの3D座標把握用オブジェクトのTransform
    public bool playerFlag = false;


    void Start()
    {
        bodyTransform = this.transform.Find("Body").GetComponent<Transform>(); //BodyオブジェクトのTransformコンポーネント情報を取得
        tarrotTransform = this.transform.Find("Tarrot").GetComponent<Transform>(); //BatteryオブジェクトのTransformコンポーネント情報を取得
        if (playerFlag) pointerPos = GameObject.Find("PointerPosition3D").GetComponent<Transform>();
    }

    void Update()
    {
        if (playerFlag)
        {
            TarrotBetweenPointer();
        }


        //this.transform.position += new Vector3(-0.01f, 0, 0); //このスクリプトがアタッチされているオブジェクトのTransform.positionをx軸方向に-0.01ずつ移動する
    }

    void TarrotBetweenPointer()
    {
        float radian;
        radian = (float)Math.Atan2(pointerPos.position.z - tarrotTransform.position.z, pointerPos.position.x - tarrotTransform.position.x); //x-z平面のtanの値計算
        radian = radian * (float)(180 / Math.PI); //角度に変換
        TurnTarrot(radian);
    }
    void TurnTarrot(float radian)
    {
        tarrotTransform.localRotation = Quaternion.Euler(-90f, 0f, -radian + 90f); //回転処理（進行方向）
    }


}