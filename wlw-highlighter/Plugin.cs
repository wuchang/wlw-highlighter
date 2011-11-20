using System;
using System.Collections.Generic;
using System.Text;
using WindowsLive.Writer.Api;
using System.Windows.Forms;

namespace me.lizj.wlw_highlighter
{

    [WriterPluginAttribute("910B5D6B-F5A5-4108-B1A2-B97D2D405F5B",
        "Code Syntax Highlighter",
    ImagePath = "Code.bmp",
    PublisherUrl="http://blog.lizj.me",
        HasEditableOptions = true)]
    [InsertableContentSourceAttribute("Code")]
    public class SHCode : WindowsLive.Writer.Api.SmartContentSource
    {
        public override WindowsLive.Writer.Api.SmartContentEditor CreateEditor(WindowsLive.Writer.Api.ISmartContentEditorSite editorSite)
        {
            return new SmartContentEditorPanel();
        }


        public override string GeneratePublishHtml(ISmartContent content, IPublishingContext publishingContext)
        {
            return string.Format("<pre class=\"{1}\">{0}</pre>", content.Properties["code"], content.Properties["class"]);
        }

        public override System.Windows.Forms.DialogResult CreateContent(System.Windows.Forms.IWin32Window dialogOwner, ISmartContent newContent)
        {
            using (var form = new frmEdit())
            {
                if (Clipboard.ContainsText())
                {
                    form.Editor.ContentText = Clipboard.GetText();
                }
                DialogResult res = form.ShowDialog();
                if (res == DialogResult.OK)
                {
                    newContent.Properties.SetString("code", form.Editor.ContentText);
                    newContent.Properties.SetString("class", form.Editor.CssClass);
                }

                return res;
            }
        }


    }
}
