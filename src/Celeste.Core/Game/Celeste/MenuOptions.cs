using System;
using System.Globalization;
using Celeste.Core.Platform.Interop;
using FMOD.Studio;
using Monocle;

namespace Celeste;

public static class MenuOptions
{
	private static TextMenu menu;

	private static bool inGame;

	private static TextMenu.Item crouchDashMode;

	private static TextMenu.Item window;

	private static TextMenu.Item viewport;

	private static EventInstance snapshot;

	private static readonly string[] RuntimeTargetFpsValues = new string[5] { "30", "60", "90", "120", "Uncapped" };

	private static readonly float[] RuntimeOverlayFontScaleValues = new float[8] { 0.5f, 0.75f, 1f, 1.25f, 1.5f, 1.75f, 2f, 2.5f };

	private static readonly int[] RuntimeOverlayUpdateMsValues = new int[6] { 100, 250, 500, 1000, 2000, 5000 };

	private static readonly int[] RuntimeOverlayPaddingValues = new int[7] { 0, 4, 8, 12, 16, 24, 32 };

	public static TextMenu Create(bool inGame = false, EventInstance snapshot = null)
	{
		MenuOptions.inGame = inGame;
		MenuOptions.snapshot = snapshot;
		menu = new TextMenu();
		menu.Add(new TextMenu.Header(Dialog.Clean("options_title")));
		menu.OnClose = delegate
		{
			crouchDashMode = null;
		};
		if (!inGame && Dialog.Languages.Count > 1)
		{
			menu.Add(new TextMenu.SubHeader(""));
			TextMenu.LanguageButton languageButton = new TextMenu.LanguageButton(Dialog.Clean("options_language"), Dialog.Language);
			languageButton.Pressed(SelectLanguage);
			menu.Add(languageButton);
		}
		menu.Add(new TextMenu.SubHeader(Dialog.Clean("options_controls")));
		CreateRumble(menu);
		CreateGrabMode(menu);
		crouchDashMode = CreateCrouchDashMode(menu);
		menu.Add(new TextMenu.Button(Dialog.Clean("options_keyconfig")).Pressed(OpenKeyboardConfig));
		menu.Add(new TextMenu.Button(Dialog.Clean("options_btnconfig")).Pressed(OpenButtonConfig));
		menu.Add(new TextMenu.SubHeader(Dialog.Clean("options_video")));
		menu.Add(new TextMenu.OnOff(Dialog.Clean("options_fullscreen"), Settings.Instance.Fullscreen).Change(SetFullscreen));
		menu.Add(window = new TextMenu.Slider(Dialog.Clean("options_window"), (int i) => i + "x", 3, 10, Settings.Instance.WindowScale).Change(SetWindow));
		menu.Add(new TextMenu.OnOff(Dialog.Clean("options_vsync"), Settings.Instance.VSync).Change(SetVSync));
		menu.Add(new TextMenu.OnOff(Dialog.Clean("OPTIONS_DISABLE_FLASH"), Settings.Instance.DisableFlashes).Change(delegate(bool b)
		{
			Settings.Instance.DisableFlashes = b;
		}));
		menu.Add(new TextMenu.Slider(Dialog.Clean("OPTIONS_DISABLE_SHAKE"), (int i) => i switch
		{
			2 => Dialog.Clean("OPTIONS_RUMBLE_ON"), 
			1 => Dialog.Clean("OPTIONS_RUMBLE_HALF"), 
			_ => Dialog.Clean("OPTIONS_RUMBLE_OFF"), 
		}, 0, 2, (int)Settings.Instance.ScreenShake).Change(delegate(int i)
		{
			Settings.Instance.ScreenShake = (ScreenshakeAmount)i;
		}));
		menu.Add(viewport = new TextMenu.Button(Dialog.Clean("OPTIONS_VIEWPORT_PC")).Pressed(OpenViewportAdjustment));
		menu.Add(new TextMenu.SubHeader(Dialog.Clean("options_audio")));
		menu.Add(new TextMenu.Slider(Dialog.Clean("options_music"), (int i) => i.ToString(), 0, 10, Settings.Instance.MusicVolume).Change(SetMusic).Enter(EnterSound).Leave(LeaveSound));
		menu.Add(new TextMenu.Slider(Dialog.Clean("options_sounds"), (int i) => i.ToString(), 0, 10, Settings.Instance.SFXVolume).Change(SetSfx).Enter(EnterSound).Leave(LeaveSound));
		menu.Add(new TextMenu.SubHeader(Dialog.Clean("options_gameplay")));
		menu.Add(new TextMenu.Slider(Dialog.Clean("options_speedrun"), (int i) => i switch
		{
			0 => Dialog.Get("OPTIONS_OFF"), 
			1 => Dialog.Get("OPTIONS_SPEEDRUN_CHAPTER"), 
			_ => Dialog.Get("OPTIONS_SPEEDRUN_FILE"), 
		}, 0, 2, (int)Settings.Instance.SpeedrunClock).Change(SetSpeedrunClock));
		CreateRuntimeOptions(menu);
		viewport.Visible = Settings.Instance.Fullscreen;
		if (window != null)
		{
			window.Visible = !Settings.Instance.Fullscreen;
		}
		if (menu.Height > menu.ScrollableMinSize)
		{
			menu.Position.Y = menu.ScrollTargetY;
		}
		return menu;
	}

