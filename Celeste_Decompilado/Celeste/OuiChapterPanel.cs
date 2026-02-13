using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using FMOD.Studio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monocle;

namespace Celeste;

public class OuiChapterPanel : Oui
{
	private class Option
	{
		public string Label;

		public string ID;

		public MTexture Icon;

		public MTexture Bg = GFX.Gui["areaselect/tab"];

		public Color BgColor = Calc.HexToColor("3c6180");

		public float Pop;

		public bool Large = true;

		public int Siblings;

		public float Slide;

		public float Appear = 1f;

		public float IconEase = 1f;

		public bool Appeared;

		public float Faded;

		public float CheckpointSlideOut;

		public string CheckpointLevelName;

		public float CheckpointRotation;

		public Vector2 CheckpointOffset;

		public float Scale
		{
			get
			{
				if (Siblings < 5)
				{
					return 1f;
				}
				return 0.8f;
			}
		}

		public bool OnTopOfUI => Pop > 0.5f;

		public void SlideTowards(int i, int count, bool snap)
		{
			float num = (float)count / 2f - 0.5f;
			float num2 = (float)i - num;
			if (snap)
			{
				Slide = num2;
			}
			else
			{
				Slide = Calc.Approach(Slide, num2, Engine.DeltaTime * 4f);
			}
		}

		public Vector2 GetRenderPosition(Vector2 center)
		{
			float num = (float)(Large ? 170 : 130) * Scale;
			if (Siblings > 0 && num * (float)Siblings > 750f)
			{
				num = 750 / Siblings;
			}
			Vector2 result = center + new Vector2(Slide * num, (float)Math.Sin(Pop * (float)Math.PI) * 70f - Pop * 12f);
			result.Y += (1f - Ease.CubeOut(Appear)) * -200f;
			result.Y -= (1f - Scale) * 80f;
			return result;
		}

		public void Render(Vector2 center, bool selected, Wiggler wiggler, Wiggler appearWiggler)
		{
			float num = Scale + (selected ? (wiggler.Value * 0.25f) : 0f) + (Appeared ? (appearWiggler.Value * 0.25f) : 0f);
			Vector2 renderPosition = GetRenderPosition(center);
			Color color = Color.Lerp(BgColor, Color.Black, (1f - Pop) * 0.6f);
			Bg.DrawCentered(renderPosition + new Vector2(0f, 10f), color, (Appeared ? Scale : num) * new Vector2(Large ? 1f : 0.9f, 1f));
			if (IconEase > 0f)
			{
				float num2 = Ease.CubeIn(IconEase);
				Color color2 = Color.Lerp(Color.White, Color.Black, Faded * 0.6f) * num2;
				Icon.DrawCentered(renderPosition, color2, (float)(Bg.Width - 50) / (float)Icon.Width * num * (2.5f - num2 * 1.5f));
			}
		}
	}

	[CompilerGenerated]
	private sealed class _003CEnter_003Ed__49 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public OuiChapterPanel _003C_003E4__this;

