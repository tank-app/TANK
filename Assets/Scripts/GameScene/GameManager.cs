using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // CSVのファイル名
    int[][] field = new int[17][]; //行,列の順 

    // CSVを配列に読み込む関数
    void csvReader(string csvFileName)
    {
        StreamReader sr = new StreamReader(csvFileName); // csvファイルをStreamReaderにセット
            {
                int i = 0; // 行数: whileで使う
                // Streamの終わりまでループ
                while (!sr.EndOfStream)
                {
                    // CSVファイルを1行読み込み
                    string line = sr.ReadLine();
                    // コンマ区切りでstring配列に格納
                    string[] strValues = line.Split(',');
                    // int配列に変換
                    int[] intValues = Array.ConvertAll(strValues, int.Parse);
                    // int配列をstring配列配列のi行目に代入
                    field[i] = intValues;
                    // 次の行へ
                    i++;
                }
            }
    }
    void Start()
    {

    }

    void Update()
    {

    }
}