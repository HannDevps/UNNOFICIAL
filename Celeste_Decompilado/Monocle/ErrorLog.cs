using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Monocle;

public static class ErrorLog
{
	public const string Filename = "error_log.txt";

	public const string Marker = "==========================================";

	public static void Write(Exception e)
	{
		Write(e.ToString());
	}

	public static void Write(string str)
	{
		StringBuilder stringBuilder = new StringBuilder();
		string text = "";
		if (Path.IsPathRooted("error_log.txt"))
		{
			string directoryName = Path.GetDirectoryName("error_log.txt");
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
		}
		if (File.Exists("error_log.txt"))
		{
			StreamReader streamReader = new StreamReader("error_log.txt");
			text = streamReader.ReadToEnd();
			streamReader.Close();
			if (!text.Contains("=========================================="))
			{
				text = "";
			}
		}
		if (Engine.Instance != null)
		{
			stringBuilder.Append(Engine.Instance.Title);
		}
		else
		{
			stringBuilder.Append("Monocle Engine");
		}
		stringBuilder.AppendLine(" Error Log");
		stringBuilder.AppendLine("==========================================");
		stringBuilder.AppendLine();
		if (Engine.Instance != null && Engine.Instance.Version != null)
		{
			stringBuilder.Append("Ver ");
			stringBuilder.AppendLine(Engine.Instance.Version.ToString());
		}
		stringBuilder.AppendLine(DateTime.Now.ToString());
		stringBuilder.AppendLine(str);
		if (text != "")
		{
			int startIndex = text.IndexOf("==========================================") + "==========================================".Length;
			string value = text.Substring(startIndex);
			stringBuilder.AppendLine(value);
		}
		StreamWriter streamWriter = new StreamWriter("error_log.txt", append: false);
		streamWriter.Write(stringBuilder.ToString());
		streamWriter.Close();
	}

	public static void Open()
	{
		if (File.Exists("error_log.txt"))
		{
			Process.Start("error_log.txt");
		}
	}
}
