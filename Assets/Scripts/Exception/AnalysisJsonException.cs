/**
 *Description:   ר�Ÿ������Json����·������
 * ����Json��ʽ������ɵ��쳣���в���
 *History:
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UIFrameWork
{
    public class AnalysisJsonException : Exception
    {
        public AnalysisJsonException() : base() { }
        public AnalysisJsonException(string exceptionMsg) : base(exceptionMsg) { }
    }
}