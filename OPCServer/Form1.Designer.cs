namespace OPCServer
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Btn_Conn = new System.Windows.Forms.Button();
            this.Btn_DisConn = new System.Windows.Forms.Button();
            this.Btn_Read = new System.Windows.Forms.Button();
            this.Btn_Write = new System.Windows.Forms.Button();
            this.Txt_R1Value = new System.Windows.Forms.TextBox();
            this.Txt_R1Quality = new System.Windows.Forms.TextBox();
            this.Txt_R1TimeStamp = new System.Windows.Forms.TextBox();
            this.Txt_R2Value = new System.Windows.Forms.TextBox();
            this.Txt_R2Quality = new System.Windows.Forms.TextBox();
            this.Txt_R2TimeStamp = new System.Windows.Forms.TextBox();
            this.Txt_W1 = new System.Windows.Forms.TextBox();
            this.Txt_W2 = new System.Windows.Forms.TextBox();
            this.Txt_WriteStatus = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Btn_Conn
            // 
            this.Btn_Conn.Location = new System.Drawing.Point(41, 36);
            this.Btn_Conn.Name = "Btn_Conn";
            this.Btn_Conn.Size = new System.Drawing.Size(75, 23);
            this.Btn_Conn.TabIndex = 0;
            this.Btn_Conn.Text = "Conn";
            this.Btn_Conn.UseVisualStyleBackColor = true;
            this.Btn_Conn.Click += new System.EventHandler(this.Btn_Conn_Click);
            // 
            // Btn_DisConn
            // 
            this.Btn_DisConn.Location = new System.Drawing.Point(355, 35);
            this.Btn_DisConn.Name = "Btn_DisConn";
            this.Btn_DisConn.Size = new System.Drawing.Size(75, 23);
            this.Btn_DisConn.TabIndex = 1;
            this.Btn_DisConn.Text = "disConn";
            this.Btn_DisConn.UseVisualStyleBackColor = true;
            this.Btn_DisConn.Click += new System.EventHandler(this.Btn_DisConn_Click);
            // 
            // Btn_Read
            // 
            this.Btn_Read.Location = new System.Drawing.Point(41, 112);
            this.Btn_Read.Name = "Btn_Read";
            this.Btn_Read.Size = new System.Drawing.Size(75, 23);
            this.Btn_Read.TabIndex = 2;
            this.Btn_Read.Text = "Read";
            this.Btn_Read.UseVisualStyleBackColor = true;
            this.Btn_Read.Click += new System.EventHandler(this.Btn_Read_Click);
            // 
            // Btn_Write
            // 
            this.Btn_Write.Location = new System.Drawing.Point(41, 191);
            this.Btn_Write.Name = "Btn_Write";
            this.Btn_Write.Size = new System.Drawing.Size(75, 23);
            this.Btn_Write.TabIndex = 3;
            this.Btn_Write.Text = "Write";
            this.Btn_Write.UseVisualStyleBackColor = true;
            this.Btn_Write.Click += new System.EventHandler(this.Btn_Write_Click);
            // 
            // Txt_R1Value
            // 
            this.Txt_R1Value.Location = new System.Drawing.Point(157, 137);
            this.Txt_R1Value.Name = "Txt_R1Value";
            this.Txt_R1Value.Size = new System.Drawing.Size(100, 21);
            this.Txt_R1Value.TabIndex = 4;
            // 
            // Txt_R1Quality
            // 
            this.Txt_R1Quality.Location = new System.Drawing.Point(305, 136);
            this.Txt_R1Quality.Name = "Txt_R1Quality";
            this.Txt_R1Quality.Size = new System.Drawing.Size(100, 21);
            this.Txt_R1Quality.TabIndex = 5;
            // 
            // Txt_R1TimeStamp
            // 
            this.Txt_R1TimeStamp.Location = new System.Drawing.Point(443, 136);
            this.Txt_R1TimeStamp.Name = "Txt_R1TimeStamp";
            this.Txt_R1TimeStamp.Size = new System.Drawing.Size(100, 21);
            this.Txt_R1TimeStamp.TabIndex = 6;
            // 
            // Txt_R2Value
            // 
            this.Txt_R2Value.Location = new System.Drawing.Point(157, 165);
            this.Txt_R2Value.Name = "Txt_R2Value";
            this.Txt_R2Value.Size = new System.Drawing.Size(100, 21);
            this.Txt_R2Value.TabIndex = 7;
            // 
            // Txt_R2Quality
            // 
            this.Txt_R2Quality.Location = new System.Drawing.Point(305, 164);
            this.Txt_R2Quality.Name = "Txt_R2Quality";
            this.Txt_R2Quality.Size = new System.Drawing.Size(100, 21);
            this.Txt_R2Quality.TabIndex = 8;
            // 
            // Txt_R2TimeStamp
            // 
            this.Txt_R2TimeStamp.Location = new System.Drawing.Point(443, 164);
            this.Txt_R2TimeStamp.Name = "Txt_R2TimeStamp";
            this.Txt_R2TimeStamp.Size = new System.Drawing.Size(100, 21);
            this.Txt_R2TimeStamp.TabIndex = 9;
            // 
            // Txt_W1
            // 
            this.Txt_W1.Location = new System.Drawing.Point(157, 227);
            this.Txt_W1.Name = "Txt_W1";
            this.Txt_W1.Size = new System.Drawing.Size(100, 21);
            this.Txt_W1.TabIndex = 10;
            // 
            // Txt_W2
            // 
            this.Txt_W2.Location = new System.Drawing.Point(157, 255);
            this.Txt_W2.Name = "Txt_W2";
            this.Txt_W2.Size = new System.Drawing.Size(100, 21);
            this.Txt_W2.TabIndex = 11;
            // 
            // Txt_WriteStatus
            // 
            this.Txt_WriteStatus.Location = new System.Drawing.Point(305, 255);
            this.Txt_WriteStatus.Name = "Txt_WriteStatus";
            this.Txt_WriteStatus.Size = new System.Drawing.Size(238, 21);
            this.Txt_WriteStatus.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 335);
            this.Controls.Add(this.Txt_WriteStatus);
            this.Controls.Add(this.Txt_W2);
            this.Controls.Add(this.Txt_W1);
            this.Controls.Add(this.Txt_R2TimeStamp);
            this.Controls.Add(this.Txt_R2Quality);
            this.Controls.Add(this.Txt_R2Value);
            this.Controls.Add(this.Txt_R1TimeStamp);
            this.Controls.Add(this.Txt_R1Quality);
            this.Controls.Add(this.Txt_R1Value);
            this.Controls.Add(this.Btn_Write);
            this.Controls.Add(this.Btn_Read);
            this.Controls.Add(this.Btn_DisConn);
            this.Controls.Add(this.Btn_Conn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Btn_Conn;
        private System.Windows.Forms.Button Btn_DisConn;
        private System.Windows.Forms.Button Btn_Read;
        private System.Windows.Forms.Button Btn_Write;
        private System.Windows.Forms.TextBox Txt_R1Value;
        private System.Windows.Forms.TextBox Txt_R1Quality;
        private System.Windows.Forms.TextBox Txt_R1TimeStamp;
        private System.Windows.Forms.TextBox Txt_R2Value;
        private System.Windows.Forms.TextBox Txt_R2Quality;
        private System.Windows.Forms.TextBox Txt_R2TimeStamp;
        private System.Windows.Forms.TextBox Txt_W1;
        private System.Windows.Forms.TextBox Txt_W2;
        private System.Windows.Forms.TextBox Txt_WriteStatus;
    }
}

