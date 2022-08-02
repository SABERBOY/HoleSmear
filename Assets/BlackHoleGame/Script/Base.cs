using UnityEngine;

namespace BlackHoleGame.Script
{
    public class Base : MonoBehaviour
    {
        private void Start()
        {
        }

        /// <summary>
        ///     碰撞器
        /// </summary>
        private Collider _col;

        /// <summary>
        ///     模型
        /// </summary>
        private MeshFilter _mf;

        /// <summary>
        ///     渲染器
        /// </summary>
        private MeshRenderer _mr;

        /// <summary>
        ///     刚体
        /// </summary>
        private Rigidbody _rig;

        /// <summary>
        ///     坐标
        /// </summary>
        internal Vector3 pos
        {
            get => transform.position;
            set => transform.position = value;
        }

        /// <summary>
        ///     尺寸
        /// </summary>
        internal Vector3 scale
        {
            get => transform.localScale;
            set => transform.localScale = value;
        }

        /// <summary>
        ///     旋转角度
        /// </summary>
        internal Vector3 eul
        {
            get => transform.localEulerAngles;
            set => transform.localEulerAngles = value;
        }

        internal Collider col
        {
            get
            {
                if (_col == null) _col = GetComponent<Collider>();
                return _col;
            }
        }

        internal MeshRenderer mr
        {
            get
            {
                if (_mr == null) _mr = GetComponent<MeshRenderer>();
                return _mr;
            }
        }

        internal Rigidbody rig
        {
            get
            {
                if (_rig == null) _rig = GetComponent<Rigidbody>();
                return _rig;
            }
        }

        internal MeshFilter mf => GetComponent<MeshFilter>();

        /// <summary>
        ///     动画管理单例
        /// </summary>
        internal AnimManager anim => AnimManager.instance;

        /// <summary>
        ///     游戏管理单例
        /// </summary>
        internal GameController gameCon => GameController.instance;

        /// <summary>
        ///     UI管理单例
        /// </summary>
        internal UIController UI => UIController.instance;

        /// <summary>
        ///     语言管理单例
        /// </summary>
        internal LanguageManager lang => LanguageManager.instance;
    }
}