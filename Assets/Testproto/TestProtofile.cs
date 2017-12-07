using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginRsp
{
    #region 属性
    public int result;
    public long UserID;
    public string OpenID;
    #endregion
    #region 定义
    #endregion
    #region 读取写入
    public void MergeFrom(byte[] _bytes)
    {
        Google.Protobuf.CodedInputStream input = new Google.Protobuf.CodedInputStream(_bytes);
        uint tag;
        while ((tag = input.ReadTag()) != 0)
        {
            switch (tag)
            {
                default:
                    input.SkipLastField();
                    break;
                case 8:
                    result = input.ReadInt32();
                    break;
                case 16:
                    UserID = input.ReadInt64();
                    break;
                case 26:
                    OpenID = input.ReadString();
                    break;
            }
        }
        input.Dispose();
    }
    public byte[] GetBytes()
    {
        System.IO.MemoryStream tmemsteam = new System.IO.MemoryStream();
        Google.Protobuf.CodedOutputStream output = new Google.Protobuf.CodedOutputStream(tmemsteam, 512, true);
        if (result != default(int))
        {
            output.WriteTag(8);
            output.WriteInt32(result);
        }
        if (UserID != default(long))
        {
            output.WriteTag(16);
            output.WriteInt64(UserID);
        }
        if (!string.IsNullOrEmpty(OpenID))
        {
            output.WriteTag(26);
            output.WriteString(OpenID);
        }
        output.Flush();
        byte[] ret = tmemsteam.ToArray();
        output.Dispose();
        tmemsteam.Dispose();
        return ret;
    }
    #endregion
}
