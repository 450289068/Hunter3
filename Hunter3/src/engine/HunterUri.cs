using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using Hunter3;
using Hunter3.Strategies;
using System.ComponentModel;

namespace Hunter3
{
    /// <summary>
    /// 为了捕获Uri的类
    /// </summary>
    public class HunterUri
    {
        delegate void StringHandler(String url);

        /// <summary>
        /// URL地址
        /// </summary>
        private string urlAddress = null;

        /// <summary>
        /// 记录了分析了多少个地址
        /// </summary>
        static private int allCount = 0;

        /// <summary>
        /// 记录了分析了多少个有用的地址
        /// </summary>
        static private int availableCount = 0;

        HunterConsole mHunterConsole;
        
        Strategy strategy;

        /// <summary>
        /// 准备下载的资源队列
        /// </summary>
        public Queue<UriResource> uriQueue;
        
        /// <summary>
        /// 捕获带有filetype结尾的链接。proxy为代理，为null表示不使用代理。
        /// </summary>
        /// <returns>返回一个本次页面中捕获的链接序列</returns>
        public List<string> HuntUris(HunterProxy proxy, HunterForm main)
        {
            Regex linkReg = null;
            String htmlCode = null;
            List<string> thisURL = new List<string>();  //记录本次匹配的所有URL项

            try
            {
                linkReg = new Regex(strategy.StrategyData.configuration.Regex);    //超链接+超链接文本

                WebProxy webproxy;
                if (proxy != null)
                {
                    webproxy = new WebProxy(proxy.IPAndPort);
                }
                else
                {
                    webproxy = null;
                }

                if (proxy != null)
                {
                    mHunterConsole.WriteDetails("正在使用代理：" + proxy.IPAndPort + "(" + proxy.Description + ")");
                }
                mHunterConsole.WriteDetails("准备分析页面：" + urlAddress);
                htmlCode = GetPageHtml(webproxy, main);
                mHunterConsole.WriteHTML(htmlCode);
            }
            catch (WebException ex)    //如果是返回超时，返回一个Count>0的随机结果
            {
                thisURL.Add("{/WebException/}" + new Random().Next().ToString());

                mHunterConsole.WriteDetails("页面" + urlAddress + "请求失败。原因：" + ex.Message);
                mHunterConsole.ReportAbandonURI(new UriResource(urlAddress, strategy.CurrentKeywordProgress,
                    strategy.CurrentSearchProgress, null), ex.Message);
                return thisURL;
            }
            catch (Exception ex)
            {
                mHunterConsole.WriteException(ex);
            }

            try
            {
                Match m = linkReg.Match(htmlCode);
                while (m.Success)
                {
                    allCount++;
                    mHunterConsole.outputAnalysedUris(DateTime.Now, allCount);

                    //得到一个网址后，保存起来
                    string linkText = m.Result("${text}");

                    thisURL.Add(linkText);   //记录本次获取到的linkText

                    if (strategy.HasForbiddenWord(linkText))
                    {
                        m = m.NextMatch();
                        continue;    //如果含有违禁词语 则放弃下载 继续下一个
                    }

                    //对linkText中的内容进行处理，去掉里面的尖括号
                    Regex r = new Regex("<(.*?)>");
                    linkText = r.Replace(linkText, "");

                    string uri = null;
                    try
                    {
                        uri = (strategy.StrategyData.configuration.Redirect.ToLower() == "true") ? GetTheRedirectUrl(m.Result("${url}")) : (m.Result("${url}"));
                    }
                    catch (WebException)
                    {
                        mHunterConsole.WriteDetails("链接" + (m.Result("${url}") + "重定向超时。"));
                        mHunterConsole.ReportAbandonURI(new UriResource((m.Result("${url}")), strategy.CurrentKeywordProgress,
                            strategy.CurrentSearchProgress, null), "重定向超时");
                        m = m.NextMatch();
                        continue;

                    }

                    if (uri.EndsWith("." + strategy.Filetype))
                    {
                        availableCount++;
                        mHunterConsole.outputAvailableUris(DateTime.Now, availableCount);

                        UriResource u = new UriResource(uri, strategy.CurrentKeywordProgress
                            , strategy.CurrentSearchProgress, linkText);  //封装成一个Uri资源

                        if (!uriQueue.Contains(u)) //考虑在多线程中，可能会出现重复项目
                            uriQueue.Enqueue(u);    //将一个资源放入队列

                        mHunterConsole.outputDownloadingUriInfo(DateTime.Now,
                        "找到的资源的URL:" + u.Url + Environment.NewLine +
                        "标题：" + u.Text + Environment.NewLine +
                        "关键字：" + strategy.GetKeyword(u.Keyword) + Environment.NewLine +
                        "搜索页码：" + u.index + Environment.NewLine +
                        "已列入下载队列。");

                        mHunterConsole.WriteDetails("正在获得有效URI：" + uri);
                    }

                    m = m.NextMatch();
                }
            }
            catch (Exception ex)
            {
                mHunterConsole.WriteException(ex);
            }

            return thisURL;
        }

