using System;
using System.Windows.Forms;

namespace FestiApp.View.Advice
{
    partial class EditableLabel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            label = new Label();
            textBox = new TextBox();
            this.SuspendLayout();

            label.Name = "label";
            label.MouseDoubleClick += LabelOnMouseDoubleClick;
            label.Dock = DockStyle.Fill;
            label.MouseDown += delegate (object sender, MouseEventArgs args) { this.OnMouseDown(args); };
            label.MouseMove += delegate (object sender, MouseEventArgs args) { this.OnMouseMove(args); };
            label.MouseUp += delegate (object sender, MouseEventArgs args) { this.OnMouseUp(args); };

            textBox.Name = "textbox";
            textBox.TextChanged += TextBoxOnTextChanged;
            textBox.Dock = DockStyle.Fill;
            textBox.Multiline = true;
            textBox.MaxLength = 500;
            textBox.LostFocus += delegate (object sender, EventArgs args) { this.EditMode = false; };

            this.Controls.Add(label);
            this.Controls.Add(textBox);
            this.AutoSize = true;
            this.ResumeLayout(false);
            this.PerformLayout();
        }


        #endregion

        private Label label;
        private TextBox textBox;
    }
}
