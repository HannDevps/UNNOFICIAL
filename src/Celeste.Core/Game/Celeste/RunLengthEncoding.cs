using System.Collections.Generic;
using System.Text;

namespace Celeste;

public static class RunLengthEncoding
{
	public static byte[] Encode(string str)
	{
		List<byte> list = new List<byte>();
		for (int i = 0; i < str.Length; i++)
		{
			byte b = 1;
			char c;
			for (c = str[i]; i + 1 < str.Length && str[i + 1] == c; i++)
			{
				if (b >= byte.MaxValue)
				{
					break;
				}
				b++;
			}
			list.Add(b);
			list.Add((byte)c);
		}
		return list.ToArray();
	}

	public static string Decode(byte[] bytes)
	{
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < bytes.Length; i += 2)
		{
			stringBuilder.Append((char)bytes[i + 1], bytes[i]);
		}
		return stringBuilder.ToString();
	}
}
