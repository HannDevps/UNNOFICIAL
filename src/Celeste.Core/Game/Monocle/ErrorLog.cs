using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Celeste.Core.Platform.Interop;

namespace Monocle;

public static class ErrorLog
{
	public const string Filename = "error_log.txt";

	public const string AndroidUnifiedFilename = "tudo_unificado.txt";

	public const string Marker = "==========================================";

	public static void Write(Exception e)
	{
		Write(e.ToString());
	}

	public static void Write(string str)
	{
		if (OperatingSystem.IsAndroid())
		{
			WriteToAndroidUnifiedLog(str);
			return;
		}

		string text2 = CelestePathBridge.ResolveErrorLogPath(Filename);
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

	private static void WriteToAndroidUnifiedLog(string str)
	{
		string text = CelestePathBridge.ResolveErrorLogPath(AndroidUnifiedFilename);
		if (Path.IsPathRooted(text))
		{
			string directoryName = Path.GetDirectoryName(text);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
		}

		using StreamWriter streamWriter = new StreamWriter(new FileStream(text, FileMode.Append, FileAccess.Write, FileShare.ReadWrite), new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
		string value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
		streamWriter.WriteLine(value + " | #ERRLOG | ERROR | ERROR_LOG | BEGIN");
		using StringReader stringReader = new StringReader(str ?? string.Empty);
		string line;
		while ((line = stringReader.ReadLine()) != null)
		{
			streamWriter.WriteLine(value + " | #ERRLOG | ERROR | ERROR_LOG | " + line);
		}

		streamWriter.WriteLine(value + " | #ERRLOG | ERROR | ERROR_LOG | END");
	}

	public static void Open()
	{
		string text = CelestePathBridge.ResolveErrorLogPath(OperatingSystem.IsAndroid() ? AndroidUnifiedFilename : Filename);
		if (File.Exists(text))
		{
			Process.Start(text);
		}
	}
}
