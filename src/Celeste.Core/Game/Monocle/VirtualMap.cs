namespace Monocle;

public class VirtualMap<T>
{
	public const int SegmentSize = 50;

	public readonly int Columns;

	public readonly int Rows;

	public readonly int SegmentColumns;

	public readonly int SegmentRows;

	public readonly T EmptyValue;

	private T[,][,] segments;

	public T this[int x, int y]
	{
		get
		{
			int num = x / 50;
			int num2 = y / 50;
			T[,] array = segments[num, num2];
			if (array == null)
			{
				return EmptyValue;
			}
			return array[x - num * 50, y - num2 * 50];
		}
		set
		{
			int num = x / 50;
			int num2 = y / 50;
			if (segments[num, num2] == null)
			{
				segments[num, num2] = new T[50, 50];
				if (EmptyValue != null && !EmptyValue.Equals(default(T)))
				{
					for (int i = 0; i < 50; i++)
					{
						for (int j = 0; j < 50; j++)
						{
							segments[num, num2][i, j] = EmptyValue;
						}
					}
				}
			}
			segments[num, num2][x - num * 50, y - num2 * 50] = value;
		}
	}

	public VirtualMap(int columns, int rows, T emptyValue = default(T))
	{
		Columns = columns;
		Rows = rows;
		SegmentColumns = columns / 50 + 1;
		SegmentRows = rows / 50 + 1;
		segments = new T[SegmentColumns, SegmentRows][,];
		EmptyValue = emptyValue;
	}

	public VirtualMap(T[,] map, T emptyValue = default(T))
		: this(map.GetLength(0), map.GetLength(1), emptyValue)
	{
		for (int i = 0; i < Columns; i++)
		{
			for (int j = 0; j < Rows; j++)
			{
				this[i, j] = map[i, j];
			}
		}
	}

	public bool AnyInSegmentAtTile(int x, int y)
	{
		int num = x / 50;
		int num2 = y / 50;
		return segments[num, num2] != null;
	}

	public bool AnyInSegment(int segmentX, int segmentY)
	{
		return segments[segmentX, segmentY] != null;
	}

	public T InSegment(int segmentX, int segmentY, int x, int y)
	{
		return segments[segmentX, segmentY][x, y];
	}

	public T[,] GetSegment(int segmentX, int segmentY)
	{
		return segments[segmentX, segmentY];
	}

	public T SafeCheck(int x, int y)
	{
		if (x >= 0 && y >= 0 && x < Columns && y < Rows)
		{
			return this[x, y];
		}
		return EmptyValue;
	}

	public T[,] ToArray()
	{
		T[,] array = new T[Columns, Rows];
		for (int i = 0; i < Columns; i++)
		{
			for (int j = 0; j < Rows; j++)
			{
				array[i, j] = this[i, j];
			}
		}
		return array;
	}

	public VirtualMap<T> Clone()
	{
		VirtualMap<T> virtualMap = new VirtualMap<T>(Columns, Rows, EmptyValue);
		for (int i = 0; i < Columns; i++)
		{
			for (int j = 0; j < Rows; j++)
			{
				virtualMap[i, j] = this[i, j];
			}
		}
		return virtualMap;
	}
}
