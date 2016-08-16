using Hunter3.Data;
using System.Threading;
namespace Hunter3
{
    /// <summary>
    /// 表示一个Hunter的下载线程
    /// </summary>
    public struct HunterDownloadThread
    {
        /// <summary>
        /// 表示一个下载线程
        /// </summary>
        public Thread downloadThread;
        public HunterDatabaseHelper databaseHelper;

    }


}
