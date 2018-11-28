using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExecuteSQL.Entity
{
    class CustomTreeView : TreeView
    {
        public bool IsParent { get; set; }
        /// <summary>
        /// 解决treeview快速连续点击事件不响应问题
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg != 0x203)
            {
                base.WndProc(ref m);
            }
        }
    }

}
