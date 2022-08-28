using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using Random = UnityEngine.Random;

namespace BlackHoleGame.Script
{
    public class DataController : MonoBehaviour
    {
        public static int moneyNum;
        public static JsonData<BigLevelData> allBigData;
        public static JsonData<SmallLevelData> allSmallData;
        public static JsonData<LanguageData> languageData;
        private static List<int> listBig;
        public static int bigLevel;
        private static int smallLevel;
        private static int levelData;
        private static int _sceneNum;
        private static TextAsset tt;
        private ResourceRequest rr;

        private static UIController UI => UIController.instance;

        public static int sceneNum
        {
            get => _sceneNum;
            set
            {
                ClearMap();
                var a = value % 3;
                switch (a)
                {
                    case 2:
                        //UI.lv1Image.color = new Color(1, 1, 1, 1);
                        UI.lv2Image.color = new Color(1, 1, 1, 1);
                        break;
                    case 1:
                        UI.lv1Image.color = new Color(1, 1, 1, 1);
                        //UI.lv2Image.color = new Color(1, 1, 1, 0.5f);
                        break;
                    case 0:
                        UI.lv1Image.color = new Color(1, 1, 1, 0.5f);
                        UI.lv2Image.color = new Color(1, 1, 1, 0.5f);
                        break;
                }

                if (value <= sceneNum && sceneNum != 0)
                {
                    GetLevelNum(bigLevel, 0);
                    _sceneNum = value;
                    return;
                }

                _sceneNum = value;
                bigLevel = value / 3;
                if (bigLevel >= allBigData.dataArray.Count)
                {
                    switch (a)
                    {
                        case 2:
                            bigLevel = Random.Range(0, allBigData.dataArray.Count);
                            smallLevel = 2;
                            break;
                        default:
                            bigLevel = Random.Range(0, allBigData.dataArray.Count);
                            for (var i = 0; i < listBig.Count; i++)
                            {
                                if (listBig[i] == -1)
                                {
                                    listBig[i] = bigLevel;
                                    break;
                                }

                                if (bigLevel == listBig[i])
                                {
                                    bigLevel = Random.Range(0, allBigData.dataArray.Count);
                                    i = -1;
                                }

                                if (i == listBig.Count - 1)
                                {
                                    listBig.RemoveAt(0);
                                    listBig.Add(bigLevel);
                                }
                            }

                            smallLevel = Random.Range(0, 2);
                            break;
                    }

                    GetLevelNum(bigLevel, smallLevel);
                    AnalyticsEvent.LevelUp(value,
                        new Dictionary<string, object>() { { "bigLevel", bigLevel }, { "smallLevel", smallLevel } });
                    return;
                }

                smallLevel = value % 3;
                GetLevelNum(bigLevel, smallLevel);
                AnalyticsEvent.LevelUp(value,
                    new Dictionary<string, object>() { { "bigLevel", bigLevel }, { "smallLevel", smallLevel } });
                PlayerPrefs.SetString(SceneData.level, sceneNum.ToString());
            }
        }

        private void Awake()
        {
            StartCoroutine(nameof(IEData));
            GetLanguageData();
        }

        private void Start()
        {
            GetAllBigLevelData();
            GetAllSmallLevelData();

            listBig = new List<int> { -1, -1, -1, -1, -1 };
        }

        /// <summary>
        ///     清除地图
        /// </summary>
        public static void ClearMap()
        {
            for (var i = 0; i < Shape.shapeList.Count; i++)
                Destroy(Shape.shapeList[i]);
            Shape.shapeList.Clear();
            for (var i = 0; i < Shape.dieShapeList.Count; i++)
                Destroy(Shape.dieShapeList[i]);
            Shape.dieShapeList.Clear();
        }

        private IEnumerator IEData()
        {
            while (true)
            {
                if (rr != null && rr.isDone)
                {
                    tt = rr.asset as TextAsset;
                    StopCoroutine("IEData");
                }

                rr = Resources.LoadAsync<TextAsset>("Data/tmpl_ layout");
                yield return rr;
            }
        }

        /// <summary>
        ///     获取所有大关卡信息
        /// </summary>
        public static void GetAllBigLevelData()
        {
            allBigData = null;
            var json = Resources.Load<TextAsset>("Data/tmpl_checkPoint").text;
            allBigData = JsonData<BigLevelData>.GetJsonData(json);
        }

        /// <summary>
        ///     获取所有小关卡信息
        /// </summary>
        public static void GetAllSmallLevelData()
        {
            allSmallData = null;
            var json = tt.text;
            allSmallData = JsonData<SmallLevelData>.GetJsonData(json);
        }

        /// <summary>
        ///     获取当前关卡信息
        /// </summary>
        /// <param name="bigLevel"></param>
        /// <param name="smallLevel"></param>
        public static void GetLevelNum(int bigLevel, int smallLevel)
        {
            levelData = int.Parse(LoadBigData(bigLevel)[smallLevel]);
            LoadData(levelData);
        }

        private static string[] LoadBigData(int num)
        {
            var bigDatas = allBigData.dataArray[num].parameter.Split(',');
            return bigDatas;
        }

