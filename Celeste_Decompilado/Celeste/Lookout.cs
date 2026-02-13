using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

[Tracked(false)]
public class Lookout : Entity
{
	private class Hud : Entity
	{
		public bool TrackMode;

		public float TrackPercent;

		public bool OnlyY;

		public float Easer;

		private float timerUp;

		private float timerDown;

		private float timerLeft;

		private float timerRight;

		private float multUp;

		private float multDown;

		private float multLeft;

		private float multRight;

		private float left;

		private float right;

		private float up;

		private float down;

		private Vector2 aim;

		private MTexture halfDot = GFX.Gui["dot"].GetSubtexture(0, 0, 64, 32);

		public Hud()
		{
			AddTag(Tags.HUD);
		}

		public override void Update()
		{
			Level level = SceneAs<Level>();
			Vector2 position = level.Camera.Position;
			Rectangle bounds = level.Bounds;
			int num = 320;
			int num2 = 180;
			bool flag = base.Scene.CollideCheck<LookoutBlocker>(new Rectangle((int)(position.X - 8f), (int)position.Y, num, num2));
			bool flag2 = base.Scene.CollideCheck<LookoutBlocker>(new Rectangle((int)(position.X + 8f), (int)position.Y, num, num2));
			bool flag3 = (TrackMode && TrackPercent >= 1f) || base.Scene.CollideCheck<LookoutBlocker>(new Rectangle((int)position.X, (int)(position.Y - 8f), num, num2));
			bool flag4 = (TrackMode && TrackPercent <= 0f) || base.Scene.CollideCheck<LookoutBlocker>(new Rectangle((int)position.X, (int)(position.Y + 8f), num, num2));
			left = Calc.Approach(left, (!flag && position.X > (float)(bounds.Left + 2)) ? 1 : 0, Engine.DeltaTime * 8f);
			right = Calc.Approach(right, (!flag2 && position.X + (float)num < (float)(bounds.Right - 2)) ? 1 : 0, Engine.DeltaTime * 8f);
			up = Calc.Approach(up, (!flag3 && position.Y > (float)(bounds.Top + 2)) ? 1 : 0, Engine.DeltaTime * 8f);
			down = Calc.Approach(down, (!flag4 && position.Y + (float)num2 < (float)(bounds.Bottom - 2)) ? 1 : 0, Engine.DeltaTime * 8f);
			aim = Input.Aim.Value;
			if (aim.X < 0f)
			{
				multLeft = Calc.Approach(multLeft, 0f, Engine.DeltaTime * 2f);
				timerLeft += Engine.DeltaTime * 12f;
			}
			else
			{
				multLeft = Calc.Approach(multLeft, 1f, Engine.DeltaTime * 2f);
				timerLeft += Engine.DeltaTime * 6f;
			}
			if (aim.X > 0f)
			{
				multRight = Calc.Approach(multRight, 0f, Engine.DeltaTime * 2f);
				timerRight += Engine.DeltaTime * 12f;
			}
			else
			{
				multRight = Calc.Approach(multRight, 1f, Engine.DeltaTime * 2f);
				timerRight += Engine.DeltaTime * 6f;
			}
			if (aim.Y < 0f)
			{
				multUp = Calc.Approach(multUp, 0f, Engine.DeltaTime * 2f);
				timerUp += Engine.DeltaTime * 12f;
			}
			else
			{
				multUp = Calc.Approach(multUp, 1f, Engine.DeltaTime * 2f);
				timerUp += Engine.DeltaTime * 6f;
			}
			if (aim.Y > 0f)
			{
				multDown = Calc.Approach(multDown, 0f, Engine.DeltaTime * 2f);
				timerDown += Engine.DeltaTime * 12f;
			}
			else
			{
				multDown = Calc.Approach(multDown, 1f, Engine.DeltaTime * 2f);
				timerDown += Engine.DeltaTime * 6f;
			}
			base.Update();
		}

