using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace BigBeer.UEditor.NetCore.Handlers
{
    public class ListFileManager: Handler
    {
        enum ResultState
        {
            [Display(Name = "成功")]
            Success,
            [Display(Name ="无效数据")]
            InvalidParam,
            [Display(Name = "授权错误")]
            AuthorizError,
            [Display(Name = "IO阻塞错误")]
            IOError,
            [Display(Name = "地址未找到")]
            PathNotFound
        }
        private int Start;   
        private int Size;
        private int Total;
        private ResultState State;
        private String PathToList;
        private String[] FileList;
        private String[] SearchExtensions;
        public ListFileManager(HttpContext context,string pathToList,string []searchExtensions) : base(context)
        {
            this.SearchExtensions = searchExtensions.Select(x=>x.ToLower()).ToArray();
            this.PathToList = pathToList;
        }

        public override void Process()
        {
            try
            {
                Start = String.IsNullOrEmpty(Request.Query["start"]) ? 0 : Convert.ToInt32(Request.Query["start"]);
                Size = String.IsNullOrEmpty(Request.Query["size"]) ? Config.GetInt("imageManagerListSize") : Convert.ToInt32(Request.Query["size"]);
            }
            catch (Exception)
            {
                State = ResultState.InvalidParam;
                WriteResult();
                return;
            }
            var buildingList = new List<String>();
            try
            {
                var localPath = Path.Combine(Config.WebRootPath, PathToList);
                buildingList.AddRange(Directory.GetFiles(localPath, "*", SearchOption.AllDirectories)
                    .Where(x => SearchExtensions.Contains(Path.GetExtension(x).ToLower()))
                    .Select(x => PathToList + x.Substring(localPath.Length).Replace("\\", "/")));
                Total = buildingList.Count;
                FileList = buildingList.OrderBy(x => x).Skip(Start).Take(Size).ToArray();
            }
            catch (UnauthorizedAccessException)
            {
                State = ResultState.AuthorizError;
            }
            catch (DirectoryNotFoundException)
            {
                State = ResultState.PathNotFound;
            }
            catch (IOException)
            {
                State = ResultState.IOError;
            }
            finally
            {
                WriteResult();
            }
        }
        private void WriteResult()
        {
            WriteJson(new
            {
                state = GetStateString(),
                list = FileList?.Select(x => new { url = x }),
                start = Start,
                size = Size,
                total = Total
            });
        }
        private string GetStateString()
        {
            switch (State)
            {
                case ResultState.Success:
                    return "SUCCESS";
                case ResultState.InvalidParam:
                    return "参数不正确";
                case ResultState.PathNotFound:
                    return "路径不存在";
                case ResultState.AuthorizError:
                    return "文件系统权限不足";
                case ResultState.IOError:
                    return "文件系统读取错误";
            }
            return "未知错误";
        }
    }
}