	private static void CreateRumble(TextMenu menu)
	{
		menu.Add(new TextMenu.Slider(Dialog.Clean("options_rumble_PC"), (int i) => i switch
		{
			2 => Dialog.Clean("OPTIONS_RUMBLE_ON"), 
			1 => Dialog.Clean("OPTIONS_RUMBLE_HALF"), 
			_ => Dialog.Clean("OPTIONS_RUMBLE_OFF"), 
		}, 0, 2, (int)Settings.Instance.Rumble).Change(delegate(int i)
		{
			Settings.Instance.Rumble = (RumbleAmount)i;
			Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
		}));
	}

	private static void CreateGrabMode(TextMenu menu)
	{
		menu.Add(new TextMenu.Slider(Dialog.Clean("OPTIONS_GRAB_MODE"), (int i) => i switch
		{
			0 => Dialog.Clean("OPTIONS_BUTTON_HOLD"), 
			1 => Dialog.Clean("OPTIONS_BUTTON_INVERT"), 
			_ => Dialog.Clean("OPTIONS_BUTTON_TOGGLE"), 
		}, 0, 2, (int)Settings.Instance.GrabMode).Change(delegate(int i)
		{
			Settings.Instance.GrabMode = (GrabModes)i;
			Input.ResetGrab();
		}));
	}

	private static TextMenu.Item CreateCrouchDashMode(TextMenu menu)
	{
		TextMenu.Option<int> option = new TextMenu.Slider(Dialog.Clean("OPTIONS_CROUCH_DASH_MODE"), (int i) => (i == 0) ? Dialog.Clean("OPTIONS_BUTTON_PRESS") : Dialog.Clean("OPTIONS_BUTTON_HOLD"), 0, 1, (int)Settings.Instance.CrouchDashMode).Change(delegate(int i)
		{
			Settings.Instance.CrouchDashMode = (CrouchDashModes)i;
		});
		option.Visible = Input.CrouchDash.Binding.HasInput;
		menu.Add(option);
		return option;
	}

