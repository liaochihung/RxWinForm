using System;
using System.Threading;
using System.Windows.Forms;
using ReactiveUI;
using RxWinForm.ViewModels;

namespace RxWinForm
{
    public partial class FormReactiveUI_2 : Form, IViewFor<ViewModel2>
    {
        public FormReactiveUI_2()
        {
            InitializeComponent();

            var context = SynchronizationContext.Current;
            VM = new ViewModel2(
                context,
                this.WhenAnyValue(x => x.textBox1.Text),
                this.WhenAnyValue(x => x.textBox2.Text),
                this.WhenAnyValue(x => x.progressBar1.Value));

            this.Bind(VM, x => x.UserName, x => x.textBox1.Text);
            this.Bind(VM, x => x.Password, x => x.textBox2.Text);
            //this.Bind(VM, x => x.Progress, x => x.progressBar1.Value);

            this.BindCommand(VM, x => x.CmdProcessLoginAsync, x => x.btnLogin);

            // extra bind
            //VM.CanUserLogin.BindTo(this, x => x.btnLogin.Visible);

            /*
             // Invoke a command whenever the Escape key is pressed
this.Events().KeyUpObs
    .Where(x => x.EventArgs.Key == Key.Escape)
    .InvokeCommand(this, x => x.ViewModel.Cancel);

// Subscribe to Cancel, and close the Window when it happens
this.WhenAnyObservable(x => x.ViewModel.Cancel)
    .Subscribe(_ => this.Close());
             */
            //this.Events()
            //this.WhenAnyObservable(x => x.VM.CmdCancel)
            //    .Subscribe(_ => this.Close());
        }

        public ViewModel2 VM { get; set; }
        object IViewFor.ViewModel
        {
            get { return VM; }
            set { VM = (ViewModel2)value; }
        }

        ViewModel2 IViewFor<ViewModel2>.ViewModel
        {
            get { return VM; }
            set { VM = value; }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("User logged.");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
