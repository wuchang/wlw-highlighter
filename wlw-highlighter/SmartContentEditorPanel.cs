﻿using System;
using System.Collections.Generic;
using System.Text;
using WindowsLive.Writer.Api;
using System.Windows.Forms;

namespace me.lizj.wlw_highlighter
{
    public class SmartContentEditorPanel : SmartContentEditor
    {
        public SmartContentEditorPanel()
        {
            this.InitializeComponent();

            this.Editor.OKButtonClick += new EventHandler(Editor_OKButtonClick);
        }

        void Editor_OKButtonClick(object sender, EventArgs e)
        {
            SelectedContent.Properties.SetString("code", this.Editor.ContentText);
            SelectedContent.Properties.SetString("class", this.Editor.CssClass);
            base.OnContentEdited();
        }

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnEdit;

        public CodeEditor Editor;
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnEdit = new System.Windows.Forms.Button();
            this.Editor = new  CodeEditor();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnEdit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 202);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(196, 28);
            this.panel1.TabIndex = 1;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(3, 2);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 0;
            this.btnEdit.Text = "&Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // Editor
            // 
            this.Editor.ContentText = "";
            this.Editor.CssClass = "brush:";
            this.Editor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Editor.Location = new System.Drawing.Point(0, 0);
            this.Editor.Name = "Editor";
            this.Editor.Size = new System.Drawing.Size(196, 202);
            this.Editor.TabIndex = 0;
            // 
            // SmartContentEditorPanel
            // 
            this.Controls.Add(this.Editor);
            this.Controls.Add(this.panel1);
            this.Name = "SmartContentEditorPanel";
            this.Size = new System.Drawing.Size(196, 230);
            this.Load += new System.EventHandler(this.SmartContentEditorPanel_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void SmartContentEditorPanel_Load(object sender, EventArgs e)
        {

        }
        protected override void OnSelectedContentChanged()
        {
            base.OnSelectedContentChanged();
            this.Editor.ContentText = this.SelectedContent.Properties["code"];
            this.Editor.CssClass = this.SelectedContent.Properties["class"];
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            using (var frm = new frmEdit())
            {
                frm.Editor.ContentText = this.Editor.ContentText;
                frm.Editor.CssClass = this.Editor.CssClass;

                DialogResult res = frm.ShowDialog();
                if (res == DialogResult.OK)
                {
                    SelectedContent.Properties.SetString("code", frm.Editor.ContentText);
                    SelectedContent.Properties.SetString("class", frm.Editor.CssClass);
                    base.OnContentEdited();
                    OnSelectedContentChanged();
                }
            }
        }
    }
}
