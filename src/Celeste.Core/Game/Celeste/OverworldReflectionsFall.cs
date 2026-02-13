using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class OverworldReflectionsFall : Scene
{
	[CompilerGenerated]
	private sealed class _003CRoutine_003Ed__7 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public OverworldReflectionsFall _003C_003E4__this;

		private float _003Cduration_003E5__2;

		private int _003Ci_003E5__3;

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
		public _003CRoutine_003Ed__7(int _003C_003E1__state)
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
			OverworldReflectionsFall overworldReflectionsFall = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				overworldReflectionsFall.mountain.EaseCamera(-1, overworldReflectionsFall.startCamera, 0.4f);
				_003Cduration_003E5__2 = 4f;
				overworldReflectionsFall.maddy.Position = overworldReflectionsFall.startCamera.Target;
				_003Ci_003E5__3 = 0;
				goto IL_010d;
			case 1:
				_003C_003E1__state = -1;
				_003Ci_003E5__3++;
				goto IL_010d;
			case 2:
				_003C_003E1__state = -1;
				overworldReflectionsFall.maddy.Add(new Coroutine(overworldReflectionsFall.MaddyFall(_003Cduration_003E5__2 + 0.1f)));
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				overworldReflectionsFall.mountain.EaseCamera(-1, overworldReflectionsFall.fallCamera, _003Cduration_003E5__2);
				overworldReflectionsFall.mountain.ForceNearFog = true;
				_003C_003E2__current = _003Cduration_003E5__2;
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 5;
				return true;
			case 5:
				{
					_003C_003E1__state = -1;
					MountainCamera transform = new MountainCamera(overworldReflectionsFall.fallCamera.Position + overworldReflectionsFall.mountain.Model.Forward * 3f, overworldReflectionsFall.fallCamera.Target);
					overworldReflectionsFall.mountain.EaseCamera(-1, transform, 0.5f);
					overworldReflectionsFall.Return();
					return false;
				}
				IL_010d:
				if (_003Ci_003E5__3 < 30)
				{
					overworldReflectionsFall.maddy.Position = overworldReflectionsFall.startCamera.Target + new Vector3(Calc.Random.Range(-0.05f, 0.05f), Calc.Random.Range(-0.05f, 0.05f), Calc.Random.Range(-0.05f, 0.05f));
					_003C_003E2__current = 0.01f;
					_003C_003E1__state = 1;
					return true;
				}
				_003C_003E2__current = 0.1f;
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
	private sealed class _003CMaddyFall_003Ed__8 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public OverworldReflectionsFall _003C_003E4__this;

		public float duration;

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
		public _003CMaddyFall_003Ed__8(int _003C_003E1__state)
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
			OverworldReflectionsFall overworldReflectionsFall = _003C_003E4__this;
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
				_003Cp_003E5__2 += Engine.DeltaTime / duration;
				break;
			}
			if (_003Cp_003E5__2 < 1f)
			{
				overworldReflectionsFall.maddy.Position = Vector3.Lerp(overworldReflectionsFall.startCamera.Target, overworldReflectionsFall.fallCamera.Target, _003Cp_003E5__2);
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

	private Level returnTo;

	private Action returnCallback;

	private Maddy3D maddy;

	private MountainRenderer mountain;

	private MountainCamera startCamera = new MountainCamera(new Vector3(-8f, 12f, -0.4f), new Vector3(-2f, 9f, -0.5f));

	private MountainCamera fallCamera = new MountainCamera(new Vector3(-10f, 6f, -0.4f), new Vector3(-4.25f, 1.5f, -1.25f));

	public OverworldReflectionsFall(Level returnTo, Action returnCallback)
	{
		this.returnTo = returnTo;
		this.returnCallback = returnCallback;
		Add(mountain = new MountainRenderer());
		mountain.SnapCamera(-1, new MountainCamera(startCamera.Position + (startCamera.Target - startCamera.Position).SafeNormalize() * 2f, startCamera.Target));
		Add(new HiresSnow
		{
			ParticleAlpha = 0f
		});
		Add(new Snow3D(mountain.Model));
		Add(maddy = new Maddy3D(mountain));
		maddy.Falling();
		Add(new Entity
		{
			new Coroutine(Routine())
		});
	}

	[IteratorStateMachine(typeof(_003CRoutine_003Ed__7))]
	private IEnumerator Routine()
	{
		mountain.EaseCamera(-1, startCamera, 0.4f);
		float duration = 4f;
		maddy.Position = startCamera.Target;
		for (int i = 0; i < 30; i++)
		{
			maddy.Position = startCamera.Target + new Vector3(Calc.Random.Range(-0.05f, 0.05f), Calc.Random.Range(-0.05f, 0.05f), Calc.Random.Range(-0.05f, 0.05f));
			yield return 0.01f;
		}
		yield return 0.1f;
		maddy.Add(new Coroutine(MaddyFall(duration + 0.1f)));
		yield return 0.1f;
		mountain.EaseCamera(-1, fallCamera, duration);
		mountain.ForceNearFog = true;
		yield return duration;
		yield return 0.25f;
		MountainCamera transform = new MountainCamera(fallCamera.Position + mountain.Model.Forward * 3f, fallCamera.Target);
		mountain.EaseCamera(-1, transform, 0.5f);
		Return();
	}

	[IteratorStateMachine(typeof(_003CMaddyFall_003Ed__8))]
	private IEnumerator MaddyFall(float duration)
	{
		for (float p = 0f; p < 1f; p += Engine.DeltaTime / duration)
		{
			maddy.Position = Vector3.Lerp(startCamera.Target, fallCamera.Target, p);
			yield return null;
		}
	}

	private void Return()
	{
		new FadeWipe(this, wipeIn: false, delegate
		{
			mountain.Dispose();
			if (returnTo != null)
			{
				Engine.Scene = returnTo;
			}
			returnCallback();
		});
	}
}
