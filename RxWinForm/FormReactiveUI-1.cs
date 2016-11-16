using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReactiveUI;
using RxWinForm.ViewModels;


// based on https://ledentsov.de/2014/12/29/no-events-reactiveui-windows-forms-mvvm/

namespace RxWinForm
{
    public partial class FormReactiveUI_1 : Form, IViewFor<ViewModel1>
    {
        public FormReactiveUI_1()
        {
            InitializeComponent();

            var context = SynchronizationContext.Current;
            VM = new ViewModel1(
                context,
                this.WhenAnyValue(x => x.textBox3.Text));

            this.Bind(VM, x => x.BackgroundTicker, x => x.textBox1.Text);
            this.Bind(VM, x => x.WordCount, x => x.textBox2.Text);
        }

        #region implement IViewFor<ViewModel1>
        public ViewModel1 VM { get; set; }
        object IViewFor.ViewModel
        {
            get { return VM; }
            set { VM = (ViewModel1)value; }
        }

        ViewModel1 IViewFor<ViewModel1>.ViewModel
        {
            get { return VM; }
            set { VM = value; }
        }
        #endregion

        private void FormReactiveUI_1_Load(object sender, EventArgs e)
        {

        }
    }
}
