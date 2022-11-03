using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace BlackHoleGame.Script
{
    public class HoleSkinLoadManager
    {
        private static int _skinLength;
        private const string SKIN_NAME = "skin";
        private const string SKIN_UI_NAME = "skin";

        public static int SkinLength
        {
            get => ObjectsList.Count;
            private set => _skinLength = value;
        }

        public static AsyncOperationHandle<IList<IResourceLocation>> GetResourcesLength<T>(string assetLabel)
            where T : Object
        {
            return Addressables.LoadResourceLocationsAsync(assetLabel, typeof(T));
        }

        public static async Task InitSprites<T>(string assetLabel) where T : Object

        {
            var locations = await Addressables.LoadResourceLocationsAsync(assetLabel, typeof(T)).Task;
            List<Task<T>> tasks = new List<Task<T>>();

            foreach (var location in locations)
            {
                tasks.Add(Addressables.LoadAssetAsync<T>(location).Task);
            }

            var loadedSprites = await Task.WhenAll(tasks);

            foreach (var sprite in loadedSprites)
            {
                // AllSprites.Add(sprite.name, sprite);
                // Debug.Log(sprite);
            }
        }

        public static IEnumerator PreLoadSkin<T>(IList<string> keys,
            Action<Dictionary<string, AsyncOperationHandle<T>>> ready) where T : Object
        {
            var locations = Addressables.LoadResourceLocationsAsync(keys,
                Addressables.MergeMode.Union, typeof(T));
            yield return locations;
            SkinLength = locations.Result.Count;
            Dictionary<string, AsyncOperationHandle<T>> oList = new Dictionary<string, AsyncOperationHandle<T>>();
            var loadOps = new List<AsyncOperationHandle>(locations.Result.Count);
            foreach (IResourceLocation location in locations.Result)
            {
                AsyncOperationHandle<T> handle =
                    Addressables.LoadAssetAsync<T>(location);
                handle.Completed += obj => { oList.Add(location.PrimaryKey, obj); };
                loadOps.Add(handle);
            }

            yield return Addressables.ResourceManager.CreateGenericGroupOperation(loadOps, true);
            Addressables.Release(locations);
            /*foreach (var op in loadOps)
            {
                Addressables.Release(op);
            }*/
            yield return PreLoadSkinUI<Sprite>("skinUI", (handles =>
            {
                foreach (var o in handles)
                {
                    SkinUIList.Add(o.Key, o.Value.Result);
                }
            }));
            ready?.Invoke(oList);
        }

        public static IEnumerator PreLoadSkinUI
            <T>(string assetLabel, Action<Dictionary<string, AsyncOperationHandle<T>>> ready)
            where T : Object
        {
            var locations = Addressables.LoadResourceLocationsAsync(assetLabel, typeof(T));
            yield return locations;
            // SkinLength = locations.Result.Count;
            Dictionary<string, AsyncOperationHandle<T>> oList = new Dictionary<string, AsyncOperationHandle<T>>();
            var loadOps = new List<AsyncOperationHandle>(locations.Result.Count);
            foreach (IResourceLocation location in locations.Result)
            {
                AsyncOperationHandle<T> handle =
                    Addressables.LoadAssetAsync<T>(location);
                handle.Completed += obj => { oList.Add(location.PrimaryKey, obj); };
                loadOps.Add(handle);
            }

            yield return Addressables.ResourceManager.CreateGenericGroupOperation(loadOps, true);
            Addressables.Release(locations);
            /*foreach (var op in loadOps)
            {
                Addressables.Release(op);
            }*/
            ready?.Invoke(oList);
        }

        private static readonly Dictionary<string, GameObject> ObjectsList =
            new Dictionary<string, GameObject>();

        private static readonly Dictionary<string, Sprite> SkinUIList =
            new Dictionary<string, Sprite>();

        public static bool GetSkin(string index, out GameObject skin)
        {
            skin = null;
            if (ObjectsList.TryGetValue(index, out skin))
            {
                return true;
            }

            return false;
        }

        public static string GetRandomSkin()
        {
            List<string> objectList = ObjectsList.Keys.ToList();
            return Random.Range(0, objectList.Count) < objectList.Count
                ? objectList[Random.Range(0, objectList.Count)]
                : objectList[0];
        }

        public static bool GetSkinUI(int index, out Sprite skin)
        {
            skin = null;
            string uiName = $"{SKIN_UI_NAME}{index}";
            if (SkinUIList.ContainsKey(uiName))
            {
                skin = SkinUIList[uiName];
                return true;
            }

            return false;
        }

        public static void AddSkin(Dictionary<string, AsyncOperationHandle<GameObject>> oList)
        {
            foreach (var o in oList)
            {
                ObjectsList[o.Key] = o.Value.Result;
            }

            Debug.Log(ObjectsList.Count);
        }
    }
}