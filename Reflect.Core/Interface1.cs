using System;
using System.Collections.Generic;
using System.Text;

namespace Reflect.Core
{
    #region 抽象

    /// <summary>
    /// 接口  主要干哪些事，不需要实现
    /// </summary>
    public interface Animal
    {
        void Jiao();
    }
    /// <summary>
    /// 抽象接口
    /// </summary>
    public abstract class DogBase : Animal
    {
        public abstract void Jiao();
    }

    public class DogA : DogBase
    {
        public override void Jiao()
        {
            Console.WriteLine("A狗叫");
        }
    }
    public class DogB : DogBase
    {
        public override void Jiao()
        {
            Console.WriteLine("B狗叫");
        }
    }
    #endregion
    #region vitual
    /// <summary>
    /// 抽象接口
    /// </summary>
    public class CatBase
    {
        public virtual void Jiao()
        {
            Console.WriteLine("猫叫");
        }
    }

    /// <summary>
    /// 继承接口重写实现
    /// </summary>
    public class CatA : CatBase
    {
    }
    /// <summary>
    /// 同上
    /// </summary>
    public class CatB : CatBase
    {
        public override void Jiao()
        {
            Console.WriteLine("这只猫不一定会叫");
        }
    }
    #endregion
}
