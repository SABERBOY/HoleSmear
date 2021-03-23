using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : Base
{
    public static GameController instance;
    private bool _isDie;
    private bool _isWin;
    public int addMoneyNum;
    public ParticleSystem caizi;
    public GameObject[] gos;
    private Transform[] gosPos;
    public GameObject holeGo;
    public GameObject[] maps;
    public int skinMoneyNum;
    public int winMoneyNum;

    public bool isDie
    {
        get => _isDie;
        set
        {
            if (value)
            {
                Hole.instance.StopMove();
                anim.DieSnake();
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
                        Invoke("WinLiZiPanel", 1f);
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
        UI.winPanel.gameObject.SetActive(true);
        UI.starTextNum = addMoneyNum;
        anim.WinTextMove();
        UI.lvPanel.SetActive(false);
        caizi.gameObject.SetActive(true);
        caizi.Play();
        Diamond.CreateDia(addMoneyNum);
    }

    public void Win()
    {
        if (Shape.shapeList.Count == 0 && isWin == false && isDie == false)
        {
            if (DataController.sceneNum % 3 == 2 && DataController.bigLevel > 2 &&
                (DataController.bigLevel + 1) % 3 != 0)
                ShowBlock();
            else
                isWin = true;
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
        StopCoroutine("IEHoleBigger");
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
                    UI.startPanel.SetActive(true);
                    Hole.instance.StopMove();
                    anim.SetAndSkinButtonCome();
                    Hole.instance.enabled = false;
                }
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
        UI.moneyTextNum = PlayerPrefs.GetInt(SceneData.money);
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
        for (var i = 0; i < games.Length; i++)
        {
            var ob = Instantiate(games[i]);
            ob.transform.SetParent(Hole.instance.transform.parent, false);
        }

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
    }

    /// <summary>
    ///     死亡后继续游戏
    /// </summary>
    public void DieContinueGame()
    {
        isDie = false;
        Hole.instance.ResetPos();
        UI.StopCountDown();
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
    public void ShowBlock()
    {
        Hole.instance.StopMove();
        if (NativeConnect.Connect.InterstitialState)
            NativeConnect.Connect.showBlock(delegate { isWin = true; });
        else
            //插屏广告未加载完成
            isWin = true;
    }

    /// <summary>
    ///     去广告支付
    /// </summary>
    public void RemoveAdPay()
    {
        NativeConnect.Connect.Pay();
    }
}