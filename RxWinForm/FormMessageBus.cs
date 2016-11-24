using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RxWinForm
{
    public partial class FormMessageBus : Form
    {
        public FormMessageBus()
        {
            InitializeComponent();
        }

        private int _count = 0;

        private void FormMessageBus_Load(object sender, EventArgs e)
        {
            var click = Observable.FromEventPattern<EventArgs>(this.button1, "Click")
                   .Do(x => _count++)
                   .Do(x => (x.Sender as Button).Text = "ClickMessage :" + _count);

            ReactiveUI.MessageBus.Current.RegisterMessageSource(click);
        }
    }
}
