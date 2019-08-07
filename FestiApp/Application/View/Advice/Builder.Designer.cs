namespace FestiApp.View.Advice
{
    partial class Builder
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pnControls = new System.Windows.Forms.Panel();
            this.SuspendLayout();

            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);

            //
            // pnControls
            //
            this.pnControls.BackColor = System.Drawing.Color.White;
            this.pnControls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnControls.Name = "pnControls";
            this.pnControls.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnControls_MouseDown);
            this.pnControls.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnControls_MouseMove);
            this.pnControls.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnControls_MouseUp);

            this.Controls.Add(this.pnControls);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel pnControls;
    }
}
