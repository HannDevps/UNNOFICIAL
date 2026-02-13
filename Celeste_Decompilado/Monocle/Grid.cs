using System;
using Microsoft.Xna.Framework;

namespace Monocle;

public class Grid : Collider
{
	public VirtualMap<bool> Data;

	public float CellWidth { get; private set; }

	public float CellHeight { get; private set; }

	public bool this[int x, int y]
	{
		get
		{
			if (x >= 0 && y >= 0 && x < CellsX && y < CellsY)
			{
				return Data[x, y];
			}
			return false;
		}
		set
		{
			Data[x, y] = value;
		}
	}

	public int CellsX => Data.Columns;

	public int CellsY => Data.Rows;

	public override float Width
	{
		get
		{
			return CellWidth * (float)CellsX;
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	public override float Height
	{
		get
		{
			return CellHeight * (float)CellsY;
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	public bool IsEmpty
	{
		get
		{
			for (int i = 0; i < CellsX; i++)
			{
				for (int j = 0; j < CellsY; j++)
				{
					if (Data[i, j])
					{
						return false;
					}
				}
			}
			return true;
		}
	}

	public override float Left
	{
		get
		{
			return Position.X;
		}
		set
		{
			Position.X = value;
		}
	}

	public override float Top
	{
		get
		{
			return Position.Y;
		}
		set
		{
			Position.Y = value;
		}
	}

	public override float Right
	{
		get
		{
			return Position.X + Width;
		}
		set
		{
			Position.X = value - Width;
		}
	}

	public override float Bottom
	{
		get
		{
			return Position.Y + Height;
		}
		set
		{
			Position.Y = value - Height;
		}
	}

	public Grid(int cellsX, int cellsY, float cellWidth, float cellHeight)
	{
		Data = new VirtualMap<bool>(cellsX, cellsY, emptyValue: false);
		CellWidth = cellWidth;
		CellHeight = cellHeight;
	}

	public Grid(float cellWidth, float cellHeight, string bitstring)
	{
		CellWidth = cellWidth;
		CellHeight = cellHeight;
		int num = 0;
		int num2 = 0;
		int num3 = 1;
		for (int i = 0; i < bitstring.Length; i++)
		{
			if (bitstring[i] == '\n')
			{
				num3++;
				num = Math.Max(num2, num);
				num2 = 0;
			}
			else
			{
				num2++;
			}
		}
		Data = new VirtualMap<bool>(num, num3, emptyValue: false);
		LoadBitstring(bitstring);
	}

	public Grid(float cellWidth, float cellHeight, bool[,] data)
	{
		CellWidth = cellWidth;
		CellHeight = cellHeight;
		Data = new VirtualMap<bool>(data, emptyValue: false);
	}

	public Grid(float cellWidth, float cellHeight, VirtualMap<bool> data)
	{
		CellWidth = cellWidth;
		CellHeight = cellHeight;
		Data = data;
	}

	public void Extend(int left, int right, int up, int down)
	{
		Position -= new Vector2((float)left * CellWidth, (float)up * CellHeight);
		int num = Data.Columns + left + right;
		int num2 = Data.Rows + up + down;
		if (num <= 0 || num2 <= 0)
		{
			Data = new VirtualMap<bool>(0, 0, emptyValue: false);
			return;
		}
		VirtualMap<bool> virtualMap = new VirtualMap<bool>(num, num2, emptyValue: false);
		for (int i = 0; i < Data.Columns; i++)
		{
			for (int j = 0; j < Data.Rows; j++)
			{
				int num3 = i + left;
				int num4 = j + up;
				if (num3 >= 0 && num3 < num && num4 >= 0 && num4 < num2)
				{
					virtualMap[num3, num4] = Data[i, j];
				}
			}
		}
		for (int k = 0; k < left; k++)
		{
			for (int l = 0; l < num2; l++)
			{
				virtualMap[k, l] = Data[0, Calc.Clamp(l - up, 0, Data.Rows - 1)];
			}
		}
		for (int m = num - right; m < num; m++)
		{
			for (int n = 0; n < num2; n++)
			{
				virtualMap[m, n] = Data[Data.Columns - 1, Calc.Clamp(n - up, 0, Data.Rows - 1)];
			}
		}
		for (int num5 = 0; num5 < up; num5++)
		{
			for (int num6 = 0; num6 < num; num6++)
			{
				virtualMap[num6, num5] = Data[Calc.Clamp(num6 - left, 0, Data.Columns - 1), 0];
			}
		}
		for (int num7 = num2 - down; num7 < num2; num7++)
		{
			for (int num8 = 0; num8 < num; num8++)
			{
				virtualMap[num8, num7] = Data[Calc.Clamp(num8 - left, 0, Data.Columns - 1), Data.Rows - 1];
			}
		}
		Data = virtualMap;
	}

	public void LoadBitstring(string bitstring)
	{
		int i = 0;
		int num = 0;
		for (int j = 0; j < bitstring.Length; j++)
		{
			if (bitstring[j] == '\n')
			{
				for (; i < CellsX; i++)
				{
					Data[i, num] = false;
				}
				i = 0;
				num++;
				if (num >= CellsY)
				{
					break;
				}
			}
			else if (i < CellsX)
			{
				if (bitstring[j] == '0')
				{
					Data[i, num] = false;
					i++;
				}
				else
				{
					Data[i, num] = true;
					i++;
				}
			}
		}
	}

	public string GetBitstring()
	{
		string text = "";
		for (int i = 0; i < CellsY; i++)
		{
			if (i != 0)
			{
				text += "\n";
			}
			for (int j = 0; j < CellsX; j++)
			{
				text = ((!Data[j, i]) ? (text + "0") : (text + "1"));
			}
		}
		return text;
	}

	public void Clear(bool to = false)
	{
		for (int i = 0; i < CellsX; i++)
		{
			for (int j = 0; j < CellsY; j++)
			{
				Data[i, j] = to;
			}
		}
	}

	public void SetRect(int x, int y, int width, int height, bool to = true)
	{
		if (x < 0)
		{
			width += x;
			x = 0;
		}
		if (y < 0)
		{
			height += y;
			y = 0;
		}
		if (x + width > CellsX)
		{
			width = CellsX - x;
		}
		if (y + height > CellsY)
		{
			height = CellsY - y;
		}
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				Data[x + i, y + j] = to;
			}
		}
	}

	public bool CheckRect(int x, int y, int width, int height)
	{
		if (x < 0)
		{
			width += x;
			x = 0;
		}
		if (y < 0)
		{
			height += y;
			y = 0;
		}
		if (x + width > CellsX)
		{
			width = CellsX - x;
		}
		if (y + height > CellsY)
		{
			height = CellsY - y;
		}
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				if (Data[x + i, y + j])
				{
					return true;
				}
			}
		}
		return false;
	}

