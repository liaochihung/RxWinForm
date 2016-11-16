using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ReactiveUI;

namespace RxWinForm.ViewModels
{
    public class ViewModel1 : ReactiveObject
    {
        private readonly ObservableAsPropertyHelper<string> backgroundTicker;
        public string BackgroundTicker
        {
            get
            {
                return backgroundTicker.Value;
            }
            set {}
        }

        private readonly ObservableAsPropertyHelper<int> wordCount;
        public int WordCount
        {
            get
            {
                return wordCount.Value;
            }
            set { }
        }

        public ViewModel1(SynchronizationContext context, IObservable<string> input)
        {
            Observable.Interval(TimeSpan.FromSeconds(1))
                .ObserveOn(context)
                .Select(_ => DateTime.Now.ToLongTimeString())
                .ToProperty(this, x => x.BackgroundTicker, out backgroundTicker);

            input
                .Where(x => x != null)
                .Select(s => s.Split().Count(x => x.Trim().Length > 0))
                .Throttle(TimeSpan.FromSeconds(0.3))
                .ObserveOn(context)
                .ToProperty(this, x => x.WordCount, out wordCount);
        }
    }
}
