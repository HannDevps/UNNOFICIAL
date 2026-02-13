using System;
using System.IO;

namespace Celeste.Core.Platform.Save;

public static class AtomicFileWriter
{
    public static void WriteAllBytes(string destinationPath, byte[] content)
    {
        var directory = Path.GetDirectoryName(destinationPath);
        if (string.IsNullOrEmpty(directory))
        {
            throw new InvalidOperationException($"Invalid destination path: {destinationPath}");
        }

        Directory.CreateDirectory(directory);

        var tempPath = destinationPath + ".tmp";
        var backupPath = destinationPath + ".bak";

        using (var stream = new FileStream(tempPath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            stream.Write(content, 0, content.Length);
            stream.Flush(true);
        }

        if (File.Exists(destinationPath))
        {
            File.Replace(tempPath, destinationPath, backupPath, true);
            if (File.Exists(backupPath))
            {
                File.Delete(backupPath);
            }
            return;
        }

        File.Move(tempPath, destinationPath);
    }
}
