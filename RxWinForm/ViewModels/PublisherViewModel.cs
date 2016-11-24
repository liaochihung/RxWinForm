using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace RxWinForm.ViewModels
{
    public class PublisherViewModel : ReactiveObject
    {
        public ReactiveCommand PublishCommand { get; protected set; }

        public PublisherViewModel()
        {
            var obs = Observable.Start(() =>
            {
                Trace.WriteLine("publisher's observable");
                return "test";
            });

            PublishCommand = ReactiveCommand.Create(()=>obs);

            //var cmd = new ReactiveCommand();
            //MessageBus.Current.RegisterMessageSource(PublishCommand.WhenAny().Select());
        }
    }
}
