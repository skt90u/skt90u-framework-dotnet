using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SevenZip;
using JUtil.Path;
using JUtil.ResourceManagement;
using JUtil.Extensions;

namespace JUtil
{
    public static class SevenZipLib
    {
        #region 7zip LibraryPath
        private static string LibraryPath
        {
            get
            {
                if (libraryPath == null)
                {
                    libraryPath = File.GetAbsolutePath(Directory.Application, "7z.dll");
                }
                
                if (!File.Exists(libraryPath))
                {
                    ResourceManager.LocalResource["7z.dll"].SaveAs(libraryPath);
                }

                return libraryPath;
            }
        }
        private static string libraryPath;
        #endregion

        public static void ExtractArchive(string archFileName, string extractFolder)
        {
            SevenZipExtractor.SetLibraryPath(LibraryPath);
            var extractor = new SevenZipExtractor(archFileName);
            extractor.BeginExtractArchive(extractFolder);
        }

        public static void CompressDirectory(string compressDirectory, string archFileName)
        {
            OutArchiveFormat archiveFormat = OutArchiveFormat.SevenZip;
            CompressionLevel compressionLevel = CompressionLevel.Fast;
            CompressDirectory(compressDirectory, archFileName, archiveFormat, compressionLevel);
        }

        public static void CompressDirectory(string compressDirectory, string archFileName, OutArchiveFormat archiveFormat, CompressionLevel compressionLevel)
        {
            SevenZipCompressor.SetLibraryPath(LibraryPath);
            SevenZipCompressor cmp = new SevenZipCompressor();
            cmp.ArchiveFormat = archiveFormat;
            cmp.CompressionLevel = compressionLevel;
            cmp.BeginCompressDirectory(compressDirectory, archFileName);  
        }


    } // end of SevenZipLib
}
