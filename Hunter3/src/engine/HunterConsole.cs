using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hunter3
{
    /// <summary>
    /// 显示输出信息的类，继承了基本的OutputHandler
    /// </summary>

    public class HunterConsole
    {
        #region 委托和事件的定义
        #region 委托和事件的定义
        public delegate void dEvents();

        public delegate void dAllInfo(int totalProgressValue, int singleProgressValue, string msg);

        public delegate void dWriteMessage(string msg);  //和上面一样

        public delegate void dSingleProgressInfo(int singleProgressValue);  //和上面一样

        /// <summary>
        /// 关于设定进度最大值的委托
        /// </summary>
        /// <param name="maxValue"></param>
        public delegate void dSetProgressMax(int maxValue);

        /// <summary>
        /// 当调用done()的时候触发的事件
        /// </summary>
        public event dEvents onDone;

        public event dWriteMessage onWriteMessage;
        public event dWriteMessage onWriteDetails;
        public event dWriteMessage onWriteDownload;
        public event dWriteMessage onWriteProxy;
        public event dWriteMessage onWriteHTML;

        /// <summary>
        /// 当调用refreshSingleProgressInfo方法的时候触发的事件
        /// </summary>
        public event dSingleProgressInfo onRefreshSingleProgressInfo;

        /// <summary>
        /// 当调用setProgressTotalMax方法时触发的事件
        /// </summary>
        public event dSetProgressMax onSetProgressTotalMax;

        /// <summary>
        /// 当调用setProgressSingleMax方法时触发的事件
        /// </summary>
        public event dSetProgressMax onSetProgressSingleMax;

        /// <summary>
        /// 返回异常的委托
        /// </summary>
        /// <param name="e"></param>
        public delegate void dWriteException(Exception e);

        #endregion

        #region 更新消息的事件触发
        
        public void refreshSingleProgressInfo(int singleProgressValue)
        {
            if (onRefreshSingleProgressInfo != null) onRefreshSingleProgressInfo(singleProgressValue);
        }

        public void WriteMessage(string msg)
        {
            if (onWriteMessage != null) onWriteMessage(msg);
        }

        public void WriteDetails(string msg)
        {
            if (onWriteDetails != null) onWriteDetails(msg);
        }

        public void WriteHTML(string msg)
        {
            if (onWriteHTML != null) onWriteHTML(msg);
        }

        public void WriteDownload(string msg)
        {
            if (onWriteDownload != null) onWriteDownload(msg);
        }

        public void WriteProxy(string msg)
        {
            if (onWriteProxy != null) onWriteProxy(msg);
        }
        #endregion

        #region 更新进度的方法

        public void SetProgressTotalMax(int max)
        {
            if (onSetProgressTotalMax != null) onSetProgressTotalMax(max);
        }

        public void SetProgressSingleMax(int max)
        {
            if (onSetProgressSingleMax != null) onSetProgressSingleMax(max);
        }
        #endregion

        #region 行为完成时调用的方法
        
        public void Done()
        {
            if (onDone != null) onDone();
        }
        #endregion

        public delegate void dLogOutput(DateTime when, string what);

        public event dLogOutput onOutputDownloadingUriInfo;

        public event dLogOutput onOutputStartInformation;

        public delegate void dNumericOutput(DateTime when, long what);

        public event dNumericOutput onOutputSpeedInfo;

        public event dNumericOutput onOutputDownloadedFileNum;

        public event dNumericOutput onOutputAnalysedUris;

        public event dNumericOutput onOutputDocumentUris;

        public event dWriteException onWriteException;

        public delegate void dReportDownloadInfo(DownloadInfo d);

        public event dReportDownloadInfo onReportDownloadedInfo;

        public delegate void dReportAbandonDownloadInfo(DownloadInfo d, string reason);

        public event dReportAbandonDownloadInfo onReportAbandonDownloadInfo;

        public delegate void dReportAbandonURI(UriResource u, string reason);

        public event dReportAbandonURI onReportAbandonURI;

        public delegate void dModeChange(string changedMode);

        public event dModeChange onModeChange;

        #endregion

        public HunterConsole() { }
        
        public void outputDownloadingUriInfo(DateTime when, string what)
        {
            if (onOutputDownloadingUriInfo != null) onOutputDownloadingUriInfo(when, what);
        }

        #region dNumbericOutput委托的方法
        
        public void outputSpeedInfo(DateTime when, int what)
        {
            if (onOutputSpeedInfo != null) onOutputSpeedInfo(when, what);
        }

        public void outputDownloadedFileNum(DateTime when, int what)
        {
            if (onOutputDownloadedFileNum != null) onOutputDownloadedFileNum(when, what);
        }

        public void outputAnalysedUris(DateTime when, int what)
        {
            if (onOutputAnalysedUris != null) onOutputAnalysedUris(when, what);
        }

        public void outputAvailableUris(DateTime when, int what)
        {
            if (onOutputDocumentUris != null) onOutputDocumentUris(when, what);
        }

        public void outputStartInformation(DateTime when, string what)
        {
            if (onOutputStartInformation != null) onOutputStartInformation(when, what);
        }

        public void WriteException(Exception e)
        {
            if (onWriteException != null) onWriteException(e);
        }

        public void ReportDownloadInfo(DownloadInfo i)
        {
            if (onReportDownloadedInfo != null) onReportDownloadedInfo(i);
        }

        public void ReportAbandonDownloadInfo(DownloadInfo i, string reason)
        {
            if (onReportAbandonDownloadInfo != null) onReportAbandonDownloadInfo(i, reason);
        }

        public void ReportAbandonURI(UriResource u, string reason)
        {
            if (onReportAbandonURI != null) onReportAbandonURI(u, reason);
        }

        #endregion

        #region 其他

        public void ModeChange(string mode)
        {
            if (onModeChange != null) onModeChange(mode);
        }
        #endregion
        
    }

}
