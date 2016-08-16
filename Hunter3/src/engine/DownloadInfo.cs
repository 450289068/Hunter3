
namespace Hunter3
{
    public class DownloadInfo    //当下载文件完成时，返回一个关于它的报告
    {
        public string Filepath;
        public int Keyword;   //关键字编号
        public int Index;      //页面索引编号
        public string Url;      //下载来源URL
        public string Md5;      //下载文件的MD5
        public bool Remove;    //是否应该删除
        public string Str_keyword;
        public string Status;   //状态 (如 已下载、已上传)

        public DownloadInfo(string f, int k, int i, string u, string m, bool d, string s, string kw)
        {
            Filepath = f;
            Keyword = k;
            Index = i;
            Remove = d;
            Url = u;
            Md5 = m;
            Status = s;
            Str_keyword = kw;
        }
    }

}