		public override void Render()
		{
			Level level = base.Scene as Level;
			float num = Ease.CubeInOut(Easer);
			Color color = Color.White * num;
			int num2 = (int)(80f * num);
			int num3 = (int)(80f * num * 0.5625f);
			int num4 = 8;
			if (level.FrozenOrPaused || level.RetryPlayerCorpse != null)
			{
				color *= 0.25f;
			}
			Draw.Rect(num2, num3, 1920 - num2 * 2 - num4, num4, color);
			Draw.Rect(num2, num3 + num4, num4 + 2, 1080 - num3 * 2 - num4, color);
			Draw.Rect(1920 - num2 - num4 - 2, num3, num4 + 2, 1080 - num3 * 2 - num4, color);
			Draw.Rect(num2 + num4, 1080 - num3 - num4, 1920 - num2 * 2 - num4, num4, color);
			if (level.FrozenOrPaused || level.RetryPlayerCorpse != null)
			{
				return;
			}
			MTexture mTexture = GFX.Gui["towerarrow"];
			float y = (float)num3 * up - (float)(Math.Sin(timerUp) * 18.0 * (double)MathHelper.Lerp(0.5f, 1f, multUp)) - (1f - multUp) * 12f;
			mTexture.DrawCentered(new Vector2(960f, y), color * up, 1f, (float)Math.PI / 2f);
			float y2 = 1080f - (float)num3 * down + (float)(Math.Sin(timerDown) * 18.0 * (double)MathHelper.Lerp(0.5f, 1f, multDown)) + (1f - multDown) * 12f;
			mTexture.DrawCentered(new Vector2(960f, y2), color * down, 1f, 4.712389f);
			if (!TrackMode && !OnlyY)
			{
				float num5 = left;
				float num6 = multLeft;
				float num7 = timerLeft;
				float num8 = right;
				float num9 = multRight;
				float num10 = timerRight;
				if (SaveData.Instance != null && SaveData.Instance.Assists.MirrorMode)
				{
					num5 = right;
					num6 = multRight;
					num7 = timerRight;
					num8 = left;
					num9 = multLeft;
					num10 = timerLeft;
				}
				float x = (float)num2 * num5 - (float)(Math.Sin(num7) * 18.0 * (double)MathHelper.Lerp(0.5f, 1f, num6)) - (1f - num6) * 12f;
				mTexture.DrawCentered(new Vector2(x, 540f), color * num5);
				float x2 = 1920f - (float)num2 * num8 + (float)(Math.Sin(num10) * 18.0 * (double)MathHelper.Lerp(0.5f, 1f, num9)) + (1f - num9) * 12f;
				mTexture.DrawCentered(new Vector2(x2, 540f), color * num8, 1f, (float)Math.PI);
			}
			else if (TrackMode)
			{
				int num11 = 1080 - num3 * 2 - 128 - 64;
				int num12 = 1920 - num2 - 64;
				float num13 = (float)(1080 - num11) / 2f + 32f;
				Draw.Rect(num12 - 7, num13 + 7f, 14f, num11 - 14, Color.Black * num);
				halfDot.DrawJustified(new Vector2(num12, num13 + 7f), new Vector2(0.5f, 1f), Color.Black * num);
				halfDot.DrawJustified(new Vector2(num12, num13 + (float)num11 - 7f), new Vector2(0.5f, 1f), Color.Black * num, new Vector2(1f, -1f));
				GFX.Gui["lookout/cursor"].DrawCentered(new Vector2(num12, num13 + (1f - TrackPercent) * (float)num11), Color.White * num, 1f);
				GFX.Gui["lookout/summit"].DrawCentered(new Vector2(num12, num13 - 64f), Color.White * num, 0.65f);
			}
		}
	}

	[CompilerGenerated]
	private sealed class _003CLookRoutine_003Ed__16 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Lookout _003C_003E4__this;

		public Player player;

		private Level _003Clevel_003E5__2;

		private float _003Caccel_003E5__3;

		private float _003Cmaxspd_003E5__4;

		private Vector2 _003Ccam_003E5__5;

		private Vector2 _003Cspeed_003E5__6;

		private Vector2 _003ClastDir_003E5__7;

		private Vector2 _003CcamStart_003E5__8;

		private Vector2 _003CcamStartCenter_003E5__9;

		private bool _003CatSummitTop_003E5__10;

		private float _003Cduration_003E5__11;

		private float _003Capproach_003E5__12;

		private Vector2 _003Cwas_003E5__13;

