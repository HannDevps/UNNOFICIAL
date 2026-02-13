using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS07_Ascend : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__9 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS07_Ascend _003C_003E4__this;

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
		public _003CCutscene_003Ed__9(int _003C_003E1__state)
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
			CS07_Ascend cS07_Ascend = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				goto IL_0046;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0046;
			case 2:
				_003C_003E1__state = -1;
				Audio.Play("event:/char/badeline/maddy_join");
				cS07_Ascend.spinning = false;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				{
					_003C_003E1__state = -1;
					cS07_Ascend.badeline.RemoveSelf();
					cS07_Ascend.player.Dashes = 2;
					cS07_Ascend.player.CreateSplitParticles();
					Input.Rumble(RumbleStrength.Light, RumbleLength.Medium);
					cS07_Ascend.Level.Displacement.AddBurst(cS07_Ascend.player.Position, 0.4f, 8f, 32f, 0.5f);
					cS07_Ascend.EndCutscene(cS07_Ascend.Level);
					return false;
				}
				IL_0046:
				if ((cS07_Ascend.player = cS07_Ascend.Scene.Tracker.GetEntity<Player>()) == null)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				cS07_Ascend.origin = cS07_Ascend.player.Position;
				Audio.Play("event:/char/badeline/maddy_split");
				cS07_Ascend.player.CreateSplitParticles();
				Input.Rumble(RumbleStrength.Light, RumbleLength.Medium);
				cS07_Ascend.Level.Displacement.AddBurst(cS07_Ascend.player.Position, 0.4f, 8f, 32f, 0.5f);
				cS07_Ascend.player.Dashes = 1;
				cS07_Ascend.player.Facing = Facings.Right;
				cS07_Ascend.Scene.Add(cS07_Ascend.badeline = new BadelineDummy(cS07_Ascend.player.Position));
				cS07_Ascend.badeline.AutoAnimator.Enabled = false;
				cS07_Ascend.spinning = true;
				cS07_Ascend.Add(new Coroutine(cS07_Ascend.SpinCharacters()));
				_003C_003E2__current = Textbox.Say(cS07_Ascend.cutscene);
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
	private sealed class _003CSpinCharacters_003Ed__10 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS07_Ascend _003C_003E4__this;

		private float _003Cdist_003E5__2;

		private Vector2 _003Ccenter_003E5__3;

		private float _003Ctimer_003E5__4;

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
		public _003CSpinCharacters_003Ed__10(int _003C_003E1__state)
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
			CS07_Ascend cS07_Ascend = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Cdist_003E5__2 = 0f;
				_003Ccenter_003E5__3 = cS07_Ascend.player.Position;
				_003Ctimer_003E5__4 = (float)Math.PI / 2f;
				cS07_Ascend.player.Sprite.Play("spin");
				cS07_Ascend.badeline.Sprite.Play("spin");
				cS07_Ascend.badeline.Sprite.Scale.X = 1f;
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if (cS07_Ascend.spinning || _003Cdist_003E5__2 > 0f)
			{
				_003Cdist_003E5__2 = Calc.Approach(_003Cdist_003E5__2, cS07_Ascend.spinning ? 1f : 0f, Engine.DeltaTime * 4f);
				int num2 = (int)(_003Ctimer_003E5__4 / ((float)Math.PI * 2f) * 14f + 10f);
				float num3 = (float)Math.Sin(_003Ctimer_003E5__4);
				float num4 = (float)Math.Cos(_003Ctimer_003E5__4);
				float num5 = Ease.CubeOut(_003Cdist_003E5__2) * 32f;
				cS07_Ascend.player.Sprite.SetAnimationFrame(num2);
				cS07_Ascend.badeline.Sprite.SetAnimationFrame(num2 + 7);
				cS07_Ascend.player.Position = _003Ccenter_003E5__3 - new Vector2(num3 * num5, num4 * _003Cdist_003E5__2 * 8f);
				cS07_Ascend.badeline.Position = _003Ccenter_003E5__3 + new Vector2(num3 * num5, num4 * _003Cdist_003E5__2 * 8f);
				_003Ctimer_003E5__4 -= Engine.DeltaTime * 2f;
				if (_003Ctimer_003E5__4 <= 0f)
				{
					_003Ctimer_003E5__4 += (float)Math.PI * 2f;
				}
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

	private int index;

	private string cutscene;

	private BadelineDummy badeline;

	private Player player;

	private Vector2 origin;

	private bool spinning;

	private bool dark;

	public CS07_Ascend(int index, string cutscene, bool dark)
	{
		this.index = index;
		this.cutscene = cutscene;
		this.dark = dark;
	}

	public override void OnBegin(Level level)
	{
		Add(new Coroutine(Cutscene()));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__9))]
	private IEnumerator Cutscene()
	{
		while ((player = base.Scene.Tracker.GetEntity<Player>()) == null)
		{
			yield return null;
		}
		origin = player.Position;
		Audio.Play("event:/char/badeline/maddy_split");
		player.CreateSplitParticles();
		Input.Rumble(RumbleStrength.Light, RumbleLength.Medium);
		Level.Displacement.AddBurst(player.Position, 0.4f, 8f, 32f, 0.5f);
		player.Dashes = 1;
		player.Facing = Facings.Right;
		base.Scene.Add(badeline = new BadelineDummy(player.Position));
		badeline.AutoAnimator.Enabled = false;
		spinning = true;
		Add(new Coroutine(SpinCharacters()));
		yield return Textbox.Say(cutscene);
		Audio.Play("event:/char/badeline/maddy_join");
		spinning = false;
		yield return 0.25f;
		badeline.RemoveSelf();
		player.Dashes = 2;
		player.CreateSplitParticles();
		Input.Rumble(RumbleStrength.Light, RumbleLength.Medium);
		Level.Displacement.AddBurst(player.Position, 0.4f, 8f, 32f, 0.5f);
		EndCutscene(Level);
	}

	[IteratorStateMachine(typeof(_003CSpinCharacters_003Ed__10))]
	private IEnumerator SpinCharacters()
	{
		float dist = 0f;
		Vector2 center = player.Position;
		float timer = (float)Math.PI / 2f;
		player.Sprite.Play("spin");
		badeline.Sprite.Play("spin");
		badeline.Sprite.Scale.X = 1f;
		while (spinning || dist > 0f)
		{
			dist = Calc.Approach(dist, spinning ? 1f : 0f, Engine.DeltaTime * 4f);
			int num = (int)(timer / ((float)Math.PI * 2f) * 14f + 10f);
			float num2 = (float)Math.Sin(timer);
			float num3 = (float)Math.Cos(timer);
			float num4 = Ease.CubeOut(dist) * 32f;
			player.Sprite.SetAnimationFrame(num);
			badeline.Sprite.SetAnimationFrame(num + 7);
			player.Position = center - new Vector2(num2 * num4, num3 * dist * 8f);
			badeline.Position = center + new Vector2(num2 * num4, num3 * dist * 8f);
			timer -= Engine.DeltaTime * 2f;
			if (timer <= 0f)
			{
				timer += (float)Math.PI * 2f;
			}
			yield return null;
		}
	}

	public override void OnEnd(Level level)
	{
		if (badeline != null)
		{
			badeline.RemoveSelf();
		}
		if (player != null)
		{
			player.Dashes = 2;
			player.Position = origin;
		}
		if (!dark)
		{
			level.Add(new HeightDisplay(index));
		}
	}
}
