using System;
using System.Collections.Generic;
using System.IO;
using Celeste.Core.Platform.Audio;
using Celeste.Core.Platform.Interop;
using FMOD;
using FMOD.Studio;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public static class Audio
{
	public static class Banks
	{
		public static Bank Master;

		public static Bank Music;

		public static Bank Sfxs;

		public static Bank UI;

		public static Bank DlcMusic;

		public static Bank DlcSfxs;

		public static Bank Load(string name, bool loadStrings)
		{
			string text = ResolveBankPath(name);
			if (!File.Exists(text + ".bank"))
			{
				throw new FileNotFoundException("FMOD bank not found", text + ".bank");
			}

			CheckFmod(system.loadBankFile(text + ".bank", LOAD_BANK_FLAGS.NORMAL, out var bank));
			bank.loadSampleData();
			if (loadStrings)
			{
				if (!File.Exists(text + ".strings.bank"))
				{
					throw new FileNotFoundException("FMOD strings bank not found", text + ".strings.bank");
				}

				CheckFmod(system.loadBankFile(text + ".strings.bank", LOAD_BANK_FLAGS.NORMAL, out var _));
			}
			return bank;
		}

		private static string ResolveBankPath(string name)
		{
			string text = Path.Combine(Engine.ContentDirectory, "FMOD", "Android", name);
			if (File.Exists(text + ".bank"))
			{
				return text;
			}

			string text2 = Path.Combine(Engine.ContentDirectory, "FMOD", "Desktop", name);
			if (File.Exists(text2 + ".bank"))
			{
				CelestePathBridge.LogWarn("FMOD", "Android banks not found; falling back to Desktop bank layout.");
				return text2;
			}

			CelestePathBridge.LogError("FMOD", $"Missing bank '{name}' in both Android and Desktop bank folders.");
			return text;
		}
	}

	private static FMOD.Studio.System system;

	private static FMOD.Studio._3D_ATTRIBUTES attributes3d = default(FMOD.Studio._3D_ATTRIBUTES);

	public static Dictionary<string, EventDescription> cachedEventDescriptions = new Dictionary<string, EventDescription>();

	private static Camera currentCamera;

	private static bool ready;

	public static bool FallbackSilentMode { get; private set; }

	public static string LastInitError { get; private set; } = "";

	private static bool IsSystemReady => system != null && ready && !FallbackSilentMode;

	private static EventInstance currentMusicEvent = null;

	private static EventInstance currentAltMusicEvent = null;

	private static EventInstance currentAmbientEvent = null;

	private static EventInstance mainDownSnapshot = null;

	public static string CurrentMusic = "";

	private static bool musicUnderwater;

	private static EventInstance musicUnderwaterSnapshot;

	public static EventInstance CurrentMusicEventInstance => currentMusicEvent;

	public static EventInstance CurrentAmbienceEventInstance => currentAmbientEvent;

	public static float MusicVolume
	{
		get
		{
			return VCAVolume("vca:/music");
		}
		set
		{
			VCAVolume("vca:/music", value);
		}
	}

	public static float SfxVolume
	{
		get
		{
			return VCAVolume("vca:/gameplay_sfx");
		}
		set
		{
			VCAVolume("vca:/gameplay_sfx", value);
			VCAVolume("vca:/ui_sfx", value);
		}
	}

	public static bool PauseMusic
	{
		get
		{
			return BusPaused("bus:/music");
		}
		set
		{
			BusPaused("bus:/music", value);
		}
	}

	public static bool PauseGameplaySfx
	{
		get
		{
			return BusPaused("bus:/gameplay_sfx");
		}
		set
		{
			BusPaused("bus:/gameplay_sfx", value);
			BusPaused("bus:/music/stings", value);
		}
	}

	public static bool PauseUISfx
	{
		get
		{
			return BusPaused("bus:/ui_sfx");
		}
		set
		{
			BusPaused("bus:/ui_sfx", value);
		}
	}

	public static bool MusicUnderwater
	{
		get
		{
			return musicUnderwater;
		}
		set
		{
			if (musicUnderwater == value)
			{
				return;
			}
			musicUnderwater = value;
			if (musicUnderwater)
			{
				if (musicUnderwaterSnapshot == null)
				{
					musicUnderwaterSnapshot = CreateSnapshot("snapshot:/underwater");
				}
				else
				{
					ResumeSnapshot(musicUnderwaterSnapshot);
				}
			}
			else
			{
				EndSnapshot(musicUnderwaterSnapshot);
			}
		}
	}

	public static void Init()
	{
		FallbackSilentMode = false;
		LastInitError = "";
		if (AudioRuntimePolicy.ShouldForceSilentAudio())
		{
			CelestePathBridge.LogWarn("FMOD", $"Android silent-audio policy is active. Skipping FMOD init. Set '{AudioRuntimePolicy.EnableFmodOnAndroidSwitch}' switch to true to test FMOD.");
			ActivateFallback("AUDIO_DISABLED_BY_POLICY_ANDROID");
			return;
		}

		CelestePathBridge.LogInfo("FMOD", "Initializing FMOD audio system");
		FMOD.Studio.INITFLAGS studioFlags = FMOD.Studio.INITFLAGS.NORMAL;
		if (Settings.Instance.LaunchWithFMODLiveUpdate)
		{
			studioFlags = FMOD.Studio.INITFLAGS.LIVEUPDATE;
		}
		CheckFmod(FMOD.Studio.System.create(out system));
		CheckFmod(system.initialize(1024, studioFlags, FMOD.INITFLAGS.NORMAL, IntPtr.Zero));
		attributes3d.forward = new VECTOR
		{
			x = 0f,
			y = 0f,
			z = 1f
		};
		attributes3d.up = new VECTOR
		{
			x = 0f,
			y = 1f,
			z = 0f
		};
		SetListenerPosition(new Vector3(0f, 0f, 1f), new Vector3(0f, 1f, 0f), new Vector3(0f, 0f, -345f));
		ready = true;
		CelestePathBridge.LogInfo("FMOD", "FMOD initialized successfully");
	}

	public static void Update()
	{
		if (IsSystemReady)
		{
			CheckFmod(system.update());
		}
	}

	public static void Unload()
	{
		ready = false;
		cachedEventDescriptions.Clear();
		if (system != null)
		{
			CheckFmod(system.unloadAll());
			CheckFmod(system.release());
			system = null;
		}
	}

	public static void ActivateFallback(string reason, Exception exception = null)
	{
		FallbackSilentMode = true;
		ready = false;
		LastInitError = reason;
		if (string.Equals(reason, "AUDIO_DISABLED_BY_POLICY_ANDROID", StringComparison.Ordinal))
		{
			CelestePathBridge.LogWarn("FMOD", $"AUDIO_FALLBACK_ACTIVE: {reason}");
		}
		else
		{
			CelestePathBridge.LogError("FMOD", $"AUDIO_FALLBACK_ACTIVE: {reason}");
		}

		if (exception != null)
		{
			CelestePathBridge.LogError("FMOD", exception.ToString());
		}

		FMOD.Studio.System system2 = system;
		system = null;
		if (system2 != null)
		{
			if (OperatingSystem.IsAndroid())
			{
				CelestePathBridge.LogWarn("FMOD", "Skipping native FMOD release during fallback on Android to avoid runtime instability after failed bank load.");
			}
			else
			{
				try
				{
					CheckFmod(system2.unloadAll());
					CheckFmod(system2.release());
				}
				catch (Exception ex)
				{
					CelestePathBridge.LogError("FMOD", "Failed while releasing FMOD during fallback: " + ex);
				}
			}
		}

		Banks.Master = default(Bank);
		Banks.Music = default(Bank);
		Banks.Sfxs = default(Bank);
		Banks.UI = default(Bank);
		Banks.DlcMusic = default(Bank);
		Banks.DlcSfxs = default(Bank);
		currentMusicEvent = null;
		currentAltMusicEvent = null;
		currentAmbientEvent = null;
		mainDownSnapshot = null;
		musicUnderwaterSnapshot = null;

		cachedEventDescriptions.Clear();
	}

	public static void SetListenerPosition(Vector3 forward, Vector3 up, Vector3 position)
	{
		if (!IsSystemReady)
		{
			return;
		}

		FMOD.Studio._3D_ATTRIBUTES attributes = default(FMOD.Studio._3D_ATTRIBUTES);
		attributes.forward.x = forward.X;
		attributes.forward.z = forward.Y;
		attributes.forward.z = forward.Z;
		attributes.up.x = up.X;
		attributes.up.y = up.Y;
		attributes.up.z = up.Z;
		attributes.position.x = position.X;
		attributes.position.y = position.Y;
		attributes.position.z = position.Z;
		system.setListenerAttributes(0, attributes);
	}

	public static void SetCamera(Camera camera)
	{
		currentCamera = camera;
	}

	internal static void CheckFmod(RESULT result)
	{
		if (result != RESULT.OK)
		{
			throw new Exception("FMOD Failed: " + result);
		}
	}

	public static EventInstance Play(string path)
	{
		EventInstance eventInstance = CreateInstance(path);
		if (eventInstance != null)
		{
			eventInstance.start();
			eventInstance.release();
		}
		return eventInstance;
	}

	public static EventInstance Play(string path, string param, float value)
	{
		EventInstance eventInstance = CreateInstance(path);
		if (eventInstance != null)
		{
			SetParameter(eventInstance, param, value);
			eventInstance.start();
			eventInstance.release();
		}
		return eventInstance;
	}

	public static EventInstance Play(string path, Vector2 position)
	{
		EventInstance eventInstance = CreateInstance(path, position);
		if (eventInstance != null)
		{
			eventInstance.start();
			eventInstance.release();
		}
		return eventInstance;
	}

	public static EventInstance Play(string path, Vector2 position, string param, float value)
	{
		EventInstance eventInstance = CreateInstance(path, position);
		if (eventInstance != null)
		{
			if (param != null)
			{
				eventInstance.setParameterValue(param, value);
			}
			eventInstance.start();
			eventInstance.release();
		}
		return eventInstance;
	}

	public static EventInstance Play(string path, Vector2 position, string param, float value, string param2, float value2)
	{
		EventInstance eventInstance = CreateInstance(path, position);
		if (eventInstance != null)
		{
			if (param != null)
			{
				eventInstance.setParameterValue(param, value);
			}
			if (param2 != null)
			{
				eventInstance.setParameterValue(param2, value2);
			}
			eventInstance.start();
			eventInstance.release();
		}
		return eventInstance;
	}

	public static EventInstance Loop(string path)
	{
		EventInstance eventInstance = CreateInstance(path);
		if (eventInstance != null)
		{
			eventInstance.start();
		}
		return eventInstance;
	}

	public static EventInstance Loop(string path, string param, float value)
	{
		EventInstance eventInstance = CreateInstance(path);
		if (eventInstance != null)
		{
			eventInstance.setParameterValue(param, value);
			eventInstance.start();
		}
		return eventInstance;
	}

	public static EventInstance Loop(string path, Vector2 position)
	{
		EventInstance eventInstance = CreateInstance(path, position);
		if (eventInstance != null)
		{
			eventInstance.start();
		}
		return eventInstance;
	}

	public static EventInstance Loop(string path, Vector2 position, string param, float value)
	{
		EventInstance eventInstance = CreateInstance(path, position);
		if (eventInstance != null)
		{
			eventInstance.setParameterValue(param, value);
			eventInstance.start();
		}
		return eventInstance;
	}

	public static void Pause(EventInstance instance)
	{
		if (instance != null)
		{
			instance.setPaused(paused: true);
		}
	}

	public static void Resume(EventInstance instance)
	{
		if (instance != null)
		{
			instance.setPaused(paused: false);
		}
	}

	public static void Position(EventInstance instance, Vector2 position)
	{
		if (instance != null)
		{
			Vector2 vector = Vector2.Zero;
			if (currentCamera != null)
			{
				vector = currentCamera.Position + new Vector2(320f, 180f) / 2f;
			}
			float num = position.X - vector.X;
			if (SaveData.Instance != null && SaveData.Instance.Assists.MirrorMode)
			{
				num = 0f - num;
			}
			attributes3d.position.x = num;
			attributes3d.position.y = position.Y - vector.Y;
			attributes3d.position.z = 0f;
			instance.set3DAttributes(attributes3d);
		}
	}

	public static void SetParameter(EventInstance instance, string param, float value)
	{
		if (instance != null)
		{
			instance.setParameterValue(param, value);
		}
	}

	public static void Stop(EventInstance instance, bool allowFadeOut = true)
	{
		if (instance != null)
		{
			instance.stop((!allowFadeOut) ? STOP_MODE.IMMEDIATE : STOP_MODE.ALLOWFADEOUT);
			instance.release();
		}
	}

	public static EventInstance CreateInstance(string path, Vector2? position = null)
	{
		EventDescription eventDescription = GetEventDescription(path);
		if (eventDescription != null)
		{
			eventDescription.createInstance(out var instance);
			eventDescription.is3D(out var is3D);
			if (is3D && position.HasValue)
			{
				Position(instance, position.Value);
			}
			return instance;
		}
		return null;
	}

	public static EventDescription GetEventDescription(string path)
	{
		if (!IsSystemReady)
		{
			return null;
		}

		EventDescription value = null;
		if (path != null && !cachedEventDescriptions.TryGetValue(path, out value))
		{
			RESULT rESULT = system.getEvent(path, out value);
			switch (rESULT)
			{
			case RESULT.OK:
				value.loadSampleData();
				cachedEventDescriptions.Add(path, value);
				break;
			default:
				throw new Exception("FMOD getEvent failed: " + rESULT);
			case RESULT.ERR_EVENT_NOTFOUND:
				break;
			}
		}
		return value;
	}

	public static void ReleaseUnusedDescriptions()
	{
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, EventDescription> cachedEventDescription in cachedEventDescriptions)
		{
			cachedEventDescription.Value.getInstanceCount(out var count);
			if (count <= 0)
			{
				cachedEventDescription.Value.unloadSampleData();
				list.Add(cachedEventDescription.Key);
			}
		}
		foreach (string item in list)
		{
			cachedEventDescriptions.Remove(item);
		}
	}

	public static string GetEventName(EventInstance instance)
	{
		if (instance != null)
		{
			instance.getDescription(out var description);
			if (description != null)
			{
				string path = "";
				description.getPath(out path);
				return path;
			}
		}
		return "";
	}

	public static bool IsPlaying(EventInstance instance)
	{
		if (instance != null)
		{
			instance.getPlaybackState(out var state);
			if (state == PLAYBACK_STATE.PLAYING || state == PLAYBACK_STATE.STARTING)
			{
				return true;
			}
		}
		return false;
	}

	public static bool BusPaused(string path, bool? pause = null)
	{
		bool paused = false;
		if (system != null && system.getBus(path, out var bus) == RESULT.OK)
		{
			if (pause.HasValue)
			{
				bus.setPaused(pause.Value);
			}
			bus.getPaused(out paused);
		}
		return paused;
	}

	public static bool BusMuted(string path, bool? mute)
	{
		bool paused = false;
		if (system != null && system.getBus(path, out var bus) == RESULT.OK)
		{
			if (mute.HasValue)
			{
				bus.setMute(mute.Value);
			}
			bus.getPaused(out paused);
		}
		return paused;
	}

	public static void BusStopAll(string path, bool immediate = false)
	{
		if (system != null && system.getBus(path, out var bus) == RESULT.OK)
		{
			bus.stopAllEvents(immediate ? STOP_MODE.IMMEDIATE : STOP_MODE.ALLOWFADEOUT);
		}
	}

	public static float VCAVolume(string path, float? volume = null)
	{
		if (!IsSystemReady)
		{
			return 0f;
		}

		VCA vca;
		RESULT vCA = system.getVCA(path, out vca);
		float volume2 = 1f;
		float finalvolume = 1f;
		if (vCA == RESULT.OK)
		{
			if (volume.HasValue)
			{
				vca.setVolume(volume.Value);
			}
			vca.getVolume(out volume2, out finalvolume);
		}
		return volume2;
	}

	public static EventInstance CreateSnapshot(string name, bool start = true)
	{
		if (!IsSystemReady)
		{
			return null;
		}

		system.getEvent(name, out var _event);
		if (_event == null)
		{
			throw new Exception("Snapshot " + name + " doesn't exist");
		}
		_event.createInstance(out var instance);
		if (start)
		{
			instance.start();
		}
		return instance;
	}

	public static void ResumeSnapshot(EventInstance snapshot)
	{
		if (snapshot != null)
		{
			snapshot.start();
		}
	}

	public static bool IsSnapshotRunning(EventInstance snapshot)
	{
		if (snapshot != null)
		{
			snapshot.getPlaybackState(out var state);
			if (state != PLAYBACK_STATE.PLAYING && state != PLAYBACK_STATE.STARTING)
			{
				return state == PLAYBACK_STATE.SUSTAINING;
			}
			return true;
		}
		return false;
	}

	public static void EndSnapshot(EventInstance snapshot)
	{
		if (snapshot != null)
		{
			snapshot.stop(STOP_MODE.ALLOWFADEOUT);
		}
	}

	public static void ReleaseSnapshot(EventInstance snapshot)
	{
		if (snapshot != null)
		{
			snapshot.stop(STOP_MODE.ALLOWFADEOUT);
			snapshot.release();
		}
	}

	public static bool SetMusic(string path, bool startPlaying = true, bool allowFadeOut = true)
	{
		if (string.IsNullOrEmpty(path) || path == "null")
		{
			Stop(currentMusicEvent, allowFadeOut);
			currentMusicEvent = null;
			CurrentMusic = "";
		}
		else if (!CurrentMusic.Equals(path, StringComparison.OrdinalIgnoreCase))
		{
			Stop(currentMusicEvent, allowFadeOut);
			EventInstance eventInstance = CreateInstance(path);
			if (eventInstance != null && startPlaying)
			{
				eventInstance.start();
			}
			currentMusicEvent = eventInstance;
			CurrentMusic = GetEventName(eventInstance);
			return true;
		}
		return false;
	}

	public static bool SetAmbience(string path, bool startPlaying = true)
	{
		if (string.IsNullOrEmpty(path) || path == "null")
		{
			Stop(currentAmbientEvent);
			currentAmbientEvent = null;
		}
		else if (!GetEventName(currentAmbientEvent).Equals(path, StringComparison.OrdinalIgnoreCase))
		{
			Stop(currentAmbientEvent);
			EventInstance eventInstance = CreateInstance(path);
			if (eventInstance != null && startPlaying)
			{
				eventInstance.start();
			}
			currentAmbientEvent = eventInstance;
			return true;
		}
		return false;
	}

	public static void SetMusicParam(string path, float value)
	{
		if (currentMusicEvent != null)
		{
			currentMusicEvent.setParameterValue(path, value);
		}
	}

	public static void SetAltMusic(string path)
	{
		if (string.IsNullOrEmpty(path))
		{
			EndSnapshot(mainDownSnapshot);
			Stop(currentAltMusicEvent);
			currentAltMusicEvent = null;
		}
		else if (!GetEventName(currentAltMusicEvent).Equals(path, StringComparison.OrdinalIgnoreCase))
		{
			StartMainDownSnapshot();
			Stop(currentAltMusicEvent);
			currentAltMusicEvent = Loop(path);
		}
	}

	private static void StartMainDownSnapshot()
	{
		if (mainDownSnapshot == null)
		{
			mainDownSnapshot = CreateSnapshot("snapshot:/music_mains_mute");
		}
		else
		{
			ResumeSnapshot(mainDownSnapshot);
		}
	}

	private static void EndMainDownSnapshot()
	{
		EndSnapshot(mainDownSnapshot);
	}
}
