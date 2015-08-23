namespace ZxdFramework.DataContract
{
    /// <summary>
    /// 定义模型支持基本的数据层的操作. 比如增删改查<br/>
    /// 请谨慎使用当前接口. 如果某个模型实现当前接口. 那么这个模型就具备了基本的数据层的操作功能. 而这个功能会绕过Dao的实现. 
    /// 引起后期程序中的模型不从Dao层操作数据. <br />
    /// 一般说来. 如果一个模型有他自己的Dao, 就不应该再实现这个接口
    /// </summary>
    public interface IModelDao : IModel
    {
        
    }
}