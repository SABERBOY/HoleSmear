using System.Collections.Generic;
using UnityEngine;
using static BlackHoleGame.Script.HoleSkinLoadManager;

namespace BlackHoleGame.Script
{
    public class FXSelectManager : MMPersistentSingleton<FXSelectManager>
    {
        [SerializeField] private Transform fxParent;
        private Dictionary<string, GameObject> _skinDict = new Dictionary<string, GameObject>();

        public void SpawnFXWithIndex(string index)
        {
            if (_skinDict.ContainsKey(index))
            {
                this._skinDict[index].SetActive(true);
                return;
            }

            if (!GetSkin(index, out var skin)) return;
            var picSkin = Instantiate(skin, fxParent, false);
            picSkin.transform.localPosition = Vector3.zero;
            this._skinDict.Add(index, picSkin);
            picSkin.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        public void HideAllFX()
        {
            foreach (var skin in _skinDict)
            {
                skin.Value.SetActive(false);
            }
        }
    }
}