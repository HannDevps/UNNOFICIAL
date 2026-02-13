using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class SwitchGate : Solid
{
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass16_0
	{
		public SwitchGate _003C_003E4__this;

		public Vector2 start;

		public Vector2 node;
	}

	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass16_1
	{
		public int particleAt;

		public _003C_003Ec__DisplayClass16_0 CS_0024_003C_003E8__locals1;

		internal void _003CSequence_003Eb__0(Tween t)
		{
			CS_0024_003C_003E8__locals1._003C_003E4__this.MoveTo(Vector2.Lerp(CS_0024_003C_003E8__locals1.start, CS_0024_003C_003E8__locals1.node, t.Eased));
			if (!CS_0024_003C_003E8__locals1._003C_003E4__this.Scene.OnInterval(0.1f))
			{
				return;
			}
			particleAt++;
			particleAt %= 2;
			for (int i = 0; (float)i < CS_0024_003C_003E8__locals1._003C_003E4__this.Width / 8f; i++)
			{
				for (int j = 0; (float)j < CS_0024_003C_003E8__locals1._003C_003E4__this.Height / 8f; j++)
				{
					if ((i + j) % 2 == particleAt)
					{
						CS_0024_003C_003E8__locals1._003C_003E4__this.SceneAs<Level>().ParticlesBG.Emit(P_Behind, CS_0024_003C_003E8__locals1._003C_003E4__this.Position + new Vector2(i * 8, j * 8) + Calc.Random.Range(Vector2.One * 2f, Vector2.One * 6f));
					}
				}
			}
		}
	}

	[CompilerGenerated]
	private sealed class _003CSequence_003Ed__16 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public SwitchGate _003C_003E4__this;

		public Vector2 node;

		private _003C_003Ec__DisplayClass16_0 _003C_003E8__1;

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
		public _003CSequence_003Ed__16(int _003C_003E1__state)
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
			SwitchGate switchGate = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E8__1 = new _003C_003Ec__DisplayClass16_0();
				_003C_003E8__1._003C_003E4__this = _003C_003E4__this;
				_003C_003E8__1.node = node;
				_003C_003E8__1.start = switchGate.Position;
				goto IL_0090;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0090;
			case 2:
				_003C_003E1__state = -1;
				switchGate.openSfx.Play("event:/game/general/touchswitch_gate_open");
				switchGate.StartShaking(0.5f);
				goto IL_014f;
			case 3:
				_003C_003E1__state = -1;
				goto IL_014f;
			case 4:
			{
				_003C_003E1__state = -1;
				_003C_003Ec__DisplayClass16_1 CS_0024_003C_003E8__locals11 = new _003C_003Ec__DisplayClass16_1
				{
					CS_0024_003C_003E8__locals1 = _003C_003E8__1,
					particleAt = 0
				};
				Tween tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.CubeOut, 2f, start: true);
				tween.OnUpdate = delegate(Tween t)
				{
					CS_0024_003C_003E8__locals11.CS_0024_003C_003E8__locals1._003C_003E4__this.MoveTo(Vector2.Lerp(CS_0024_003C_003E8__locals11.CS_0024_003C_003E8__locals1.start, CS_0024_003C_003E8__locals11.CS_0024_003C_003E8__locals1.node, t.Eased));
					if (CS_0024_003C_003E8__locals11.CS_0024_003C_003E8__locals1._003C_003E4__this.Scene.OnInterval(0.1f))
					{
						CS_0024_003C_003E8__locals11.particleAt++;
						CS_0024_003C_003E8__locals11.particleAt %= 2;
						for (int m = 0; (float)m < CS_0024_003C_003E8__locals11.CS_0024_003C_003E8__locals1._003C_003E4__this.Width / 8f; m++)
						{
							for (int n = 0; (float)n < CS_0024_003C_003E8__locals11.CS_0024_003C_003E8__locals1._003C_003E4__this.Height / 8f; n++)
							{
								if ((m + n) % 2 == CS_0024_003C_003E8__locals11.particleAt)
								{
									CS_0024_003C_003E8__locals11.CS_0024_003C_003E8__locals1._003C_003E4__this.SceneAs<Level>().ParticlesBG.Emit(P_Behind, CS_0024_003C_003E8__locals11.CS_0024_003C_003E8__locals1._003C_003E4__this.Position + new Vector2(m * 8, n * 8) + Calc.Random.Range(Vector2.One * 2f, Vector2.One * 6f));
								}
							}
						}
					}
				};
				switchGate.Add(tween);
				_003C_003E2__current = 1.8f;
				_003C_003E1__state = 5;
				return true;
			}
			case 5:
			{
				_003C_003E1__state = -1;
				bool collidable = switchGate.Collidable;
				switchGate.Collidable = false;
				if (_003C_003E8__1.node.X <= _003C_003E8__1.start.X)
				{
					Vector2 vector = new Vector2(0f, 2f);
					for (int i = 0; (float)i < switchGate.Height / 8f; i++)
					{
						Vector2 vector2 = new Vector2(switchGate.Left - 1f, switchGate.Top + 4f + (float)(i * 8));
						Vector2 point = vector2 + Vector2.UnitX;
						if (switchGate.Scene.CollideCheck<Solid>(vector2) && !switchGate.Scene.CollideCheck<Solid>(point))
						{
							switchGate.SceneAs<Level>().ParticlesFG.Emit(P_Dust, vector2 + vector, (float)Math.PI);
							switchGate.SceneAs<Level>().ParticlesFG.Emit(P_Dust, vector2 - vector, (float)Math.PI);
						}
					}
				}
				if (_003C_003E8__1.node.X >= _003C_003E8__1.start.X)
				{
					Vector2 vector3 = new Vector2(0f, 2f);
					for (int j = 0; (float)j < switchGate.Height / 8f; j++)
					{
						Vector2 vector4 = new Vector2(switchGate.Right + 1f, switchGate.Top + 4f + (float)(j * 8));
						Vector2 point2 = vector4 - Vector2.UnitX * 2f;
						if (switchGate.Scene.CollideCheck<Solid>(vector4) && !switchGate.Scene.CollideCheck<Solid>(point2))
						{
							switchGate.SceneAs<Level>().ParticlesFG.Emit(P_Dust, vector4 + vector3, 0f);
							switchGate.SceneAs<Level>().ParticlesFG.Emit(P_Dust, vector4 - vector3, 0f);
						}
					}
				}
				if (_003C_003E8__1.node.Y <= _003C_003E8__1.start.Y)
				{
					Vector2 vector5 = new Vector2(2f, 0f);
					for (int k = 0; (float)k < switchGate.Width / 8f; k++)
					{
						Vector2 vector6 = new Vector2(switchGate.Left + 4f + (float)(k * 8), switchGate.Top - 1f);
						Vector2 point3 = vector6 + Vector2.UnitY;
						if (switchGate.Scene.CollideCheck<Solid>(vector6) && !switchGate.Scene.CollideCheck<Solid>(point3))
						{
							switchGate.SceneAs<Level>().ParticlesFG.Emit(P_Dust, vector6 + vector5, -(float)Math.PI / 2f);
							switchGate.SceneAs<Level>().ParticlesFG.Emit(P_Dust, vector6 - vector5, -(float)Math.PI / 2f);
						}
					}
				}
				if (_003C_003E8__1.node.Y >= _003C_003E8__1.start.Y)
				{
					Vector2 vector7 = new Vector2(2f, 0f);
					for (int l = 0; (float)l < switchGate.Width / 8f; l++)
					{
						Vector2 vector8 = new Vector2(switchGate.Left + 4f + (float)(l * 8), switchGate.Bottom + 1f);
						Vector2 point4 = vector8 - Vector2.UnitY * 2f;
						if (switchGate.Scene.CollideCheck<Solid>(vector8) && !switchGate.Scene.CollideCheck<Solid>(point4))
						{
							switchGate.SceneAs<Level>().ParticlesFG.Emit(P_Dust, vector8 + vector7, (float)Math.PI / 2f);
							switchGate.SceneAs<Level>().ParticlesFG.Emit(P_Dust, vector8 - vector7, (float)Math.PI / 2f);
						}
					}
				}
				switchGate.Collidable = collidable;
				Audio.Play("event:/game/general/touchswitch_gate_finish", switchGate.Position);
				switchGate.StartShaking(0.2f);
				break;
			}
			case 6:
				{
					_003C_003E1__state = -1;
					break;
				}
				IL_014f:
				if (switchGate.icon.Rate < 1f)
				{
					switchGate.icon.Color = Color.Lerp(switchGate.inactiveColor, switchGate.activeColor, switchGate.icon.Rate);
					switchGate.icon.Rate += Engine.DeltaTime * 2f;
					_003C_003E2__current = null;
					_003C_003E1__state = 3;
					return true;
				}
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 4;
				return true;
				IL_0090:
				if (!Switch.Check(switchGate.Scene))
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				if (switchGate.persistent)
				{
					Switch.SetLevelFlag(switchGate.SceneAs<Level>());
				}
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 2;
				return true;
			}
			if (switchGate.icon.Rate > 0f)
			{
				switchGate.icon.Color = Color.Lerp(switchGate.activeColor, switchGate.finishColor, 1f - switchGate.icon.Rate);
				switchGate.icon.Rate -= Engine.DeltaTime * 4f;
				_003C_003E2__current = null;
				_003C_003E1__state = 6;
				return true;
			}
			switchGate.icon.Rate = 0f;
			switchGate.icon.SetAnimationFrame(0);
			switchGate.wiggler.Start();
			bool collidable2 = switchGate.Collidable;
			switchGate.Collidable = false;
			if (!switchGate.Scene.CollideCheck<Solid>(switchGate.Center))
			{
				for (int num2 = 0; num2 < 32; num2++)
				{
					float num3 = Calc.Random.NextFloat((float)Math.PI * 2f);
					switchGate.SceneAs<Level>().ParticlesFG.Emit(TouchSwitch.P_Fire, switchGate.Position + switchGate.iconOffset + Calc.AngleToVector(num3, 4f), num3);
				}
			}
			switchGate.Collidable = collidable2;
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

	public static ParticleType P_Behind;

	public static ParticleType P_Dust;

	private MTexture[,] nineSlice;

	private Sprite icon;

	private Vector2 iconOffset;

	private Wiggler wiggler;

	private Vector2 node;

	private SoundSource openSfx;

	private bool persistent;

	private Color inactiveColor = Calc.HexToColor("5fcde4");

	private Color activeColor = Color.White;

	private Color finishColor = Calc.HexToColor("f141df");

	public SwitchGate(Vector2 position, float width, float height, Vector2 node, bool persistent, string spriteName)
		: base(position, width, height, safe: false)
	{
		this.node = node;
		this.persistent = persistent;
		Add(icon = new Sprite(GFX.Game, "objects/switchgate/icon"));
		icon.Add("spin", "", 0.1f, "spin");
		icon.Play("spin");
		icon.Rate = 0f;
		icon.Color = inactiveColor;
		icon.Position = (iconOffset = new Vector2(width / 2f, height / 2f));
		icon.CenterOrigin();
		Add(wiggler = Wiggler.Create(0.5f, 4f, delegate(float f)
		{
			icon.Scale = Vector2.One * (1f + f);
		}));
		MTexture mTexture = GFX.Game["objects/switchgate/" + spriteName];
		nineSlice = new MTexture[3, 3];
		for (int num = 0; num < 3; num++)
		{
			for (int num2 = 0; num2 < 3; num2++)
			{
				nineSlice[num, num2] = mTexture.GetSubtexture(new Rectangle(num * 8, num2 * 8, 8, 8));
			}
		}
		Add(openSfx = new SoundSource());
		Add(new LightOcclude(0.5f));
	}

	public SwitchGate(EntityData data, Vector2 offset)
		: this(data.Position + offset, data.Width, data.Height, data.Nodes[0] + offset, data.Bool("persistent"), data.Attr("sprite", "block"))
	{
	}

	public override void Awake(Scene scene)
	{
		base.Awake(scene);
		if (Switch.CheckLevelFlag(SceneAs<Level>()))
		{
			MoveTo(node);
			icon.Rate = 0f;
			icon.SetAnimationFrame(0);
			icon.Color = finishColor;
		}
		else
		{
			Add(new Coroutine(Sequence(node)));
		}
	}

	public override void Render()
	{
		float num = base.Collider.Width / 8f - 1f;
		float num2 = base.Collider.Height / 8f - 1f;
		for (int i = 0; (float)i <= num; i++)
		{
			for (int j = 0; (float)j <= num2; j++)
			{
				int num3 = (((float)i < num) ? Math.Min(i, 1) : 2);
				int num4 = (((float)j < num2) ? Math.Min(j, 1) : 2);
				nineSlice[num3, num4].Draw(Position + base.Shake + new Vector2(i * 8, j * 8));
			}
		}
		icon.Position = iconOffset + base.Shake;
		icon.DrawOutline();
		base.Render();
	}

	[IteratorStateMachine(typeof(_003CSequence_003Ed__16))]
	private IEnumerator Sequence(Vector2 node)
	{
		Vector2 start = Position;
		while (!Switch.Check(base.Scene))
		{
			yield return null;
		}
		if (persistent)
		{
			Switch.SetLevelFlag(SceneAs<Level>());
		}
		yield return 0.1f;
		openSfx.Play("event:/game/general/touchswitch_gate_open");
		StartShaking(0.5f);
		while (icon.Rate < 1f)
		{
			icon.Color = Color.Lerp(inactiveColor, activeColor, icon.Rate);
			icon.Rate += Engine.DeltaTime * 2f;
			yield return null;
		}
		yield return 0.1f;
		int particleAt = 0;
		Tween tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.CubeOut, 2f, start: true);
		tween.OnUpdate = delegate(Tween t)
		{
			MoveTo(Vector2.Lerp(start, node, t.Eased));
			if (base.Scene.OnInterval(0.1f))
			{
				particleAt++;
				particleAt %= 2;
				for (int i = 0; (float)i < base.Width / 8f; i++)
				{
					for (int j = 0; (float)j < base.Height / 8f; j++)
					{
						if ((i + j) % 2 == particleAt)
						{
							SceneAs<Level>().ParticlesBG.Emit(P_Behind, Position + new Vector2(i * 8, j * 8) + Calc.Random.Range(Vector2.One * 2f, Vector2.One * 6f));
						}
					}
				}
			}
		};
		Add(tween);
		yield return 1.8f;
		bool collidable = Collidable;
		Collidable = false;
		if (node.X <= start.X)
		{
			Vector2 vector = new Vector2(0f, 2f);
			for (int num = 0; (float)num < base.Height / 8f; num++)
			{
				Vector2 vector2 = new Vector2(base.Left - 1f, base.Top + 4f + (float)(num * 8));
				Vector2 point = vector2 + Vector2.UnitX;
				if (base.Scene.CollideCheck<Solid>(vector2) && !base.Scene.CollideCheck<Solid>(point))
				{
					SceneAs<Level>().ParticlesFG.Emit(P_Dust, vector2 + vector, (float)Math.PI);
					SceneAs<Level>().ParticlesFG.Emit(P_Dust, vector2 - vector, (float)Math.PI);
				}
			}
		}
		if (node.X >= start.X)
		{
			Vector2 vector3 = new Vector2(0f, 2f);
			for (int num2 = 0; (float)num2 < base.Height / 8f; num2++)
			{
				Vector2 vector4 = new Vector2(base.Right + 1f, base.Top + 4f + (float)(num2 * 8));
				Vector2 point2 = vector4 - Vector2.UnitX * 2f;
				if (base.Scene.CollideCheck<Solid>(vector4) && !base.Scene.CollideCheck<Solid>(point2))
				{
					SceneAs<Level>().ParticlesFG.Emit(P_Dust, vector4 + vector3, 0f);
					SceneAs<Level>().ParticlesFG.Emit(P_Dust, vector4 - vector3, 0f);
				}
			}
		}
		if (node.Y <= start.Y)
		{
			Vector2 vector5 = new Vector2(2f, 0f);
			for (int num3 = 0; (float)num3 < base.Width / 8f; num3++)
			{
				Vector2 vector6 = new Vector2(base.Left + 4f + (float)(num3 * 8), base.Top - 1f);
				Vector2 point3 = vector6 + Vector2.UnitY;
				if (base.Scene.CollideCheck<Solid>(vector6) && !base.Scene.CollideCheck<Solid>(point3))
				{
					SceneAs<Level>().ParticlesFG.Emit(P_Dust, vector6 + vector5, -(float)Math.PI / 2f);
					SceneAs<Level>().ParticlesFG.Emit(P_Dust, vector6 - vector5, -(float)Math.PI / 2f);
				}
			}
		}
		if (node.Y >= start.Y)
		{
			Vector2 vector7 = new Vector2(2f, 0f);
			for (int num4 = 0; (float)num4 < base.Width / 8f; num4++)
			{
				Vector2 vector8 = new Vector2(base.Left + 4f + (float)(num4 * 8), base.Bottom + 1f);
				Vector2 point4 = vector8 - Vector2.UnitY * 2f;
				if (base.Scene.CollideCheck<Solid>(vector8) && !base.Scene.CollideCheck<Solid>(point4))
				{
					SceneAs<Level>().ParticlesFG.Emit(P_Dust, vector8 + vector7, (float)Math.PI / 2f);
					SceneAs<Level>().ParticlesFG.Emit(P_Dust, vector8 - vector7, (float)Math.PI / 2f);
				}
			}
		}
		Collidable = collidable;
		Audio.Play("event:/game/general/touchswitch_gate_finish", Position);
		StartShaking(0.2f);
		while (icon.Rate > 0f)
		{
			icon.Color = Color.Lerp(activeColor, finishColor, 1f - icon.Rate);
			icon.Rate -= Engine.DeltaTime * 4f;
			yield return null;
		}
		icon.Rate = 0f;
		icon.SetAnimationFrame(0);
		wiggler.Start();
		bool collidable2 = Collidable;
		Collidable = false;
		if (!base.Scene.CollideCheck<Solid>(base.Center))
		{
			for (int num5 = 0; num5 < 32; num5++)
			{
				float num6 = Calc.Random.NextFloat((float)Math.PI * 2f);
				SceneAs<Level>().ParticlesFG.Emit(TouchSwitch.P_Fire, Position + iconOffset + Calc.AngleToVector(num6, 4f), num6);
			}
		}
		Collidable = collidable2;
	}
}
