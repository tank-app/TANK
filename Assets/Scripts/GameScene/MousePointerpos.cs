using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MousePointerpos : MonoBehaviour
{
    //Canvasの変数
    public Canvas canvas;
    //キャンバス内のレクトトランスフォーム
    public RectTransform canvasRect;
    //マウスの位置の最終的な格納先
    public Vector2 MousePos;
    //自身のゲームオブジェクトのRectTransform
    public RectTransform pointer;

    // Start is called before the first frame update
    void Start()
    {
        //マウスポインター非表示
        Cursor.visible = false;
        //HierarchyにあるCanvasオブジェクトを探してcanvasに入れいる
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        //canvas内にあるRectTransformをcanvasRectに入れる
        canvasRect = canvas.GetComponent<RectTransform>();
        //自身のゲームオブジェクトのRectTransformをpointerに入れる
        pointer = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * CanvasのRectTransform内にあるマウスの位置をRectTransformのローカルポジションに変換する
         * canvas.worldCameraはカメラ
         * 出力先はMousePos
         */
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, canvas.worldCamera, out MousePos);
        //RectTransformの座標を更新
        pointer.anchoredPosition = new Vector2(MousePos.x, MousePos.y);
    }
}