        public static void LoadData(int num)
        {
            var datas = allSmallData.dataArray[num - 1].parameter.Split(',');
            var startPointDatas = allSmallData.dataArray[num - 1].startPoint.Split(':');
            SceneData.holeStartPos = new Vector3(float.Parse(startPointDatas[0]), float.Parse(startPointDatas[1]), -8);
            var mapData = allSmallData.dataArray[num - 1].Column3;
            SceneData.mapID = mapData;
            var fieldViewData = allSmallData.dataArray[num - 1].fieldView;
            SceneData.fieldView = float.Parse(fieldViewData);
            for (var i = 0; i < datas.Length; i++)
            {
                if (string.IsNullOrEmpty(datas[i])) continue;
                var singleShape = datas[i].Split(':');
                var pos = new Vector3(float.Parse(singleShape[3]), float.Parse(singleShape[4]),
                    float.Parse(singleShape[5]));
                var scale = new Vector3(float.Parse(singleShape[6]), float.Parse(singleShape[7]),
                    float.Parse(singleShape[8]));
                var eul = new Vector3(float.Parse(singleShape[9]), float.Parse(singleShape[10]),
                    float.Parse(singleShape[11]));
                var ID = singleShape[2];
                Shape.CreateShape(pos, scale, eul, singleShape[1], singleShape[12], ID, singleShape[0]);
            }
        }

        public static void GetLanguageData()
        {
            languageData = null;
            var json = Resources.Load<TextAsset>("Data/tmpl_language").text;
            languageData = JsonData<LanguageData>.GetJsonData(json);
        }
    }

    public class SceneData
    {
        /// <summary>
        ///     黑洞尺寸
        /// </summary>
        private static float _holeSize = 1.25f;

        /// <summary>
        ///     黑洞移动速度
        /// </summary>
        public static float holeMoveSpeed = 0.015f;

        /// <summary>
        ///     黑洞开始坐标
        /// </summary>
        public static Vector3 holeStartPos = new Vector3(0, -1, -8);

        /// <summary>
        ///     摄像头距离
        /// </summary>
        public static float fieldView = 31.8f;

        /// <summary>
        ///     地图类型ID
        /// </summary>
        public static int mapID = 1;

        /// <summary>
        ///     皮肤ID
        /// </summary>
        public static int skinID = 0;

        /// <summary>
        ///     游戏货币
        /// </summary>
        public static string money = "MoneyNum";

        /// <summary>
        ///     关卡数
        /// </summary>
        public static string level = "SceneNum";

        /// <summary>
        ///     语言编号
        /// </summary>
        public static string flag = "FlagNum";

        /// <summary>
        ///     是否震动
        /// </summary>
        public static string isShock = "IsShock";

        /// <summary>
        ///     皮肤编号
        /// </summary>
        public static string skin = "SkinID";

        /// <summary>
        ///     皮肤状态
        /// </summary>
        public static string skinState = "SkinState";

        /// <summary>
        ///     礼物条增加数量
        /// </summary>
        public static float giftBarAddNum = 0.51f;

        public static float holeSize
        {
            get => _holeSize;
            set
            {
                Hole.instance.scale = new Vector3(value / 2, value / 2, 2);
                _holeSize = value;
            }
        }

        public static event Action<int> onOnDiamondChanged;

        public static int DiamondNum
        {
            get => PlayerPrefs.GetInt(SceneData.money, 500);
            set
            {
                PlayerPrefs.SetInt(SceneData.money, value);
                onOnDiamondChanged?.Invoke(value);
            }
        }

        private static string holeSkin = "HoleSkinID";

        private static HoleSkinDataList holeSkinDataList;

        public static HoleSkinDataList GetSkinDataLists()
        {
            holeSkinDataList = JsonUtility.FromJson<HoleSkinDataList>(SkinData);
            return holeSkinDataList;
        }

        public static void SetSkinDataLists(HoleSkinDataList value)
        {
            holeSkinDataList = value;
            SkinData=JsonUtility.ToJson(holeSkinDataList);
        }

        public static void SetSkinList()
        {
            var skinDataList = SkinData;
            // var json = JsonUtility.ToJson(skinDataList);
            // var jsonObject = JsonUtility.FromJson<HoleSkinDataList>(json);
        }

        /// <summary>
        ///    获取默认皮肤数据
        /// </summary>
        /// <returns></returns>
        private static HoleSkinDataList GetDefaultSkinDataList()
        {
            var skinIDList = new List<HoleSkinData>(HoleSkinLoadManager.SkinLength);
            for (var i = 0; i < HoleSkinLoadManager.SkinLength; i++)
            {
                skinIDList.Add(new HoleSkinData
                {
                    skinID = i,
                    skinState = false,
                    skinType = EHoleSkinType.Common
                });
            }

            var skinDataList = new HoleSkinDataList { data = skinIDList.ToArray() };
            return skinDataList;
        }

        private static string SkinData
        {
            get => PlayerPrefs.GetString(SceneData.holeSkin, JsonUtility.ToJson(GetDefaultSkinDataList()));
            set { PlayerPrefs.SetString(SceneData.holeSkin, value); }
        }

        [Serializable]
        public struct HoleSkinData
        {
            [SerializeField] public int skinID;
            [SerializeField] public bool skinState;
            [SerializeField] public EHoleSkinType skinType;
        }

        [Serializable]
        public struct HoleSkinDataList
        {
            [SerializeField] public HoleSkinData[] data;
        }

        [Serializable]
        public enum EHoleSkinType
        {
            /// <summary>
            ///    普通皮肤
            /// </summary>
            Common,

            /// <summary>
            ///    精英皮肤
            /// </summary>
            Rare,

            /// <summary>
            ///   卓越皮肤
            /// </summary>
            Epic,

            /// <summary>
            /// 稀有皮肤
            /// </summary>
            Legendary,
        }
    }
}