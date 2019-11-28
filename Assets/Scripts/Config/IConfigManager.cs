/**
/ *Description:   ͨ�����ù������ӿ�
 * ����"��ֵ��"�����ļ�ͨ�ý���
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
        /// ֻ������,Ӧ������
        /// �õ���ֵ�Լ�������
        /// </summary>
        Dictionary<string, string> AppSetting { get; }

        /// <summary>
        /// �õ������ļ�(AppSetting)�������
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