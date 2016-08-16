using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hunter3.Data;

namespace Hunter3.Data
{
    //一个连接数据库的辅助类
    public class HunterDatabaseHelper
    {
        public Database database;
        private ProjectInfo project;

        public HunterDatabaseHelper(ProjectInfo pj)
        {
            project = pj;
            database = new Database(pj.mHunterConsole, pj.db_username, pj.db_pwd, pj.db_ip, pj.db_dbname, pj.db_port);
        }

        public Database GetDatabaseInstance()
        {
            return new Database(project.mHunterConsole, project.db_username, project.db_pwd, project.db_ip, project.db_dbname, project.db_port);
        }
    }

}