		private Vector2 _003Cdirection_003E5__14;

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
		public _003CLookRoutine_003Ed__16(int _003C_003E1__state)
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
			Lookout lookout = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				_003Clevel_003E5__2 = lookout.SceneAs<Level>();
				SandwichLava sandwichLava = lookout.Scene.Entities.FindFirst<SandwichLava>();
				if (sandwichLava != null)
				{
					sandwichLava.Waiting = true;
				}
				if (player.Holding != null)
				{
					player.Drop();
				}
				player.StateMachine.State = 11;
				_003C_003E2__current = player.DummyWalkToExact((int)lookout.X, walkBackwards: false, 1f, cancelOnFall: true);
				_003C_003E1__state = 1;
				return true;
			}
			case 1:
				_003C_003E1__state = -1;
				if (Math.Abs(lookout.X - player.X) > 4f || player.Dead || !player.OnGround())
				{
					if (!player.Dead)
					{
						player.StateMachine.State = 0;
					}
					return false;
				}
				Audio.Play("event:/game/general/lookout_use", lookout.Position);
				if (player.Facing == Facings.Right)
				{
					lookout.sprite.Play(lookout.animPrefix + "lookRight");
				}
				else
				{
					lookout.sprite.Play(lookout.animPrefix + "lookLeft");
				}
				player.Sprite.Visible = (player.Hair.Visible = false);
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				lookout.Scene.Add(lookout.hud = new Hud());
				lookout.hud.TrackMode = lookout.nodes != null;
				lookout.hud.OnlyY = lookout.onlyY;
				lookout.nodePercent = 0f;
				lookout.node = 0;
				Audio.Play("event:/ui/game/lookout_on");
				goto IL_025b;
			case 3:
				_003C_003E1__state = -1;
				goto IL_025b;
			case 4:
				_003C_003E1__state = -1;
				goto IL_0cab;
			case 5:
				_003C_003E1__state = -1;
				goto IL_0d73;
			case 6:
			{
				_003C_003E1__state = -1;
				_003Cduration_003E5__11 = 3f;
				_003Capproach_003E5__12 = 0f;
				Coroutine component = new Coroutine(_003Clevel_003E5__2.ZoomTo(new Vector2(160f, 90f), 2f, _003Cduration_003E5__11));
				lookout.Add(component);
				goto IL_0ea1;
			}
			case 7:
				_003C_003E1__state = -1;
				goto IL_0ea1;
			case 8:
				_003C_003E1__state = -1;
				_003Cduration_003E5__11 += Engine.DeltaTime / _003Capproach_003E5__12;
				goto IL_0ff0;
			case 9:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_0ea1:
				if (!Input.MenuCancel.Pressed && !Input.MenuConfirm.Pressed && !Input.Dash.Pressed && !Input.Jump.Pressed && lookout.interacting)
				{
					_003Capproach_003E5__12 = Calc.Approach(_003Capproach_003E5__12, 1f, Engine.DeltaTime / _003Cduration_003E5__11);
					Audio.SetMusicParam("escape", _003Capproach_003E5__12);
					_003C_003E2__current = null;
					_003C_003E1__state = 7;
					return true;
				}
				goto IL_0edc;
				IL_1051:
				Audio.SetMusicParam("escape", 0f);
				_003Clevel_003E5__2.ScreenPadding = 0f;
				_003Clevel_003E5__2.ZoomSnap(Vector2.Zero, 1f);
				lookout.Scene.Remove(lookout.hud);
				lookout.interacting = false;
				player.StateMachine.State = 0;
				_003C_003E2__current = null;
				_003C_003E1__state = 9;
				return true;
				IL_0edc:
				if ((_003CcamStart_003E5__8 - _003Clevel_003E5__2.Camera.Position).Length() > 600f)
				{
					_003Cwas_003E5__13 = _003Clevel_003E5__2.Camera.Position;
					_003Cdirection_003E5__14 = (_003Cwas_003E5__13 - _003CcamStart_003E5__8).SafeNormalize();
					_003Capproach_003E5__12 = (_003CatSummitTop_003E5__10 ? 1f : 0.5f);
					new FadeWipe(lookout.Scene, wipeIn: false).Duration = _003Capproach_003E5__12;
					_003Cduration_003E5__11 = 0f;
					goto IL_0ff0;
				}
				goto IL_1051;
				IL_0ce6:
				player.Sprite.Visible = (player.Hair.Visible = true);
				lookout.sprite.Play(lookout.animPrefix + "idle");
				Audio.Play("event:/ui/game/lookout_off");
				goto IL_0d73;
				IL_025b:
				if ((lookout.hud.Easer = Calc.Approach(lookout.hud.Easer, 1f, Engine.DeltaTime * 3f)) < 1f)
				{
					_003Clevel_003E5__2.ScreenPadding = (int)(Ease.CubeInOut(lookout.hud.Easer) * 16f);
					_003C_003E2__current = null;
					_003C_003E1__state = 3;
					return true;
				}
				_003Caccel_003E5__3 = 800f;
				_003Cmaxspd_003E5__4 = 240f;
				_003Ccam_003E5__5 = _003Clevel_003E5__2.Camera.Position;
				_003Cspeed_003E5__6 = Vector2.Zero;
				_003ClastDir_003E5__7 = Vector2.Zero;
				_003CcamStart_003E5__8 = _003Clevel_003E5__2.Camera.Position;
				_003CcamStartCenter_003E5__9 = _003CcamStart_003E5__8 + new Vector2(160f, 90f);
				goto IL_0cab;
				IL_0ff0:
				if (_003Cduration_003E5__11 < 1f)
				{
					_003Clevel_003E5__2.Camera.Position = _003Cwas_003E5__13 - _003Cdirection_003E5__14 * MathHelper.Lerp(0f, 64f, Ease.CubeIn(_003Cduration_003E5__11));
					_003C_003E2__current = null;
					_003C_003E1__state = 8;
					return true;
				}
				_003Clevel_003E5__2.Camera.Position = _003CcamStart_003E5__8 + _003Cdirection_003E5__14 * 32f;
				new FadeWipe(lookout.Scene, wipeIn: true);
				_003Cwas_003E5__13 = default(Vector2);
				_003Cdirection_003E5__14 = default(Vector2);
				goto IL_1051;
				IL_0cab:
				if (!Input.MenuCancel.Pressed && !Input.MenuConfirm.Pressed && !Input.Dash.Pressed && !Input.Jump.Pressed && lookout.interacting)
				{
					Vector2 value = Input.Aim.Value;
					if (lookout.onlyY)
					{
						value.X = 0f;
					}
					if (Math.Sign(value.X) != Math.Sign(_003ClastDir_003E5__7.X) || Math.Sign(value.Y) != Math.Sign(_003ClastDir_003E5__7.Y))
					{
						Audio.Play("event:/game/general/lookout_move", lookout.Position);
					}
					_003ClastDir_003E5__7 = value;
					if (lookout.sprite.CurrentAnimationID != "lookLeft" && lookout.sprite.CurrentAnimationID != "lookRight")
					{
						if (value.X == 0f)
						{
							if (value.Y == 0f)
							{
								lookout.sprite.Play(lookout.animPrefix + "looking");
							}
							else if (value.Y > 0f)
							{
								lookout.sprite.Play(lookout.animPrefix + "lookingDown");
							}
							else
							{
								lookout.sprite.Play(lookout.animPrefix + "lookingUp");
							}
						}
						else if (value.X > 0f)
						{
							if (value.Y == 0f)
							{
								lookout.sprite.Play(lookout.animPrefix + "lookingRight");
							}
							else if (value.Y > 0f)
							{
								lookout.sprite.Play(lookout.animPrefix + "lookingDownRight");
							}
							else
							{
								lookout.sprite.Play(lookout.animPrefix + "lookingUpRight");
							}
						}
						else if (value.X < 0f)
						{
							if (value.Y == 0f)
							{
								lookout.sprite.Play(lookout.animPrefix + "lookingLeft");
							}
							else if (value.Y > 0f)
							{
								lookout.sprite.Play(lookout.animPrefix + "lookingDownLeft");
							}
							else
							{
								lookout.sprite.Play(lookout.animPrefix + "lookingUpLeft");
							}
						}
					}
					if (lookout.nodes == null)
					{
						_003Cspeed_003E5__6 += _003Caccel_003E5__3 * value * Engine.DeltaTime;
						if (value.X == 0f)
						{
							_003Cspeed_003E5__6.X = Calc.Approach(_003Cspeed_003E5__6.X, 0f, _003Caccel_003E5__3 * 2f * Engine.DeltaTime);
						}
						if (value.Y == 0f)
						{
							_003Cspeed_003E5__6.Y = Calc.Approach(_003Cspeed_003E5__6.Y, 0f, _003Caccel_003E5__3 * 2f * Engine.DeltaTime);
						}
						if (_003Cspeed_003E5__6.Length() > _003Cmaxspd_003E5__4)
						{
							_003Cspeed_003E5__6 = _003Cspeed_003E5__6.SafeNormalize(_003Cmaxspd_003E5__4);
						}
						Vector2 vector = _003Ccam_003E5__5;
						List<Entity> entities = lookout.Scene.Tracker.GetEntities<LookoutBlocker>();
						_003Ccam_003E5__5.X += _003Cspeed_003E5__6.X * Engine.DeltaTime;
						if (_003Ccam_003E5__5.X < (float)_003Clevel_003E5__2.Bounds.Left || _003Ccam_003E5__5.X + 320f > (float)_003Clevel_003E5__2.Bounds.Right)
						{
							_003Cspeed_003E5__6.X = 0f;
						}
						_003Ccam_003E5__5.X = Calc.Clamp(_003Ccam_003E5__5.X, _003Clevel_003E5__2.Bounds.Left, _003Clevel_003E5__2.Bounds.Right - 320);
						foreach (Entity item in entities)
						{
							if (_003Ccam_003E5__5.X + 320f > item.Left && _003Ccam_003E5__5.Y + 180f > item.Top && _003Ccam_003E5__5.X < item.Right && _003Ccam_003E5__5.Y < item.Bottom)
							{
								_003Ccam_003E5__5.X = vector.X;
								_003Cspeed_003E5__6.X = 0f;
							}
						}
						_003Ccam_003E5__5.Y += _003Cspeed_003E5__6.Y * Engine.DeltaTime;
						if (_003Ccam_003E5__5.Y < (float)_003Clevel_003E5__2.Bounds.Top || _003Ccam_003E5__5.Y + 180f > (float)_003Clevel_003E5__2.Bounds.Bottom)
						{
							_003Cspeed_003E5__6.Y = 0f;
						}
						_003Ccam_003E5__5.Y = Calc.Clamp(_003Ccam_003E5__5.Y, _003Clevel_003E5__2.Bounds.Top, _003Clevel_003E5__2.Bounds.Bottom - 180);
						foreach (Entity item2 in entities)
						{
							if (_003Ccam_003E5__5.X + 320f > item2.Left && _003Ccam_003E5__5.Y + 180f > item2.Top && _003Ccam_003E5__5.X < item2.Right && _003Ccam_003E5__5.Y < item2.Bottom)
							{
								_003Ccam_003E5__5.Y = vector.Y;
								_003Cspeed_003E5__6.Y = 0f;
							}
						}
						_003Clevel_003E5__2.Camera.Position = _003Ccam_003E5__5;
					}
					else
					{
						Vector2 vector2 = ((lookout.node <= 0) ? _003CcamStartCenter_003E5__9 : lookout.nodes[lookout.node - 1]);
						Vector2 vector3 = lookout.nodes[lookout.node];
						float num2 = (vector2 - vector3).Length();
						(vector3 - vector2).SafeNormalize();
						if (lookout.nodePercent < 0.25f && lookout.node > 0)
						{
							Vector2 begin = Vector2.Lerp((lookout.node <= 1) ? _003CcamStartCenter_003E5__9 : lookout.nodes[lookout.node - 2], vector2, 0.75f);
							Vector2 end = Vector2.Lerp(vector2, vector3, 0.25f);
							SimpleCurve simpleCurve = new SimpleCurve(begin, end, vector2);
							_003Clevel_003E5__2.Camera.Position = simpleCurve.GetPoint(0.5f + lookout.nodePercent / 0.25f * 0.5f);
						}
						else if (lookout.nodePercent > 0.75f && lookout.node < lookout.nodes.Count - 1)
						{
							Vector2 value2 = lookout.nodes[lookout.node + 1];
							Vector2 begin2 = Vector2.Lerp(vector2, vector3, 0.75f);
							Vector2 end2 = Vector2.Lerp(vector3, value2, 0.25f);
							SimpleCurve simpleCurve2 = new SimpleCurve(begin2, end2, vector3);
							_003Clevel_003E5__2.Camera.Position = simpleCurve2.GetPoint((lookout.nodePercent - 0.75f) / 0.25f * 0.5f);
						}
						else
						{
							_003Clevel_003E5__2.Camera.Position = Vector2.Lerp(vector2, vector3, lookout.nodePercent);
						}
						_003Clevel_003E5__2.Camera.Position += new Vector2(-160f, -90f);
						lookout.nodePercent -= value.Y * (_003Cmaxspd_003E5__4 / num2) * Engine.DeltaTime;
						if (lookout.nodePercent < 0f)
						{
							if (lookout.node > 0)
							{
								lookout.node--;
								lookout.nodePercent = 1f;
							}
							else
							{
								lookout.nodePercent = 0f;
							}
						}
						else if (lookout.nodePercent > 1f)
						{
							if (lookout.node < lookout.nodes.Count - 1)
							{
								lookout.node++;
								lookout.nodePercent = 0f;
							}
							else
							{
								lookout.nodePercent = 1f;
								if (lookout.summit)
								{
									goto IL_0ce6;
								}
							}
						}
						float num3 = 0f;
						float num4 = 0f;
						for (int i = 0; i < lookout.nodes.Count; i++)
						{
							float num5 = (((i == 0) ? _003CcamStartCenter_003E5__9 : lookout.nodes[i - 1]) - lookout.nodes[i]).Length();
							num4 += num5;
							if (i < lookout.node)
							{
								num3 += num5;
							}
							else if (i == lookout.node)
							{
								num3 += num5 * lookout.nodePercent;
							}
						}
						lookout.hud.TrackPercent = num3 / num4;
					}
					_003C_003E2__current = null;
					_003C_003E1__state = 4;
					return true;
				}
				goto IL_0ce6;
				IL_0d73:
				if ((lookout.hud.Easer = Calc.Approach(lookout.hud.Easer, 0f, Engine.DeltaTime * 3f)) > 0f)
				{
					_003Clevel_003E5__2.ScreenPadding = (int)(Ease.CubeInOut(lookout.hud.Easer) * 16f);
					_003C_003E2__current = null;
					_003C_003E1__state = 5;
					return true;
				}
				_003CatSummitTop_003E5__10 = lookout.summit && lookout.node >= lookout.nodes.Count - 1 && lookout.nodePercent >= 0.95f;
				if (_003CatSummitTop_003E5__10)
				{
					_003C_003E2__current = 0.5f;
					_003C_003E1__state = 6;
					return true;
				}
				goto IL_0edc;
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

	private TalkComponent talk;

	private Hud hud;

	private Sprite sprite;

	private Tween lightTween;

	private bool interacting;

	private bool onlyY;

	private List<Vector2> nodes;

	private int node;

	private float nodePercent;

	private bool summit;

	private string animPrefix = "";

	public Lookout(EntityData data, Vector2 offset)
		: base(data.Position + offset)
	{
		base.Depth = -8500;
		Add(talk = new TalkComponent(new Rectangle(-24, -8, 48, 8), new Vector2(-0.5f, -20f), Interact));
		talk.PlayerMustBeFacing = false;
		summit = data.Bool("summit");
		onlyY = data.Bool("onlyY");
		base.Collider = new Hitbox(4f, 4f, -2f, -4f);
		VertexLight vertexLight = new VertexLight(new Vector2(-1f, -11f), Color.White, 0.8f, 16, 24);
		Add(vertexLight);
		lightTween = vertexLight.CreatePulseTween();
		Add(lightTween);
		Add(sprite = GFX.SpriteBank.Create("lookout"));
		sprite.OnFrameChange = delegate(string s)
		{
			switch (s)
			{
			case "idle":
			case "badeline_idle":
			case "nobackpack_idle":
				if (sprite.CurrentAnimationFrame == sprite.CurrentAnimationTotalFrames - 1)
				{
					lightTween.Start();
				}
				break;
			}
		};
		Vector2[] array = data.NodesOffset(offset);
		if (array != null && array.Length != 0)
		{
			nodes = new List<Vector2>(array);
		}
	}

	public override void Removed(Scene scene)
	{
		base.Removed(scene);
		if (interacting)
		{
			Player entity = scene.Tracker.GetEntity<Player>();
			if (entity != null)
			{
				entity.StateMachine.State = 0;
			}
		}
	}

	private void Interact(Player player)
	{
		if (player.DefaultSpriteMode == PlayerSpriteMode.MadelineAsBadeline || SaveData.Instance.Assists.PlayAsBadeline)
		{
			animPrefix = "badeline_";
		}
		else if (player.DefaultSpriteMode == PlayerSpriteMode.MadelineNoBackpack)
		{
			animPrefix = "nobackpack_";
		}
		else
		{
			animPrefix = "";
		}
		Coroutine coroutine = new Coroutine(LookRoutine(player));
		coroutine.RemoveOnComplete = true;
		Add(coroutine);
		interacting = true;
	}

	public void StopInteracting()
	{
		interacting = false;
		sprite.Play(animPrefix + "idle");
	}

	public override void Update()
	{
		base.Update();
		Player entity = base.Scene.Tracker.GetEntity<Player>();
		if (entity != null)
		{
			sprite.Active = interacting || entity.StateMachine.State != 11;
			if (!sprite.Active)
			{
				sprite.SetAnimationFrame(0);
			}
		}
		if (talk != null && CollideCheck<Solid>())
		{
			Remove(talk);
			talk = null;
		}
	}

	[IteratorStateMachine(typeof(_003CLookRoutine_003Ed__16))]
	private IEnumerator LookRoutine(Player player)
	{
		Level level = SceneAs<Level>();
		SandwichLava sandwichLava = base.Scene.Entities.FindFirst<SandwichLava>();
		if (sandwichLava != null)
		{
			sandwichLava.Waiting = true;
		}
		if (player.Holding != null)
		{
			player.Drop();
		}
		player.StateMachine.State = 11;
		yield return player.DummyWalkToExact((int)base.X, walkBackwards: false, 1f, cancelOnFall: true);
		if (Math.Abs(base.X - player.X) > 4f || player.Dead || !player.OnGround())
		{
			if (!player.Dead)
			{
				player.StateMachine.State = 0;
			}
			yield break;
		}
		Audio.Play("event:/game/general/lookout_use", Position);
		if (player.Facing == Facings.Right)
		{
			sprite.Play(animPrefix + "lookRight");
		}
		else
		{
			sprite.Play(animPrefix + "lookLeft");
		}
		PlayerSprite playerSprite = player.Sprite;
		PlayerHair hair = player.Hair;
		bool visible = false;
		hair.Visible = false;
		playerSprite.Visible = visible;
		yield return 0.2f;
		base.Scene.Add(hud = new Hud());
		hud.TrackMode = nodes != null;
		hud.OnlyY = onlyY;
		nodePercent = 0f;
		node = 0;
		Audio.Play("event:/ui/game/lookout_on");
		while ((hud.Easer = Calc.Approach(hud.Easer, 1f, Engine.DeltaTime * 3f)) < 1f)
		{
			level.ScreenPadding = (int)(Ease.CubeInOut(hud.Easer) * 16f);
			yield return null;
		}
		float accel = 800f;
		float maxspd = 240f;
		Vector2 cam = level.Camera.Position;
		Vector2 speed = Vector2.Zero;
		Vector2 lastDir = Vector2.Zero;
		Vector2 camStart = level.Camera.Position;
		Vector2 camStartCenter = camStart + new Vector2(160f, 90f);
		while (!Input.MenuCancel.Pressed && !Input.MenuConfirm.Pressed && !Input.Dash.Pressed && !Input.Jump.Pressed && interacting)
		{
			Vector2 value = Input.Aim.Value;
			if (onlyY)
			{
				value.X = 0f;
			}
			if (Math.Sign(value.X) != Math.Sign(lastDir.X) || Math.Sign(value.Y) != Math.Sign(lastDir.Y))
			{
				Audio.Play("event:/game/general/lookout_move", Position);
			}
			lastDir = value;
			if (sprite.CurrentAnimationID != "lookLeft" && sprite.CurrentAnimationID != "lookRight")
			{
				if (value.X == 0f)
				{
					if (value.Y == 0f)
					{
						sprite.Play(animPrefix + "looking");
					}
					else if (value.Y > 0f)
					{
						sprite.Play(animPrefix + "lookingDown");
					}
					else
					{
						sprite.Play(animPrefix + "lookingUp");
					}
				}
				else if (value.X > 0f)
				{
					if (value.Y == 0f)
					{
						sprite.Play(animPrefix + "lookingRight");
					}
					else if (value.Y > 0f)
					{
						sprite.Play(animPrefix + "lookingDownRight");
					}
					else
					{
						sprite.Play(animPrefix + "lookingUpRight");
					}
				}
				else if (value.X < 0f)
				{
					if (value.Y == 0f)
					{
						sprite.Play(animPrefix + "lookingLeft");
					}
					else if (value.Y > 0f)
					{
						sprite.Play(animPrefix + "lookingDownLeft");
					}
					else
					{
						sprite.Play(animPrefix + "lookingUpLeft");
					}
				}
			}
			if (nodes == null)
			{
				speed += accel * value * Engine.DeltaTime;
				if (value.X == 0f)
				{
					speed.X = Calc.Approach(speed.X, 0f, accel * 2f * Engine.DeltaTime);
				}
				if (value.Y == 0f)
				{
					speed.Y = Calc.Approach(speed.Y, 0f, accel * 2f * Engine.DeltaTime);
				}
				if (speed.Length() > maxspd)
				{
					speed = speed.SafeNormalize(maxspd);
				}
				Vector2 vector = cam;
				List<Entity> entities = base.Scene.Tracker.GetEntities<LookoutBlocker>();
				cam.X += speed.X * Engine.DeltaTime;
				if (cam.X < (float)level.Bounds.Left || cam.X + 320f > (float)level.Bounds.Right)
				{
					speed.X = 0f;
				}
				cam.X = Calc.Clamp(cam.X, level.Bounds.Left, level.Bounds.Right - 320);
				foreach (Entity item in entities)
				{
					if (cam.X + 320f > item.Left && cam.Y + 180f > item.Top && cam.X < item.Right && cam.Y < item.Bottom)
					{
						cam.X = vector.X;
						speed.X = 0f;
					}
				}
				cam.Y += speed.Y * Engine.DeltaTime;
				if (cam.Y < (float)level.Bounds.Top || cam.Y + 180f > (float)level.Bounds.Bottom)
				{
					speed.Y = 0f;
				}
				cam.Y = Calc.Clamp(cam.Y, level.Bounds.Top, level.Bounds.Bottom - 180);
				foreach (Entity item2 in entities)
				{
					if (cam.X + 320f > item2.Left && cam.Y + 180f > item2.Top && cam.X < item2.Right && cam.Y < item2.Bottom)
					{
						cam.Y = vector.Y;
						speed.Y = 0f;
					}
				}
				level.Camera.Position = cam;
			}
			else
			{
				Vector2 vector2 = ((node <= 0) ? camStartCenter : nodes[node - 1]);
				Vector2 vector3 = nodes[node];
				float num = (vector2 - vector3).Length();
				(vector3 - vector2).SafeNormalize();
				if (nodePercent < 0.25f && node > 0)
				{
					Vector2 begin = Vector2.Lerp((node <= 1) ? camStartCenter : nodes[node - 2], vector2, 0.75f);
					Vector2 end = Vector2.Lerp(vector2, vector3, 0.25f);
					SimpleCurve simpleCurve = new SimpleCurve(begin, end, vector2);
					level.Camera.Position = simpleCurve.GetPoint(0.5f + nodePercent / 0.25f * 0.5f);
				}
				else if (nodePercent > 0.75f && node < nodes.Count - 1)
				{
					Vector2 value2 = nodes[node + 1];
					Vector2 begin2 = Vector2.Lerp(vector2, vector3, 0.75f);
					Vector2 end2 = Vector2.Lerp(vector3, value2, 0.25f);
					SimpleCurve simpleCurve2 = new SimpleCurve(begin2, end2, vector3);
					level.Camera.Position = simpleCurve2.GetPoint((nodePercent - 0.75f) / 0.25f * 0.5f);
				}
				else
				{
					level.Camera.Position = Vector2.Lerp(vector2, vector3, nodePercent);
				}
				level.Camera.Position += new Vector2(-160f, -90f);
				nodePercent -= value.Y * (maxspd / num) * Engine.DeltaTime;
				if (nodePercent < 0f)
				{
					if (node > 0)
					{
						node--;
						nodePercent = 1f;
					}
					else
					{
						nodePercent = 0f;
					}
				}
				else if (nodePercent > 1f)
				{
					if (node < nodes.Count - 1)
					{
						node++;
						nodePercent = 0f;
					}
					else
					{
						nodePercent = 1f;
						if (summit)
						{
							break;
						}
					}
				}
				float num2 = 0f;
				float num3 = 0f;
				for (int i = 0; i < nodes.Count; i++)
				{
					float num4 = (((i == 0) ? camStartCenter : nodes[i - 1]) - nodes[i]).Length();
					num3 += num4;
					if (i < node)
					{
						num2 += num4;
					}
					else if (i == node)
					{
						num2 += num4 * nodePercent;
					}
				}
				hud.TrackPercent = num2 / num3;
			}
			yield return null;
		}
		PlayerSprite playerSprite2 = player.Sprite;
		PlayerHair hair2 = player.Hair;
		visible = true;
		hair2.Visible = true;
		playerSprite2.Visible = visible;
		sprite.Play(animPrefix + "idle");
		Audio.Play("event:/ui/game/lookout_off");
		while ((hud.Easer = Calc.Approach(hud.Easer, 0f, Engine.DeltaTime * 3f)) > 0f)
		{
			level.ScreenPadding = (int)(Ease.CubeInOut(hud.Easer) * 16f);
			yield return null;
		}
		bool atSummitTop = summit && node >= nodes.Count - 1 && nodePercent >= 0.95f;
		if (atSummitTop)
		{
			yield return 0.5f;
			float duration = 3f;
			float approach = 0f;
			Coroutine component = new Coroutine(level.ZoomTo(new Vector2(160f, 90f), 2f, duration));
			Add(component);
			while (!Input.MenuCancel.Pressed && !Input.MenuConfirm.Pressed && !Input.Dash.Pressed && !Input.Jump.Pressed && interacting)
			{
				approach = Calc.Approach(approach, 1f, Engine.DeltaTime / duration);
				Audio.SetMusicParam("escape", approach);
				yield return null;
			}
		}
		if ((camStart - level.Camera.Position).Length() > 600f)
		{
			Vector2 was = level.Camera.Position;
			Vector2 direction = (was - camStart).SafeNormalize();
			float approach = (atSummitTop ? 1f : 0.5f);
			new FadeWipe(base.Scene, wipeIn: false).Duration = approach;
			for (float duration = 0f; duration < 1f; duration += Engine.DeltaTime / approach)
			{
				level.Camera.Position = was - direction * MathHelper.Lerp(0f, 64f, Ease.CubeIn(duration));
				yield return null;
			}
			level.Camera.Position = camStart + direction * 32f;
			new FadeWipe(base.Scene, wipeIn: true);
		}
		Audio.SetMusicParam("escape", 0f);
		level.ScreenPadding = 0f;
		level.ZoomSnap(Vector2.Zero, 1f);
		base.Scene.Remove(hud);
		interacting = false;
		player.StateMachine.State = 0;
		yield return null;
	}
}
