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
    public partial class FormMouseDraw : Form
    {
        public FormMouseDraw()
        {
            InitializeComponent();
        }

        private Point _startPoint;
        private IDisposable _showMouseDraggingPosition;
        private void WireUpEvents()
        {
            var mouseDown = Observable.FromEventPattern<MouseEventArgs>(this, "MouseDown")
                .Select(arg => arg.EventArgs.Location);

            var mouseMove = Observable.FromEventPattern<MouseEventArgs>(this, "MouseMove")
                .Select(arg => arg.EventArgs.Location);

            var mouseUp = Observable.FromEventPattern<MouseEventArgs>(this, "MouseUp")
                .Select(arg => arg.EventArgs.Location);

            var mouseDrag = mouseMove
                .SkipUntil(mouseDown)
                .TakeUntil(mouseUp)
                .Repeat();

            // remember the point when mouse down
            mouseDown.Subscribe(pos => _startPoint = pos);

            // show dragging position
            _showMouseDraggingPosition = mouseDrag.Subscribe(pos => label1.Text = pos.ToString());

            mouseDrag.Subscribe(currentPos =>
            {
                using (var g = this.CreateGraphics())
                {
                    if (_startPoint == currentPos)
                        return;

                    g.DrawLine(new Pen(Color.Green), _startPoint, currentPos);
                    _startPoint = currentPos;
                }
            });
        }

        private void FormMouseDraw_Load(object sender, EventArgs e)
        {
            WireUpEvents();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // unregister show dragging position
            _showMouseDraggingPosition.Dispose();
        }
    }
}