	public bool CheckColumn(int x)
	{
		for (int i = 0; i < CellsY; i++)
		{
			if (!Data[x, i])
			{
				return false;
			}
		}
		return true;
	}

	public bool CheckRow(int y)
	{
		for (int i = 0; i < CellsX; i++)
		{
			if (!Data[i, y])
			{
				return false;
			}
		}
		return true;
	}

	public override Collider Clone()
	{
		return new Grid(CellWidth, CellHeight, Data.Clone());
	}

	public override void Render(Camera camera, Color color)
	{
		if (camera == null)
		{
			for (int i = 0; i < CellsX; i++)
			{
				for (int j = 0; j < CellsY; j++)
				{
					if (Data[i, j])
					{
						Draw.HollowRect(base.AbsoluteLeft + (float)i * CellWidth, base.AbsoluteTop + (float)j * CellHeight, CellWidth, CellHeight, color);
					}
				}
			}
			return;
		}
		int num = (int)Math.Max(0f, (camera.Left - base.AbsoluteLeft) / CellWidth);
		int num2 = (int)Math.Min(CellsX - 1, Math.Ceiling((camera.Right - base.AbsoluteLeft) / CellWidth));
		int num3 = (int)Math.Max(0f, (camera.Top - base.AbsoluteTop) / CellHeight);
		int num4 = (int)Math.Min(CellsY - 1, Math.Ceiling((camera.Bottom - base.AbsoluteTop) / CellHeight));
		for (int k = num; k <= num2; k++)
		{
			for (int l = num3; l <= num4; l++)
			{
				if (Data[k, l])
				{
					Draw.HollowRect(base.AbsoluteLeft + (float)k * CellWidth, base.AbsoluteTop + (float)l * CellHeight, CellWidth, CellHeight, color);
				}
			}
		}
	}

	public override bool Collide(Vector2 point)
	{
		if (point.X >= base.AbsoluteLeft && point.Y >= base.AbsoluteTop && point.X < base.AbsoluteRight && point.Y < base.AbsoluteBottom)
		{
			return Data[(int)((point.X - base.AbsoluteLeft) / CellWidth), (int)((point.Y - base.AbsoluteTop) / CellHeight)];
		}
		return false;
	}

	public override bool Collide(Rectangle rect)
	{
		if (rect.Intersects(base.Bounds))
		{
			int num = (int)(((float)rect.Left - base.AbsoluteLeft) / CellWidth);
			int num2 = (int)(((float)rect.Top - base.AbsoluteTop) / CellHeight);
			int width = (int)(((float)rect.Right - base.AbsoluteLeft - 1f) / CellWidth) - num + 1;
			int height = (int)(((float)rect.Bottom - base.AbsoluteTop - 1f) / CellHeight) - num2 + 1;
			return CheckRect(num, num2, width, height);
		}
		return false;
	}

	public override bool Collide(Vector2 from, Vector2 to)
	{
		from -= base.AbsolutePosition;
		to -= base.AbsolutePosition;
		from /= new Vector2(CellWidth, CellHeight);
		to /= new Vector2(CellWidth, CellHeight);
		bool flag = Math.Abs(to.Y - from.Y) > Math.Abs(to.X - from.X);
		if (flag)
		{
			float x = from.X;
			from.X = from.Y;
			from.Y = x;
			x = to.X;
			to.X = to.Y;
			to.Y = x;
		}
		if (from.X > to.X)
		{
			Vector2 vector = from;
			from = to;
			to = vector;
		}
		float num = 0f;
		float num2 = Math.Abs(to.Y - from.Y) / (to.X - from.X);
		int num3 = ((from.Y < to.Y) ? 1 : (-1));
		int num4 = (int)from.Y;
		int num5 = (int)to.X;
		for (int i = (int)from.X; i <= num5; i++)
		{
			if (flag)
			{
				if (this[num4, i])
				{
					return true;
				}
			}
			else if (this[i, num4])
			{
				return true;
			}
			num += num2;
			if (num >= 0.5f)
			{
				num4 += num3;
				num -= 1f;
			}
		}
		return false;
	}

	public override bool Collide(Hitbox hitbox)
	{
		return Collide(hitbox.Bounds);
	}

	public override bool Collide(Grid grid)
	{
		throw new NotImplementedException();
	}

	public override bool Collide(Circle circle)
	{
		return false;
	}

	public override bool Collide(ColliderList list)
	{
		return list.Collide(this);
	}

	public static bool IsBitstringEmpty(string bitstring)
	{
		for (int i = 0; i < bitstring.Length; i++)
		{
			if (bitstring[i] == '1')
			{
				return false;
			}
		}
		return true;
	}
}
