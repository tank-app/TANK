using System.Collections;
using UnityEngine;

public class MouseSynchronizeObject : MonoBehaviour
{
    //位置情報
    private static Vector3 position;
    //スクリーン座標をワールド座標に変換した位置座標
    public static Vector3 screenToWorldPointPosition;
    void Start()
    {

    }

    void Update()
    {
        //Vector3でマウス位置座標を取得
        position = Input.mousePosition;
        //Z軸修正
        position.z = 10f;
        //マウス位置座標をスクリーン座標からワールド座標に変換する
        screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(position);
        //ワールド座標に変換されたマウス座標を代入
        gameObject.transform.position = screenToWorldPointPosition;

    }
}