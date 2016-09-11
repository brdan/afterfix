namespace POS.Controls
{
    partial class MsgGlobal
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
            this.components = new System.ComponentModel.Container();
            this.lblIcon = new System.Windows.Forms.Label();
            this.lblMsg = new System.Windows.Forms.Label();
            this.tmrAnimate = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lblIcon
            // 
            this.lblIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(32)))), ((int)(((byte)(50)))));
            this.lblIcon.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblIcon.Font = new System.Drawing.Font("Heydings Icons", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(52)))), ((int)(((byte)(70)))));
            this.lblIcon.Location = new System.Drawing.Point(0, 0);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(44, 49);
            this.lblIcon.TabIndex = 0;
            this.lblIcon.Text = "i";
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblIcon.Click += new System.EventHandler(this.click);
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMsg.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMsg.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblMsg.Location = new System.Drawing.Point(44, 0);
            this.lblMsg.Margin = new System.Windows.Forms.Padding(0);
            this.lblMsg.MaximumSize = new System.Drawing.Size(244, 0);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Padding = new System.Windows.Forms.Padding(5, 15, 0, 15);
            this.lblMsg.Size = new System.Drawing.Size(89, 49);
            this.lblMsg.TabIndex = 1;
            this.lblMsg.Text = "Successfully!";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblMsg.Click += new System.EventHandler(this.click);
            // 
            // tmrAnimate
            // 
            this.tmrAnimate.Interval = 1;
            this.tmrAnimate.Tick += new System.EventHandler(this.tmrAnimate_Tick);
            // 
            // MsgGlobal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(52)))), ((int)(((byte)(70)))));
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.lblIcon);
            this.Location = new System.Drawing.Point(736, 583);
            this.MaximumSize = new System.Drawing.Size(288, 0);
            this.MinimumSize = new System.Drawing.Size(288, 41);
            this.Name = "MsgGlobal";
            this.Size = new System.Drawing.Size(288, 49);
            this.Load += new System.EventHandler(this.MsgGlobal_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Timer tmrAnimate;
    }
}
