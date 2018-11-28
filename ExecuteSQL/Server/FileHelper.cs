using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExecuteSQL.Server
{
    class FileHelper
    {
        public static void GetFileName(string path, ref List<string> filenameList)
        {
            DirectoryInfo root = new DirectoryInfo(path);
            foreach (FileInfo f in root.GetFiles())
            {
                filenameList.Add(f.FullName);
            }
        }

        //获得指定路径下所有子目录名
        public static void GetDirectory(string path, ref List<string> filenameList)
        {
            GetFileName(path, ref filenameList);
            DirectoryInfo root = new DirectoryInfo(path);
            foreach (DirectoryInfo d in root.GetDirectories())
            {
                GetDirectory(d.FullName, ref filenameList);
            }
        }

        public static void Copy(string sourceFile, string destinationFile)
        {
            if (File.Exists(sourceFile))
            {
                File.Copy(sourceFile, destinationFile, true);
            }
        }

        /// <summary>
        /// 文件文件内容
        /// </summary>
        /// <param name="fn">路径</param>
        /// <param name="ec">编码</param>
        /// <returns></returns>
        public static string ReadFile(string fn, Encoding ec = null)
        {
            try
            {
                string content = string.Empty;
                Encoding encoding = ec == null ? Encoding.Default : ec;
                if (!File.Exists(fn))
                {
                    throw new Exception("文件不存在。");
                }
                StreamReader sr = new StreamReader(fn, encoding);
                content = sr.ReadToEnd();
                sr.Close();
                return content;
            }
            catch(Exception e)
            {   
                throw new Exception(string.Format("读取文件出错:{0}", e.Message));
            }

        }
    }
}
