using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AnimManager : Base
{
    public static AnimManager instance;

    public GameObject addDias;
    public Image[] addDiasImage;
    public Image bar;
    private bool bo;
    private Vector3[] diaPathPos;
    public Image gift;
    public Button giftButton1;
    public Button giftButton2;
    private Tweener giftSnakeTweener;
    public Text moneyHintsText;
    public RectTransform setButton;
    public RectTransform skinButton;
    public Image spin;
    public GameObject spinPanel;
    public Text[] spinTexts;
    private int[] spinTimes;
    public Image star;
    private float startY;
    public Button vdieoButton;
    public Text videoHintsText;
    public Image winImage;
    public Text winText1;
    public Text winText2;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        giftButton1.interactable = true;
        giftButton2.interactable = true;
        startY = spinPanel.transform.position.y;
        spinTimes = new[] { 2, 3, 4, 5, 6, 10 };
        addDiasImage = addDias.GetComponentsInChildren<Image>();
        diaPathPos = new[]
        {
            new Vector3(80, 40, 0), new Vector3(-80, 40, 0), new Vector3(80, -40, 0), new Vector3(-80, -40, 0),
            new Vector3(0, -100, 0), new Vector3(0, 100, 0)
        };
        addDias.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) ShowHints(videoHintsText);
    }

    /// <summary>
    ///     死亡抖动
    /// </summary>
    public void DieSnake()
    {
        var a = Camera.main.DOShakeRotation(1f, Vector3.forward * 1.5f);
        // a.onComplete = UI.OpenDiePanel;
        Invoke(nameof(OpenDiePanel), 1f);
        a.SetEase(Ease.InOutCubic);
    }

    private void OpenDiePanel()
    {
        UI.OpenDiePanel();
    }

    /// <summary>
    ///     胜利文字动画
    /// </summary>
    public void WinTextMove()
    {
        Tweener w1 = winText1.transform.DOMove(winText1.transform.position + Vector3.left * 1000, 0.5f).From();
        Tweener w2 = winText2.transform.DOMove(winText2.transform.position + Vector3.right * 1000, 0.5f).From();
        w1.SetEase(Ease.OutExpo);
        w2.SetEase(Ease.OutExpo);
        // w2.onComplete = StarBigger;
        Invoke(nameof(StarBigger), 0.5f);
    }

    /// <summary>
    ///     获胜界面图片旋转
    /// </summary>
    public void RotaImage()
    {
        Tweener a = winImage.transform.DORotate(Vector3.back * 180, 5f);
        a.SetEase(Ease.Linear);
        a.SetLoops(-1, LoopType.Incremental);
    }

    /// <summary>
    ///     获胜界面钻石动画
    /// </summary>
    public void StarBigger()
    {
        star.gameObject.SetActive(true);
        Tweener a = star.transform.DOScale(Vector3.zero, 0.5f).From();
        a.SetEase(Ease.OutBack);
        // a.onComplete = BarMove;
        Invoke(nameof(BarMove), 0.5f);
    }

    /// <summary>
    ///     礼物条出现动画
    /// </summary>
    public void BarMove()
    {
        giftButton1.gameObject.SetActive(true);
        Tweener a = giftButton1.transform.DOScale(Vector3.zero, 0.5f).From();
        a.SetEase(Ease.OutBack);
        // a.OnComplete(delegate { CheckFillAmount(); });
        Invoke(nameof(CheckFillAmount), 0.5f);
    }

    private void CheckFillAmount()
    {
        AddBar();
        if (bar.fillAmount == 1)
        {
            giftButton1.enabled = true;
            giftButton2.enabled = true;
            VideoAndSpinButton(giftButton2.transform);
        }
        else
        {
            VideoAndSpinButton(vdieoButton.transform);
        }
    }

    /// <summary>
    ///     礼物条增加动画
    /// </summary>
    public void AddBar()
    {
        var barNum = bar.fillAmount + SceneData.giftBarAddNum;
        Tweener a = bar.DOFillAmount(barNum, 1f);
        if (barNum >= 1)
            // a.OnComplete(delegate { ShowGiftButtonSnake(); });
            Invoke(nameof(ShowGiftButtonSnake), 1f);
    }

    private void ShowGiftButtonSnake()
    {
        vdieoButton.gameObject.SetActive(false);
        giftButton1.enabled = true;
        giftButton2.gameObject.SetActive(true);
        giftButton2.enabled = true;
        GiftButtonSnake();
    }

    /// <summary>
    ///     礼物按钮抖动动画
    /// </summary>
    public void GiftButtonSnake()
    {
        if (giftSnakeTweener == null || giftSnakeTweener.IsPlaying() == false)
        {
            giftSnakeTweener = gift.transform.DOPunchRotation(Vector3.forward * 5, 1f);
            giftSnakeTweener.SetEase(Ease.InOutCubic);
            giftSnakeTweener.SetLoops(-1, LoopType.Yoyo);
        }
    }

    /// <summary>
    ///     奖励按钮放大缩小动画
    /// </summary>
    public void VideoAndSpinButton(Transform tran)
    {
        tran.gameObject.SetActive(true);
        Tweener a = tran.DOScale(Vector3.one * 1.1f, 0.5f);
        a.SetEase(Ease.Linear);
        a.SetLoops(6, LoopType.Yoyo);
        // a.onComplete = NextLevelButton;
        Invoke(nameof(NextLevelButton), 0.5f);
    }

    /// <summary>
    ///     获胜后继续按钮动画
    /// </summary>
    public void NextLevelButton()
    {
        UI.winButton.gameObject.SetActive(true);
        Tweener wc = UI.winButton.transform.DOScale(Vector3.zero, 1f).From();
        wc.SetEase(Ease.OutExpo);
    }

    /// <summary>
    ///     重置获胜界面
    /// </summary>
    public void ReSetWinPanel()
    {
        star.gameObject.SetActive(false);
        giftButton1.gameObject.SetActive(false);
        giftButton2.gameObject.SetActive(false);
        vdieoButton.gameObject.SetActive(false);
        UI.winButton.gameObject.SetActive(false);
    }

    /// <summary>
    ///     设置和皮肤按钮移出
    /// </summary>
    public void SetAndSkinButtonMove()
    {
        setButton.gameObject.SetActive(false);
        skinButton.gameObject.SetActive(false);
        // setButton.DOMoveX(setButton.position.x - setButton.sizeDelta.x, 0.5f);
        // skinButton.DOMoveX(skinButton.position.x + skinButton.sizeDelta.x, 0.5f);
    }

    /// <summary>
    ///     设置和皮肤按钮进入
    /// </summary>
    public void SetAndSkinButtonCome()
    {
        setButton.gameObject.SetActive(true);
        skinButton.gameObject.SetActive(true);
        // setButton.DOMoveX(setButton.position.x + setButton.sizeDelta.x, 0.5f);
        // skinButton.DOMoveX(skinButton.position.x - skinButton.sizeDelta.x, 0.5f);
    }

    /// <summary>
    ///     打开转盘界面动画
    /// </summary>
    public void OpenSpinPanel()
    {
        for (var i = 0; i < spinTexts.Length; i++) spinTexts[i].text = (gameCon.addMoneyNum * spinTimes[i]).ToString();
        spinPanel.gameObject.SetActive(true);
        Tweener a = spinPanel.transform.DOLocalMoveY(0, 0.5f);
        a.SetEase(Ease.OutBack);
        // a.onComplete = RotSpin;
        Invoke(nameof(RotSpin), 0.5f);
    }

    /// <summary>
    ///     关闭转盘界面动画
    /// </summary>
    public void CloseSpinPanel()
    {
        // UI.winPanel.SetActive(false);
        Tweener a = spinPanel.transform.DOMoveY(startY, 0.5f);
        spinPanel.gameObject.SetActive(false);
        // a.onComplete = gameCon.WinSpinNextLevel;
        bar.fillAmount = 0;
        vdieoButton.gameObject.SetActive(true);
        giftButton2.gameObject.SetActive(false);
        giftSnakeTweener.Pause();
    }

    /// <summary>
    ///     转盘旋转
    /// </summary>
    public void RotSpin()
    {
        var num = Random.Range(0, 6);
        Tweener a = spin.transform.DORotate(Vector3.back * (1080 + 60 * num), 3f);
        gameCon.addMoneyNum *= spinTimes[num];
        a.SetEase(Ease.OutCirc);
        a.OnComplete(delegate
        {
            SpinAddDiamond();
            Tweener spin = spinTexts[num].transform.DOScale(Vector3.one * 2f, 0.5f);
            spin.SetEase(Ease.Linear);
            spin.SetLoops(2, LoopType.Yoyo);
        });
    }

    /// <summary>
    ///     转盘增加钻石动画
    /// </summary>
    public void SpinAddDiamond()
    {
        addDias.SetActive(true);

        for (var i = 0; i < addDiasImage.Length; i++)
        {
            Tweener a = addDiasImage[i].transform.DOLocalPath(new[] { diaPathPos[i], new Vector3(450, 530, 0) }, 1.5f);
            a.SetEase(Ease.InQuart);
            if (i == addDiasImage.Length - 1)
                a.OnComplete(delegate
                {
                    addDias.SetActive(false);
                    UI.StartAddMoney();
                    CloseSpinPanel();
                });
        }
    }

    /// <summary>
    ///     转盘显示激励视频
    /// </summary>
    public void SpinShowVideo()
    {
        giftButton1.enabled = false;
        giftButton2.enabled = false;
        if (NativeConnect.Connect.VideoState)
        {
            Debug.Log($"SPIN:{DataController.sceneNum % 3 == 2}");
            NativeConnect.Connect.showVideo("Rvdoublediamond", delegate(string str)
            {
                if (str.Equals("True"))
                {
                    Invoke("OpenSpinPanel", 1f);
                }
                else if (str.Equals("False"))
                {
                    ShowHints(videoHintsText);
                    giftButton1.enabled = true;
                    giftButton2.enabled = true;
                }
                else if (str.Equals("Close"))
                {
                    giftButton1.enabled = true;
                    giftButton2.enabled = true;
                }
            });
        }
    }

    /// <summary>
    ///     显示广告失败动画
    /// </summary>
    public void ShowHints(Text text)
    {
        if (bo) return;
        bo = true;
        text.enabled = true;
        Tweener a = text.transform.DOScale(Vector3.zero, 3f);
        a.SetEase(Ease.InExpo);
        a.OnComplete(delegate
        {
            text.enabled = false;
            text.transform.localScale = Vector3.one;
            bo = false;
        });
    }
}