		private float _003Cp_003E5__2;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		[DebuggerHidden]
		public _003CEnter_003Ed__49(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			int num = _003C_003E1__state;
			OuiChapterPanel ouiChapterPanel = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				ouiChapterPanel.Visible = true;
				ouiChapterPanel.Area.Mode = AreaMode.Normal;
				ouiChapterPanel.Reset();
				ouiChapterPanel.Overworld.Mountain.EaseCamera(ouiChapterPanel.Area.ID, ouiChapterPanel.Data.MountainSelect);
				_003Cp_003E5__2 = 0f;
				break;
			case 1:
				_003C_003E1__state = -1;
				ouiChapterPanel.Position = ouiChapterPanel.ClosePosition + (ouiChapterPanel.OpenPosition - ouiChapterPanel.ClosePosition) * Ease.CubeOut(_003Cp_003E5__2);
				_003Cp_003E5__2 += Engine.DeltaTime * 4f;
				break;
			}
			if (_003Cp_003E5__2 < 1f)
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			ouiChapterPanel.Position = ouiChapterPanel.OpenPosition;
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class _003CLeave_003Ed__53 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public OuiChapterPanel _003C_003E4__this;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		[DebuggerHidden]
		public _003CLeave_003Ed__53(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			int num = _003C_003E1__state;
			OuiChapterPanel ouiChapterPanel = _003C_003E4__this;
			if (num != 0)
			{
				return false;
			}
			_003C_003E1__state = -1;
			ouiChapterPanel.Overworld.Mountain.EaseCamera(ouiChapterPanel.Area.ID, ouiChapterPanel.Data.MountainIdle);
			ouiChapterPanel.Add(new Coroutine(ouiChapterPanel.EaseOut()));
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class _003CEaseOut_003Ed__54 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public OuiChapterPanel _003C_003E4__this;

		private float _003Cp_003E5__2;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		[DebuggerHidden]
		public _003CEaseOut_003Ed__54(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			int num = _003C_003E1__state;
			OuiChapterPanel ouiChapterPanel = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Cp_003E5__2 = 0f;
				break;
			case 1:
				_003C_003E1__state = -1;
				_003Cp_003E5__2 += Engine.DeltaTime * 4f;
				break;
			}
			if (_003Cp_003E5__2 < 1f)
			{
				ouiChapterPanel.Position = ouiChapterPanel.OpenPosition + (ouiChapterPanel.ClosePosition - ouiChapterPanel.OpenPosition) * Ease.CubeIn(_003Cp_003E5__2);
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			if (!ouiChapterPanel.Selected)
			{
				ouiChapterPanel.Visible = false;
			}
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class _003CStartRoutine_003Ed__56 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public OuiChapterPanel _003C_003E4__this;

		public string checkpoint;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		[DebuggerHidden]
		public _003CStartRoutine_003Ed__56(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			int num = _003C_003E1__state;
			OuiChapterPanel ouiChapterPanel = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				ouiChapterPanel.EnteringChapter = true;
				ouiChapterPanel.Overworld.Maddy.Hide(down: false);
				ouiChapterPanel.Overworld.Mountain.EaseCamera(ouiChapterPanel.Area.ID, ouiChapterPanel.Data.MountainZoom, 1f);
				ouiChapterPanel.Add(new Coroutine(ouiChapterPanel.EaseOut(removeChildren: false)));
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				ScreenWipe.WipeColor = Color.Black;
				AreaData.Get(ouiChapterPanel.Area).Wipe(ouiChapterPanel.Overworld, arg2: false, null);
				Audio.SetMusic(null);
				Audio.SetAmbience(null);
				if ((ouiChapterPanel.Area.ID == 0 || ouiChapterPanel.Area.ID == 9) && checkpoint == null && ouiChapterPanel.Area.Mode == AreaMode.Normal)
				{
					ouiChapterPanel.Overworld.RendererList.UpdateLists();
					ouiChapterPanel.Overworld.RendererList.MoveToFront(ouiChapterPanel.Overworld.Snow);
				}
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				LevelEnter.Go(new Session(ouiChapterPanel.Area, checkpoint), fromSaveData: false);
				return false;
			}
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class _003CSwapRoutine_003Ed__58 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public OuiChapterPanel _003C_003E4__this;

		private float _003CfromHeight_003E5__2;

		private int _003CtoHeight_003E5__3;

		private float _003Coffset_003E5__4;

		private float _003Cp_003E5__5;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		[DebuggerHidden]
		public _003CSwapRoutine_003Ed__58(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			int num = _003C_003E1__state;
			OuiChapterPanel ouiChapterPanel = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003CfromHeight_003E5__2 = ouiChapterPanel.height;
				_003CtoHeight_003E5__3 = (ouiChapterPanel.selectingMode ? 730 : ouiChapterPanel.GetModeHeight());
				ouiChapterPanel.resizing = true;
				ouiChapterPanel.PlayExpandSfx(_003CfromHeight_003E5__2, _003CtoHeight_003E5__3);
				_003Coffset_003E5__4 = 800f;
				_003Cp_003E5__5 = 0f;
				goto IL_010a;
			case 1:
				_003C_003E1__state = -1;
				ouiChapterPanel.contentOffset.X = 440f + _003Coffset_003E5__4 * Ease.CubeIn(_003Cp_003E5__5);
				ouiChapterPanel.height = MathHelper.Lerp(_003CfromHeight_003E5__2, _003CtoHeight_003E5__3, Ease.CubeOut(_003Cp_003E5__5 * 0.5f));
				_003Cp_003E5__5 += Engine.DeltaTime * 4f;
				goto IL_010a;
			case 2:
				{
					_003C_003E1__state = -1;
					ouiChapterPanel.height = MathHelper.Lerp(_003CfromHeight_003E5__2, _003CtoHeight_003E5__3, Ease.CubeOut(Math.Min(1f, 0.5f + _003Cp_003E5__5 * 0.5f)));
					ouiChapterPanel.contentOffset.X = 440f + _003Coffset_003E5__4 * (1f - Ease.CubeOut(_003Cp_003E5__5));
					_003Cp_003E5__5 += Engine.DeltaTime * 4f;
					break;
				}
				IL_010a:
				if (_003Cp_003E5__5 < 1f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				ouiChapterPanel.selectingMode = !ouiChapterPanel.selectingMode;
				if (!ouiChapterPanel.selectingMode)
				{
					HashSet<string> checkpoints = SaveData.Instance.GetCheckpoints(ouiChapterPanel.Area);
					int siblings = checkpoints.Count + 1;
					ouiChapterPanel.checkpoints.Clear();
					ouiChapterPanel.checkpoints.Add(new Option
					{
						Label = Dialog.Clean("overworld_start"),
						BgColor = Calc.HexToColor("eabe26"),
						Icon = GFX.Gui["areaselect/startpoint"],
						CheckpointLevelName = null,
						CheckpointRotation = (float)Calc.Random.Choose(-1, 1) * Calc.Random.Range(0.05f, 0.2f),
						CheckpointOffset = new Vector2(Calc.Random.Range(-16, 16), Calc.Random.Range(-16, 16)),
						Large = false,
						Siblings = siblings
					});
					foreach (string item in checkpoints)
					{
						ouiChapterPanel.checkpoints.Add(new Option
						{
							Label = AreaData.GetCheckpointName(ouiChapterPanel.Area, item),
							Icon = GFX.Gui["areaselect/checkpoint"],
							CheckpointLevelName = item,
							CheckpointRotation = (float)Calc.Random.Choose(-1, 1) * Calc.Random.Range(0.05f, 0.2f),
							CheckpointOffset = new Vector2(Calc.Random.Range(-16, 16), Calc.Random.Range(-16, 16)),
							Large = false,
							Siblings = siblings
						});
					}
					if (!ouiChapterPanel.RealStats.Modes[(int)ouiChapterPanel.Area.Mode].Completed && !SaveData.Instance.DebugMode && !SaveData.Instance.CheatMode)
					{
						ouiChapterPanel.option = ouiChapterPanel.checkpoints.Count - 1;
						for (int i = 0; i < ouiChapterPanel.checkpoints.Count - 1; i++)
						{
							ouiChapterPanel.options[i].CheckpointSlideOut = 1f;
						}
					}
					else
					{
						ouiChapterPanel.option = 0;
					}
					for (int j = 0; j < ouiChapterPanel.options.Count; j++)
					{
						ouiChapterPanel.options[j].SlideTowards(j, ouiChapterPanel.options.Count, snap: true);
					}
				}
				ouiChapterPanel.options[ouiChapterPanel.option].Pop = 1f;
				_003Cp_003E5__5 = 0f;
				break;
			}
			if (_003Cp_003E5__5 < 1f)
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 2;
				return true;
			}
			ouiChapterPanel.contentOffset.X = 440f;
			ouiChapterPanel.height = _003CtoHeight_003E5__3;
			ouiChapterPanel.Focused = true;
			ouiChapterPanel.resizing = false;
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class _003CIncrementStatsDisplay_003Ed__65 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public bool doHeartGem;

		public OuiChapterPanel _003C_003E4__this;

		public bool doStrawberries;

		public AreaModeStats newModeStats;

		public AreaModeStats modeStats;

		public bool doDeaths;

		public bool doRemixUnlock;

		private int _003Cadd_003E5__2;

		private Option _003Co_003E5__3;

		private float _003Ct_003E5__4;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		[DebuggerHidden]
		public _003CIncrementStatsDisplay_003Ed__65(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			int num = _003C_003E1__state;
			OuiChapterPanel ouiChapterPanel = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				if (doHeartGem)
				{
					Audio.Play("event:/ui/postgame/crystal_heart");
					ouiChapterPanel.heart.Visible = true;
					ouiChapterPanel.heart.SetCurrentMode(ouiChapterPanel.Area.Mode, has: true);
					ouiChapterPanel.heart.Appear(ouiChapterPanel.Area.Mode);
					_003C_003E2__current = 1.8f;
					_003C_003E1__state = 1;
					return true;
				}
				goto IL_00c9;
			case 1:
				_003C_003E1__state = -1;
				goto IL_00c9;
			case 2:
				_003C_003E1__state = -1;
				goto IL_0188;
			case 3:
				_003C_003E1__state = -1;
				goto IL_0188;
			case 4:
				_003C_003E1__state = -1;
				modeStats.TotalStrawberries++;
				goto IL_0188;
			case 5:
				_003C_003E1__state = -1;
				if (newModeStats.Completed && !modeStats.Completed && ouiChapterPanel.Area.Mode == AreaMode.Normal)
				{
					_003C_003E2__current = 0.25f;
					_003C_003E1__state = 6;
					return true;
				}
				goto IL_02e7;
			case 6:
				_003C_003E1__state = -1;
				Audio.Play((ouiChapterPanel.strawberries.Amount >= ouiChapterPanel.Data.Mode[0].TotalStrawberries) ? "event:/ui/postgame/strawberry_total_all" : "event:/ui/postgame/strawberry_total");
				ouiChapterPanel.strawberries.OutOf = ouiChapterPanel.Data.Mode[0].TotalStrawberries;
				ouiChapterPanel.strawberries.ShowOutOf = true;
				ouiChapterPanel.strawberries.Wiggle();
				modeStats.Completed = true;
				Input.Rumble(RumbleStrength.Light, RumbleLength.Medium);
				_003C_003E2__current = 0.6f;
				_003C_003E1__state = 7;
				return true;
			case 7:
				_003C_003E1__state = -1;
				goto IL_02e7;
			case 8:
				_003C_003E1__state = -1;
				modeStats.Deaths += _003Cadd_003E5__2;
				ouiChapterPanel.deaths.Amount = modeStats.Deaths;
				if (modeStats.Deaths >= newModeStats.Deaths)
				{
					Audio.Play("event:/ui/postgame/death_final");
				}
				else
				{
					Audio.Play("event:/ui/postgame/death_count");
				}
				Input.Rumble(RumbleStrength.Light, RumbleLength.Short);
				goto IL_03bc;
			case 9:
				_003C_003E1__state = -1;
				ouiChapterPanel.deaths.CanWiggle = false;
				goto IL_0404;
			case 10:
				_003C_003E1__state = -1;
				ouiChapterPanel.spotlightPosition = _003Co_003E5__3.GetRenderPosition(ouiChapterPanel.OptionsRenderPosition);
				_003Ct_003E5__4 = 0f;
				goto IL_04fa;
			case 11:
				_003C_003E1__state = -1;
				_003Ct_003E5__4 += Engine.DeltaTime / 0.5f;
				goto IL_04fa;
			case 12:
				_003C_003E1__state = -1;
				goto IL_0542;
			case 13:
				_003C_003E1__state = -1;
				goto IL_0542;
			case 14:
				_003C_003E1__state = -1;
				_003Ct_003E5__4 = 0f;
				goto IL_06a3;
			case 15:
				{
					_003C_003E1__state = -1;
					_003Ct_003E5__4 += Engine.DeltaTime / 0.5f;
					goto IL_06a3;
				}
				IL_06a3:
				if (_003Ct_003E5__4 < 1f)
				{
					ouiChapterPanel.spotlightAlpha = (1f - _003Ct_003E5__4) * 0.5f;
					ouiChapterPanel.spotlightRadius = 128f + 128f * Ease.CubeOut(_003Ct_003E5__4);
					ouiChapterPanel.remixUnlockText.Alpha = 1f - Ease.CubeOut(_003Ct_003E5__4);
					_003C_003E2__current = null;
					_003C_003E1__state = 15;
					return true;
				}
				ouiChapterPanel.remixUnlockText.RemoveSelf();
				ouiChapterPanel.remixUnlockText = null;
				_003Co_003E5__3.Appeared = false;
				_003Co_003E5__3 = null;
				break;
				IL_01b8:
				if (newModeStats.TotalStrawberries > modeStats.TotalStrawberries)
				{
					int num2 = newModeStats.TotalStrawberries - modeStats.TotalStrawberries;
					if (num2 < 3)
					{
						_003C_003E2__current = 0.3f;
						_003C_003E1__state = 2;
						return true;
					}
					if (num2 < 8)
					{
						_003C_003E2__current = 0.2f;
						_003C_003E1__state = 3;
						return true;
					}
					_003C_003E2__current = 0.1f;
					_003C_003E1__state = 4;
					return true;
				}
				ouiChapterPanel.strawberries.CanWiggle = false;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 5;
				return true;
				IL_03bc:
				if (newModeStats.Deaths > modeStats.Deaths)
				{
					_003C_003E2__current = ouiChapterPanel.HandleDeathTick(modeStats.Deaths, newModeStats.Deaths, out _003Cadd_003E5__2);
					_003C_003E1__state = 8;
					return true;
				}
				_003C_003E2__current = 0.8f;
				_003C_003E1__state = 9;
				return true;
				IL_0542:
				if ((_003Co_003E5__3.IconEase += Engine.DeltaTime * 2f) < 1f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 13;
					return true;
				}
				_003Co_003E5__3.IconEase = 1f;
				ouiChapterPanel.modeAppearWiggler.Start();
				Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
				ouiChapterPanel.remixUnlockText = new AreaCompleteTitle(ouiChapterPanel.spotlightPosition + new Vector2(0f, 80f), Dialog.Clean("OVERWORLD_REMIX_UNLOCKED"), 1f);
				ouiChapterPanel.remixUnlockText.Tag = Tags.HUD;
				ouiChapterPanel.Overworld.Add(ouiChapterPanel.remixUnlockText);
				_003C_003E2__current = 1.5f;
				_003C_003E1__state = 14;
				return true;
				IL_0404:
				if (doRemixUnlock)
				{
					ouiChapterPanel.bSideUnlockSfx = Audio.Play("event:/ui/postgame/unlock_bside");
					_003Co_003E5__3 = ouiChapterPanel.AddRemixButton();
					_003Co_003E5__3.Appear = 0f;
					_003Co_003E5__3.IconEase = 0f;
					_003Co_003E5__3.Appeared = true;
					_003C_003E2__current = 0.5f;
					_003C_003E1__state = 10;
					return true;
				}
				break;
				IL_04fa:
				if (_003Ct_003E5__4 < 1f)
				{
					ouiChapterPanel.spotlightAlpha = _003Ct_003E5__4 * 0.9f;
					ouiChapterPanel.spotlightRadius = 128f * Ease.CubeOut(_003Ct_003E5__4);
					_003C_003E2__current = null;
					_003C_003E1__state = 11;
					return true;
				}
				_003C_003E2__current = 0.3f;
				_003C_003E1__state = 12;
				return true;
				IL_0188:
				modeStats.TotalStrawberries++;
				ouiChapterPanel.strawberries.Amount = modeStats.TotalStrawberries;
				Input.Rumble(RumbleStrength.Light, RumbleLength.Short);
				goto IL_01b8;
				IL_00c9:
				if (doStrawberries)
				{
					ouiChapterPanel.strawberries.CanWiggle = true;
					ouiChapterPanel.strawberries.Visible = true;
					goto IL_01b8;
				}
				goto IL_02e7;
				IL_02e7:
				if (doDeaths)
				{
					Audio.Play("event:/ui/postgame/death_appear");
					ouiChapterPanel.deaths.CanWiggle = true;
					ouiChapterPanel.deaths.Visible = true;
					goto IL_03bc;
				}
				goto IL_0404;
			}
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class _003CIncrementStats_003Ed__66 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public OuiChapterPanel _003C_003E4__this;

		public bool shouldAdvance;

		private AreaModeStats _003CmodeStats_003E5__2;

		private AreaModeStats _003CnewModeStats_003E5__3;

		private bool _003CdoStrawberries_003E5__4;

		private bool _003CdoHeartGem_003E5__5;

		private bool _003CdoDeaths_003E5__6;

		private bool _003CdoRemixUnlock_003E5__7;

		private bool _003Cskipped_003E5__8;

		private Coroutine _003Croutine_003E5__9;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		[DebuggerHidden]
		public _003CIncrementStats_003Ed__66(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			int num = _003C_003E1__state;
			OuiChapterPanel ouiChapterPanel = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				ouiChapterPanel.Focused = false;
				ouiChapterPanel.Overworld.ShowInputUI = false;
				if (ouiChapterPanel.Data.Interlude)
				{
					if (shouldAdvance && ouiChapterPanel.OverworldStartMode == Overworld.StartMode.AreaComplete)
					{
						_003C_003E2__current = 1.2f;
						_003C_003E1__state = 1;
						return true;
					}
					ouiChapterPanel.Focused = true;
					goto IL_00a7;
				}
				AreaData data = ouiChapterPanel.Data;
				AreaStats displayedStats = ouiChapterPanel.DisplayedStats;
				AreaStats areaStats = SaveData.Instance.Areas[data.ID];
				_003CmodeStats_003E5__2 = displayedStats.Modes[(int)ouiChapterPanel.Area.Mode];
				_003CnewModeStats_003E5__3 = areaStats.Modes[(int)ouiChapterPanel.Area.Mode];
				_003CdoStrawberries_003E5__4 = _003CnewModeStats_003E5__3.TotalStrawberries > _003CmodeStats_003E5__2.TotalStrawberries;
				_003CdoHeartGem_003E5__5 = _003CnewModeStats_003E5__3.HeartGem && !_003CmodeStats_003E5__2.HeartGem;
				_003CdoDeaths_003E5__6 = _003CnewModeStats_003E5__3.Deaths > _003CmodeStats_003E5__2.Deaths && (ouiChapterPanel.Area.Mode != AreaMode.Normal || _003CnewModeStats_003E5__3.Completed);
				_003CdoRemixUnlock_003E5__7 = ouiChapterPanel.Area.Mode == AreaMode.Normal && data.HasMode(AreaMode.BSide) && areaStats.Cassette && !displayedStats.Cassette;
				if (_003CdoStrawberries_003E5__4 | _003CdoHeartGem_003E5__5 | _003CdoDeaths_003E5__6 | _003CdoRemixUnlock_003E5__7)
				{
					_003C_003E2__current = 0.8f;
					_003C_003E1__state = 3;
					return true;
				}
				goto IL_0202;
			}
			case 1:
				_003C_003E1__state = -1;
				ouiChapterPanel.Overworld.Goto<OuiChapterSelect>().AdvanceToNext();
				goto IL_00a7;
			case 2:
				_003C_003E1__state = -1;
				return false;
			case 3:
				_003C_003E1__state = -1;
				goto IL_0202;
			case 4:
				_003C_003E1__state = -1;
				goto IL_02d0;
			case 5:
				_003C_003E1__state = -1;
				goto IL_02d0;
			case 6:
				_003C_003E1__state = -1;
				_003Croutine_003E5__9 = null;
				if (shouldAdvance && ouiChapterPanel.OverworldStartMode == Overworld.StartMode.AreaComplete)
				{
					if ((!_003CdoDeaths_003E5__6 && !_003CdoStrawberries_003E5__4 && !_003CdoHeartGem_003E5__5) || Settings.Instance.SpeedrunClock != SpeedrunType.Off)
					{
						_003C_003E2__current = 1.2f;
						_003C_003E1__state = 7;
						return true;
					}
					goto IL_04a0;
				}
				ouiChapterPanel.Focused = true;
				ouiChapterPanel.Overworld.ShowInputUI = true;
				break;
			case 7:
				{
					_003C_003E1__state = -1;
					goto IL_04a0;
				}
				IL_00a7:
				_003C_003E2__current = null;
				_003C_003E1__state = 2;
				return true;
				IL_04a0:
				ouiChapterPanel.Overworld.Goto<OuiChapterSelect>().AdvanceToNext();
				break;
				IL_0202:
				_003Cskipped_003E5__8 = false;
				_003Croutine_003E5__9 = new Coroutine(ouiChapterPanel.IncrementStatsDisplay(_003CmodeStats_003E5__2, _003CnewModeStats_003E5__3, _003CdoHeartGem_003E5__5, _003CdoStrawberries_003E5__4, _003CdoDeaths_003E5__6, _003CdoRemixUnlock_003E5__7));
				ouiChapterPanel.Add(_003Croutine_003E5__9);
				_003C_003E2__current = null;
				_003C_003E1__state = 4;
				return true;
				IL_02d0:
				if (!_003Croutine_003E5__9.Finished)
				{
					if (!MInput.GamePads[0].Pressed(Buttons.Start) && !MInput.Keyboard.Pressed(Keys.Enter))
					{
						_003C_003E2__current = null;
						_003C_003E1__state = 5;
						return true;
					}
					_003Croutine_003E5__9.Active = false;
					_003Croutine_003E5__9.RemoveSelf();
					_003Cskipped_003E5__8 = true;
					Audio.Stop(ouiChapterPanel.bSideUnlockSfx);
					Audio.Play("event:/new_content/ui/skip_all");
				}
				if (_003Cskipped_003E5__8 & _003CdoRemixUnlock_003E5__7)
				{
					ouiChapterPanel.spotlightAlpha = 0f;
					ouiChapterPanel.spotlightRadius = 0f;
					if (ouiChapterPanel.remixUnlockText != null)
					{
						ouiChapterPanel.remixUnlockText.RemoveSelf();
						ouiChapterPanel.remixUnlockText = null;
					}
					if (ouiChapterPanel.modes.Count <= 1 || ouiChapterPanel.modes[1].ID != "B")
					{
						ouiChapterPanel.AddRemixButton();
					}
					else
					{
						Option option = ouiChapterPanel.modes[1];
						option.IconEase = 1f;
						option.Appear = 1f;
						option.Appeared = false;
					}
				}
				ouiChapterPanel.DisplayedStats = ouiChapterPanel.RealStats;
				if (_003Cskipped_003E5__8)
				{
					_003CdoStrawberries_003E5__4 = _003CdoStrawberries_003E5__4 && _003CmodeStats_003E5__2.TotalStrawberries != _003CnewModeStats_003E5__3.TotalStrawberries;
					_003CdoDeaths_003E5__6 &= _003CmodeStats_003E5__2.Deaths != _003CnewModeStats_003E5__3.Deaths;
					_003CdoHeartGem_003E5__5 = _003CdoHeartGem_003E5__5 && !ouiChapterPanel.heart.Visible;
					ouiChapterPanel.UpdateStats(wiggle: true, _003CdoStrawberries_003E5__4, _003CdoDeaths_003E5__6, _003CdoHeartGem_003E5__5);
				}
				_003C_003E2__current = null;
				_003C_003E1__state = 6;
				return true;
			}
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	public AreaKey Area;

	public AreaStats RealStats;

	public AreaStats DisplayedStats;

	public AreaData Data;

	public Overworld.StartMode OverworldStartMode;

	public bool EnteringChapter;

	public const int ContentOffsetX = 440;

	public const int NoStatsHeight = 300;

	public const int StatsHeight = 540;

	public const int CheckpointsHeight = 730;

	private bool initialized;

	private string chapter = "";

	private bool selectingMode = true;

	private float height;

	private bool resizing;

	private Wiggler wiggler;

	private Wiggler modeAppearWiggler;

	private MTexture card = new MTexture();

	private Vector2 contentOffset;

	private float spotlightRadius;

	private float spotlightAlpha;

	private Vector2 spotlightPosition;

	private AreaCompleteTitle remixUnlockText;

	private StrawberriesCounter strawberries = new StrawberriesCounter(centeredX: true, 0, 0, showOutOf: true);

	private Vector2 strawberriesOffset;

	private DeathsCounter deaths = new DeathsCounter(AreaMode.Normal, centeredX: true, 0);

	private Vector2 deathsOffset;

	private HeartGemDisplay heart = new HeartGemDisplay(0, hasGem: false);

	private Vector2 heartOffset;

	private int checkpoint;

	private List<Option> modes = new List<Option>();

	private List<Option> checkpoints = new List<Option>();

	private FMOD.Studio.EventInstance bSideUnlockSfx;

	public Vector2 OpenPosition => new Vector2(1070f, 100f);

	public Vector2 ClosePosition => new Vector2(2220f, 100f);

	public Vector2 IconOffset => new Vector2(690f, 86f);

	private Vector2 OptionsRenderPosition => Position + new Vector2(contentOffset.X, 128f + height);

	private int option
	{
		get
		{
			if (!selectingMode)
			{
				return checkpoint;
			}
			return (int)Area.Mode;
		}
		set
		{
			if (selectingMode)
			{
				Area.Mode = (AreaMode)value;
			}
			else
			{
				checkpoint = value;
			}
		}
	}

	private List<Option> options
	{
		get
		{
			if (!selectingMode)
			{
				return checkpoints;
			}
			return modes;
		}
	}

	public OuiChapterPanel()
	{
		Add(strawberries);
		Add(deaths);
		Add(heart);
		deaths.CanWiggle = false;
		strawberries.CanWiggle = false;
		strawberries.OverworldSfx = true;
		Add(wiggler = Wiggler.Create(0.4f, 4f));
		Add(modeAppearWiggler = Wiggler.Create(0.4f, 4f));
	}

	public override bool IsStart(Overworld overworld, Overworld.StartMode start)
	{
		if (start == Overworld.StartMode.AreaComplete || start == Overworld.StartMode.AreaQuit)
		{
			bool shouldAdvance = start == Overworld.StartMode.AreaComplete && (Celeste.PlayMode == Celeste.PlayModes.Event || (SaveData.Instance.CurrentSession != null && SaveData.Instance.CurrentSession.ShouldAdvance));
			Position = OpenPosition;
			Reset();
			Add(new Coroutine(IncrementStats(shouldAdvance)));
			overworld.ShowInputUI = false;
			overworld.Mountain.SnapState(Data.MountainState);
			overworld.Mountain.SnapCamera(Area.ID, Data.MountainZoom);
			overworld.Mountain.EaseCamera(Area.ID, Data.MountainSelect, 1f);
			OverworldStartMode = start;
			return true;
		}
		Position = ClosePosition;
		return false;
	}

	[IteratorStateMachine(typeof(_003CEnter_003Ed__49))]
	public override IEnumerator Enter(Oui from)
	{
		Visible = true;
		Area.Mode = AreaMode.Normal;
		Reset();
		base.Overworld.Mountain.EaseCamera(Area.ID, Data.MountainSelect);
		for (float p = 0f; p < 1f; p += Engine.DeltaTime * 4f)
		{
			yield return null;
			Position = ClosePosition + (OpenPosition - ClosePosition) * Ease.CubeOut(p);
		}
		Position = OpenPosition;
	}

	private void Reset()
	{
		Area = SaveData.Instance.LastArea;
		Data = AreaData.Areas[Area.ID];
		RealStats = SaveData.Instance.Areas[Area.ID];
		if (SaveData.Instance.CurrentSession != null && SaveData.Instance.CurrentSession.OldStats != null && SaveData.Instance.CurrentSession.Area.ID == Area.ID)
		{
			DisplayedStats = SaveData.Instance.CurrentSession.OldStats;
			SaveData.Instance.CurrentSession = null;
		}
		else
		{
			DisplayedStats = RealStats;
		}
		height = GetModeHeight();
		modes.Clear();
		bool flag = false;
		if (!Data.Interlude && Data.HasMode(AreaMode.BSide) && (DisplayedStats.Cassette || ((SaveData.Instance.DebugMode || SaveData.Instance.CheatMode) && DisplayedStats.Cassette == RealStats.Cassette)))
		{
			flag = true;
		}
		bool num = !Data.Interlude && Data.HasMode(AreaMode.CSide) && SaveData.Instance.UnlockedModes >= 3 && Celeste.PlayMode != Celeste.PlayModes.Event;
		modes.Add(new Option
		{
			Label = Dialog.Clean(Data.Interlude ? "FILE_BEGIN" : "overworld_normal").ToUpper(),
			Icon = GFX.Gui["menu/play"],
			ID = "A"
		});
		if (flag)
		{
			AddRemixButton();
		}
		if (num)
		{
			modes.Add(new Option
			{
				Label = Dialog.Clean("overworld_remix2"),
				Icon = GFX.Gui["menu/rmx2"],
				ID = "C"
			});
		}
		selectingMode = true;
		UpdateStats(wiggle: false);
		SetStatsPosition(approach: false);
		for (int i = 0; i < options.Count; i++)
		{
			options[i].SlideTowards(i, options.Count, snap: true);
		}
		chapter = Dialog.Get("area_chapter").Replace("{x}", Area.ChapterIndex.ToString().PadLeft(2));
		contentOffset = new Vector2(440f, 120f);
		initialized = true;
	}

	private int GetModeHeight()
	{
		AreaModeStats areaModeStats = RealStats.Modes[(int)Area.Mode];
		bool flag = areaModeStats.Strawberries.Count <= 0;
		if (!Data.Interlude && ((areaModeStats.Deaths > 0 && Area.Mode != AreaMode.Normal) || areaModeStats.Completed || areaModeStats.HeartGem))
		{
			flag = false;
		}
		if (!flag)
		{
			return 540;
		}
		return 300;
	}

	private Option AddRemixButton()
	{
		Option option = new Option
		{
			Label = Dialog.Clean("overworld_remix"),
			Icon = GFX.Gui["menu/remix"],
			ID = "B"
		};
		modes.Insert(1, option);
		return option;
	}

	[IteratorStateMachine(typeof(_003CLeave_003Ed__53))]
	public override IEnumerator Leave(Oui next)
	{
		base.Overworld.Mountain.EaseCamera(Area.ID, Data.MountainIdle);
		Add(new Coroutine(EaseOut()));
		yield break;
	}

	[IteratorStateMachine(typeof(_003CEaseOut_003Ed__54))]
	public IEnumerator EaseOut(bool removeChildren = true)
	{
		for (float p = 0f; p < 1f; p += Engine.DeltaTime * 4f)
		{
			Position = OpenPosition + (ClosePosition - OpenPosition) * Ease.CubeIn(p);
			yield return null;
		}
		if (!base.Selected)
		{
			Visible = false;
		}
	}

	public void Start(string checkpoint = null)
	{
		Focused = false;
		Audio.Play("event:/ui/world_map/chapter/checkpoint_start");
		Add(new Coroutine(StartRoutine(checkpoint)));
	}

	[IteratorStateMachine(typeof(_003CStartRoutine_003Ed__56))]
	private IEnumerator StartRoutine(string checkpoint = null)
	{
		EnteringChapter = true;
		base.Overworld.Maddy.Hide(down: false);
		base.Overworld.Mountain.EaseCamera(Area.ID, Data.MountainZoom, 1f);
		Add(new Coroutine(EaseOut(removeChildren: false)));
		yield return 0.2f;
		ScreenWipe.WipeColor = Color.Black;
		AreaData.Get(Area).Wipe(base.Overworld, arg2: false, null);
		Audio.SetMusic(null);
		Audio.SetAmbience(null);
		if ((Area.ID == 0 || Area.ID == 9) && checkpoint == null && Area.Mode == AreaMode.Normal)
		{
			base.Overworld.RendererList.UpdateLists();
			base.Overworld.RendererList.MoveToFront(base.Overworld.Snow);
		}
		yield return 0.5f;
		LevelEnter.Go(new Session(Area, checkpoint), fromSaveData: false);
	}

	private void Swap()
	{
		Focused = false;
		base.Overworld.ShowInputUI = !selectingMode;
		Add(new Coroutine(SwapRoutine()));
	}

	[IteratorStateMachine(typeof(_003CSwapRoutine_003Ed__58))]
	private IEnumerator SwapRoutine()
	{
		float fromHeight = height;
		int toHeight = (selectingMode ? 730 : GetModeHeight());
		resizing = true;
		PlayExpandSfx(fromHeight, toHeight);
		float offset = 800f;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime * 4f)
		{
			yield return null;
			contentOffset.X = 440f + offset * Ease.CubeIn(p);
			height = MathHelper.Lerp(fromHeight, toHeight, Ease.CubeOut(p * 0.5f));
		}
		selectingMode = !selectingMode;
		if (!selectingMode)
		{
			HashSet<string> hashSet = SaveData.Instance.GetCheckpoints(Area);
			int siblings = hashSet.Count + 1;
			checkpoints.Clear();
			checkpoints.Add(new Option
			{
				Label = Dialog.Clean("overworld_start"),
				BgColor = Calc.HexToColor("eabe26"),
				Icon = GFX.Gui["areaselect/startpoint"],
				CheckpointLevelName = null,
				CheckpointRotation = (float)Calc.Random.Choose(-1, 1) * Calc.Random.Range(0.05f, 0.2f),
				CheckpointOffset = new Vector2(Calc.Random.Range(-16, 16), Calc.Random.Range(-16, 16)),
				Large = false,
				Siblings = siblings
			});
			foreach (string item in hashSet)
			{
				checkpoints.Add(new Option
				{
					Label = AreaData.GetCheckpointName(Area, item),
					Icon = GFX.Gui["areaselect/checkpoint"],
					CheckpointLevelName = item,
					CheckpointRotation = (float)Calc.Random.Choose(-1, 1) * Calc.Random.Range(0.05f, 0.2f),
					CheckpointOffset = new Vector2(Calc.Random.Range(-16, 16), Calc.Random.Range(-16, 16)),
					Large = false,
					Siblings = siblings
				});
			}
			if (!RealStats.Modes[(int)Area.Mode].Completed && !SaveData.Instance.DebugMode && !SaveData.Instance.CheatMode)
			{
				option = checkpoints.Count - 1;
				for (int i = 0; i < checkpoints.Count - 1; i++)
				{
					options[i].CheckpointSlideOut = 1f;
				}
			}
			else
			{
				option = 0;
			}
			for (int j = 0; j < options.Count; j++)
			{
				options[j].SlideTowards(j, options.Count, snap: true);
			}
		}
		options[option].Pop = 1f;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime * 4f)
		{
			yield return null;
			height = MathHelper.Lerp(fromHeight, toHeight, Ease.CubeOut(Math.Min(1f, 0.5f + p * 0.5f)));
			contentOffset.X = 440f + offset * (1f - Ease.CubeOut(p));
		}
		contentOffset.X = 440f;
		height = toHeight;
		Focused = true;
		resizing = false;
	}

	public override void Update()
	{
		if (!initialized)
		{
			return;
		}
		base.Update();
		for (int i = 0; i < options.Count; i++)
		{
			Option option = options[i];
			option.Pop = Calc.Approach(option.Pop, (this.option == i) ? 1f : 0f, Engine.DeltaTime * 4f);
			option.Appear = Calc.Approach(option.Appear, 1f, Engine.DeltaTime * 3f);
			option.CheckpointSlideOut = Calc.Approach(option.CheckpointSlideOut, (this.option > i) ? 1 : 0, Engine.DeltaTime * 4f);
			option.Faded = Calc.Approach(option.Faded, (this.option != i && !option.Appeared) ? 1 : 0, Engine.DeltaTime * 4f);
			option.SlideTowards(i, options.Count, snap: false);
		}
		if (selectingMode && !resizing)
		{
			height = Calc.Approach(height, GetModeHeight(), Engine.DeltaTime * 1600f);
		}
		if (base.Selected && Focused)
		{
			if (Input.MenuLeft.Pressed && this.option > 0)
			{
				Audio.Play("event:/ui/world_map/chapter/tab_roll_left");
				this.option--;
				wiggler.Start();
				if (selectingMode)
				{
					UpdateStats();
					PlayExpandSfx(height, GetModeHeight());
				}
				else
				{
					Audio.Play("event:/ui/world_map/chapter/checkpoint_photo_add");
				}
			}
			else if (Input.MenuRight.Pressed && this.option + 1 < options.Count)
			{
				Audio.Play("event:/ui/world_map/chapter/tab_roll_right");
				this.option++;
				wiggler.Start();
				if (selectingMode)
				{
					UpdateStats();
					PlayExpandSfx(height, GetModeHeight());
				}
				else
				{
					Audio.Play("event:/ui/world_map/chapter/checkpoint_photo_remove");
				}
			}
			else if (Input.MenuConfirm.Pressed)
			{
				if (selectingMode)
				{
					if (!SaveData.Instance.FoundAnyCheckpoints(Area))
					{
						Start();
					}
					else
					{
						Audio.Play("event:/ui/world_map/chapter/level_select");
						Swap();
					}
				}
				else
				{
					Start(options[this.option].CheckpointLevelName);
				}
			}
			else if (Input.MenuCancel.Pressed)
			{
				if (selectingMode)
				{
					Audio.Play("event:/ui/world_map/chapter/back");
					base.Overworld.Goto<OuiChapterSelect>();
				}
				else
				{
					Audio.Play("event:/ui/world_map/chapter/checkpoint_back");
					Swap();
				}
			}
		}
		SetStatsPosition(approach: true);
	}

	public override void Render()
	{
		if (!initialized)
		{
			return;
		}
		Vector2 optionsRenderPosition = OptionsRenderPosition;
		for (int i = 0; i < options.Count; i++)
		{
			if (!options[i].OnTopOfUI)
			{
				options[i].Render(optionsRenderPosition, option == i, wiggler, modeAppearWiggler);
			}
		}
		bool flag = false;
		if (RealStats.Modes[(int)Area.Mode].Completed)
		{
			int mode = (int)Area.Mode;
			foreach (EntityData goldenberry in AreaData.Areas[Area.ID].Mode[mode].MapData.Goldenberries)
			{
				EntityID item = new EntityID(goldenberry.Level.Name, goldenberry.ID);
				if (RealStats.Modes[mode].Strawberries.Contains(item))
				{
					flag = true;
					break;
				}
			}
		}
		MTexture mTexture = GFX.Gui[(!flag) ? "areaselect/cardtop" : "areaselect/cardtop_golden"];
		mTexture.Draw(Position + new Vector2(0f, -32f));
		MTexture mTexture2 = GFX.Gui[(!flag) ? "areaselect/card" : "areaselect/card_golden"];
		card = mTexture2.GetSubtexture(0, mTexture2.Height - (int)height, mTexture2.Width, (int)height, card);
		card.Draw(Position + new Vector2(0f, -32 + mTexture.Height));
		for (int j = 0; j < options.Count; j++)
		{
			if (options[j].OnTopOfUI)
			{
				options[j].Render(optionsRenderPosition, option == j, wiggler, modeAppearWiggler);
			}
		}
		ActiveFont.Draw(options[option].Label, optionsRenderPosition + new Vector2(0f, -140f), Vector2.One * 0.5f, Vector2.One * (1f + wiggler.Value * 0.1f), Color.Black * 0.8f);
		if (selectingMode)
		{
			strawberries.Position = contentOffset + new Vector2(0f, 170f) + strawberriesOffset;
			deaths.Position = contentOffset + new Vector2(0f, 170f) + deathsOffset;
			heart.Position = contentOffset + new Vector2(0f, 170f) + heartOffset;
			base.Render();
		}
		if (!selectingMode)
		{
			Vector2 center = Position + new Vector2(contentOffset.X, 340f);
			for (int num = options.Count - 1; num >= 0; num--)
			{
				DrawCheckpoint(center, options[num], num);
			}
		}
		GFX.Gui["areaselect/title"].Draw(Position + new Vector2(-60f, 0f), Vector2.Zero, Data.TitleBaseColor);
		GFX.Gui["areaselect/accent"].Draw(Position + new Vector2(-60f, 0f), Vector2.Zero, Data.TitleAccentColor);
		string text = Dialog.Clean(AreaData.Get(Area).Name);
		if (Data.Interlude)
		{
			ActiveFont.Draw(text, Position + IconOffset + new Vector2(-100f, 0f), new Vector2(1f, 0.5f), Vector2.One * 1f, Data.TitleTextColor * 0.8f);
		}
		else
		{
			ActiveFont.Draw(chapter, Position + IconOffset + new Vector2(-100f, -2f), new Vector2(1f, 1f), Vector2.One * 0.6f, Data.TitleAccentColor * 0.8f);
			ActiveFont.Draw(text, Position + IconOffset + new Vector2(-100f, -18f), new Vector2(1f, 0f), Vector2.One * 1f, Data.TitleTextColor * 0.8f);
		}
		if (spotlightAlpha > 0f)
		{
			HiresRenderer.EndRender();
			SpotlightWipe.DrawSpotlight(spotlightPosition, spotlightRadius, Color.Black * spotlightAlpha);
			HiresRenderer.BeginRender();
		}
	}

	private void DrawCheckpoint(Vector2 center, Option option, int checkpointIndex)
	{
		MTexture checkpointPreview = GetCheckpointPreview(Area, option.CheckpointLevelName);
		MTexture mTexture = MTN.Checkpoints["polaroid"];
		float checkpointRotation = option.CheckpointRotation;
		Vector2 vector = center + option.CheckpointOffset;
		vector += Vector2.UnitX * 800f * Ease.CubeIn(option.CheckpointSlideOut);
		mTexture.DrawCentered(vector, Color.White, 0.75f, checkpointRotation);
		MTexture mTexture2 = GFX.Gui["collectables/strawberry"];
		if (checkpointPreview != null)
		{
			Vector2 scale = Vector2.One * 0.75f;
			if (SaveData.Instance.Assists.MirrorMode)
			{
				scale.X = 0f - scale.X;
			}
			scale *= 720f / (float)checkpointPreview.Width;
			HiresRenderer.EndRender();
			HiresRenderer.BeginRender(BlendState.AlphaBlend, SamplerState.PointClamp);
			checkpointPreview.DrawCentered(vector, Color.White, scale, checkpointRotation);
			HiresRenderer.EndRender();
			HiresRenderer.BeginRender();
		}
		int mode = (int)Area.Mode;
		if (!RealStats.Modes[mode].Completed && !SaveData.Instance.CheatMode && !SaveData.Instance.DebugMode)
		{
			return;
		}
		Vector2 vec = new Vector2(300f, 220f);
		vec = vector + vec.Rotate(checkpointRotation);
		int num = 0;
		num = ((checkpointIndex != 0) ? Data.Mode[mode].Checkpoints[checkpointIndex - 1].Strawberries : Data.Mode[mode].StartStrawberries);
		bool[] array = new bool[num];
		foreach (EntityID strawberry in RealStats.Modes[mode].Strawberries)
		{
			for (int i = 0; i < num; i++)
			{
				EntityData entityData = Data.Mode[mode].StrawberriesByCheckpoint[checkpointIndex, i];
				if (entityData != null && entityData.Level.Name == strawberry.Level && entityData.ID == strawberry.ID)
				{
					array[i] = true;
				}
			}
		}
		Vector2 vector2 = Calc.AngleToVector(checkpointRotation, 1f);
		Vector2 vector3 = vec - vector2 * num * 44f;
		if (Area.Mode == AreaMode.Normal && Data.CassetteCheckpointIndex == checkpointIndex)
		{
			Vector2 position = vector3 - vector2 * 60f;
			if (RealStats.Cassette)
			{
				MTN.Journal["cassette"].DrawCentered(position, Color.White, 1f, checkpointRotation);
			}
			else
			{
				MTN.Journal["cassette_outline"].DrawCentered(position, Color.DarkGray, 1f, checkpointRotation);
			}
		}
		for (int j = 0; j < num; j++)
		{
			mTexture2.DrawCentered(vector3, array[j] ? Color.White : (Color.Black * 0.3f), 0.5f, checkpointRotation);
			vector3 += vector2 * 44f;
		}
	}

	private void UpdateStats(bool wiggle = true, bool? overrideStrawberryWiggle = null, bool? overrideDeathWiggle = null, bool? overrideHeartWiggle = null)
	{
		AreaModeStats areaModeStats = DisplayedStats.Modes[(int)Area.Mode];
		AreaData areaData = AreaData.Get(Area);
		deaths.Visible = areaModeStats.Deaths > 0 && (Area.Mode != AreaMode.Normal || RealStats.Modes[(int)Area.Mode].Completed) && !AreaData.Get(Area).Interlude;
		deaths.Amount = areaModeStats.Deaths;
		deaths.SetMode(areaData.IsFinal ? AreaMode.CSide : Area.Mode);
		heart.Visible = areaModeStats.HeartGem && !areaData.Interlude && areaData.CanFullClear;
		heart.SetCurrentMode(Area.Mode, areaModeStats.HeartGem);
		strawberries.Visible = (areaModeStats.TotalStrawberries > 0 || (areaModeStats.Completed && Area.Mode == AreaMode.Normal && AreaData.Get(Area).Mode[0].TotalStrawberries > 0)) && !AreaData.Get(Area).Interlude;
		strawberries.Amount = areaModeStats.TotalStrawberries;
		strawberries.OutOf = Data.Mode[0].TotalStrawberries;
		strawberries.ShowOutOf = areaModeStats.Completed && Area.Mode == AreaMode.Normal;
		strawberries.Golden = Area.Mode != AreaMode.Normal;
		if (wiggle)
		{
			if (strawberries.Visible && (!overrideStrawberryWiggle.HasValue || overrideStrawberryWiggle.Value))
			{
				strawberries.Wiggle();
			}
			if (heart.Visible && (!overrideHeartWiggle.HasValue || overrideHeartWiggle.Value))
			{
				heart.Wiggle();
			}
			if (deaths.Visible && (!overrideDeathWiggle.HasValue || overrideDeathWiggle.Value))
			{
				deaths.Wiggle();
			}
		}
	}

	private void SetStatsPosition(bool approach)
	{
		if (heart.Visible && (strawberries.Visible || deaths.Visible))
		{
			heartOffset = Approach(heartOffset, new Vector2(-120f, 0f), !approach);
			strawberriesOffset = Approach(strawberriesOffset, new Vector2(120f, deaths.Visible ? (-40) : 0), !approach);
			deathsOffset = Approach(deathsOffset, new Vector2(120f, strawberries.Visible ? 40 : 0), !approach);
		}
		else if (heart.Visible)
		{
			heartOffset = Approach(heartOffset, Vector2.Zero, !approach);
		}
		else
		{
			strawberriesOffset = Approach(strawberriesOffset, new Vector2(0f, deaths.Visible ? (-40) : 0), !approach);
			deathsOffset = Approach(deathsOffset, new Vector2(0f, strawberries.Visible ? 40 : 0), !approach);
		}
	}

	private Vector2 Approach(Vector2 from, Vector2 to, bool snap)
	{
		if (snap)
		{
			return to;
		}
		return from += (to - from) * (1f - (float)Math.Pow(0.0010000000474974513, Engine.DeltaTime));
	}

	[IteratorStateMachine(typeof(_003CIncrementStatsDisplay_003Ed__65))]
	private IEnumerator IncrementStatsDisplay(AreaModeStats modeStats, AreaModeStats newModeStats, bool doHeartGem, bool doStrawberries, bool doDeaths, bool doRemixUnlock)
	{
		if (doHeartGem)
		{
			Audio.Play("event:/ui/postgame/crystal_heart");
			heart.Visible = true;
			heart.SetCurrentMode(Area.Mode, has: true);
			heart.Appear(Area.Mode);
			yield return 1.8f;
		}
		if (doStrawberries)
		{
			strawberries.CanWiggle = true;
			strawberries.Visible = true;
			while (newModeStats.TotalStrawberries > modeStats.TotalStrawberries)
			{
				int num = newModeStats.TotalStrawberries - modeStats.TotalStrawberries;
				if (num < 3)
				{
					yield return 0.3f;
				}
				else if (num < 8)
				{
					yield return 0.2f;
				}
				else
				{
					yield return 0.1f;
					modeStats.TotalStrawberries++;
				}
				modeStats.TotalStrawberries++;
				strawberries.Amount = modeStats.TotalStrawberries;
				Input.Rumble(RumbleStrength.Light, RumbleLength.Short);
			}
			strawberries.CanWiggle = false;
			yield return 0.5f;
			if (newModeStats.Completed && !modeStats.Completed && Area.Mode == AreaMode.Normal)
			{
				yield return 0.25f;
				Audio.Play((strawberries.Amount >= Data.Mode[0].TotalStrawberries) ? "event:/ui/postgame/strawberry_total_all" : "event:/ui/postgame/strawberry_total");
				strawberries.OutOf = Data.Mode[0].TotalStrawberries;
				strawberries.ShowOutOf = true;
				strawberries.Wiggle();
				modeStats.Completed = true;
				Input.Rumble(RumbleStrength.Light, RumbleLength.Medium);
				yield return 0.6f;
			}
		}
		if (doDeaths)
		{
			Audio.Play("event:/ui/postgame/death_appear");
			deaths.CanWiggle = true;
			deaths.Visible = true;
			while (newModeStats.Deaths > modeStats.Deaths)
			{
				int add;
				yield return HandleDeathTick(modeStats.Deaths, newModeStats.Deaths, out add);
				modeStats.Deaths += add;
				deaths.Amount = modeStats.Deaths;
				if (modeStats.Deaths >= newModeStats.Deaths)
				{
					Audio.Play("event:/ui/postgame/death_final");
				}
				else
				{
					Audio.Play("event:/ui/postgame/death_count");
				}
				Input.Rumble(RumbleStrength.Light, RumbleLength.Short);
			}
			yield return 0.8f;
			deaths.CanWiggle = false;
		}
		if (doRemixUnlock)
		{
			bSideUnlockSfx = Audio.Play("event:/ui/postgame/unlock_bside");
			Option o = AddRemixButton();
			o.Appear = 0f;
			o.IconEase = 0f;
			o.Appeared = true;
			yield return 0.5f;
			spotlightPosition = o.GetRenderPosition(OptionsRenderPosition);
			for (float t = 0f; t < 1f; t += Engine.DeltaTime / 0.5f)
			{
				spotlightAlpha = t * 0.9f;
				spotlightRadius = 128f * Ease.CubeOut(t);
				yield return null;
			}
			yield return 0.3f;
			while ((o.IconEase += Engine.DeltaTime * 2f) < 1f)
			{
				yield return null;
			}
			o.IconEase = 1f;
			modeAppearWiggler.Start();
			Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
			remixUnlockText = new AreaCompleteTitle(spotlightPosition + new Vector2(0f, 80f), Dialog.Clean("OVERWORLD_REMIX_UNLOCKED"), 1f);
			remixUnlockText.Tag = Tags.HUD;
			base.Overworld.Add(remixUnlockText);
			yield return 1.5f;
			for (float t = 0f; t < 1f; t += Engine.DeltaTime / 0.5f)
			{
				spotlightAlpha = (1f - t) * 0.5f;
				spotlightRadius = 128f + 128f * Ease.CubeOut(t);
				remixUnlockText.Alpha = 1f - Ease.CubeOut(t);
				yield return null;
			}
			remixUnlockText.RemoveSelf();
			remixUnlockText = null;
			o.Appeared = false;
		}
	}

	[IteratorStateMachine(typeof(_003CIncrementStats_003Ed__66))]
	public IEnumerator IncrementStats(bool shouldAdvance)
	{
		Focused = false;
		base.Overworld.ShowInputUI = false;
		if (Data.Interlude)
		{
			if (shouldAdvance && OverworldStartMode == Overworld.StartMode.AreaComplete)
			{
				yield return 1.2f;
				base.Overworld.Goto<OuiChapterSelect>().AdvanceToNext();
			}
			else
			{
				Focused = true;
			}
			yield return null;
			yield break;
		}
		AreaData data = Data;
		AreaStats displayedStats = DisplayedStats;
		AreaStats areaStats = SaveData.Instance.Areas[data.ID];
		AreaModeStats modeStats = displayedStats.Modes[(int)Area.Mode];
		AreaModeStats newModeStats = areaStats.Modes[(int)Area.Mode];
		bool doStrawberries = newModeStats.TotalStrawberries > modeStats.TotalStrawberries;
		bool doHeartGem = newModeStats.HeartGem && !modeStats.HeartGem;
		bool doDeaths = newModeStats.Deaths > modeStats.Deaths && (Area.Mode != AreaMode.Normal || newModeStats.Completed);
		bool doRemixUnlock = Area.Mode == AreaMode.Normal && data.HasMode(AreaMode.BSide) && areaStats.Cassette && !displayedStats.Cassette;
		if (doStrawberries || doHeartGem || doDeaths || doRemixUnlock)
		{
			yield return 0.8f;
		}
		bool skipped = false;
		Coroutine routine = new Coroutine(IncrementStatsDisplay(modeStats, newModeStats, doHeartGem, doStrawberries, doDeaths, doRemixUnlock));
		Add(routine);
		yield return null;
		while (!routine.Finished)
		{
			if (MInput.GamePads[0].Pressed(Buttons.Start) || MInput.Keyboard.Pressed(Keys.Enter))
			{
				routine.Active = false;
				routine.RemoveSelf();
				skipped = true;
				Audio.Stop(bSideUnlockSfx);
				Audio.Play("event:/new_content/ui/skip_all");
				break;
			}
			yield return null;
		}
		if (skipped && doRemixUnlock)
		{
			spotlightAlpha = 0f;
			spotlightRadius = 0f;
			if (remixUnlockText != null)
			{
				remixUnlockText.RemoveSelf();
				remixUnlockText = null;
			}
			if (modes.Count <= 1 || modes[1].ID != "B")
			{
				AddRemixButton();
			}
			else
			{
				Option obj = modes[1];
				obj.IconEase = 1f;
				obj.Appear = 1f;
				obj.Appeared = false;
			}
		}
		DisplayedStats = RealStats;
		if (skipped)
		{
			doStrawberries = doStrawberries && modeStats.TotalStrawberries != newModeStats.TotalStrawberries;
			doDeaths &= modeStats.Deaths != newModeStats.Deaths;
			doHeartGem = doHeartGem && !heart.Visible;
			UpdateStats(wiggle: true, doStrawberries, doDeaths, doHeartGem);
		}
		yield return null;
		if (shouldAdvance && OverworldStartMode == Overworld.StartMode.AreaComplete)
		{
			if ((!doDeaths && !doStrawberries && !doHeartGem) || Settings.Instance.SpeedrunClock != SpeedrunType.Off)
			{
				yield return 1.2f;
			}
			base.Overworld.Goto<OuiChapterSelect>().AdvanceToNext();
		}
		else
		{
			Focused = true;
			base.Overworld.ShowInputUI = true;
		}
	}

	private float HandleDeathTick(int oldDeaths, int newDeaths, out int add)
	{
		int num = newDeaths - oldDeaths;
		if (num < 3)
		{
			add = 1;
			return 0.3f;
		}
		if (num < 8)
		{
			add = 2;
			return 0.2f;
		}
		if (num < 30)
		{
			add = 5;
			return 0.1f;
		}
		if (num < 100)
		{
			add = 10;
			return 0.1f;
		}
		if (num < 1000)
		{
			add = 25;
			return 0.1f;
		}
		add = 100;
		return 0.1f;
	}

	private void PlayExpandSfx(float currentHeight, float nextHeight)
	{
		if (nextHeight > currentHeight)
		{
			Audio.Play("event:/ui/world_map/chapter/pane_expand");
		}
		else if (nextHeight < currentHeight)
		{
			Audio.Play("event:/ui/world_map/chapter/pane_contract");
		}
	}

	public static string GetCheckpointPreviewName(AreaKey area, string level)
	{
		if (level == null)
		{
			return area.ToString();
		}
		return area.ToString() + "_" + level;
	}

	private MTexture GetCheckpointPreview(AreaKey area, string level)
	{
		string checkpointPreviewName = GetCheckpointPreviewName(area, level);
		if (MTN.Checkpoints.Has(checkpointPreviewName))
		{
			return MTN.Checkpoints[checkpointPreviewName];
		}
		return null;
	}
}
