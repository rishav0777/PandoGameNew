using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GenerateRtId : MonoBehaviour
{
    
    public string GetRtId()
    {
        string RtId = SystemInfo.deviceUniqueIdentifier;// + SystemInfo.deviceName+ SystemInfo.deviceType;//"a9cf6f73-e65e-487c-8fed-da1bf4ec1989";//+ SystemInfo.deviceModel +
        String rt="";print("rtid: "+RtId);
        for(int j = 0; j < RtId.Length; j++)
        {
            if (RtId[j] == ' ') continue;
            rt = rt + RtId[j];
        }
        return rt;
    }
}
