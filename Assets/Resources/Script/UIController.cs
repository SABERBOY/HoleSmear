using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : Base
{
    public static UIController instance;
    private char[] crr;
    public GameObject diePanel;
    public Image dieTimeImage;
    public float dieTimeNum = 10;
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
    public Text moneyText;
    public Button payButton;
    public GameObject setPanel;
    public GameObject shock;
    public Toggle shockSwitch;
    public Text skinMoney;
    public GameObject skinPanel;
    public Text starText;
    public GameObject startPanel;
    public GameObject tick;
    public Button winButton;
    public GameObject winPanel;

    public int moneyTextNum
    {
        get => int.Parse(moneyText.text);
        set => moneyText.text = value.ToString();
    }

    public int skinNum
    {
        get => int.Parse(skinMoney.text);
        set => skinMoney.text = value.ToString();
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
        StartCoroutine("IEFPS");
        anim.RotaImage();
        crr = new[] {'0', '0', '0', '0', '0', '0'};
    }

    private void Update()
    {
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
    }

    private IEnumerator IEFPS()
    {
        while (true)
        {
            FPSText.text = (1 / Time.deltaTime).ToString();
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
        PlayerPrefs.SetInt(SceneData.money, moneyTextNum);
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
        PlayerPrefs.SetInt(SceneData.money, moneyTextNum);
    }

    /// <summary>
    ///     开始钻石增加协程
    /// </summary>
    public void StartAddMoney()
    {
        StartCoroutine("IEAddMoney");
        for (var i = 0; i < anim.addDiasImage.Length; i++) anim.addDiasImage[i].transform.localPosition = Vector3.zero;
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
            dieTimeImage.fillAmount = dieTimeNum / 10;
            dieTimeText.text = ((int) (dieTimeNum + 1)).ToString();
            yield return new WaitForSeconds(0.02f);
        }
    }

    /// <summary>
    ///     停止倒计时协程
    /// </summary>
    public void StopCountDown()
    {
        StopCoroutine("IECountDown");
    }

    /// <summary>
    ///     打开死亡界面
    /// </summary>
    public void OpenDiePanel()
    {
        diePanel.gameObject.SetActive(true);
        lvPanel.SetActive(false);
        StartCoroutine("IECountDown");
    }

    /// <summary>
    ///     打开设置界面
    /// </summary>
    public void OpenSetPanel()
    {
        NativeConnect.Connect.showBlock(s => { });
        Hole.instance.enabled = false;
        setPanel.SetActive(true);
    }

    /// <summary>
    ///     关闭设置界面
    /// </summary>
    public void CloseSetPanel()
    {
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
        NativeConnect.Connect.showBlock(s => { });
        skinMoney.text = moneyText.text;
        Hole.instance.enabled = false;
        skinPanel.SetActive(true);
        var go = GameObject.Find("S" + SceneData.skinID);
        var str = PlayerPrefs.GetString(SceneData.skinState);
        if (!string.IsNullOrEmpty(str))
        {
            crr = str.ToCharArray();
            for (var i = 1; i < crr.Length; i++)
                if (crr[i] == '1')
                {
                    var button = GameObject.Find("S" + i);
                    var im = button.GetComponentsInChildren<Image>();
                    for (var j = 0; j < im.Length; j++)
                        if (im[j].name.Contains("Image"))
                            im[j].gameObject.SetActive(false);
                    button.GetComponent<Image>().color = Color.white;
                }
        }

        var pos = tick.transform.localPosition;
        tick.transform.parent = go.transform;
        tick.transform.localPosition = pos;
    }

    /// <summary>
    ///     关闭皮肤界面
    /// </summary>
    public void CloseSkinPanel()
    {
        skinPanel.SetActive(false);
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
            gameCon.skinMoneyNum = num * 500;
            if (moneyTextNum < gameCon.skinMoneyNum)
            {
                anim.ShowHints(anim.moneyHintsText);
                return;
            }

            var im = go.GetComponentsInChildren<Image>();
            im[1].gameObject.SetActive(false);
            image.color = Color.white;
            StartCoroutine("IESpendMoney");
            crr[num] = '1';
            var str = new string(crr);
            PlayerPrefs.SetString(SceneData.skinState, str);
        }

        ChangeSkin(num);
        var pos = tick.transform.localPosition;
        tick.transform.parent = go;
        tick.transform.localPosition = pos;
    }

    /// <summary>
    ///     改变皮肤
    /// </summary>
    public void ChangeSkin(int a)
    {
        SceneData.skinID = a;
        PlayerPrefs.SetInt(SceneData.skin, SceneData.skinID);
        var go = Hole.instance.transform.parent.gameObject;
        Hole.instance.transform.parent = null;
        go.SetActive(false);
        var map = gameCon.maps[a];
        map.SetActive(true);
        Hole.instance.transform.parent = map.transform;
    }
}