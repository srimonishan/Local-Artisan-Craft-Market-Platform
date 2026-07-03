namespace Craft_Market_Platform.Forms
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.lblLogoSub = new System.Windows.Forms.Label();
            this.lblLogo = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.chkShowPassword = new System.Windows.Forms.CheckBox();
            this.lblError = new System.Windows.Forms.Label();
            this.pnlLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.lblLogoSub);
            this.pnlLeft.Controls.Add(this.lblLogo);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(300, 450);
            this.pnlLeft.TabIndex = 0;
            // 
            // lblLogoSub
            // 
            this.lblLogoSub.AutoSize = true;
            this.lblLogoSub.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogoSub.ForeColor = System.Drawing.Color.White;
            this.lblLogoSub.Location = new System.Drawing.Point(90, 215);
            this.lblLogoSub.Name = "lblLogoSub";
            this.lblLogoSub.Size = new System.Drawing.Size(126, 28);
            this.lblLogoSub.TabIndex = 2;
            this.lblLogoSub.Text = "Admin Portal";
            this.lblLogoSub.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLogo
            // 
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogo.ForeColor = System.Drawing.Color.White;
            this.lblLogo.Location = new System.Drawing.Point(12, 137);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(276, 78);
            this.lblLogo.TabIndex = 1;
            this.lblLogo.Text = "Local Artisan && Craft Market Platform";
            this.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(344, 46);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(248, 46);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Admin Sign In";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.lblUsername.Location = new System.Drawing.Point(347, 126);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(97, 25);
            this.lblUsername.TabIndex = 2;
            this.lblUsername.Text = "Username";
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Location = new System.Drawing.Point(352, 154);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(380, 39);
            this.txtUsername.TabIndex = 3;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.lblPassword.Location = new System.Drawing.Point(347, 212);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(91, 25);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(351, 235);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(380, 39);
            this.txtPassword.TabIndex = 5;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.Location = new System.Drawing.Point(351, 331);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(180, 45);
            this.btnLogin.TabIndex = 7;
            this.btnLogin.Text = "LOG IN";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(551, 331);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(180, 45);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "CLEAR";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // chkShowPassword
            // 
            this.chkShowPassword.AutoSize = true;
            this.chkShowPassword.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.chkShowPassword.Location = new System.Drawing.Point(351, 274);
            this.chkShowPassword.Name = "chkShowPassword";
            this.chkShowPassword.Size = new System.Drawing.Size(148, 27);
            this.chkShowPassword.TabIndex = 6;
            this.chkShowPassword.Text = "Show Password";
            this.chkShowPassword.UseVisualStyleBackColor = true;
            this.chkShowPassword.CheckedChanged += new System.EventHandler(this.chkShowPassword_CheckedChanged);
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.lblError.Location = new System.Drawing.Point(348, 100);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(47, 23);
            this.lblError.TabIndex = 9;
            this.lblError.Text = "Error";
            this.lblError.Visible = false;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.chkShowPassword);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pnlLeft);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Admin Portal - Login";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Label lblLogoSub;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.CheckBox chkShowPassword;
        private System.Windows.Forms.Label lblError;
    }
}
