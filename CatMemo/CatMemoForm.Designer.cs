namespace CatMemo
{
    partial class CatMemoForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CatMemoForm));
            this.moveTimer = new System.Windows.Forms.Timer(this.components);
            this.catPicture = new System.Windows.Forms.PictureBox();
            this.fukidashiPicture = new System.Windows.Forms.PictureBox();
            this.fukidashiText = new System.Windows.Forms.TextBox();
            this.timerOnOffTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.catPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fukidashiPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // moveTimer
            // 
            this.moveTimer.Tick += new System.EventHandler(this.MoveTimer_Tick);
            // 
            // catPicture
            // 
            this.catPicture.BackColor = System.Drawing.Color.Transparent;
            this.catPicture.Image = ((System.Drawing.Image)(resources.GetObject("catPicture.Image")));
            this.catPicture.Location = new System.Drawing.Point(300, 83);
            this.catPicture.Name = "catPicture";
            this.catPicture.Size = new System.Drawing.Size(220, 231);
            this.catPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.catPicture.TabIndex = 0;
            this.catPicture.TabStop = false;
            this.catPicture.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.CatPicture_MouseDoubleClick);
            this.catPicture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CatPicture_MouseDown);
            this.catPicture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CatPicture_MouseUp);
            // 
            // fukidashiPicture
            // 
            this.fukidashiPicture.BackColor = System.Drawing.Color.Transparent;
            this.fukidashiPicture.Image = ((System.Drawing.Image)(resources.GetObject("fukidashiPicture.Image")));
            this.fukidashiPicture.Location = new System.Drawing.Point(4, -4);
            this.fukidashiPicture.Name = "fukidashiPicture";
            this.fukidashiPicture.Size = new System.Drawing.Size(303, 144);
            this.fukidashiPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.fukidashiPicture.TabIndex = 4;
            this.fukidashiPicture.TabStop = false;
            // 
            // fukidashiText
            // 
            this.fukidashiText.AcceptsReturn = true;
            this.fukidashiText.AcceptsTab = true;
            this.fukidashiText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.fukidashiText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.fukidashiText.Font = new System.Drawing.Font("ＭＳ ゴシック", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.fukidashiText.ImeMode = System.Windows.Forms.ImeMode.On;
            this.fukidashiText.Location = new System.Drawing.Point(4, 9);
            this.fukidashiText.Margin = new System.Windows.Forms.Padding(20, 150, 20, 20);
            this.fukidashiText.Multiline = true;
            this.fukidashiText.Name = "fukidashiText";
            this.fukidashiText.Size = new System.Drawing.Size(249, 80);
            this.fukidashiText.TabIndex = 5;
            this.fukidashiText.TextChanged += new System.EventHandler(this.FukidashiText_TextChanged);
            // 
            // timerOnOffTimer
            // 
            this.timerOnOffTimer.Tick += new System.EventHandler(this.TimerOnOffTimer_Tick);
            // 
            // CatMemoForm
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(520, 311);
            this.Controls.Add(this.fukidashiPicture);
            this.Controls.Add(this.fukidashiText);
            this.Controls.Add(this.catPicture);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CatMemoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ネコメモ";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Magenta;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CatMemoForm_FormClosing);
            this.Load += new System.EventHandler(this.CatMemoForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.CatMemoForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.CatMemoForm_DragEnter);
            this.Leave += new System.EventHandler(this.CatMemoForm_Leave);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.CatPicture_MouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CatPicture_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CatPicture_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.catPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fukidashiPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer moveTimer;
        private System.Windows.Forms.PictureBox catPicture;
        public System.Windows.Forms.PictureBox fukidashiPicture;
        public System.Windows.Forms.TextBox fukidashiText;
        private System.Windows.Forms.Timer timerOnOffTimer;
    }
}

