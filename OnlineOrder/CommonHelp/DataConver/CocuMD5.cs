using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Cocu
{
    //加密密码
   public static class CocuMD5
    {
       //加密字符串
       public static string StrToMD5(string str)
       {
           MD5 md5 = MD5.Create();//创建md5对象

           byte[] buffer=Encoding.Default.GetBytes(str);//转为字节数组

           byte[] md5Buffer= md5.ComputeHash(buffer);//转为MD5的字节数组

           StringBuilder sb = new StringBuilder();
           for (int i = 0; i < md5Buffer.Length; i++)
           {
              sb.Append(md5Buffer[i].ToString("x2"));//在原来的字符串上追加字符串
               //x2代表转为十六进制，保留两位
           }
           return sb.ToString();
       }
       //加密文件
       public static string FileToMD5(string path)
       {
           MD5 md5 = MD5.Create();
           using (FileStream file = File.OpenRead(path))
           {
               byte[] filebuffer = md5.ComputeHash(file);
                   md5.Clear();
               
               StringBuilder sb = new StringBuilder();
               for (int i = 0; i < filebuffer.Length; i++)
               {
                  sb.Append(filebuffer[i].ToString("x2"));//在原来的字符串上追加字符串
                   //x2代表转为十六进制，保留两位
               } 
                   return sb.ToString();
           }
              
       }
    }
}
