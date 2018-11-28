using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExecuteSQL.Server.Enum;
using System.Windows.Forms;
using System.Threading;

namespace ExecuteSQL.Server
{
    class SQLExecutePlayer
    {
        /// <summary>
        /// 状态
        /// </summary>
        private PlayerState state;
        /// <summary>
        /// 当前执行索引
        /// </summary>
        private int currentIndex;
        /// <summary>
        /// sql字典
        /// </summary>
        private Dictionary<string, string> dicSQL;

        DBConnection connection;

        public SQLExecutePlayer()
        {
            state = PlayerState.prepare;
            currentIndex = 0;
            dicSQL = new Dictionary<string, string>();
            connection = new DBConnection();
        }

        /// <summary>
        /// 增加字典条目
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool AddDicItem(string key, string value)
        {
            if (state != PlayerState.prepare)
            {
                throw new Exception(string.Format("当前处于状态：{0}，不能执行此操作。", state));
            }
            if (dicSQL.ContainsKey(key))
            {
                return false;
            }
            dicSQL.Add(key, value);
            return true;
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool RemoveDicItem(string key)
        {
            if (state != PlayerState.prepare)
            {
                throw new Exception(string.Format("当前处于状态：{0}，不能执行此操作。", state));
            }
            return dicSQL.Remove(key);
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool RemoveDicItem(int index)
        {
            if (state != PlayerState.prepare)
            {
                throw new Exception(string.Format("当前处于状态：{0}，不能执行此操作。", state));
            }
            return dicSQL.Remove(dicSQL.ElementAt(index).Key);
        }

        /// <summary>
        /// 逐个执行
        /// </summary>
        /// <param name="tb"></param>
        /// <returns></returns>
        public bool Exec(TextBox tb = null)
        {
            if (state != PlayerState.prepare)
            {
                throw new Exception(string.Format("当前处于状态：{0}，不能执行此操作。", state));
            }
            state = PlayerState.working;
            DBConnection conn = null;
            try
            {
                conn = new DBConnection();
                var sql = string.Empty;
                for (int i = currentIndex; i < dicSQL.Count; i++)
                {
                    Thread.Sleep(500);
                    sql = dicSQL.ElementAt(i).Value;
                    tb.Text = sql;
                    tb.Refresh();
                    if (state != PlayerState.working)
                    {
                        return false;            
                    }
                    if (!string.IsNullOrEmpty(sql))
                    {
                        conn.ExecuteNonQueryDb(sql);
                    }
                }
            }
            catch(Exception ex)
            {
                state = PlayerState.error;
                if (conn != null)
                {
                    conn.close();
                }
                throw ex;
            }
            state = PlayerState.prepare;
            return true;
        }

        /// <summary>
        /// 暂停
        /// </summary>
        /// <returns></returns>
        public bool PauseExec()
        {
            //处于执行中才暂停
            if (state != PlayerState.working)
            {
                throw new Exception(string.Format("当前处于状态：{0}，不能执行此操作。", state));
            }
            state = PlayerState.pause;
            return true;
        }

        /// <summary>
        /// 继续
        /// </summary>
        /// <returns></returns>
        public bool ContinueExec(TextBox tb = null)
        {   
            if (state != PlayerState.pause && state != PlayerState.error)
            {
                throw new Exception(string.Format("当前处于状态：{0}，不能执行此操作。", state));
            }
            DBConnection conn = null;
            try
            {
                conn = new DBConnection();
                state = PlayerState.working;
                if (tb != null && !string.IsNullOrEmpty(tb.Text))
                {
                    new DBConnection().ExecuteNonQueryDb(tb.Text);
                }

                currentIndex++;

                //DBConnection conn = new DBConnection();
                var sql = string.Empty;
                for (int i = currentIndex; i < dicSQL.Count; i++)
                {
                    sql = dicSQL.ElementAt(i).Value;
                    tb.Text = sql;
                    tb.Refresh();
                    if (state != PlayerState.working)
                    {
                        return false;
                    }
                    if (!string.IsNullOrEmpty(sql))
                    {
                        conn.ExecuteNonQueryDb(sql);
                    }
                }

            }
            catch(Exception ex)
            {
                state = PlayerState.error;
                if (conn != null)
                {
                    conn.close();
                }
                throw ex;
            }
            state = PlayerState.prepare;
            return true;

        }

        /// <summary>
        /// 结束执行
        /// </summary>
        /// <returns></returns>
        public bool OverExec()
        {
            //不在准备状态可以终止
            if (state == PlayerState.prepare)
            {
                throw new Exception(string.Format("当前处于状态：{0}，不能执行此操作。", state));
            }
            //reset();
            state = PlayerState.prepare;
            currentIndex = 0;
            return true;
        }

        public Dictionary<string, string> GetDicSQL()
        {
            return dicSQL;
        }

        /// <summary>
        /// 重置
        /// </summary>
        private void reset()
        {
            state = PlayerState.prepare;
            currentIndex = 0;
            dicSQL = new Dictionary<string, string>();
        }


        public PlayerState GetState()
        {
            return state;
        }
    }

    
}
