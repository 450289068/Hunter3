///////////////////////////////////////////////////////////////////////////
//
// File Name  :
// File Date  :
// Version    :
// Author     : DannyLai(L.F.)
// E-mail     : laifeng@kingsoft.com
// Decription :
//
///////////////////////////////////////////////////////////////////////////


using System;
using System.Diagnostics;
using System.IO;


namespace SpecimenSort
{
    public class Log
    {
        String mLogPath = null;
        String mLogFile = null;
        
        public String LogPath { get { return mLogPath; } }
        public String LogFile { get { return mLogFile; } }
    
        readonly String INFO   = "  [INFO]";
        readonly String DEBUG  = " [DEBUG]";
        readonly String WARN   = "  [WARN]";
        readonly String ERROR  = " [ERROR]";
        readonly String OUTPUT = "[OUTPUT]";
        
        enum LogType
        {
            Info,
            Debug,
            Warn,
            Error,
        }
    
        public Log( String path_exe )
        {
            mLogPath = path_exe;
            mLogPath += "log";
            CreateDirectory( mLogPath );
            
            String str = String.Format( "log-{0:yyyy-MM-dd_hh-mm-ss-tt}.txt", DateTime.Now );
            // Console.WriteLine( "{0}", str );
            
            mLogFile = mLogPath;
            mLogFile += "\\";
            mLogFile += str;
        }
        
        #region 日志跟踪类型
        /**
         * 日志跟踪类型
         *
         */
        public void Log_i( String message )
        {
            Log_v( LogType.Info, INFO, message );
        }
        
        public void Log_d( String message )
        {
            Log_v( LogType.Debug, DEBUG, message );
        }
        
        public void Log_w( String message )
        {
            Log_v( LogType.Warn, WARN, message );
        }
        
        public void Log_e( String message )
        {
            Log_v( LogType.Error, ERROR, message );
        }
        
        public void Log_o( String message )
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine( "{0} {1}", OUTPUT, message );
            Console.ResetColor();
        }
        
        private void Log_v( LogType t, String s, String message )
        {
            switch ( t )
            {
            case LogType.Info:
                Console.ForegroundColor = ConsoleColor.White;
                break;
            
            case LogType.Debug:
                Console.ForegroundColor = ConsoleColor.Cyan;
                break;
                
            case LogType.Warn:
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
                
            case LogType.Error:
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            
            default:
                break;
            }
            
            Console.WriteLine( "{0} {1}", s, message );
            Console.ResetColor();
            
            FileStream file_stream = null;
            StreamWriter stream_writer = null;
            
            try {
                lock (this)
                {
                    FileInfo fi = new FileInfo(mLogFile);
                    if (!fi.Exists)
                    {
                        file_stream = fi.Create();
                        stream_writer = new StreamWriter(file_stream);
                    }
                    else
                    {
                        file_stream = fi.Open(FileMode.Append, FileAccess.Write);
                        stream_writer = new StreamWriter(file_stream);
                    }

                    stream_writer.WriteLine(DateTime.Now + " " + s + " " + message);
                }
            }
            finally {
                if ( stream_writer != null ) {
                    stream_writer.Close();
                    stream_writer.Dispose();
                }
                
                if ( file_stream != null ) {
                    file_stream.Close();
                    file_stream.Dispose();
                }
            }
        }
        #endregion
        
        private void CreateDirectory( String str_path )
        {
            DirectoryInfo di = new DirectoryInfo( str_path );
            if ( ! di.Exists ) {
                di.Create();
            }
        }
    }
}