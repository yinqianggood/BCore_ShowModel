
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UIFrameWork
{
    public class LauguageMgr : MonoBehaviour
    {
        public static LauguageMgr instance;
        Dictionary<string, string> dicLauguageCache = new Dictionary<string, string>();

        void Awake()
        {
            instance = this;
            Init();
        }

        /// <summary>
        /// ��ʾ�ı���Ϣ
        /// </summary>
        /// <param name="lauguageID"></param>
        /// <returns></returns>
        public string ShowText(string lauguageID)
        {
            string strQueryResult=string.Empty;
            if (string.IsNullOrEmpty(lauguageID)) return null;
            //��ѯ����
            if (dicLauguageCache!=null&&dicLauguageCache.Count>=1)
            {
                dicLauguageCache.TryGetValue(lauguageID, out strQueryResult);
                if (!string.IsNullOrEmpty(strQueryResult))
                {
                    return strQueryResult;
                }
            }
            Debug.Log($"{GetType()}/ShowText()/Query is Null! Parameter lauguageID {lauguageID}");
            return null;
        }

        private void Init()
        {
            IConfigManager config = new ConfigManagerByJson("LauguageJSONConfig");
            dicLauguageCache = config?.AppSetting;
        }
    }
}