namespace Transsion.UtilitiesCrowd
{
    internal class Constants
    {
        /*public static readonly string KEY_APPKEY = "appkey";
        public static readonly string KEY_SESSION = "session";
        public static readonly string KEY_PARAM1 = "param1";
        public static readonly string KEY_PARAM2 = "param2";
        public static readonly string KEY_ACTION = "action";
        public static readonly string EVENT_CUST = "game_custom";
        public static readonly int APPID = 3622;*/

        /// <summary>
        /// 打开app
        /// </summary>
        public static readonly string ACTION_APP_START = "app_start";

        /// <summary>
        /// 加载开始
        /// </summary>
        public static readonly string ACTION_LOADING_BEGIN = "loading_begin";

        /// <summary>
        /// 加载结束
        /// </summary>
        public static readonly string ACTION_LOADING_END = "loading_end";

        /// <summary>
        /// 展示登录界面
        /// </summary>
        public static readonly string ACTION_LOGIN_UI = "login_ui";

        /// <summary>
        /// 点击登录 登录方式
        /// </summary>
        public static readonly string ACTION_LOGIN_CLICK = "login_click";

        /// <summary>
        /// 登录结果    0失败,1成功  登录成功后的用户id
        /// </summary>
        public static readonly string ACTION_LOGIN_RESULT = "login_result";

        /// <summary>
        /// 创建角色    角色名称
        /// </summary>
        public static readonly string ACTION_CREATE_ROLE = "create_role";

        /// <summary>
        /// 成功进游
        /// </summary>
        public static readonly string ACTION_GAME_START = "game_start";

        /// <summary>
        /// 开始新手引导
        /// </summary>
        public static readonly string ACTION_TUTORIAL_BEGIN = "tutorial_begin";

        /// <summary>
        /// 完成新手引导
        /// </summary>
        public static readonly string ACTION_TUTORIAL_END = "tutorial_end";

        /// <summary>
        /// 领取新手奖励
        /// </summary>
        public static readonly string ACTION_TUTORIAL_REWARD = "tutorial_reward";

        /// <summary>
        /// 点击各模块
        /// </summary>
        public static readonly string ACTION_MODULE_CHECK = "module_check";

        /// <summary>
        /// 进入子游戏
        /// </summary>
        public static readonly string ACTION_PLAY_MISSION = "play_mission";

        /// <summary>
        /// 关卡开始    关卡数1
        /// </summary>
        public static readonly string ACTION_LEVEL_BEGIN = "level_begin";

        /// <summary>
        /// 关卡结束    关卡数 关卡结果
        /// </summary>
        public static readonly string ACTION_LEVEL_END = "level_end";

        /// <summary>
        /// 关卡奖励    奖励内容
        /// </summary>
        public static readonly string ACTION_LEVEL_REWARD = "level_reward";
    }

    public interface IGameAnalytics
    {
        void Track(params object[] args);
    }

    public class CrowdGameAnalytics
    {
        private static IGameAnalytics _analytics;

        private static IGameAnalytics Analytics
        {
            get
            {
                /*return _analytics ??=
#if TRANSSIONAD
                    new TranssionGameAnalytics();
#endif*/
                if (_analytics == null)
                {
#if TRANSSIONAD
                    _analytics = new TranssionGameAnalytics();
#endif
                }

                return _analytics;
            }
        }

        public static void Track(params object[] args)
        {
            Analytics?.Track(args);
        }

        public static void EventAppStart()
        {
            Track(Constants.ACTION_APP_START);
        }

        public static void EventLevelBegin(int level)
        {
            Track(Constants.ACTION_LEVEL_BEGIN, level.ToString());
        }

        public static void EventLevelEnd(int level, bool isSuccess)
        {
            Track(Constants.ACTION_LEVEL_END, level.ToString(), isSuccess ? "1" : "0");
        }
    }
}