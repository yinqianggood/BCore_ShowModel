/**
/ *Description:   通用配置管理器接口
 * 基于"键值对"配置文件通用解析
 *History:
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UIFrameWork
{
    public interface IConfigManager
    {
        /// <summary>
        /// 只读属性,应用设置
        /// 得到键值对集合数据
        /// </summary>
        Dictionary<string, string> AppSetting { get; }

        /// <summary>
        /// 得到配置文件(AppSetting)最大数量
        /// </summary>
        /// <returns></returns>
        int GetAppSettingMaxNumber();
    }

    [Serializable]
    internal class KeyValueInfo
    {
        public List<KeyValueNode> ConfigInfo;
    }

    [Serializable]
    internal class KeyValueNode
    {
        public string Key;
        public string Value;
    }
}