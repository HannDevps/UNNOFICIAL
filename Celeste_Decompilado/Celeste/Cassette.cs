using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using FMOD.Studio;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class Cassette : Entity
{
	private class UnlockedBSide : Entity
	{
		[CompilerGenerated]
		private sealed class _003CEaseIn_003Ed__5 : IEnumerator<object>, IDisposable, IEnumerator
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UnlockedBSide _003C_003E4__this;

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
			public _003CEaseIn_003Ed__5(int _003C_003E1__state)
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
				UnlockedBSide unlockedBSide = _003C_003E4__this;
				switch (num)
				{
				default:
					return false;
				case 0:
					_003C_003E1__state = -1;
					_ = unlockedBSide.Scene;
					goto IL_0049;
				case 1:
					_003C_003E1__state = -1;
					goto IL_0049;
				case 2:
					{
						_003C_003E1__state = -1;
						unlockedBSide.waitForKeyPress = true;
						return false;
					}
					IL_0049:
					if ((unlockedBSide.alpha += Engine.DeltaTime / 0.5f) < 1f)
					{
						_003C_003E2__current = null;
						_003C_003E1__state = 1;
						return true;
					}
					unlockedBSide.alpha = 1f;
					_003C_003E2__current = 1.5f;
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
		private sealed class _003CEaseOut_003Ed__6 : IEnumerator<object>, IDisposable, IEnumerator
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UnlockedBSide _003C_003E4__this;

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
			public _003CEaseOut_003Ed__6(int _003C_003E1__state)
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
				UnlockedBSide unlockedBSide = _003C_003E4__this;
				switch (num)
				{
				default:
					return false;
				case 0:
					_003C_003E1__state = -1;
					unlockedBSide.waitForKeyPress = false;
					break;
				case 1:
					_003C_003E1__state = -1;
					break;
				}
				if ((unlockedBSide.alpha -= Engine.DeltaTime / 0.5f) > 0f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				unlockedBSide.alpha = 0f;
				unlockedBSide.RemoveSelf();
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

		private float alpha;

		private string text;

		private bool waitForKeyPress;

		private float timer;

		public override void Added(Scene scene)
		{
			base.Added(scene);
			base.Tag = (int)Tags.HUD | (int)Tags.PauseUpdate;
			text = ActiveFont.FontSize.AutoNewline(Dialog.Clean("UI_REMIX_UNLOCKED"), 900);
			base.Depth = -10000;
		}

		[IteratorStateMachine(typeof(_003CEaseIn_003Ed__5))]
		public IEnumerator EaseIn()
		{
			_ = base.Scene;
			while ((alpha += Engine.DeltaTime / 0.5f) < 1f)
			{
				yield return null;
			}
			alpha = 1f;
			yield return 1.5f;
			waitForKeyPress = true;
		}

		[IteratorStateMachine(typeof(_003CEaseOut_003Ed__6))]
		public IEnumerator EaseOut()
		{
			waitForKeyPress = false;
			while ((alpha -= Engine.DeltaTime / 0.5f) > 0f)
			{
				yield return null;
			}
			alpha = 0f;
			RemoveSelf();
		}

		public override void Update()
		{
			timer += Engine.DeltaTime;
			base.Update();
		}

		public override void Render()
		{
			float num = Ease.CubeOut(alpha);
			Vector2 vector = Celeste.TargetCenter + new Vector2(0f, 64f);
			Vector2 vector2 = Vector2.UnitY * 64f * (1f - num);
			Draw.Rect(-10f, -10f, 1940f, 1100f, Color.Black * num * 0.8f);
			GFX.Gui["collectables/cassette"].DrawJustified(vector - vector2 + new Vector2(0f, 32f), new Vector2(0.5f, 1f), Color.White * num);
			ActiveFont.Draw(text, vector + vector2, new Vector2(0.5f, 0f), Vector2.One, Color.White * num);
			if (waitForKeyPress)
			{
				GFX.Gui["textboxbutton"].DrawCentered(new Vector2(1824f, 984 + ((timer % 1f < 0.25f) ? 6 : 0)));
			}
		}
	}

	[CompilerGenerated]
	private sealed class _003CCollectRoutine_003Ed__19 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Cassette _003C_003E4__this;

		public Player player;

		private Level _003Clevel_003E5__2;

		private CassetteBlockManager _003Ccbm_003E5__3;

		private Vector2 _003CcamWas_003E5__4;

		private Vector2 _003CcamTo_003E5__5;

		private Vector2 _003Cfrom_003E5__6;

		private Vector2 _003Cto_003E5__7;

		private float _003Cduration_003E5__8;

		private float _003Cp_003E5__9;

		private UnlockedBSide _003Cmessage_003E5__10;

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
		public _003CCollectRoutine_003Ed__19(int _003C_003E1__state)
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
			Cassette cassette = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cassette.collecting = true;
				_003Clevel_003E5__2 = cassette.Scene as Level;
				_003Ccbm_003E5__3 = cassette.Scene.Tracker.GetEntity<CassetteBlockManager>();
				_003Clevel_003E5__2.PauseLock = true;
				_003Clevel_003E5__2.Frozen = true;
				cassette.Tag = Tags.FrozenUpdate;
				_003Clevel_003E5__2.Session.Cassette = true;
				_003Clevel_003E5__2.Session.RespawnPoint = _003Clevel_003E5__2.GetSpawnPoint(cassette.nodes[1]);
				_003Clevel_003E5__2.Session.UpdateLevelStartDashes();
				SaveData.Instance.RegisterCassette(_003Clevel_003E5__2.Session.Area);
				if (_003Ccbm_003E5__3 != null)
				{
					_003Ccbm_003E5__3.StopBlocks();
				}
				cassette.Depth = -1000000;
				_003Clevel_003E5__2.Shake();
				_003Clevel_003E5__2.Flash(Color.White);
				_003Clevel_003E5__2.Displacement.Clear();
				_003CcamWas_003E5__4 = _003Clevel_003E5__2.Camera.Position;
				_003CcamTo_003E5__5 = (cassette.Position - new Vector2(160f, 90f)).Clamp(_003Clevel_003E5__2.Bounds.Left - 64, _003Clevel_003E5__2.Bounds.Top - 32, _003Clevel_003E5__2.Bounds.Right + 64 - 320, _003Clevel_003E5__2.Bounds.Bottom + 32 - 180);
				_003Clevel_003E5__2.Camera.Position = _003CcamTo_003E5__5;
				_003Clevel_003E5__2.ZoomSnap((cassette.Position - _003Clevel_003E5__2.Camera.Position).Clamp(60f, 60f, 260f, 120f), 2f);
				cassette.sprite.Play("spin", restart: true);
				cassette.sprite.Rate = 2f;
				_003Cp_003E5__9 = 0f;
				goto IL_02bf;
			case 1:
				_003C_003E1__state = -1;
				_003Cp_003E5__9 += Engine.DeltaTime;
				goto IL_02bf;
			case 2:
				_003C_003E1__state = -1;
				_003Cfrom_003E5__6 = cassette.Position;
				_003Cto_003E5__7 = new Vector2(cassette.X, _003Clevel_003E5__2.Camera.Top - 16f);
				_003Cduration_003E5__8 = 0.4f;
				_003Cp_003E5__9 = 0f;
				goto IL_0402;
			case 3:
				_003C_003E1__state = -1;
				_003Cp_003E5__9 += Engine.DeltaTime / _003Cduration_003E5__8;
				goto IL_0402;
			case 4:
				_003C_003E1__state = -1;
				goto IL_04b2;
			case 5:
				_003C_003E1__state = -1;
				goto IL_04b2;
			case 6:
				_003C_003E1__state = -1;
				_003Cmessage_003E5__10 = null;
				_003Cduration_003E5__8 = 0.25f;
				cassette.Add(new Coroutine(_003Clevel_003E5__2.ZoomBack(_003Cduration_003E5__8 - 0.05f)));
				_003Cp_003E5__9 = 0f;
				goto IL_0597;
			case 7:
				_003C_003E1__state = -1;
				_003Cp_003E5__9 += Engine.DeltaTime / _003Cduration_003E5__8;
				goto IL_0597;
			case 8:
				{
					_003C_003E1__state = -1;
					if (_003Ccbm_003E5__3 != null)
					{
						_003Ccbm_003E5__3.Finish();
					}
					_003Clevel_003E5__2.PauseLock = false;
					_003Clevel_003E5__2.ResetZoom();
					cassette.RemoveSelf();
					return false;
				}
				IL_04b2:
				if (!Input.MenuConfirm.Pressed)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 5;
					return true;
				}
				Audio.SetParameter(cassette.remixSfx, "end", 1f);
				_003C_003E2__current = _003Cmessage_003E5__10.EaseOut();
				_003C_003E1__state = 6;
				return true;
				IL_0597:
				if (_003Cp_003E5__9 < 1f)
				{
					_003Clevel_003E5__2.Camera.Position = Vector2.Lerp(_003CcamTo_003E5__5, _003CcamWas_003E5__4, Ease.SineInOut(_003Cp_003E5__9));
					_003C_003E2__current = null;
					_003C_003E1__state = 7;
					return true;
				}
				if (!player.Dead && cassette.nodes != null && cassette.nodes.Length >= 2)
				{
					Audio.Play("event:/game/general/cassette_bubblereturn", _003Clevel_003E5__2.Camera.Position + new Vector2(160f, 90f));
					player.StartCassetteFly(cassette.nodes[1], cassette.nodes[0]);
				}
				foreach (SandwichLava item in _003Clevel_003E5__2.Entities.FindAll<SandwichLava>())
				{
					item.Leave();
				}
				_003Clevel_003E5__2.Frozen = false;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 8;
				return true;
				IL_0402:
				if (_003Cp_003E5__9 < 1f)
				{
					cassette.sprite.Scale.X = MathHelper.Lerp(1f, 0.1f, _003Cp_003E5__9);
					cassette.sprite.Scale.Y = MathHelper.Lerp(1f, 3f, _003Cp_003E5__9);
					cassette.Position = Vector2.Lerp(_003Cfrom_003E5__6, _003Cto_003E5__7, Ease.CubeIn(_003Cp_003E5__9));
					_003C_003E2__current = null;
					_003C_003E1__state = 3;
					return true;
				}
				cassette.Visible = false;
				_003Cfrom_003E5__6 = default(Vector2);
				_003Cto_003E5__7 = default(Vector2);
				cassette.remixSfx = Audio.Play("event:/game/general/cassette_preview", "remix", _003Clevel_003E5__2.Session.Area.ID);
				_003Cmessage_003E5__10 = new UnlockedBSide();
				cassette.Scene.Add(_003Cmessage_003E5__10);
				_003C_003E2__current = _003Cmessage_003E5__10.EaseIn();
				_003C_003E1__state = 4;
				return true;
				IL_02bf:
				if (_003Cp_003E5__9 < 1.5f)
				{
					cassette.sprite.Rate += Engine.DeltaTime * 4f;
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				cassette.sprite.Rate = 0f;
				cassette.sprite.SetAnimationFrame(0);
				cassette.scaleWiggler.Start();
				_003C_003E2__current = 0.25f;
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

	public static ParticleType P_Shine;

	public static ParticleType P_Collect;

	public bool IsGhost;

	private Sprite sprite;

	private SineWave hover;

	private BloomPoint bloom;

	private VertexLight light;

	private Wiggler scaleWiggler;

	private bool collected;

	private Vector2[] nodes;

	private FMOD.Studio.EventInstance remixSfx;

	private bool collecting;

	public Cassette(Vector2 position, Vector2[] nodes)
		: base(position)
	{
		base.Collider = new Hitbox(16f, 16f, -8f, -8f);
		this.nodes = nodes;
		Add(new PlayerCollider(OnPlayer));
	}

	public Cassette(EntityData data, Vector2 offset)
		: this(data.Position + offset, data.NodesOffset(offset))
	{
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		IsGhost = SaveData.Instance.Areas[SceneAs<Level>().Session.Area.ID].Cassette;
		Add(sprite = GFX.SpriteBank.Create(IsGhost ? "cassetteGhost" : "cassette"));
		sprite.Play("idle");
		Add(scaleWiggler = Wiggler.Create(0.25f, 4f, delegate(float f)
		{
			sprite.Scale = Vector2.One * (1f + f * 0.25f);
		}));
		Add(bloom = new BloomPoint(0.25f, 16f));
		Add(light = new VertexLight(Color.White, 0.4f, 32, 64));
		Add(hover = new SineWave(0.5f));
		hover.OnUpdate = delegate(float f)
		{
			Sprite obj = sprite;
			VertexLight vertexLight = light;
			float num = (bloom.Y = f * 2f);
			float y = (vertexLight.Y = num);
			obj.Y = y;
		};
		if (IsGhost)
		{
			sprite.Color = Color.White * 0.8f;
		}
	}

	public override void SceneEnd(Scene scene)
	{
		base.SceneEnd(scene);
		Audio.Stop(remixSfx);
	}

	public override void Removed(Scene scene)
	{
		base.Removed(scene);
		Audio.Stop(remixSfx);
	}

	public override void Update()
	{
		base.Update();
		if (!collecting && base.Scene.OnInterval(0.1f))
		{
			SceneAs<Level>().Particles.Emit(P_Shine, 1, base.Center, new Vector2(12f, 10f));
		}
	}

	private void OnPlayer(Player player)
	{
		if (!collected)
		{
			player?.RefillStamina();
			Audio.Play("event:/game/general/cassette_get", Position);
			collected = true;
			Celeste.Freeze(0.1f);
			Add(new Coroutine(CollectRoutine(player)));
		}
	}

	[IteratorStateMachine(typeof(_003CCollectRoutine_003Ed__19))]
	private IEnumerator CollectRoutine(Player player)
	{
		collecting = true;
		Level level = base.Scene as Level;
		CassetteBlockManager cbm = base.Scene.Tracker.GetEntity<CassetteBlockManager>();
		level.PauseLock = true;
		level.Frozen = true;
		base.Tag = Tags.FrozenUpdate;
		level.Session.Cassette = true;
		level.Session.RespawnPoint = level.GetSpawnPoint(nodes[1]);
		level.Session.UpdateLevelStartDashes();
		SaveData.Instance.RegisterCassette(level.Session.Area);
		cbm?.StopBlocks();
		base.Depth = -1000000;
		level.Shake();
		level.Flash(Color.White);
		level.Displacement.Clear();
		Vector2 camWas = level.Camera.Position;
		Vector2 camTo = (Position - new Vector2(160f, 90f)).Clamp(level.Bounds.Left - 64, level.Bounds.Top - 32, level.Bounds.Right + 64 - 320, level.Bounds.Bottom + 32 - 180);
		level.Camera.Position = camTo;
		level.ZoomSnap((Position - level.Camera.Position).Clamp(60f, 60f, 260f, 120f), 2f);
		sprite.Play("spin", restart: true);
		sprite.Rate = 2f;
		for (float p = 0f; p < 1.5f; p += Engine.DeltaTime)
		{
			sprite.Rate += Engine.DeltaTime * 4f;
			yield return null;
		}
		sprite.Rate = 0f;
		sprite.SetAnimationFrame(0);
		scaleWiggler.Start();
		yield return 0.25f;
		Vector2 from = Position;
		Vector2 to = new Vector2(base.X, level.Camera.Top - 16f);
		float duration = 0.4f;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime / duration)
		{
			sprite.Scale.X = MathHelper.Lerp(1f, 0.1f, p);
			sprite.Scale.Y = MathHelper.Lerp(1f, 3f, p);
			Position = Vector2.Lerp(from, to, Ease.CubeIn(p));
			yield return null;
		}
		Visible = false;
		remixSfx = Audio.Play("event:/game/general/cassette_preview", "remix", level.Session.Area.ID);
		UnlockedBSide message = new UnlockedBSide();
		base.Scene.Add(message);
		yield return message.EaseIn();
		while (!Input.MenuConfirm.Pressed)
		{
			yield return null;
		}
		Audio.SetParameter(remixSfx, "end", 1f);
		yield return message.EaseOut();
		duration = 0.25f;
		Add(new Coroutine(level.ZoomBack(duration - 0.05f)));
		for (float p = 0f; p < 1f; p += Engine.DeltaTime / duration)
		{
			level.Camera.Position = Vector2.Lerp(camTo, camWas, Ease.SineInOut(p));
			yield return null;
		}
		if (!player.Dead && nodes != null && nodes.Length >= 2)
		{
			Audio.Play("event:/game/general/cassette_bubblereturn", level.Camera.Position + new Vector2(160f, 90f));
			player.StartCassetteFly(nodes[1], nodes[0]);
		}
		foreach (SandwichLava item in level.Entities.FindAll<SandwichLava>())
		{
			item.Leave();
		}
		level.Frozen = false;
		yield return 0.25f;
		cbm?.Finish();
		level.PauseLock = false;
		level.ResetZoom();
		RemoveSelf();
	}
}
