using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Celeste.Core.Platform.Interop;

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
		string text2 = CelestePathBridge.ResolveErrorLogPath("error_log.txt");
		StringBuilder stringBuilder = new StringBuilder();
		string text = "";
		if (Path.IsPathRooted(text2))
		{
			string directoryName = Path.GetDirectoryName(text2);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
		}
		if (File.Exists(text2))
		{
			StreamReader streamReader = new StreamReader(text2);
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
		StreamWriter streamWriter = new StreamWriter(text2, append: false);
		streamWriter.Write(stringBuilder.ToString());
		streamWriter.Close();
	}

	public static void Open()
	{
		string text = CelestePathBridge.ResolveErrorLogPath("error_log.txt");
		if (File.Exists(text))
		{
			Process.Start(text);
		}
	}
}
