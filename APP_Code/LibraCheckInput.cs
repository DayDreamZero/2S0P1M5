using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 
/// </summary>
public class LibraCheckInput
{
    public LibraCheckInput()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    public static string transApostrophe(string str)
    {
        string strTrim = str.Trim();
        int len = strTrim.Length;
        string ansStr = "";
        for(int i = 0; i < len; i++)
        {
            if (strTrim[i] == '\'')
                ansStr += "''";
            else
                ansStr += strTrim[i];
        }
        return ansStr;
    }

    /*
    public static string transInLike(string str)
    {
        string strTrim = str.Trim();
        int len = strTrim.Length;
        string ansStr = "";
        for (int i = 0; i < len; i++)
        {
            if (strTrim[i] == '\'')
                ansStr += "''";
            else if(strTrim[i]=='%')
                ansStr += "[%]";
            else if (strTrim[i] == '_')
                ansStr += "[_]";
            else if (strTrim[i] == '[')
                ansStr += "[[]";
            else if (strTrim[i] == ']')
                ansStr += "[]]";
            else
                ansStr += strTrim[i];
        }
        return ansStr;
    }*/

    public static bool IsNum(string str)
    {
        string strTrim = str.Trim();
        int len = strTrim.Length;
        for (int i = 0; i < len; i++)
        {
            if (strTrim[i] < '0' || strTrim[i] > '9')
                return false;
        }
        return true;
    }
}