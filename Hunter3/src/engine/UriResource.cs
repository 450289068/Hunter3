
namespace Hunter3
{
    /// <summary>
    /// 一个Uri资源要包含的信息
    /// </summary>
    public class UriResource
    {
        public string Url;     //地址
        public int Keyword;   //关键字编号
        public int index;      //页面索引编号
        public string Text;  //URL标题

        /// <summary>
        /// 一个Uri资源类的构造函数
        /// </summary>
        /// <param name="u">URL</param>
        /// <param name="kw">关键字索引</param>
        /// <param name="i">页面索引</param>
        /// <param name="ut">超链接关键字</param>
        public UriResource(string u, int kw, int i, string ut)
        {
            Url = u;
            Keyword = kw;
            index = i;
            Text = ut;
        }
    }

    
}
