using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using FMOD.Studio;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CSGEN_StrawberrySeeds : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__7 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CSGEN_StrawberrySeeds _003C_003E4__this;

		public Level level;

		private float _003Cdist_003E5__2;

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
		public _003CCutscene_003Ed__7(int _003C_003E1__state)
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
			CSGEN_StrawberrySeeds cSGEN_StrawberrySeeds = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				cSGEN_StrawberrySeeds.sfx = Audio.Play("event:/game/general/seed_complete_main", cSGEN_StrawberrySeeds.Position);
				cSGEN_StrawberrySeeds.snapshot = Audio.CreateSnapshot("snapshot:/music_mains_mute");
				Player entity = cSGEN_StrawberrySeeds.Scene.Tracker.GetEntity<Player>();
				if (entity != null)
				{
					cSGEN_StrawberrySeeds.cameraStart = entity.CameraTarget;
				}
				foreach (StrawberrySeed seed in cSGEN_StrawberrySeeds.strawberry.Seeds)
				{
					seed.OnAllCollected();
				}
				cSGEN_StrawberrySeeds.strawberry.Depth = -2000002;
				cSGEN_StrawberrySeeds.strawberry.AddTag(Tags.FrozenUpdate);
				_003C_003E2__current = 0.35f;
				_003C_003E1__state = 1;
				return true;
			}
			case 1:
				_003C_003E1__state = -1;
				cSGEN_StrawberrySeeds.Tag = (int)Tags.FrozenUpdate | (int)Tags.HUD;
				level.Frozen = true;
				level.FormationBackdrop.Display = true;
				level.FormationBackdrop.Alpha = 0.5f;
				level.Displacement.Clear();
				level.Displacement.Enabled = false;
				Audio.BusPaused("bus:/gameplay_sfx/ambience", true);
				Audio.BusPaused("bus:/gameplay_sfx/char", true);
				Audio.BusPaused("bus:/gameplay_sfx/game/general/yes_pause", true);
				Audio.BusPaused("bus:/gameplay_sfx/game/chapters", true);
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 2;
				return true;
			case 2:
			{
				_003C_003E1__state = -1;
				cSGEN_StrawberrySeeds.system = new ParticleSystem(-2000002, 50);
				cSGEN_StrawberrySeeds.system.Tag = Tags.FrozenUpdate;
				level.Add(cSGEN_StrawberrySeeds.system);
				float num2 = (float)Math.PI * 2f / (float)cSGEN_StrawberrySeeds.strawberry.Seeds.Count;
				float num3 = (float)Math.PI / 2f;
				Vector2 zero = Vector2.Zero;
				foreach (StrawberrySeed seed2 in cSGEN_StrawberrySeeds.strawberry.Seeds)
				{
					zero += seed2.Position;
				}
				zero /= (float)cSGEN_StrawberrySeeds.strawberry.Seeds.Count;
				foreach (StrawberrySeed seed3 in cSGEN_StrawberrySeeds.strawberry.Seeds)
				{
					seed3.StartSpinAnimation(zero, cSGEN_StrawberrySeeds.strawberry.Position, num3, 4f);
					num3 -= num2;
				}
				Vector2 val = cSGEN_StrawberrySeeds.strawberry.Position - new Vector2(160f, 90f);
				val = val.Clamp(level.Bounds.Left, level.Bounds.Top, level.Bounds.Right - 320, level.Bounds.Bottom - 180);
				cSGEN_StrawberrySeeds.Add(new Coroutine(CutsceneEntity.CameraTo(val, 3.5f, Ease.CubeInOut)));
				_003C_003E2__current = 4f;
				_003C_003E1__state = 3;
				return true;
			}
			case 3:
				_003C_003E1__state = -1;
				Input.Rumble(RumbleStrength.Light, RumbleLength.Long);
				Audio.Play("event:/game/general/seed_complete_berry", cSGEN_StrawberrySeeds.strawberry.Position);
				foreach (StrawberrySeed seed4 in cSGEN_StrawberrySeeds.strawberry.Seeds)
				{
					seed4.StartCombineAnimation(cSGEN_StrawberrySeeds.strawberry.Position, 0.6f, cSGEN_StrawberrySeeds.system);
				}
				_003C_003E2__current = 0.6f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
				foreach (StrawberrySeed seed5 in cSGEN_StrawberrySeeds.strawberry.Seeds)
				{
					seed5.RemoveSelf();
				}
				cSGEN_StrawberrySeeds.strawberry.CollectedSeeds();
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				_003Cdist_003E5__2 = (level.Camera.Position - cSGEN_StrawberrySeeds.cameraStart).Length();
				_003C_003E2__current = CutsceneEntity.CameraTo(cSGEN_StrawberrySeeds.cameraStart, _003Cdist_003E5__2 / 180f);
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				if (_003Cdist_003E5__2 > 80f)
				{
					_003C_003E2__current = 0.25f;
					_003C_003E1__state = 7;
					return true;
				}
				break;
			case 7:
				_003C_003E1__state = -1;
				break;
			}
			level.EndCutscene();
			cSGEN_StrawberrySeeds.OnEnd(level);
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

	private Strawberry strawberry;

	private Vector2 cameraStart;

	private ParticleSystem system;

	private FMOD.Studio.EventInstance snapshot;

	private FMOD.Studio.EventInstance sfx;

	public CSGEN_StrawberrySeeds(Strawberry strawberry)
	{
		this.strawberry = strawberry;
	}

	public override void OnBegin(Level level)
	{
		cameraStart = level.Camera.Position;
		Add(new Coroutine(Cutscene(level)));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__7))]
	private IEnumerator Cutscene(Level level)
	{
		sfx = Audio.Play("event:/game/general/seed_complete_main", Position);
		snapshot = Audio.CreateSnapshot("snapshot:/music_mains_mute");
		Player entity = base.Scene.Tracker.GetEntity<Player>();
		if (entity != null)
		{
			cameraStart = entity.CameraTarget;
		}
		foreach (StrawberrySeed seed in strawberry.Seeds)
		{
			seed.OnAllCollected();
		}
		strawberry.Depth = -2000002;
		strawberry.AddTag(Tags.FrozenUpdate);
		yield return 0.35f;
		base.Tag = (int)Tags.FrozenUpdate | (int)Tags.HUD;
		level.Frozen = true;
		level.FormationBackdrop.Display = true;
		level.FormationBackdrop.Alpha = 0.5f;
		level.Displacement.Clear();
		level.Displacement.Enabled = false;
		Audio.BusPaused("bus:/gameplay_sfx/ambience", true);
		Audio.BusPaused("bus:/gameplay_sfx/char", true);
		Audio.BusPaused("bus:/gameplay_sfx/game/general/yes_pause", true);
		Audio.BusPaused("bus:/gameplay_sfx/game/chapters", true);
		yield return 0.1f;
		system = new ParticleSystem(-2000002, 50);
		system.Tag = Tags.FrozenUpdate;
		level.Add(system);
		float num = (float)Math.PI * 2f / (float)strawberry.Seeds.Count;
		float num2 = (float)Math.PI / 2f;
		Vector2 zero = Vector2.Zero;
		foreach (StrawberrySeed seed2 in strawberry.Seeds)
		{
			zero += seed2.Position;
		}
		zero /= (float)strawberry.Seeds.Count;
		foreach (StrawberrySeed seed3 in strawberry.Seeds)
		{
			seed3.StartSpinAnimation(zero, strawberry.Position, num2, 4f);
			num2 -= num;
		}
		Vector2 val = strawberry.Position - new Vector2(160f, 90f);
		val = val.Clamp(level.Bounds.Left, level.Bounds.Top, level.Bounds.Right - 320, level.Bounds.Bottom - 180);
		Add(new Coroutine(CutsceneEntity.CameraTo(val, 3.5f, Ease.CubeInOut)));
		yield return 4f;
		Input.Rumble(RumbleStrength.Light, RumbleLength.Long);
		Audio.Play("event:/game/general/seed_complete_berry", strawberry.Position);
		foreach (StrawberrySeed seed4 in strawberry.Seeds)
		{
			seed4.StartCombineAnimation(strawberry.Position, 0.6f, system);
		}
		yield return 0.6f;
		Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
		foreach (StrawberrySeed seed5 in strawberry.Seeds)
		{
			seed5.RemoveSelf();
		}
		strawberry.CollectedSeeds();
		yield return 0.5f;
		float dist = (level.Camera.Position - cameraStart).Length();
		yield return CutsceneEntity.CameraTo(cameraStart, dist / 180f);
		if (dist > 80f)
		{
			yield return 0.25f;
		}
		level.EndCutscene();
		OnEnd(level);
	}

	public override void OnEnd(Level level)
	{
		if (WasSkipped)
		{
			Audio.Stop(sfx);
		}
		level.OnEndOfFrame += delegate
		{
			if (WasSkipped)
			{
				foreach (StrawberrySeed seed in strawberry.Seeds)
				{
					seed.RemoveSelf();
				}
				strawberry.CollectedSeeds();
				level.Camera.Position = cameraStart;
			}
			strawberry.Depth = -100;
			strawberry.RemoveTag(Tags.FrozenUpdate);
			level.Frozen = false;
			level.FormationBackdrop.Display = false;
			level.Displacement.Enabled = true;
		};
		RemoveSelf();
	}

	private void EndSfx()
	{
		Audio.BusPaused("bus:/gameplay_sfx/ambience", false);
		Audio.BusPaused("bus:/gameplay_sfx/char", false);
		Audio.BusPaused("bus:/gameplay_sfx/game/general/yes_pause", false);
		Audio.BusPaused("bus:/gameplay_sfx/game/chapters", false);
		Audio.ReleaseSnapshot(snapshot);
	}

	public override void Removed(Scene scene)
	{
		EndSfx();
		base.Removed(scene);
	}

	public override void SceneEnd(Scene scene)
	{
		EndSfx();
		base.SceneEnd(scene);
	}
}
