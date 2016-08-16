///////////////////////////////////////////////////////////////////////////
//
// File Name   :
// File Date   :
// Author      : DannyLai(L.F.)
// E-mail      : laifeng@kingsoft.com
// Description :
//
///////////////////////////////////////////////////////////////////////////


using System;

namespace SpecimenSort
{
    class AppLogHelper
    {
        public static Log _log = null;
        public static Log Log
        {
            get {
                return _log;
            }
            set {
                _log = value;
            }
        }
        
        public static void Log_i( String message )
        {
            _log.Log_i( message );
        }
        
        public static void Log_d( String message )
        {
            _log.Log_d( message );
        }
        
        public static void Log_w( String message )
        {
            _log.Log_w( message );
        }
        
        public static void Log_e( String message )
        {
            _log.Log_e( message );
        }
        
        public static void Log_o( String message )
        {
            _log.Log_o( message );
        }
    } // AppLogHelper
}