
namespace SCC_Trainer
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
            this.posX = new System.Windows.Forms.NumericUpDown();
            this.posY = new System.Windows.Forms.NumericUpDown();
            this.posZ = new System.Windows.Forms.NumericUpDown();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.InitButtom = new System.Windows.Forms.Button();
            this.curMapLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.posX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.posY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.posZ)).BeginInit();
            this.SuspendLayout();
            // 
            // posX
            // 
            this.posX.DecimalPlaces = 4;
            this.posX.Location = new System.Drawing.Point(86, 99);
            this.posX.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.posX.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.posX.Name = "posX";
            this.posX.Size = new System.Drawing.Size(94, 23);
            this.posX.TabIndex = 1;
            this.posX.ValueChanged += new System.EventHandler(this.posX_ValueChanged);
            // 
            // posY
            // 
            this.posY.DecimalPlaces = 4;
            this.posY.Location = new System.Drawing.Point(244, 99);
            this.posY.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.posY.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.posY.Name = "posY";
            this.posY.Size = new System.Drawing.Size(94, 23);
            this.posY.TabIndex = 2;
            this.posY.ValueChanged += new System.EventHandler(this.posY_ValueChanged);
            // 
            // posZ
            // 
            this.posZ.DecimalPlaces = 4;
            this.posZ.Location = new System.Drawing.Point(403, 99);
            this.posZ.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.posZ.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.posZ.Name = "posZ";
            this.posZ.Size = new System.Drawing.Size(94, 23);
            this.posZ.TabIndex = 3;
            this.posZ.ValueChanged += new System.EventHandler(this.posZ_ValueChanged);
            // 
            // InitButtom
            // 
            this.InitButtom.Location = new System.Drawing.Point(244, 205);
            this.InitButtom.Name = "InitButtom";
            this.InitButtom.Size = new System.Drawing.Size(101, 28);
            this.InitButtom.TabIndex = 4;
            this.InitButtom.Text = "Initialize data";
            this.InitButtom.UseVisualStyleBackColor = true;
            this.InitButtom.Click += new System.EventHandler(this.InitButtom_Click);
            // 
            // curMapLabel
            // 
            this.curMapLabel.AutoSize = true;
            this.curMapLabel.Location = new System.Drawing.Point(12, 9);
            this.curMapLabel.Name = "curMapLabel";
            this.curMapLabel.Size = new System.Drawing.Size(86, 15);
            this.curMapLabel.TabIndex = 5;
            this.curMapLabel.Text = "Current Map: x";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 245);
            this.Controls.Add(this.curMapLabel);
            this.Controls.Add(this.InitButtom);
            this.Controls.Add(this.posZ);
            this.Controls.Add(this.posY);
            this.Controls.Add(this.posX);
            this.Name = "Form1";
            this.Text = "SCC Trainer";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.posX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.posY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.posZ)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NumericUpDown posX;
        private System.Windows.Forms.NumericUpDown posY;
        private System.Windows.Forms.NumericUpDown posZ;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button InitButtom;
        private System.Windows.Forms.Label curMapLabel;
    }
}

