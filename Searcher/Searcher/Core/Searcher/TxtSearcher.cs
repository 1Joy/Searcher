﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Searcher.Core.Searcher
{
    public class TxtSearcher : BaseSearcher
    {
        public TxtSearcher():base("txt", ".txt")
        {
            
        }
        protected override List<string> FilterFileSuffix(List<string> fileNames)
        {
            return fileNames.Where(name => Path.GetExtension(name) == ".txt").ToList();
        }

        protected override bool SearchByTargetStr(string targetStr, string fileFullPath)
        {
            if (base.SearchByTargetStr(targetStr, fileFullPath))
                return true;
            //读取文件内容
            ///todo 这里可能有编码问题
            using (FileStream fs = new FileStream(fileFullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using(StreamReader sr = new StreamReader(fs))
                {
                    string content;
                    while ((content = sr.ReadLine()) != null)
                    {
                        if (content.Contains(targetStr))
                            return true;
                    }
                }
            }
            
            return false;
        }
    }
}
