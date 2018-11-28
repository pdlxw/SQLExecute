
namespace ExecuteSQL
{
    partial class SqlForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.sql_tv = new ExecuteSQL.Entity.CustomTreeView();
            this.bt_fd = new System.Windows.Forms.Button();
            this.bt_file = new System.Windows.Forms.Button();
            this.DirPanel = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.playerPanel = new System.Windows.Forms.Panel();
            this.bt_execGoOn = new System.Windows.Forms.Button();
            this.bt_execover = new System.Windows.Forms.Button();
            this.bt_execPause = new System.Windows.Forms.Button();
            this.pauseAwhile = new System.Windows.Forms.CheckBox();
            this.bt_execOneByOne = new System.Windows.Forms.Button();
            this.tb_currentSql = new System.Windows.Forms.TextBox();
            this.bt_execute = new System.Windows.Forms.Button();
            this.SqlPanel = new System.Windows.Forms.GroupBox();
            this.SqlBox = new System.Windows.Forms.RichTextBox();
            this.MsgPanel = new System.Windows.Forms.GroupBox();
            this.MsgListBox = new System.Windows.Forms.TextBox();
            this.lb_dbstr = new System.Windows.Forms.Label();
            this.DirPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.playerPanel.SuspendLayout();
            this.SqlPanel.SuspendLayout();
            this.MsgPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // sql_tv
            // 
            this.sql_tv.CheckBoxes = true;
            this.sql_tv.Location = new System.Drawing.Point(6, 20);
            this.sql_tv.Name = "sql_tv";
            this.sql_tv.Size = new System.Drawing.Size(303, 423);
            this.sql_tv.TabIndex = 0;
            this.sql_tv.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.sql_tv_AfterCheck);
            this.sql_tv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.sql_tv_AfterSelect);
            // 
            // bt_fd
            // 
            this.bt_fd.Location = new System.Drawing.Point(172, 36);
            this.bt_fd.Name = "bt_fd";
            this.bt_fd.Size = new System.Drawing.Size(70, 23);
            this.bt_fd.TabIndex = 1;
            this.bt_fd.Text = "选择目录";
            this.bt_fd.UseVisualStyleBackColor = true;
            this.bt_fd.Click += new System.EventHandler(this.bt_fd_Click);
            // 
            // bt_file
            // 
            this.bt_file.Location = new System.Drawing.Point(248, 36);
            this.bt_file.Name = "bt_file";
            this.bt_file.Size = new System.Drawing.Size(69, 23);
            this.bt_file.TabIndex = 5;
            this.bt_file.Text = "选择文件";
            this.bt_file.UseVisualStyleBackColor = true;
            this.bt_file.Click += new System.EventHandler(this.bt_file_Click);
            // 
            // DirPanel
            // 
            this.DirPanel.Controls.Add(this.sql_tv);
            this.DirPanel.Location = new System.Drawing.Point(2, 65);
            this.DirPanel.Name = "DirPanel";
            this.DirPanel.Size = new System.Drawing.Size(315, 453);
            this.DirPanel.TabIndex = 6;
            this.DirPanel.TabStop = false;
            this.DirPanel.Text = "文件目录";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.playerPanel);
            this.groupBox1.Controls.Add(this.pauseAwhile);
            this.groupBox1.Controls.Add(this.bt_execOneByOne);
            this.groupBox1.Controls.Add(this.tb_currentSql);
            this.groupBox1.Controls.Add(this.bt_execute);
            this.groupBox1.Location = new System.Drawing.Point(684, 65);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(461, 343);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "当前";
            // 
            // playerPanel
            // 
            this.playerPanel.Controls.Add(this.bt_execGoOn);
            this.playerPanel.Controls.Add(this.bt_execover);
            this.playerPanel.Controls.Add(this.bt_execPause);
            this.playerPanel.Location = new System.Drawing.Point(7, 307);
            this.playerPanel.Name = "playerPanel";
            this.playerPanel.Size = new System.Drawing.Size(159, 23);
            this.playerPanel.TabIndex = 16;
            this.playerPanel.Visible = false;
            // 
            // bt_execGoOn
            // 
            this.bt_execGoOn.Location = new System.Drawing.Point(46, 0);
            this.bt_execGoOn.Name = "bt_execGoOn";
            this.bt_execGoOn.Size = new System.Drawing.Size(41, 23);
            this.bt_execGoOn.TabIndex = 12;
            this.bt_execGoOn.Text = "继续";
            this.bt_execGoOn.UseVisualStyleBackColor = true;
            this.bt_execGoOn.Click += new System.EventHandler(this.bt_execGoOn_Click);
            // 
            // bt_execover
            // 
            this.bt_execover.Location = new System.Drawing.Point(93, 0);
            this.bt_execover.Name = "bt_execover";
            this.bt_execover.Size = new System.Drawing.Size(39, 23);
            this.bt_execover.TabIndex = 13;
            this.bt_execover.Text = "终止";
            this.bt_execover.UseVisualStyleBackColor = true;
            this.bt_execover.Click += new System.EventHandler(this.bt_execover_Click);
            // 
            // bt_execPause
            // 
            this.bt_execPause.Location = new System.Drawing.Point(0, 0);
            this.bt_execPause.Name = "bt_execPause";
            this.bt_execPause.Size = new System.Drawing.Size(40, 23);
            this.bt_execPause.TabIndex = 14;
            this.bt_execPause.Text = "暂停";
            this.bt_execPause.UseVisualStyleBackColor = true;
            this.bt_execPause.Click += new System.EventHandler(this.bt_execPause_Click);
            // 
            // pauseAwhile
            // 
            this.pauseAwhile.AutoSize = true;
            this.pauseAwhile.Location = new System.Drawing.Point(100, 284);
            this.pauseAwhile.Name = "pauseAwhile";
            this.pauseAwhile.Size = new System.Drawing.Size(66, 16);
            this.pauseAwhile.TabIndex = 15;
            this.pauseAwhile.Text = "间隔1秒";
            this.pauseAwhile.UseVisualStyleBackColor = true;
            this.pauseAwhile.Visible = false;
            // 
            // bt_execOneByOne
            // 
            this.bt_execOneByOne.Location = new System.Drawing.Point(7, 278);
            this.bt_execOneByOne.Name = "bt_execOneByOne";
            this.bt_execOneByOne.Size = new System.Drawing.Size(87, 23);
            this.bt_execOneByOne.TabIndex = 11;
            this.bt_execOneByOne.Text = "逐个文件执行";
            this.bt_execOneByOne.UseVisualStyleBackColor = true;
            this.bt_execOneByOne.Click += new System.EventHandler(this.bt_execOneByOne_Click);
            // 
            // tb_currentSql
            // 
            this.tb_currentSql.Location = new System.Drawing.Point(7, 21);
            this.tb_currentSql.Multiline = true;
            this.tb_currentSql.Name = "tb_currentSql";
            this.tb_currentSql.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_currentSql.Size = new System.Drawing.Size(447, 250);
            this.tb_currentSql.TabIndex = 0;
            // 
            // bt_execute
            // 
            this.bt_execute.Location = new System.Drawing.Point(379, 277);
            this.bt_execute.Name = "bt_execute";
            this.bt_execute.Size = new System.Drawing.Size(75, 23);
            this.bt_execute.TabIndex = 10;
            this.bt_execute.Text = "执行当前";
            this.bt_execute.UseVisualStyleBackColor = true;
            this.bt_execute.Click += new System.EventHandler(this.bt_execute_Click);
            // 
            // SqlPanel
            // 
            this.SqlPanel.Controls.Add(this.SqlBox);
            this.SqlPanel.Location = new System.Drawing.Point(323, 65);
            this.SqlPanel.Name = "SqlPanel";
            this.SqlPanel.Size = new System.Drawing.Size(355, 453);
            this.SqlPanel.TabIndex = 7;
            this.SqlPanel.TabStop = false;
            this.SqlPanel.Text = "已选预览";
            // 
            // SqlBox
            // 
            this.SqlBox.Location = new System.Drawing.Point(6, 20);
            this.SqlBox.Name = "SqlBox";
            this.SqlBox.ReadOnly = true;
            this.SqlBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.SqlBox.Size = new System.Drawing.Size(342, 423);
            this.SqlBox.TabIndex = 12;
            this.SqlBox.Text = "";
            // 
            // MsgPanel
            // 
            this.MsgPanel.Controls.Add(this.MsgListBox);
            this.MsgPanel.Location = new System.Drawing.Point(684, 414);
            this.MsgPanel.Name = "MsgPanel";
            this.MsgPanel.Size = new System.Drawing.Size(461, 104);
            this.MsgPanel.TabIndex = 8;
            this.MsgPanel.TabStop = false;
            this.MsgPanel.Text = "消息";
            // 
            // MsgListBox
            // 
            this.MsgListBox.Location = new System.Drawing.Point(7, 21);
            this.MsgListBox.Multiline = true;
            this.MsgListBox.Name = "MsgListBox";
            this.MsgListBox.ReadOnly = true;
            this.MsgListBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MsgListBox.Size = new System.Drawing.Size(447, 73);
            this.MsgListBox.TabIndex = 0;
            // 
            // lb_dbstr
            // 
            this.lb_dbstr.AutoSize = true;
            this.lb_dbstr.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lb_dbstr.Location = new System.Drawing.Point(12, 3);
            this.lb_dbstr.Name = "lb_dbstr";
            this.lb_dbstr.Size = new System.Drawing.Size(0, 12);
            this.lb_dbstr.TabIndex = 9;
            // 
            // SqlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 532);
            this.Controls.Add(this.lb_dbstr);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.MsgPanel);
            this.Controls.Add(this.SqlPanel);
            this.Controls.Add(this.DirPanel);
            this.Controls.Add(this.bt_file);
            this.Controls.Add(this.bt_fd);
            this.Name = "SqlForm";
            this.Text = "EXECUTESQL";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DirPanel.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.playerPanel.ResumeLayout(false);
            this.SqlPanel.ResumeLayout(false);
            this.MsgPanel.ResumeLayout(false);
            this.MsgPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ExecuteSQL.Entity.CustomTreeView sql_tv;
        private System.Windows.Forms.Button bt_fd;
        private System.Windows.Forms.Button bt_file;
        private System.Windows.Forms.GroupBox DirPanel;
        private System.Windows.Forms.GroupBox SqlPanel;
        private System.Windows.Forms.GroupBox MsgPanel;
        private System.Windows.Forms.Button bt_execute;
        private System.Windows.Forms.RichTextBox SqlBox;
        private System.Windows.Forms.TextBox MsgListBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tb_currentSql;
        private System.Windows.Forms.Button bt_execGoOn;
        private System.Windows.Forms.Button bt_execOneByOne;
        private System.Windows.Forms.Button bt_execover;
        private System.Windows.Forms.Label lb_dbstr;
        private System.Windows.Forms.Button bt_execPause;
        private System.Windows.Forms.CheckBox pauseAwhile;
        private System.Windows.Forms.Panel playerPanel;
    }
}

