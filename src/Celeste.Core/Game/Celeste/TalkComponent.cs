using System;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class TalkComponent : Component
{
	public class HoverDisplay
	{
		public MTexture Texture;

		public Vector2 InputPosition;

		public string SfxIn = "event:/ui/game/hotspot_main_in";

		public string SfxOut = "event:/ui/game/hotspot_main_out";
	}

	public class TalkComponentUI : Entity
	{
		public TalkComponent Handler;

		private bool highlighted;

		private float slide;

		private float timer;

		private Wiggler wiggler;

		private float alpha = 1f;

		private Color lineColor = new Color(1f, 1f, 1f);

		public bool Highlighted
		{
			get
			{
				return highlighted;
			}
			set
			{
				if ((highlighted != value) & Display)
				{
					highlighted = value;
					if (highlighted)
					{
						Audio.Play(Handler.HoverUI.SfxIn);
					}
					else
					{
						Audio.Play(Handler.HoverUI.SfxOut);
					}
					wiggler.Start();
				}
			}
		}

		public bool Display
		{
			get
			{
				if (!Handler.Enabled)
				{
					return false;
				}
				if (base.Scene == null)
				{
					return false;
				}
				if (base.Scene.Tracker.GetEntity<Textbox>() != null)
				{
					return false;
				}
				Player entity = base.Scene.Tracker.GetEntity<Player>();
				if (entity == null || entity.StateMachine.State == 11)
				{
					return false;
				}
				Level level = base.Scene as Level;
				if (!level.FrozenOrPaused)
				{
					return level.RetryPlayerCorpse == null;
				}
				return false;
			}
		}

		public TalkComponentUI(TalkComponent handler)
		{
			Handler = handler;
			AddTag((int)Tags.HUD | (int)Tags.Persistent);
			Add(wiggler = Wiggler.Create(0.25f, 4f));
		}

		public override void Awake(Scene scene)
		{
			base.Awake(scene);
			if (Handler.Entity == null || base.Scene.CollideCheck<FakeWall>(Handler.Entity.Position))
			{
				alpha = 0f;
			}
		}

		public override void Update()
		{
			timer += Engine.DeltaTime;
			slide = Calc.Approach(slide, Display ? 1 : 0, Engine.DeltaTime * 4f);
			if (alpha < 1f && Handler.Entity != null && !base.Scene.CollideCheck<FakeWall>(Handler.Entity.Position))
			{
				alpha = Calc.Approach(alpha, 1f, 2f * Engine.DeltaTime);
			}
			base.Update();
		}

		public override void Render()
		{
			Level level = base.Scene as Level;
			if (level.FrozenOrPaused || !(slide > 0f) || Handler.Entity == null)
			{
				return;
			}
			Vector2 vector = level.Camera.Position.FloorV2();
			Vector2 vector2 = Handler.Entity.Position + Handler.DrawAt - vector;
			if (SaveData.Instance != null && SaveData.Instance.Assists.MirrorMode)
			{
				vector2.X = 320f - vector2.X;
			}
			vector2.X *= 6f;
			vector2.Y *= 6f;
			vector2.Y += (float)Math.Sin(timer * 4f) * 12f + 64f * (1f - Ease.CubeOut(slide));
			float num = ((!Highlighted) ? (1f + wiggler.Value * 0.5f) : (1f - wiggler.Value * 0.5f));
			float num2 = Ease.CubeInOut(slide) * alpha;
			Color color = lineColor * num2;
			if (Highlighted)
			{
				Handler.HoverUI.Texture.DrawJustified(vector2, new Vector2(0.5f, 1f), color * alpha, num);
			}
			else
			{
				GFX.Gui["hover/idle"].DrawJustified(vector2, new Vector2(0.5f, 1f), color * alpha, num);
			}
			if (Highlighted)
			{
				Vector2 position = vector2 + Handler.HoverUI.InputPosition * num;
				if (Input.GuiInputController())
				{
					Input.GuiButton(Input.Talk).DrawJustified(position, new Vector2(0.5f), Color.White * num2, num);
				}
				else
				{
					ActiveFont.DrawOutline(Input.FirstKey(Input.Talk).ToString().ToUpper(), position, new Vector2(0.5f), new Vector2(num), Color.White * num2, 2f, Color.Black);
				}
			}
		}
	}

	public static TalkComponent PlayerOver;

	public bool Enabled = true;

	public Rectangle Bounds;

	public Vector2 DrawAt;

	public Action<Player> OnTalk;

	public bool PlayerMustBeFacing = true;

	public TalkComponentUI UI;

	public HoverDisplay HoverUI;

	private float cooldown;

	private float hoverTimer;

	private float disableDelay;

	public TalkComponent(Rectangle bounds, Vector2 drawAt, Action<Player> onTalk, HoverDisplay hoverDisplay = null)
		: base(active: true, visible: true)
	{
		Bounds = bounds;
		DrawAt = drawAt;
		OnTalk = onTalk;
		if (hoverDisplay == null)
		{
			HoverUI = new HoverDisplay
			{
				Texture = GFX.Gui["hover/highlight"],
				InputPosition = new Vector2(0f, -75f)
			};
		}
		else
		{
			HoverUI = hoverDisplay;
		}
	}

	public override void Update()
	{
		if (UI == null)
		{
			base.Entity.Scene.Add(UI = new TalkComponentUI(this));
		}
		Player entity = base.Scene.Tracker.GetEntity<Player>();
		bool flag = disableDelay < 0.05f && entity != null && entity.CollideRect(new Rectangle((int)(base.Entity.X + (float)Bounds.X), (int)(base.Entity.Y + (float)Bounds.Y), Bounds.Width, Bounds.Height)) && entity.OnGround() && entity.Bottom < base.Entity.Y + (float)Bounds.Bottom + 4f && entity.StateMachine.State == 0 && (!PlayerMustBeFacing || Math.Abs(entity.X - base.Entity.X) <= 16f || entity.Facing == (Facings)Math.Sign(base.Entity.X - entity.X)) && (PlayerOver == null || PlayerOver == this);
		if (flag)
		{
			hoverTimer += Engine.DeltaTime;
		}
		else if (UI.Display)
		{
			hoverTimer = 0f;
		}
		if (PlayerOver == this && !flag)
		{
			PlayerOver = null;
		}
		else if (flag)
		{
			PlayerOver = this;
		}
		if (flag && cooldown <= 0f && entity != null && (int)entity.StateMachine == 0 && Input.Talk.Pressed && Enabled && !base.Scene.Paused)
		{
			cooldown = 0.1f;
			if (OnTalk != null)
			{
				OnTalk(entity);
			}
		}
		if (flag && (int)entity.StateMachine == 0)
		{
			cooldown -= Engine.DeltaTime;
		}
		if (!Enabled)
		{
			disableDelay += Engine.DeltaTime;
		}
		else
		{
			disableDelay = 0f;
		}
		UI.Highlighted = flag && hoverTimer > 0.1f;
		base.Update();
	}

	public override void Removed(Entity entity)
	{
		Dispose();
		base.Removed(entity);
	}

	public override void EntityRemoved(Scene scene)
	{
		Dispose();
		base.EntityRemoved(scene);
	}

	public override void SceneEnd(Scene scene)
	{
		Dispose();
		base.SceneEnd(scene);
	}

	private void Dispose()
	{
		if (PlayerOver == this)
		{
			PlayerOver = null;
		}
		base.Scene.Remove(UI);
		UI = null;
	}

	public override void DebugRender(Camera camera)
	{
		base.DebugRender(camera);
		Draw.HollowRect(base.Entity.X + (float)Bounds.X, base.Entity.Y + (float)Bounds.Y, Bounds.Width, Bounds.Height, Color.Green);
	}
}
