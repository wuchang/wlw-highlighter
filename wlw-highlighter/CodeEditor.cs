using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace me.lizj.wlw_highlighter
{
    public partial class CodeEditor : UserControl
    {
        public CodeEditor()
        {
            InitializeComponent();

            //this.pnlOptions.Height = 0;

            //this.Options = new SHOptions();
        }

        public event EventHandler OKButtonClick;
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (OKButtonClick != null)
            {
                OKButtonClick(sender, e);
            }
        }

        public string ContentText
        {
            get { return HttpUtility.HtmlEncode(this.txtContent.Text); }
            set { this.txtContent.Text = HttpUtility.HtmlDecode(value); }
        }

        private SHOptions options;
        public SHOptions Options
        {
            get { return this.options; }
            set
            {
                if (value == null)
                {
                    return;
                }
                this.options = value;
                this.propertyGrid1.SelectedObject = this.options;
            }
        }

        public string CssClass
        {
            get
            {
                this.options.Brush = this.txtBrush.Text.Trim();
                return this.options.ToString();
            }
            set
            {
                if (this.DesignMode)
                {
                    return;
                }
                SHOptions options = SHOptions.Parse(value);
                this.txtBrush.Text = options.Brush;
                this.Options = options;
            }
        }

        private void btnShowOptions_Click(object sender, EventArgs e)
        {
            this.pnlOptions.Height = this.pnlOptions.Height == 0 ? 250 : 0;
        }
    }
}
