using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using FMOD.Studio;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class WaveDashTutorialMachine : JumpThru
{
	[CompilerGenerated]
	private sealed class _003CInteractRoutine_003Ed__24 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public WaveDashTutorialMachine _003C_003E4__this;

		public Player player;

		private Level _003Clevel_003E5__2;

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
		public _003CInteractRoutine_003Ed__24(int _003C_003E1__state)
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
			WaveDashTutorialMachine waveDashTutorialMachine = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Clevel_003E5__2 = waveDashTutorialMachine.Scene as Level;
				player.StateMachine.State = 11;
				player.StateMachine.Locked = true;
				_003C_003E2__current = CutsceneEntity.CameraTo(new Vector2(waveDashTutorialMachine.X, waveDashTutorialMachine.Y - 30f) - new Vector2(160f, 90f), 0.25f, Ease.CubeOut);
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = _003Clevel_003E5__2.ZoomTo(new Vector2(160f, 90f), 10f, 1f);
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				waveDashTutorialMachine.usingSfx = Audio.Play("event:/state/cafe_computer_active", player.Position);
				Audio.Play("event:/new_content/game/10_farewell/cafe_computer_on", player.Position);
				Audio.Play("event:/new_content/game/10_farewell/cafe_computer_startupsfx", player.Position);
				waveDashTutorialMachine.presentation = new WaveDashPresentation(waveDashTutorialMachine.usingSfx);
				waveDashTutorialMachine.Scene.Add(waveDashTutorialMachine.presentation);
				goto IL_0176;
			case 3:
				_003C_003E1__state = -1;
				goto IL_0176;
			case 4:
				{
					_003C_003E1__state = -1;
					player.StateMachine.Locked = false;
					player.StateMachine.State = 0;
					waveDashTutorialMachine.inCutscene = false;
					_003Clevel_003E5__2.EndCutscene();
					Audio.SetAltMusic(null);
					return false;
				}
				IL_0176:
				if (waveDashTutorialMachine.presentation.Viewing)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 3;
					return true;
				}
				_003C_003E2__current = _003Clevel_003E5__2.ZoomTo(new Vector2(160f, 90f), waveDashTutorialMachine.interactStartZoom, 1f);
				_003C_003E1__state = 4;
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

	private Entity frontEntity;

	private Image backSprite;

	private Image frontRightSprite;

	private Image frontLeftSprite;

	private Sprite noise;

	private Sprite neon;

	private Solid frontWall;

	private float insideEase;

	private float cameraEase;

	private bool playerInside;

	private bool inCutscene;

	private Coroutine routine;

	private WaveDashPresentation presentation;

	private float interactStartZoom;

	private FMOD.Studio.EventInstance snapshot;

	private FMOD.Studio.EventInstance usingSfx;

	private SoundSource signSfx;

	private TalkComponent talk;

	public WaveDashTutorialMachine(Vector2 position)
		: base(position, 88, safe: true)
	{
		base.Tag = Tags.TransitionUpdate;
		base.Depth = 1000;
		base.Hitbox.Position = new Vector2(-41f, -59f);
		Add(backSprite = new Image(GFX.Game["objects/wavedashtutorial/building_back"]));
		backSprite.JustifyOrigin(0.5f, 1f);
		Add(noise = new Sprite(GFX.Game, "objects/wavedashtutorial/noise"));
		noise.AddLoop("static", "", 0.05f);
		noise.Play("static");
		noise.CenterOrigin();
		noise.Position = new Vector2(0f, -30f);
		noise.Color = Color.White * 0.5f;
		Add(frontLeftSprite = new Image(GFX.Game["objects/wavedashtutorial/building_front_left"]));
		frontLeftSprite.JustifyOrigin(0.5f, 1f);
		Add(talk = new TalkComponent(new Rectangle(-12, -8, 24, 8), new Vector2(0f, -50f), OnInteract));
		talk.Enabled = false;
		SurfaceSoundIndex = 42;
	}

	public WaveDashTutorialMachine(EntityData data, Vector2 position)
		: this(data.Position + position)
	{
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		scene.Add(frontEntity = new Entity(Position));
		frontEntity.Tag = Tags.TransitionUpdate;
		frontEntity.Depth = -10500;
		frontEntity.Add(frontRightSprite = new Image(GFX.Game["objects/wavedashtutorial/building_front_right"]));
		frontRightSprite.JustifyOrigin(0.5f, 1f);
		frontEntity.Add(neon = new Sprite(GFX.Game, "objects/wavedashtutorial/neon_"));
		neon.AddLoop("loop", "", 0.07f);
		neon.Play("loop");
		neon.JustifyOrigin(0.5f, 1f);
		scene.Add(frontWall = new Solid(Position + new Vector2(-41f, -59f), 88f, 38f, safe: true));
		frontWall.SurfaceSoundIndex = 42;
	}

	public override void Awake(Scene scene)
	{
		base.Awake(scene);
		Add(signSfx = new SoundSource(new Vector2(8f, -16f), "event:/new_content/env/local/cafe_sign"));
	}

	public override void Update()
	{
		base.Update();
		if (!inCutscene)
		{
			Player entity = base.Scene.Tracker.GetEntity<Player>();
			if (entity != null)
			{
				frontWall.Collidable = true;
				bool flag = (entity.X > base.X - 37f && entity.X < base.X + 46f && entity.Y > base.Y - 58f) || frontWall.CollideCheck(entity);
				if (flag != playerInside)
				{
					playerInside = flag;
					if (playerInside)
					{
						signSfx.Stop();
						snapshot = Audio.CreateSnapshot("snapshot:/game_10_inside_cafe");
					}
					else
					{
						signSfx.Play("event:/new_content/env/local/cafe_sign");
						Audio.ReleaseSnapshot(snapshot);
						snapshot = null;
					}
				}
			}
			SceneAs<Level>().ZoomSnap(new Vector2(160f, 90f), 1f + Ease.QuadInOut(cameraEase) * 0.75f);
		}
		talk.Enabled = playerInside;
		frontWall.Collidable = !playerInside;
		insideEase = Calc.Approach(insideEase, playerInside ? 1f : 0f, Engine.DeltaTime * 4f);
		cameraEase = Calc.Approach(cameraEase, playerInside ? 1f : 0f, Engine.DeltaTime * 2f);
		frontRightSprite.Color = Color.White * (1f - insideEase);
		frontLeftSprite.Color = frontRightSprite.Color;
		neon.Color = frontRightSprite.Color;
		frontRightSprite.Visible = insideEase < 1f;
		frontLeftSprite.Visible = insideEase < 1f;
		neon.Visible = insideEase < 1f;
		if (base.Scene.OnInterval(0.05f))
		{
			noise.Scale = Calc.Random.Choose(new Vector2(1f, 1f), new Vector2(-1f, 1f), new Vector2(1f, -1f), new Vector2(-1f, -1f));
		}
	}

	private void OnInteract(Player player)
	{
		if (!inCutscene)
		{
			Level level = base.Scene as Level;
			if (usingSfx != null)
			{
				Audio.SetParameter(usingSfx, "end", 1f);
				Audio.Stop(usingSfx);
			}
			inCutscene = true;
			interactStartZoom = level.ZoomTarget;
			level.StartCutscene(SkipInteraction, fadeInOnSkip: true, endingChapterAfterCutscene: false, resetZoomOnSkip: false);
			Add(routine = new Coroutine(InteractRoutine(player)));
		}
	}

	[IteratorStateMachine(typeof(_003CInteractRoutine_003Ed__24))]
	private IEnumerator InteractRoutine(Player player)
	{
		Level level = base.Scene as Level;
		player.StateMachine.State = 11;
		player.StateMachine.Locked = true;
		yield return CutsceneEntity.CameraTo(new Vector2(base.X, base.Y - 30f) - new Vector2(160f, 90f), 0.25f, Ease.CubeOut);
		yield return level.ZoomTo(new Vector2(160f, 90f), 10f, 1f);
		usingSfx = Audio.Play("event:/state/cafe_computer_active", player.Position);
		Audio.Play("event:/new_content/game/10_farewell/cafe_computer_on", player.Position);
		Audio.Play("event:/new_content/game/10_farewell/cafe_computer_startupsfx", player.Position);
		presentation = new WaveDashPresentation(usingSfx);
		base.Scene.Add(presentation);
		while (presentation.Viewing)
		{
			yield return null;
		}
		yield return level.ZoomTo(new Vector2(160f, 90f), interactStartZoom, 1f);
		player.StateMachine.Locked = false;
		player.StateMachine.State = 0;
		inCutscene = false;
		level.EndCutscene();
		Audio.SetAltMusic(null);
	}

	private void SkipInteraction(Level level)
	{
		Audio.SetAltMusic(null);
		inCutscene = false;
		level.ZoomSnap(new Vector2(160f, 90f), interactStartZoom);
		if (usingSfx != null)
		{
			Audio.SetParameter(usingSfx, "end", 1f);
			usingSfx.release();
		}
		if (presentation != null)
		{
			presentation.RemoveSelf();
		}
		presentation = null;
		if (routine != null)
		{
			routine.RemoveSelf();
		}
		routine = null;
		Player entity = level.Tracker.GetEntity<Player>();
		if (entity != null)
		{
			entity.StateMachine.Locked = false;
			entity.StateMachine.State = 0;
		}
	}

	public override void Removed(Scene scene)
	{
		base.Removed(scene);
		Dispose();
	}

	public override void SceneEnd(Scene scene)
	{
		base.SceneEnd(scene);
		Dispose();
	}

	private void Dispose()
	{
		if (usingSfx != null)
		{
			Audio.SetParameter(usingSfx, "quit", 1f);
			usingSfx.release();
			usingSfx = null;
		}
		Audio.ReleaseSnapshot(snapshot);
		snapshot = null;
	}
}
