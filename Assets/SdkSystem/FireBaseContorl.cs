using Firebase.Analytics;
using System;
using UnityEngine;


namespace SDK
{
    /// <summary>
    /// FireBase管理
    /// </summary>
    public class FireBaseContorl
    {

        public void Initialize()
        {
            Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                var dependencyStatus = task.Result;

                if (dependencyStatus == Firebase.DependencyStatus.Available)
                {
                    Firebase.FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;
                    FireBaseInit();
                }
                else
                {

                    UnityEngine.Debug.LogError(System.String.Format(
                        "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                    // Firebase Unity SDK is not safe to use here.

                }
            });
        }


        /// <summary>
        /// 处理必要的firebase模块的初始化
        /// </summary>
        private void FireBaseInit()
        {

            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);

            // 设置用户的注册方法
            FirebaseAnalytics.SetUserProperty(
                FirebaseAnalytics.UserPropertySignUpMethod,
                "Google");

            // 设置用户ID
            string USER_ID = SystemInfo.deviceUniqueIdentifier;
            FirebaseAnalytics.SetUserId(USER_ID);
            // Set default session duration values.

            //FirebaseAnalytics.SetMinimumSessionDuration(new TimeSpan(0, 0, 10));
            FirebaseAnalytics.SetSessionTimeoutDuration(new TimeSpan(0, 30, 0));

            AnalyticsLogin();

        }

        /// <summary>
        /// 登陆
        /// </summary>
        private void AnalyticsLogin()
        {
            // 记录一个没有参数的事件
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLogin);
        }

        /// <summary>
        /// 统计打点
        /// </summary>
        /// <param name="category">第一级：分类</param>
        /// <param name="action">第二级：动作</param>
        /// <param name="label">第三级：值</param>
        //public void PostEvent(ENUM_Category category, ENUM_Action action, string label)
        //{
        //    FirebaseAnalytics.LogEvent(category.ToString(), action.ToString(), label);
        //}

    }
}





