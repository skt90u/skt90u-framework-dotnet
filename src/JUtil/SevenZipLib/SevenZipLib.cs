using JUtil.Extensions;
using JUtil.Path;
using JUtil.ResourceManagement;
using SevenZip;

namespace JUtil
{
    /// <summary>
    /// .Net SevenZip Library
    /// </summary>
    public static class SevenZipLib
    {
        #region 7zip LibraryPath

        /// <summary>
        /// 取得 7z.dll 檔案路徑，如果檔案不存在就從內嵌資源拷貝一份到對應路徑
        /// </summary>
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

        #region Extract
        
        /// <summary>
        /// 解壓縮
        /// </summary>
        /// <param name="archFileName">壓縮檔路徑</param>
        /// <param name="extractFolder">解壓縮到指定目錄</param>
        public static void ExtractArchive(string archFileName, string extractFolder)
        {
            SevenZipExtractor.SetLibraryPath(LibraryPath);
            var extractor = new SevenZipExtractor(archFileName);
            extractor.BeginExtractArchive(extractFolder);
        }

        #endregion

        #region Compress
        
        /// <summary>
        /// 壓縮
        /// </summary>
        /// <param name="compressDirectory">要壓縮的目錄路徑</param>
        /// <param name="archFileName">指定壓縮檔路徑</param>
        public static void CompressDirectory(string compressDirectory, string archFileName)
        {
            OutArchiveFormat archiveFormat = OutArchiveFormat.SevenZip;
            CompressionLevel compressionLevel = CompressionLevel.Fast;
            CompressDirectory(compressDirectory, archFileName, archiveFormat, compressionLevel);
        }

        /// <summary>
        /// 壓縮
        /// </summary>
        /// <param name="compressDirectory">要壓縮的目錄路徑</param>
        /// <param name="archFileName">指定壓縮檔路徑</param>
        /// <param name="archiveFormat">壓縮格式</param>
        /// <param name="compressionLevel">壓縮比</param>
        public static void CompressDirectory(string compressDirectory, string archFileName, OutArchiveFormat archiveFormat, CompressionLevel compressionLevel)
        {
            SevenZipCompressor.SetLibraryPath(LibraryPath);
            SevenZipCompressor cmp = new SevenZipCompressor();
            cmp.ArchiveFormat = archiveFormat;
            cmp.CompressionLevel = compressionLevel;
            cmp.BeginCompressDirectory(compressDirectory, archFileName);
        }

        #endregion


    } // end of SevenZipLib
}
