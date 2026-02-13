using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS07_Credits : CutsceneEntity
{
	private class Fill : Backdrop
	{
		public override void Render(Scene scene)
		{
			Draw.Rect(-10f, -10f, 340f, 200f, Color);
		}
	}

	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass24_0
	{
		public NPC oshiro;

		public CS07_Credits _003C_003E4__this;

		internal void _003CRoutine_003Eb__2(string anim)
		{
			if (oshiro.Sprite.CurrentAnimationFrame == 10)
			{
				Entity entity = new Entity(oshiro.Position)
				{
					Depth = oshiro.Depth + 1
				};
				_003C_003E4__this.Scene.Add(entity);
				entity.Add(new Image(GFX.Game["characters/oshiro/broom"])
				{
					Origin = oshiro.Sprite.Origin
				});
				oshiro.Sprite.OnFrameChange = null;
			}
		}
	}

	[CompilerGenerated]
	private sealed class _003CRoutine_003Ed__24 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS07_Credits _003C_003E4__this;

		private _003C_003Ec__DisplayClass24_0 _003C_003E8__1;

		private Petals _003Cpetals_003E5__2;

		private Player _003Cother_003E5__3;

		private float _003CplayerHighJump_003E5__4;

		private float _003CbaddyHighJump_003E5__5;

		private List<Entity>.Enumerator _003C_003E7__wrap5;

		private float _003Cp_003E5__7;

		private Vector2 _003CoshiroTarget_003E5__8;

		private Coroutine _003CoshiroRoutine_003E5__9;

		private BirdNPC _003Cbird_003E5__10;

		private Vector2 _003Cfrom_003E5__11;

		private Vector2 _003Cto_003E5__12;

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
		public _003CRoutine_003Ed__24(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			int num = _003C_003E1__state;
			if (num == -3 || num == 35)
			{
				try
				{
				}
				finally
				{
					_003C_003Em__Finally1();
				}
			}
		}

		private bool MoveNext()
		{
			try
			{
				int num = _003C_003E1__state;
				CS07_Credits CS_0024_003C_003E8__locals209 = _003C_003E4__this;
				Vector2 target3;
				switch (num)
				{
				default:
					return false;
				case 0:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.Level.Background.Backdrops.Add(CS_0024_003C_003E8__locals209.fillbg = new Fill());
					CS_0024_003C_003E8__locals209.Level.Completed = true;
					CS_0024_003C_003E8__locals209.Level.Entities.FindFirst<SpeedrunTimerDisplay>()?.RemoveSelf();
					CS_0024_003C_003E8__locals209.Level.Entities.FindFirst<TotalStrawberriesDisplay>()?.RemoveSelf();
					CS_0024_003C_003E8__locals209.Level.Entities.FindFirst<GameplayStats>()?.RemoveSelf();
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				case 1:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.Level.Wipe.Cancel();
					_003C_003E2__current = 0.5f;
					_003C_003E1__state = 2;
					return true;
				case 2:
				{
					_003C_003E1__state = -1;
					float alignment = 1f;
					if (SaveData.Instance != null && SaveData.Instance.Assists.MirrorMode)
					{
						alignment = 0f;
					}
					CS_0024_003C_003E8__locals209.credits = new Credits(alignment, 0.6f, haveTitle: false, havePolaroids: true);
					CS_0024_003C_003E8__locals209.credits.AllowInput = false;
					_003C_003E2__current = 3f;
					_003C_003E1__state = 3;
					return true;
				}
				case 3:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.SetBgFade(0f);
					CS_0024_003C_003E8__locals209.Add(new Coroutine(CS_0024_003C_003E8__locals209.FadeTo(0f)));
					_003C_003E2__current = CS_0024_003C_003E8__locals209.SetupLevel();
					_003C_003E1__state = 4;
					return true;
				case 4:
					_003C_003E1__state = -1;
					_003C_003E2__current = CS_0024_003C_003E8__locals209.WaitForPlayer();
					_003C_003E1__state = 5;
					return true;
				case 5:
					_003C_003E1__state = -1;
					_003C_003E2__current = CS_0024_003C_003E8__locals209.FadeTo(1f);
					_003C_003E1__state = 6;
					return true;
				case 6:
					_003C_003E1__state = -1;
					_003C_003E2__current = 1f;
					_003C_003E1__state = 7;
					return true;
				case 7:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.SetBgFade(0.1f);
					_003C_003E2__current = CS_0024_003C_003E8__locals209.NextLevel("credits-dashes");
					_003C_003E1__state = 8;
					return true;
				case 8:
					_003C_003E1__state = -1;
					_003C_003E2__current = CS_0024_003C_003E8__locals209.SetupLevel();
					_003C_003E1__state = 9;
					return true;
				case 9:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.Add(new Coroutine(CS_0024_003C_003E8__locals209.FadeTo(0f)));
					_003C_003E2__current = CS_0024_003C_003E8__locals209.WaitForPlayer();
					_003C_003E1__state = 10;
					return true;
				case 10:
					_003C_003E1__state = -1;
					_003C_003E2__current = CS_0024_003C_003E8__locals209.FadeTo(1f);
					_003C_003E1__state = 11;
					return true;
				case 11:
					_003C_003E1__state = -1;
					_003C_003E2__current = 1f;
					_003C_003E1__state = 12;
					return true;
				case 12:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.SetBgFade(0.2f);
					_003C_003E2__current = CS_0024_003C_003E8__locals209.NextLevel("credits-walking");
					_003C_003E1__state = 13;
					return true;
				case 13:
					_003C_003E1__state = -1;
					_003C_003E2__current = CS_0024_003C_003E8__locals209.SetupLevel();
					_003C_003E1__state = 14;
					return true;
				case 14:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.Add(new Coroutine(CS_0024_003C_003E8__locals209.FadeTo(0f)));
					_003C_003E2__current = 5.8f;
					_003C_003E1__state = 15;
					return true;
				case 15:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.badelineAutoFloat = false;
					_003C_003E2__current = 0.5f;
					_003C_003E1__state = 16;
					return true;
				case 16:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.badeline.Sprite.Scale.X = 1f;
					_003C_003E2__current = 0.5f;
					_003C_003E1__state = 17;
					return true;
				case 17:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.autoWalk = false;
					CS_0024_003C_003E8__locals209.player.Speed = Vector2.Zero;
					CS_0024_003C_003E8__locals209.player.Facing = Facings.Right;
					_003C_003E2__current = 1.5f;
					_003C_003E1__state = 18;
					return true;
				case 18:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.badeline.Sprite.Scale.X = -1f;
					_003C_003E2__current = 1f;
					_003C_003E1__state = 19;
					return true;
				case 19:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.badeline.Sprite.Scale.X = -1f;
					CS_0024_003C_003E8__locals209.badelineAutoWalk = true;
					CS_0024_003C_003E8__locals209.badelineWalkApproachFrom = CS_0024_003C_003E8__locals209.badeline.Position;
					CS_0024_003C_003E8__locals209.Add(new Coroutine(CS_0024_003C_003E8__locals209.BadelineApproachWalking()));
					_003C_003E2__current = 0.7f;
					_003C_003E1__state = 20;
					return true;
				case 20:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.autoWalk = true;
					CS_0024_003C_003E8__locals209.player.Facing = Facings.Left;
					_003C_003E2__current = CS_0024_003C_003E8__locals209.WaitForPlayer();
					_003C_003E1__state = 21;
					return true;
				case 21:
					_003C_003E1__state = -1;
					_003C_003E2__current = CS_0024_003C_003E8__locals209.FadeTo(1f);
					_003C_003E1__state = 22;
					return true;
				case 22:
					_003C_003E1__state = -1;
					_003C_003E2__current = 1f;
					_003C_003E1__state = 23;
					return true;
				case 23:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.SetBgFade(0.3f);
					_003C_003E2__current = CS_0024_003C_003E8__locals209.NextLevel("credits-tree");
					_003C_003E1__state = 24;
					return true;
				case 24:
					_003C_003E1__state = -1;
					_003C_003E2__current = CS_0024_003C_003E8__locals209.SetupLevel();
					_003C_003E1__state = 25;
					return true;
				case 25:
				{
					_003C_003E1__state = -1;
					_003Cpetals_003E5__2 = new Petals();
					CS_0024_003C_003E8__locals209.Level.Foreground.Backdrops.Add(_003Cpetals_003E5__2);
					CS_0024_003C_003E8__locals209.autoUpdateCamera = false;
					Vector2 target2 = CS_0024_003C_003E8__locals209.Level.Camera.Position + new Vector2(-220f, 32f);
					CS_0024_003C_003E8__locals209.Level.Camera.Position += new Vector2(-100f, 0f);
					CS_0024_003C_003E8__locals209.badelineWalkApproach = 1f;
					CS_0024_003C_003E8__locals209.badelineAutoFloat = false;
					CS_0024_003C_003E8__locals209.badelineAutoWalk = true;
					CS_0024_003C_003E8__locals209.badeline.Floatness = 0f;
					CS_0024_003C_003E8__locals209.Add(new Coroutine(CS_0024_003C_003E8__locals209.FadeTo(0f)));
					CS_0024_003C_003E8__locals209.Add(new Coroutine(CutsceneEntity.CameraTo(target2, 12f, Ease.Linear)));
					_003C_003E2__current = 3.5f;
					_003C_003E1__state = 26;
					return true;
				}
				case 26:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.badeline.Sprite.Play("idle");
					CS_0024_003C_003E8__locals209.badelineAutoWalk = false;
					_003C_003E2__current = 0.25f;
					_003C_003E1__state = 27;
					return true;
				case 27:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.autoWalk = false;
					CS_0024_003C_003E8__locals209.player.Sprite.Play("idle");
					CS_0024_003C_003E8__locals209.player.Speed = Vector2.Zero;
					CS_0024_003C_003E8__locals209.player.DummyAutoAnimate = false;
					CS_0024_003C_003E8__locals209.player.Facing = Facings.Right;
					_003C_003E2__current = 0.5f;
					_003C_003E1__state = 28;
					return true;
				case 28:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.player.Sprite.Play("sitDown");
					_003C_003E2__current = 4f;
					_003C_003E1__state = 29;
					return true;
				case 29:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.badeline.Sprite.Play("laugh");
					_003C_003E2__current = 1.75f;
					_003C_003E1__state = 30;
					return true;
				case 30:
					_003C_003E1__state = -1;
					_003C_003E2__current = CS_0024_003C_003E8__locals209.FadeTo(1f);
					_003C_003E1__state = 31;
					return true;
				case 31:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.Level.Foreground.Backdrops.Remove(_003Cpetals_003E5__2);
					_003Cpetals_003E5__2 = null;
					_003C_003E2__current = 1f;
					_003C_003E1__state = 32;
					return true;
				case 32:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.SetBgFade(0.4f);
					_003C_003E2__current = CS_0024_003C_003E8__locals209.NextLevel("credits-clouds");
					_003C_003E1__state = 33;
					return true;
				case 33:
					_003C_003E1__state = -1;
					_003C_003E2__current = CS_0024_003C_003E8__locals209.SetupLevel();
					_003C_003E1__state = 34;
					return true;
				case 34:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.autoWalk = false;
					CS_0024_003C_003E8__locals209.player.Speed = Vector2.Zero;
					CS_0024_003C_003E8__locals209.autoUpdateCamera = false;
					CS_0024_003C_003E8__locals209.player.ForceCameraUpdate = false;
					CS_0024_003C_003E8__locals209.badeline.Visible = false;
					_003Cother_003E5__3 = null;
					_003C_003E7__wrap5 = CS_0024_003C_003E8__locals209.Scene.Tracker.GetEntities<CreditsTrigger>().GetEnumerator();
					_003C_003E1__state = -3;
					goto IL_0af8;
				case 35:
					_003C_003E1__state = -3;
					_003Cother_003E5__3.StateMachine.State = 11;
					_003Cother_003E5__3.Facing = Facings.Left;
					CS_0024_003C_003E8__locals209.Scene.Add(_003Cother_003E5__3);
					goto IL_0af8;
				case 36:
					_003C_003E1__state = -1;
					_003Cp_003E5__7 += Engine.DeltaTime;
					goto IL_0d01;
				case 37:
					_003C_003E1__state = -1;
					_003Cother_003E5__3 = null;
					_003C_003E2__current = 1f;
					_003C_003E1__state = 38;
					return true;
				case 38:
					_003C_003E1__state = -1;
					_003C_003E8__1 = new _003C_003Ec__DisplayClass24_0();
					_003C_003E8__1._003C_003E4__this = CS_0024_003C_003E8__locals209;
					CS_0024_003C_003E8__locals209.SetBgFade(0.5f);
					_003C_003E2__current = CS_0024_003C_003E8__locals209.NextLevel("credits-resort");
					_003C_003E1__state = 39;
					return true;
				case 39:
					_003C_003E1__state = -1;
					_003C_003E2__current = CS_0024_003C_003E8__locals209.SetupLevel();
					_003C_003E1__state = 40;
					return true;
				case 40:
				{
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.Add(new Coroutine(CS_0024_003C_003E8__locals209.FadeTo(0f)));
					CS_0024_003C_003E8__locals209.badelineWalkApproach = 1f;
					CS_0024_003C_003E8__locals209.badelineAutoFloat = false;
					CS_0024_003C_003E8__locals209.badelineAutoWalk = true;
					CS_0024_003C_003E8__locals209.badeline.Floatness = 0f;
					Vector2 vector = Vector2.Zero;
					foreach (CreditsTrigger item in CS_0024_003C_003E8__locals209.Scene.Entities.FindAll<CreditsTrigger>())
					{
						if (item.Event == "Oshiro")
						{
							vector = item.Position;
						}
					}
					_003C_003E8__1.oshiro = new NPC(vector + new Vector2(0f, 4f));
					_003C_003E8__1.oshiro.Add(_003C_003E8__1.oshiro.Sprite = new OshiroSprite(1));
					_003C_003E8__1.oshiro.MoveAnim = "sweeping";
					_003C_003E8__1.oshiro.IdleAnim = "sweeping";
					_003C_003E8__1.oshiro.Sprite.Play("sweeping");
					_003C_003E8__1.oshiro.Maxspeed = 10f;
					_003C_003E8__1.oshiro.Depth = -60;
					CS_0024_003C_003E8__locals209.Scene.Add(_003C_003E8__1.oshiro);
					CS_0024_003C_003E8__locals209.Add(new Coroutine(CS_0024_003C_003E8__locals209.DustyRoutine(_003C_003E8__1.oshiro)));
					_003C_003E2__current = 4.8f;
					_003C_003E1__state = 41;
					return true;
				}
				case 41:
					_003C_003E1__state = -1;
					_003CoshiroTarget_003E5__8 = _003C_003E8__1.oshiro.Position + new Vector2(116f, 0f);
					_003CoshiroRoutine_003E5__9 = new Coroutine(_003C_003E8__1.oshiro.MoveTo(_003CoshiroTarget_003E5__8));
					CS_0024_003C_003E8__locals209.Add(_003CoshiroRoutine_003E5__9);
					_003C_003E2__current = 2f;
					_003C_003E1__state = 42;
					return true;
				case 42:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.autoUpdateCamera = false;
					_003C_003E2__current = CutsceneEntity.CameraTo(new Vector2(CS_0024_003C_003E8__locals209.Level.Bounds.Left + 64, CS_0024_003C_003E8__locals209.Level.Bounds.Top), 2f);
					_003C_003E1__state = 43;
					return true;
				case 43:
					_003C_003E1__state = -1;
					_003C_003E2__current = 5f;
					_003C_003E1__state = 44;
					return true;
				case 44:
					_003C_003E1__state = -1;
					_003Cbird_003E5__10 = new BirdNPC(_003C_003E8__1.oshiro.Position + new Vector2(280f, -160f), BirdNPC.Modes.None);
					_003Cbird_003E5__10.Depth = 10010;
					_003Cbird_003E5__10.Light.Visible = false;
					CS_0024_003C_003E8__locals209.Scene.Add(_003Cbird_003E5__10);
					_003Cbird_003E5__10.Facing = Facings.Left;
					_003Cbird_003E5__10.Sprite.Play("fall");
					_003Cfrom_003E5__11 = _003Cbird_003E5__10.Position;
					_003Cto_003E5__12 = _003CoshiroTarget_003E5__8 + new Vector2(50f, -12f);
					_003CbaddyHighJump_003E5__5 = 0f;
					goto IL_1216;
				case 45:
					_003C_003E1__state = -1;
					goto IL_1216;
				case 46:
					_003C_003E1__state = -1;
					_003Cbird_003E5__10.Sprite.Play("croak");
					_003C_003E2__current = 0.6f;
					_003C_003E1__state = 47;
					return true;
				case 47:
					_003C_003E1__state = -1;
					_003Cfrom_003E5__11 = default(Vector2);
					_003Cto_003E5__12 = default(Vector2);
					_003C_003E8__1.oshiro.Maxspeed = 40f;
					_003C_003E8__1.oshiro.MoveAnim = "move";
					_003C_003E8__1.oshiro.IdleAnim = "idle";
					_003C_003E2__current = _003C_003E8__1.oshiro.MoveTo(_003CoshiroTarget_003E5__8 + new Vector2(14f, 0f));
					_003C_003E1__state = 48;
					return true;
				case 48:
					_003C_003E1__state = -1;
					_003C_003E2__current = 2f;
					_003C_003E1__state = 49;
					return true;
				case 49:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.Add(new Coroutine(_003Cbird_003E5__10.StartleAndFlyAway()));
					_003C_003E2__current = 0.75f;
					_003C_003E1__state = 50;
					return true;
				case 50:
					_003C_003E1__state = -1;
					_003Cbird_003E5__10.Light.Visible = false;
					_003Cbird_003E5__10.Depth = 10010;
					_003C_003E8__1.oshiro.Sprite.Scale.X = -1f;
					_003C_003E2__current = CS_0024_003C_003E8__locals209.FadeTo(1f);
					_003C_003E1__state = 51;
					return true;
				case 51:
					_003C_003E1__state = -1;
					_003C_003E8__1 = null;
					_003CoshiroTarget_003E5__8 = default(Vector2);
					_003CoshiroRoutine_003E5__9 = null;
					_003Cbird_003E5__10 = null;
					_003C_003E2__current = 1f;
					_003C_003E1__state = 52;
					return true;
				case 52:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.SetBgFade(0.6f);
					_003C_003E2__current = CS_0024_003C_003E8__locals209.NextLevel("credits-wallslide");
					_003C_003E1__state = 53;
					return true;
				case 53:
					_003C_003E1__state = -1;
					_003C_003E2__current = CS_0024_003C_003E8__locals209.SetupLevel();
					_003C_003E1__state = 54;
					return true;
				case 54:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.badelineAutoFloat = false;
					CS_0024_003C_003E8__locals209.badeline.Floatness = 0f;
					CS_0024_003C_003E8__locals209.badeline.Sprite.Play("idle");
					CS_0024_003C_003E8__locals209.badeline.Sprite.Scale.X = 1f;
					foreach (CreditsTrigger entity2 in CS_0024_003C_003E8__locals209.Scene.Tracker.GetEntities<CreditsTrigger>())
					{
						if (entity2.Event == "BadelineOffset")
						{
							CS_0024_003C_003E8__locals209.badeline.Position = entity2.Position + new Vector2(8f, 16f);
						}
					}
					CS_0024_003C_003E8__locals209.Add(new Coroutine(CS_0024_003C_003E8__locals209.FadeTo(0f)));
					CS_0024_003C_003E8__locals209.Add(new Coroutine(CS_0024_003C_003E8__locals209.WaitForPlayer()));
					goto IL_1615;
				case 55:
					_003C_003E1__state = -1;
					goto IL_1615;
				case 56:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.badelineAutoWalk = true;
					CS_0024_003C_003E8__locals209.badelineWalkApproachFrom = CS_0024_003C_003E8__locals209.badeline.Position;
					CS_0024_003C_003E8__locals209.badelineWalkApproach = 0f;
					CS_0024_003C_003E8__locals209.badeline.Sprite.Play("walk");
					goto IL_16ed;
				case 57:
					_003C_003E1__state = -1;
					goto IL_16ed;
				case 58:
					_003C_003E1__state = -1;
					goto IL_1719;
				case 59:
					_003C_003E1__state = -1;
					_003C_003E2__current = 1f;
					_003C_003E1__state = 60;
					return true;
				case 60:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.SetBgFade(0.7f);
					_003C_003E2__current = CS_0024_003C_003E8__locals209.NextLevel("credits-payphone");
					_003C_003E1__state = 61;
					return true;
				case 61:
					_003C_003E1__state = -1;
					_003C_003E2__current = CS_0024_003C_003E8__locals209.SetupLevel();
					_003C_003E1__state = 62;
					return true;
				case 62:
				{
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.player.Speed = Vector2.Zero;
					CS_0024_003C_003E8__locals209.player.Facing = Facings.Left;
					CS_0024_003C_003E8__locals209.autoWalk = false;
					CS_0024_003C_003E8__locals209.badeline.Sprite.Play("idle");
					CS_0024_003C_003E8__locals209.badeline.Floatness = 0f;
					CS_0024_003C_003E8__locals209.badeline.Y = CS_0024_003C_003E8__locals209.player.Y;
					CS_0024_003C_003E8__locals209.badeline.Sprite.Scale.X = 1f;
					CS_0024_003C_003E8__locals209.badelineAutoFloat = false;
					CS_0024_003C_003E8__locals209.autoUpdateCamera = false;
					CS_0024_003C_003E8__locals209.Level.Camera.X += 100f;
					Vector2 target = CS_0024_003C_003E8__locals209.Level.Camera.Position + new Vector2(-200f, 0f);
					CS_0024_003C_003E8__locals209.Add(new Coroutine(CutsceneEntity.CameraTo(target, 14f, Ease.Linear)));
					CS_0024_003C_003E8__locals209.Add(new Coroutine(CS_0024_003C_003E8__locals209.FadeTo(0f)));
					_003C_003E2__current = 1.5f;
					_003C_003E1__state = 63;
					return true;
				}
				case 63:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.badeline.Sprite.Scale.X = -1f;
					_003C_003E2__current = 0.5f;
					_003C_003E1__state = 64;
					return true;
				case 64:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.Add(new Coroutine(CS_0024_003C_003E8__locals209.badeline.FloatTo(CS_0024_003C_003E8__locals209.badeline.Position + new Vector2(16f, -12f), -1, faceDirection: false)));
					_003C_003E2__current = 0.5f;
					_003C_003E1__state = 65;
					return true;
				case 65:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.player.Facing = Facings.Right;
					_003C_003E2__current = 1.5f;
					_003C_003E1__state = 66;
					return true;
				case 66:
					_003C_003E1__state = -1;
					_003CoshiroTarget_003E5__8 = CS_0024_003C_003E8__locals209.badeline.Position;
					_003Cto_003E5__12 = CS_0024_003C_003E8__locals209.player.Center;
					CS_0024_003C_003E8__locals209.Add(new Coroutine(CS_0024_003C_003E8__locals209.BadelineAround(_003CoshiroTarget_003E5__8, _003Cto_003E5__12, CS_0024_003C_003E8__locals209.badeline)));
					_003C_003E2__current = 0.5f;
					_003C_003E1__state = 67;
					return true;
				case 67:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.Add(new Coroutine(CS_0024_003C_003E8__locals209.BadelineAround(_003CoshiroTarget_003E5__8, _003Cto_003E5__12)));
					_003C_003E2__current = 0.5f;
					_003C_003E1__state = 68;
					return true;
				case 68:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.Add(new Coroutine(CS_0024_003C_003E8__locals209.BadelineAround(_003CoshiroTarget_003E5__8, _003Cto_003E5__12)));
					_003C_003E2__current = 3f;
					_003C_003E1__state = 69;
					return true;
				case 69:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.badeline.Sprite.Play("laugh");
					_003C_003E2__current = 0.5f;
					_003C_003E1__state = 70;
					return true;
				case 70:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.player.Facing = Facings.Left;
					_003C_003E2__current = 0.5f;
					_003C_003E1__state = 71;
					return true;
				case 71:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.player.DummyAutoAnimate = false;
					CS_0024_003C_003E8__locals209.player.Sprite.Play("sitDown");
					_003C_003E2__current = 3f;
					_003C_003E1__state = 72;
					return true;
				case 72:
					_003C_003E1__state = -1;
					_003C_003E2__current = CS_0024_003C_003E8__locals209.FadeTo(1f);
					_003C_003E1__state = 73;
					return true;
				case 73:
					_003C_003E1__state = -1;
					_003CoshiroTarget_003E5__8 = default(Vector2);
					_003Cto_003E5__12 = default(Vector2);
					_003C_003E2__current = 1f;
					_003C_003E1__state = 74;
					return true;
				case 74:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.SetBgFade(0.8f);
					_003C_003E2__current = CS_0024_003C_003E8__locals209.NextLevel("credits-city");
					_003C_003E1__state = 75;
					return true;
				case 75:
					_003C_003E1__state = -1;
					_003C_003E2__current = CS_0024_003C_003E8__locals209.SetupLevel();
					_003C_003E1__state = 76;
					return true;
				case 76:
				{
					_003C_003E1__state = -1;
					BirdNPC birdNPC = CS_0024_003C_003E8__locals209.Scene.Entities.FindFirst<BirdNPC>();
					if (birdNPC != null)
					{
						birdNPC.Facing = Facings.Right;
					}
					CS_0024_003C_003E8__locals209.badelineWalkApproach = 1f;
					CS_0024_003C_003E8__locals209.badelineAutoFloat = false;
					CS_0024_003C_003E8__locals209.badelineAutoWalk = true;
					CS_0024_003C_003E8__locals209.badeline.Floatness = 0f;
					CS_0024_003C_003E8__locals209.Add(new Coroutine(CS_0024_003C_003E8__locals209.FadeTo(0f)));
					_003C_003E2__current = CS_0024_003C_003E8__locals209.WaitForPlayer();
					_003C_003E1__state = 77;
					return true;
				}
				case 77:
					_003C_003E1__state = -1;
					_003C_003E2__current = CS_0024_003C_003E8__locals209.FadeTo(1f);
					_003C_003E1__state = 78;
					return true;
				case 78:
					_003C_003E1__state = -1;
					_003C_003E2__current = 1f;
					_003C_003E1__state = 79;
					return true;
				case 79:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.SetBgFade(0f);
					_003C_003E2__current = CS_0024_003C_003E8__locals209.NextLevel("credits-prologue");
					_003C_003E1__state = 80;
					return true;
				case 80:
					_003C_003E1__state = -1;
					_003C_003E2__current = CS_0024_003C_003E8__locals209.SetupLevel();
					_003C_003E1__state = 81;
					return true;
				case 81:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals209.badelineWalkApproach = 1f;
					CS_0024_003C_003E8__locals209.badelineAutoFloat = false;
					CS_0024_003C_003E8__locals209.badelineAutoWalk = true;
					CS_0024_003C_003E8__locals209.badeline.Floatness = 0f;
					CS_0024_003C_003E8__locals209.Add(new Coroutine(CS_0024_003C_003E8__locals209.FadeTo(0f)));
					_003C_003E2__current = CS_0024_003C_003E8__locals209.WaitForPlayer();
					_003C_003E1__state = 82;
					return true;
				case 82:
					_003C_003E1__state = -1;
					_003C_003E2__current = CS_0024_003C_003E8__locals209.FadeTo(1f);
					_003C_003E1__state = 83;
					return true;
				case 83:
					_003C_003E1__state = -1;
					break;
				case 84:
					{
						_003C_003E1__state = -1;
						break;
					}
					IL_16ed:
					if (CS_0024_003C_003E8__locals209.badelineWalkApproach != 1f)
					{
						CS_0024_003C_003E8__locals209.badelineWalkApproach = Calc.Approach(CS_0024_003C_003E8__locals209.badelineWalkApproach, 1f, Engine.DeltaTime * 4f);
						_003C_003E2__current = null;
						_003C_003E1__state = 57;
						return true;
					}
					goto IL_1719;
					IL_1719:
					if (CS_0024_003C_003E8__locals209.player.X > (float)(CS_0024_003C_003E8__locals209.Level.Bounds.X + 160))
					{
						_003C_003E2__current = null;
						_003C_003E1__state = 58;
						return true;
					}
					_003C_003E2__current = CS_0024_003C_003E8__locals209.FadeTo(1f);
					_003C_003E1__state = 59;
					return true;
					IL_1615:
					if (CS_0024_003C_003E8__locals209.player.X > CS_0024_003C_003E8__locals209.badeline.X - 16f)
					{
						_003C_003E2__current = null;
						_003C_003E1__state = 55;
						return true;
					}
					CS_0024_003C_003E8__locals209.badeline.Sprite.Scale.X = -1f;
					_003C_003E2__current = 0.1f;
					_003C_003E1__state = 56;
					return true;
					IL_1216:
					if (_003CbaddyHighJump_003E5__5 < 1f)
					{
						_003Cbird_003E5__10.Position = _003Cfrom_003E5__11 + (_003Cto_003E5__12 - _003Cfrom_003E5__11) * Ease.QuadOut(_003CbaddyHighJump_003E5__5);
						if (_003CbaddyHighJump_003E5__5 > 0.5f)
						{
							_003Cbird_003E5__10.Sprite.Play("fly");
							_003Cbird_003E5__10.Depth = -1000000;
							_003Cbird_003E5__10.Light.Visible = true;
						}
						_003CbaddyHighJump_003E5__5 += Engine.DeltaTime * 0.5f;
						_003C_003E2__current = null;
						_003C_003E1__state = 45;
						return true;
					}
					_003Cbird_003E5__10.Position = _003Cto_003E5__12;
					_003CoshiroRoutine_003E5__9.RemoveSelf();
					_003C_003E8__1.oshiro.Sprite.Play("putBroomAway");
					_003C_003E8__1.oshiro.Sprite.OnFrameChange = delegate
					{
						if (_003C_003E8__1.oshiro.Sprite.CurrentAnimationFrame == 10)
						{
							Entity entity = new Entity(_003C_003E8__1.oshiro.Position)
							{
								Depth = _003C_003E8__1.oshiro.Depth + 1
							};
							_003C_003E8__1._003C_003E4__this.Scene.Add(entity);
							entity.Add(new Image(GFX.Game["characters/oshiro/broom"])
							{
								Origin = _003C_003E8__1.oshiro.Sprite.Origin
							});
							_003C_003E8__1.oshiro.Sprite.OnFrameChange = null;
						}
					};
					_003Cbird_003E5__10.Sprite.Play("idle");
					_003C_003E2__current = 0.5f;
					_003C_003E1__state = 46;
					return true;
					IL_0af8:
					while (_003C_003E7__wrap5.MoveNext())
					{
						CreditsTrigger creditsTrigger2 = (CreditsTrigger)_003C_003E7__wrap5.Current;
						if (creditsTrigger2.Event == "BadelineOffset")
						{
							_003Cother_003E5__3 = new Player(creditsTrigger2.Position, PlayerSpriteMode.Badeline);
							_003Cother_003E5__3.OverrideHairColor = BadelineOldsite.HairColor;
							_003C_003E2__current = null;
							_003C_003E1__state = 35;
							return true;
						}
					}
					_003C_003Em__Finally1();
					_003C_003E7__wrap5 = default(List<Entity>.Enumerator);
					CS_0024_003C_003E8__locals209.Add(new Coroutine(CS_0024_003C_003E8__locals209.FadeTo(0f)));
					CS_0024_003C_003E8__locals209.Level.Camera.Position += new Vector2(0f, -100f);
					target3 = CS_0024_003C_003E8__locals209.Level.Camera.Position + new Vector2(0f, 160f);
					CS_0024_003C_003E8__locals209.Add(new Coroutine(CutsceneEntity.CameraTo(target3, 12f, Ease.Linear)));
					_003CplayerHighJump_003E5__4 = 0f;
					_003CbaddyHighJump_003E5__5 = 0f;
					_003Cp_003E5__7 = 0f;
					goto IL_0d01;
					IL_0d01:
					if (_003Cp_003E5__7 < 10f)
					{
						if (((_003Cp_003E5__7 > 3f && _003Cp_003E5__7 < 6f) || _003Cp_003E5__7 > 9f) && CS_0024_003C_003E8__locals209.player.Speed.Y < 0f && CS_0024_003C_003E8__locals209.player.OnGround(4))
						{
							_003CplayerHighJump_003E5__4 = 0.25f;
						}
						if (_003Cp_003E5__7 > 5f && _003Cp_003E5__7 < 8f && _003Cother_003E5__3.Speed.Y < 0f && _003Cother_003E5__3.OnGround(4))
						{
							_003CbaddyHighJump_003E5__5 = 0.25f;
						}
						if (_003CplayerHighJump_003E5__4 > 0f)
						{
							_003CplayerHighJump_003E5__4 -= Engine.DeltaTime;
							CS_0024_003C_003E8__locals209.player.Speed.Y = -200f;
						}
						if (_003CbaddyHighJump_003E5__5 > 0f)
						{
							_003CbaddyHighJump_003E5__5 -= Engine.DeltaTime;
							_003Cother_003E5__3.Speed.Y = -200f;
						}
						_003C_003E2__current = null;
						_003C_003E1__state = 36;
						return true;
					}
					_003C_003E2__current = CS_0024_003C_003E8__locals209.FadeTo(1f);
					_003C_003E1__state = 37;
					return true;
				}
				if (CS_0024_003C_003E8__locals209.credits.BottomTimer < 2f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 84;
					return true;
				}
				if (!CS_0024_003C_003E8__locals209.gotoEpilogue)
				{
					CS_0024_003C_003E8__locals209.snow = new HiresSnow();
					CS_0024_003C_003E8__locals209.snow.Alpha = 0f;
					CS_0024_003C_003E8__locals209.snow.AttachAlphaTo = new FadeWipe(CS_0024_003C_003E8__locals209.Level, wipeIn: false, delegate
					{
						CS_0024_003C_003E8__locals209.EndCutscene(CS_0024_003C_003E8__locals209.Level);
					});
					CS_0024_003C_003E8__locals209.Level.Add(CS_0024_003C_003E8__locals209.Level.HiresSnow = CS_0024_003C_003E8__locals209.snow);
				}
				else
				{
					new FadeWipe(CS_0024_003C_003E8__locals209.Level, wipeIn: false, delegate
					{
						CS_0024_003C_003E8__locals209.EndCutscene(CS_0024_003C_003E8__locals209.Level);
					});
				}
				return false;
			}
			catch
			{
				//try-fault
				((IDisposable)this).Dispose();
				throw;
			}
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		private void _003C_003Em__Finally1()
		{
			_003C_003E1__state = -1;
			((IDisposable)_003C_003E7__wrap5/*cast due to .constrained prefix*/).Dispose();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class _003CSetupLevel_003Ed__25 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS07_Credits _003C_003E4__this;

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
		public _003CSetupLevel_003Ed__25(int _003C_003E1__state)
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
			CS07_Credits cS07_Credits = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS07_Credits.Level.SnapColorGrade("credits");
				cS07_Credits.player = null;
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if ((cS07_Credits.player = cS07_Credits.Scene.Tracker.GetEntity<Player>()) == null)
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			cS07_Credits.Level.Add(cS07_Credits.badeline = new BadelineDummy(cS07_Credits.player.Position + new Vector2(16f, -16f)));
			cS07_Credits.badeline.Floatness = 4f;
			cS07_Credits.badelineAutoFloat = true;
			cS07_Credits.badelineAutoWalk = false;
			cS07_Credits.badelineWalkApproach = 0f;
			cS07_Credits.Level.Session.Inventory.Dashes = 1;
			cS07_Credits.player.Dashes = 1;
			cS07_Credits.player.StateMachine.State = 11;
			cS07_Credits.player.DummyFriction = false;
			cS07_Credits.player.DummyMaxspeed = false;
			cS07_Credits.player.Facing = Facings.Left;
			cS07_Credits.autoWalk = true;
			cS07_Credits.autoUpdateCamera = true;
			cS07_Credits.Level.CameraOffset.X = 70f;
			cS07_Credits.Level.CameraOffset.Y = -24f;
			cS07_Credits.Level.Camera.Position = cS07_Credits.player.CameraTarget;
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
	private sealed class _003CWaitForPlayer_003Ed__26 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS07_Credits _003C_003E4__this;

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
		public _003CWaitForPlayer_003Ed__26(int _003C_003E1__state)
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
			CS07_Credits cS07_Credits = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				goto IL_0073;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0055;
			case 2:
				{
					_003C_003E1__state = -1;
					goto IL_0073;
				}
				IL_0073:
				if (cS07_Credits.player.X > (float)(cS07_Credits.Level.Bounds.X + 160))
				{
					if (cS07_Credits.Event != null)
					{
						_003C_003E2__current = cS07_Credits.DoEvent(cS07_Credits.Event);
						_003C_003E1__state = 1;
						return true;
					}
					goto IL_0055;
				}
				return false;
				IL_0055:
				cS07_Credits.Event = null;
				_003C_003E2__current = null;
				_003C_003E1__state = 2;
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
	private sealed class _003C_003Ec__DisplayClass27_0
	{
		public CS07_Credits _003C_003E4__this;

		public string name;

		internal void _003CNextLevel_003Eb__0()
		{
			_003C_003E4__this.Level.UnloadLevel();
			_003C_003E4__this.Level.Session.Level = name;
			_003C_003E4__this.Level.Session.RespawnPoint = _003C_003E4__this.Level.GetSpawnPoint(new Vector2(_003C_003E4__this.Level.Bounds.Left, _003C_003E4__this.Level.Bounds.Top));
			_003C_003E4__this.Level.LoadLevel(Player.IntroTypes.None);
			_003C_003E4__this.Level.Wipe.Cancel();
		}
	}

	[CompilerGenerated]
	private sealed class _003CNextLevel_003Ed__27 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS07_Credits _003C_003E4__this;

		public string name;

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
		public _003CNextLevel_003Ed__27(int _003C_003E1__state)
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
			CS07_Credits cS07_Credits = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				_003C_003Ec__DisplayClass27_0 CS_0024_003C_003E8__locals9 = new _003C_003Ec__DisplayClass27_0
				{
					_003C_003E4__this = _003C_003E4__this,
					name = name
				};
				if (cS07_Credits.player != null)
				{
					cS07_Credits.player.RemoveSelf();
				}
				cS07_Credits.player = null;
				cS07_Credits.Level.OnEndOfFrame += delegate
				{
					CS_0024_003C_003E8__locals9._003C_003E4__this.Level.UnloadLevel();
					CS_0024_003C_003E8__locals9._003C_003E4__this.Level.Session.Level = CS_0024_003C_003E8__locals9.name;
					CS_0024_003C_003E8__locals9._003C_003E4__this.Level.Session.RespawnPoint = CS_0024_003C_003E8__locals9._003C_003E4__this.Level.GetSpawnPoint(new Vector2(CS_0024_003C_003E8__locals9._003C_003E4__this.Level.Bounds.Left, CS_0024_003C_003E8__locals9._003C_003E4__this.Level.Bounds.Top));
					CS_0024_003C_003E8__locals9._003C_003E4__this.Level.LoadLevel(Player.IntroTypes.None);
					CS_0024_003C_003E8__locals9._003C_003E4__this.Level.Wipe.Cancel();
				};
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = null;
				_003C_003E1__state = 2;
				return true;
			case 2:
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
	private sealed class _003CFadeTo_003Ed__28 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS07_Credits _003C_003E4__this;

		public float value;

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
		public _003CFadeTo_003Ed__28(int _003C_003E1__state)
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
			CS07_Credits cS07_Credits = _003C_003E4__this;
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
			if ((cS07_Credits.fade = Calc.Approach(cS07_Credits.fade, value, Engine.DeltaTime * 0.5f)) != value)
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			cS07_Credits.fade = value;
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
	private sealed class _003CBadelineApproachWalking_003Ed__29 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS07_Credits _003C_003E4__this;

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
		public _003CBadelineApproachWalking_003Ed__29(int _003C_003E1__state)
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
			CS07_Credits cS07_Credits = _003C_003E4__this;
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
			if (cS07_Credits.badelineWalkApproach < 1f)
			{
				cS07_Credits.badeline.Floatness = Calc.Approach(cS07_Credits.badeline.Floatness, 0f, Engine.DeltaTime * 8f);
				cS07_Credits.badelineWalkApproach = Calc.Approach(cS07_Credits.badelineWalkApproach, 1f, Engine.DeltaTime * 0.6f);
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
	private sealed class _003CDustyRoutine_003Ed__30 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Entity oshiro;

		public CS07_Credits _003C_003E4__this;

		private List<Entity> _003Cdusty_003E5__2;

		private float _003Ctimer_003E5__3;

		private Vector2 _003Coffset_003E5__4;

		private Vector2 _003Cstart_003E5__5;

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
		public _003CDustyRoutine_003Ed__30(int _003C_003E1__state)
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
			CS07_Credits cS07_Credits = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				_003Cdusty_003E5__2 = new List<Entity>();
				_003Ctimer_003E5__3 = 0f;
				_003Coffset_003E5__4 = oshiro.Position + new Vector2(220f, -24f);
				_003Cstart_003E5__5 = _003Coffset_003E5__4;
				for (int i = 0; i < 3; i++)
				{
					Entity entity = new Entity(_003Coffset_003E5__4 + new Vector2(i * 24, 0f));
					entity.Depth = -50;
					entity.Add(new DustGraphic(ignoreSolids: true, autoControlEyes: false, autoExpandDust: true));
					Image image = new Image(GFX.Game["decals/3-resort/brokenbox_" + (char)(97 + i)]);
					image.JustifyOrigin(0.5f, 1f);
					image.Position = new Vector2(0f, -4f);
					entity.Add(image);
					cS07_Credits.Scene.Add(entity);
					_003Cdusty_003E5__2.Add(entity);
				}
				_003C_003E2__current = 3.8f;
				_003C_003E1__state = 1;
				return true;
			}
			case 1:
				_003C_003E1__state = -1;
				break;
			case 2:
				_003C_003E1__state = -1;
				break;
			}
			for (int j = 0; j < _003Cdusty_003E5__2.Count; j++)
			{
				Entity entity2 = _003Cdusty_003E5__2[j];
				entity2.X = _003Coffset_003E5__4.X + (float)(j * 24);
				entity2.Y = _003Coffset_003E5__4.Y + (float)Math.Sin(_003Ctimer_003E5__3 * 4f + (float)j * 0.8f) * 4f;
			}
			if (_003Coffset_003E5__4.X < (float)(cS07_Credits.Level.Bounds.Left + 120))
			{
				_003Coffset_003E5__4.Y = Calc.Approach(_003Coffset_003E5__4.Y, _003Cstart_003E5__5.Y + 16f, Engine.DeltaTime * 16f);
			}
			_003Coffset_003E5__4.X -= 26f * Engine.DeltaTime;
			_003Ctimer_003E5__3 += Engine.DeltaTime;
			_003C_003E2__current = null;
			_003C_003E1__state = 2;
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
	private sealed class _003CBadelineAround_003Ed__31 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BadelineDummy badeline;

		public CS07_Credits _003C_003E4__this;

		public Vector2 start;

		public Vector2 around;

		private bool _003CremoveAtEnd_003E5__2;

		private float _003Cangle_003E5__3;

		private float _003Cdist_003E5__4;

		private float _003Cduration_003E5__5;

		private float _003Cp_003E5__6;

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
		public _003CBadelineAround_003Ed__31(int _003C_003E1__state)
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
			CS07_Credits cS07_Credits = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003CremoveAtEnd_003E5__2 = badeline == null;
				if (badeline == null)
				{
					cS07_Credits.Scene.Add(badeline = new BadelineDummy(start));
				}
				badeline.Sprite.Play("fallSlow");
				_003Cangle_003E5__3 = Calc.Angle(around, start);
				_003Cdist_003E5__4 = (around - start).Length();
				_003Cduration_003E5__5 = 3f;
				_003Cp_003E5__6 = 0f;
				break;
			case 1:
				_003C_003E1__state = -1;
				_003Cp_003E5__6 += Engine.DeltaTime / _003Cduration_003E5__5;
				break;
			}
			if (_003Cp_003E5__6 < 1f)
			{
				float num2 = _003Cp_003E5__6 * 2f;
				badeline.Position = around + Calc.AngleToVector(_003Cangle_003E5__3 - num2 * ((float)Math.PI * 2f), _003Cdist_003E5__4 + Calc.YoYo(_003Cp_003E5__6) * 16f + (float)Math.Sin(_003Cp_003E5__6 * ((float)Math.PI * 2f) * 4f) * 5f);
				badeline.Sprite.Scale.X = Math.Sign(around.X - badeline.X);
				if (!_003CremoveAtEnd_003E5__2)
				{
					cS07_Credits.player.Facing = (Facings)Math.Sign(badeline.X - cS07_Credits.player.X);
				}
				if (cS07_Credits.Scene.OnInterval(0.1f))
				{
					TrailManager.Add(badeline, Player.NormalHairColor);
				}
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			if (_003CremoveAtEnd_003E5__2)
			{
				badeline.Vanish();
			}
			else
			{
				badeline.Sprite.Play("laugh");
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
	private sealed class _003CDoEvent_003Ed__32 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public string e;

		public CS07_Credits _003C_003E4__this;

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
		public _003CDoEvent_003Ed__32(int _003C_003E1__state)
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
			CS07_Credits cS07_Credits = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				if (e == "WaitJumpDash")
				{
					_003C_003E2__current = cS07_Credits.EventWaitJumpDash();
					_003C_003E1__state = 1;
					return true;
				}
				if (e == "WaitJumpDoubleDash")
				{
					_003C_003E2__current = cS07_Credits.EventWaitJumpDoubleDash();
					_003C_003E1__state = 2;
					return true;
				}
				if (e == "ClimbDown")
				{
					_003C_003E2__current = cS07_Credits.EventClimbDown();
					_003C_003E1__state = 3;
					return true;
				}
				if (e == "Wait")
				{
					_003C_003E2__current = cS07_Credits.EventWait();
					_003C_003E1__state = 4;
					return true;
				}
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			case 2:
				_003C_003E1__state = -1;
				break;
			case 3:
				_003C_003E1__state = -1;
				break;
			case 4:
				_003C_003E1__state = -1;
				break;
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
	private sealed class _003CEventWaitJumpDash_003Ed__33 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS07_Credits _003C_003E4__this;

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
		public _003CEventWaitJumpDash_003Ed__33(int _003C_003E1__state)
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
			CS07_Credits cS07_Credits = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS07_Credits.autoWalk = false;
				cS07_Credits.player.DummyFriction = true;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS07_Credits.PlayerJump(-1);
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS07_Credits.player.OverrideDashDirection = new Vector2(-1f, -1f);
				cS07_Credits.player.StateMachine.State = cS07_Credits.player.StartDash();
				_003C_003E2__current = 0.6f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				cS07_Credits.player.OverrideDashDirection = null;
				cS07_Credits.player.StateMachine.State = 11;
				cS07_Credits.autoWalk = true;
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
	private sealed class _003CEventWaitJumpDoubleDash_003Ed__34 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS07_Credits _003C_003E4__this;

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
		public _003CEventWaitJumpDoubleDash_003Ed__34(int _003C_003E1__state)
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
			CS07_Credits cS07_Credits = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS07_Credits.autoWalk = false;
				cS07_Credits.player.DummyFriction = true;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS07_Credits.player.Facing = Facings.Right;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS07_Credits.BadelineCombine();
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				cS07_Credits.player.Dashes = 2;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				cS07_Credits.player.Facing = Facings.Left;
				_003C_003E2__current = 0.7f;
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				cS07_Credits.PlayerJump(-1);
				_003C_003E2__current = 0.4f;
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				cS07_Credits.player.OverrideDashDirection = new Vector2(-1f, -1f);
				cS07_Credits.player.StateMachine.State = cS07_Credits.player.StartDash();
				_003C_003E2__current = 0.6f;
				_003C_003E1__state = 7;
				return true;
			case 7:
				_003C_003E1__state = -1;
				cS07_Credits.player.OverrideDashDirection = new Vector2(-1f, 0f);
				cS07_Credits.player.StateMachine.State = cS07_Credits.player.StartDash();
				_003C_003E2__current = 0.6f;
				_003C_003E1__state = 8;
				return true;
			case 8:
				_003C_003E1__state = -1;
				cS07_Credits.player.OverrideDashDirection = null;
				cS07_Credits.player.StateMachine.State = 11;
				cS07_Credits.autoWalk = true;
				goto IL_0243;
			case 9:
				_003C_003E1__state = -1;
				goto IL_0243;
			case 10:
				_003C_003E1__state = -1;
				cS07_Credits.player.Facing = Facings.Right;
				_003C_003E2__current = 1f;
				_003C_003E1__state = 11;
				return true;
			case 11:
				_003C_003E1__state = -1;
				cS07_Credits.Level.Displacement.AddBurst(cS07_Credits.player.Position, 0.4f, 8f, 32f, 0.5f);
				cS07_Credits.badeline.Position = cS07_Credits.player.Position;
				cS07_Credits.badeline.Visible = true;
				cS07_Credits.badelineAutoFloat = true;
				cS07_Credits.player.Dashes = 1;
				_003C_003E2__current = 0.8f;
				_003C_003E1__state = 12;
				return true;
			case 12:
				{
					_003C_003E1__state = -1;
					cS07_Credits.player.Facing = Facings.Left;
					cS07_Credits.autoWalk = true;
					cS07_Credits.player.DummyFriction = false;
					return false;
				}
				IL_0243:
				if (!cS07_Credits.player.OnGround())
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 9;
					return true;
				}
				cS07_Credits.autoWalk = false;
				cS07_Credits.player.DummyFriction = true;
				cS07_Credits.player.Dashes = 2;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 10;
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
	private sealed class _003CEventClimbDown_003Ed__35 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS07_Credits _003C_003E4__this;

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
		public _003CEventClimbDown_003Ed__35(int _003C_003E1__state)
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
			CS07_Credits cS07_Credits = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS07_Credits.autoWalk = false;
				cS07_Credits.player.DummyFriction = true;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS07_Credits.PlayerJump(-1);
				_003C_003E2__current = 0.4f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				goto IL_00b4;
			case 3:
				_003C_003E1__state = -1;
				goto IL_00b4;
			case 4:
				_003C_003E1__state = -1;
				goto IL_0152;
			case 5:
				_003C_003E1__state = -1;
				goto IL_01be;
			case 6:
				_003C_003E1__state = -1;
				goto IL_01be;
			case 7:
				_003C_003E1__state = -1;
				goto IL_025c;
			case 8:
				{
					_003C_003E1__state = -1;
					cS07_Credits.autoWalk = true;
					return false;
				}
				IL_01be:
				if (!cS07_Credits.player.CollideCheck<Solid>(cS07_Credits.player.Position + new Vector2(1f, 0f)))
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 6;
					return true;
				}
				cS07_Credits.player.DummyAutoAnimate = false;
				cS07_Credits.player.Sprite.Play("wallslide");
				goto IL_025c;
				IL_025c:
				if (!cS07_Credits.player.CollideCheck<Solid>(cS07_Credits.player.Position + new Vector2(0f, 32f)))
				{
					cS07_Credits.player.CreateWallSlideParticles(1);
					cS07_Credits.player.Speed.Y = Math.Min(cS07_Credits.player.Speed.Y, 40f);
					_003C_003E2__current = null;
					_003C_003E1__state = 7;
					return true;
				}
				cS07_Credits.PlayerJump(-1);
				_003C_003E2__current = 0.4f;
				_003C_003E1__state = 8;
				return true;
				IL_0152:
				if (cS07_Credits.player.CollideCheck<Solid>(cS07_Credits.player.Position + new Vector2(-1f, 32f)))
				{
					cS07_Credits.player.CreateWallSlideParticles(-1);
					cS07_Credits.player.Speed.Y = Math.Min(cS07_Credits.player.Speed.Y, 40f);
					_003C_003E2__current = null;
					_003C_003E1__state = 4;
					return true;
				}
				cS07_Credits.PlayerJump(1);
				_003C_003E2__current = 0.4f;
				_003C_003E1__state = 5;
				return true;
				IL_00b4:
				if (!cS07_Credits.player.CollideCheck<Solid>(cS07_Credits.player.Position + new Vector2(-1f, 0f)))
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 3;
					return true;
				}
				cS07_Credits.player.DummyAutoAnimate = false;
				cS07_Credits.player.Sprite.Play("wallslide");
				goto IL_0152;
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
	private sealed class _003CEventWait_003Ed__36 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS07_Credits _003C_003E4__this;

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
		public _003CEventWait_003Ed__36(int _003C_003E1__state)
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
			CS07_Credits cS07_Credits = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS07_Credits.badeline.Sprite.Play("idle");
				cS07_Credits.badelineAutoWalk = false;
				cS07_Credits.autoWalk = false;
				cS07_Credits.player.DummyFriction = true;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS07_Credits.player.DummyAutoAnimate = false;
				cS07_Credits.player.Speed = Vector2.Zero;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS07_Credits.player.Sprite.Play("lookUp");
				_003C_003E2__current = 2f;
				_003C_003E1__state = 3;
				return true;
			case 3:
			{
				_003C_003E1__state = -1;
				BirdNPC birdNPC = cS07_Credits.Scene.Entities.FindFirst<BirdNPC>();
				if (birdNPC != null)
				{
					birdNPC.AutoFly = true;
				}
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 4;
				return true;
			}
			case 4:
				_003C_003E1__state = -1;
				cS07_Credits.player.Sprite.Play("idle");
				_003C_003E2__current = 1f;
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				cS07_Credits.autoWalk = true;
				cS07_Credits.player.DummyFriction = false;
				cS07_Credits.player.DummyAutoAnimate = true;
				cS07_Credits.badelineAutoWalk = true;
				cS07_Credits.badelineWalkApproach = 0f;
				cS07_Credits.badelineWalkApproachFrom = cS07_Credits.badeline.Position;
				cS07_Credits.badeline.Sprite.Play("walk");
				break;
			case 6:
				_003C_003E1__state = -1;
				break;
			}
			if (cS07_Credits.badelineWalkApproach < 1f)
			{
				cS07_Credits.badelineWalkApproach += Engine.DeltaTime * 4f;
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

	[CompilerGenerated]
	private sealed class _003CBadelineCombine_003Ed__37 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS07_Credits _003C_003E4__this;

		private Vector2 _003Cfrom_003E5__2;

		private float _003Cp_003E5__3;

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
		public _003CBadelineCombine_003Ed__37(int _003C_003E1__state)
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
			CS07_Credits cS07_Credits = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Cfrom_003E5__2 = cS07_Credits.badeline.Position;
				cS07_Credits.badelineAutoFloat = false;
				_003Cp_003E5__3 = 0f;
				break;
			case 1:
				_003C_003E1__state = -1;
				_003Cp_003E5__3 += Engine.DeltaTime / 0.25f;
				break;
			}
			if (_003Cp_003E5__3 < 1f)
			{
				cS07_Credits.badeline.Position = Vector2.Lerp(_003Cfrom_003E5__2, cS07_Credits.player.Position, Ease.CubeIn(_003Cp_003E5__3));
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			cS07_Credits.badeline.Visible = false;
			cS07_Credits.Level.Displacement.AddBurst(cS07_Credits.player.Position, 0.4f, 8f, 32f, 0.5f);
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

	public const float CameraXOffset = 70f;

	public const float CameraYOffset = -24f;

	public static CS07_Credits Instance;

	public string Event;

	private MTexture gradient = GFX.Gui["creditsgradient"].GetSubtexture(0, 1, 1920, 1);

	private Credits credits;

	private Player player;

	private bool autoWalk = true;

	private bool autoUpdateCamera = true;

	private BadelineDummy badeline;

	private bool badelineAutoFloat = true;

	private bool badelineAutoWalk;

	private float badelineWalkApproach;

	private Vector2 badelineWalkApproachFrom;

	private float walkOffset;

	private bool wasDashAssistOn;

	private Fill fillbg;

	private float fade = 1f;

	private HiresSnow snow;

	private bool gotoEpilogue;

	public CS07_Credits()
	{
		MInput.Disabled = true;
		Instance = this;
		base.Tag = (int)Tags.Global | (int)Tags.HUD;
		wasDashAssistOn = SaveData.Instance.Assists.DashAssist;
		SaveData.Instance.Assists.DashAssist = false;
	}

	public override void OnBegin(Level level)
	{
		Audio.BusMuted("bus:/gameplay_sfx", true);
		gotoEpilogue = level.Session.OldStats.Modes[0].Completed;
		gotoEpilogue = true;
		Add(new Coroutine(Routine()));
		Add(new PostUpdateHook(PostUpdate));
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		(base.Scene as Level).InCredits = true;
	}

	[IteratorStateMachine(typeof(_003CRoutine_003Ed__24))]
	private IEnumerator Routine()
	{
		Level.Background.Backdrops.Add(fillbg = new Fill());
		Level.Completed = true;
		Level.Entities.FindFirst<SpeedrunTimerDisplay>()?.RemoveSelf();
		Level.Entities.FindFirst<TotalStrawberriesDisplay>()?.RemoveSelf();
		Level.Entities.FindFirst<GameplayStats>()?.RemoveSelf();
		yield return null;
		Level.Wipe.Cancel();
		yield return 0.5f;
		float alignment = 1f;
		if (SaveData.Instance != null && SaveData.Instance.Assists.MirrorMode)
		{
			alignment = 0f;
		}
		credits = new Credits(alignment, 0.6f, haveTitle: false, havePolaroids: true);
		credits.AllowInput = false;
		yield return 3f;
		SetBgFade(0f);
		Add(new Coroutine(FadeTo(0f)));
		yield return SetupLevel();
		yield return WaitForPlayer();
		yield return FadeTo(1f);
		yield return 1f;
		SetBgFade(0.1f);
		yield return NextLevel("credits-dashes");
		yield return SetupLevel();
		Add(new Coroutine(FadeTo(0f)));
		yield return WaitForPlayer();
		yield return FadeTo(1f);
		yield return 1f;
		SetBgFade(0.2f);
		yield return NextLevel("credits-walking");
		yield return SetupLevel();
		Add(new Coroutine(FadeTo(0f)));
		yield return 5.8f;
		badelineAutoFloat = false;
		yield return 0.5f;
		badeline.Sprite.Scale.X = 1f;
		yield return 0.5f;
		autoWalk = false;
		player.Speed = Vector2.Zero;
		player.Facing = Facings.Right;
		yield return 1.5f;
		badeline.Sprite.Scale.X = -1f;
		yield return 1f;
		badeline.Sprite.Scale.X = -1f;
		badelineAutoWalk = true;
		badelineWalkApproachFrom = badeline.Position;
		Add(new Coroutine(BadelineApproachWalking()));
		yield return 0.7f;
		autoWalk = true;
		player.Facing = Facings.Left;
		yield return WaitForPlayer();
		yield return FadeTo(1f);
		yield return 1f;
		SetBgFade(0.3f);
		yield return NextLevel("credits-tree");
		yield return SetupLevel();
		Petals petals = new Petals();
		Level.Foreground.Backdrops.Add(petals);
		autoUpdateCamera = false;
		Vector2 target = Level.Camera.Position + new Vector2(-220f, 32f);
		Level.Camera.Position += new Vector2(-100f, 0f);
		badelineWalkApproach = 1f;
		badelineAutoFloat = false;
		badelineAutoWalk = true;
		badeline.Floatness = 0f;
		Add(new Coroutine(FadeTo(0f)));
		Add(new Coroutine(CutsceneEntity.CameraTo(target, 12f, Ease.Linear)));
		yield return 3.5f;
		badeline.Sprite.Play("idle");
		badelineAutoWalk = false;
		yield return 0.25f;
		autoWalk = false;
		player.Sprite.Play("idle");
		player.Speed = Vector2.Zero;
		player.DummyAutoAnimate = false;
		player.Facing = Facings.Right;
		yield return 0.5f;
		player.Sprite.Play("sitDown");
		yield return 4f;
		badeline.Sprite.Play("laugh");
		yield return 1.75f;
		yield return FadeTo(1f);
		Level.Foreground.Backdrops.Remove(petals);
		yield return 1f;
		SetBgFade(0.4f);
		yield return NextLevel("credits-clouds");
		yield return SetupLevel();
		autoWalk = false;
		player.Speed = Vector2.Zero;
		autoUpdateCamera = false;
		player.ForceCameraUpdate = false;
		badeline.Visible = false;
		Player other = null;
		foreach (CreditsTrigger entity2 in base.Scene.Tracker.GetEntities<CreditsTrigger>())
		{
			if (entity2.Event == "BadelineOffset")
			{
				other = new Player(entity2.Position, PlayerSpriteMode.Badeline)
				{
					OverrideHairColor = BadelineOldsite.HairColor
				};
				yield return null;
				other.StateMachine.State = 11;
				other.Facing = Facings.Left;
				base.Scene.Add(other);
			}
		}
		Add(new Coroutine(FadeTo(0f)));
		Level.Camera.Position += new Vector2(0f, -100f);
		Vector2 target2 = Level.Camera.Position + new Vector2(0f, 160f);
		Add(new Coroutine(CutsceneEntity.CameraTo(target2, 12f, Ease.Linear)));
		float playerHighJump = 0f;
		float baddyHighJump = 0f;
		for (float p = 0f; p < 10f; p += Engine.DeltaTime)
		{
			if (((p > 3f && p < 6f) || p > 9f) && player.Speed.Y < 0f && player.OnGround(4))
			{
				playerHighJump = 0.25f;
			}
			if (p > 5f && p < 8f && other.Speed.Y < 0f && other.OnGround(4))
			{
				baddyHighJump = 0.25f;
			}
			if (playerHighJump > 0f)
			{
				playerHighJump -= Engine.DeltaTime;
				player.Speed.Y = -200f;
			}
			if (baddyHighJump > 0f)
			{
				baddyHighJump -= Engine.DeltaTime;
				other.Speed.Y = -200f;
			}
			yield return null;
		}
		yield return FadeTo(1f);
		yield return 1f;
		SetBgFade(0.5f);
		yield return NextLevel("credits-resort");
		yield return SetupLevel();
		Add(new Coroutine(FadeTo(0f)));
		badelineWalkApproach = 1f;
		badelineAutoFloat = false;
		badelineAutoWalk = true;
		badeline.Floatness = 0f;
		Vector2 vector = Vector2.Zero;
		foreach (CreditsTrigger item in base.Scene.Entities.FindAll<CreditsTrigger>())
		{
			if (item.Event == "Oshiro")
			{
				vector = item.Position;
			}
		}
		NPC oshiro = new NPC(vector + new Vector2(0f, 4f));
		oshiro.Add(oshiro.Sprite = new OshiroSprite(1));
		oshiro.MoveAnim = "sweeping";
		oshiro.IdleAnim = "sweeping";
		oshiro.Sprite.Play("sweeping");
		oshiro.Maxspeed = 10f;
		oshiro.Depth = -60;
		base.Scene.Add(oshiro);
		Add(new Coroutine(DustyRoutine(oshiro)));
		yield return 4.8f;
		Vector2 oshiroTarget = oshiro.Position + new Vector2(116f, 0f);
		Coroutine oshiroRoutine = new Coroutine(oshiro.MoveTo(oshiroTarget));
		Add(oshiroRoutine);
		yield return 2f;
		autoUpdateCamera = false;
		yield return CutsceneEntity.CameraTo(new Vector2(Level.Bounds.Left + 64, Level.Bounds.Top), 2f);
		yield return 5f;
		BirdNPC bird = new BirdNPC(oshiro.Position + new Vector2(280f, -160f), BirdNPC.Modes.None)
		{
			Depth = 10010,
			Light = 
			{
				Visible = false
			}
		};
		base.Scene.Add(bird);
		bird.Facing = Facings.Left;
		bird.Sprite.Play("fall");
		Vector2 from = bird.Position;
		Vector2 to = oshiroTarget + new Vector2(50f, -12f);
		baddyHighJump = 0f;
		while (baddyHighJump < 1f)
		{
			bird.Position = from + (to - from) * Ease.QuadOut(baddyHighJump);
			if (baddyHighJump > 0.5f)
			{
				bird.Sprite.Play("fly");
				bird.Depth = -1000000;
				bird.Light.Visible = true;
			}
			baddyHighJump += Engine.DeltaTime * 0.5f;
			yield return null;
		}
		bird.Position = to;
		oshiroRoutine.RemoveSelf();
		oshiro.Sprite.Play("putBroomAway");
		oshiro.Sprite.OnFrameChange = delegate
		{
			if (oshiro.Sprite.CurrentAnimationFrame == 10)
			{
				Entity entity = new Entity(oshiro.Position)
				{
					Depth = oshiro.Depth + 1
				};
				base.Scene.Add(entity);
				entity.Add(new Image(GFX.Game["characters/oshiro/broom"])
				{
					Origin = oshiro.Sprite.Origin
				});
				oshiro.Sprite.OnFrameChange = null;
			}
		};
		bird.Sprite.Play("idle");
		yield return 0.5f;
		bird.Sprite.Play("croak");
		yield return 0.6f;
		oshiro.Maxspeed = 40f;
		oshiro.MoveAnim = "move";
		oshiro.IdleAnim = "idle";
		yield return oshiro.MoveTo(oshiroTarget + new Vector2(14f, 0f));
		yield return 2f;
		Add(new Coroutine(bird.StartleAndFlyAway()));
		yield return 0.75f;
		bird.Light.Visible = false;
		bird.Depth = 10010;
		oshiro.Sprite.Scale.X = -1f;
		yield return FadeTo(1f);
		yield return 1f;
		SetBgFade(0.6f);
		yield return NextLevel("credits-wallslide");
		yield return SetupLevel();
		badelineAutoFloat = false;
		badeline.Floatness = 0f;
		badeline.Sprite.Play("idle");
		badeline.Sprite.Scale.X = 1f;
		foreach (CreditsTrigger entity3 in base.Scene.Tracker.GetEntities<CreditsTrigger>())
		{
			if (entity3.Event == "BadelineOffset")
			{
				badeline.Position = entity3.Position + new Vector2(8f, 16f);
			}
		}
		Add(new Coroutine(FadeTo(0f)));
		Add(new Coroutine(WaitForPlayer()));
		while (player.X > badeline.X - 16f)
		{
			yield return null;
		}
		badeline.Sprite.Scale.X = -1f;
		yield return 0.1f;
		badelineAutoWalk = true;
		badelineWalkApproachFrom = badeline.Position;
		badelineWalkApproach = 0f;
		badeline.Sprite.Play("walk");
		while (badelineWalkApproach != 1f)
		{
			badelineWalkApproach = Calc.Approach(badelineWalkApproach, 1f, Engine.DeltaTime * 4f);
			yield return null;
		}
		while (player.X > (float)(Level.Bounds.X + 160))
		{
			yield return null;
		}
		yield return FadeTo(1f);
		yield return 1f;
		SetBgFade(0.7f);
		yield return NextLevel("credits-payphone");
		yield return SetupLevel();
		player.Speed = Vector2.Zero;
		player.Facing = Facings.Left;
		autoWalk = false;
		badeline.Sprite.Play("idle");
		badeline.Floatness = 0f;
		badeline.Y = player.Y;
		badeline.Sprite.Scale.X = 1f;
		badelineAutoFloat = false;
		autoUpdateCamera = false;
		Level.Camera.X += 100f;
		Vector2 target3 = Level.Camera.Position + new Vector2(-200f, 0f);
		Add(new Coroutine(CutsceneEntity.CameraTo(target3, 14f, Ease.Linear)));
		Add(new Coroutine(FadeTo(0f)));
		yield return 1.5f;
		badeline.Sprite.Scale.X = -1f;
		yield return 0.5f;
		Add(new Coroutine(badeline.FloatTo(badeline.Position + new Vector2(16f, -12f), -1, faceDirection: false)));
		yield return 0.5f;
		player.Facing = Facings.Right;
		yield return 1.5f;
		oshiroTarget = badeline.Position;
		to = player.Center;
		Add(new Coroutine(BadelineAround(oshiroTarget, to, badeline)));
		yield return 0.5f;
		Add(new Coroutine(BadelineAround(oshiroTarget, to)));
		yield return 0.5f;
		Add(new Coroutine(BadelineAround(oshiroTarget, to)));
		yield return 3f;
		badeline.Sprite.Play("laugh");
		yield return 0.5f;
		player.Facing = Facings.Left;
		yield return 0.5f;
		player.DummyAutoAnimate = false;
		player.Sprite.Play("sitDown");
		yield return 3f;
		yield return FadeTo(1f);
		yield return 1f;
		SetBgFade(0.8f);
		yield return NextLevel("credits-city");
		yield return SetupLevel();
		BirdNPC birdNPC = base.Scene.Entities.FindFirst<BirdNPC>();
		if (birdNPC != null)
		{
			birdNPC.Facing = Facings.Right;
		}
		badelineWalkApproach = 1f;
		badelineAutoFloat = false;
		badelineAutoWalk = true;
		badeline.Floatness = 0f;
		Add(new Coroutine(FadeTo(0f)));
		yield return WaitForPlayer();
		yield return FadeTo(1f);
		yield return 1f;
		SetBgFade(0f);
		yield return NextLevel("credits-prologue");
		yield return SetupLevel();
		badelineWalkApproach = 1f;
		badelineAutoFloat = false;
		badelineAutoWalk = true;
		badeline.Floatness = 0f;
		Add(new Coroutine(FadeTo(0f)));
		yield return WaitForPlayer();
		yield return FadeTo(1f);
		while (credits.BottomTimer < 2f)
		{
			yield return null;
		}
		if (!gotoEpilogue)
		{
			snow = new HiresSnow();
			snow.Alpha = 0f;
			snow.AttachAlphaTo = new FadeWipe(Level, wipeIn: false, delegate
			{
				EndCutscene(Level);
			});
			Level.Add(Level.HiresSnow = snow);
		}
		else
		{
			new FadeWipe(Level, wipeIn: false, delegate
			{
				EndCutscene(Level);
			});
		}
	}

	[IteratorStateMachine(typeof(_003CSetupLevel_003Ed__25))]
	private IEnumerator SetupLevel()
	{
		Level.SnapColorGrade("credits");
		player = null;
		while ((player = base.Scene.Tracker.GetEntity<Player>()) == null)
		{
			yield return null;
		}
		Level.Add(badeline = new BadelineDummy(player.Position + new Vector2(16f, -16f)));
		badeline.Floatness = 4f;
		badelineAutoFloat = true;
		badelineAutoWalk = false;
		badelineWalkApproach = 0f;
		Level.Session.Inventory.Dashes = 1;
		player.Dashes = 1;
		player.StateMachine.State = 11;
		player.DummyFriction = false;
		player.DummyMaxspeed = false;
		player.Facing = Facings.Left;
		autoWalk = true;
		autoUpdateCamera = true;
		Level.CameraOffset.X = 70f;
		Level.CameraOffset.Y = -24f;
		Level.Camera.Position = player.CameraTarget;
	}

	[IteratorStateMachine(typeof(_003CWaitForPlayer_003Ed__26))]
	private IEnumerator WaitForPlayer()
	{
		while (player.X > (float)(Level.Bounds.X + 160))
		{
			if (Event != null)
			{
				yield return DoEvent(Event);
			}
			Event = null;
			yield return null;
		}
	}

	[IteratorStateMachine(typeof(_003CNextLevel_003Ed__27))]
	private IEnumerator NextLevel(string name)
	{
		if (player != null)
		{
			player.RemoveSelf();
		}
		player = null;
		Level.OnEndOfFrame += delegate
		{
			Level.UnloadLevel();
			Level.Session.Level = name;
			Level.Session.RespawnPoint = Level.GetSpawnPoint(new Vector2(Level.Bounds.Left, Level.Bounds.Top));
			Level.LoadLevel(Player.IntroTypes.None);
			Level.Wipe.Cancel();
		};
		yield return null;
		yield return null;
	}

	[IteratorStateMachine(typeof(_003CFadeTo_003Ed__28))]
	private IEnumerator FadeTo(float value)
	{
		while ((fade = Calc.Approach(fade, value, Engine.DeltaTime * 0.5f)) != value)
		{
			yield return null;
		}
		fade = value;
	}

	[IteratorStateMachine(typeof(_003CBadelineApproachWalking_003Ed__29))]
	private IEnumerator BadelineApproachWalking()
	{
		while (badelineWalkApproach < 1f)
		{
			badeline.Floatness = Calc.Approach(badeline.Floatness, 0f, Engine.DeltaTime * 8f);
			badelineWalkApproach = Calc.Approach(badelineWalkApproach, 1f, Engine.DeltaTime * 0.6f);
			yield return null;
		}
	}

	[IteratorStateMachine(typeof(_003CDustyRoutine_003Ed__30))]
	private IEnumerator DustyRoutine(Entity oshiro)
	{
		List<Entity> dusty = new List<Entity>();
		float timer = 0f;
		Vector2 offset = oshiro.Position + new Vector2(220f, -24f);
		Vector2 start = offset;
		for (int i = 0; i < 3; i++)
		{
			Entity entity = new Entity(offset + new Vector2(i * 24, 0f));
			entity.Depth = -50;
			entity.Add(new DustGraphic(ignoreSolids: true, autoControlEyes: false, autoExpandDust: true));
			Image image = new Image(GFX.Game["decals/3-resort/brokenbox_" + (char)(97 + i)]);
			image.JustifyOrigin(0.5f, 1f);
			image.Position = new Vector2(0f, -4f);
			entity.Add(image);
			base.Scene.Add(entity);
			dusty.Add(entity);
		}
		yield return 3.8f;
		while (true)
		{
			for (int j = 0; j < dusty.Count; j++)
			{
				Entity entity2 = dusty[j];
				entity2.X = offset.X + (float)(j * 24);
				entity2.Y = offset.Y + (float)Math.Sin(timer * 4f + (float)j * 0.8f) * 4f;
			}
			if (offset.X < (float)(Level.Bounds.Left + 120))
			{
				offset.Y = Calc.Approach(offset.Y, start.Y + 16f, Engine.DeltaTime * 16f);
			}
			offset.X -= 26f * Engine.DeltaTime;
			timer += Engine.DeltaTime;
			yield return null;
		}
	}

	[IteratorStateMachine(typeof(_003CBadelineAround_003Ed__31))]
	private IEnumerator BadelineAround(Vector2 start, Vector2 around, BadelineDummy badeline = null)
	{
		bool removeAtEnd = badeline == null;
		if (badeline == null)
		{
			Scene scene = base.Scene;
			BadelineDummy entity;
			badeline = (entity = new BadelineDummy(start));
			scene.Add(entity);
		}
		badeline.Sprite.Play("fallSlow");
		float angle = Calc.Angle(around, start);
		float dist = (around - start).Length();
		float duration = 3f;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime / duration)
		{
			float num = p * 2f;
			badeline.Position = around + Calc.AngleToVector(angle - num * ((float)Math.PI * 2f), dist + Calc.YoYo(p) * 16f + (float)Math.Sin(p * ((float)Math.PI * 2f) * 4f) * 5f);
			badeline.Sprite.Scale.X = Math.Sign(around.X - badeline.X);
			if (!removeAtEnd)
			{
				player.Facing = (Facings)Math.Sign(badeline.X - player.X);
			}
			if (base.Scene.OnInterval(0.1f))
			{
				TrailManager.Add(badeline, Player.NormalHairColor);
			}
			yield return null;
		}
		if (removeAtEnd)
		{
			badeline.Vanish();
		}
		else
		{
			badeline.Sprite.Play("laugh");
		}
	}

	[IteratorStateMachine(typeof(_003CDoEvent_003Ed__32))]
	private IEnumerator DoEvent(string e)
	{
		switch (e)
		{
		case "WaitJumpDash":
			yield return EventWaitJumpDash();
			break;
		case "WaitJumpDoubleDash":
			yield return EventWaitJumpDoubleDash();
			break;
		case "ClimbDown":
			yield return EventClimbDown();
			break;
		case "Wait":
			yield return EventWait();
			break;
		}
	}

	[IteratorStateMachine(typeof(_003CEventWaitJumpDash_003Ed__33))]
	private IEnumerator EventWaitJumpDash()
	{
		autoWalk = false;
		player.DummyFriction = true;
		yield return 0.1f;
		PlayerJump(-1);
		yield return 0.2f;
		player.OverrideDashDirection = new Vector2(-1f, -1f);
		player.StateMachine.State = player.StartDash();
		yield return 0.6f;
		player.OverrideDashDirection = null;
		player.StateMachine.State = 11;
		autoWalk = true;
	}

	[IteratorStateMachine(typeof(_003CEventWaitJumpDoubleDash_003Ed__34))]
	private IEnumerator EventWaitJumpDoubleDash()
	{
		autoWalk = false;
		player.DummyFriction = true;
		yield return 0.1f;
		player.Facing = Facings.Right;
		yield return 0.25f;
		yield return BadelineCombine();
		player.Dashes = 2;
		yield return 0.5f;
		player.Facing = Facings.Left;
		yield return 0.7f;
		PlayerJump(-1);
		yield return 0.4f;
		player.OverrideDashDirection = new Vector2(-1f, -1f);
		player.StateMachine.State = player.StartDash();
		yield return 0.6f;
		player.OverrideDashDirection = new Vector2(-1f, 0f);
		player.StateMachine.State = player.StartDash();
		yield return 0.6f;
		player.OverrideDashDirection = null;
		player.StateMachine.State = 11;
		autoWalk = true;
		while (!player.OnGround())
		{
			yield return null;
		}
		autoWalk = false;
		player.DummyFriction = true;
		player.Dashes = 2;
		yield return 0.5f;
		player.Facing = Facings.Right;
		yield return 1f;
		Level.Displacement.AddBurst(player.Position, 0.4f, 8f, 32f, 0.5f);
		badeline.Position = player.Position;
		badeline.Visible = true;
		badelineAutoFloat = true;
		player.Dashes = 1;
		yield return 0.8f;
		player.Facing = Facings.Left;
		autoWalk = true;
		player.DummyFriction = false;
	}

	[IteratorStateMachine(typeof(_003CEventClimbDown_003Ed__35))]
	private IEnumerator EventClimbDown()
	{
		autoWalk = false;
		player.DummyFriction = true;
		yield return 0.1f;
		PlayerJump(-1);
		yield return 0.4f;
		while (!player.CollideCheck<Solid>(player.Position + new Vector2(-1f, 0f)))
		{
			yield return null;
		}
		player.DummyAutoAnimate = false;
		player.Sprite.Play("wallslide");
		while (player.CollideCheck<Solid>(player.Position + new Vector2(-1f, 32f)))
		{
			player.CreateWallSlideParticles(-1);
			player.Speed.Y = Math.Min(player.Speed.Y, 40f);
			yield return null;
		}
		PlayerJump(1);
		yield return 0.4f;
		while (!player.CollideCheck<Solid>(player.Position + new Vector2(1f, 0f)))
		{
			yield return null;
		}
		player.DummyAutoAnimate = false;
		player.Sprite.Play("wallslide");
		while (!player.CollideCheck<Solid>(player.Position + new Vector2(0f, 32f)))
		{
			player.CreateWallSlideParticles(1);
			player.Speed.Y = Math.Min(player.Speed.Y, 40f);
			yield return null;
		}
		PlayerJump(-1);
		yield return 0.4f;
		autoWalk = true;
	}

	[IteratorStateMachine(typeof(_003CEventWait_003Ed__36))]
	private IEnumerator EventWait()
	{
		badeline.Sprite.Play("idle");
		badelineAutoWalk = false;
		autoWalk = false;
		player.DummyFriction = true;
		yield return 0.1f;
		player.DummyAutoAnimate = false;
		player.Speed = Vector2.Zero;
		yield return 0.5f;
		player.Sprite.Play("lookUp");
		yield return 2f;
		BirdNPC birdNPC = base.Scene.Entities.FindFirst<BirdNPC>();
		if (birdNPC != null)
		{
			birdNPC.AutoFly = true;
		}
		yield return 0.1f;
		player.Sprite.Play("idle");
		yield return 1f;
		autoWalk = true;
		player.DummyFriction = false;
		player.DummyAutoAnimate = true;
		badelineAutoWalk = true;
		badelineWalkApproach = 0f;
		badelineWalkApproachFrom = badeline.Position;
		badeline.Sprite.Play("walk");
		while (badelineWalkApproach < 1f)
		{
			badelineWalkApproach += Engine.DeltaTime * 4f;
			yield return null;
		}
	}

	[IteratorStateMachine(typeof(_003CBadelineCombine_003Ed__37))]
	private IEnumerator BadelineCombine()
	{
		Vector2 from = badeline.Position;
		badelineAutoFloat = false;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime / 0.25f)
		{
			badeline.Position = Vector2.Lerp(from, player.Position, Ease.CubeIn(p));
			yield return null;
		}
		badeline.Visible = false;
		Level.Displacement.AddBurst(player.Position, 0.4f, 8f, 32f, 0.5f);
	}

	private void PlayerJump(int direction)
	{
		player.Facing = (Facings)direction;
		player.DummyFriction = false;
		player.DummyAutoAnimate = true;
		player.Speed.X = direction * 120;
		player.Jump();
		player.AutoJump = true;
		player.AutoJumpTimer = 2f;
	}

	private void SetBgFade(float alpha)
	{
		fillbg.Color = Color.Black * alpha;
	}

	public override void Update()
	{
		MInput.Disabled = false;
		if (Level.CanPause && (Input.Pause.Pressed || Input.ESC.Pressed))
		{
			Input.Pause.ConsumeBuffer();
			Input.ESC.ConsumeBuffer();
			Level.Pause(0, minimal: true);
		}
		MInput.Disabled = true;
		if (player != null && player.Scene != null)
		{
			if (player.OverrideDashDirection.HasValue)
			{
				Input.MoveX.Value = (int)player.OverrideDashDirection.Value.X;
				Input.MoveY.Value = (int)player.OverrideDashDirection.Value.Y;
			}
			if (autoWalk)
			{
				if (player.OnGround())
				{
					player.Speed.X = -44.8f;
					bool flag = player.CollideCheck<Solid>(player.Position + new Vector2(-20f, 0f));
					bool flag2 = !player.CollideCheck<Solid>(player.Position + new Vector2(-8f, 1f)) && !player.CollideCheck<Solid>(player.Position + new Vector2(-8f, 32f));
					if (flag || flag2)
					{
						player.Jump();
						player.AutoJump = true;
						player.AutoJumpTimer = (flag ? 0.6f : 2f);
					}
				}
				else
				{
					player.Speed.X = -64f;
				}
			}
			if (badeline != null && badelineAutoFloat)
			{
				Vector2 position = badeline.Position;
				Vector2 vector = player.Position + new Vector2(16f, -16f);
				badeline.Position = position + (vector - position) * (1f - (float)Math.Pow(0.009999999776482582, Engine.DeltaTime));
				badeline.Sprite.Scale.X = -1f;
			}
			if (badeline != null && badelineAutoWalk)
			{
				player.GetChasePosition(base.Scene.TimeActive, 0.35f + (float)Math.Sin(walkOffset) * 0.1f, out var chaseState);
				if (chaseState.OnGround)
				{
					walkOffset += Engine.DeltaTime;
				}
				if (badelineWalkApproach >= 1f)
				{
					badeline.Position = chaseState.Position;
					if (badeline.Sprite.Has(chaseState.Animation))
					{
						badeline.Sprite.Play(chaseState.Animation);
					}
					badeline.Sprite.Scale.X = (float)chaseState.Facing;
				}
				else
				{
					badeline.Position = Vector2.Lerp(badelineWalkApproachFrom, chaseState.Position, badelineWalkApproach);
				}
			}
			if (Math.Abs(player.Speed.X) > 90f)
			{
				player.Speed.X = Calc.Approach(player.Speed.X, 90f * (float)Math.Sign(player.Speed.X), 1000f * Engine.DeltaTime);
			}
		}
		if (credits != null)
		{
			credits.Update();
		}
		base.Update();
	}

	public void PostUpdate()
	{
		if (player != null && player.Scene != null && autoUpdateCamera)
		{
			Vector2 position = Level.Camera.Position;
			Vector2 cameraTarget = player.CameraTarget;
			if (!player.OnGround())
			{
				cameraTarget.Y = (Level.Camera.Y * 2f + cameraTarget.Y) / 3f;
			}
			Level.Camera.Position = position + (cameraTarget - position) * (1f - (float)Math.Pow(0.009999999776482582, Engine.DeltaTime));
			Level.Camera.X = (int)cameraTarget.X;
		}
	}

	public override void Render()
	{
		bool flag = SaveData.Instance != null && SaveData.Instance.Assists.MirrorMode;
		if (!Level.Paused)
		{
			if (flag)
			{
				gradient.Draw(new Vector2(1720f, -10f), Vector2.Zero, Color.White * 0.6f, new Vector2(-1f, 1100f));
			}
			else
			{
				gradient.Draw(new Vector2(200f, -10f), Vector2.Zero, Color.White * 0.6f, new Vector2(1f, 1100f));
			}
		}
		if (fade > 0f)
		{
			Draw.Rect(-10f, -10f, 1940f, 1100f, Color.Black * Ease.CubeInOut(fade));
		}
		if (credits != null && !Level.Paused)
		{
			credits.Render(new Vector2(flag ? 100 : 1820, 0f));
		}
		base.Render();
	}

	public override void OnEnd(Level level)
	{
		SaveData.Instance.Assists.DashAssist = wasDashAssistOn;
		Audio.BusMuted("bus:/gameplay_sfx", false);
		Instance = null;
		MInput.Disabled = false;
		if (!gotoEpilogue)
		{
			Engine.Scene = new OverworldLoader(Overworld.StartMode.AreaComplete, snow);
		}
		else
		{
			LevelEnter.Go(new Session(new AreaKey(8)), fromSaveData: false);
		}
	}
}
