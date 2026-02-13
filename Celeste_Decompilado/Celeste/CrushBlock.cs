using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CrushBlock : Solid
{
	public enum Axes
	{
		Both,
		Horizontal,
		Vertical
	}

	private struct MoveState
	{
		public Vector2 From;

		public Vector2 Direction;

		public MoveState(Vector2 from, Vector2 direction)
		{
			From = from;
			Direction = direction;
		}
	}

	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass41_0
	{
		public CrushBlock _003C_003E4__this;

		public SoundSource sfx;

		public Action _003C_003E9__1;

		internal void _003CAttackSequence_003Eb__1()
		{
			_003C_003E4__this.face.Play("hurt");
			_003C_003E4__this.currentMoveLoopSfx.Stop();
			_003C_003E4__this.TurnOffImages();
		}

		internal void _003CAttackSequence_003Eb__0()
		{
			sfx.RemoveSelf();
		}
	}

	[CompilerGenerated]
	private sealed class _003CAttackSequence_003Ed__41 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CrushBlock _003C_003E4__this;

		private _003C_003Ec__DisplayClass41_0 _003C_003E8__1;

		private bool _003Cslowing_003E5__2;

		private float _003Cspeed_003E5__3;

		private float _003CwaypointSfxDelay_003E5__4;

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
		public _003CAttackSequence_003Ed__41(int _003C_003E1__state)
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
			CrushBlock crushBlock = _003C_003E4__this;
			bool flag;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E8__1 = new _003C_003Ec__DisplayClass41_0();
				_003C_003E8__1._003C_003E4__this = _003C_003E4__this;
				Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
				crushBlock.StartShaking(0.4f);
				_003C_003E2__current = 0.4f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				if (!crushBlock.chillOut)
				{
					crushBlock.canActivate = true;
				}
				crushBlock.StopPlayerRunIntoAnimation = false;
				_003Cslowing_003E5__2 = false;
				_003Cspeed_003E5__3 = 0f;
				goto IL_00ab;
			case 2:
				_003C_003E1__state = -1;
				goto IL_00ab;
			case 3:
				_003C_003E1__state = -1;
				_003Cspeed_003E5__3 = 0f;
				_003CwaypointSfxDelay_003E5__4 = 0f;
				goto IL_0a27;
			case 4:
			{
				_003C_003E1__state = -1;
				crushBlock.StopPlayerRunIntoAnimation = false;
				MoveState moveState = crushBlock.returnStack[crushBlock.returnStack.Count - 1];
				_003Cspeed_003E5__3 = Calc.Approach(_003Cspeed_003E5__3, 60f, 160f * Engine.DeltaTime);
				_003CwaypointSfxDelay_003E5__4 -= Engine.DeltaTime;
				if (moveState.Direction.X != 0f)
				{
					crushBlock.MoveTowardsX(moveState.From.X, _003Cspeed_003E5__3 * Engine.DeltaTime);
				}
				if (moveState.Direction.Y != 0f)
				{
					crushBlock.MoveTowardsY(moveState.From.Y, _003Cspeed_003E5__3 * Engine.DeltaTime);
				}
				if ((moveState.Direction.X == 0f || crushBlock.ExactPosition.X == moveState.From.X) && (moveState.Direction.Y == 0f || crushBlock.ExactPosition.Y == moveState.From.Y))
				{
					_003Cspeed_003E5__3 = 0f;
					crushBlock.returnStack.RemoveAt(crushBlock.returnStack.Count - 1);
					crushBlock.StopPlayerRunIntoAnimation = true;
					if (crushBlock.returnStack.Count <= 0)
					{
						crushBlock.face.Play("idle");
						crushBlock.returnLoopSfx.Stop();
						if (_003CwaypointSfxDelay_003E5__4 <= 0f)
						{
							Audio.Play("event:/game/06_reflection/crushblock_rest", crushBlock.Center);
						}
					}
					else if (_003CwaypointSfxDelay_003E5__4 <= 0f)
					{
						Audio.Play("event:/game/06_reflection/crushblock_rest_waypoint", crushBlock.Center);
					}
					_003CwaypointSfxDelay_003E5__4 = 0.1f;
					crushBlock.StartShaking(0.2f);
					_003C_003E2__current = 0.2f;
					_003C_003E1__state = 5;
					return true;
				}
				goto IL_0a27;
			}
			case 5:
				{
					_003C_003E1__state = -1;
					goto IL_0a27;
				}
				IL_00ab:
				if (!crushBlock.chillOut)
				{
					_003Cspeed_003E5__3 = Calc.Approach(_003Cspeed_003E5__3, 240f, 500f * Engine.DeltaTime);
				}
				else if (_003Cslowing_003E5__2 || crushBlock.CollideCheck<SolidTiles>(crushBlock.Position + crushBlock.crushDir * 256f))
				{
					_003Cspeed_003E5__3 = Calc.Approach(_003Cspeed_003E5__3, 24f, 500f * Engine.DeltaTime * 0.25f);
					if (!_003Cslowing_003E5__2)
					{
						_003Cslowing_003E5__2 = true;
						Alarm.Set(crushBlock, 0.5f, delegate
						{
							_003C_003E8__1._003C_003E4__this.face.Play("hurt");
							_003C_003E8__1._003C_003E4__this.currentMoveLoopSfx.Stop();
							_003C_003E8__1._003C_003E4__this.TurnOffImages();
						});
					}
				}
				else
				{
					_003Cspeed_003E5__3 = Calc.Approach(_003Cspeed_003E5__3, 240f, 500f * Engine.DeltaTime);
				}
				flag = ((crushBlock.crushDir.X == 0f) ? crushBlock.MoveVCheck(_003Cspeed_003E5__3 * crushBlock.crushDir.Y * Engine.DeltaTime) : crushBlock.MoveHCheck(_003Cspeed_003E5__3 * crushBlock.crushDir.X * Engine.DeltaTime));
				if (crushBlock.Top >= (float)(crushBlock.level.Bounds.Bottom + 32))
				{
					crushBlock.RemoveSelf();
					return false;
				}
				if (flag)
				{
					FallingBlock fallingBlock = crushBlock.CollideFirst<FallingBlock>(crushBlock.Position + crushBlock.crushDir);
					if (fallingBlock != null)
					{
						fallingBlock.Triggered = true;
					}
					if (crushBlock.crushDir == -Vector2.UnitX)
					{
						Vector2 vector = new Vector2(0f, 2f);
						for (int num2 = 0; (float)num2 < crushBlock.Height / 8f; num2++)
						{
							Vector2 vector2 = new Vector2(crushBlock.Left - 1f, crushBlock.Top + 4f + (float)(num2 * 8));
							if (!crushBlock.Scene.CollideCheck<Water>(vector2) && crushBlock.Scene.CollideCheck<Solid>(vector2))
							{
								crushBlock.SceneAs<Level>().ParticlesFG.Emit(P_Impact, vector2 + vector, 0f);
								crushBlock.SceneAs<Level>().ParticlesFG.Emit(P_Impact, vector2 - vector, 0f);
							}
						}
					}
					else if (crushBlock.crushDir == Vector2.UnitX)
					{
						Vector2 vector3 = new Vector2(0f, 2f);
						for (int num3 = 0; (float)num3 < crushBlock.Height / 8f; num3++)
						{
							Vector2 vector4 = new Vector2(crushBlock.Right + 1f, crushBlock.Top + 4f + (float)(num3 * 8));
							if (!crushBlock.Scene.CollideCheck<Water>(vector4) && crushBlock.Scene.CollideCheck<Solid>(vector4))
							{
								crushBlock.SceneAs<Level>().ParticlesFG.Emit(P_Impact, vector4 + vector3, (float)Math.PI);
								crushBlock.SceneAs<Level>().ParticlesFG.Emit(P_Impact, vector4 - vector3, (float)Math.PI);
							}
						}
					}
					else if (crushBlock.crushDir == -Vector2.UnitY)
					{
						Vector2 vector5 = new Vector2(2f, 0f);
						for (int num4 = 0; (float)num4 < crushBlock.Width / 8f; num4++)
						{
							Vector2 vector6 = new Vector2(crushBlock.Left + 4f + (float)(num4 * 8), crushBlock.Top - 1f);
							if (!crushBlock.Scene.CollideCheck<Water>(vector6) && crushBlock.Scene.CollideCheck<Solid>(vector6))
							{
								crushBlock.SceneAs<Level>().ParticlesFG.Emit(P_Impact, vector6 + vector5, (float)Math.PI / 2f);
								crushBlock.SceneAs<Level>().ParticlesFG.Emit(P_Impact, vector6 - vector5, (float)Math.PI / 2f);
							}
						}
					}
					else if (crushBlock.crushDir == Vector2.UnitY)
					{
						Vector2 vector7 = new Vector2(2f, 0f);
						for (int num5 = 0; (float)num5 < crushBlock.Width / 8f; num5++)
						{
							Vector2 vector8 = new Vector2(crushBlock.Left + 4f + (float)(num5 * 8), crushBlock.Bottom + 1f);
							if (!crushBlock.Scene.CollideCheck<Water>(vector8) && crushBlock.Scene.CollideCheck<Solid>(vector8))
							{
								crushBlock.SceneAs<Level>().ParticlesFG.Emit(P_Impact, vector8 + vector7, -(float)Math.PI / 2f);
								crushBlock.SceneAs<Level>().ParticlesFG.Emit(P_Impact, vector8 - vector7, -(float)Math.PI / 2f);
							}
						}
					}
					Audio.Play("event:/game/06_reflection/crushblock_impact", crushBlock.Center);
					crushBlock.level.DirectionalShake(crushBlock.crushDir);
					Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
					crushBlock.StartShaking(0.4f);
					crushBlock.StopPlayerRunIntoAnimation = true;
					_003C_003E8__1.sfx = crushBlock.currentMoveLoopSfx;
					crushBlock.currentMoveLoopSfx.Param("end", 1f);
					crushBlock.currentMoveLoopSfx = null;
					Alarm.Set(crushBlock, 0.5f, delegate
					{
						_003C_003E8__1.sfx.RemoveSelf();
					});
					crushBlock.crushDir = Vector2.Zero;
					crushBlock.TurnOffImages();
					if (!crushBlock.chillOut)
					{
						crushBlock.face.Play("hurt");
						crushBlock.returnLoopSfx.Play("event:/game/06_reflection/crushblock_return_loop");
						_003C_003E2__current = 0.4f;
						_003C_003E1__state = 3;
						return true;
					}
					break;
				}
				if (crushBlock.Scene.OnInterval(0.02f))
				{
					Vector2 position;
					float direction;
					if (crushBlock.crushDir == Vector2.UnitX)
					{
						position = new Vector2(crushBlock.Left + 1f, Calc.Random.Range(crushBlock.Top + 3f, crushBlock.Bottom - 3f));
						direction = (float)Math.PI;
					}
					else if (crushBlock.crushDir == -Vector2.UnitX)
					{
						position = new Vector2(crushBlock.Right - 1f, Calc.Random.Range(crushBlock.Top + 3f, crushBlock.Bottom - 3f));
						direction = 0f;
					}
					else if (crushBlock.crushDir == Vector2.UnitY)
					{
						position = new Vector2(Calc.Random.Range(crushBlock.Left + 3f, crushBlock.Right - 3f), crushBlock.Top + 1f);
						direction = -(float)Math.PI / 2f;
					}
					else
					{
						position = new Vector2(Calc.Random.Range(crushBlock.Left + 3f, crushBlock.Right - 3f), crushBlock.Bottom - 1f);
						direction = (float)Math.PI / 2f;
					}
					crushBlock.level.Particles.Emit(P_Crushing, position, direction);
				}
				_003C_003E2__current = null;
				_003C_003E1__state = 2;
				return true;
				IL_0a27:
				if (crushBlock.returnStack.Count > 0)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 4;
					return true;
				}
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

	public static ParticleType P_Impact;

	public static ParticleType P_Crushing;

	public static ParticleType P_Activate;

	private const float CrushSpeed = 240f;

	private const float CrushAccel = 500f;

	private const float ReturnSpeed = 60f;

	private const float ReturnAccel = 160f;

	private Color fill = Calc.HexToColor("62222b");

	private Level level;

	private bool canActivate;

	private Vector2 crushDir;

	private List<MoveState> returnStack;

	private Coroutine attackCoroutine;

	private bool canMoveVertically;

	private bool canMoveHorizontally;

	private bool chillOut;

	private bool giant;

	private Sprite face;

	private string nextFaceDirection;

	private List<Image> idleImages = new List<Image>();

	private List<Image> activeTopImages = new List<Image>();

	private List<Image> activeRightImages = new List<Image>();

	private List<Image> activeLeftImages = new List<Image>();

	private List<Image> activeBottomImages = new List<Image>();

	private SoundSource currentMoveLoopSfx;

	private SoundSource returnLoopSfx;

	private bool Submerged => base.Scene.CollideCheck<Water>(new Rectangle((int)(base.Center.X - 4f), (int)base.Center.Y, 8, 4));

	public CrushBlock(Vector2 position, float width, float height, Axes axes, bool chillOut = false)
		: base(position, width, height, safe: false)
	{
		OnDashCollide = OnDashed;
		returnStack = new List<MoveState>();
		this.chillOut = chillOut;
		giant = base.Width >= 48f && base.Height >= 48f && chillOut;
		canActivate = true;
		attackCoroutine = new Coroutine();
		attackCoroutine.RemoveOnComplete = false;
		Add(attackCoroutine);
		List<MTexture> atlasSubtextures = GFX.Game.GetAtlasSubtextures("objects/crushblock/block");
		MTexture idle;
		switch (axes)
		{
		default:
			idle = atlasSubtextures[3];
			canMoveHorizontally = (canMoveVertically = true);
			break;
		case Axes.Horizontal:
			idle = atlasSubtextures[1];
			canMoveHorizontally = true;
			canMoveVertically = false;
			break;
		case Axes.Vertical:
			idle = atlasSubtextures[2];
			canMoveHorizontally = false;
			canMoveVertically = true;
			break;
		}
		Add(face = GFX.SpriteBank.Create(giant ? "giant_crushblock_face" : "crushblock_face"));
		face.Position = new Vector2(base.Width, base.Height) / 2f;
		face.Play("idle");
		face.OnLastFrame = delegate(string f)
		{
			if (f == "hit")
			{
				face.Play(nextFaceDirection);
			}
		};
		int num = (int)(base.Width / 8f) - 1;
		int num2 = (int)(base.Height / 8f) - 1;
		AddImage(idle, 0, 0, 0, 0, -1, -1);
		AddImage(idle, num, 0, 3, 0, 1, -1);
		AddImage(idle, 0, num2, 0, 3, -1, 1);
		AddImage(idle, num, num2, 3, 3, 1, 1);
		for (int num3 = 1; num3 < num; num3++)
		{
			AddImage(idle, num3, 0, Calc.Random.Choose(1, 2), 0, 0, -1);
			AddImage(idle, num3, num2, Calc.Random.Choose(1, 2), 3, 0, 1);
		}
		for (int num4 = 1; num4 < num2; num4++)
		{
			AddImage(idle, 0, num4, 0, Calc.Random.Choose(1, 2), -1);
			AddImage(idle, num, num4, 3, Calc.Random.Choose(1, 2), 1);
		}
		Add(new LightOcclude(0.2f));
		Add(returnLoopSfx = new SoundSource());
		Add(new WaterInteraction(() => crushDir != Vector2.Zero));
	}

	public CrushBlock(EntityData data, Vector2 offset)
		: this(data.Position + offset, data.Width, data.Height, data.Enum("axes", Axes.Both), data.Bool("chillout"))
	{
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		level = SceneAs<Level>();
	}

	public override void Update()
	{
		base.Update();
		if (crushDir == Vector2.Zero)
		{
			face.Position = new Vector2(base.Width, base.Height) / 2f;
			if (CollideCheck<Player>(Position + new Vector2(-1f, 0f)))
			{
				face.X -= 1f;
			}
			else if (CollideCheck<Player>(Position + new Vector2(1f, 0f)))
			{
				face.X += 1f;
			}
			else if (CollideCheck<Player>(Position + new Vector2(0f, -1f)))
			{
				face.Y -= 1f;
			}
		}
		if (currentMoveLoopSfx != null)
		{
			currentMoveLoopSfx.Param("submerged", Submerged ? 1 : 0);
		}
		if (returnLoopSfx != null)
		{
			returnLoopSfx.Param("submerged", Submerged ? 1 : 0);
		}
	}

	public override void Render()
	{
		Vector2 position = Position;
		Position += base.Shake;
		Draw.Rect(base.X + 2f, base.Y + 2f, base.Width - 4f, base.Height - 4f, fill);
		base.Render();
		Position = position;
	}

	private void AddImage(MTexture idle, int x, int y, int tx, int ty, int borderX = 0, int borderY = 0)
	{
		MTexture subtexture = idle.GetSubtexture(tx * 8, ty * 8, 8, 8);
		Vector2 vector = new Vector2(x * 8, y * 8);
		if (borderX != 0)
		{
			Image image = new Image(subtexture);
			image.Color = Color.Black;
			image.Position = vector + new Vector2(borderX, 0f);
			Add(image);
		}
		if (borderY != 0)
		{
			Image image2 = new Image(subtexture);
			image2.Color = Color.Black;
			image2.Position = vector + new Vector2(0f, borderY);
			Add(image2);
		}
		Image image3 = new Image(subtexture);
		image3.Position = vector;
		Add(image3);
		idleImages.Add(image3);
		if (borderX != 0 || borderY != 0)
		{
			if (borderX < 0)
			{
				Image image4 = new Image(GFX.Game["objects/crushblock/lit_left"].GetSubtexture(0, ty * 8, 8, 8));
				activeLeftImages.Add(image4);
				image4.Position = vector;
				image4.Visible = false;
				Add(image4);
			}
			else if (borderX > 0)
			{
				Image image5 = new Image(GFX.Game["objects/crushblock/lit_right"].GetSubtexture(0, ty * 8, 8, 8));
				activeRightImages.Add(image5);
				image5.Position = vector;
				image5.Visible = false;
				Add(image5);
			}
			if (borderY < 0)
			{
				Image image6 = new Image(GFX.Game["objects/crushblock/lit_top"].GetSubtexture(tx * 8, 0, 8, 8));
				activeTopImages.Add(image6);
				image6.Position = vector;
				image6.Visible = false;
				Add(image6);
			}
			else if (borderY > 0)
			{
				Image image7 = new Image(GFX.Game["objects/crushblock/lit_bottom"].GetSubtexture(tx * 8, 0, 8, 8));
				activeBottomImages.Add(image7);
				image7.Position = vector;
				image7.Visible = false;
				Add(image7);
			}
		}
	}

	private void TurnOffImages()
	{
		foreach (Image activeLeftImage in activeLeftImages)
		{
			activeLeftImage.Visible = false;
		}
		foreach (Image activeRightImage in activeRightImages)
		{
			activeRightImage.Visible = false;
		}
		foreach (Image activeTopImage in activeTopImages)
		{
			activeTopImage.Visible = false;
		}
		foreach (Image activeBottomImage in activeBottomImages)
		{
			activeBottomImage.Visible = false;
		}
	}

	private DashCollisionResults OnDashed(Player player, Vector2 direction)
	{
		if (CanActivate(-direction))
		{
			Attack(-direction);
			return DashCollisionResults.Rebound;
		}
		return DashCollisionResults.NormalCollision;
	}

	private bool CanActivate(Vector2 direction)
	{
		if (giant && direction.X <= 0f)
		{
			return false;
		}
		if (canActivate && crushDir != direction)
		{
			if (direction.X != 0f && !canMoveHorizontally)
			{
				return false;
			}
			if (direction.Y != 0f && !canMoveVertically)
			{
				return false;
			}
			return true;
		}
		return false;
	}

	private void Attack(Vector2 direction)
	{
		Audio.Play("event:/game/06_reflection/crushblock_activate", base.Center);
		if (currentMoveLoopSfx != null)
		{
			currentMoveLoopSfx.Param("end", 1f);
			SoundSource sfx = currentMoveLoopSfx;
			Alarm.Set(this, 0.5f, delegate
			{
				sfx.RemoveSelf();
			});
		}
		Add(currentMoveLoopSfx = new SoundSource());
		currentMoveLoopSfx.Position = new Vector2(base.Width, base.Height) / 2f;
		if (SaveData.Instance != null && SaveData.Instance.Name != null && SaveData.Instance.Name.StartsWith("FWAHAHA", StringComparison.InvariantCultureIgnoreCase))
		{
			currentMoveLoopSfx.Play("event:/game/06_reflection/crushblock_move_loop_covert");
		}
		else
		{
			currentMoveLoopSfx.Play("event:/game/06_reflection/crushblock_move_loop");
		}
		face.Play("hit");
		crushDir = direction;
		canActivate = false;
		attackCoroutine.Replace(AttackSequence());
		ClearRemainder();
		TurnOffImages();
		ActivateParticles(crushDir);
		if (crushDir.X < 0f)
		{
			foreach (Image activeLeftImage in activeLeftImages)
			{
				activeLeftImage.Visible = true;
			}
			nextFaceDirection = "left";
		}
		else if (crushDir.X > 0f)
		{
			foreach (Image activeRightImage in activeRightImages)
			{
				activeRightImage.Visible = true;
			}
			nextFaceDirection = "right";
		}
		else if (crushDir.Y < 0f)
		{
			foreach (Image activeTopImage in activeTopImages)
			{
				activeTopImage.Visible = true;
			}
			nextFaceDirection = "up";
		}
		else if (crushDir.Y > 0f)
		{
			foreach (Image activeBottomImage in activeBottomImages)
			{
				activeBottomImage.Visible = true;
			}
			nextFaceDirection = "down";
		}
		bool flag = true;
		if (returnStack.Count > 0)
		{
			MoveState moveState = returnStack[returnStack.Count - 1];
			if (moveState.Direction == direction || moveState.Direction == -direction)
			{
				flag = false;
			}
		}
		if (flag)
		{
			returnStack.Add(new MoveState(Position, crushDir));
		}
	}

	private void ActivateParticles(Vector2 dir)
	{
		float direction;
		Vector2 position;
		Vector2 positionRange;
		int num;
		if (dir == Vector2.UnitX)
		{
			direction = 0f;
			position = base.CenterRight - Vector2.UnitX;
			positionRange = Vector2.UnitY * (base.Height - 2f) * 0.5f;
			num = (int)(base.Height / 8f) * 4;
		}
		else if (dir == -Vector2.UnitX)
		{
			direction = (float)Math.PI;
			position = base.CenterLeft + Vector2.UnitX;
			positionRange = Vector2.UnitY * (base.Height - 2f) * 0.5f;
			num = (int)(base.Height / 8f) * 4;
		}
		else if (dir == Vector2.UnitY)
		{
			direction = (float)Math.PI / 2f;
			position = base.BottomCenter - Vector2.UnitY;
			positionRange = Vector2.UnitX * (base.Width - 2f) * 0.5f;
			num = (int)(base.Width / 8f) * 4;
		}
		else
		{
			direction = -(float)Math.PI / 2f;
			position = base.TopCenter + Vector2.UnitY;
			positionRange = Vector2.UnitX * (base.Width - 2f) * 0.5f;
			num = (int)(base.Width / 8f) * 4;
		}
		num += 2;
		level.Particles.Emit(P_Activate, num, position, positionRange, direction);
	}

	[IteratorStateMachine(typeof(_003CAttackSequence_003Ed__41))]
	private IEnumerator AttackSequence()
	{
		Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
		StartShaking(0.4f);
		yield return 0.4f;
		if (!chillOut)
		{
			canActivate = true;
		}
		StopPlayerRunIntoAnimation = false;
		bool slowing = false;
		float speed = 0f;
		while (true)
		{
			if (!chillOut)
			{
				speed = Calc.Approach(speed, 240f, 500f * Engine.DeltaTime);
			}
			else if (slowing || CollideCheck<SolidTiles>(Position + crushDir * 256f))
			{
				speed = Calc.Approach(speed, 24f, 500f * Engine.DeltaTime * 0.25f);
				if (!slowing)
				{
					slowing = true;
					Alarm.Set(this, 0.5f, delegate
					{
						face.Play("hurt");
						currentMoveLoopSfx.Stop();
						TurnOffImages();
					});
				}
			}
			else
			{
				speed = Calc.Approach(speed, 240f, 500f * Engine.DeltaTime);
			}
			bool flag = ((crushDir.X == 0f) ? MoveVCheck(speed * crushDir.Y * Engine.DeltaTime) : MoveHCheck(speed * crushDir.X * Engine.DeltaTime));
			if (base.Top >= (float)(level.Bounds.Bottom + 32))
			{
				RemoveSelf();
				yield break;
			}
			if (flag)
			{
				break;
			}
			if (base.Scene.OnInterval(0.02f))
			{
				Vector2 position;
				float direction;
				if (crushDir == Vector2.UnitX)
				{
					position = new Vector2(base.Left + 1f, Calc.Random.Range(base.Top + 3f, base.Bottom - 3f));
					direction = (float)Math.PI;
				}
				else if (crushDir == -Vector2.UnitX)
				{
					position = new Vector2(base.Right - 1f, Calc.Random.Range(base.Top + 3f, base.Bottom - 3f));
					direction = 0f;
				}
				else if (crushDir == Vector2.UnitY)
				{
					position = new Vector2(Calc.Random.Range(base.Left + 3f, base.Right - 3f), base.Top + 1f);
					direction = -(float)Math.PI / 2f;
				}
				else
				{
					position = new Vector2(Calc.Random.Range(base.Left + 3f, base.Right - 3f), base.Bottom - 1f);
					direction = (float)Math.PI / 2f;
				}
				level.Particles.Emit(P_Crushing, position, direction);
			}
			yield return null;
		}
		FallingBlock fallingBlock = CollideFirst<FallingBlock>(Position + crushDir);
		if (fallingBlock != null)
		{
			fallingBlock.Triggered = true;
		}
		if (crushDir == -Vector2.UnitX)
		{
			Vector2 vector = new Vector2(0f, 2f);
			for (int num = 0; (float)num < base.Height / 8f; num++)
			{
				Vector2 vector2 = new Vector2(base.Left - 1f, base.Top + 4f + (float)(num * 8));
				if (!base.Scene.CollideCheck<Water>(vector2) && base.Scene.CollideCheck<Solid>(vector2))
				{
					SceneAs<Level>().ParticlesFG.Emit(P_Impact, vector2 + vector, 0f);
					SceneAs<Level>().ParticlesFG.Emit(P_Impact, vector2 - vector, 0f);
				}
			}
		}
		else if (crushDir == Vector2.UnitX)
		{
			Vector2 vector3 = new Vector2(0f, 2f);
			for (int num2 = 0; (float)num2 < base.Height / 8f; num2++)
			{
				Vector2 vector4 = new Vector2(base.Right + 1f, base.Top + 4f + (float)(num2 * 8));
				if (!base.Scene.CollideCheck<Water>(vector4) && base.Scene.CollideCheck<Solid>(vector4))
				{
					SceneAs<Level>().ParticlesFG.Emit(P_Impact, vector4 + vector3, (float)Math.PI);
					SceneAs<Level>().ParticlesFG.Emit(P_Impact, vector4 - vector3, (float)Math.PI);
				}
			}
		}
		else if (crushDir == -Vector2.UnitY)
		{
			Vector2 vector5 = new Vector2(2f, 0f);
			for (int num3 = 0; (float)num3 < base.Width / 8f; num3++)
			{
				Vector2 vector6 = new Vector2(base.Left + 4f + (float)(num3 * 8), base.Top - 1f);
				if (!base.Scene.CollideCheck<Water>(vector6) && base.Scene.CollideCheck<Solid>(vector6))
				{
					SceneAs<Level>().ParticlesFG.Emit(P_Impact, vector6 + vector5, (float)Math.PI / 2f);
					SceneAs<Level>().ParticlesFG.Emit(P_Impact, vector6 - vector5, (float)Math.PI / 2f);
				}
			}
		}
		else if (crushDir == Vector2.UnitY)
		{
			Vector2 vector7 = new Vector2(2f, 0f);
			for (int num4 = 0; (float)num4 < base.Width / 8f; num4++)
			{
				Vector2 vector8 = new Vector2(base.Left + 4f + (float)(num4 * 8), base.Bottom + 1f);
				if (!base.Scene.CollideCheck<Water>(vector8) && base.Scene.CollideCheck<Solid>(vector8))
				{
					SceneAs<Level>().ParticlesFG.Emit(P_Impact, vector8 + vector7, -(float)Math.PI / 2f);
					SceneAs<Level>().ParticlesFG.Emit(P_Impact, vector8 - vector7, -(float)Math.PI / 2f);
				}
			}
		}
		Audio.Play("event:/game/06_reflection/crushblock_impact", base.Center);
		level.DirectionalShake(crushDir);
		Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
		StartShaking(0.4f);
		StopPlayerRunIntoAnimation = true;
		SoundSource sfx = currentMoveLoopSfx;
		currentMoveLoopSfx.Param("end", 1f);
		currentMoveLoopSfx = null;
		Alarm.Set(this, 0.5f, delegate
		{
			sfx.RemoveSelf();
		});
		crushDir = Vector2.Zero;
		TurnOffImages();
		if (chillOut)
		{
			yield break;
		}
		face.Play("hurt");
		returnLoopSfx.Play("event:/game/06_reflection/crushblock_return_loop");
		yield return 0.4f;
		speed = 0f;
		float waypointSfxDelay = 0f;
		while (returnStack.Count > 0)
		{
			yield return null;
			StopPlayerRunIntoAnimation = false;
			MoveState moveState = returnStack[returnStack.Count - 1];
			speed = Calc.Approach(speed, 60f, 160f * Engine.DeltaTime);
			waypointSfxDelay -= Engine.DeltaTime;
			if (moveState.Direction.X != 0f)
			{
				MoveTowardsX(moveState.From.X, speed * Engine.DeltaTime);
			}
			if (moveState.Direction.Y != 0f)
			{
				MoveTowardsY(moveState.From.Y, speed * Engine.DeltaTime);
			}
			if ((moveState.Direction.X != 0f && base.ExactPosition.X != moveState.From.X) || (moveState.Direction.Y != 0f && base.ExactPosition.Y != moveState.From.Y))
			{
				continue;
			}
			speed = 0f;
			returnStack.RemoveAt(returnStack.Count - 1);
			StopPlayerRunIntoAnimation = true;
			if (returnStack.Count <= 0)
			{
				face.Play("idle");
				returnLoopSfx.Stop();
				if (waypointSfxDelay <= 0f)
				{
					Audio.Play("event:/game/06_reflection/crushblock_rest", base.Center);
				}
			}
			else if (waypointSfxDelay <= 0f)
			{
				Audio.Play("event:/game/06_reflection/crushblock_rest_waypoint", base.Center);
			}
			waypointSfxDelay = 0.1f;
			StartShaking(0.2f);
			yield return 0.2f;
		}
	}

	private bool MoveHCheck(float amount)
	{
		if (MoveHCollideSolidsAndBounds(level, amount, thruDashBlocks: true))
		{
			if (amount < 0f && base.Left <= (float)level.Bounds.Left)
			{
				return true;
			}
			if (amount > 0f && base.Right >= (float)level.Bounds.Right)
			{
				return true;
			}
			for (int i = 1; i <= 4; i++)
			{
				for (int num = 1; num >= -1; num -= 2)
				{
					Vector2 vector = new Vector2(Math.Sign(amount), i * num);
					if (!CollideCheck<Solid>(Position + vector))
					{
						MoveVExact(i * num);
						MoveHExact(Math.Sign(amount));
						return false;
					}
				}
			}
			return true;
		}
		return false;
	}

	private bool MoveVCheck(float amount)
	{
		if (MoveVCollideSolidsAndBounds(level, amount, thruDashBlocks: true, null, checkBottom: false))
		{
			if (amount < 0f && base.Top <= (float)level.Bounds.Top)
			{
				return true;
			}
			for (int i = 1; i <= 4; i++)
			{
				for (int num = 1; num >= -1; num -= 2)
				{
					Vector2 vector = new Vector2(i * num, Math.Sign(amount));
					if (!CollideCheck<Solid>(Position + vector))
					{
						MoveHExact(i * num);
						MoveVExact(Math.Sign(amount));
						return false;
					}
				}
			}
			return true;
		}
		return false;
	}
}
