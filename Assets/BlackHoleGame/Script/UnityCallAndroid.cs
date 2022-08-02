namespace BlackHoleGame.Script
{
    public class UnityCallAndroid
    {
        public static T CallStaticFunction<T>(string javaClassName, string methodName, params object[] args)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
		using (AndroidJavaClass ajc = new AndroidJavaClass(javaClassName))
		{
			return ajc.CallStatic<T>(methodName, args);
		}
#else
            return default;
#endif
        }

        public static void CallStaticFunction(string javaClassName, string methodName, params object[] args)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
		using (AndroidJavaClass ajc = new AndroidJavaClass(javaClassName))
		{
			ajc.CallStatic(methodName, args);
		}
#endif
        }

        public static T GetStaticVariable<T>(string javaClassName, string methodName)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
		using (AndroidJavaClass ajc = new AndroidJavaClass(javaClassName))
		{
			return ajc.GetStatic<T> (methodName);
		}
#else
            return default;
#endif
        }
    }
}