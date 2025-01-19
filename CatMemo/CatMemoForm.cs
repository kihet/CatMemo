using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using CatMemo.Components;
using System.Linq;

namespace CatMemo
{
    public partial class CatMemoForm : Form
    {

        public CatMemoForm()
        {
            InitializeComponent();
        }
        #region 【イベントハンドラ】

        private void CatMemoForm_Load(object sender, EventArgs e)
        {
            SetTransparentColor(Color.Magenta);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.KeyPreview = true;
            
            SetFukidashi();
            SetCatPictureImage();
            SetFormBounds();
            SetCatPictureImageLocation();
            SystemEvents.DisplaySettingsChanged += SystemEvents_DisplaySettingsChanged;
            this.KeyDown += CatMemoForm_KeyDown;
        }

        private void CatMemoForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers != Keys.Shift) return;
            if (e.KeyCode == Keys.Right && ((fukidashiText.Font.Size) < 25))
            {
                var font = new Font(fukidashiText.Font.FontFamily, fukidashiText.Font.Size + 1, FontStyle.Regular);
                fukidashiText.Font = font;
                FukidashiText_TextChanged(fukidashiText, e);
            }
            else if (e.KeyCode == Keys.Left && (fukidashiText.Font.Size) > 8)
            {
                var font = new Font(fukidashiText.Font.FontFamily, fukidashiText.Font.Size - 1, FontStyle.Regular);
                fukidashiText.Font = font;
                FukidashiText_TextChanged(fukidashiText, e);
            }
        }
        private void CatMemoForm_DragDrop(object sender, DragEventArgs e)
        {
            ImageConverter imgconv = new ImageConverter();
            foreach (var fileName in (string[])e.Data.GetData(DataFormats.FileDrop, false))
            {
                if (Common.IsPictureFile(fileName))
                {
                    var img = Image.FromFile(fileName);
                    byte[] b = (byte[])imgconv.ConvertTo(img, typeof(byte[]));
                    Properties.Settings.Default.imageSet = string.Join(",", b);
                }
            }

            SetCatPictureImage();
            SetFormBounds();
            SetCatPictureImageLocation();
            FukidashiText_TextChanged(fukidashiText, new EventArgs());

            Properties.Settings.Default.Save();


        }

        private void CatMemoForm_DragEnter(object sender, DragEventArgs e)
        {
            foreach (var fileName in (string[])e.Data.GetData(DataFormats.FileDrop, false))
            {
                if (Common.IsPictureFile(fileName))
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
        }

        private void CatMemoForm_Leave(object sender, EventArgs e)
        {
            timerOnOffTimer.Stop();
        }
        private void CatMemoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.memoText = fukidashiText.Text;
            Properties.Settings.Default.fontSize = fukidashiText.Font.Size;
            Properties.Settings.Default.Save();
        }

        private void CatPicture_Paint(object sender, PaintEventArgs e)
        {
            // アプリケーションのドラッグによる位置変更を反映させないため
            if (moveTimer.Enabled || timerOnOffTimer.Enabled)
            {
                return;
            }

            Bitmap buffer = new Bitmap(catPicture.Width, catPicture.Height);
            using (Graphics g = Graphics.FromImage(buffer))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                g.FillRectangle(new Pen(this.BackColor).Brush, catPicture.Left, catPicture.Top, catPicture.Width, catPicture.Height);
                g.DrawImage(catPicture.Image, 0, 0, catPicture.Width, catPicture.Height);
            }
            var frames = Common.ExtractFrames(buffer);

            // 各フレームに透過処理を適用
            var processedFrames = frames.Select(frame =>
                Common.RemoveTransparencyEdges(frame, this.BackColor)).ToList();

            // 処理済みGIFを再構築
            var processedImage = Common.RebuildGif(processedFrames, 100); // 100ms の遅延
            //processedImage = Common.RemoveTransparencyEdges(buffer, this.BackColor);

            catPicture.Image = processedImage;
            e.Graphics.DrawImage(processedImage, 0, 0);
            catPicture.Paint -= CatPicture_Paint;
        }

        private void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
        {
            // モニタの解像度にアプリケーションの配置を合わせるため
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, Screen.PrimaryScreen.Bounds.Height - this.Height - 50);
        }

        private void CatPicture_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Application.Exit();
            }
            else if (e.Button == MouseButtons.Left)
            {
                moveTimer.Start();
            }
        }
        private void CatPicture_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                moveTimer.Stop();
            }
        }

        private void CatPicture_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.TopMost = !this.TopMost;
        }

        private void MoveTimer_Tick(object sender, EventArgs e)
        {
            this.Location = new Point(MousePosition.X - fukidashiPicture.Width - 50, MousePosition.Y - fukidashiPicture.Height);
            if (!timerOnOffTimer.Enabled) timerOnOffTimer.Start();
        }


        private void FukidashiText_TextChanged(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            Encoding encoding = Encoding.GetEncoding(65001,
                                EncoderFallback.ExceptionFallback,
                                new DecoderReplacementFallback(""));
            Graphics g = tb.CreateGraphics();
            var byteCount = encoding.GetByteCount(tb.Text.ToCharArray());
            var textHeight = g.MeasureString(tb.Text, tb.Font, tb.Width).Height;
            var minHeight = fukidashiText.Font.Size * 30;
            minHeight = 280;
            var minTBHeight = (int)(fukidashiText.Font.Size * 9);
            minTBHeight = 80;
            var minDY = (int)(fukidashiText.Font.Size * 16 / 10);
            minDY = 311;
            if (byteCount < minHeight)
            {

            }
            else
            {
                var abureLength = (int)(textHeight - tb.Height);
                tb.Height += (abureLength);
                fukidashiPicture.Height += (abureLength * 12 / 9);
                this.Height += (abureLength);
                tb.Top += (abureLength / 6);
                tb.Left += (abureLength / 6);
                catPicture.Top += (abureLength);
                this.Top -= (abureLength);
                this.Width += abureLength/3;
                this.Left -= (abureLength)/3;

                fukidashiPicture.Width += abureLength/3;
                catPicture.Left += abureLength/3;

            }
        }

        private void TimerOnOffTimer_Tick(object sender, EventArgs e)
        {

        }

        #endregion

        #region 【コントロールを設定する関数】
        private void SetFormBounds()
        {
            this.Top = Screen.PrimaryScreen.Bounds.Height - (catPicture.Height + catPicture.Location.Y);
            this.Height = Screen.PrimaryScreen.Bounds.Height - this.Top;
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, Screen.PrimaryScreen.Bounds.Height - this.Height - 50);
        }

        private void SetCatPictureImage()
        {
            var img = catPicture.Image;
            if (!string.IsNullOrEmpty(Properties.Settings.Default.imageSet))
            {
                img = Common.GetImageFromCSVString(Properties.Settings.Default.imageSet);
            }
            var bmp = new Bitmap(img);
            catPicture.BackgroundImage = Common.FillImage(bmp, Const.IMAGE_BACK_COLOR);
            catPicture.BackgroundImageLayout = ImageLayout.Stretch;
            catPicture.Image = img;
            var plusHeight = ((int)(img.Height * ((float)catPicture.Width / img.Width))) - catPicture.Height + 300;
            catPicture.Height = ((int)(img.Height * ((float)catPicture.Width / img.Width)));
            catPicture.Top -= plusHeight;
            if (catPicture.Top < 0) catPicture.Top = 0;
            catPicture.Paint += CatPicture_Paint;

        }

        private void SetCatPictureImageLocation()
        {
            var locationY = this.Height - catPicture.Height;
            if (locationY < 0) locationY = 0;
            catPicture.Top = locationY;

        }

        private void SetFukidashi()
        {
            fukidashiText.Parent = fukidashiPicture;
            fukidashiPicture.Controls.Add(fukidashiText);
            fukidashiText.Location = new Point(30, 30);

            fukidashiText.Font = new Font(fukidashiText.Font.FontFamily, Properties.Settings.Default.fontSize, FontStyle.Regular);
            var memoText = Properties.Settings.Default.memoText;
            fukidashiText.Text = memoText;
        }

        /// <summary>
        /// フォームの透過を設定
        /// </summary>
        /// <param name="transparentColor">透過する色</param>
        private void SetTransparentColor(Color transparentColor)
        {
            this.BackColor = transparentColor;
            this.TransparencyKey = transparentColor;
        }
        #endregion
    }

}
