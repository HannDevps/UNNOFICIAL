using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class ReflectionHeartStatue : Entity
{
	public class Torch : Entity
	{
		public string[] Code;

		private Sprite sprite;

		private Session session;

		public string Flag => "heartTorch_" + Index;

		public bool Activated => session.GetFlag(Flag);

		public int Index { get; private set; }

		public Torch(Session session, Vector2 position, int index, string[] code)
			: base(position)
		{
			Index = index;
			Code = code;
			base.Depth = 8999;
			this.session = session;
			Image image = new Image(GFX.Game.GetAtlasSubtextures("objects/reflectionHeart/hint")[index]);
			image.CenterOrigin();
			image.Position = new Vector2(0f, 28f);
			Add(image);
			Add(sprite = new Sprite(GFX.Game, "objects/reflectionHeart/torch"));
			sprite.AddLoop("idle", "", 0f, default(int));
			sprite.AddLoop("lit", "", 0.08f, 1, 2, 3, 4, 5, 6);
			sprite.Play("idle");
			sprite.Origin = new Vector2(32f, 64f);
		}

		public override void Added(Scene scene)
		{
			base.Added(scene);
			if (Activated)
			{
				PlayLit();
			}
		}

		public void Activate()
		{
			session.SetFlag(Flag);
			Alarm.Set(this, 0.2f, delegate
			{
				Audio.Play("event:/game/06_reflection/supersecret_torch_" + (Index + 1), Position);
				PlayLit();
			});
		}

		private void PlayLit()
		{
			sprite.Play("lit");
			sprite.SetAnimationFrame(Calc.Random.Next(sprite.CurrentAnimationTotalFrames));
			Add(new VertexLight(Color.LightSeaGreen, 1f, 24, 48));
			Add(new BloomPoint(0.6f, 16f));
		}
	}

	[CompilerGenerated]
	private sealed class _003CActivateRoutine_003Ed__14 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ReflectionHeartStatue _003C_003E4__this;

		private Entity _003Cdummy_003E5__2;

		private Image _003Cwhite_003E5__3;

		private BloomPoint _003Cglow_003E5__4;

		private List<Entity> _003Cabsorbs_003E5__5;

		private float _003Cduration_003E5__6;

		private int _003Ci_003E5__7;

		private float _003Cp_003E5__8;

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
		public _003CActivateRoutine_003Ed__14(int _003C_003E1__state)
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
			ReflectionHeartStatue reflectionHeartStatue = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.533f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				Audio.Play("event:/game/06_reflection/supersecret_heartappear");
				_003Cdummy_003E5__2 = new Entity(reflectionHeartStatue.Position + new Vector2(0f, -52f));
				_003Cdummy_003E5__2.Depth = 1;
				reflectionHeartStatue.Scene.Add(_003Cdummy_003E5__2);
				_003Cwhite_003E5__3 = new Image(GFX.Game["collectables/heartgem/white00"]);
				_003Cwhite_003E5__3.CenterOrigin();
				_003Cwhite_003E5__3.Scale = Vector2.Zero;
				_003Cdummy_003E5__2.Add(_003Cwhite_003E5__3);
				_003Cglow_003E5__4 = new BloomPoint(0f, 16f);
				_003Cdummy_003E5__2.Add(_003Cglow_003E5__4);
				_003Cabsorbs_003E5__5 = new List<Entity>();
				_003Ci_003E5__7 = 0;
				goto IL_018f;
			case 2:
				_003C_003E1__state = -1;
				_003Ci_003E5__7++;
				goto IL_018f;
			case 3:
				_003C_003E1__state = -1;
				_003Cduration_003E5__6 = 0.6f;
				_003Cp_003E5__8 = 0f;
				break;
			case 4:
				{
					_003C_003E1__state = -1;
					_003Cp_003E5__8 += Engine.DeltaTime / _003Cduration_003E5__6;
					break;
				}
				IL_018f:
				if (_003Ci_003E5__7 < 20)
				{
					AbsorbOrb absorbOrb = new AbsorbOrb(reflectionHeartStatue.Position + new Vector2(0f, -20f), _003Cdummy_003E5__2);
					reflectionHeartStatue.Scene.Add(absorbOrb);
					_003Cabsorbs_003E5__5.Add(absorbOrb);
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				_003C_003E2__current = 0.8f;
				_003C_003E1__state = 3;
				return true;
			}
			if (_003Cp_003E5__8 < 1f)
			{
				_003Cwhite_003E5__3.Scale = Vector2.One * _003Cp_003E5__8;
				_003Cglow_003E5__4.Alpha = _003Cp_003E5__8;
				(reflectionHeartStatue.Scene as Level).Shake();
				_003C_003E2__current = null;
				_003C_003E1__state = 4;
				return true;
			}
			foreach (Entity item in _003Cabsorbs_003E5__5)
			{
				item.RemoveSelf();
			}
			(reflectionHeartStatue.Scene as Level).Flash(Color.White);
			reflectionHeartStatue.Scene.Remove(_003Cdummy_003E5__2);
			reflectionHeartStatue.Scene.Add(new HeartGem(reflectionHeartStatue.Position + new Vector2(0f, -52f)));
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

	private static readonly string[] Code = new string[6] { "U", "L", "DR", "UR", "L", "UL" };

	private const string FlagPrefix = "heartTorch_";

	private List<string> currentInputs = new List<string>();

	private List<Torch> torches = new List<Torch>();

	private Vector2 offset;

	private Vector2[] nodes;

	private DashListener dashListener;

	private bool enabled;

	public ReflectionHeartStatue(EntityData data, Vector2 offset)
		: base(data.Position + offset)
	{
		this.offset = offset;
		nodes = data.Nodes;
		base.Depth = 8999;
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		Session session = (base.Scene as Level).Session;
		Image image = new Image(GFX.Game["objects/reflectionHeart/statue"]);
		image.JustifyOrigin(0.5f, 1f);
		image.Origin.Y -= 1f;
		Add(image);
		List<string[]> list = new List<string[]>();
		list.Add(Code);
		list.Add(FlipCode(h: true, v: false));
		list.Add(FlipCode(h: false, v: true));
		list.Add(FlipCode(h: true, v: true));
		for (int i = 0; i < 4; i++)
		{
			Torch torch = new Torch(session, offset + nodes[i], i, list[i]);
			base.Scene.Add(torch);
			torches.Add(torch);
		}
		int num = Code.Length;
		Vector2 vector = nodes[4] + offset - Position;
		for (int j = 0; j < num; j++)
		{
			Image image2 = new Image(GFX.Game["objects/reflectionHeart/gem"]);
			image2.CenterOrigin();
			image2.Color = ForsakenCitySatellite.Colors[Code[j]];
			image2.Position = vector + new Vector2(((float)j - (float)(num - 1) / 2f) * 24f, 0f);
			Add(image2);
			Add(new BloomPoint(image2.Position, 0.3f, 12f));
		}
		enabled = !session.HeartGem;
		if (!enabled)
		{
			return;
		}
		Add(dashListener = new DashListener());
		dashListener.OnDash = delegate(Vector2 dir)
		{
			string text = "";
			if (dir.Y < 0f)
			{
				text = "U";
			}
			else if (dir.Y > 0f)
			{
				text = "D";
			}
			if (dir.X < 0f)
			{
				text += "L";
			}
			else if (dir.X > 0f)
			{
				text += "R";
			}
			int num2 = 0;
			if (dir.X < 0f && dir.Y == 0f)
			{
				num2 = 1;
			}
			else if (dir.X < 0f && dir.Y < 0f)
			{
				num2 = 2;
			}
			else if (dir.X == 0f && dir.Y < 0f)
			{
				num2 = 3;
			}
			else if (dir.X > 0f && dir.Y < 0f)
			{
				num2 = 4;
			}
			else if (dir.X > 0f && dir.Y == 0f)
			{
				num2 = 5;
			}
			else if (dir.X > 0f && dir.Y > 0f)
			{
				num2 = 6;
			}
			else if (dir.X == 0f && dir.Y > 0f)
			{
				num2 = 7;
			}
			else if (dir.X < 0f && dir.Y > 0f)
			{
				num2 = 8;
			}
			Audio.Play("event:/game/06_reflection/supersecret_dashflavour", base.Scene.Tracker.GetEntity<Player>()?.Position ?? Vector2.Zero, "dash_direction", num2);
			currentInputs.Add(text);
			if (currentInputs.Count > Code.Length)
			{
				currentInputs.RemoveAt(0);
			}
			foreach (Torch torch2 in torches)
			{
				if (!torch2.Activated && CheckCode(torch2.Code))
				{
					torch2.Activate();
				}
			}
			CheckIfAllActivated();
		};
		CheckIfAllActivated(skipActivateRoutine: true);
	}

	private string[] FlipCode(bool h, bool v)
	{
		string[] array = new string[Code.Length];
		for (int i = 0; i < Code.Length; i++)
		{
			string text = Code[i];
			if (h)
			{
				text = (text.Contains('L') ? text.Replace('L', 'R') : text.Replace('R', 'L'));
			}
			if (v)
			{
				text = (text.Contains('U') ? text.Replace('U', 'D') : text.Replace('D', 'U'));
			}
			array[i] = text;
		}
		return array;
	}

	private bool CheckCode(string[] code)
	{
		if (currentInputs.Count < code.Length)
		{
			return false;
		}
		for (int i = 0; i < code.Length; i++)
		{
			if (!currentInputs[i].Equals(code[i]))
			{
				return false;
			}
		}
		return true;
	}

	private void CheckIfAllActivated(bool skipActivateRoutine = false)
	{
		if (!enabled)
		{
			return;
		}
		bool flag = true;
		foreach (Torch torch in torches)
		{
			if (!torch.Activated)
			{
				flag = false;
			}
		}
		if (flag)
		{
			Activate(skipActivateRoutine);
		}
	}

	public void Activate(bool skipActivateRoutine)
	{
		enabled = false;
		if (skipActivateRoutine)
		{
			base.Scene.Add(new HeartGem(Position + new Vector2(0f, -52f)));
		}
		else
		{
			Add(new Coroutine(ActivateRoutine()));
		}
	}

	[IteratorStateMachine(typeof(_003CActivateRoutine_003Ed__14))]
	private IEnumerator ActivateRoutine()
	{
		yield return 0.533f;
		Audio.Play("event:/game/06_reflection/supersecret_heartappear");
		Entity dummy = new Entity(Position + new Vector2(0f, -52f))
		{
			Depth = 1
		};
		base.Scene.Add(dummy);
		Image white = new Image(GFX.Game["collectables/heartgem/white00"]);
		white.CenterOrigin();
		white.Scale = Vector2.Zero;
		dummy.Add(white);
		BloomPoint glow = new BloomPoint(0f, 16f);
		dummy.Add(glow);
		List<Entity> absorbs = new List<Entity>();
		for (int i = 0; i < 20; i++)
		{
			AbsorbOrb absorbOrb = new AbsorbOrb(Position + new Vector2(0f, -20f), dummy);
			base.Scene.Add(absorbOrb);
			absorbs.Add(absorbOrb);
			yield return null;
		}
		yield return 0.8f;
		float duration = 0.6f;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime / duration)
		{
			white.Scale = Vector2.One * p;
			glow.Alpha = p;
			(base.Scene as Level).Shake();
			yield return null;
		}
		foreach (Entity item in absorbs)
		{
			item.RemoveSelf();
		}
		(base.Scene as Level).Flash(Color.White);
		base.Scene.Remove(dummy);
		base.Scene.Add(new HeartGem(Position + new Vector2(0f, -52f)));
	}

	public override void Update()
	{
		if (dashListener != null && !enabled)
		{
			Remove(dashListener);
			dashListener = null;
		}
		base.Update();
	}
}
