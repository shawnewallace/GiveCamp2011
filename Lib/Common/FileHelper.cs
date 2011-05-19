using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lib.Common {

    public static class FileHelper {
        public static List<FileInfo> GetListOfFiles(string path, string searchPattern, int? numFiles) {
            if (numFiles == null)
                return (new DirectoryInfo(path)
                    .GetFiles(searchPattern).OrderByDescending(f => f.LastWriteTime))
                    .ToList();

            return (new DirectoryInfo(path)
                .GetFiles(searchPattern).OrderByDescending(f => f.LastWriteTime))
                .Take(numFiles.Value)
                .ToList();
        }
    }

}
