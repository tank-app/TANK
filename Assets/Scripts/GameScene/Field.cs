using System;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public GameObject insideWall;   //Prefab情報を格納
    public GameObject hole;         //Prefab情報を格納
    private GameObject cloneObject; //Instantiate関数により生成したインスタンス情報(GameObject型)を格納
    private Transform cloneObjectTransform; //cloneObjectのTransformコンポーネント情報を格納
    void Start()
    {
        insideObject(1, 10, 1); //insideObject関数実行
    }

    void Update()
    {

    }
    void insideObject(int x, int y, int z)
    {
        cloneObject = Instantiate(insideWall, new Vector3(5.0f * x + 2.5f, y / 2.0f, 5.0f * z + 2.5f), Quaternion.identity); //insideWallを指定した座標に生成し，そのGameObject情報をcloneObjectに格納
        cloneObjectTransform = cloneObject.GetComponent<Transform>(); //cloneObjectのTransformコンポーネント情報をcloneObjectTransformに格納
        cloneObjectTransform.localScale = new Vector3(5, y, 5); //壁の高さを変更
    }
}