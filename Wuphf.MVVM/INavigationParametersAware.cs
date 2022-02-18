using System;
using System.Collections.Generic;

namespace Wuphf.MVVM
{
    public interface INavigationParametersAware
    {
        void NavigatedTo(Dictionary<string, object> parameters);

    }
}
