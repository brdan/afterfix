namespace POS
{
    partial class frmDashboard
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnNewOrder = new System.Windows.Forms.Button();
            this.btnLoyaltyAndMarketing = new System.Windows.Forms.Button();
            this.btnEcommerce = new System.Windows.Forms.Button();
            this.btnStockManagement = new System.Windows.Forms.Button();
            this.btnOrderHistory = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnQuickEdit = new System.Windows.Forms.Button();
            this.btnCustomers = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Light", 20F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(30, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 37);
            this.label1.TabIndex = 3;
            this.label1.Text = "Dashboard";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.flowLayoutPanel1.Controls.Add(this.btnNewOrder);
            this.flowLayoutPanel1.Controls.Add(this.btnLoyaltyAndMarketing);
            this.flowLayoutPanel1.Controls.Add(this.btnEcommerce);
            this.flowLayoutPanel1.Controls.Add(this.btnStockManagement);
            this.flowLayoutPanel1.Controls.Add(this.btnOrderHistory);
            this.flowLayoutPanel1.Controls.Add(this.btnSettings);
            this.flowLayoutPanel1.Controls.Add(this.btnQuickEdit);
            this.flowLayoutPanel1.Controls.Add(this.btnCustomers);
            this.flowLayoutPanel1.Controls.Add(this.btnLogout);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(37, 107);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(956, 631);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // btnNewOrder
            // 
            this.btnNewOrder.AccessibleName = "NewOrder";
            this.btnNewOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(52)))), ((int)(((byte)(70)))));
            this.btnNewOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewOrder.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnNewOrder.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnNewOrder.Location = new System.Drawing.Point(3, 3);
            this.btnNewOrder.Name = "btnNewOrder";
            this.btnNewOrder.Size = new System.Drawing.Size(312, 204);
            this.btnNewOrder.TabIndex = 0;
            this.btnNewOrder.Text = "NEW ORDER";
            this.btnNewOrder.UseVisualStyleBackColor = false;
            this.btnNewOrder.Click += new System.EventHandler(this.btns_Click);
            // 
            // btnLoyaltyAndMarketing
            // 
            this.btnLoyaltyAndMarketing.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(52)))), ((int)(((byte)(70)))));
            this.btnLoyaltyAndMarketing.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoyaltyAndMarketing.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnLoyaltyAndMarketing.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnLoyaltyAndMarketing.Location = new System.Drawing.Point(321, 3);
            this.btnLoyaltyAndMarketing.Name = "btnLoyaltyAndMarketing";
            this.btnLoyaltyAndMarketing.Size = new System.Drawing.Size(312, 204);
            this.btnLoyaltyAndMarketing.TabIndex = 1;
            this.btnLoyaltyAndMarketing.Text = "LOYALTY & MARKETING";
            this.btnLoyaltyAndMarketing.UseMnemonic = false;
            this.btnLoyaltyAndMarketing.UseVisualStyleBackColor = false;
            this.btnLoyaltyAndMarketing.Click += new System.EventHandler(this.btns_Click);
            // 
            // btnEcommerce
            // 
            this.btnEcommerce.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(52)))), ((int)(((byte)(70)))));
            this.btnEcommerce.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEcommerce.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnEcommerce.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnEcommerce.Location = new System.Drawing.Point(639, 3);
            this.btnEcommerce.Name = "btnEcommerce";
            this.btnEcommerce.Size = new System.Drawing.Size(312, 204);
            this.btnEcommerce.TabIndex = 2;
            this.btnEcommerce.Text = "E-COMMERCE";
            this.btnEcommerce.UseVisualStyleBackColor = false;
            this.btnEcommerce.Click += new System.EventHandler(this.btns_Click);
            // 
            // btnStockManagement
            // 
            this.btnStockManagement.AccessibleName = "StockManagement";
            this.btnStockManagement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(52)))), ((int)(((byte)(70)))));
            this.btnStockManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStockManagement.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnStockManagement.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnStockManagement.Location = new System.Drawing.Point(3, 213);
            this.btnStockManagement.Name = "btnStockManagement";
            this.btnStockManagement.Size = new System.Drawing.Size(312, 204);
            this.btnStockManagement.TabIndex = 3;
            this.btnStockManagement.Text = "STOCK MANAGEMENT";
            this.btnStockManagement.UseVisualStyleBackColor = false;
            this.btnStockManagement.Click += new System.EventHandler(this.btns_Click);
            // 
            // btnOrderHistory
            // 
            this.btnOrderHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(52)))), ((int)(((byte)(70)))));
            this.btnOrderHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOrderHistory.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnOrderHistory.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnOrderHistory.Location = new System.Drawing.Point(321, 213);
            this.btnOrderHistory.Name = "btnOrderHistory";
            this.btnOrderHistory.Size = new System.Drawing.Size(312, 204);
            this.btnOrderHistory.TabIndex = 8;
            this.btnOrderHistory.Text = "ORDER HISTORY";
            this.btnOrderHistory.UseVisualStyleBackColor = false;
            this.btnOrderHistory.Click += new System.EventHandler(this.btns_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(52)))), ((int)(((byte)(70)))));
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnSettings.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnSettings.Location = new System.Drawing.Point(639, 213);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(312, 204);
            this.btnSettings.TabIndex = 4;
            this.btnSettings.Text = "SETTINGS";
            this.btnSettings.UseVisualStyleBackColor = false;
            this.btnSettings.Click += new System.EventHandler(this.btns_Click);
            // 
            // btnQuickEdit
            // 
            this.btnQuickEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(52)))), ((int)(((byte)(70)))));
            this.btnQuickEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuickEdit.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnQuickEdit.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnQuickEdit.Location = new System.Drawing.Point(3, 423);
            this.btnQuickEdit.Name = "btnQuickEdit";
            this.btnQuickEdit.Size = new System.Drawing.Size(312, 204);
            this.btnQuickEdit.TabIndex = 5;
            this.btnQuickEdit.Text = "QUICK EDIT";
            this.btnQuickEdit.UseVisualStyleBackColor = false;
            this.btnQuickEdit.Click += new System.EventHandler(this.btns_Click);
            // 
            // btnCustomers
            // 
            this.btnCustomers.AccessibleName = "Customers";
            this.btnCustomers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(52)))), ((int)(((byte)(70)))));
            this.btnCustomers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCustomers.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnCustomers.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnCustomers.Location = new System.Drawing.Point(321, 423);
            this.btnCustomers.Name = "btnCustomers";
            this.btnCustomers.Size = new System.Drawing.Size(312, 204);
            this.btnCustomers.TabIndex = 7;
            this.btnCustomers.Text = "CUSTOMERS";
            this.btnCustomers.UseVisualStyleBackColor = false;
            this.btnCustomers.Click += new System.EventHandler(this.btns_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(52)))), ((int)(((byte)(70)))));
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnLogout.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnLogout.Location = new System.Drawing.Point(639, 423);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(312, 204);
            this.btnLogout.TabIndex = 9;
            this.btnLogout.Text = "LOGOUT";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btns_Click);
            // 
            // frmDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "s";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnNewOrder;
        private System.Windows.Forms.Button btnLoyaltyAndMarketing;
        private System.Windows.Forms.Button btnEcommerce;
        private System.Windows.Forms.Button btnStockManagement;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnQuickEdit;
        private System.Windows.Forms.Button btnCustomers;
        private System.Windows.Forms.Button btnOrderHistory;
        private System.Windows.Forms.Button btnLogout;
    }
}