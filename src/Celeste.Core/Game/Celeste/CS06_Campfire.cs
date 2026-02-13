using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Xml;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS06_Campfire : CutsceneEntity
{
	private class Option
	{
		public Question Question;

		public string Goto;

		public List<Question> OnlyAppearIfAsked;

		public List<Question> DoNotAppearIfAsked;

		public bool CanRepeat;

		public float Highlight;

		public const float Width = 1400f;

		public const float Height = 140f;

		public const float Padding = 20f;

		public const float TextScale = 0.7f;

		public Option(Question question, string go)
		{
			Question = question;
			Goto = go;
		}

		public Option Require(params Question[] onlyAppearIfAsked)
		{
			OnlyAppearIfAsked = new List<Question>(onlyAppearIfAsked);
			return this;
		}

		public Option ExcludedBy(params Question[] doNotAppearIfAsked)
		{
			DoNotAppearIfAsked = new List<Question>(doNotAppearIfAsked);
			return this;
		}

		public Option Repeatable()
		{
			CanRepeat = true;
			return this;
		}

		public bool CanAsk(HashSet<Question> asked)
		{
			if (!CanRepeat && asked.Contains(Question))
			{
				return false;
			}
			if (OnlyAppearIfAsked != null)
			{
				foreach (Question item in OnlyAppearIfAsked)
				{
					if (!asked.Contains(item))
					{
						return false;
					}
				}
			}
			if (DoNotAppearIfAsked != null)
			{
				bool flag = true;
				foreach (Question item2 in DoNotAppearIfAsked)
				{
					if (!asked.Contains(item2))
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					return false;
				}
			}
			return true;
		}

		public void Update()
		{
			Question.Portrait.Update();
		}

		public void Render(Vector2 position, float ease)
		{
			float num = Ease.CubeOut(ease);
			float num2 = Ease.CubeInOut(Highlight);
			position.Y += -32f * (1f - num);
			position.X += num2 * 32f;
			Color color = Color.Lerp(Color.Gray, Color.White, num2) * num;
			float alpha = MathHelper.Lerp(0.6f, 1f, num2) * num;
			Color color2 = Color.White * (0.5f + num2 * 0.5f);
			GFX.Portraits[Question.Textbox].Draw(position, Vector2.Zero, color);
			Facings facings = Question.PortraitSide;
			if (SaveData.Instance != null && SaveData.Instance.Assists.MirrorMode)
			{
				facings = (Facings)(0 - facings);
			}
			float num3 = 100f;
			Question.Portrait.Scale = Vector2.One * (num3 / Question.PortraitSize);
			if (facings == Facings.Right)
			{
				Question.Portrait.Position = position + new Vector2(1380f - num3 * 0.5f, 70f);
				Question.Portrait.Scale.X *= -1f;
			}
			else
			{
				Question.Portrait.Position = position + new Vector2(20f + num3 * 0.5f, 70f);
			}
			Question.Portrait.Color = color2 * num;
			Question.Portrait.Render();
			float num4 = (140f - ActiveFont.LineHeight * 0.7f) / 2f;
			Vector2 position2 = new Vector2(0f, position.Y + 70f);
			Vector2 justify = new Vector2(0f, 0.5f);
			if (facings == Facings.Right)
			{
				justify.X = 1f;
				position2.X = position.X + 1400f - 20f - num4 - num3;
			}
			else
			{
				position2.X = position.X + 20f + num4 + num3;
			}
			Question.AskText.Draw(position2, justify, Vector2.One * 0.7f, alpha);
		}
	}

	private class Question
	{
		public string Ask;

		public string Answer;

		public string Textbox;

		public FancyText.Text AskText;

		public Sprite Portrait;

		public Facings PortraitSide;

		public float PortraitSize;

		public Question(string id)
		{
			int maxLineWidth = 1828;
			Ask = "ch6_theo_ask_" + id;
			Answer = "ch6_theo_say_" + id;
			AskText = FancyText.Parse(Dialog.Get(Ask), maxLineWidth, -1);
			foreach (FancyText.Node node in AskText.Nodes)
			{
				if (node is FancyText.Portrait)
				{
					FancyText.Portrait portrait = node as FancyText.Portrait;
					Portrait = GFX.PortraitsSpriteBank.Create(portrait.SpriteId);
					Portrait.Play(portrait.IdleAnimation);
					PortraitSide = (Facings)portrait.Side;
					Textbox = "textbox/" + portrait.Sprite + "_ask";
					XmlElement xML = GFX.PortraitsSpriteBank.SpriteData[portrait.SpriteId].Sources[0].XML;
					if (xML != null)
					{
						PortraitSize = xML.AttrInt("size", 160);
					}
					break;
				}
			}
		}
	}

	[CompilerGenerated]
	private sealed class _003CPlayerLightApproach_003Ed__20 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_Campfire _003C_003E4__this;

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
		public _003CPlayerLightApproach_003Ed__20(int _003C_003E1__state)
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
			CS06_Campfire cS06_Campfire = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if (cS06_Campfire.player.Light.Alpha < 1f)
			{
				cS06_Campfire.player.Light.Alpha = Calc.Approach(cS06_Campfire.player.Light.Alpha, 1f, Engine.DeltaTime * 2f);
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
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

	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__21 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_Campfire _003C_003E4__this;

		public Level level;

		private Coroutine _003CcamTo_003E5__2;

		private FadeWipe _003Cfade_003E5__3;

		private float _003Cp_003E5__4;

		private Option _003Cselected_003E5__5;

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
		public _003CCutscene_003Ed__21(int _003C_003E1__state)
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
			CS06_Campfire CS_0024_003C_003E8__locals52 = _003C_003E4__this;
			string text;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				CS_0024_003C_003E8__locals52.Add(new Coroutine(CS_0024_003C_003E8__locals52.PlayerLightApproach()));
				CS_0024_003C_003E8__locals52.Add(_003CcamTo_003E5__2 = new Coroutine(CutsceneEntity.CameraTo(new Vector2(level.Camera.X + 90f, level.Camera.Y), 6f, Ease.CubeIn)));
				CS_0024_003C_003E8__locals52.player.DummyAutoAnimate = false;
				CS_0024_003C_003E8__locals52.player.Sprite.Play("carryTheoWalk");
				_003Cp_003E5__4 = 0f;
				goto IL_0182;
			case 2:
				_003C_003E1__state = -1;
				_003Cp_003E5__4 += Engine.DeltaTime;
				goto IL_0182;
			case 3:
			{
				_003C_003E1__state = -1;
				Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
				Vector2 position = CS_0024_003C_003E8__locals52.player.Position + new Vector2(16f, 1f);
				CS_0024_003C_003E8__locals52.Level.ParticlesFG.Emit(Payphone.P_Snow, 2, position, Vector2.UnitX * 4f);
				CS_0024_003C_003E8__locals52.Level.ParticlesFG.Emit(Payphone.P_SnowB, 12, position, Vector2.UnitX * 10f);
				_003C_003E2__current = 0.7f;
				_003C_003E1__state = 4;
				return true;
			}
			case 4:
				_003C_003E1__state = -1;
				_003Cfade_003E5__3 = new FadeWipe(level, wipeIn: false);
				_003Cfade_003E5__3.Duration = 1.5f;
				_003Cfade_003E5__3.EndTimer = 2.5f;
				_003C_003E2__current = _003Cfade_003E5__3.Wait();
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				CS_0024_003C_003E8__locals52.bonfire.SetMode(Bonfire.Mode.Lit);
				_003C_003E2__current = 2.45f;
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				_003CcamTo_003E5__2.Cancel();
				CS_0024_003C_003E8__locals52.theo.Position = CS_0024_003C_003E8__locals52.theoCampfirePosition;
				CS_0024_003C_003E8__locals52.theo.Sprite.Play("sleep");
				CS_0024_003C_003E8__locals52.theo.Sprite.SetAnimationFrame(CS_0024_003C_003E8__locals52.theo.Sprite.CurrentAnimationTotalFrames - 1);
				CS_0024_003C_003E8__locals52.player.Position = CS_0024_003C_003E8__locals52.playerCampfirePosition;
				CS_0024_003C_003E8__locals52.player.Facing = Facings.Left;
				CS_0024_003C_003E8__locals52.player.Sprite.Play("asleep");
				level.Session.SetFlag("starsbg");
				level.Session.SetFlag("duskbg", setTo: false);
				_003Cfade_003E5__3.EndTimer = 0f;
				new FadeWipe(level, wipeIn: true);
				_003C_003E2__current = null;
				_003C_003E1__state = 7;
				return true;
			case 7:
				_003C_003E1__state = -1;
				level.ResetZoom();
				level.Camera.Position = new Vector2(CS_0024_003C_003E8__locals52.bonfire.X - 160f, CS_0024_003C_003E8__locals52.bonfire.Y - 140f);
				_003C_003E2__current = 3f;
				_003C_003E1__state = 8;
				return true;
			case 8:
				_003C_003E1__state = -1;
				Audio.SetMusic("event:/music/lvl6/madeline_and_theo");
				_003C_003E2__current = 1.5f;
				_003C_003E1__state = 9;
				return true;
			case 9:
				_003C_003E1__state = -1;
				CS_0024_003C_003E8__locals52.Add(Wiggler.Create(0.6f, 3f, delegate(float v)
				{
					CS_0024_003C_003E8__locals52.theo.Sprite.Scale = Vector2.One * (1f + 0.1f * v);
				}, start: true, removeSelfOnFinish: true));
				CS_0024_003C_003E8__locals52.Level.Particles.Emit(NPC01_Theo.P_YOLO, 4, CS_0024_003C_003E8__locals52.theo.Position + new Vector2(-4f, -14f), Vector2.One * 3f);
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 10;
				return true;
			case 10:
				_003C_003E1__state = -1;
				CS_0024_003C_003E8__locals52.theo.Sprite.Play("wakeup");
				_003C_003E2__current = 1f;
				_003C_003E1__state = 11;
				return true;
			case 11:
				_003C_003E1__state = -1;
				CS_0024_003C_003E8__locals52.player.Sprite.Play("halfWakeUp");
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 12;
				return true;
			case 12:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("ch6_theo_intro");
				_003C_003E1__state = 13;
				return true;
			case 13:
				_003C_003E1__state = -1;
				text = "start";
				goto IL_07fe;
			case 14:
				_003C_003E1__state = -1;
				goto IL_0620;
			case 15:
				_003C_003E1__state = -1;
				goto IL_06f7;
			case 16:
				_003C_003E1__state = -1;
				goto IL_06f7;
			case 17:
				_003C_003E1__state = -1;
				goto IL_072b;
			case 18:
				_003C_003E1__state = -1;
				text = _003Cselected_003E5__5.Goto;
				if (!string.IsNullOrEmpty(text))
				{
					_003Cselected_003E5__5 = null;
					goto IL_07fe;
				}
				goto IL_0817;
			case 19:
				{
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals52.EndCutscene(level);
					return false;
				}
				IL_06f7:
				if (!Input.MenuConfirm.Pressed)
				{
					if (Input.MenuUp.Pressed && CS_0024_003C_003E8__locals52.currentOptionIndex > 0)
					{
						Audio.Play("event:/ui/game/chatoptions_roll_up");
						CS_0024_003C_003E8__locals52.currentOptionIndex--;
					}
					else if (Input.MenuDown.Pressed && CS_0024_003C_003E8__locals52.currentOptionIndex < CS_0024_003C_003E8__locals52.currentOptions.Count - 1)
					{
						Audio.Play("event:/ui/game/chatoptions_roll_down");
						CS_0024_003C_003E8__locals52.currentOptionIndex++;
					}
					_003C_003E2__current = null;
					_003C_003E1__state = 16;
					return true;
				}
				Audio.Play("event:/ui/game/chatoptions_select");
				goto IL_072b;
				IL_072b:
				if ((CS_0024_003C_003E8__locals52.optionEase -= Engine.DeltaTime * 4f) > 0f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 17;
					return true;
				}
				_003Cselected_003E5__5 = CS_0024_003C_003E8__locals52.currentOptions[CS_0024_003C_003E8__locals52.currentOptionIndex];
				CS_0024_003C_003E8__locals52.asked.Add(_003Cselected_003E5__5.Question);
				CS_0024_003C_003E8__locals52.currentOptions = null;
				_003C_003E2__current = Textbox.Say(_003Cselected_003E5__5.Question.Answer, CS_0024_003C_003E8__locals52.WaitABit, CS_0024_003C_003E8__locals52.SelfieSequence, CS_0024_003C_003E8__locals52.BeerSequence);
				_003C_003E1__state = 18;
				return true;
				IL_0817:
				_003C_003E2__current = new FadeWipe(level, wipeIn: false)
				{
					Duration = 3f
				}.Wait();
				_003C_003E1__state = 19;
				return true;
				IL_07fe:
				if (!string.IsNullOrEmpty(text) && CS_0024_003C_003E8__locals52.nodes.ContainsKey(text))
				{
					CS_0024_003C_003E8__locals52.currentOptionIndex = 0;
					CS_0024_003C_003E8__locals52.currentOptions = new List<Option>();
					Option[] array = CS_0024_003C_003E8__locals52.nodes[text];
					foreach (Option option in array)
					{
						if (option.CanAsk(CS_0024_003C_003E8__locals52.asked))
						{
							CS_0024_003C_003E8__locals52.currentOptions.Add(option);
						}
					}
					if (CS_0024_003C_003E8__locals52.currentOptions.Count > 0)
					{
						Audio.Play("event:/ui/game/chatoptions_appear");
						goto IL_0620;
					}
				}
				goto IL_0817;
				IL_0182:
				if (_003Cp_003E5__4 < 3.5f)
				{
					SpotlightWipe.FocusPoint = new Vector2(40f, 120f);
					CS_0024_003C_003E8__locals52.player.NaiveMove(new Vector2(32f * Engine.DeltaTime, 0f));
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				CS_0024_003C_003E8__locals52.player.Sprite.Play("carryTheoCollapse");
				Audio.Play("event:/char/madeline/theo_collapse", CS_0024_003C_003E8__locals52.player.Position);
				_003C_003E2__current = 0.3f;
				_003C_003E1__state = 3;
				return true;
				IL_0620:
				if ((CS_0024_003C_003E8__locals52.optionEase += Engine.DeltaTime * 4f) < 1f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 14;
					return true;
				}
				CS_0024_003C_003E8__locals52.optionEase = 1f;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 15;
				return true;
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
	private sealed class _003CWaitABit_003Ed__22 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

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
		public _003CWaitABit_003Ed__22(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			switch (_003C_003E1__state)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.8f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
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
	private sealed class _003CSelfieSequence_003Ed__23 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_Campfire _003C_003E4__this;

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
		public _003CSelfieSequence_003Ed__23(int _003C_003E1__state)
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
			CS06_Campfire CS_0024_003C_003E8__locals25 = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				CS_0024_003C_003E8__locals25.Add(new Coroutine(CS_0024_003C_003E8__locals25.Level.ZoomTo(new Vector2(160f, 105f), 2f, 0.5f)));
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				CS_0024_003C_003E8__locals25.theo.Sprite.Play("idle");
				CS_0024_003C_003E8__locals25.Add(Alarm.Create(Alarm.AlarmMode.Oneshot, delegate
				{
					CS_0024_003C_003E8__locals25.theo.Sprite.Scale.X = -1f;
				}, 0.25f, start: true));
				CS_0024_003C_003E8__locals25.player.DummyAutoAnimate = true;
				_003C_003E2__current = CS_0024_003C_003E8__locals25.player.DummyWalkToExact((int)(CS_0024_003C_003E8__locals25.theo.X + 5f), walkBackwards: false, 0.7f);
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				Audio.Play("event:/game/02_old_site/theoselfie_foley", CS_0024_003C_003E8__locals25.theo.Position);
				CS_0024_003C_003E8__locals25.theo.Sprite.Play("takeSelfie");
				_003C_003E2__current = 1f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				CS_0024_003C_003E8__locals25.selfie = new Selfie(CS_0024_003C_003E8__locals25.SceneAs<Level>());
				CS_0024_003C_003E8__locals25.Scene.Add(CS_0024_003C_003E8__locals25.selfie);
				_003C_003E2__current = CS_0024_003C_003E8__locals25.selfie.PictureRoutine("selfieCampfire");
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				CS_0024_003C_003E8__locals25.selfie = null;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				_003C_003E2__current = CS_0024_003C_003E8__locals25.Level.ZoomBack(0.5f);
				_003C_003E1__state = 7;
				return true;
			case 7:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 8;
				return true;
			case 8:
				_003C_003E1__state = -1;
				CS_0024_003C_003E8__locals25.theo.Sprite.Scale.X = 1f;
				_003C_003E2__current = CS_0024_003C_003E8__locals25.player.DummyWalkToExact((int)CS_0024_003C_003E8__locals25.playerCampfirePosition.X, walkBackwards: false, 0.7f);
				_003C_003E1__state = 9;
				return true;
			case 9:
				_003C_003E1__state = -1;
				CS_0024_003C_003E8__locals25.theo.Sprite.Play("wakeup");
				_003C_003E2__current = 0.1;
				_003C_003E1__state = 10;
				return true;
			case 10:
				_003C_003E1__state = -1;
				CS_0024_003C_003E8__locals25.player.DummyAutoAnimate = false;
				CS_0024_003C_003E8__locals25.player.Facing = Facings.Left;
				CS_0024_003C_003E8__locals25.player.Sprite.Play("sleep");
				_003C_003E2__current = 2f;
				_003C_003E1__state = 11;
				return true;
			case 11:
				_003C_003E1__state = -1;
				CS_0024_003C_003E8__locals25.player.Sprite.Play("halfWakeUp");
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
	private sealed class _003CBeerSequence_003Ed__24 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

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
		public _003CBeerSequence_003Ed__24(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			switch (_003C_003E1__state)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
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

	public const string Flag = "campfire_chat";

	public const string DuskBackgroundFlag = "duskbg";

	public const string StarsBackgrundFlag = "starsbg";

	private NPC theo;

	private Player player;

	private Bonfire bonfire;

	private Plateau plateau;

	private Vector2 cameraStart;

	private Vector2 playerCampfirePosition;

	private Vector2 theoCampfirePosition;

	private Selfie selfie;

	private float optionEase;

	private Dictionary<string, Option[]> nodes = new Dictionary<string, Option[]>();

	private HashSet<Question> asked = new HashSet<Question>();

	private List<Option> currentOptions = new List<Option>();

	private int currentOptionIndex;

	public CS06_Campfire(NPC theo, Player player)
	{
		base.Tag = Tags.HUD;
		this.theo = theo;
		this.player = player;
		Question question = new Question("outfor");
		Question question2 = new Question("temple");
		Question question3 = new Question("explain");
		Question question4 = new Question("thankyou");
		Question question5 = new Question("why");
		Question question6 = new Question("depression");
		Question question7 = new Question("defense");
		Question question8 = new Question("vacation");
		Question question9 = new Question("trust");
		Question question10 = new Question("family");
		Question question11 = new Question("grandpa");
		Question question12 = new Question("tips");
		Question question13 = new Question("selfie");
		Question question14 = new Question("sleep");
		Question question15 = new Question("sleep_confirm");
		Question question16 = new Question("sleep_cancel");
		nodes.Add("start", new Option[14]
		{
			new Option(question, "start").ExcludedBy(question5),
			new Option(question2, "start").Require(question9),
			new Option(question9, "start").Require(question3),
			new Option(question10, "start").Require(question9, question5),
			new Option(question11, "start").Require(question10, question7),
			new Option(question12, "start").Require(question11),
			new Option(question3, "start"),
			new Option(question4, "start").Require(question3),
			new Option(question5, "start").Require(question3, question9),
			new Option(question6, "start").Require(question5),
			new Option(question7, "start").Require(question6),
			new Option(question8, "start").Require(question6),
			new Option(question13, "").Require(question7, question11),
			new Option(question14, "sleep").Require(question5).ExcludedBy(question7, question11).Repeatable()
		});
		nodes.Add("sleep", new Option[2]
		{
			new Option(question16, "start").Repeatable(),
			new Option(question15, "")
		});
	}

	public override void OnBegin(Level level)
	{
		Audio.SetMusic(null, startPlaying: false, allowFadeOut: false);
		level.SnapColorGrade(null);
		level.Bloom.Base = 0f;
		level.Session.SetFlag("duskbg");
		plateau = base.Scene.Entities.FindFirst<Plateau>();
		bonfire = base.Scene.Tracker.GetEntity<Bonfire>();
		level.Camera.Position = new Vector2(level.Bounds.Left, bonfire.Y - 144f);
		level.ZoomSnap(new Vector2(80f, 120f), 2f);
		cameraStart = level.Camera.Position;
		theo.X = level.Camera.X - 48f;
		theoCampfirePosition = new Vector2(bonfire.X - 16f, bonfire.Y);
		player.Light.Alpha = 0f;
		player.X = level.Bounds.Left - 40;
		player.StateMachine.State = 11;
		player.StateMachine.Locked = true;
		playerCampfirePosition = new Vector2(bonfire.X + 20f, bonfire.Y);
		if (level.Session.GetFlag("campfire_chat"))
		{
			WasSkipped = true;
			level.ResetZoom();
			level.EndCutscene();
			EndCutscene(level);
		}
		else
		{
			Add(new Coroutine(Cutscene(level)));
		}
	}

	[IteratorStateMachine(typeof(_003CPlayerLightApproach_003Ed__20))]
	private IEnumerator PlayerLightApproach()
	{
		while (player.Light.Alpha < 1f)
		{
			player.Light.Alpha = Calc.Approach(player.Light.Alpha, 1f, Engine.DeltaTime * 2f);
			yield return null;
		}
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__21))]
	private IEnumerator Cutscene(Level level)
	{
		yield return 0.1f;
		Add(new Coroutine(PlayerLightApproach()));
		CS06_Campfire cS06_Campfire = this;
		Coroutine component;
		Coroutine camTo = (component = new Coroutine(CutsceneEntity.CameraTo(new Vector2(level.Camera.X + 90f, level.Camera.Y), 6f, Ease.CubeIn)));
		cS06_Campfire.Add(component);
		player.DummyAutoAnimate = false;
		player.Sprite.Play("carryTheoWalk");
		for (float p = 0f; p < 3.5f; p += Engine.DeltaTime)
		{
			SpotlightWipe.FocusPoint = new Vector2(40f, 120f);
			player.NaiveMove(new Vector2(32f * Engine.DeltaTime, 0f));
			yield return null;
		}
		player.Sprite.Play("carryTheoCollapse");
		Audio.Play("event:/char/madeline/theo_collapse", player.Position);
		yield return 0.3f;
		Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
		Vector2 position = player.Position + new Vector2(16f, 1f);
		Level.ParticlesFG.Emit(Payphone.P_Snow, 2, position, Vector2.UnitX * 4f);
		Level.ParticlesFG.Emit(Payphone.P_SnowB, 12, position, Vector2.UnitX * 10f);
		yield return 0.7f;
		FadeWipe fade = new FadeWipe(level, wipeIn: false)
		{
			Duration = 1.5f,
			EndTimer = 2.5f
		};
		yield return fade.Wait();
		bonfire.SetMode(Bonfire.Mode.Lit);
		yield return 2.45f;
		camTo.Cancel();
		theo.Position = theoCampfirePosition;
		theo.Sprite.Play("sleep");
		theo.Sprite.SetAnimationFrame(theo.Sprite.CurrentAnimationTotalFrames - 1);
		player.Position = playerCampfirePosition;
		player.Facing = Facings.Left;
		player.Sprite.Play("asleep");
		level.Session.SetFlag("starsbg");
		level.Session.SetFlag("duskbg", setTo: false);
		fade.EndTimer = 0f;
		new FadeWipe(level, wipeIn: true);
		yield return null;
		level.ResetZoom();
		level.Camera.Position = new Vector2(bonfire.X - 160f, bonfire.Y - 140f);
		yield return 3f;
		Audio.SetMusic("event:/music/lvl6/madeline_and_theo");
		yield return 1.5f;
		Add(Wiggler.Create(0.6f, 3f, delegate(float v)
		{
			theo.Sprite.Scale = Vector2.One * (1f + 0.1f * v);
		}, start: true, removeSelfOnFinish: true));
		Level.Particles.Emit(NPC01_Theo.P_YOLO, 4, theo.Position + new Vector2(-4f, -14f), Vector2.One * 3f);
		yield return 0.5f;
		theo.Sprite.Play("wakeup");
		yield return 1f;
		player.Sprite.Play("halfWakeUp");
		yield return 0.25f;
		yield return Textbox.Say("ch6_theo_intro");
		string text = "start";
		while (!string.IsNullOrEmpty(text) && nodes.ContainsKey(text))
		{
			currentOptionIndex = 0;
			currentOptions = new List<Option>();
			Option[] array = nodes[text];
			foreach (Option option in array)
			{
				if (option.CanAsk(asked))
				{
					currentOptions.Add(option);
				}
			}
			if (currentOptions.Count <= 0)
			{
				break;
			}
			Audio.Play("event:/ui/game/chatoptions_appear");
			while ((optionEase += Engine.DeltaTime * 4f) < 1f)
			{
				yield return null;
			}
			optionEase = 1f;
			yield return 0.25f;
			while (!Input.MenuConfirm.Pressed)
			{
				if (Input.MenuUp.Pressed && currentOptionIndex > 0)
				{
					Audio.Play("event:/ui/game/chatoptions_roll_up");
					currentOptionIndex--;
				}
				else if (Input.MenuDown.Pressed && currentOptionIndex < currentOptions.Count - 1)
				{
					Audio.Play("event:/ui/game/chatoptions_roll_down");
					currentOptionIndex++;
				}
				yield return null;
			}
			Audio.Play("event:/ui/game/chatoptions_select");
			while ((optionEase -= Engine.DeltaTime * 4f) > 0f)
			{
				yield return null;
			}
			Option selected = currentOptions[currentOptionIndex];
			asked.Add(selected.Question);
			currentOptions = null;
			yield return Textbox.Say(selected.Question.Answer, WaitABit, SelfieSequence, BeerSequence);
			text = selected.Goto;
			if (string.IsNullOrEmpty(text))
			{
				break;
			}
		}
		FadeWipe fadeWipe = new FadeWipe(level, wipeIn: false);
		fadeWipe.Duration = 3f;
		yield return fadeWipe.Wait();
		EndCutscene(level);
	}

	[IteratorStateMachine(typeof(_003CWaitABit_003Ed__22))]
	private IEnumerator WaitABit()
	{
		yield return 0.8f;
	}

	[IteratorStateMachine(typeof(_003CSelfieSequence_003Ed__23))]
	private IEnumerator SelfieSequence()
	{
		Add(new Coroutine(Level.ZoomTo(new Vector2(160f, 105f), 2f, 0.5f)));
		yield return 0.1f;
		theo.Sprite.Play("idle");
		Add(Alarm.Create(Alarm.AlarmMode.Oneshot, delegate
		{
			theo.Sprite.Scale.X = -1f;
		}, 0.25f, start: true));
		player.DummyAutoAnimate = true;
		yield return player.DummyWalkToExact((int)(theo.X + 5f), walkBackwards: false, 0.7f);
		yield return 0.2f;
		Audio.Play("event:/game/02_old_site/theoselfie_foley", theo.Position);
		theo.Sprite.Play("takeSelfie");
		yield return 1f;
		selfie = new Selfie(SceneAs<Level>());
		base.Scene.Add(selfie);
		yield return selfie.PictureRoutine("selfieCampfire");
		selfie = null;
		yield return 0.5f;
		yield return Level.ZoomBack(0.5f);
		yield return 0.2f;
		theo.Sprite.Scale.X = 1f;
		yield return player.DummyWalkToExact((int)playerCampfirePosition.X, walkBackwards: false, 0.7f);
		theo.Sprite.Play("wakeup");
		yield return 0.1;
		player.DummyAutoAnimate = false;
		player.Facing = Facings.Left;
		player.Sprite.Play("sleep");
		yield return 2f;
		player.Sprite.Play("halfWakeUp");
	}

	[IteratorStateMachine(typeof(_003CBeerSequence_003Ed__24))]
	private IEnumerator BeerSequence()
	{
		yield return 0.5f;
	}

	public override void OnEnd(Level level)
	{
		if (!WasSkipped)
		{
			level.ZoomSnap(new Vector2(160f, 120f), 2f);
			FadeWipe fadeWipe = new FadeWipe(level, wipeIn: true);
			fadeWipe.Duration = 3f;
			Coroutine zoom = new Coroutine(level.ZoomBack(fadeWipe.Duration));
			fadeWipe.OnUpdate = delegate
			{
				zoom.Update();
			};
		}
		if (selfie != null)
		{
			selfie.RemoveSelf();
		}
		level.Session.SetFlag("campfire_chat");
		level.Session.SetFlag("starsbg", setTo: false);
		level.Session.SetFlag("duskbg", setTo: false);
		level.Session.Dreaming = true;
		level.Add(new StarJumpController());
		level.Add(new CS06_StarJumpEnd(theo, player, playerCampfirePosition, cameraStart));
		level.Add(new FlyFeather(level.LevelOffset + new Vector2(272f, 2616f), shielded: false, singleUse: false));
		SetBloom(1f);
		bonfire.Activated = false;
		bonfire.SetMode(Bonfire.Mode.Lit);
		theo.Sprite.Play("sleep");
		theo.Sprite.SetAnimationFrame(theo.Sprite.CurrentAnimationTotalFrames - 1);
		theo.Sprite.Scale.X = 1f;
		theo.Position = theoCampfirePosition;
		player.Sprite.Play("asleep");
		player.Position = playerCampfirePosition;
		player.StateMachine.Locked = false;
		player.StateMachine.State = 15;
		player.Speed = Vector2.Zero;
		player.Facing = Facings.Left;
		level.Camera.Position = player.CameraTarget;
		if (WasSkipped)
		{
			player.StateMachine.State = 0;
		}
		RemoveSelf();
	}

	private void SetBloom(float add)
	{
		Level.Session.BloomBaseAdd = add;
		Level.Bloom.Base = AreaData.Get(Level).BloomBase + add;
	}

	public override void Update()
	{
		if (currentOptions != null)
		{
			for (int i = 0; i < currentOptions.Count; i++)
			{
				currentOptions[i].Update();
				currentOptions[i].Highlight = Calc.Approach(currentOptions[i].Highlight, (currentOptionIndex == i) ? 1 : 0, Engine.DeltaTime * 4f);
			}
		}
		base.Update();
	}

	public override void Render()
	{
		if (Level.Paused || currentOptions == null)
		{
			return;
		}
		int num = 0;
		foreach (Option currentOption in currentOptions)
		{
			Vector2 position = new Vector2(260f, 120f + 160f * (float)num);
			currentOption.Render(position, optionEase);
			num++;
		}
	}
}
