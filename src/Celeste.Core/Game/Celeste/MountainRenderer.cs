using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monocle;

namespace Celeste;

public class MountainRenderer : Renderer
{
	public bool ForceNearFog;

	public Action OnEaseEnd;

	public static readonly Vector3 RotateLookAt = new Vector3(0f, 7f, 0f);

	private const float rotateDistance = 15f;

	private const float rotateYPosition = 3f;

	private bool rotateAroundCenter;

	private bool rotateAroundTarget;

	private float rotateAroundTargetDistance;

	private float rotateTimer = (float)Math.PI / 2f;

	private const float DurationDivisor = 3f;

	public MountainCamera UntiltedCamera;

	public MountainModel Model;

	public bool AllowUserRotation = true;

	private Vector2 userOffset;

	private bool inFreeCameraDebugMode;

	private float percent = 1f;

	private float duration = 1f;

	private MountainCamera easeCameraFrom;

	private MountainCamera easeCameraTo;

	private float easeCameraRotationAngleTo;

	private float timer;

	private float door;

	public MountainCamera Camera => Model.Camera;

	public bool Animating { get; private set; }

	public int Area { get; private set; }

	public MountainRenderer()
	{
		Model = new MountainModel();
		GotoRotationMode();
	}

	public void Dispose()
	{
		Model.Dispose();
	}

