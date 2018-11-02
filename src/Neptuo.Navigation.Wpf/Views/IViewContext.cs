using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Navigation.Views
{
    public interface IViewContext<T>
    {
        void Close(T result);
    }
}
