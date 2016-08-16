using System.Collections.Generic;
using System.Windows.Forms;
namespace Hunter3
{
    /// <summary>
    /// 表示一个记录Hunter输出结果的类，其实为一个表格
    /// </summary>
    public class Recorder
    {
        /// <summary>
        /// 表示纵向的表
        /// </summary>
        public string[] columns;

        /// <summary>
        /// 表示某一行的数据
        /// </summary>
        public List<string[]> rows = new List<string[]>();

        /// <summary>
        /// 最大的行数
        /// </summary>
        public int maximum_rows;

        public Recorder(int _max_rows, params string[] _columns_name)
        {
            columns = _columns_name;
            maximum_rows = _max_rows;
        }
        /// <summary>
        /// 添加一行数据。若数据添加后大于最大行数，则删除第一行
        /// </summary>
        /// <param name="_rows"></param>
        public void addRow(params string[] _rows)
        {
            rows.Add(_rows);
            if (rows.Count > maximum_rows) rows.RemoveAt(0);
        }

        /// <summary>
        /// 初始一个ListView
        /// </summary>
        public void initListView(ListView lv)
        {
            lv.View = View.Details;
            foreach (string header in columns)
            {
                lv.Columns.Add(header, lv.Width / columns.Length);
            }
        }

        /// <summary>
        /// 更新到ListView。它只更新最后一条记录
        /// </summary>
        public void update(ListView lv)
        {
            string[] content = rows[rows.Count - 1];
            lv.Items.Insert(0, new ListViewItem(content));
            if (lv.Items.Count > maximum_rows) //lv.Items.RemoveAt(maximum_rows);
                lv.Items.Clear();   //此处改为Clear
        }

        /// <summary>
        /// 重绘整个ListView，加入所有数据
        /// </summary>
        /// <param name="?"></param>
        public void updateAll(ListView lv)
        {
            lv.Clear();
            initListView(lv);
            foreach (string[] rs in rows)
            {
                string[] content = rs;
                lv.Items.Insert(0, new ListViewItem(rs));
            }
        }

    }


}
