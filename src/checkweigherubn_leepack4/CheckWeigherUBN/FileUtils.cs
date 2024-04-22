using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace CheckWeigherUBN
{
  public class FileUtils
  {
    public static bool CheckFileInUse(FileInfo file)
	{
		FileStream stream = null;
		try
		{
			if (file.Exists)
			{
				stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
			}
		}
		catch (IOException)
		{
			return true;
		}
		finally
		{
      if (stream != null)
      {
        stream.Close();
      }
			//stream?.Close();
		}
		return false;
	}

    public static bool CheckPathRootExists(string file_path)
    {
      bool IsOk = false;
      if (file_path != "")
      {
        try
        {
          string PathRoot = Path.GetPathRoot(file_path);
          for (int i = 0; i < DriveInfo.GetDrives().Length; i++)
          {
            if (IsOk)
            {
              break;
            }
            DriveInfo drive = DriveInfo.GetDrives()[i];
            IsOk = drive.Name.Contains(PathRoot);
          }
        }
        catch
        {
        }
      }
      return IsOk;
    }

    public static bool CheckFolderContainFile(string file_path)
    {
      bool ret = false;
      try
      {
        DirectoryInfo folderparent = Directory.GetParent(file_path);
        ret = folderparent.Exists;
      }
      catch
      {
      }
      return ret;
    }

    public static bool CheckFileExists(string file_path)
    {
      return File.Exists(file_path);
    }

    public static bool CreateNewFolderIfNotExists(string folderPath)
    {
      bool ret = false;
      try
      {
        DirectoryInfo folderCreated = Directory.CreateDirectory(folderPath);
        return folderCreated.Exists;
      }
      catch
      {
        return false;
      }
    }

    public static string GetDirectoryName(string file_path)
    {
      string folderparent = "";
      if (file_path != "")
      {
        folderparent = Path.GetDirectoryName(file_path);
      }
      return folderparent;
    }

    public static bool CreateNewFolderContainsFileIfnotExists(string file_path)
    {
      bool ret = false;
      try
      {
        if (CheckFolderContainFile(file_path))
        {
          return true;
        }
        string folderparent = Path.GetDirectoryName(file_path);
        DirectoryInfo folderCreated = Directory.CreateDirectory(folderparent);
        return folderCreated.Exists;
      }
      catch
      {
        return false;
      }
    }
  }
}
