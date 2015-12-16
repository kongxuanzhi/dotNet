using RecruCol.CommonHelp.DataConver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace RecruCol.Admin.paraClass
{
    public class paraCheckArt
    {
        public int  id { get; set; }  ////不参与搜索的可以设为字段，这样t.GetProperties()只能得到属性
        public string title  { get; set; }
        public string pubTime  { get; set; }
        public string author   { get; set; }
        public string src      { get; set; }
        public string article   { get; set; }
        public string type     { get; set; }
        public string bigType  { get; set; }
        public string isLegal  { get; set; }
        public string isDelete { get; set; }
    }
}