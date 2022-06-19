using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameController : Base
{
    public static GameController instance;
    private bool _isDie;
    private bool _isWin;

    [FormerlySerializedAs("GameMain")] [SerializeField]
    private Transform gameMain;

    public Transform GameMain => gameMain;

    public int addMoneyNum;

    public GameObject caizi;

    // public AssetReference caizi;
    public GameObject[] gos;
    private Transform[] gosPos;
    public GameObject holeGo;
    public GameObject[] maps;
    public int skinMoneyNum;
    public int winMoneyNum;

    private GameObject _gameMap;

    public GameObject GameMap
    {
        get => _gameMap;
        set
        {
            if (_gameMap != null)
            {
                _gameMap.SetActive(false);
                Addressables.ReleaseInstance(_gameMap);
                // Resources.UnloadAsset(_gameMap);
                Destroy(_gameMap);
                Resources.UnloadUnusedAssets();
                _gameMap = null;
            }

            _gameMap = value;
        }
    }

    public bool isDie
    {
        get => _isDie;
        set
        {
            if (value)
            {
                Hole.instance.StopMove();
                anim.DieSnake();
                NativeConnect.Connect.ShowFloatingWindow(true);
            }
            else
            {
                UI.diePanel.gameObject.SetActive(false);
                UI.dieTimeNum = 10;
                Hole.instance.StartMove();
                UI.lvPanel.SetActive(true);
            }

            _isDie = value;
        }
    }

    public bool isWin
    {
        get => _isWin;
        set
        {
            if (value)
            {
                Hole.instance.StopMove();
                var a = DataController.sceneNum % 3;
                switch (a)
                {
                    case 2:
                        Invoke(nameof(WinLiZiPanel), 1f);
                        break;
                    //case 1:
                    //    Invoke("WinLiZiPanel", 1f);
                    //    break;
                    //case 0:
                    //    Invoke("WinLiZiPanel", 1f);
                    //    break;
                    default:
                        StartCoroutine("IEHoleBigger");
                        break;
                }

                NativeConnect.Connect.ShowFloatingWindow(true);
            }
            else
            {
                Hole.instance.StartMove();
                UI.lvPanel.SetActive(true);
            }

            _isWin = value;
        }
    }

    /// <summary>
    ///     显示获胜界面
    /// </summary>
    public void WinLiZiPanel()
    {
        // anim.SpinShowVideo();
        UI.winPanel.gameObject.SetActive(true);
        UI.starTextNum = addMoneyNum;
        anim.WinTextMove();
        UI.lvPanel.SetActive(false);
        // var cz = caizi.LoadAssetAsync<GameObject>();
        var go = caizi;
        var particleSystem = Instantiate(go, gameMain).GetComponent<ParticleSystem>();
        // Addressables.Release(cz);
        particleSystem.gameObject.SetActive(true);
        particleSystem.Play();
        StartCoroutine(ReleaseParticle(particleSystem));

        Diamond.CreateDia(addMoneyNum);
    }

    private IEnumerator ReleaseParticle(ParticleSystem particleSystem1)
    {
        yield return new WaitForSeconds(3.0f);
        GameObject o;
        (o = particleSystem1.gameObject).SetActive(false);
        Addressables.ReleaseInstance(o);
        // Resources.UnloadAsset(o);
        Addressables.ReleaseInstance(o);
        Destroy(o);
        Resources.UnloadUnusedAssets();
    }

    public void Win()
    {
        if (Shape.shapeList.Count == 0 && isWin == false && isDie == false)
        {
            ShowBlock(DataController.sceneNum % 3 != 2);
            /*if (DataController.sceneNum % 3 == 2)
            {u
                ShowBlock();
            }
            else
            {
                isWin = true;
            }*/
            /*if (DataController.sceneNum % 3 == 2 && DataController.bigLevel > 2 &&
                (DataController.bigLevel + 1) % 3 != 0)
            else
                isWin = true;*/
            NativeConnect.Connect.showBanner();
        }
    }

    /// <summary>
    ///     黑洞变大
    /// </summary>
    /// <returns></returns>
    private IEnumerator IEHoleBigger()
    {
        Hole.instance.lizi.gameObject.SetActive(false);
        HoleShader.instance.StartShader();
        while (true)
        {
            SceneData.holeSize += 0.45f;
            if (SceneData.holeSize >= 30)
                Smaller();
            yield return new WaitForSeconds(0.02f);
        }
    }

    private void Smaller()
    {
        StartCoroutine("IEHoleImage");
        UI.winPanel.gameObject.SetActive(false);
        Hole.instance.ResetPos();
        HoleShader.instance.StopShader();
        SceneData.holeSize = 1.25f;
        DataController.sceneNum++;
        if (DataController.sceneNum % 3 == 0)
        {
            UI.lvTextLeft.text = (DataController.sceneNum / 3 + 1).ToString();
            UI.lvTextRight.text = (DataController.sceneNum / 3 + 2).ToString();
        }

        ReGos();
    }

    private IEnumerator IEHoleImage()
    {
        StopCoroutine(nameof(IEHoleBigger));
        while (true)
        {
            UI.holeImage.gameObject.SetActive(true);
            UI.holeImage.transform.localScale -= Vector3.one * 0.4f;
            if (UI.holeImage.transform.localScale.x <= 1)
            {
                UI.holeImage.gameObject.SetActive(false);
                UI.holeImage.transform.localScale = new Vector3(18, 18, 1);
                isWin = false;
                StopCoroutine("IEHoleImage");
                Hole.instance.lizi.gameObject.SetActive(true);
                Hole.instance.lizi.Stop();
                if (DataController.sceneNum % 3 == 0)
                {
                    Hole.instance.StopMove();
                    anim.SetAndSkinButtonCome();
                    Hole.instance.enabled = false;
                }

                UI.startPanel.SetActive(true);
            }

            yield return new WaitForSeconds(0.02f);
        }
    }

    private void Awake()
    {
        // NativeConnect.Connect.Init();
        //PlayerPrefs.DeleteAll();
        Application.targetFrameRate = 30;
        instance = this;
        winMoneyNum = Random.Range(15, 26);
        addMoneyNum = winMoneyNum;
    }

    private void Start()
    {
        StartSet();
#if TEST
        if (UI.moneyTextNum==0)
        {
            UI.moneyTextNum = 10000;
        }
#endif
    }

    private void StartSet()
    {
        UI.moneyTextNum = PlayerPrefs.GetInt(SceneData.money, 500);
        lang.flagNum = PlayerPrefs.GetInt(SceneData.flag);
        UI.ChangeSkin(PlayerPrefs.GetInt(SceneData.skin));
        UI.shockSwitch.isOn = Convert.ToBoolean(PlayerPrefs.GetInt(SceneData.isShock));
        UI.shock.SetActive(UI.shockSwitch.isOn);
        UI.ChangeFlag();
    }

    /// <summary>
    ///     重置场景模型
    /// </summary>
    private void ReGos()
    {
        var games = Resources.LoadAll<GameObject>("Tree" + SceneData.skinID);
        foreach (var t in games) Instantiate(t, Hole.instance.transform.parent, false);

        winMoneyNum = Random.Range(15, 26);
        addMoneyNum = winMoneyNum;
    }

    public void LoadMap()
    {
        DataController.ClearMap();
        var num = int.Parse(UI.levelNum.text);
        DataController.bigLevel = (num - 1) / 3;
        DataController.sceneNum = num - 1;
    }

    /// <summary>
    ///     死亡后重新开始
    /// </summary>
    public void DieNewGame()
    {
        DataController.ClearMap();
        isDie = false;
        DataController.sceneNum -= DataController.sceneNum % 3;
        Hole.instance.ResetPos();
        UI.StopCountDown();
        UI.startPanel.SetActive(true);
    }

    /// <summary>
    ///     死亡后继续游戏
    /// </summary>
    public void DieContinueGame()
    {
        isDie = false;
        Hole.instance.ResetPos();
        UI.StopCountDown();
        UI.startPanel.SetActive(true);
    }

    /// <summary>
    ///     死亡后显示激励视频
    /// </summary>
    public void ShowVideo()
    {
        if (NativeConnect.Connect.VideoState)
            NativeConnect.Connect.showVideo("DefaultRewardedVideo", delegate(string str)
            {
                if (str.Equals("True")) DieContinueGame();
                else if (str.Equals("Close")) DieNewGame();
                else if (str.Equals("False")) anim.ShowHints(anim.videoHintsText);
            });
        else
            //激励视频未加载完成
            anim.ShowHints(anim.videoHintsText);
    }

    /// <summary>
    ///     过关后显示激励视频
    /// </summary>
    public void WinShowVideo()
    {
        if (NativeConnect.Connect.VideoState)
            NativeConnect.Connect.showVideo("Rvdoublediamond", delegate(string str)
            {
                if (str.Equals("True"))
                {
                    UI.StartAddStarNum();
                    Diamond.CreateDia(addMoneyNum);
                    addMoneyNum *= 2;
                    Invoke("WinNextLevel", 1f);
                }
                else if (str.Equals("Close"))
                {
                    WinNextLevel();
                }
                else if (str.Equals("False"))
                {
                    anim.ShowHints(anim.videoHintsText);
                }
            });
        else
            //激励视频未加载完成
            anim.ShowHints(anim.videoHintsText);
    }

    /// <summary>
    ///     获胜后下一关
    /// </summary>
    public void WinNextLevel()
    {
        UI.winPanel.SetActive(false);
        StartCoroutine("IEHoleBigger");
        UI.StartAddMoney();
        anim.ReSetWinPanel();
    }

    /// <summary>
    ///     转盘后下一关
    /// </summary>
    public void WinSpinNextLevel()
    {
        StartCoroutine("IEHoleBigger");
        anim.ReSetWinPanel();
    }

    /// <summary>
    ///     显示插屏广告
    /// </summary>
    /// <param name="need"></param>
    public void ShowBlock(bool need)
    {
        Hole.instance.StopMove();
        if (need)
            NativeConnect.Connect.showBlock(delegate { isWin = true; });
        else
            isWin = true;
        /*if (NativeConnect.Connect.InterstitialState)
            NativeConnect.Connect.showBlock(delegate { isWin = true; });
        else
            //插屏广告未加载完成
            isWin = true;*/
    }

    /// <summary>
    ///     去广告支付
    /// </summary>
    public void RemoveAdPay()
    {
        NativeConnect.Connect.Pay();
    }
}