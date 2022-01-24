
namespace Moonwalk_Simulator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.main = new System.Windows.Forms.Timer(this.components);
            this.moonwalk = new System.Windows.Forms.Timer(this.components);
            this.refresh = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // main
            // 
            this.main.Enabled = true;
            this.main.Interval = 10;
            this.main.Tick += new System.EventHandler(this.main_Tick);
            // 
            // moonwalk
            // 
            this.moonwalk.Enabled = true;
            this.moonwalk.Interval = 50;
            this.moonwalk.Tick += new System.EventHandler(this.moowalk_Tick);
            // 
            // refresh
            // 
            this.refresh.Enabled = true;
            this.refresh.Interval = 5;
            this.refresh.Tick += new System.EventHandler(this.refresh_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer main;
        private System.Windows.Forms.Timer moonwalk;
        private System.Windows.Forms.Timer refresh;
    }
}

