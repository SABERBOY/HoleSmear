using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace BlackHoleGame.Script
{
    public class HoleSkinSelectPanel : MonoBehaviour
    {
        [FormerlySerializedAs("_skinRawImageTransform")] [SerializeField]
        private Transform skinRawImageTransform;

        [SerializeField] private RawImage skinRawImage;

        [FormerlySerializedAs("_selectButton")] [SerializeField]
        private Button selectButton;

        [FormerlySerializedAs("_leftButton")] [SerializeField]
        private Button leftButton;

        [FormerlySerializedAs("_rightButton")] [SerializeField]
        private Button rightButton;

        [SerializeField] private Sprite[] skins;

        [SerializeField] private Transform noThanksButton;
        private string randomSkinIndex = string.Empty;

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
            // this.skinRawImageTransform.DOLocalRotate(new Vector3(0, 0, -360), 1f, RotateMode.FastBeyond360)
            //     .SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
        }

        private void OnNoThanksButtonClick()
        {
            this.OnCloseButtonClick();
        }

        private void OnRightButtonClick()
        {
            // Debug.Log("OnRightButtonClick");
        }

        private void OnLeftButtonClick()
        {
            // Debug.Log("OnLeftButtonClick");
        }

        private void OnSelectButtonClick()
        {
            // Debug.Log("OnSelectButtonClick");
            NativeConnect.Connect.ShowVideo((() =>
                {
                    // Debug.Log("OnRightButtonClick ShowVideo success");
                    Hole.instance.SetSkin(this.randomSkinIndex);
                    this.OnCloseButtonClick();
                }),
                () =>
                {
                    // Debug.Log("OnRightButtonClick ShowVideo error");
                });
        }

        public void Show()
        {
            this.noThanksButton.gameObject.SetActive(false);
            this.gameObject.SetActive(true);
            this.randomSkinIndex =HoleSkinLoadManager.GetRandomSkin();
            FXSelectManager.Instance.SpawnFXWithIndex(this.randomSkinIndex);
            Invoke(nameof(ShowNoThanksButton), 3f);
        }

        private void ShowNoThanksButton()
        {
            this.noThanksButton.gameObject.SetActive(true);
        }

        private void OnCloseButtonClick()
        {
            // NativeConnect.Connect.ShowFloatingWindow(false);
            this.gameObject.SetActive(false);
            this.noThanksButton.gameObject.SetActive(false);
            FXSelectManager.Instance.HideAllFX();
        }
    }

    public class HoleSkinSelectModel
    {
        private static Dictionary<string, GameObject> _skinDict = new Dictionary<string, GameObject>();

        // private static readonly string path = "HoleSkinSelectPanel";
        /*public string address;
        AsyncOperationHandle<GameObject> opHandle;

        public IEnumerator Start()
        {
            opHandle = Addressables.LoadAssetAsync<GameObject>(address);

            // yielding when already done still waits until the next frame
            // so don't yield if done.
            if (!opHandle.IsDone)
                yield return opHandle;

            if (opHandle.Status == AsyncOperationStatus.Succeeded)
            {
                Instantiate(opHandle.Result, transform);
            }
            else
            {
                Addressables.Release(opHandle);
            }
        }

        void OnDestroy()
        {
            Addressables.Release(opHandle);
        }*/

        public static IEnumerator Show()
        {
            // NativeConnect.Connect.ShowFloatingWindow(true);
            var path = AssetReferenceManager.Instance.HoleSkinSelectPanel.AssetGUID;
            if (_skinDict.ContainsKey(path))
            {
                _skinDict[path].GetComponent<HoleSkinSelectPanel>().Show();
                _skinDict[path].SetActive(true);
                yield break;
            }

            var panelPrefabHandle =
                Addressables.LoadAssetAsync<GameObject>(AssetReferenceManager.Instance.HoleSkinSelectPanel);

            if (!panelPrefabHandle.IsDone)
                yield return panelPrefabHandle;

            if (panelPrefabHandle.Status == AsyncOperationStatus.Succeeded)
            {
                var panelGameObject =
                    GameObject.Instantiate(panelPrefabHandle.Result, UIController.instance.transform, false);
                _skinDict.Add(path, panelGameObject);
                panelGameObject.transform.SetAsLastSibling();
                panelGameObject.GetComponent<HoleSkinSelectPanel>().Show();
                panelGameObject.SetActive(true);
            }
            else
            {
                Addressables.Release(panelPrefabHandle);
            }


            // Addressables.Release(panelGameObject);
        }
    }
}