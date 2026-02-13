using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Celeste.Editor;
using Celeste.Pico8;
using FMOD.Studio;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public static class Commands
{
	[Command("global_stats", "logs global steam stats")]
	private static void CmdGlobalStats()
	{
		foreach (Stat value in Enum.GetValues(typeof(Stat)))
		{
			Engine.Commands.Log(value.ToString() + ": " + Stats.Global(value));
		}
	}

	[Command("export_dialog", "export dialog files to binary format")]
	private static void CmdExportDialog()
	{
		foreach (string item in Directory.EnumerateFiles(Path.Combine("Content", "Dialog"), "*.txt"))
		{
			if (item.EndsWith(".txt"))
			{
				Language language = Language.FromTxt(item);
				language.Export(item + ".export");
				language.Dispose();
			}
		}
	}

	[Command("give_golden", "gives you a golden strawb")]
	private static void CmdGiveGolden()
	{
		if (Engine.Scene is Level level)
		{
			Player entity = level.Tracker.GetEntity<Player>();
			if (entity != null)
			{
				EntityData entityData = new EntityData();
				entityData.Position = entity.Position + new Vector2(0f, -16f);
				entityData.ID = Calc.Random.Next();
				entityData.Name = "goldenBerry";
				Strawberry entity2 = new Strawberry(gid: new EntityID(level.Session.Level, entityData.ID), data: entityData, offset: Vector2.Zero);
				level.Add(entity2);
			}
		}
	}

	[Command("unlock_doors", "unlock all lockblocks")]
	private static void CmdUnlockDoors()
	{
		foreach (LockBlock item in (Engine.Scene as Level).Entities.FindAll<LockBlock>())
		{
			item.RemoveSelf();
		}
	}

	[Command("ltng", "disable lightning")]
	private static void CmdLightning(bool disabled = true)
	{
		(Engine.Scene as Level).Session.SetFlag("disable_lightning", disabled);
	}

	[Command("bounce", "bounces the player!")]
	private static void CmdBounce()
	{
		Player entity = Engine.Scene.Tracker.GetEntity<Player>();
		entity?.Bounce(entity.Bottom);
	}

	[Command("sound_instances", "gets active sound count")]
	private static void CmdSounds()
	{
		int num = 0;
		foreach (KeyValuePair<string, EventDescription> cachedEventDescription in Audio.cachedEventDescriptions)
		{
			cachedEventDescription.Value.getInstanceCount(out var count);
			if (count > 0)
			{
				cachedEventDescription.Value.getPath(out var path);
				Engine.Commands.Log(path + ": " + count);
				Console.WriteLine(path + ": " + count);
			}
			num += count;
		}
		Engine.Commands.Log("total: " + num);
		Console.WriteLine("total: " + num);
	}

	[Command("lighting", "checks lightiing values")]
	private static void CmdLighting()
	{
		if (Engine.Scene is Level level)
		{
			Engine.Commands.Log("base(" + level.BaseLightingAlpha + "), session add(" + level.Session.LightingAlphaAdd + "), current (" + level.Lighting.Alpha + ")");
		}
	}

	[Command("detailed_levels", "counts detailed levels")]
	private static void CmdDetailedLevels(int area = -1, int mode = 0)
	{
		if (area == -1)
		{
			int num = 0;
			int num2 = 0;
			foreach (AreaData area2 in AreaData.Areas)
			{
				for (int i = 0; i < area2.Mode.Length; i++)
				{
					ModeProperties modeProperties = area2.Mode[i];
					if (modeProperties == null)
					{
						continue;
					}
					foreach (LevelData level in modeProperties.MapData.Levels)
					{
						if (!level.Dummy)
						{
							num++;
							if (level.BgDecals.Count + level.FgDecals.Count >= 2)
							{
								num2++;
							}
						}
					}
				}
			}
			Engine.Commands.Log(num2 + " / " + num);
			return;
		}
		int num3 = 0;
		int num4 = 0;
		List<string> list = new List<string>();
		foreach (LevelData level2 in AreaData.GetMode(area, (AreaMode)mode).MapData.Levels)
		{
			if (!level2.Dummy)
			{
				num3++;
				if (level2.BgDecals.Count + level2.FgDecals.Count >= 2)
				{
					num4++;
				}
				else
				{
					list.Add(level2.Name);
				}
			}
		}
		Engine.Commands.Log(string.Join(", ", list), Color.Red);
		Engine.Commands.Log(num4 + " / " + num3);
	}

	[Command("hearts", "gives a certain number of hearts (default all)")]
	private static void CmdHearts(int amount = 24)
	{
		int num = 0;
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < SaveData.Instance.Areas.Count; j++)
			{
				AreaModeStats areaModeStats = SaveData.Instance.Areas[j].Modes[i];
				if (areaModeStats != null)
				{
					if (num < amount)
					{
						num++;
						areaModeStats.HeartGem = true;
					}
					else
					{
						areaModeStats.HeartGem = false;
					}
				}
			}
		}
		Calc.Log(SaveData.Instance.TotalHeartGems);
	}

	[Command("logsession", "log session to output")]
	private static void CmdLogSession()
	{
		Session session = (Engine.Scene as Level).Session;
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(Session));
		StringWriter stringWriter = new StringWriter();
		xmlSerializer.Serialize(stringWriter, session);
		Console.WriteLine(stringWriter.ToString());
	}

	[Command("postcard", "views a postcard")]
	private static void CmdPostcard(string id, int area = 1)
	{
		Engine.Scene = new PreviewPostcard(new Postcard(Dialog.Get("POSTCARD_" + id), area));
	}

	[Command("postcard_cside", "views a postcard")]
	private static void CmdPostcardCside()
	{
		Engine.Scene = new PreviewPostcard(new Postcard(Dialog.Get("POSTCARD_CSIDES"), "event:/ui/main/postcard_csides_in", "event:/ui/main/postcard_csides_out"));
	}

	[Command("postcard_variants", "views a postcard")]
	private static void CmdPostcardVariants()
	{
		Engine.Scene = new PreviewPostcard(new Postcard(Dialog.Get("POSTCARD_VARIANTS"), "event:/new_content/ui/postcard_variants_in", "event:/new_content/ui/postcard_variants_out"));
	}

	[Command("check_all_languages", "compares all langauges to english")]
	private static void CmdCheckLangauges(bool compareContent = false)
	{
		Engine.Commands.Log("---------------------");
		bool flag = true;
		foreach (KeyValuePair<string, Language> language in Dialog.Languages)
		{
			flag &= CmdCheckLangauge(language.Key, compareContent);
		}
		Engine.Commands.Log("---------------------");
		Engine.Commands.Log("REUSLT: " + flag, flag ? Color.LawnGreen : Color.Red);
	}

	[Command("check_language", "compares all langauges to english")]
	private static bool CmdCheckLangauge(string id, bool compareContent = false)
	{
		bool flag = true;
		Language language = Dialog.Languages[id];
		Language language2 = Dialog.Languages["english"];
		int num;
		if (language.FontFace != language2.FontFace)
		{
			if (Settings.Instance != null)
			{
				num = ((language.FontFace != Dialog.Languages[Settings.Instance.Language].FontFace) ? 1 : 0);
				if (num == 0)
				{
					goto IL_0071;
				}
			}
			else
			{
				num = 1;
			}
			Fonts.Load(language.FontFace);
		}
		else
		{
			num = 0;
		}
		goto IL_0071;
		IL_0071:
		bool flag2 = Dialog.CheckLanguageFontCharacters(id);
		bool flag3 = Dialog.CompareLanguages("english", id, compareContent);
		if (num != 0)
		{
			Fonts.Unload(language.FontFace);
		}
		Engine.Commands.Log(id + " [FONT: " + flag2 + ", MATCH: " + flag3 + "]", (flag2 && flag3) ? Color.White : Color.Red);
		return flag && flag2 && flag3;
	}

	[Command("characters", "gets all the characters of each text file (writes to console")]
	private static void CmdTextCharacters()
	{
		Dialog.CheckCharacters();
	}

	[Command("berries_order", "checks strawbs order")]
	private static void CmdBerriesOrder()
	{
		foreach (AreaData area in AreaData.Areas)
		{
			for (int i = 0; i < area.Mode.Length; i++)
			{
				if (area.Mode[i] == null)
				{
					continue;
				}
				HashSet<string> hashSet = new HashSet<string>();
				EntityData[,] array = new EntityData[10, 25];
				foreach (EntityData strawberry in area.Mode[i].MapData.Strawberries)
				{
					int num = strawberry.Int("checkpointID");
					int num2 = strawberry.Int("order");
					string item = num + ":" + num2;
					if (hashSet.Contains(item))
					{
						Engine.Commands.Log("Conflicting Berry: Area[" + area.ID + "] Mode[" + i + "] Checkpoint[" + num + "] Order[" + num2 + "]", Color.Red);
					}
					else
					{
						hashSet.Add(item);
					}
					array[num, num2] = strawberry;
				}
				for (int j = 0; j < array.GetLength(0); j++)
				{
					for (int k = 1; k < array.GetLength(1); k++)
					{
						if (array[j, k] != null && array[j, k - 1] == null)
						{
							Engine.Commands.Log("Missing Berry Order #" + (k - 1) + ": Area[" + area.ID + "] Mode[" + i + "] Checkpoint[" + j + "]", Color.Red);
						}
					}
				}
			}
		}
	}

	[Command("ow_reflection_fall", "tests reflection overworld fall cutscene")]
	private static void CmdOWReflectionFall()
	{
		Engine.Scene = new OverworldReflectionsFall(null, delegate
		{
			Engine.Scene = new OverworldLoader(Overworld.StartMode.Titlescreen);
		});
	}

	[Command("core", "set the core mode of the level")]
	private static void CmdCore(int mode = 0, bool session = false)
	{
		(Engine.Scene as Level).CoreMode = (Session.CoreModes)mode;
		if (session)
		{
			(Engine.Scene as Level).Session.CoreMode = (Session.CoreModes)mode;
		}
	}

	[Command("audio", "checks audio state of session")]
	private static void CmdAudio()
	{
		if (!(Engine.Scene is Level))
		{
			return;
		}
		AudioState audio = (Engine.Scene as Level).Session.Audio;
		Engine.Commands.Log("MUSIC: " + audio.Music.Event, Color.Green);
		foreach (MEP parameter in audio.Music.Parameters)
		{
			Engine.Commands.Log("    " + parameter.Key + " = " + parameter.Value);
		}
		Engine.Commands.Log("AMBIENCE: " + audio.Ambience.Event, Color.Green);
		foreach (MEP parameter2 in audio.Ambience.Parameters)
		{
			Engine.Commands.Log("    " + parameter2.Key + " = " + parameter2.Value);
		}
	}

	[Command("heartgem", "give heart gem")]
	private static void CmdHeartGem(int area, int mode, bool gem = true)
	{
		SaveData.Instance.Areas[area].Modes[mode].HeartGem = gem;
	}

	[Command("summitgem", "gives summit gem")]
	private static void CmdSummitGem(string gem)
	{
		if (gem == "all")
		{
			for (int i = 0; i < 6; i++)
			{
				(Engine.Scene as Level).Session.SummitGems[i] = true;
			}
		}
		else
		{
			(Engine.Scene as Level).Session.SummitGems[int.Parse(gem)] = true;
		}
	}

	[Command("screenpadding", "sets level screenpadding")]
	private static void CmdScreenPadding(int value)
	{
		if (Engine.Scene is Level level)
		{
			level.ScreenPadding = value;
		}
	}

	[Command("textures", "counts textures in memory")]
	private static void CmdTextures()
	{
		Engine.Commands.Log(VirtualContent.Count);
		VirtualContent.BySize();
	}

	[Command("givekey", "creates a key on the player")]
	private static void CmdGiveKey()
	{
		Player entity = Engine.Scene.Tracker.GetEntity<Player>();
		if (entity != null)
		{
			Level obj = Engine.Scene as Level;
			Key key = new Key(entity, new EntityID("unknown", 1073741823 + Calc.Random.Next(10000)));
			obj.Add(key);
			obj.Session.Keys.Add(key.ID);
		}
	}

	[Command("ref_fall", "test the reflection fall sequence")]
	private static void CmdRefFall()
	{
		SaveData.InitializeDebugMode();
		Session session = new Session(new AreaKey(6));
		session.Level = "04";
		Engine.Scene = new LevelLoader(session, session.GetSpawnPoint(new Vector2(session.LevelData.Bounds.Center.X, session.LevelData.Bounds.Top)))
		{
			PlayerIntroTypeOverride = Player.IntroTypes.Fall,
			Level = 
			{
				new BackgroundFadeIn(Color.Black, 2f, 30f)
			}
		};
	}

	[Command("lines", "Counts Dialog Lines")]
	private static void CmdLines(string language)
	{
		if (string.IsNullOrEmpty(language))
		{
			language = Dialog.Language.Id;
		}
		if (Dialog.Languages.ContainsKey(language))
		{
			Engine.Commands.Log(language + ": " + Dialog.Languages[language].Lines + " lines, " + Dialog.Languages[language].Words + " words");
		}
		else
		{
			Engine.Commands.Log("language '" + language + "' doesn't exist");
		}
	}

	[Command("leaf", "play the leaf minigame")]
	private static void CmdLeaf()
	{
		Engine.Scene = new TestBreathingGame();
	}

	[Command("wipes", "plays screen wipes for kert")]
	private static void CmdWipes()
	{
		Engine.Scene = new TestWipes();
	}

	[Command("pico", "plays pico-8 game, optional room skip (x/y)")]
	private static void CmdPico(int roomX = 0, int roomY = 0)
	{
		Engine.Scene = new Emulator(null, roomX, roomY);
	}

	[Command("colorgrading", "sets color grading enabled (true/false)")]
	private static void CmdColorGrading(bool enabled)
	{
		ColorGrade.Enabled = enabled;
	}

	[Command("portraits", "portrait debugger")]
	private static void CmdPortraits()
	{
		Engine.Scene = new PreviewPortrait();
	}

	[Command("dialog", "dialog debugger")]
	private static void CmdDialog()
	{
		Engine.Scene = new PreviewDialog();
	}

	[Command("titlescreen", "go to the titlescreen")]
	private static void CmdTitlescreen()
	{
		Engine.Scene = new OverworldLoader(Overworld.StartMode.Titlescreen);
	}

	[Command("time", "set the time speed")]
	private static void CmdTime(float rate = 1f)
	{
		Engine.TimeRate = rate;
	}

	[Command("load", "test a level")]
	private static void CmdLoad(int id = 0, string level = null)
	{
		SaveData.InitializeDebugMode();
		SaveData.Instance.LastArea = new AreaKey(id);
		Session session = new Session(new AreaKey(id));
		if (level != null && session.MapData.Get(level) != null)
		{
			session.Level = level;
			session.FirstLevel = false;
		}
		Engine.Scene = new LevelLoader(session);
	}

	[Command("hard", "test a hard level")]
	private static void CmdHard(int id = 0, string level = null)
	{
		SaveData.InitializeDebugMode();
		SaveData.Instance.LastArea = new AreaKey(id, AreaMode.BSide);
		Session session = new Session(new AreaKey(id, AreaMode.BSide));
		if (level != null)
		{
			session.Level = level;
			session.FirstLevel = false;
		}
		Engine.Scene = new LevelLoader(session);
	}

	[Command("music_progress", "set music progress value")]
	private static void CmdMusProgress(int progress)
	{
		Audio.SetMusicParam("progress", progress);
	}

	[Command("rmx2", "test a RMX2 level")]
	private static void CmdRMX2(int id = 0, string level = null)
	{
		SaveData.InitializeDebugMode();
		SaveData.Instance.LastArea = new AreaKey(id, AreaMode.CSide);
		Session session = new Session(new AreaKey(id, AreaMode.CSide));
		if (level != null)
		{
			session.Level = level;
			session.FirstLevel = false;
		}
		Engine.Scene = new LevelLoader(session);
	}

	[Command("complete", "test the complete screen for an area")]
	private static void CmdComplete(int index = 1, int mode = 0, int deaths = 0, int strawberries = 0, bool gem = false)
	{
		if (SaveData.Instance == null)
		{
			SaveData.InitializeDebugMode();
			SaveData.Instance.CurrentSession = new Session(AreaKey.Default);
		}
		AreaKey area = new AreaKey(index, (AreaMode)mode);
		int num = 0;
		Session session = new Session(area);
		while (session.Strawberries.Count < strawberries)
		{
			num++;
			session.Strawberries.Add(new EntityID("null", num));
		}
		session.Deaths = deaths;
		session.Cassette = gem;
		session.Time = 100000L + (long)Calc.Random.Next();
		Engine.Scene = new LevelExit(LevelExit.Mode.Completed, session);
	}

	[Command("ow_complete", "test the completion sequence on the overworld after a level")]
	private static void CmdOWComplete(int index = 1, int mode = 0, int deaths = 0, int strawberries = -1, bool cassette = true, bool heartGem = true, float beatBestTimeBy = 1.7921f, float beatBestFullClearTimeBy = 1.7921f)
	{
		if (SaveData.Instance == null)
		{
			SaveData.InitializeDebugMode();
			SaveData.Instance.CurrentSession = new Session(AreaKey.Default);
		}
		AreaKey areaKey = new AreaKey(index, (AreaMode)mode);
		Session session = new Session(areaKey);
		AreaStats areaStats = SaveData.Instance.Areas[index];
		AreaModeStats areaModeStats = areaStats.Modes[mode];
		double totalSeconds = TimeSpan.FromTicks(areaModeStats.BestTime).TotalSeconds;
		double totalSeconds2 = TimeSpan.FromTicks(areaModeStats.BestFullClearTime).TotalSeconds;
		SaveData.Instance.RegisterCompletion(session);
		SaveData.Instance.CurrentSession = session;
		SaveData.Instance.CurrentSession.OldStats = new AreaStats(index);
		SaveData.Instance.LastArea = areaKey;
		if (mode == 0)
		{
			if (strawberries == -1)
			{
				areaStats.Modes[0].TotalStrawberries = AreaData.Areas[index].Mode[0].TotalStrawberries;
			}
			else
			{
				areaStats.Modes[0].TotalStrawberries = Math.Max(areaStats.TotalStrawberries, strawberries);
			}
			if (cassette)
			{
				areaStats.Cassette = true;
			}
		}
		areaModeStats.Deaths = Math.Max(deaths, areaModeStats.Deaths);
		if (heartGem)
		{
			areaModeStats.HeartGem = true;
		}
		if (totalSeconds <= 0.0)
		{
			areaModeStats.BestTime = TimeSpan.FromMinutes(5.0).Ticks;
		}
		else if (beatBestTimeBy > 0f)
		{
			areaModeStats.BestTime = TimeSpan.FromSeconds(totalSeconds - (double)beatBestTimeBy).Ticks;
		}
		if (beatBestFullClearTimeBy > 0f)
		{
			if (totalSeconds2 <= 0.0)
			{
				areaModeStats.BestFullClearTime = TimeSpan.FromMinutes(5.0).Ticks;
			}
			else
			{
				areaModeStats.BestFullClearTime = TimeSpan.FromSeconds(totalSeconds2 - (double)beatBestFullClearTimeBy).Ticks;
			}
		}
		Engine.Scene = new OverworldLoader(Overworld.StartMode.AreaComplete);
	}

	[Command("mapedit", "edit a map")]
	private static void CmdMapEdit(int index = -1, int mode = 0)
	{
		AreaKey area = ((index != -1) ? new AreaKey(index, (AreaMode)mode) : ((!(Engine.Scene is Level)) ? AreaKey.Default : (Engine.Scene as Level).Session.Area));
		Engine.Scene = new MapEditor(area);
		Engine.Commands.Open = false;
	}

	[Command("dflag", "Set a savedata flag")]
	private static void CmdDFlag(string flag, bool setTo = true)
	{
		if (setTo)
		{
			SaveData.Instance.SetFlag(flag);
		}
		else
		{
			SaveData.Instance.Flags.Remove(flag);
		}
	}

	[Command("meet", "Sets flags as though you met Theo")]
	private static void CmdMeet(bool met = true, bool knowsName = true)
	{
		if (met)
		{
			SaveData.Instance.SetFlag("MetTheo");
		}
		else
		{
			SaveData.Instance.Flags.Remove("MetTheo");
		}
		if (knowsName)
		{
			SaveData.Instance.SetFlag("TheoKnowsName");
		}
		else
		{
			SaveData.Instance.Flags.Remove("TheoKnowsName");
		}
	}

	[Command("flag", "set a session flag")]
	private static void CmdFlag(string flag, bool setTo = true)
	{
		SaveData.Instance.CurrentSession.SetFlag(flag, setTo);
	}

	[Command("level_flag", "set a session load flag")]
	private static void CmdLevelFlag(string flag)
	{
		SaveData.Instance.CurrentSession.LevelFlags.Add(flag);
	}

	[Command("e", "edit a map")]
	private static void CmdE(int index = -1, int mode = 0)
	{
		CmdMapEdit(index, mode);
	}

	[Command("overworld", "go to the overworld")]
	private static void CmdOverworld()
	{
		if (SaveData.Instance == null)
		{
			SaveData.InitializeDebugMode();
			SaveData.Instance.CurrentSession = new Session(AreaKey.Default);
		}
		Engine.Scene = new OverworldLoader(Overworld.StartMode.Titlescreen);
	}

	[Command("music", "play a music track")]
	private static void CmdMusic(string song)
	{
		Audio.SetMusic(SFX.EventnameByHandle(song));
	}

	[Command("sd_clearflags", "clears all flags from the save file")]
	private static void CmdClearSave()
	{
		SaveData.Instance.Flags.Clear();
	}

	[Command("music_vol", "set the music volume")]
	private static void CmdMusicVol(int num)
	{
		Settings.Instance.MusicVolume = num;
		Settings.Instance.ApplyVolumes();
	}

	[Command("sfx_vol", "set the sfx volume")]
	private static void CmdSFXVol(int num)
	{
		Settings.Instance.SFXVolume = num;
		Settings.Instance.ApplyVolumes();
	}

	[Command("p_dreamdash", "enable dream dashing")]
	private static void CmdDreamDash(bool set = true)
	{
		(Engine.Scene as Level).Session.Inventory.DreamDash = set;
	}

	[Command("p_twodashes", "enable two dashes")]
	private static void CmdTwoDashes(bool set = true)
	{
		(Engine.Scene as Level).Session.Inventory.Dashes = ((!set) ? 1 : 2);
	}

	[Command("berries", "check how many strawberries are in the given chapter, or the entire game")]
	private static void CmdStrawberries(int chapterID = -1)
	{
		Color lime = Color.Lime;
		Color red = Color.Red;
		Color yellow = Color.Yellow;
		Color gray = Color.Gray;
		if (chapterID == -1)
		{
			int num = 0;
			int[] array = new int[AreaData.Areas.Count];
			for (int i = 0; i < AreaData.Areas.Count; i++)
			{
				new MapData(new AreaKey(i)).GetStrawberries(out array[i]);
				num += array[i];
			}
			Engine.Commands.Log("Grand Total Strawberries: " + num, yellow);
			for (int j = 0; j < array.Length; j++)
			{
				Color color = ((array[j] == AreaData.Areas[j].Mode[0].TotalStrawberries) ? ((array[j] != 0) ? lime : gray) : red);
				Engine.Commands.Log("Chapter " + j + ": " + array[j], color);
			}
			return;
		}
		AreaData areaData = AreaData.Areas[chapterID];
		int totalStrawberries = areaData.Mode[0].TotalStrawberries;
		int[] array2 = new int[areaData.Mode[0].Checkpoints.Length + 1];
		array2[0] = areaData.Mode[0].StartStrawberries;
		for (int k = 1; k < array2.Length; k++)
		{
			array2[k] = areaData.Mode[0].Checkpoints[k - 1].Strawberries;
		}
		int total;
		int[] strawberries = new MapData(new AreaKey(chapterID)).GetStrawberries(out total);
		Engine.Commands.Log("Chapter " + chapterID + " Strawberries");
		Engine.Commands.Log("Total: " + total, (totalStrawberries == total) ? lime : red);
		for (int l = 0; l < array2.Length; l++)
		{
			Color color2 = ((strawberries[l] == array2[l]) ? ((strawberries[l] != 0) ? lime : gray) : red);
			Engine.Commands.Log("CP" + l + ": " + strawberries[l], color2);
		}
	}

	[Command("say", "initiate a dialog message")]
	private static void CmdSay(string id)
	{
		Engine.Scene.Add(new Textbox(id));
	}

	[Command("level_count", "print out total level count!")]
	private static void CmdTotalLevels(int areaID = -1, int mode = 0)
	{
		if (areaID >= 0)
		{
			Engine.Commands.Log(GetLevelsInArea(new AreaKey(areaID, (AreaMode)mode)));
			return;
		}
		int num = 0;
		foreach (AreaData area in AreaData.Areas)
		{
			for (int i = 0; i < area.Mode.Length; i++)
			{
				num += GetLevelsInArea(new AreaKey(area.ID, (AreaMode)i));
			}
		}
		Engine.Commands.Log(num);
	}

	[Command("input_gui", "override input gui")]
	private static void CmdInputGui(string prefix)
	{
		Input.OverrideInputPrefix = prefix;
	}

	private static int GetLevelsInArea(AreaKey key)
	{
		return AreaData.Get(key).Mode[(int)key.Mode]?.MapData.LevelCount ?? 0;
	}

	[Command("assist", "toggle assist mode for current savefile")]
	private static void CmdAssist()
	{
		SaveData.Instance.AssistMode = !SaveData.Instance.AssistMode;
		SaveData.Instance.VariantMode = false;
	}

	[Command("variants", "toggle varaint mode for current savefile")]
	private static void CmdVariants()
	{
		SaveData.Instance.VariantMode = !SaveData.Instance.VariantMode;
		SaveData.Instance.AssistMode = false;
	}

	[Command("cheat", "toggle cheat mode for the current savefile")]
	private static void CmdCheat()
	{
		SaveData.Instance.CheatMode = !SaveData.Instance.CheatMode;
	}

	[Command("capture", "capture the last ~200 frames of player movement to a file")]
	private static void CmdCapture(string filename)
	{
		Player entity = Engine.Scene.Tracker.GetEntity<Player>();
		if (entity != null)
		{
			PlaybackData.Export(entity.ChaserStates, filename + ".bin");
		}
	}

	[Command("playback", "play back the file name")]
	private static void CmdPlayback(string filename)
	{
		filename += ".bin";
		if (File.Exists(filename))
		{
			Engine.Scene = new PreviewRecording(filename);
		}
		else
		{
			Engine.Commands.Log("FILE NOT FOUND");
		}
	}

	[Command("fonts", "check loaded fonts")]
	private static void CmdFonts()
	{
		Fonts.Log();
	}

	[Command("rename", "renames a level")]
	private static void CmdRename(string current, string newName)
	{
		if (!(Engine.Scene is MapEditor mapEditor))
		{
			Engine.Commands.Log("Must be in the Map Editor");
		}
		else
		{
			mapEditor.Rename(current, newName);
		}
	}

	[Command("blackhole_strength", "value 0 - 3")]
	private static void CmdBlackHoleStrength(int strength)
	{
		strength = Calc.Clamp(strength, 0, 3);
		if (Engine.Scene is Level level)
		{
			level.Background.Get<BlackholeBG>()?.NextStrength(level, (BlackholeBG.Strengths)strength);
		}
	}
}
