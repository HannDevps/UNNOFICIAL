using System;
using Android.App;
using Android.OS;
using Android.Views;
using Celeste.Core.Platform.Logging;

namespace Celeste.Android.Platform.Fullscreen;

public sealed class ImmersiveFullscreenController
{
    private readonly IAppLogger _logger;

    public ImmersiveFullscreenController(IAppLogger logger)
    {
        _logger = logger;
    }

    public void Apply(Activity activity, string reason)
    {
        try
        {
            var window = activity.Window;
            if (window is null)
            {
                _logger.Log(LogLevel.Warn, "FULLSCREEN", "Window unavailable while applying immersive mode", context: reason);
                return;
            }

            var decorView = window.DecorView;
            if (decorView is null)
            {
                _logger.Log(LogLevel.Warn, "FULLSCREEN", "DecorView unavailable while applying immersive mode", context: reason);
                return;
            }

            if (Build.VERSION.SdkInt >= BuildVersionCodes.R)
            {
                window.SetDecorFitsSystemWindows(false);
                var controller = decorView.WindowInsetsController;
                if (controller is not null)
                {
                    controller.Hide(WindowInsets.Type.StatusBars() | WindowInsets.Type.NavigationBars());
                    controller.SystemBarsBehavior = (int)WindowInsetsControllerBehavior.ShowTransientBarsBySwipe;
                }
                else
                {
                    _logger.Log(LogLevel.Warn, "FULLSCREEN", "WindowInsetsController unavailable", context: reason);
                    return;
                }
            }
            else
            {
                var flags = SystemUiFlags.LayoutStable |
                            SystemUiFlags.LayoutHideNavigation |
                            SystemUiFlags.LayoutFullscreen |
                            SystemUiFlags.HideNavigation |
                            SystemUiFlags.Fullscreen |
                            SystemUiFlags.ImmersiveSticky;

                decorView.SystemUiVisibility = (StatusBarVisibility)flags;
            }

            _logger.Log(LogLevel.Info, "FULLSCREEN", "Immersive fullscreen applied", context: reason);
        }
        catch (Exception exception)
        {
            _logger.Log(LogLevel.Error, "FULLSCREEN", "Failed to apply immersive fullscreen", exception, reason);
        }
    }
}
