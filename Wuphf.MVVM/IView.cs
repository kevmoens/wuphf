using System;
namespace Wuphf.MVVM
{
    public interface IView
    {
        object BindingContext { get; set; }
    }
}
