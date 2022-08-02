using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BlackHoleGame.Script
{
    public class PoolManager
    {
        private static Dictionary<string, GameObject> prefabs; //预设集合

        //内存池集合
        private static Dictionary<string, ObjPool> _poolObjs;

        public static Dictionary<string, ObjPool> poolObjs
        {
            get
            {
                if (_poolObjs == null) _poolObjs = new Dictionary<string, ObjPool>();
                return _poolObjs;
            }
        }

        /// <summary>
        ///     通过键查找预设
        /// </summary>
        /// <param name="name">键</param>
        /// <returns></returns>
        public static GameObject GetPre(string name)
        {
            GameObject go = null;
            if (prefabs == null) prefabs = new Dictionary<string, GameObject>();
            if (prefabs.ContainsKey(name)) //查找集合中是否存在这个键
            {
                go = prefabs[name];
            }
            else //不存在
            {
                go = Resources.Load<GameObject>(name); //在文件夹中读取预设
                if (go == null) //如果没有找到
                    throw new Exception("不存在该预设");
                prefabs.Add(name, go); //将找到的预设添加到集合
            }

            return go;
        }

        public static ObjPool GetObjPool(string name)
        {
            if (poolObjs.ContainsKey(name) == false) AddObjPool(name);
            return poolObjs[name];
        }

        public static void AddObjPool(string name)
        {
            var objpool = new ObjPool();
            poolObjs.Add(name, objpool);
        }


        /// <summary>
        ///     生成，创建
        /// </summary>
        /// <returns></returns>
        public static GameObject CreatObj(string name)
        {
            var objPool = GetObjPool(name);
            var obj = objPool.GetUnUesdObj(name);
            return obj;
        }

        /// <summary>
        ///     删除
        /// </summary>
        public static void Destory(GameObject go)
        {
            go.SetActive(false);
        }
    }

    public class ObjPool
    {
        private List<GameObject> _objList;

        public List<GameObject> objList
        {
            get
            {
                if (_objList == null) _objList = new List<GameObject>();
                return _objList;
            }
        }

        /// <summary>
        ///     获取未使用的对象
        /// </summary>
        /// <returns></returns>
        public GameObject GetUnUesdObj(string name)
        {
            GameObject go = null;
            for (var i = 0; i < objList.Count; i++)
                if (objList[i].activeSelf == false)
                {
                    objList[i].SetActive(true);
                    go = objList[i];
                    break;
                }

            if (go == null) //遍历后没有找到
            {
                go = Object.Instantiate(PoolManager.GetPre(name));
                var objPool = PoolManager.GetObjPool(name);
                objPool.objList.Add(go);
                //this.objList.Add(go);
            }

            return go;
        }
    }
}