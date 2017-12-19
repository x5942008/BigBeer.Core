using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BigBeer.Core.PaySample.LogicDispose
{
    /// <summary>
    /// A类型支付
    /// </summary>
    public class PayTypeA : PayBase
    {
        public override IPayResult OrderPay(string OrderNo)
        {
            return OrderPayAsync(OrderNo).Result;
        }
        /// <summary>
        /// 进行本地数据库逻辑修改编写
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async override Task<IPayResult> OrderPayAsync(string OrderNo)
        {
            //操作逻辑
            return  new PayResult(){
                 Status= PayResultStatus.Sucess,
                NotifyData = ("name","message","Url","OrderNo","PayType","paymethod")
            };
        }
    }
}
