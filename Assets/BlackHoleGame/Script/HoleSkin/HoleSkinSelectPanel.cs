using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace BlackHoleGame.Script
{
    public class HoleSkinSelectPanel : MonoBehaviour
    {
        [FormerlySerializedAs("_skinRawImage")] [SerializeField]
        private RawImage skinRawImage;

        [FormerlySerializedAs("_selectButton")] [SerializeField]
        private Button selectButton;

        [FormerlySerializedAs("_leftButton")] [SerializeField]
        private Button leftButton;

        [FormerlySerializedAs("_rightButton")] [SerializeField]
        private Button rightButton;

        [SerializeField] private Transform noThanksButton;
        private int randomSkinIndex = -1;

        private void Start()
        {
            this.leftButton.gameObject.SetActive(false);
            this.rightButton.gameObject.SetActive(false);
            this.selectButton.onClick.RemoveAllListeners();
            this.leftButton.onClick.RemoveAllListeners();
            this.rightButton.onClick.RemoveAllListeners();
            this.noThanksButton.GetComponent<Button>().onClick.RemoveAllListeners();

            this.selectButton.onClick.AddListener(this.OnSelectButtonClick);
            this.leftButton.onClick.AddListener(this.OnLeftButtonClick);
            this.rightButton.onClick.AddListener(this.OnRightButtonClick);
            this.noThanksButton.GetComponent<Button>().onClick.AddListener(this.OnNoThanksButtonClick);
        }

        private void OnNoThanksButtonClick()
        {
            this.OnCloseButtonClick();
        }

        private void OnRightButtonClick()
        {
            Debug.Log("OnRightButtonClick");
        }

        private void OnLeftButtonClick()
        {
            Debug.Log("OnLeftButtonClick");
        }

        private void OnSelectButtonClick()
        {
            Debug.Log("OnSelectButtonClick");
            NativeConnect.Connect.ShowVideo((() =>
                {
                    Debug.Log("OnRightButtonClick ShowVideo success");
                    Hole.instance.SetSkin(this.randomSkinIndex);
                    this.OnCloseButtonClick();
                }),
                () => { Debug.Log("OnRightButtonClick ShowVideo error"); });
        }

        public void Show()
        {
            this.gameObject.SetActive(true);
            this.randomSkinIndex = Random.Range(0, HoleSkinLoadManager.SkinLength);
            FXSelectManager.Instance.SpawnFXWithIndex(this.randomSkinIndex);
            Invoke(nameof(ShowNoThanksButton), 3f);
        }

        private void ShowNoThanksButton()
        {
            this.noThanksButton.gameObject.SetActive(true);
        }

        private void OnCloseButtonClick()
        {
            this.gameObject.SetActive(false);
            this.noThanksButton.gameObject.SetActive(false);
            FXSelectManager.Instance.HideAllFX();
        }
    }

    public class HoleSkinSelectModel
    {
        private static Dictionary<string, GameObject> _skinDict = new Dictionary<string, GameObject>();
        private static readonly string path = "HoleSkinSelectPanel";

        public static void Show()
        {
            if (_skinDict.ContainsKey(path))
            {
                _skinDict[path].GetComponent<HoleSkinSelectPanel>().Show();
                _skinDict[path].SetActive(true);
                return;
            }

            var panelPrefab = Addressables
                .LoadAssetAsync<GameObject>(path).WaitForCompletion();
            var panelGameObject = GameObject.Instantiate(panelPrefab, UIController.instance.transform, false);
            _skinDict.Add(path, panelGameObject);
            panelGameObject.transform.SetAsLastSibling();
            panelGameObject.GetComponent<HoleSkinSelectPanel>().Show();
            panelGameObject.SetActive(true);
            // Addressables.Release(panelGameObject);
        }
    }
}