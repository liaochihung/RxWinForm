using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace RxWinForm.ViewModels
{
    public class SubscriberViewModel
    {
        public SubscriberViewModel()
        {
            MessageBus.Current.Listen<int>().Subscribe(
                _ =>
                {
                    Value++;
                });
        }

        private readonly ObservableAsPropertyHelper<int> _value;

        public int Value
        {
            get { return _value.Value; }
            set { }
        }
    }
}
