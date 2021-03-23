using System;
using System.Collections.Generic;
using UnityEngine;

public class JsonData<T>
{
    public List<T> dataArray;

    public static JsonData<T> GetJsonData(string str)
    {
        JsonData<T> tData = null;
        tData = JsonUtility.FromJson<JsonData<T>>(str);
        return tData;
    }
}

//小关信息
[Serializable]
public class SmallLevelData
{
    public int Column3; //背景颜色
    public string fieldView; //场景视图距离
    public int ID; //关卡ID
    public string name; //关卡名称
    public string parameter; //关卡信息
    public string startPoint; //初始坐标
}

//大关信息
[Serializable]
public class BigLevelData
{
    public int ID;
    public string name;
    public string parameter;
}

//多语言信息
[Serializable]
public class LanguageData
{
    public string CN;
    public string Desc;
    public string EN;
    public int ID;
}