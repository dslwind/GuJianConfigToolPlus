using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LXCustomTools.Help
{
    public class InitializationFileConfig
    {
        #region dll
        /// <summary>
        /// 为INI文件中指定的节点取得字符串
        /// </summary>
        /// <param name="lpAppName">欲在其中查找关键字的节点名称</param>
        /// <param name="lpKeyName">欲获取的项名</param>
        /// <param name="lpDefault">指定的项没有找到时返回的默认值</param>
        /// <param name="lpReturnedString">指定一个字串缓冲区，长度至少为nSize</param>
        /// <param name="nSize">指定装载到lpReturnedString缓冲区的最大字符数量</param>
        /// <param name="lpFileName">INI文件完整路径</param>
        /// <returns>复制到lpReturnedString缓冲区的字节数量，其中不包括那些NULL中止字符</returns>
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);
        /// <summary>
        /// 修改INI文件中内容
        /// </summary>
        /// <param name="lpApplicationName">欲在其中写入的节点名称</param>
        /// <param name="lpKeyName">欲设置的项名</param>
        /// <param name="lpString">要写入的新字符串</param>
        /// <param name="lpFileName">INI文件完整路径</param>
        /// <returns>非零表示成功，零表示失败</returns>
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int WritePrivateProfileString(string lpApplicationName, string lpKeyName, string lpString, string lpFileName);

        #endregion
        
        #region 获取配置数据
        /// <summary>
        /// 读取INI文件值
        /// </summary>
        /// <param name="section">节点名</param>
        /// <param name="key">键</param>
        /// <param name="def">未取到值时返回的默认值</param>
        /// <param name="filePath">INI文件完整路径</param>
        /// <returns>读取的值</returns>
        public string Read(string filePath, string section, string key, string def = null)
        {
            StringBuilder sb = new StringBuilder(2048);
            GetPrivateProfileString(section, key, def, sb, 2048, filePath);
            return sb.ToString();
        }

        /// <summary>
        /// 读取INI文件值
        /// </summary>
        /// <param name="section">节点名</param>
        /// <param name="key">键</param>
        /// <param name="def">未取到值时返回的默认值</param>
        /// <returns>读取的值</returns>
        public string Read(string section, string key, string def = null)
        {
            if (string.IsNullOrEmpty(FileFullPath) || string.IsNullOrWhiteSpace(FileFullPath))
            {
                MessageBox.Show($"配置文件定位失败：{FileFullPath}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return def;
            }
            StringBuilder sb = new StringBuilder(2048);
            GetPrivateProfileString(section, key, def, sb, 2048, FileFullPath);
            return sb.ToString();
        }
        /// <summary>
        /// 读取布尔值
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public bool ReadBoolean(string section, string key, bool def = false)
        {
            string val = Read(section, key, def.ToString());
            int intVal;
            if (int.TryParse(val, out intVal))
            {
                return intVal > 0;
            }
            else
            {
                return val.Trim().ToLower() == "true";
            }
        }
        /// <summary>
        /// 读取整数
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public int ReadInt(string section, string key, int def = 0)
        {
            int value;
            if (int.TryParse(Read(section, key, def.ToString()), out value))
                return value;
            return def;
        }
        /// <summary>
        /// 读取单精度小数
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public float ReadFloat(string section, string key, float def = 0)
        {
            float value;
            if (float.TryParse(Read(section, key, def.ToString()), out value))
                return value;
            return def;
        }
        /// <summary>
        /// 读取双精度小数
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public double ReadDouble(string section, string key, double def = 0)
        {
            double value;
            if (double.TryParse(Read(section, key, def.ToString()), out value))
                return value;
            return def;
        }
        #endregion

        #region 写入配置数据
        /// <summary>
        /// 写INI文件值
        /// </summary>
        /// <param name="section">欲在其中写入的节点名称</param>
        /// <param name="key">欲设置的项名</param>
        /// <param name="value">要写入的新字符串</param>
        /// <param name="filePath">INI文件完整路径</param>
        /// <returns>非零表示成功，零表示失败</returns>
        public int Write(string filePath, string section, string key, string value)
        {
            return WritePrivateProfileString(section, key, value, filePath);
        }
        /// <summary>
        /// 写INI文件值
        /// </summary>
        /// <param name="section">欲在其中写入的节点名称</param>
        /// <param name="key">欲设置的项名</param>
        /// <param name="value">要写入的值</param>
        /// <param name="filePath">INI文件完整路径</param>
        /// <returns>非零表示成功，零表示失败</returns>
        public int Write(string filePath, string section, string key, int value)
        {
            return WritePrivateProfileString(section, key, value.ToString(), filePath);
        }
        /// <summary>
        /// 写INI文件值
        /// </summary>
        /// <param name="section">欲在其中写入的节点名称</param>
        /// <param name="key">欲设置的项名</param>
        /// <param name="value">要写入的值</param>
        /// <param name="filePath">INI文件完整路径</param>
        /// <returns>非零表示成功，零表示失败</returns>
        public int Write(string filePath, string section, string key, float value)
        {
            return WritePrivateProfileString(section, key, value.ToString(), filePath);
        }
        /// <summary>
        /// 写INI文件值
        /// </summary>
        /// <param name="section">欲在其中写入的节点名称</param>
        /// <param name="key">欲设置的项名</param>
        /// <param name="value">要写入的值</param>
        /// <param name="filePath">INI文件完整路径</param>
        /// <returns>非零表示成功，零表示失败</returns>
        public int Write(string filePath, string section, string key, double value)
        {
            return WritePrivateProfileString(section, key, value.ToString(), filePath);
        }
        /// <summary>
        /// 写INI文件值
        /// </summary>
        /// <param name="section">欲在其中写入的节点名称</param>
        /// <param name="key">欲设置的项名</param>
        /// <param name="value">要写入的值</param>
        /// <param name="filePath">INI文件完整路径</param>
        /// <param name="pattern">是否以数值形式保存数值，默认为真：以数值形式保存</param>
        /// <returns>非零表示成功，零表示失败</returns>
        public int Write(string filePath, string section, string key, bool value, bool pattern = true)
        {
            if (pattern)
            {
                return WritePrivateProfileString(section, key, Convert.ToInt32(value).ToString(), filePath);
            }
            return WritePrivateProfileString(section, key, value.ToString(), filePath);
        }

        /// <summary>
        /// 写INI文件值
        /// </summary>
        /// <param name="section">欲在其中写入的节点名称</param>
        /// <param name="key">欲设置的项名</param>
        /// <param name="value">要写入的新字符串</param>
        /// <returns>非零表示成功，零表示失败</returns>
        public int Write(string section, string key, string value)
        {
            if (string.IsNullOrEmpty(FileFullPath) || string.IsNullOrWhiteSpace(FileFullPath))
            {
                MessageBox.Show($"配置文件定位失败：{FileFullPath}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            return WritePrivateProfileString(section, key, value, FileFullPath);
        }

        /// <summary>
        /// 写INI文件值
        /// </summary>
        /// <param name="section">欲在其中写入的节点名称</param>
        /// <param name="key">欲设置的项名</param>
        /// <param name="value">要写入的值</param>
        /// <returns>非零表示成功，零表示失败</returns>
        public int Write(string section, string key, int value)
        {
            if (string.IsNullOrEmpty(FileFullPath) || string.IsNullOrWhiteSpace(FileFullPath))
            {
                MessageBox.Show($"配置文件定位失败：{FileFullPath}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            return WritePrivateProfileString(section, key, value.ToString(), FileFullPath);
        }

        /// <summary>
        /// 写INI文件值
        /// </summary>
        /// <param name="section">欲在其中写入的节点名称</param>
        /// <param name="key">欲设置的项名</param>
        /// <param name="value">要写入的值</param>
        /// <returns>非零表示成功，零表示失败</returns>
        public int Write(string section, string key, float value)
        {
            if (string.IsNullOrEmpty(FileFullPath) || string.IsNullOrWhiteSpace(FileFullPath))
            {
                MessageBox.Show($"配置文件定位失败：{FileFullPath}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            return WritePrivateProfileString(section, key, value.ToString(), FileFullPath);
        }

        /// <summary>
        /// 写INI文件值
        /// </summary>
        /// <param name="section">欲在其中写入的节点名称</param>
        /// <param name="key">欲设置的项名</param>
        /// <param name="value">要写入的值</param>
        /// <returns>非零表示成功，零表示失败</returns>
        public int Write(string section, string key, double value)
        {
            if (string.IsNullOrEmpty(FileFullPath) || string.IsNullOrWhiteSpace(FileFullPath))
            {
                MessageBox.Show($"配置文件定位失败：{FileFullPath}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            return WritePrivateProfileString(section, key, value.ToString(), FileFullPath);
        }

        /// <summary>
        /// 写INI文件值
        /// </summary>
        /// <param name="section">欲在其中写入的节点名称</param>
        /// <param name="key">欲设置的项名</param>
        /// <param name="value">要写入的值</param>
        /// <param name="pattern">是否以数值形式保存数值，默认为真：以数值形式保存</param>
        /// <returns>非零表示成功，零表示失败</returns>
        public int Write(string section, string key, bool value,bool pattern = true)
        {
            if (string.IsNullOrEmpty(FileFullPath) || string.IsNullOrWhiteSpace(FileFullPath))
            {
                MessageBox.Show($"配置文件定位失败：{FileFullPath}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            if (pattern)
            {
                return WritePrivateProfileString(section, key, Convert.ToInt32(value).ToString(), FileFullPath);
            }
            return WritePrivateProfileString(section, key, value.ToString(), FileFullPath);
        }
        #endregion

        #region 删除配置数据
        /// <summary>
        /// 删除节
        /// </summary>
        /// <param name="section">节点名</param>
        /// <param name="filePath">INI文件完整路径</param>
        /// <returns>非零表示成功，零表示失败</returns>
        public int DeleteSection(string filePath, string section)
        {
            return Write(filePath, section, null, null);
        }

        /// <summary>
        /// 删除节
        /// </summary>
        /// <param name="section">节点名</param>
        /// <returns>非零表示成功，零表示失败</returns>
        public int DeleteSection(string section)
        {
            if (string.IsNullOrEmpty(FileFullPath) || string.IsNullOrWhiteSpace(FileFullPath))
            {
                MessageBox.Show($"配置文件定位失败：{FileFullPath}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            return Write(FileFullPath, section, null, null);
        }

        /// <summary>
        /// 删除键的值
        /// </summary>
        /// <param name="section">节点名</param>
        /// <param name="key">键名</param>
        /// <param name="filePath">INI文件完整路径</param>
        /// <returns>非零表示成功，零表示失败</returns>
        public int DeleteKey(string filePath, string section, string key)
        {
            return Write(filePath, section, key, null);
        }

        /// <summary>
        /// 删除键的值
        /// </summary>
        /// <param name="section">节点名</param>
        /// <param name="key">键名</param>
        /// <returns>非零表示成功，零表示失败</returns>
        public int DeleteKey(string section, string key)
        {
            if (string.IsNullOrEmpty(FileFullPath) || string.IsNullOrWhiteSpace(FileFullPath))
            {
                MessageBox.Show($"配置文件定位失败：{FileFullPath}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            return Write(FileFullPath, section, key, null);
        }
        #endregion

        private string _path;
        /// <summary>
        /// 文件完整路径
        /// </summary>
        public string FileFullPath
        {
            get { return _path; }
            set { _path = value; }
        }

        public InitializationFileConfig(string path)
        {
            FileFullPath = path;
        }

        public InitializationFileConfig()
        {

        }
    }

}
