using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReactiveUI;
//using ReactiveUI.Legacy;
using ReactiveCommand = ReactiveUI.ReactiveCommand;

namespace RxWinForm.ViewModels
{
    public class ViewModel2 : ReactiveObject
    {
        readonly ObservableAsPropertyHelper<string> _userName;

        public string UserName
        {
            get { return _userName.Value; }
            set { }
        }

        readonly ObservableAsPropertyHelper<string> _password;

        public string Password
        {
            get { return _password.Value; }
            set { }
        }

        private readonly ObservableAsPropertyHelper<int> _progress;

        public int Progress
        {
            get { return _progress.Value; }
            set { }
        }

        public ReactiveCommand CmdLogin { get; private set; }
        public ReactiveCommand CmdProcessLoginAsync { get; protected set; }
        public ReactiveCommand CmdCancel { get; protected set; }

        public IObservable<bool> CanUserLogin { get; private set; }
        public ViewModel2(
            SynchronizationContext context, 
            IObservable<string> name, 
            IObservable<string> password,
            IObservable<int> progress)
        {
            name.ToProperty(this, x => x.UserName, out _userName);
            password.ToProperty(this, x => x.Password, out _password);
            progress.ToProperty(this, x => x.Progress, out _progress);

            CanUserLogin = this.WhenAnyValue(
                x => x.UserName, x => x.Password,
                (user, pass) =>
                    !string.IsNullOrWhiteSpace(user) &&
                    !string.IsNullOrWhiteSpace(pass) &&
                    user.Length >= 2 && pass.Length >= 3)
                .DistinctUntilChanged();

            CmdLogin = ReactiveCommand.Create(() => CanUserLogin, CanUserLogin);
                //.InvokeCommand(CmdProcessLoginAsync);

            CmdProcessLoginAsync = ReactiveCommand.CreateFromTask(() =>
            {
                return Task.Run(() =>
                {
                    Progress = 0;
                    while (Progress <= 100)
                    {
                        Progress += 10;
                        Thread.Sleep(100);
                    }
                });
            }, CanUserLogin);

            //CmdCancel = ReactiveCommand.CreateCombined();

        }
    }

    public struct AsyncVoid
    {
        public static readonly AsyncVoid Default = new AsyncVoid();
    }
}
