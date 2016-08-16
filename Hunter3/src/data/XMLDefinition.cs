
namespace Hunter3.Data
{
    public class XMLDefinition
    {
        public enum ElementName{
            root,   //根
                sample,         //每个样张
                    link,       //链接地址
                    md5,        //MD5码
                    path,        //存盘的地址
                    keyword     //关键字
        };
    }
}
