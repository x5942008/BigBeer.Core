using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BigBeer.Core.PaySample
{
    public class PayServer : IPay
    {
        ///没有数据库所有暂时忽略在构造函数注入数据库
        PayOptions Options { get; set; }

        public PayServer(PayOptions options)
        {
            Options = options;
        }

        public IPayResult OrderPay(string OrderNo)
        {
            return OrderPayAsync(OrderNo).Result;
        }

        public Task<IPayResult> OrderPayAsync(string OrderNo)
        {
            //var order = Db.CapitalRecords.FirstOrDefault(t => t.No == orderNo);
            //await Db.Entry(order).ReloadAsync();
            if (order == null || order.Status != PayStatus.Success)
                throw new ArgumentNullException("orderno");
            if (!PaymentOptions.Subjects.ContainsKey(order.Type))
                throw new NullReferenceException("has no pay subject");
            var paymenttype = PaymentOptions.Subjects[order.Type];
            IPay payment = (IPay)Activator.CreateInstance(paymenttype, Db);
            if (payment == null)
                throw new NullReferenceException("payment is null");
            return await payment.PayOrderAsync(orderNo);
        }
    }
}