	private static void CreateRuntimeOptions(TextMenu menu)
	{
		if (!CelesteRuntimeConfigBridge.TryGetSnapshot(out RuntimeUiConfigSnapshot snapshot))
		{
			return;
		}

		menu.Add(new TextMenu.SubHeader(RuntimeText("runtime_section")));
		menu.Add(new TextMenu.SubHeader(RuntimeText("runtime_game"), topPadding: false));

		menu.Add(new TextMenu.OnOff(RuntimeText("runtime_vsync"), snapshot.VSync).Change(delegate(bool on)
		{
			ApplyRuntimeUpdate(delegate(RuntimeUiConfigUpdate update)
			{
				update.VSync = on;
			});
		}));

		int targetFpsIndex = FindStringIndex(RuntimeTargetFpsValues, snapshot.TargetFps, fallbackIndex: 1);
		menu.Add(new TextMenu.Slider(RuntimeText("runtime_target_fps"), (int i) => (i >= 0 && i < RuntimeTargetFpsValues.Length) ? ((RuntimeTargetFpsValues[i] == "Uncapped") ? RuntimeText("runtime_value_uncapped") : RuntimeTargetFpsValues[i]) : "60", 0, RuntimeTargetFpsValues.Length - 1, targetFpsIndex).Change(delegate(int i)
		{
			ApplyRuntimeUpdate(delegate(RuntimeUiConfigUpdate update)
			{
				update.TargetFps = RuntimeTargetFpsValues[Math.Clamp(i, 0, RuntimeTargetFpsValues.Length - 1)];
			});
		}));

		menu.Add(new TextMenu.Slider(RuntimeText("runtime_aspect"), (int i) => i switch
		{
			0 => RuntimeText("runtime_value_fit"), 
			1 => RuntimeText("runtime_value_fill"), 
			_ => RuntimeText("runtime_value_stretch"), 
		}, 0, 2, (int)snapshot.AspectMode).Change(delegate(int i)
		{
			ApplyRuntimeUpdate(delegate(RuntimeUiConfigUpdate update)
			{
				update.AspectMode = (RuntimeUiAspectModes)Math.Clamp(i, 0, 2);
			});
		}));

		menu.Add(new TextMenu.Slider(RuntimeText("runtime_scale_filter"), (int i) => (i == 0) ? RuntimeText("runtime_value_point") : RuntimeText("runtime_value_linear"), 0, 1, (int)snapshot.ScaleFilter).Change(delegate(int i)
		{
			ApplyRuntimeUpdate(delegate(RuntimeUiConfigUpdate update)
			{
				update.ScaleFilter = (RuntimeUiScaleFilters)Math.Clamp(i, 0, 1);
			});
		}));

		menu.Add(new TextMenu.OnOff(RuntimeText("runtime_bloom"), snapshot.Bloom).Change(delegate(bool on)
		{
			ApplyRuntimeUpdate(delegate(RuntimeUiConfigUpdate update)
			{
				update.Bloom = on;
			});
		}));

		menu.Add(new TextMenu.Slider(RuntimeText("runtime_post"), (int i) => i switch
		{
			0 => RuntimeText("runtime_value_low"), 
			1 => RuntimeText("runtime_value_medium"), 
			_ => RuntimeText("runtime_value_high"), 
		}, 0, 2, (int)snapshot.PostProcessingQuality).Change(delegate(int i)
		{
			ApplyRuntimeUpdate(delegate(RuntimeUiConfigUpdate update)
			{
				update.PostProcessingQuality = (RuntimeUiQualityLevels)Math.Clamp(i, 0, 2);
			});
		}));

		menu.Add(new TextMenu.Slider(RuntimeText("runtime_particles"), (int i) => i switch
		{
			0 => RuntimeText("runtime_value_low"), 
			1 => RuntimeText("runtime_value_medium"), 
			_ => RuntimeText("runtime_value_high"), 
		}, 0, 2, (int)snapshot.Particles).Change(delegate(int i)
		{
			ApplyRuntimeUpdate(delegate(RuntimeUiConfigUpdate update)
			{
				update.Particles = (RuntimeUiQualityLevels)Math.Clamp(i, 0, 2);
			});
		}));

		menu.Add(new TextMenu.OnOff(RuntimeText("runtime_compat"), snapshot.ForceCompatibilityCompositor).Change(delegate(bool on)
		{
			ApplyRuntimeUpdate(delegate(RuntimeUiConfigUpdate update)
			{
				update.ForceCompatibilityCompositor = on;
			});
		}));

		menu.Add(new TextMenu.OnOff(RuntimeText("runtime_legacy"), snapshot.ForceLegacyBlendStates).Change(delegate(bool on)
		{
			ApplyRuntimeUpdate(delegate(RuntimeUiConfigUpdate update)
			{
				update.ForceLegacyBlendStates = on;
			});
		}));

		menu.Add(new TextMenu.OnOff(RuntimeText("runtime_edge"), snapshot.UseEdgeToEdgeOnAndroid).Change(delegate(bool on)
		{
			ApplyRuntimeUpdate(delegate(RuntimeUiConfigUpdate update)
			{
				update.UseEdgeToEdgeOnAndroid = on;
			});
		}));

		menu.Add(new TextMenu.OnOff(RuntimeText("runtime_logs"), snapshot.EnableDiagnosticLogs).Change(delegate(bool on)
		{
			ApplyRuntimeUpdate(delegate(RuntimeUiConfigUpdate update)
			{
				update.EnableDiagnosticLogs = on;
			});
		}));

		menu.Add(new TextMenu.SubHeader(RuntimeText("runtime_overlay")));

		menu.Add(new TextMenu.OnOff(RuntimeText("runtime_show_fps"), snapshot.ShowFps).Change(delegate(bool on)
		{
			ApplyRuntimeUpdate(delegate(RuntimeUiConfigUpdate update)
			{
				update.ShowFps = on;
			});
		}));

		menu.Add(new TextMenu.OnOff(RuntimeText("runtime_show_memory"), snapshot.ShowMemory).Change(delegate(bool on)
		{
			ApplyRuntimeUpdate(delegate(RuntimeUiConfigUpdate update)
			{
				update.ShowMemory = on;
			});
		}));

		menu.Add(new TextMenu.OnOff(RuntimeText("runtime_show_resolution"), snapshot.ShowResolution).Change(delegate(bool on)
		{
			ApplyRuntimeUpdate(delegate(RuntimeUiConfigUpdate update)
			{
				update.ShowResolution = on;
			});
		}));

		menu.Add(new TextMenu.OnOff(RuntimeText("runtime_show_viewport"), snapshot.ShowViewport).Change(delegate(bool on)
		{
			ApplyRuntimeUpdate(delegate(RuntimeUiConfigUpdate update)
			{
				update.ShowViewport = on;
			});
		}));

		menu.Add(new TextMenu.OnOff(RuntimeText("runtime_show_scale"), snapshot.ShowScale).Change(delegate(bool on)
		{
			ApplyRuntimeUpdate(delegate(RuntimeUiConfigUpdate update)
			{
				update.ShowScale = on;
			});
		}));

		menu.Add(new TextMenu.Slider(RuntimeText("runtime_overlay_position"), (int i) => i switch
		{
			0 => RuntimeText("runtime_value_topleft"), 
			1 => RuntimeText("runtime_value_topright"), 
			2 => RuntimeText("runtime_value_bottomleft"), 
			_ => RuntimeText("runtime_value_bottomright"), 
		}, 0, 3, (int)snapshot.OverlayPosition).Change(delegate(int i)
		{
			ApplyRuntimeUpdate(delegate(RuntimeUiConfigUpdate update)
			{
				update.OverlayPosition = (RuntimeUiOverlayPositions)Math.Clamp(i, 0, 3);
			});
		}));

		int fontScaleIndex = FindNearestIndex(RuntimeOverlayFontScaleValues, snapshot.OverlayFontScale);
		menu.Add(new TextMenu.Slider(RuntimeText("runtime_overlay_font_scale"), (int i) => (RuntimeOverlayFontScaleValues[Math.Clamp(i, 0, RuntimeOverlayFontScaleValues.Length - 1)] * 100f).ToString("0", CultureInfo.InvariantCulture) + "%", 0, RuntimeOverlayFontScaleValues.Length - 1, fontScaleIndex).Change(delegate(int i)
		{
			ApplyRuntimeUpdate(delegate(RuntimeUiConfigUpdate update)
			{
				update.OverlayFontScale = RuntimeOverlayFontScaleValues[Math.Clamp(i, 0, RuntimeOverlayFontScaleValues.Length - 1)];
			});
		}));

		int updateIntervalIndex = FindNearestIndex(RuntimeOverlayUpdateMsValues, snapshot.OverlayUpdateIntervalMs);
		menu.Add(new TextMenu.Slider(RuntimeText("runtime_overlay_update"), (int i) => RuntimeOverlayUpdateMsValues[Math.Clamp(i, 0, RuntimeOverlayUpdateMsValues.Length - 1)] + " ms", 0, RuntimeOverlayUpdateMsValues.Length - 1, updateIntervalIndex).Change(delegate(int i)
		{
			ApplyRuntimeUpdate(delegate(RuntimeUiConfigUpdate update)
			{
				update.OverlayUpdateIntervalMs = RuntimeOverlayUpdateMsValues[Math.Clamp(i, 0, RuntimeOverlayUpdateMsValues.Length - 1)];
			});
		}));

		menu.Add(new TextMenu.OnOff(RuntimeText("runtime_overlay_background"), snapshot.OverlayBackground).Change(delegate(bool on)
		{
			ApplyRuntimeUpdate(delegate(RuntimeUiConfigUpdate update)
			{
				update.OverlayBackground = on;
			});
		}));

		int paddingIndex = FindNearestIndex(RuntimeOverlayPaddingValues, snapshot.OverlayPadding);
		menu.Add(new TextMenu.Slider(RuntimeText("runtime_overlay_padding"), (int i) => RuntimeOverlayPaddingValues[Math.Clamp(i, 0, RuntimeOverlayPaddingValues.Length - 1)] + " px", 0, RuntimeOverlayPaddingValues.Length - 1, paddingIndex).Change(delegate(int i)
		{
			ApplyRuntimeUpdate(delegate(RuntimeUiConfigUpdate update)
			{
				update.OverlayPadding = RuntimeOverlayPaddingValues[Math.Clamp(i, 0, RuntimeOverlayPaddingValues.Length - 1)];
			});
		}));
	}

