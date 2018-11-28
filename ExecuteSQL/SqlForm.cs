using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExecuteSQL.Server;
using System.IO;
using System.Configuration;
using System.Threading;
using ExecuteSQL.Server.Enum;
using ExecuteSQL.Entity;

namespace ExecuteSQL
{
    public partial class SqlForm : Form
    {
        /// <summary>
        /// 文件路径、文件内容字典
        /// </summary>
        private Dictionary<string, string> dicSql = new Dictionary<string, string>();
        /// <summary>
        /// SQL执行对象
        /// </summary>
        private SQLExecutePlayer sqlExecutePlayer;

        public SqlForm()
        {
            InitializeComponent();
            lb_dbstr.Text = string.Format("数据库连接：{0}", ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            sqlExecutePlayer = new SQLExecutePlayer();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void bt_fd_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            //folder.ShowDialog();
            if (folder.ShowDialog() == DialogResult.Cancel)
            {
                MsgListBox.Text = "取消选择文件目录。";
                return;
            }
            //每次选择，清掉treeview所有节点
            sql_tv.Nodes.Clear();
            //选择的目录节点
            var startNode = new TreeNode();
            startNode.Tag = folder.SelectedPath;
            startNode.Text = new DirectoryInfo(folder.SelectedPath).Name;
            startNode.ToolTipText = folder.SelectedPath;
            
            sql_tv.Nodes.Add(startNode);
            //文件路径、文件内容字典
            dicSql.Clear();
            setTreeView(folder.SelectedPath, startNode.Nodes);
            //展开目录下的所有节点
            startNode.ExpandAll();

            //setTreeViewFilter(folder.SelectedPath, new TreeNode().Nodes);
        }

        /// <summary>
        /// 选择文件，可多选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_file_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog file = new OpenFileDialog();
                file.Multiselect = true;
                //标识1|*.txt;*.sql|标识2|*.xls
                file.Filter = "sql|*.txt;*.sql";
                //file.ShowDialog();
                if (file.ShowDialog() == DialogResult.Cancel)
                {
                    MsgListBox.Text = "取消选择文件。";
                    return;
                }
                if (file.FileName == string.Empty)
                {
                    MsgListBox.Text = "未选择文件。";
                    return;
                }
                //每次选择，清空treeview所有节点
                sql_tv.Nodes.Clear();
                var startNode = new TreeNode();
                DirectoryInfo pathInfo = new DirectoryInfo(file.FileNames[0]);
                startNode.Tag = pathInfo.Parent.FullName;
                startNode.Text = pathInfo.Parent.Name;
                startNode.ToolTipText = pathInfo.Parent.FullName;
                sql_tv.Nodes.Add(startNode);
                //清空文件路径、文件内容节点
                dicSql.Clear();
                foreach (var f in file.FileNames)
                {
                    var cNode = new TreeNode();
                    cNode.Tag = f;
                    cNode.Text = System.IO.Path.GetFileName(f);
                    cNode.ToolTipText = f;
                    startNode.Nodes.Add(cNode);
                    dicSql.Add(f, FileHelper.ReadFile(f));
                }
                //展开所选文件目录下的所有节点
                startNode.ExpandAll();
            }
            catch (Exception ex)
            {
                dicSql.Clear();
                throw new Exception(string.Format("文件处理出错:{0}", ex.Message));
            }
        }

        /// <summary>
        /// 设置节点
        /// </summary>
        /// <param name="rootPath"></param>
        /// <param name="nodes"></param>
        private void setTreeView(string rootPath, TreeNodeCollection nodes)
        {
            try
            {
                DirectoryInfo root = new DirectoryInfo(rootPath);
                TreeNode tempNode;
                //递归遍历目录
                foreach (DirectoryInfo d in root.GetDirectories())
                {
                    tempNode = new TreeNode();
                    tempNode.Tag = d.FullName;
                    tempNode.Text = d.Name;
                    tempNode.ToolTipText = d.FullName;
                    nodes.Add(tempNode);
                    setTreeView(d.FullName, tempNode.Nodes);
                }
                //遍历当前目录下所有文件
                foreach (FileInfo f in root.GetFiles().Where(s => s.Extension.ToLower() == ".txt" || s.Extension.ToLower() == ".sql"))
                {
                    var cnode = new TreeNode();
                    cnode.Tag = f.FullName;
                    cnode.Text = f.Name;
                    cnode.ToolTipText = f.FullName;
                    nodes.Add(cnode);
                    dicSql.Add(f.FullName, FileHelper.ReadFile(f.FullName));
                }
            }
            catch(Exception ex)
            {
                dicSql.Clear();
                throw new Exception(string.Format("文件目录处理出错:{0}", ex.Message));
            }
        }

