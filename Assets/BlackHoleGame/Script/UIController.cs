﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using DG.Tweening;
using SDK;
using Transsion.UtilitiesCrowd;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BlackHoleGame.Script
{
    public class UIController : Base
    {
        public static UIController instance;
        private char[] crr;
        public GameObject diePanel;
        public Image dieTimeImage;
        [NonSerialized] public float dieTimeNum = GlobalConfig.RevivalTime;
        public Text dieTimeText;
        public Image[] flagImages;
        public Text FPSText;
        public Image holeImage;
        public InputField levelNum;
        public Image lv1Image;
        public Image lv2Image;
        public GameObject lvPanel;
        public Text lvTextLeft;

        public Text lvTextRight;

        // public Text moneyText;
        public Button payButton;
        public GameObject setPanel;
        public GameObject shock;
        public Toggle shockSwitch;
        public Text skinMoney;
        public GameObject skinPanel;
        public List<GameObject> SkinGameObjects;
        public Text starText;
        public GameObject startPanel;
        public GameObject tick;
        public Button winButton;
        public GameObject winPanel;
        public RectTransform floatTransform;

        /// <summary>
        /// diamond number
        /// </summary>
        [SerializeField] private Text diamondText;


        private List<int> skinNeedMoney = GlobalConfig.SkinCastMoneyList;

        /// <summary>
        /// 试用皮肤的名称
        /// </summary>
        private int testSkinIndex = 0;

        public int moneyTextNum
        {
            get => SceneData.DiamondNum;
            set
            {
                SceneData.DiamondNum = value;
                OnDiamondChanged(value);
            }
        }

        private int skinNum
        {
            get => moneyTextNum;
            set => OnDiamondChanged(value);
        }

        public int starTextNum
        {
            get => int.Parse(starText.text);
            set => starText.text = value.ToString();
        }

        public void ClosePay(string str)
        {
            payButton.gameObject.SetActive(false);
        }

        private void Awake()
        {
            //Screen.SetResolution(1080, 1920, false);
            instance = this;
        }

        private void Start()
        {
            StartCoroutine(nameof(Iefps));
            anim.RotaImage();
            crr = new[] { '0', '0', '0', '0', '0', '0' };
            var str = PlayerPrefs.GetString(SceneData.skinState);
            if (string.IsNullOrEmpty(str))
            {
                str = new string(crr);
                PlayerPrefs.SetString(SceneData.skinState, str);
            }

            var trigger = startPanel.GetComponent<EventTrigger>();
            var entry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerDown
            };
            entry.callback.AddListener(data =>
            {
                // Debug.Log("Entering");
                // NativeConnect.Connect.ShowFloatingWindow(false);
                StartGame();
            });
            trigger.triggers.Add(entry);
            // SceneData.onOnDiamondChanged += OnDiamondChanged;
            // StartCoroutine(BindDiamondLater());
            Hole.instance.LoadMap();
            this.lvPanel.SetActive(true);
        }

        private IEnumerator BindDiamondLater()
        {
            yield return new WaitForEndOfFrame();
            // SceneData.onOnDiamondChanged += OnDiamondChanged;
        }

        private void OnDiamondChanged(int obj)
        {
            diamondText.text = obj.ToString("N0");
            skinMoney.text = obj.ToString("N0");
        }


        private void OnDestroy()
        {
            SceneData.onOnDiamondChanged -= OnDiamondChanged;
        }

        /// <summary>
        ///     开始游戏
        /// </summary>
        public void StartGame()
        {
            anim.SetAndSkinButtonMove();
            startPanel.SetActive(false);
            Hole.instance.enabled = true;
            Hole.instance.StartMove();
            CrowdGameAnalytics.EventLevelBegin(DataController.sceneNum);
        }

        private IEnumerator Iefps()
        {
            while (true)
            {
                FPSText.text = (1 / Time.deltaTime).ToString(CultureInfo.InvariantCulture);
                yield return new WaitForSeconds(1);
            }
        }

        public void StartAddStarNum()
        {
            StartCoroutine("IEAddStarNum");
            Tweener a = starText.transform.DOScale(Vector3.one * 1.8f, 0.5f);
            a.SetEase(Ease.Linear);
            a.SetLoops(3, LoopType.Yoyo);
        }

        private IEnumerator IEAddStarNum()
        {
            var startStarNum = starTextNum;
            while (starTextNum <= startStarNum * 2)
            {
                var a = startStarNum / 15;
                starTextNum += a;
                yield return null;
            }

            starTextNum = startStarNum * 2;
        }

        /// <summary>
        ///     钻石数量增加协程
        /// </summary>
        /// <returns></returns>
        private IEnumerator IEAddMoney()
        {
            var startMoneyNum = moneyTextNum;
            while (moneyTextNum <= startMoneyNum + gameCon.addMoneyNum)
            {
                var a = gameCon.addMoneyNum / 15;
                moneyTextNum += a;
                yield return null;
            }

            moneyTextNum = startMoneyNum + gameCon.addMoneyNum;
            // PlayerPrefs.SetInt(SceneData.money, moneyTextNum);
            SceneData.DiamondNum = moneyTextNum;
            gameCon.addMoneyNum = gameCon.winMoneyNum;
        }

        /// <summary>
        ///     钻石花费减少协程
        /// </summary>
        /// <returns></returns>
        private IEnumerator IESpendMoney()
        {
            var startMoneyNum = moneyTextNum;
            while (moneyTextNum >= startMoneyNum - gameCon.skinMoneyNum)
            {
                var a = gameCon.skinMoneyNum / 15;
                moneyTextNum -= a;
                skinNum -= a;
                yield return null;
            }

            moneyTextNum = startMoneyNum - gameCon.skinMoneyNum;
            skinNum = moneyTextNum;
            // PlayerPrefs.SetInt(SceneData.money, moneyTextNum);
            SceneData.DiamondNum = moneyTextNum;
        }

        /// <summary>
        ///     开始钻石增加协程
        /// </summary>
        public void StartAddMoney()
        {
            StartCoroutine(nameof(IEAddMoney));
            foreach (var t in anim.addDiasImage)
                t.transform.localPosition = Vector3.zero;
        }

        /// <summary>
        ///     倒计时协程
        /// </summary>
        /// <returns></returns>
        private IEnumerator IECountDown()
        {
            while (true)
            {
                if (dieTimeNum <= 0) gameCon.DieNewGame();
                dieTimeNum -= Time.deltaTime;
                dieTimeImage.fillAmount = dieTimeNum / GlobalConfig.RevivalTime;
                dieTimeText.text = ((int)(dieTimeNum + 1)).ToString();
                yield return new WaitForSeconds(0.02f);
            }
        }

        /// <summary>
        ///     停止倒计时协程
        /// </summary>
        public void StopCountDown()
        {
            StopCoroutine(nameof(IECountDown));
        }

        /// <summary>
        ///     打开死亡界面
        /// </summary>
        public void OpenDiePanel()
        {
            diePanel.gameObject.SetActive(true);
            lvPanel.SetActive(false);
            StartCoroutine(nameof(IECountDown));
        }

        /// <summary>
        ///     打开设置界面
        /// </summary>
        public void OpenSetPanel()
        {
            // NativeConnect.Connect.showBlock(s => { });
            NativeConnect.Connect.ShowFloatingWindow(true);
            Hole.instance.enabled = false;
            setPanel.SetActive(true);
        }

        /// <summary>
        ///     关闭设置界面
        /// </summary>
        public void CloseSetPanel()
        {
            NativeConnect.Connect.ShowFloatingWindow(false);
            setPanel.SetActive(false);
            //Hole.instance.enabled = true;
        }

        /// <summary>
        ///     语言左按钮
        /// </summary>
        public void LeftFlag()
        {
            lang.flagNum--;
            if (lang.flagNum < 0)
                lang.flagNum = 1;
            ChangeFlag();
        }

        /// <summary>
        ///     语言右按钮
        /// </summary>
        public void RightFlag()
        {
            lang.flagNum++;
            if (lang.flagNum > 1)
                lang.flagNum = 0;
            ChangeFlag();
        }

        /// <summary>
        ///     改变语言
        /// </summary>
        public void ChangeFlag()
        {
            foreach (var item in flagImages)
                item.gameObject.SetActive(false);
            flagImages[lang.flagNum].gameObject.SetActive(true);
            PlayerPrefs.SetInt(SceneData.flag, lang.flagNum);
        }

        /// <summary>
        ///     是否震动
        /// </summary>
        public void ChangeShock()
        {
            shock.SetActive(!shock.activeSelf);
            PlayerPrefs.SetInt(SceneData.isShock, Convert.ToInt32(shockSwitch.isOn));
        }

        /// <summary>
        ///     打开皮肤界面
        /// </summary>
        public void OpenSkinPanel()
        {
            // NativeConnect.Connect.showBlock(s => { });
            NativeConnect.Connect.ShowFloatingWindow(true);
            skinMoney.text = diamondText.text;
            Hole.instance.enabled = false;
            for (var i = 0; i < SkinGameObjects.Count; i++)
            {
                var o = SkinGameObjects[i];
                o.transform.GetChild(0).GetComponentInChildren<Text>().text = skinNeedMoney[i].ToString();
            }

            skinPanel.SetActive(true);
            var go = GameObject.Find("S" + SceneData.skinID);
            var str = PlayerPrefs.GetString(SceneData.skinState);
            if (!string.IsNullOrEmpty(str))
            {
                crr = str.ToCharArray();
                for (var i = 1; i < crr.Length; i++)
                {
                    var button = GameObject.Find("S" + i);
                    var tryButton = button.transform.GetChild(1).GetComponent<Button>();
                    if (crr[i] == '1')
                    {
                        var im = button.GetComponentsInChildren<Image>();
                        foreach (var t in im)
                            if (t.name.Contains("Image"))
                                t.gameObject.SetActive(false);

                        button.GetComponent<Image>().color = Color.white;
                        tryButton.gameObject.SetActive(false);
                    }
                    else
                    {
                        tryButton.gameObject.SetActive(true);
                        tryButton.onClick.RemoveAllListeners();
                        var i2 = i;
                        tryButton.onClick.AddListener((() =>
                        {
                            var i1 = i2;
                            SdkSystem.Instance.ShowRewardVideoAd(() =>
                                {
                                    // Debug.Log("视频广告播放完成");
                                    // anim.ShowHints(anim.videoHintsText);
                                    this.testSkinIndex = i1;
                                    // tryButton.gameObject.SetActive(false);
                                    StartCoroutine(ChangeSkinLater(i1));
                                    this.CloseSkinPanel();
                                },
                                () =>
                                {
                                    // Debug.Log("视频广告播放失败");
                                    anim.ShowHints(anim.videoHintsText);
                                });
                        }));
                    }
                }
            }

            var pos = tick.transform.localPosition;
            tick.transform.SetParent(go.transform, false);
            tick.transform.localPosition = pos;
        }

        private IEnumerator ChangeSkinLater(int i1)
        {
            yield return new WaitForSeconds(0.1F);
            ChangeSkin(this.testSkinIndex, true);
        }

        /// <summary>
        ///     关闭皮肤界面
        /// </summary>
        public void CloseSkinPanel()
        {
            skinPanel.SetActive(false);
            NativeConnect.Connect.ShowFloatingWindow(false);
        }

        /// <summary>
        ///     选择皮肤
        /// </summary>
        public void SelectSkin()
        {
            var go = EventSystem.current.currentSelectedGameObject.transform;
            var image = go.GetComponent<Image>();
            var num = int.Parse(go.name[1].ToString());
            if (image.color != Color.white)
            {
                gameCon.skinMoneyNum = skinNeedMoney[num - 1];
                // Debug.Log(gameCon.skinMoneyNum);
                if (moneyTextNum < gameCon.skinMoneyNum)
                {
                    anim.ShowHints(anim.moneyHintsText);
                    return;
                }

                var im = go.GetComponentsInChildren<Image>();
                im[1].gameObject.SetActive(false);
                image.color = Color.white;
                StartCoroutine(nameof(IESpendMoney));
                crr[num] = '1';
                var str = new string(crr);
                PlayerPrefs.SetString(SceneData.skinState, str);
            }

            ChangeSkin(num);
            var pos = tick.transform.localPosition;
            tick.transform.SetParent(go, false);
            tick.transform.localPosition = pos;
        }


        public void TrySelectSkin()
        {
            var go = EventSystem.current.currentSelectedGameObject.transform;
            // Debug.Log(go.name);
        }

        /// <summary>
        /// 改变皮肤
        /// </summary>
        /// <param name="a">皮肤下标</param>
        /// <param name="isTry">是否是试用</param>
        public void ChangeSkin(int a, bool isTry = false)
        {
            Resources.UnloadUnusedAssets();
            if (!isTry)
            {
                SceneData.skinID = a;
                PlayerPrefs.SetInt(SceneData.skin, SceneData.skinID);
            }
            else
            {
                SceneData.skinID = a;
            }

            /*var go = Hole.instance.transform.parent.gameObject;
        Hole.instance.transform.parent = null;
        go.SetActive(false);*/
            // var op = Addressables.LoadAssetAsync<GameObject>($"Map{a}");
            var go = Resources.Load<GameObject>($"Maps/Map{a}"); //op.WaitForCompletion();
            var map = Instantiate(go); //gameCon.maps[a];
            GameController.instance.GameMap = map;
            // Addressables.Release(go);
            // Resources.UnloadAsset(go);
            Hole.instance.SetPlaner(map.transform.Find("Plane"));
            map.SetActive(true);
            // Hole.instance.transform.parent = map.transform;
            // StartGame();
        }

        /// <summary>
        /// 开始下一关
        /// </summary>
        public void ResetAndNext()
        {
            CrowdGameAnalytics.EventLevelEnd(DataController.sceneNum,true);
            if (this.testSkinIndex != 0)
            {
                UI.ChangeSkin(PlayerPrefs.GetInt(SceneData.skin));
                this.testSkinIndex = 0;
            }
        }
    }
}