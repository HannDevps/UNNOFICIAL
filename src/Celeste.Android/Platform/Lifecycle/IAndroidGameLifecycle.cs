namespace Celeste.Android.Platform.Lifecycle;

public interface IAndroidGameLifecycle
{
    void HandlePause();

    void HandleResume();

    void HandleFocusChanged(bool hasFocus);

    void HandleLowMemory();

    void HandleTrimMemory(int level, string levelName);

    void HandleDestroy();
}