	public override void Update(Scene scene)
	{
		timer += Engine.DeltaTime;
		Model.Update();
		Vector2 vector = (AllowUserRotation ? (-Input.MountainAim.Value * 0.8f) : Vector2.Zero);
		userOffset += (vector - userOffset) * (1f - (float)Math.Pow(0.009999999776482582, Engine.DeltaTime));
		if (!rotateAroundCenter)
		{
			if (Area == 8)
			{
				userOffset.Y = Math.Max(0f, userOffset.Y);
			}
			if (Area == 7)
			{
				userOffset.X = Calc.Clamp(userOffset.X, -0.4f, 0.4f);
			}
		}
		if (!inFreeCameraDebugMode)
		{
			if (rotateAroundCenter)
			{
				rotateTimer -= Engine.DeltaTime * 0.1f;
				Vector3 vector2 = new Vector3((float)Math.Cos(rotateTimer) * 15f, 3f, (float)Math.Sin(rotateTimer) * 15f);
				Model.Camera.Position += (vector2 - Model.Camera.Position) * (1f - (float)Math.Pow(0.10000000149011612, Engine.DeltaTime));
				Model.Camera.Target = RotateLookAt + Vector3.Up * userOffset.Y;
				Quaternion quaternion = default(Quaternion).LookAt(Model.Camera.Position, Model.Camera.Target, Vector3.Up);
				Model.Camera.Rotation = Quaternion.Slerp(Model.Camera.Rotation, quaternion, Engine.DeltaTime * 4f);
				UntiltedCamera = Camera;
			}
			else
			{
				if (Animating)
				{
					percent = Calc.Approach(percent, 1f, Engine.DeltaTime / duration);
					float num = Ease.SineOut(percent);
					Model.Camera.Position = GetBetween(easeCameraFrom.Position, easeCameraTo.Position, num);
					Model.Camera.Target = GetBetween(easeCameraFrom.Target, easeCameraTo.Target, num);
					Vector3 vector3 = easeCameraFrom.Rotation.Forward();
					Vector3 vector4 = easeCameraTo.Rotation.Forward();
					Vector2 vector5 = Calc.AngleToVector(length: Calc.LerpClamp(vector3.XZ().Length(), vector4.XZ().Length(), num), angleRadians: MathHelper.Lerp(vector3.XZ().Angle(), easeCameraRotationAngleTo, num));
					float y = Calc.LerpClamp(vector3.Y, vector4.Y, num);
					Model.Camera.Rotation = default(Quaternion).LookAt(new Vector3(vector5.X, y, vector5.Y), Vector3.Up);
					if (percent >= 1f)
					{
						rotateTimer = new Vector2(Model.Camera.Position.X, Model.Camera.Position.Z).Angle();
						Animating = false;
						if (OnEaseEnd != null)
						{
							OnEaseEnd();
						}
					}
				}
				else if (rotateAroundTarget)
				{
					rotateTimer -= Engine.DeltaTime * 0.1f;
					float num2 = (new Vector2(easeCameraTo.Target.X, easeCameraTo.Target.Z) - new Vector2(easeCameraTo.Position.X, easeCameraTo.Position.Z)).Length();
					Vector3 vector6 = new Vector3(easeCameraTo.Target.X + (float)Math.Cos(rotateTimer) * num2, easeCameraTo.Position.Y, easeCameraTo.Target.Z + (float)Math.Sin(rotateTimer) * num2);
					Model.Camera.Position += (vector6 - Model.Camera.Position) * (1f - (float)Math.Pow(0.10000000149011612, Engine.DeltaTime));
					Model.Camera.Target = easeCameraTo.Target + Vector3.Up * userOffset.Y * 2f + Vector3.Left * userOffset.X * 2f;
					Quaternion quaternion2 = default(Quaternion).LookAt(Model.Camera.Position, Model.Camera.Target, Vector3.Up);
					Model.Camera.Rotation = Quaternion.Slerp(Model.Camera.Rotation, quaternion2, Engine.DeltaTime * 4f);
					UntiltedCamera = Camera;
				}
				else
				{
					Model.Camera.Rotation = easeCameraTo.Rotation;
					Model.Camera.Target = easeCameraTo.Target;
				}
				UntiltedCamera = Camera;
				if (userOffset != Vector2.Zero && !rotateAroundTarget)
				{
					Vector3 vector7 = Model.Camera.Rotation.Left() * userOffset.X * 0.25f;
					Vector3 vector8 = Model.Camera.Rotation.Up() * userOffset.Y * 0.25f;
					Vector3 pos = Model.Camera.Position + Model.Camera.Rotation.Forward() + vector7 + vector8;
					Model.Camera.LookAt(pos);
				}
			}
		}
		else
		{
			Vector3 vector9 = Vector3.Transform(Vector3.Forward, Model.Camera.Rotation.Conjugated());
			Model.Camera.Rotation = Model.Camera.Rotation.LookAt(Vector3.Zero, vector9, Vector3.Up);
			Vector3 vector10 = Vector3.Transform(Vector3.Left, Model.Camera.Rotation.Conjugated());
			Vector3 vector11 = new Vector3(0f, 0f, 0f);
			if (MInput.Keyboard.Check(Keys.W))
			{
				vector11 += vector9;
			}
			if (MInput.Keyboard.Check(Keys.S))
			{
				vector11 -= vector9;
			}
			if (MInput.Keyboard.Check(Keys.D))
			{
				vector11 -= vector10;
			}
			if (MInput.Keyboard.Check(Keys.A))
			{
				vector11 += vector10;
			}
			if (MInput.Keyboard.Check(Keys.Q))
			{
				vector11 += Vector3.Up;
			}
			if (MInput.Keyboard.Check(Keys.Z))
			{
				vector11 += Vector3.Down;
			}
			Model.Camera.Position += vector11 * (MInput.Keyboard.Check(Keys.LeftShift) ? 0.5f : 5f) * Engine.DeltaTime;
			if (MInput.Mouse.CheckLeftButton)
			{
				MouseState state = Mouse.GetState();
				int num3 = Engine.Graphics.GraphicsDevice.Viewport.Width / 2;
				int num4 = Engine.Graphics.GraphicsDevice.Viewport.Height / 2;
				int num5 = state.X - num3;
				int num6 = state.Y - num4;
				Model.Camera.Rotation *= Quaternion.CreateFromAxisAngle(Vector3.Up, (float)num5 * 0.1f * Engine.DeltaTime);
				Model.Camera.Rotation *= Quaternion.CreateFromAxisAngle(vector10, (float)(-num6) * 0.1f * Engine.DeltaTime);
				Mouse.SetPosition(num3, num4);
			}
			if (Area >= 0)
			{
				Vector3 target = AreaData.Areas[Area].MountainIdle.Target;
				Vector3 vector12 = vector10 * 0.05f;
				Vector3 vector13 = Vector3.Up * 0.05f;
				Model.DebugPoints.Clear();
				Model.DebugPoints.Add(new VertexPositionColor(target - vector12 + vector13, Color.Red));
				Model.DebugPoints.Add(new VertexPositionColor(target + vector12 + vector13, Color.Red));
				Model.DebugPoints.Add(new VertexPositionColor(target + vector12 - vector13, Color.Red));
				Model.DebugPoints.Add(new VertexPositionColor(target - vector12 + vector13, Color.Red));
				Model.DebugPoints.Add(new VertexPositionColor(target + vector12 - vector13, Color.Red));
				Model.DebugPoints.Add(new VertexPositionColor(target - vector12 - vector13, Color.Red));
				Model.DebugPoints.Add(new VertexPositionColor(target - vector12 * 0.25f - vector13, Color.Red));
				Model.DebugPoints.Add(new VertexPositionColor(target + vector12 * 0.25f - vector13, Color.Red));
				Model.DebugPoints.Add(new VertexPositionColor(target + vector12 * 0.25f + Vector3.Down * 100f, Color.Red));
				Model.DebugPoints.Add(new VertexPositionColor(target - vector12 * 0.25f - vector13, Color.Red));
				Model.DebugPoints.Add(new VertexPositionColor(target + vector12 * 0.25f + Vector3.Down * 100f, Color.Red));
				Model.DebugPoints.Add(new VertexPositionColor(target - vector12 * 0.25f + Vector3.Down * 100f, Color.Red));
			}
		}
		door = Calc.Approach(door, (Area == 9 && !rotateAroundCenter) ? 1 : 0, Engine.DeltaTime * 1f);
		Model.CoreWallPosition = Vector3.Lerp(Vector3.Zero, -new Vector3(-1.5f, 1.5f, 1f), Ease.CubeInOut(door));
		Model.NearFogAlpha = Calc.Approach(Model.NearFogAlpha, (ForceNearFog || rotateAroundCenter) ? 1 : 0, (float)(rotateAroundCenter ? 1 : 4) * Engine.DeltaTime);
		if (Celeste.PlayMode == Celeste.PlayModes.Debug)
		{
			if (MInput.Keyboard.Pressed(Keys.P))
			{
				Console.WriteLine(GetCameraString());
			}
			if (MInput.Keyboard.Pressed(Keys.F2))
			{
				Engine.Scene = new OverworldLoader(Overworld.StartMode.ReturnFromOptions);
			}
			if (MInput.Keyboard.Pressed(Keys.Space))
			{
				inFreeCameraDebugMode = !inFreeCameraDebugMode;
			}
			Model.DrawDebugPoints = inFreeCameraDebugMode;
			if (MInput.Keyboard.Pressed(Keys.F1))
			{
				AreaData.ReloadMountainViews();
			}
		}
	}

