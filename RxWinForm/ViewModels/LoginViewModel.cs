using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace RxWinForm.ViewModels
{
    public class LoginViewModel : ReactiveObject
    {
        private readonly ReactiveCommand<Unit, Unit> _loginCommand;
        private readonly ReactiveCommand<Unit, Unit> _resetCommand;
        private string _userName;
        private string _password;

        public LoginViewModel()
        {
            var canLogin = this.WhenAnyValue(
                x => x.UserName,
                x => x.Password,
                (userName, password) => !string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password));
            this._loginCommand = ReactiveCommand.CreateFromObservable(
                this.LoginAsync,
                canLogin);

            this._resetCommand = ReactiveCommand.Create(
                () =>
                {
                    this.UserName = null;
                    this.Password = null;
                });
        }

        public ReactiveCommand<Unit, Unit> LoginCommand => this._loginCommand;

        // note that if no client code requires the full API of the generic ReactiveCommand<TParam, TResult>,
        // we can just declare the type as ReactiveCommand
        public ReactiveCommand ResetCommand => this._resetCommand;

        public string UserName
        {
            get { return this._userName; }
            set { this.RaiseAndSetIfChanged(ref this._userName, value); }
        }

        public string Password
        {
            get { return this._password; }
            set { this.RaiseAndSetIfChanged(ref this._password, value); }
        }

        // here we simulate logins by randomly passing/failing
        private IObservable<Unit> LoginAsync() =>
            Observable
                .Return(new Random().Next(0, 2) == 1)
                .Delay(TimeSpan.FromSeconds(1))
                //.Do(
                    //() =>
                    //{
                    //    success =>
                    //    {
                    //        if (!success)
                    //        {
                    //            throw new InvalidOperationException("Failed to login.");
                    //        }
                    //    }
                    //})
                    .Select(_ => Unit.Default);
    }
}
