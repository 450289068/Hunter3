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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using MySql.Data.MySqlClient;


namespace SpecimenSort
{
    public class MySqlDBConnection
    {
        String mServer;
        String mDatabase;
        String mUid;
        String mPassword;
    
        public String Server   { get { return   mServer; } set { mServer   =   Server; } }
        public String Database { get { return mDatabase; } set { mDatabase = Database; } }
        public String UID      { get { return      mUid; } set { mUid      =      UID; } }
        public String Password { get { return mPassword; } set { mPassword = Password; } }
        
        public MySqlConnection NewConnectionInstance { get {
            String str_connection = String.Format("SERVER={0};DATABASE={1};UID={2};PASSWORD={3};", mServer, mDatabase, mUid, mPassword);
            return new MySqlConnection(str_connection);
        } }
        
        Log mlog = null;
    
        public MySqlDBConnection( String str_server, String str_database, String str_uid, String str_password, Log log )
        {
            Debug.Assert( ! String.IsNullOrWhiteSpace( str_server   ), "不允许为空" );
            Debug.Assert( ! String.IsNullOrWhiteSpace( str_database ), "不允许为空" );
            Debug.Assert( ! String.IsNullOrWhiteSpace( str_uid      ), "不允许为空" );
            Debug.Assert( ! String.IsNullOrWhiteSpace( str_password ), "不允许为空" );
        
            mServer   = str_server;
            mDatabase = str_database;
            mUid      = str_uid;
            mPassword = str_password;
            
            mlog = log;
            
            String str_connection = String.Format( "SERVER={0};DATABASE={1};UID={2};PASSWORD={3};", str_server, str_database, str_uid, str_password );
            mlog.Log_i( "连接数据库 " + str_connection );
        }

        public bool TestOpenClose()
        {
            try
            {
                MySqlConnection c = NewConnectionInstance;
                c.Open();
                c.Close();
            }
            catch (MySqlException e)
            {
                mlog.Log_e("连接数据库失败 MySqlException: " + e.Message);
                return false;
            }
            catch (Exception e)
            {
                mlog.Log_e("连接数据库失败 Exception: " + e.Message);
                return false;
            }

            return true;
        }

    }
}