	private Vector3 GetBetween(Vector3 from, Vector3 to, float ease)
	{
		Vector2 vector = new Vector2(from.X, from.Z);
		Vector2 vector2 = new Vector2(to.X, to.Z);
		float startAngle = Calc.Angle(vector, Vector2.Zero);
		float endAngle = Calc.Angle(vector2, Vector2.Zero);
		float angleRadians = Calc.AngleLerp(startAngle, endAngle, ease);
		float num = vector.Length();
		float num2 = vector2.Length();
		float length = num + (num2 - num) * ease;
		float y = from.Y + (to.Y - from.Y) * ease;
		Vector2 vector3 = -Calc.AngleToVector(angleRadians, length);
		return new Vector3(vector3.X, y, vector3.Y);
	}

	public override void BeforeRender(Scene scene)
	{
		Model.BeforeRender(scene);
	}

	public override void Render(Scene scene)
	{
		Model.Render();
		Draw.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.LinearClamp, null, null, null, Engine.ScreenMatrix);
		OVR.Atlas["vignette"].Draw(Vector2.Zero, Vector2.Zero, Color.White * 0.2f);
		Draw.SpriteBatch.End();
		if (inFreeCameraDebugMode)
		{
			Draw.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.LinearClamp, null, null, null, Engine.ScreenMatrix);
			ActiveFont.DrawOutline(GetCameraString(), new Vector2(8f, 8f), Vector2.Zero, Vector2.One * 0.75f, Color.White, 2f, Color.Black);
			Draw.SpriteBatch.End();
		}
	}

	public void SnapCamera(int area, MountainCamera transform, bool targetRotate = false)
	{
		Area = area;
		Animating = false;
		rotateAroundCenter = false;
		rotateAroundTarget = targetRotate;
		Model.Camera = transform;
		percent = 1f;
	}

	public void SnapState(int state)
	{
		Model.SnapState(state);
	}

	public float EaseCamera(int area, MountainCamera transform, float? duration = null, bool nearTarget = true, bool targetRotate = false)
	{
		if (Area != area && area >= 0)
		{
			PlayWhoosh(Area, area);
		}
		Area = area;
		percent = 0f;
		Animating = true;
		rotateAroundCenter = false;
		rotateAroundTarget = targetRotate;
		userOffset = Vector2.Zero;
		easeCameraFrom = Model.Camera;
		if (nearTarget)
		{
			easeCameraFrom.Target = easeCameraFrom.Position + (easeCameraFrom.Target - easeCameraFrom.Position).SafeNormalize() * 0.5f;
		}
		easeCameraTo = transform;
		float num = easeCameraFrom.Rotation.Forward().XZ().Angle();
		float radiansB = easeCameraTo.Rotation.Forward().XZ().Angle();
		float num2 = Calc.AngleDiff(num, radiansB);
		float num3 = (float)(-Math.Sign(num2)) * ((float)Math.PI * 2f - Math.Abs(num2));
		Vector3 between = GetBetween(easeCameraFrom.Position, easeCameraTo.Position, 0.5f);
		Vector2 vector = Calc.AngleToVector(MathHelper.Lerp(num, num + num2, 0.5f), 1f);
		Vector2 vector2 = Calc.AngleToVector(MathHelper.Lerp(num, num + num3, 0.5f), 1f);
		if ((between + new Vector3(vector.X, 0f, vector.Y)).Length() < (between + new Vector3(vector2.X, 0f, vector2.Y)).Length())
		{
			easeCameraRotationAngleTo = num + num2;
		}
		else
		{
			easeCameraRotationAngleTo = num + num3;
		}
		if (!duration.HasValue)
		{
			this.duration = GetDuration(easeCameraFrom, easeCameraTo);
		}
		else
		{
			this.duration = duration.Value;
		}
		return this.duration;
	}

	public void EaseState(int state)
	{
		Model.EaseState(state);
	}

	public void GotoRotationMode()
	{
		if (!rotateAroundCenter)
		{
			rotateAroundCenter = true;
			rotateTimer = new Vector2(Model.Camera.Position.X, Model.Camera.Position.Z).Angle();
			Model.EaseState(0);
		}
	}

	private float GetDuration(MountainCamera from, MountainCamera to)
	{
		float value = Calc.AngleDiff(Calc.Angle(new Vector2(from.Position.X, from.Position.Z), new Vector2(from.Target.X, from.Target.Z)), Calc.Angle(new Vector2(to.Position.X, to.Position.Z), new Vector2(to.Target.X, to.Target.Z)));
		return Calc.Clamp((float)(Math.Max(val2: Math.Sqrt((from.Position - to.Position).Length()) / 3.0, val1: Math.Abs(value) * 0.5f) * 0.699999988079071), 0.3f, 1.1f);
	}

	private void PlayWhoosh(int from, int to)
	{
		string text = "";
		if (from == 0 && to == 1)
		{
			text = "event:/ui/world_map/whoosh/400ms_forward";
		}
		else if (from == 1 && to == 0)
		{
			text = "event:/ui/world_map/whoosh/400ms_back";
		}
		else if (from == 1 && to == 2)
		{
			text = "event:/ui/world_map/whoosh/600ms_forward";
		}
		else if (from == 2 && to == 1)
		{
			text = "event:/ui/world_map/whoosh/600ms_back";
		}
		else if (from < to && from > 1 && from < 7)
		{
			text = "event:/ui/world_map/whoosh/700ms_forward";
		}
		else if (from > to && from > 2 && from < 8)
		{
			text = "event:/ui/world_map/whoosh/700ms_back";
		}
		else if (from == 7 && to == 8)
		{
			text = "event:/ui/world_map/whoosh/1000ms_forward";
		}
		else if (from == 8 && to == 7)
		{
			text = "event:/ui/world_map/whoosh/1000ms_back";
		}
		else if (from == 8 && to == 9)
		{
			text = "event:/ui/world_map/whoosh/600ms_forward";
		}
		else if (from == 9 && to == 8)
		{
			text = "event:/ui/world_map/whoosh/600ms_back";
		}
		else if (from == 9 && to == 10)
		{
			text = "event:/ui/world_map/whoosh/1000ms_forward";
		}
		else if (from == 10 && to == 9)
		{
			text = "event:/ui/world_map/whoosh/1000ms_back";
		}
		if (!string.IsNullOrEmpty(text))
		{
			Audio.Play(text);
		}
	}

	private string GetCameraString()
	{
		Vector3 position = Model.Camera.Position;
		Vector3 vector = position + Vector3.Transform(Vector3.Forward, Model.Camera.Rotation.Conjugated()) * 2f;
		return "position=\"" + position.X.ToString("0.000") + ", " + position.Y.ToString("0.000") + ", " + position.Z.ToString("0.000") + "\" \ntarget=\"" + vector.X.ToString("0.000") + ", " + vector.Y.ToString("0.000") + ", " + vector.Z.ToString("0.000") + "\"";
	}
}
