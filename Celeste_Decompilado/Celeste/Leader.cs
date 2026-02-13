using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class Leader : Component
{
	public const int MaxPastPoints = 350;

	public List<Follower> Followers = new List<Follower>();

	public List<Vector2> PastPoints = new List<Vector2>();

	public Vector2 Position;

	private static List<Strawberry> storedBerries;

	private static List<Vector2> storedOffsets;

	public Leader()
		: base(active: true, visible: false)
	{
	}

	public Leader(Vector2 position)
		: base(active: true, visible: false)
	{
		Position = position;
	}

	public void GainFollower(Follower follower)
	{
		Followers.Add(follower);
		follower.OnGainLeaderUtil(this);
	}

	public void LoseFollower(Follower follower)
	{
		Followers.Remove(follower);
		follower.OnLoseLeaderUtil();
	}

	public void LoseFollowers()
	{
		foreach (Follower follower in Followers)
		{
			follower.OnLoseLeaderUtil();
		}
		Followers.Clear();
	}

	public override void Update()
	{
		Vector2 vector = base.Entity.Position + Position;
		if (base.Scene.OnInterval(0.02f) && (PastPoints.Count == 0 || (vector - PastPoints[0]).Length() >= 3f))
		{
			PastPoints.Insert(0, vector);
			if (PastPoints.Count > 350)
			{
				PastPoints.RemoveAt(PastPoints.Count - 1);
			}
		}
		int num = 5;
		foreach (Follower follower in Followers)
		{
			if (num >= PastPoints.Count)
			{
				break;
			}
			Vector2 vector2 = PastPoints[num];
			if (follower.DelayTimer <= 0f && follower.MoveTowardsLeader)
			{
				follower.Entity.Position = follower.Entity.Position + (vector2 - follower.Entity.Position) * (1f - (float)Math.Pow(0.009999999776482582, Engine.DeltaTime));
			}
			num += 5;
		}
	}

	public bool HasFollower<T>()
	{
		foreach (Follower follower in Followers)
		{
			if (follower.Entity is T)
			{
				return true;
			}
		}
		return false;
	}

	public void TransferFollowers()
	{
		for (int i = 0; i < Followers.Count; i++)
		{
			Follower follower = Followers[i];
			if (!follower.Entity.TagCheck(Tags.Persistent))
			{
				LoseFollower(follower);
				i--;
			}
		}
	}

	public static void StoreStrawberries(Leader leader)
	{
		storedBerries = new List<Strawberry>();
		storedOffsets = new List<Vector2>();
		foreach (Follower follower in leader.Followers)
		{
			if (follower.Entity is Strawberry)
			{
				storedBerries.Add(follower.Entity as Strawberry);
				storedOffsets.Add(follower.Entity.Position - leader.Entity.Position);
			}
		}
		foreach (Strawberry storedBerry in storedBerries)
		{
			leader.Followers.Remove(storedBerry.Follower);
			storedBerry.Follower.Leader = null;
			storedBerry.AddTag(Tags.Global);
		}
	}

	public static void RestoreStrawberries(Leader leader)
	{
		leader.PastPoints.Clear();
		for (int i = 0; i < storedBerries.Count; i++)
		{
			Strawberry strawberry = storedBerries[i];
			leader.GainFollower(strawberry.Follower);
			strawberry.Position = leader.Entity.Position + storedOffsets[i];
			strawberry.RemoveTag(Tags.Global);
		}
	}
}
