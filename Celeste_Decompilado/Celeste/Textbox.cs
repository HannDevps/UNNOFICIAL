using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Xml;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

[Tracked(false)]
public class Textbox : Entity
{
	[CompilerGenerated]
	private sealed class _003CRunRoutine_003Ed__67 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Textbox _003C_003E4__this;

		private FancyText.Node _003Clast_003E5__2;

		private float _003CdelayBuildup_003E5__3;

		private FancyText.Node _003Ccurrent_003E5__4;

		private float _003Cdelay_003E5__5;

		private FancyText.Anchors _003Cnext_003E5__6;

		private FancyText.Portrait _003Cnext_003E5__7;

		private FancyText.Trigger _003Ctrigger_003E5__8;

		private FancyText.Char _003Cch_003E5__9;

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
		public _003CRunRoutine_003Ed__67(int _003C_003E1__state)
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
			Textbox textbox = _003C_003E4__this;
			XmlElement xmlElement;
			bool flag;
			int index;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Clast_003E5__2 = null;
				_003CdelayBuildup_003E5__3 = 0f;
				goto IL_0923;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0106;
			case 2:
				_003C_003E1__state = -1;
				goto IL_01ac;
			case 3:
				_003C_003E1__state = -1;
				goto IL_052e;
			case 4:
				_003C_003E1__state = -1;
				goto IL_052e;
			case 5:
				_003C_003E1__state = -1;
				textbox.canSkip = true;
				goto IL_05d7;
			case 6:
				_003C_003E1__state = -1;
				goto IL_05d7;
			case 7:
				_003C_003E1__state = -1;
				goto IL_0646;
			case 8:
				_003C_003E1__state = -1;
				goto IL_0646;
			case 9:
				_003C_003E1__state = -1;
				goto IL_0701;
			case 10:
				_003C_003E1__state = -1;
				goto IL_074c;
			case 11:
				_003C_003E1__state = -1;
				goto IL_07bc;
			case 12:
				_003C_003E1__state = -1;
				textbox.index++;
				goto IL_0881;
			case 13:
				_003C_003E1__state = -1;
				goto IL_091c;
			case 14:
				_003C_003E1__state = -1;
				goto IL_096d;
			case 15:
				{
					_003C_003E1__state = -1;
					break;
				}
				IL_0646:
				if (!textbox.ContinuePressed())
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 8;
					return true;
				}
				textbox.waitingForInput = false;
				goto IL_0655;
				IL_08a0:
				_003Clast_003E5__2 = _003Ccurrent_003E5__4;
				textbox.index++;
				if (_003Cdelay_003E5__5 < 0.016f)
				{
					_003CdelayBuildup_003E5__3 += _003Cdelay_003E5__5;
					goto IL_091c;
				}
				_003CdelayBuildup_003E5__3 = 0f;
				if (_003Cdelay_003E5__5 > 0.5f)
				{
					textbox.PlayIdleAnimation();
				}
				_003C_003E2__current = _003Cdelay_003E5__5;
				_003C_003E1__state = 13;
				return true;
				IL_0106:
				textbox.anchor = _003Cnext_003E5__6;
				goto IL_08a0;
				IL_05d7:
				_003Cnext_003E5__7 = null;
				goto IL_08a0;
				IL_052e:
				if (textbox.portraitSprite.CurrentAnimationID == _003Cnext_003E5__7.BeginAnimation && textbox.portraitSprite.Animating)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 4;
					return true;
				}
				goto IL_0558;
				IL_01ac:
				textbox.textbox = GFX.Portraits["textbox/default"];
				textbox.textboxOverlay = null;
				textbox.portraitExists = false;
				textbox.activeTalker = null;
				textbox.isPortraitGlitchy = false;
				xmlElement = null;
				if (!string.IsNullOrEmpty(_003Cnext_003E5__7.Sprite))
				{
					if (GFX.PortraitsSpriteBank.Has(_003Cnext_003E5__7.SpriteId))
					{
						xmlElement = GFX.PortraitsSpriteBank.SpriteData[_003Cnext_003E5__7.SpriteId].Sources[0].XML;
					}
					textbox.portraitExists = xmlElement != null;
					textbox.isPortraitGlitchy = _003Cnext_003E5__7.Glitchy;
					if (textbox.isPortraitGlitchy && textbox.portraitGlitchy == null)
					{
						textbox.portraitGlitchy = new Sprite(GFX.Portraits, "noise/");
						textbox.portraitGlitchy.AddLoop("noise", "", 0.1f);
						textbox.portraitGlitchy.Play("noise");
					}
				}
				if (textbox.portraitExists)
				{
					if (textbox.portrait == null || _003Cnext_003E5__7.Sprite != textbox.portrait.Sprite)
					{
						GFX.PortraitsSpriteBank.CreateOn(textbox.portraitSprite, _003Cnext_003E5__7.SpriteId);
						textbox.portraitScale = 240f / (float)xmlElement.AttrInt("size", 160);
						if (!textbox.talkers.ContainsKey(_003Cnext_003E5__7.SfxEvent))
						{
							SoundSource soundSource = new SoundSource().Play(_003Cnext_003E5__7.SfxEvent);
							textbox.talkers.Add(_003Cnext_003E5__7.SfxEvent, soundSource);
							textbox.Add(soundSource);
						}
					}
					if (textbox.talkers.ContainsKey(_003Cnext_003E5__7.SfxEvent))
					{
						textbox.activeTalker = textbox.talkers[_003Cnext_003E5__7.SfxEvent];
					}
					string text = "textbox/" + xmlElement.Attr("textbox", "default");
					textbox.textbox = GFX.Portraits[text];
					if (GFX.Portraits.Has(text + "_overlay"))
					{
						textbox.textboxOverlay = GFX.Portraits[text + "_overlay"];
					}
					string text2 = xmlElement.Attr("phonestatic", "");
					if (!string.IsNullOrEmpty(text2))
					{
						if (text2 == "ex")
						{
							textbox.phonestatic.Play("event:/char/dialogue/sfx_support/phone_static_ex");
						}
						else if (text2 == "mom")
						{
							textbox.phonestatic.Play("event:/char/dialogue/sfx_support/phone_static_mom");
						}
					}
					textbox.canSkip = false;
					FancyText.Portrait portrait = textbox.portrait;
					textbox.portrait = _003Cnext_003E5__7;
					if (_003Cnext_003E5__7.Pop)
					{
						textbox.portraitWiggle.Start();
					}
					if (portrait == null || portrait.Sprite != _003Cnext_003E5__7.Sprite || portrait.Animation != _003Cnext_003E5__7.Animation)
					{
						if (textbox.portraitSprite.Has(_003Cnext_003E5__7.BeginAnimation))
						{
							textbox.portraitSprite.Play(_003Cnext_003E5__7.BeginAnimation, restart: true);
							_003C_003E2__current = textbox.EaseOpen();
							_003C_003E1__state = 3;
							return true;
						}
						goto IL_0558;
					}
					goto IL_058f;
				}
				textbox.portrait = null;
				_003C_003E2__current = textbox.EaseOpen();
				_003C_003E1__state = 6;
				return true;
				IL_07bc:
				flag = false;
				if (textbox.index - 5 > textbox.Start)
				{
					for (int i = textbox.index; i < Math.Min(textbox.index + 4, textbox.Nodes.Count); i++)
					{
						if (textbox.Nodes[i] is FancyText.NewPage)
						{
							flag = true;
							textbox.PlayIdleAnimation();
						}
					}
				}
				if (!flag && !_003Cch_003E5__9.IsPunctuation)
				{
					textbox.PlayTalkAnimation();
				}
				if (_003Clast_003E5__2 != null && _003Clast_003E5__2 is FancyText.NewPage)
				{
					textbox.index--;
					_003C_003E2__current = 0.2f;
					_003C_003E1__state = 12;
					return true;
				}
				goto IL_0881;
				IL_0923:
				if (textbox.index < textbox.Nodes.Count)
				{
					_003Ccurrent_003E5__4 = textbox.Nodes[textbox.index];
					_003Cdelay_003E5__5 = 0f;
					if (_003Ccurrent_003E5__4 is FancyText.Anchor)
					{
						if (textbox.RenderOffset == Vector2.Zero)
						{
							_003Cnext_003E5__6 = (_003Ccurrent_003E5__4 as FancyText.Anchor).Position;
							if (textbox.ease >= 1f && _003Cnext_003E5__6 != textbox.anchor)
							{
								_003C_003E2__current = textbox.EaseClose(final: false);
								_003C_003E1__state = 1;
								return true;
							}
							goto IL_0106;
						}
					}
					else
					{
						if (_003Ccurrent_003E5__4 is FancyText.Portrait)
						{
							_003Cnext_003E5__7 = _003Ccurrent_003E5__4 as FancyText.Portrait;
							textbox.phonestatic.Stop();
							if (textbox.ease >= 1f && (textbox.portrait == null || _003Cnext_003E5__7.Sprite != textbox.portrait.Sprite || _003Cnext_003E5__7.Side != textbox.portrait.Side))
							{
								_003C_003E2__current = textbox.EaseClose(final: false);
								_003C_003E1__state = 2;
								return true;
							}
							goto IL_01ac;
						}
						if (_003Ccurrent_003E5__4 is FancyText.NewPage)
						{
							textbox.PlayIdleAnimation();
							if (textbox.ease >= 1f)
							{
								textbox.waitingForInput = true;
								_003C_003E2__current = 0.1f;
								_003C_003E1__state = 7;
								return true;
							}
							goto IL_0655;
						}
						if (_003Ccurrent_003E5__4 is FancyText.Wait)
						{
							textbox.PlayIdleAnimation();
							_003Cdelay_003E5__5 = (_003Ccurrent_003E5__4 as FancyText.Wait).Duration;
						}
						else
						{
							if (_003Ccurrent_003E5__4 is FancyText.Trigger)
							{
								textbox.isInTrigger = true;
								textbox.PlayIdleAnimation();
								_003Ctrigger_003E5__8 = _003Ccurrent_003E5__4 as FancyText.Trigger;
								if (!_003Ctrigger_003E5__8.Silent)
								{
									_003C_003E2__current = textbox.EaseClose(final: false);
									_003C_003E1__state = 9;
									return true;
								}
								goto IL_0701;
							}
							if (_003Ccurrent_003E5__4 is FancyText.Char)
							{
								_003Cch_003E5__9 = _003Ccurrent_003E5__4 as FancyText.Char;
								textbox.lastChar = (char)_003Cch_003E5__9.Character;
								if (textbox.ease < 1f)
								{
									_003C_003E2__current = textbox.EaseOpen();
									_003C_003E1__state = 11;
									return true;
								}
								goto IL_07bc;
							}
						}
					}
					goto IL_08a0;
				}
				textbox.PlayIdleAnimation();
				if (!(textbox.ease > 0f))
				{
					break;
				}
				textbox.waitingForInput = true;
				goto IL_096d;
				IL_0881:
				_003Cdelay_003E5__5 = _003Cch_003E5__9.Delay + _003CdelayBuildup_003E5__3;
				_003Cch_003E5__9 = null;
				goto IL_08a0;
				IL_0558:
				if (textbox.portraitSprite.Has(_003Cnext_003E5__7.IdleAnimation))
				{
					textbox.portraitIdling = true;
					textbox.portraitSprite.Play(_003Cnext_003E5__7.IdleAnimation, restart: true);
				}
				goto IL_058f;
				IL_096d:
				if (!textbox.ContinuePressed())
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 14;
					return true;
				}
				textbox.waitingForInput = false;
				textbox.Start = textbox.Nodes.Count;
				_003C_003E2__current = textbox.EaseClose(final: true);
				_003C_003E1__state = 15;
				return true;
				IL_0655:
				textbox.Start = textbox.index + 1;
				textbox.Page++;
				goto IL_08a0;
				IL_074c:
				textbox.isInTrigger = false;
				_003Ctrigger_003E5__8 = null;
				goto IL_08a0;
				IL_058f:
				_003C_003E2__current = textbox.EaseOpen();
				_003C_003E1__state = 5;
				return true;
				IL_091c:
				_003Ccurrent_003E5__4 = null;
				goto IL_0923;
				IL_0701:
				index = _003Ctrigger_003E5__8.Index;
				if (textbox.events != null && index >= 0 && index < textbox.events.Length)
				{
					_003C_003E2__current = textbox.events[index]();
					_003C_003E1__state = 10;
					return true;
				}
				goto IL_074c;
			}
			textbox.Close();
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
	private sealed class _003CEaseOpen_003Ed__72 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Textbox _003C_003E4__this;

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
		public _003CEaseOpen_003Ed__72(int _003C_003E1__state)
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
			Textbox textbox = _003C_003E4__this;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				_003C_003E1__state = -1;
			}
			else
			{
				_003C_003E1__state = -1;
				if (!(textbox.ease < 1f))
				{
					goto IL_00f2;
				}
				textbox.easingOpen = true;
				if (textbox.portrait != null && textbox.portrait.Sprite.IndexOf("madeline", StringComparison.InvariantCultureIgnoreCase) >= 0)
				{
					Audio.Play("event:/ui/game/textbox_madeline_in");
				}
				else
				{
					Audio.Play("event:/ui/game/textbox_other_in");
				}
			}
			if ((textbox.ease += (textbox.runRoutine.UseRawDeltaTime ? Engine.RawDeltaTime : Engine.DeltaTime) / 0.4f) < 1f)
			{
				textbox.gradientFade = Math.Max(textbox.gradientFade, textbox.ease);
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			textbox.ease = (textbox.gradientFade = 1f);
			textbox.easingOpen = false;
			goto IL_00f2;
			IL_00f2:
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
	private sealed class _003CEaseClose_003Ed__73 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Textbox _003C_003E4__this;

		public bool final;

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
		public _003CEaseClose_003Ed__73(int _003C_003E1__state)
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
			Textbox textbox = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				textbox.easingClose = true;
				if (textbox.portrait != null && textbox.portrait.Sprite.IndexOf("madeline", StringComparison.InvariantCultureIgnoreCase) >= 0)
				{
					Audio.Play("event:/ui/game/textbox_madeline_out");
				}
				else
				{
					Audio.Play("event:/ui/game/textbox_other_out");
				}
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if ((textbox.ease -= (textbox.runRoutine.UseRawDeltaTime ? Engine.RawDeltaTime : Engine.DeltaTime) / 0.4f) > 0f)
			{
				if (final)
				{
					textbox.gradientFade = textbox.ease;
				}
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			textbox.ease = 0f;
			textbox.easingClose = false;
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
	private sealed class _003CSkipDialog_003Ed__74 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Textbox _003C_003E4__this;

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
		public _003CSkipDialog_003Ed__74(int _003C_003E1__state)
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
			Textbox textbox = _003C_003E4__this;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				_003C_003E1__state = -1;
				textbox.disableInput = false;
			}
			else
			{
				_003C_003E1__state = -1;
			}
			if (!textbox.waitingForInput && textbox.canSkip && !textbox.easingOpen && !textbox.easingClose && textbox.ContinuePressed())
			{
				textbox.StopTalker();
				textbox.disableInput = true;
				while (!textbox.waitingForInput && textbox.canSkip && !textbox.easingOpen && !textbox.easingClose && !textbox.isInTrigger && !textbox.runRoutine.Finished)
				{
					textbox.runRoutine.Update();
				}
			}
			_003C_003E2__current = null;
			_003C_003E1__state = 1;
			return true;
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
	private sealed class _003CSay_003Ed__83 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public string dialog;

		public Func<IEnumerator>[] events;

		private Textbox _003Ctextbox_003E5__2;

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
		public _003CSay_003Ed__83(int _003C_003E1__state)
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
				_003Ctextbox_003E5__2 = new Textbox(dialog, events);
				Engine.Scene.Add(_003Ctextbox_003E5__2);
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if (_003Ctextbox_003E5__2.Opened)
			{
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
	private sealed class _003CSayWhileFrozen_003Ed__84 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public string dialog;

		public Func<IEnumerator>[] events;

		private Textbox _003Ctextbox_003E5__2;

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
		public _003CSayWhileFrozen_003Ed__84(int _003C_003E1__state)
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
				_003Ctextbox_003E5__2 = new Textbox(dialog, events);
				_003Ctextbox_003E5__2.Tag |= Tags.FrozenUpdate;
				Engine.Scene.Add(_003Ctextbox_003E5__2);
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if (_003Ctextbox_003E5__2.Opened)
			{
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

	private MTexture textbox = GFX.Portraits["textbox/default"];

	private MTexture textboxOverlay;

	private const int textboxInnerWidth = 1688;

	private const int textboxInnerHeight = 272;

	private const float portraitPadding = 16f;

	private const float tweenDuration = 0.4f;

	private const float switchToIdleAnimationDelay = 0.5f;

	private readonly float innerTextPadding;

	private readonly float maxLineWidthNoPortrait;

	private readonly float maxLineWidth;

	private readonly int linesPerPage;

	private const int stopVoiceCharactersEarly = 4;

	private float ease;

	private FancyText.Text text;

	private Func<IEnumerator>[] events;

	private Coroutine runRoutine;

	private Coroutine skipRoutine;

	private PixelFont font;

	private float lineHeight;

	private FancyText.Anchors anchor;

	private FancyText.Portrait portrait;

	private int index;

	private bool waitingForInput;

	private bool disableInput;

	private int shakeSeed;

	private float timer;

	private float gradientFade;

	private bool isInTrigger;

	private bool canSkip = true;

	private bool easingClose;

	private bool easingOpen;

	public Vector2 RenderOffset;

	private bool autoPressContinue;

	private char lastChar;

	private Sprite portraitSprite = new Sprite(null, null);

	private bool portraitExists;

	private bool portraitIdling;

	private float portraitScale = 1.5f;

	private Wiggler portraitWiggle;

	private Sprite portraitGlitchy;

	private bool isPortraitGlitchy;

	private Dictionary<string, SoundSource> talkers = new Dictionary<string, SoundSource>();

	private SoundSource activeTalker;

	private SoundSource phonestatic;

	public bool Opened { get; private set; }

	public int Page { get; private set; }

	public List<FancyText.Node> Nodes => text.Nodes;

	public bool UseRawDeltaTime
	{
		set
		{
			runRoutine.UseRawDeltaTime = value;
		}
	}

	public int Start { get; private set; }

	public string PortraitName
	{
		get
		{
			if (portrait == null || portrait.Sprite == null)
			{
				return "";
			}
			return portrait.Sprite;
		}
	}

	public string PortraitAnimation
	{
		get
		{
			if (portrait == null || portrait.Sprite == null)
			{
				return "";
			}
			return portrait.Animation;
		}
	}

	public Textbox(string dialog, params Func<IEnumerator>[] events)
		: this(dialog, null, events)
	{
	}

	public Textbox(string dialog, Language language, params Func<IEnumerator>[] events)
	{
		base.Tag = (int)Tags.PauseUpdate | (int)Tags.HUD;
		Opened = true;
		font = Dialog.Language.Font;
		lineHeight = Dialog.Language.FontSize.LineHeight - 1;
		portraitSprite.UseRawDeltaTime = true;
		Add(portraitWiggle = Wiggler.Create(0.4f, 4f));
		this.events = events;
		linesPerPage = (int)(240f / lineHeight);
		innerTextPadding = (272f - lineHeight * (float)linesPerPage) / 2f;
		maxLineWidthNoPortrait = 1688f - innerTextPadding * 2f;
		maxLineWidth = maxLineWidthNoPortrait - 240f - 32f;
		text = FancyText.Parse(Dialog.Get(dialog, language), (int)maxLineWidth, linesPerPage, 0f, null, language);
		index = 0;
		Start = 0;
		skipRoutine = new Coroutine(SkipDialog());
		runRoutine = new Coroutine(RunRoutine());
		runRoutine.UseRawDeltaTime = true;
		if (Level.DialogSnapshot == null)
		{
			Level.DialogSnapshot = Audio.CreateSnapshot("snapshot:/dialogue_in_progress", start: false);
		}
		Audio.ResumeSnapshot(Level.DialogSnapshot);
		Add(phonestatic = new SoundSource());
	}

	public void SetStart(int value)
	{
		int num = (Start = value);
		index = num;
	}

	[IteratorStateMachine(typeof(_003CRunRoutine_003Ed__67))]
	private IEnumerator RunRoutine()
	{
		FancyText.Node last = null;
		float delayBuildup = 0f;
		while (index < Nodes.Count)
		{
			FancyText.Node current = Nodes[index];
			float delay = 0f;
			if (current is FancyText.Anchor)
			{
				if (RenderOffset == Vector2.Zero)
				{
					FancyText.Anchors next = (current as FancyText.Anchor).Position;
					if (ease >= 1f && next != anchor)
					{
						yield return EaseClose(final: false);
					}
					anchor = next;
				}
			}
			else if (current is FancyText.Portrait)
			{
				FancyText.Portrait next2 = current as FancyText.Portrait;
				phonestatic.Stop();
				if (ease >= 1f && (this.portrait == null || next2.Sprite != this.portrait.Sprite || next2.Side != this.portrait.Side))
				{
					yield return EaseClose(final: false);
				}
				textbox = GFX.Portraits["textbox/default"];
				textboxOverlay = null;
				portraitExists = false;
				activeTalker = null;
				isPortraitGlitchy = false;
				XmlElement xmlElement = null;
				if (!string.IsNullOrEmpty(next2.Sprite))
				{
					if (GFX.PortraitsSpriteBank.Has(next2.SpriteId))
					{
						xmlElement = GFX.PortraitsSpriteBank.SpriteData[next2.SpriteId].Sources[0].XML;
					}
					portraitExists = xmlElement != null;
					isPortraitGlitchy = next2.Glitchy;
					if (isPortraitGlitchy && portraitGlitchy == null)
					{
						portraitGlitchy = new Sprite(GFX.Portraits, "noise/");
						portraitGlitchy.AddLoop("noise", "", 0.1f);
						portraitGlitchy.Play("noise");
					}
				}
				if (portraitExists)
				{
					if (this.portrait == null || next2.Sprite != this.portrait.Sprite)
					{
						GFX.PortraitsSpriteBank.CreateOn(portraitSprite, next2.SpriteId);
						portraitScale = 240f / (float)xmlElement.AttrInt("size", 160);
						if (!talkers.ContainsKey(next2.SfxEvent))
						{
							SoundSource soundSource = new SoundSource().Play(next2.SfxEvent);
							talkers.Add(next2.SfxEvent, soundSource);
							Add(soundSource);
						}
					}
					if (talkers.ContainsKey(next2.SfxEvent))
					{
						activeTalker = talkers[next2.SfxEvent];
					}
					string text = "textbox/" + xmlElement.Attr("textbox", "default");
					textbox = GFX.Portraits[text];
					if (GFX.Portraits.Has(text + "_overlay"))
					{
						textboxOverlay = GFX.Portraits[text + "_overlay"];
					}
					string text2 = xmlElement.Attr("phonestatic", "");
					if (!string.IsNullOrEmpty(text2))
					{
						if (text2 == "ex")
						{
							phonestatic.Play("event:/char/dialogue/sfx_support/phone_static_ex");
						}
						else if (text2 == "mom")
						{
							phonestatic.Play("event:/char/dialogue/sfx_support/phone_static_mom");
						}
					}
					canSkip = false;
					FancyText.Portrait portrait = this.portrait;
					this.portrait = next2;
					if (next2.Pop)
					{
						portraitWiggle.Start();
					}
					if (portrait == null || portrait.Sprite != next2.Sprite || portrait.Animation != next2.Animation)
					{
						if (portraitSprite.Has(next2.BeginAnimation))
						{
							portraitSprite.Play(next2.BeginAnimation, restart: true);
							yield return EaseOpen();
							while (portraitSprite.CurrentAnimationID == next2.BeginAnimation && portraitSprite.Animating)
							{
								yield return null;
							}
						}
						if (portraitSprite.Has(next2.IdleAnimation))
						{
							portraitIdling = true;
							portraitSprite.Play(next2.IdleAnimation, restart: true);
						}
					}
					yield return EaseOpen();
					canSkip = true;
				}
				else
				{
					this.portrait = null;
					yield return EaseOpen();
				}
			}
			else if (current is FancyText.NewPage)
			{
				PlayIdleAnimation();
				if (ease >= 1f)
				{
					waitingForInput = true;
					yield return 0.1f;
					while (!ContinuePressed())
					{
						yield return null;
					}
					waitingForInput = false;
				}
				Start = index + 1;
				Page++;
			}
			else if (current is FancyText.Wait)
			{
				PlayIdleAnimation();
				delay = (current as FancyText.Wait).Duration;
			}
			else if (current is FancyText.Trigger)
			{
				isInTrigger = true;
				PlayIdleAnimation();
				FancyText.Trigger trigger = current as FancyText.Trigger;
				if (!trigger.Silent)
				{
					yield return EaseClose(final: false);
				}
				int num = trigger.Index;
				if (events != null && num >= 0 && num < events.Length)
				{
					yield return events[num]();
				}
				isInTrigger = false;
			}
			else if (current is FancyText.Char)
			{
				FancyText.Char ch = current as FancyText.Char;
				lastChar = (char)ch.Character;
				if (ease < 1f)
				{
					yield return EaseOpen();
				}
				bool flag = false;
				if (index - 5 > Start)
				{
					for (int i = index; i < Math.Min(index + 4, Nodes.Count); i++)
					{
						if (Nodes[i] is FancyText.NewPage)
						{
							flag = true;
							PlayIdleAnimation();
						}
					}
				}
				if (!flag && !ch.IsPunctuation)
				{
					PlayTalkAnimation();
				}
				if (last != null && last is FancyText.NewPage)
				{
					index--;
					yield return 0.2f;
					index++;
				}
				delay = ch.Delay + delayBuildup;
			}
			last = current;
			index++;
			if (delay < 0.016f)
			{
				delayBuildup += delay;
				continue;
			}
			delayBuildup = 0f;
			if (delay > 0.5f)
			{
				PlayIdleAnimation();
			}
			yield return delay;
		}
		PlayIdleAnimation();
		if (ease > 0f)
		{
			waitingForInput = true;
			while (!ContinuePressed())
			{
				yield return null;
			}
			waitingForInput = false;
			Start = Nodes.Count;
			yield return EaseClose(final: true);
		}
		Close();
	}

	private void PlayIdleAnimation()
	{
		StopTalker();
		if (!portraitIdling && portraitSprite != null && portrait != null && portraitSprite.Has(portrait.IdleAnimation))
		{
			portraitSprite.Play(portrait.IdleAnimation);
			portraitIdling = true;
		}
	}

	private void StopTalker()
	{
		if (activeTalker != null)
		{
			activeTalker.Param("dialogue_portrait", 0f);
			activeTalker.Param("dialogue_end", 1f);
		}
	}

	private void PlayTalkAnimation()
	{
		StartTalker();
		if (portraitIdling && portraitSprite != null && portrait != null && portraitSprite.Has(portrait.TalkAnimation))
		{
			portraitSprite.Play(portrait.TalkAnimation);
			portraitIdling = false;
		}
	}

	private void StartTalker()
	{
		if (activeTalker != null)
		{
			activeTalker.Param("dialogue_portrait", (portrait == null) ? 1 : portrait.SfxExpression);
			activeTalker.Param("dialogue_end", 0f);
		}
	}

	[IteratorStateMachine(typeof(_003CEaseOpen_003Ed__72))]
	private IEnumerator EaseOpen()
	{
		if (ease < 1f)
		{
			easingOpen = true;
			if (portrait != null && portrait.Sprite.IndexOf("madeline", StringComparison.InvariantCultureIgnoreCase) >= 0)
			{
				Audio.Play("event:/ui/game/textbox_madeline_in");
			}
			else
			{
				Audio.Play("event:/ui/game/textbox_other_in");
			}
			while ((ease += (runRoutine.UseRawDeltaTime ? Engine.RawDeltaTime : Engine.DeltaTime) / 0.4f) < 1f)
			{
				gradientFade = Math.Max(gradientFade, ease);
				yield return null;
			}
			ease = (gradientFade = 1f);
			easingOpen = false;
		}
	}

	[IteratorStateMachine(typeof(_003CEaseClose_003Ed__73))]
	private IEnumerator EaseClose(bool final)
	{
		easingClose = true;
		if (portrait != null && portrait.Sprite.IndexOf("madeline", StringComparison.InvariantCultureIgnoreCase) >= 0)
		{
			Audio.Play("event:/ui/game/textbox_madeline_out");
		}
		else
		{
			Audio.Play("event:/ui/game/textbox_other_out");
		}
		while ((ease -= (runRoutine.UseRawDeltaTime ? Engine.RawDeltaTime : Engine.DeltaTime) / 0.4f) > 0f)
		{
			if (final)
			{
				gradientFade = ease;
			}
			yield return null;
		}
		ease = 0f;
		easingClose = false;
	}

	[IteratorStateMachine(typeof(_003CSkipDialog_003Ed__74))]
	private IEnumerator SkipDialog()
	{
		while (true)
		{
			if (!waitingForInput && canSkip && !easingOpen && !easingClose && ContinuePressed())
			{
				StopTalker();
				disableInput = true;
				while (!waitingForInput && canSkip && !easingOpen && !easingClose && !isInTrigger && !runRoutine.Finished)
				{
					runRoutine.Update();
				}
			}
			yield return null;
			disableInput = false;
		}
	}

	public bool SkipToPage(int page)
	{
		autoPressContinue = true;
		while (Page != page && !runRoutine.Finished)
		{
			Update();
		}
		autoPressContinue = false;
		Update();
		while (Opened && ease < 1f)
		{
			Update();
		}
		if (Page == page)
		{
			return Opened;
		}
		return false;
	}

	public void Close()
	{
		Opened = false;
		if (base.Scene != null)
		{
			base.Scene.Remove(this);
		}
	}

	private bool ContinuePressed()
	{
		if (!autoPressContinue)
		{
			if (Input.MenuConfirm.Pressed || Input.MenuCancel.Pressed)
			{
				return !disableInput;
			}
			return false;
		}
		return true;
	}

	public override void Update()
	{
		if (base.Scene is Level level && (level.FrozenOrPaused || level.RetryPlayerCorpse != null))
		{
			return;
		}
		if (!autoPressContinue)
		{
			skipRoutine.Update();
		}
		runRoutine.Update();
		if (base.Scene != null && base.Scene.OnInterval(0.05f))
		{
			shakeSeed = Calc.Random.Next();
		}
		if (portraitSprite != null && ease >= 1f)
		{
			portraitSprite.Update();
		}
		if (portraitGlitchy != null && ease >= 1f)
		{
			portraitGlitchy.Update();
		}
		timer += Engine.DeltaTime;
		portraitWiggle.Update();
		int num = Math.Min(index, Nodes.Count);
		for (int i = Start; i < num; i++)
		{
			if (Nodes[i] is FancyText.Char)
			{
				FancyText.Char obj = Nodes[i] as FancyText.Char;
				if (obj.Fade < 1f)
				{
					obj.Fade = Calc.Clamp(obj.Fade + 8f * Engine.DeltaTime, 0f, 1f);
				}
			}
		}
	}

	public override void Render()
	{
		if (base.Scene is Level level && (level.FrozenOrPaused || level.RetryPlayerCorpse != null || level.SkippingCutscene))
		{
			return;
		}
		float num = Ease.CubeInOut(ease);
		if (num < 0.05f)
		{
			return;
		}
		float num2 = 116f;
		Vector2 vector = new Vector2(num2, num2 / 2f) + RenderOffset;
		if (RenderOffset == Vector2.Zero)
		{
			if (anchor == FancyText.Anchors.Bottom)
			{
				vector = new Vector2(num2, 1080f - num2 / 2f - 272f);
			}
			else if (anchor == FancyText.Anchors.Middle)
			{
				vector = new Vector2(num2, 404f);
			}
			vector.Y += (int)(136f * (1f - num));
		}
		textbox.DrawCentered(vector + new Vector2(1688f, 272f * num) / 2f, Color.White, new Vector2(1f, num));
		if (waitingForInput)
		{
			float num3 = ((portrait == null || PortraitSide(portrait) < 0) ? 1688f : 1432f);
			Vector2 position = new Vector2(vector.X + num3, vector.Y + 272f) + new Vector2(-48f, -40 + ((timer % 1f < 0.25f) ? 6 : 0));
			GFX.Gui["textboxbutton"].DrawCentered(position);
		}
		if (portraitExists)
		{
			if (PortraitSide(portrait) > 0)
			{
				portraitSprite.Position = new Vector2(vector.X + 1688f - 240f - 16f, vector.Y);
				portraitSprite.Scale.X = 0f - portraitScale;
			}
			else
			{
				portraitSprite.Position = new Vector2(vector.X + 16f, vector.Y);
				portraitSprite.Scale.X = portraitScale;
			}
			portraitSprite.Scale.X *= ((!portrait.Flipped) ? 1 : (-1));
			portraitSprite.Scale.Y = portraitScale * ((272f * num - 32f) / 240f) * (float)((!portrait.UpsideDown) ? 1 : (-1));
			portraitSprite.Scale *= 0.9f + portraitWiggle.Value * 0.1f;
			portraitSprite.Position += new Vector2(120f, 272f * num * 0.5f);
			portraitSprite.Color = Color.White * num;
			if (Math.Abs(portraitSprite.Scale.Y) > 0.05f)
			{
				portraitSprite.Render();
				if (isPortraitGlitchy && portraitGlitchy != null)
				{
					portraitGlitchy.Position = portraitSprite.Position;
					portraitGlitchy.Origin = portraitSprite.Origin;
					portraitGlitchy.Scale = portraitSprite.Scale;
					portraitGlitchy.Color = Color.White * 0.2f * num;
					portraitGlitchy.Render();
				}
			}
		}
		if (textboxOverlay != null)
		{
			int num4 = 1;
			if (portrait != null && PortraitSide(portrait) > 0)
			{
				num4 = -1;
			}
			textboxOverlay.DrawCentered(vector + new Vector2(1688f, 272f * num) / 2f, Color.White, new Vector2(num4, num));
		}
		Calc.PushRandom(shakeSeed);
		int num5 = 1;
		for (int i = Start; i < text.Nodes.Count; i++)
		{
			if (text.Nodes[i] is FancyText.NewLine)
			{
				num5++;
			}
			else if (text.Nodes[i] is FancyText.NewPage)
			{
				break;
			}
		}
		Vector2 vector2 = new Vector2(innerTextPadding + ((portrait != null && PortraitSide(portrait) < 0) ? 256f : 0f), innerTextPadding);
		Vector2 vector3 = new Vector2((portrait == null) ? maxLineWidthNoPortrait : maxLineWidth, (float)linesPerPage * lineHeight * num) / 2f;
		float num6 = ((num5 >= 4) ? 0.75f : 1f);
		text.Draw(vector + vector2 + vector3, new Vector2(0.5f, 0.5f), new Vector2(1f, num) * num6, num, Start);
		Calc.PopRandom();
	}

	public int PortraitSide(FancyText.Portrait portrait)
	{
		if (SaveData.Instance != null && SaveData.Instance.Assists.MirrorMode)
		{
			return -portrait.Side;
		}
		return portrait.Side;
	}

	public override void Removed(Scene scene)
	{
		Audio.EndSnapshot(Level.DialogSnapshot);
		base.Removed(scene);
	}

	public override void SceneEnd(Scene scene)
	{
		Audio.EndSnapshot(Level.DialogSnapshot);
		base.SceneEnd(scene);
	}

	[IteratorStateMachine(typeof(_003CSay_003Ed__83))]
	public static IEnumerator Say(string dialog, params Func<IEnumerator>[] events)
	{
		Textbox textbox = new Textbox(dialog, events);
		Engine.Scene.Add(textbox);
		while (textbox.Opened)
		{
			yield return null;
		}
	}

	[IteratorStateMachine(typeof(_003CSayWhileFrozen_003Ed__84))]
	public static IEnumerator SayWhileFrozen(string dialog, params Func<IEnumerator>[] events)
	{
		Textbox textbox = new Textbox(dialog, events);
		textbox.Tag |= Tags.FrozenUpdate;
		Engine.Scene.Add(textbox);
		while (textbox.Opened)
		{
			yield return null;
		}
	}
}
