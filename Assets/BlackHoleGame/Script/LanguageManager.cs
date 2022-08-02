using System;
using UnityEngine;
using UnityEngine.UI;

namespace BlackHoleGame.Script
{
    public class LanguageManager : MonoBehaviour
    {
        public static LanguageManager instance;

        private int _flagNum;
        public Text addHints;
        public Text dieCancelText;
        public Text dieContinueText;
        public Text dieText1;
        public Text dieText2;
        public Text languageText;
        public Text moneyHints;
        public Text musicText;
        public Text setText;
        public Text shockOffText;
        public Text shockOnText;
        public Text shockText;
        public Text spinBarText;
        public Text spinTitle;
        public Text startText;
        public Text winCancelText;
        public Text winText1;
        public Text winText2;

        public int flagNum
        {
            get => _flagNum;
            set
            {
                switch (value)
                {
                    case 0:
                        setText.text = GetLang((int)LanguageType.设置).EN;
                        shockText.text = GetLang((int)LanguageType.振动).EN;
                        shockOnText.text = GetLang((int)LanguageType.振动开).EN;
                        shockOffText.text = GetLang((int)LanguageType.振动关).EN;
                        languageText.text = GetLang((int)LanguageType.语言).EN;
                        startText.text = GetLang((int)LanguageType.开始).EN;
                        winText1.text = GetLang((int)LanguageType.特殊关卡).EN;
                        winText2.text = GetLang((int)LanguageType.特殊完成).EN;
                        winCancelText.text = GetLang((int)LanguageType.跳过).EN;
                        dieText1.text = GetLang((int)LanguageType.死亡输了).EN;
                        dieText2.text = GetLang((int)LanguageType.死亡是否继续).EN;
                        dieContinueText.text = GetLang((int)LanguageType.死亡继续).EN;
                        dieCancelText.text = GetLang((int)LanguageType.跳过).EN;
                        spinTitle.text = GetLang((int)LanguageType.转盘标题).EN;
                        spinBarText.text = GetLang((int)LanguageType.转盘进度条).EN;
                        moneyHints.text = GetLang((int)LanguageType.金币不足).EN;
                        addHints.text = GetLang((int)LanguageType.广告失败).EN;
                        break;
                    case 1:
                        setText.text = GetLang((int)LanguageType.设置).CN;
                        shockText.text = GetLang((int)LanguageType.振动).CN;
                        shockOnText.text = GetLang((int)LanguageType.振动开).CN;
                        shockOffText.text = GetLang((int)LanguageType.振动关).CN;
                        languageText.text = GetLang((int)LanguageType.语言).CN;
                        startText.text = GetLang((int)LanguageType.开始).CN;
                        winText1.text = GetLang((int)LanguageType.特殊关卡).CN;
                        winText2.text = GetLang((int)LanguageType.特殊完成).CN;
                        winCancelText.text = GetLang((int)LanguageType.跳过).CN;
                        dieText1.text = GetLang((int)LanguageType.死亡输了).CN;
                        dieText2.text = GetLang((int)LanguageType.死亡是否继续).CN;
                        dieContinueText.text = GetLang((int)LanguageType.死亡继续).CN;
                        dieCancelText.text = GetLang((int)LanguageType.跳过).CN;
                        spinTitle.text = GetLang((int)LanguageType.转盘标题).CN;
                        spinBarText.text = GetLang((int)LanguageType.转盘进度条).CN;
                        moneyHints.text = GetLang((int)LanguageType.金币不足).CN;
                        addHints.text = GetLang((int)LanguageType.广告失败).CN;
                        break;
                }

                _flagNum = value;
            }
        }

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
        }

        private LanguageData GetLang(int num)
        {
            return DataController.languageData.dataArray[num];
        }
    }

    public enum LanguageType
    {
        设置,
        皮肤,
        去广告,
        开始,
        振动,
        音效,
        振动关,
        振动开,
        语言,
        金币不足,
        皮肤主题,
        皮肤金币,
        皮肤返回,
        死亡是否继续,
        死亡输了,
        死亡继续,
        特殊关卡,
        特殊奖励,
        特殊完成,
        特殊礼物,
        特殊获取,
        转盘标题,
        转盘获取,
        转盘进度条,
        跳过,
        转盘恭喜,
        转盘确定,
        广告失败
    }
}