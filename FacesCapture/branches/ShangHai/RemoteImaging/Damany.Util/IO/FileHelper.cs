using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Damany.Util.IO
{
    public static class FileHelper
    {
        public static void EnsureDelete(string path)
        {
            if (String.IsNullOrEmpty(path))
                throw new ArgumentException("path is null or empty.", "path");

            if (!File.Exists(path))
                throw new ArgumentException(path + " doesn't exist.", "path");

            try
            {
                File.Delete(path);
            }
            catch (Exception)
            {
                //在系统重新启动的时候删除文件. 
                //参加: http://msdn.microsoft.com/en-us/library/aa365240(v=vs.85).aspx
                Win32Native.MoveFileEx(path, null, 4);
            }
        }
    }
}