        public void setUrlAdress(string url)
        {
            urlAddress = url;
        }
        
        public HunterUri(Hunter h)
        {
            strategy = h.projectInfo.strategy ;
            if (h.projectInfo.index.ToString() == h.projectInfo.strategy.StrategyData.configuration.StartIndex)
            {
                urlAddress = h.projectInfo.strategy.GetFirstSearchURL();
            }
            else
            {
                urlAddress = h.projectInfo.strategy.GetSearchURL(h.projectInfo.index, h.projectInfo.strategy.Keywords[h.projectInfo.strategy.CurrentKeywordProgress]);
            }
            mHunterConsole = h.mHunterConsole;
            uriQueue = h.uriQueue;
            
        }

        /// <summary>
        /// 获取页面Html代码
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public string GetPageHtml(WebProxy proxy, HunterForm main)
        {
            return GetPageHtml(Encoding.UTF8, proxy, main);
        }

        public string GetPageHtml(Encoding encoding, WebProxy proxy, HunterForm main)
        {
            if ( (strategy.StrategyData.configuration.UseIE && ((HunterMain)main).mHunterConfig.HunterCore == HunterConfig.Core.Default)
                || ((HunterMain)main).mHunterConfig.HunterCore == HunterConfig.Core.IE)
            {
                lock (this)
                {
                    try
                    {
                        if (proxy != null)
                        {
                            HunterProxy.InternetSetOption(proxy.Address.ToString ().Replace ("http://","").Replace ("/",""));
                        }
                        else
                        {
                            HunterProxy.InternetSetOption(String.Empty);
                        }
                        string html = string.Empty;
                        AutoResetEvent are = new AutoResetEvent(false);
                        ((HunterMain)main).IEBrowser.NewWindow += (object sender, CancelEventArgs e) =>
                        {
                            e.Cancel = true;
                        };
                        ((HunterMain)main).IEBrowser.DocumentCompleted += (object sender, WebBrowserDocumentCompletedEventArgs e) =>
                        {
                            ((HunterMain)main).IEBrowser.ScriptErrorsSuppressed = true;
                            html = ((HunterMain)main).IEBrowser.DocumentText;
                            ((HunterMain)main).IEBrowser.Dispose();
                            ((HunterMain)main).IEBrowser = new WebBrowser();
                            are.Set();
                        };
                        String[] parameters = { urlAddress };
                        main.Invoke(new StringHandler(((HunterMain)main).IEBrowser.Navigate), parameters);
                        are.WaitOne(15000);
                        if (html != String.Empty)
                        {
                            return html;
                        }
                        else
                        {
                            throw new WebException();
                        }
                    }
                    catch (Exception) { throw; }
                }
            }
            else
            {
                HttpWebRequest request = null;
                HttpWebResponse response = null;
                StreamReader reader = null;
                try
                {
                    request = (HttpWebRequest)WebRequest.Create(urlAddress);
                    if (proxy != null) request.Proxy = proxy;
                    strategy.Disguise(request);

                    response = (HttpWebResponse)request.GetResponse();
                    if (response.StatusCode == HttpStatusCode.OK && response.ContentLength < 1024 * 1024)
                    {
                        reader = new StreamReader(response.GetResponseStream(), encoding);
                        string html = reader.ReadToEnd();
                        return html;
                    }
                }
                catch (Exception) { throw; }
                finally
                {
                    if (response != null)
                    {
                        response.Close();
                        response = null;
                    }
                    if (reader != null)
                        reader.Close();
                    if (request != null)
                        request = null;
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// 获得重定向地址
        /// </summary>
        /// <param name="originalAddress">需要解析的地址</param>
        /// <returns>返回定向后的地址</returns>
        public string GetTheRedirectUrl(string originalAddress)
        {
            string redirectUrl;
            try
            {
                WebRequest myRequest = WebRequest.Create(originalAddress);
                myRequest.Timeout = 10000;

                WebResponse myResponse = myRequest.GetResponse();
                redirectUrl = myResponse.ResponseUri.ToString();

                myResponse.Close();
                return redirectUrl;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }

    
}
