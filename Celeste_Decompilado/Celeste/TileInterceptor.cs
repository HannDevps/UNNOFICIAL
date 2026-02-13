using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

[Tracked(false)]
public class TileInterceptor : Component
{
	public Action<MTexture, Vector2, Point> Intercepter;

	public bool HighPriority;

	public TileInterceptor(Action<MTexture, Vector2, Point> intercepter, bool highPriority)
		: base(active: false, visible: false)
	{
		Intercepter = intercepter;
		HighPriority = highPriority;
	}

	public TileInterceptor(TileGrid applyToGrid, bool highPriority)
		: base(active: false, visible: false)
	{
		Intercepter = delegate(MTexture t, Vector2 v, Point p)
		{
			applyToGrid.Tiles[p.X, p.Y] = t;
		};
		HighPriority = highPriority;
	}

	public static bool TileCheck(Scene scene, MTexture tile, Vector2 at)
	{
		at += Vector2.One * 4f;
		TileInterceptor tileInterceptor = null;
		List<Component> components = scene.Tracker.GetComponents<TileInterceptor>();
		for (int num = components.Count - 1; num >= 0; num--)
		{
			TileInterceptor tileInterceptor2 = (TileInterceptor)components[num];
			if ((tileInterceptor == null || tileInterceptor2.HighPriority) && tileInterceptor2.Entity.CollidePoint(at))
			{
				tileInterceptor = tileInterceptor2;
				if (tileInterceptor2.HighPriority)
				{
					break;
				}
			}
		}
		if (tileInterceptor != null)
		{
			Point arg = new Point((int)((at.X - tileInterceptor.Entity.X) / 8f), (int)((at.Y - tileInterceptor.Entity.Y) / 8f));
			tileInterceptor.Intercepter(tile, at, arg);
			return true;
		}
		return false;
	}
}
