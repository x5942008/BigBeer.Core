using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BigBeer.Framework.Mvc.ImageService
{
    /// <summary>
    /// 注册图片服务
    /// </summary>
    public static class ImageServiceExtensions
    {
        /// <summary>
        /// 注册图片服务
        /// 上传格式为:{images:['base64','base64','base64',..]}
        /// 返回:["","",""]
        /// 默认上传路径:/image/upload
        /// 默认显示路径:/image/display
        /// 缩略图参数:?p=300x300 或者 ?w=300&h=300. 前者必须是格式00x00格式,后者 w,h 可以任意传一个.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="authServerOptions"></param>
        /// <returns>[{o:"",a:""},{o:"",a:""}]</returns>
        public static IApplicationBuilder UseImageService(this IApplicationBuilder @this, ImageServiceOptions authServerOptions)
        {
            return @this.Map(new PathString(authServerOptions.Uploadpath), (builder) =>
            {
                builder.UseMiddleware<ImageServiceUploadMiddleware>(authServerOptions);
            }).Map(new PathString(authServerOptions.Displaypath), (builder) =>
            {
                builder.UseMiddleware<ImageServiceDisplayMiddleware>(authServerOptions);
            });
        }
    }
}