	private static void ApplyRuntimeUpdate(Action<RuntimeUiConfigUpdate> configure)
	{
		RuntimeUiConfigUpdate runtimeUiConfigUpdate = new RuntimeUiConfigUpdate();
		configure(runtimeUiConfigUpdate);
		CelesteRuntimeConfigBridge.TryApplyUpdate(runtimeUiConfigUpdate);
	}

	private static int FindStringIndex(string[] values, string value, int fallbackIndex)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			return fallbackIndex;
		}

		for (int i = 0; i < values.Length; i++)
		{
			if (string.Equals(values[i], value, StringComparison.OrdinalIgnoreCase))
			{
				return i;
			}
		}

		return fallbackIndex;
	}

	private static int FindNearestIndex(float[] values, float value)
	{
		int num = 0;
		float num2 = float.MaxValue;
		for (int i = 0; i < values.Length; i++)
		{
			float num3 = Math.Abs(values[i] - value);
			if (num3 < num2)
			{
				num2 = num3;
				num = i;
			}
		}

		return num;
	}

	private static int FindNearestIndex(int[] values, int value)
	{
		int num = 0;
		int num2 = int.MaxValue;
		for (int i = 0; i < values.Length; i++)
		{
			int num3 = Math.Abs(values[i] - value);
			if (num3 < num2)
			{
				num2 = num3;
				num = i;
			}
		}

		return num;
	}

	private static string RuntimeText(string key)
	{
		string text = Settings.Instance?.Language ?? "english";
		switch (text)
		{
		case "brazilian":
			return key switch
			{
				"runtime_section" => "RUNTIME ANDROID", 
				"runtime_game" => "CONFIGURACAO DO JOGO", 
				"runtime_overlay" => "CONFIGURACAO DO OVERLAY", 
				"runtime_vsync" => "VSync do Runtime", 
				"runtime_target_fps" => "FPS Alvo do Runtime", 
				"runtime_aspect" => "Modo de Aspecto", 
				"runtime_scale_filter" => "Filtro de Escala", 
				"runtime_bloom" => "Bloom", 
				"runtime_post" => "Pos-processamento", 
				"runtime_particles" => "Particulas", 
				"runtime_compat" => "Compositor de Compatibilidade", 
				"runtime_legacy" => "Blend Legado", 
				"runtime_edge" => "Tela sem Bordas", 
				"runtime_logs" => "Logs de Diagnostico", 
				"runtime_show_fps" => "Overlay: FPS", 
				"runtime_show_memory" => "Overlay: Memoria", 
				"runtime_show_resolution" => "Overlay: Resolucao", 
				"runtime_show_viewport" => "Overlay: Viewport", 
				"runtime_show_scale" => "Overlay: Escala", 
				"runtime_overlay_position" => "Posicao do Overlay", 
				"runtime_overlay_font_scale" => "Escala da Fonte", 
				"runtime_overlay_update" => "Atualizacao do Overlay", 
				"runtime_overlay_background" => "Fundo do Overlay", 
				"runtime_overlay_padding" => "Padding do Overlay", 
				"runtime_value_fit" => "Ajustar", 
				"runtime_value_fill" => "Preencher", 
				"runtime_value_stretch" => "Esticar", 
				"runtime_value_point" => "Ponto", 
				"runtime_value_linear" => "Linear", 
				"runtime_value_low" => "Baixo", 
				"runtime_value_medium" => "Medio", 
				"runtime_value_high" => "Alto", 
				"runtime_value_uncapped" => "Sem Limite", 
				"runtime_value_topleft" => "Topo Esquerda", 
				"runtime_value_topright" => "Topo Direita", 
				"runtime_value_bottomleft" => "Baixo Esquerda", 
				"runtime_value_bottomright" => "Baixo Direita", 
				_ => key, 
			};
		case "spanish":
			return key switch
			{
				"runtime_section" => "RUNTIME ANDROID", 
				"runtime_game" => "CONFIGURACION DEL JUEGO", 
				"runtime_overlay" => "CONFIGURACION DEL OVERLAY", 
				"runtime_vsync" => "VSync de Runtime", 
				"runtime_target_fps" => "FPS Objetivo", 
				"runtime_aspect" => "Modo de Aspecto", 
				"runtime_scale_filter" => "Filtro de Escala", 
				"runtime_bloom" => "Bloom", 
				"runtime_post" => "Postprocesado", 
				"runtime_particles" => "Particulas", 
				"runtime_compat" => "Compositor de Compatibilidad", 
				"runtime_legacy" => "Blend Heredado", 
				"runtime_edge" => "Pantalla sin Bordes", 
				"runtime_logs" => "Logs de Diagnostico", 
				"runtime_show_fps" => "Overlay: FPS", 
				"runtime_show_memory" => "Overlay: Memoria", 
				"runtime_show_resolution" => "Overlay: Resolucion", 
				"runtime_show_viewport" => "Overlay: Viewport", 
				"runtime_show_scale" => "Overlay: Escala", 
				"runtime_overlay_position" => "Posicion del Overlay", 
				"runtime_overlay_font_scale" => "Escala de Fuente", 
				"runtime_overlay_update" => "Actualizacion del Overlay", 
				"runtime_overlay_background" => "Fondo del Overlay", 
				"runtime_overlay_padding" => "Padding del Overlay", 
				"runtime_value_fit" => "Ajustar", 
				"runtime_value_fill" => "Rellenar", 
				"runtime_value_stretch" => "Estirar", 
				"runtime_value_point" => "Punto", 
				"runtime_value_linear" => "Lineal", 
				"runtime_value_low" => "Bajo", 
				"runtime_value_medium" => "Medio", 
				"runtime_value_high" => "Alto", 
				"runtime_value_uncapped" => "Sin Limite", 
				"runtime_value_topleft" => "Arriba Izquierda", 
				"runtime_value_topright" => "Arriba Derecha", 
				"runtime_value_bottomleft" => "Abajo Izquierda", 
				"runtime_value_bottomright" => "Abajo Derecha", 
				_ => key, 
			};
		case "french":
			return key switch
			{
				"runtime_section" => "RUNTIME ANDROID", 
				"runtime_game" => "CONFIGURATION JEU", 
				"runtime_overlay" => "CONFIGURATION OVERLAY", 
				"runtime_vsync" => "VSync Runtime", 
				"runtime_target_fps" => "FPS Cible", 
				"runtime_aspect" => "Mode Aspect", 
				"runtime_scale_filter" => "Filtre d Echelle", 
				"runtime_bloom" => "Bloom", 
				"runtime_post" => "Post-traitement", 
				"runtime_particles" => "Particules", 
				"runtime_compat" => "Compositeur Compatibilite", 
				"runtime_legacy" => "Blend Legacy", 
				"runtime_edge" => "Ecran sans Bordures", 
				"runtime_logs" => "Logs Diagnostic", 
				"runtime_show_fps" => "Overlay : FPS", 
				"runtime_show_memory" => "Overlay : Memoire", 
				"runtime_show_resolution" => "Overlay : Resolution", 
				"runtime_show_viewport" => "Overlay : Viewport", 
				"runtime_show_scale" => "Overlay : Echelle", 
				"runtime_overlay_position" => "Position Overlay", 
				"runtime_overlay_font_scale" => "Echelle Police", 
				"runtime_overlay_update" => "Frequence Overlay", 
				"runtime_overlay_background" => "Fond Overlay", 
				"runtime_overlay_padding" => "Padding Overlay", 
				"runtime_value_fit" => "Ajuster", 
				"runtime_value_fill" => "Remplir", 
				"runtime_value_stretch" => "Etirer", 
				"runtime_value_point" => "Point", 
				"runtime_value_linear" => "Lineaire", 
				"runtime_value_low" => "Bas", 
				"runtime_value_medium" => "Moyen", 
				"runtime_value_high" => "Eleve", 
				"runtime_value_uncapped" => "Illimite", 
				"runtime_value_topleft" => "Haut Gauche", 
				"runtime_value_topright" => "Haut Droite", 
				"runtime_value_bottomleft" => "Bas Gauche", 
				"runtime_value_bottomright" => "Bas Droite", 
				_ => key, 
			};
		case "german":
			return key switch
			{
				"runtime_section" => "ANDROID RUNTIME", 
				"runtime_game" => "SPIEL KONFIG", 
				"runtime_overlay" => "OVERLAY KONFIG", 
				"runtime_vsync" => "Runtime VSync", 
				"runtime_target_fps" => "Ziel FPS", 
				"runtime_aspect" => "Seitenverhaltnis", 
				"runtime_scale_filter" => "Skalierungsfilter", 
				"runtime_bloom" => "Bloom", 
				"runtime_post" => "Post Processing", 
				"runtime_particles" => "Partikel", 
				"runtime_compat" => "Kompatibilitats Compositor", 
				"runtime_legacy" => "Legacy Blend", 
				"runtime_edge" => "Randloser Bildschirm", 
				"runtime_logs" => "Diagnose Logs", 
				"runtime_show_fps" => "Overlay: FPS", 
				"runtime_show_memory" => "Overlay: Speicher", 
				"runtime_show_resolution" => "Overlay: Auflosung", 
				"runtime_show_viewport" => "Overlay: Viewport", 
				"runtime_show_scale" => "Overlay: Skalierung", 
				"runtime_overlay_position" => "Overlay Position", 
				"runtime_overlay_font_scale" => "Overlay Schriftgrosse", 
				"runtime_overlay_update" => "Overlay Aktualisierung", 
				"runtime_overlay_background" => "Overlay Hintergrund", 
				"runtime_overlay_padding" => "Overlay Padding", 
				"runtime_value_fit" => "Anpassen", 
				"runtime_value_fill" => "Fullen", 
				"runtime_value_stretch" => "Strecken", 
				"runtime_value_point" => "Punkt", 
				"runtime_value_linear" => "Linear", 
				"runtime_value_low" => "Niedrig", 
				"runtime_value_medium" => "Mittel", 
				"runtime_value_high" => "Hoch", 
				"runtime_value_uncapped" => "Unbegrenzt", 
				"runtime_value_topleft" => "Oben Links", 
				"runtime_value_topright" => "Oben Rechts", 
				"runtime_value_bottomleft" => "Unten Links", 
				"runtime_value_bottomright" => "Unten Rechts", 
				_ => key, 
			};
		case "italian":
			return key switch
			{
				"runtime_section" => "RUNTIME ANDROID", 
				"runtime_game" => "CONFIGURAZIONE GIOCO", 
				"runtime_overlay" => "CONFIGURAZIONE OVERLAY", 
				"runtime_vsync" => "VSync Runtime", 
				"runtime_target_fps" => "FPS Obiettivo", 
				"runtime_aspect" => "Modalita Aspetto", 
				"runtime_scale_filter" => "Filtro Scala", 
				"runtime_bloom" => "Bloom", 
				"runtime_post" => "Post Processing", 
				"runtime_particles" => "Particelle", 
				"runtime_compat" => "Compositore Compatibilita", 
				"runtime_legacy" => "Blend Legacy", 
				"runtime_edge" => "Schermo senza Bordi", 
				"runtime_logs" => "Log Diagnostici", 
				"runtime_show_fps" => "Overlay: FPS", 
				"runtime_show_memory" => "Overlay: Memoria", 
				"runtime_show_resolution" => "Overlay: Risoluzione", 
				"runtime_show_viewport" => "Overlay: Viewport", 
				"runtime_show_scale" => "Overlay: Scala", 
				"runtime_overlay_position" => "Posizione Overlay", 
				"runtime_overlay_font_scale" => "Scala Font Overlay", 
				"runtime_overlay_update" => "Aggiornamento Overlay", 
				"runtime_overlay_background" => "Sfondo Overlay", 
				"runtime_overlay_padding" => "Padding Overlay", 
				"runtime_value_fit" => "Adatta", 
				"runtime_value_fill" => "Riempi", 
				"runtime_value_stretch" => "Allunga", 
				"runtime_value_point" => "Punto", 
				"runtime_value_linear" => "Lineare", 
				"runtime_value_low" => "Basso", 
				"runtime_value_medium" => "Medio", 
				"runtime_value_high" => "Alto", 
				"runtime_value_uncapped" => "Senza Limite", 
				"runtime_value_topleft" => "Alto Sinistra", 
				"runtime_value_topright" => "Alto Destra", 
				"runtime_value_bottomleft" => "Basso Sinistra", 
				"runtime_value_bottomright" => "Basso Destra", 
				_ => key, 
			};
		default:
			return key switch
			{
				"runtime_section" => "ANDROID RUNTIME", 
				"runtime_game" => "GAME CONFIG", 
				"runtime_overlay" => "OVERLAY CONFIG", 
				"runtime_vsync" => "Runtime VSync", 
				"runtime_target_fps" => "Runtime Target FPS", 
				"runtime_aspect" => "Aspect Mode", 
				"runtime_scale_filter" => "Scale Filter", 
				"runtime_bloom" => "Bloom", 
				"runtime_post" => "Post Processing", 
				"runtime_particles" => "Particles", 
				"runtime_compat" => "Compatibility Compositor", 
				"runtime_legacy" => "Legacy Blend States", 
				"runtime_edge" => "Edge-to-Edge", 
				"runtime_logs" => "Diagnostic Logs", 
				"runtime_show_fps" => "Overlay: FPS", 
				"runtime_show_memory" => "Overlay: Memory", 
				"runtime_show_resolution" => "Overlay: Resolution", 
				"runtime_show_viewport" => "Overlay: Viewport", 
				"runtime_show_scale" => "Overlay: Scale", 
				"runtime_overlay_position" => "Overlay Position", 
				"runtime_overlay_font_scale" => "Overlay Font Scale", 
				"runtime_overlay_update" => "Overlay Refresh", 
				"runtime_overlay_background" => "Overlay Background", 
				"runtime_overlay_padding" => "Overlay Padding", 
				"runtime_value_fit" => "Fit", 
				"runtime_value_fill" => "Fill", 
				"runtime_value_stretch" => "Stretch", 
				"runtime_value_point" => "Point", 
				"runtime_value_linear" => "Linear", 
				"runtime_value_low" => "Low", 
				"runtime_value_medium" => "Medium", 
				"runtime_value_high" => "High", 
				"runtime_value_uncapped" => "Uncapped", 
				"runtime_value_topleft" => "Top Left", 
				"runtime_value_topright" => "Top Right", 
				"runtime_value_bottomleft" => "Bottom Left", 
				"runtime_value_bottomright" => "Bottom Right", 
				_ => key, 
			};
		}
	}

	private static void SetFullscreen(bool on)
	{
		Settings.Instance.Fullscreen = on;
		Settings.Instance.ApplyScreen();
		if (window != null)
		{
			window.Visible = !on;
		}
		if (viewport != null)
		{
			viewport.Visible = on;
		}
	}

	private static void SetVSync(bool on)
	{
		Settings.Instance.VSync = on;
		Engine.Graphics.SynchronizeWithVerticalRetrace = Settings.Instance.VSync;
		Engine.Graphics.ApplyChanges();
	}

	private static void SetWindow(int scale)
	{
		Settings.Instance.WindowScale = scale;
		Settings.Instance.ApplyScreen();
	}

	private static void SetMusic(int volume)
	{
		Settings.Instance.MusicVolume = volume;
		Settings.Instance.ApplyMusicVolume();
	}

	private static void SetSfx(int volume)
	{
		Settings.Instance.SFXVolume = volume;
		Settings.Instance.ApplySFXVolume();
	}

	private static void SetSpeedrunClock(int val)
	{
		Settings.Instance.SpeedrunClock = (SpeedrunType)val;
	}

	private static void OpenViewportAdjustment()
	{
		if (Engine.Scene is Overworld)
		{
			(Engine.Scene as Overworld).ShowInputUI = false;
		}
		menu.Visible = false;
		menu.Focused = false;
		ViewportAdjustmentUI viewportAdjustmentUI = new ViewportAdjustmentUI();
		viewportAdjustmentUI.OnClose = delegate
		{
			menu.Visible = true;
			menu.Focused = true;
			if (Engine.Scene is Overworld)
			{
				(Engine.Scene as Overworld).ShowInputUI = true;
			}
		};
		Engine.Scene.Add(viewportAdjustmentUI);
		Engine.Scene.OnEndOfFrame += delegate
		{
			Engine.Scene.Entities.UpdateLists();
		};
	}

	private static void SelectLanguage()
	{
		menu.Focused = false;
		LanguageSelectUI languageSelectUI = new LanguageSelectUI();
		languageSelectUI.OnClose = delegate
		{
			menu.Focused = true;
		};
		Engine.Scene.Add(languageSelectUI);
		Engine.Scene.OnEndOfFrame += delegate
		{
			Engine.Scene.Entities.UpdateLists();
		};
	}

	private static void OpenKeyboardConfig()
	{
		menu.Focused = false;
		KeyboardConfigUI keyboardConfigUI = new KeyboardConfigUI();
		keyboardConfigUI.OnClose = delegate
		{
			menu.Focused = true;
		};
		Engine.Scene.Add(keyboardConfigUI);
		Engine.Scene.OnEndOfFrame += delegate
		{
			Engine.Scene.Entities.UpdateLists();
		};
	}

	private static void OpenButtonConfig()
	{
		menu.Focused = false;
		if (Engine.Scene is Overworld)
		{
			(Engine.Scene as Overworld).ShowConfirmUI = false;
		}
		ButtonConfigUI buttonConfigUI = new ButtonConfigUI();
		buttonConfigUI.OnClose = delegate
		{
			menu.Focused = true;
			if (Engine.Scene is Overworld)
			{
				(Engine.Scene as Overworld).ShowConfirmUI = true;
			}
		};
		Engine.Scene.Add(buttonConfigUI);
		Engine.Scene.OnEndOfFrame += delegate
		{
			Engine.Scene.Entities.UpdateLists();
		};
	}

	private static void EnterSound()
	{
		if (inGame && snapshot != null)
		{
			Audio.EndSnapshot(snapshot);
		}
	}

	private static void LeaveSound()
	{
		if (inGame && snapshot != null)
		{
			Audio.ResumeSnapshot(snapshot);
		}
	}

	public static void UpdateCrouchDashModeVisibility()
	{
		if (crouchDashMode != null)
		{
			crouchDashMode.Visible = Input.CrouchDash.Binding.HasInput;
		}
	}
}