        /// <summary>
        /// 选择预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sql_tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown && dicSql.ContainsKey(e.Node.Tag.ToString()))
            {
                tb_currentSql.Text = dicSql[e.Node.Tag.ToString()];
            }
        }

        /// <summary>
        /// 点击节点选择框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sql_tv_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //if (!dicSql.ContainsKey(e.Node.Tag.ToString()))
            //{
            //    return;
            //}
            //避免更新节点选择状态的时候，无限循环的问题
            if (e.Action != TreeViewAction.Unknown)
            {
                //
                //Thread.Sleep(500);

                //是否响应事件
                var state = sqlExecutePlayer.GetState();
                if (state != PlayerState.prepare)
                {
                    e.Node.Checked = e.Node.Checked ? false : true;
                    MsgListBox.Text = string.Format("当前处于状态：{0}，不能执行此操作。", state);
                    return;
                }
                //更新子节点选择框状态
                updateChildNodeCheck(e.Node, e.Node.Checked);
                //更新父节点选择框状态
                updateParentNodeCheck(e.Node);
            }         
            //if (e.Node.Checked)
            //{
            //    if (dicSql.ContainsKey(e.Node.Tag.ToString()))
            //    {
            //        tb_currentSql.Text = dicSql[e.Node.Tag.ToString()];
            //        sqlExecutePlayer.AddDicItem(e.Node.Tag.ToString(), dicSql[e.Node.Tag.ToString()]);
            //        SqlBox.Text = multiSQL(sqlExecutePlayer.GetDicSQL());
            //    }
            //}
            //else
            //{
            //    sqlExecutePlayer.RemoveDicItem(e.Node.Tag.ToString());
            //    SqlBox.Text = multiSQL(sqlExecutePlayer.GetDicSQL());
            //}
        }

        /// <summary>
        /// 逐个执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_execOneByOne_Click(object sender, EventArgs e)
        {
            setPlayerPanelVisible(true);
            try
            {
                MsgListBox.Text = "开始执行...";
                MsgListBox.Refresh();
                sqlExecutePlayer.Exec(tb_currentSql);
                MsgListBox.Text = "执行完成。";
                setPlayerPanelVisible(false);
            }
            catch (Exception ex)
            {
                MsgListBox.Text = ex.Message;
            }
            
        }

        /// <summary>
        /// 暂停执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_execPause_Click(object sender, EventArgs e)
        {
            try
            {
                MsgListBox.Text = "暂停。";
                sqlExecutePlayer.PauseExec();
            }
            catch (Exception ex)
            {
                MsgListBox.Text = ex.Message;
            }
        }

        /// <summary>
        /// 继续执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_execGoOn_Click(object sender, EventArgs e)
        {
            try
            {
                MsgListBox.Text = "继续执行...";
                MsgListBox.Refresh();
                sqlExecutePlayer.ContinueExec(tb_currentSql);
                MsgListBox.Text = "执行完成。";
                setPlayerPanelVisible(false);
            }
            catch (Exception ex)
            {
                MsgListBox.Text = ex.Message;
            }
        }

        /// <summary>
        /// 结束执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_execover_Click(object sender, EventArgs e)
        {
            try
            {
                MsgListBox.Text = "终止执行...";
                MsgListBox.Refresh();
                sqlExecutePlayer.OverExec();
                MsgListBox.Text = "已终止执行。";
                setPlayerPanelVisible(false);
            }
            catch (Exception ex)
            {
                MsgListBox.Text = ex.Message;
            }
        }

        private void setPlayerPanelVisible(bool visible)
        {
            playerPanel.Visible = visible;
        }

        /// <summary>
        /// 预览所有选择文件
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        private string multiSQL(Dictionary<string, string> dic)
        {
            string multisql = string.Empty;
            foreach (var sql in dic)
            {
                //multisql += sql;
                multisql += string.Format("文件=>  {0}\n语句=>  {1}\n-----分割线-----\n\n", sql.Key, sql.Value);
            }
            return multisql;
        }

        /// <summary>
        /// 执行当前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_execute_Click(object sender, EventArgs e)
        {
            var sql = tb_currentSql.Text;
            if (!string.IsNullOrWhiteSpace(sql))
            {
                DBConnection conn = null;
                try
                {
                    MsgListBox.Text = "开始执行当前SQL...";
                    MsgListBox.Refresh();
                    conn = new DBConnection();
                    conn.ExecuteNonQueryDb(sql);
                    MsgListBox.Text = "执行当前SQL完成。";
                }
                catch(Exception ex)
                {
                    if (conn != null)
                    {
                        conn.close();
                    }
                    MsgListBox.Text = ex.Message;
                }
                
            }
        }

        /// <summary>
        /// 更新子节点选择框状态
        /// </summary>
        /// <param name="node"></param>
        /// <param name="state"></param>
        private void updateChildNodeCheck(TreeNode node, bool state)
        {
            node.Checked = state;
            if (dicSql.ContainsKey(node.Tag.ToString()))
            {
                if (state)
                {

                    tb_currentSql.Text = dicSql[node.Tag.ToString()];
                    tb_currentSql.Refresh();
                    sqlExecutePlayer.AddDicItem(node.Tag.ToString(), dicSql[node.Tag.ToString()]);
                    SqlBox.Text = multiSQL(sqlExecutePlayer.GetDicSQL());
                    SqlBox.Refresh();
                }
                else
                {
                    sqlExecutePlayer.RemoveDicItem(node.Tag.ToString());
                    SqlBox.Text = multiSQL(sqlExecutePlayer.GetDicSQL());
                }
            }
            foreach (TreeNode tn in node.Nodes)
            {
                updateChildNodeCheck(tn, state);
            }
        }

        /// <summary>
        /// 更新父节点选择框状态
        /// </summary>
        /// <param name="node"></param>
        private void updateParentNodeCheck(TreeNode node)
        {
            if (node.Parent != null)
            {
                //兄弟节点被选中的个数 
                int brotherNodeCheckedCount = 0;
                //兄弟节点数
                int brotherNodeCount = node.Parent.Nodes.Count;

                //遍历该节点的兄弟节点 
                foreach (TreeNode tn in node.Parent.Nodes)
                {
                    if (tn.Checked == true)
                    {
                        brotherNodeCheckedCount++;
                    }
                }

                //兄弟节点全没选，其父节点也不选 
                //if (brotherNodeCheckedCount == 0)
                //{
                //    node.Parent.Checked = false;
                //    updateParentNodeCheck(node.Parent);
                //}

                //兄弟节点全选，则父节点选择，否则不选择 
                if (brotherNodeCheckedCount == brotherNodeCount)
                {
                    node.Parent.Checked = true;
                    updateParentNodeCheck(node.Parent);
                }
                else
                {
                    node.Parent.Checked = false;
                    var tn = node.Parent.Parent;
                    while(tn != null)
                    {
                        tn.Checked = false;
                        tn = tn.Parent;
                    }
                    //updateParentNodeCheck(node.Parent);
                }

            }
        }

            //private void setTreeViewFilter(string rootPath, TreeNodeCollection nodes)
            //{
            //    var dic = new Dictionary<string, string>();
            //    DirectoryInfo root = new DirectoryInfo(rootPath);
            //    var files = root.GetFiles("*.*", SearchOption.AllDirectories).Where(s => s.Extension.ToLower() == ".txt" || s.Extension.ToLower() == ".sql");
            //    TreeNode tempNode;

            //    if (files.Count() == 0)
            //    {
            //        nodes.RemoveByKey(rootPath);
            //    }
            //    else
            //    {
            //        foreach (var file in files)
            //        {
            //            dic.Add(file.FullName, file.Name);
            //        }
            //    }
            //    var filesgbdir = dic.GroupBy(f => new DirectoryInfo(f.Key).Parent.FullName).OrderBy(fg => fg.Key);
            //    var parentDir = rootPath;
            //    foreach (var fileList in filesgbdir)
            //    {
            //        if (fileList.Key != rootPath)
            //        {
            //            var childDir = 
            //        }
            //        foreach (var file in fileList)
            //        {

            //        }
            //        parentDir = fileList.Key;
            //    }
            //    foreach (DirectoryInfo d in root.GetDirectories())
            //    {
            //        tempNode = new TreeNode();
            //        tempNode.Tag = d.FullName;
            //        tempNode.Text = d.Name;
            //        nodes.Add(tempNode);
            //        setTreeView(d.FullName, tempNode.Nodes);
            //    }
            //    foreach (FileInfo f in root.GetFiles())
            //    {
            //        var cnode = new TreeNode();
            //        cnode.Tag = f.FullName;
            //        cnode.Text = f.Name;
            //        nodes.Add(cnode);
            //    }
            //}

        }
}
