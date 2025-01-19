using System.Drawing;

namespace CatMemo.Components
{
    /// <summary>
    /// 変更不可の設定値クラス
    /// </summary>
    public static class Const
    {
        /// <summary>
        /// 透明度（アルファ値）の閾値
        /// </summary>
        public const int ALPHA_THRESHOLD = 50;

        /// <summary>
        /// 許容するRGBの近さの閾値
        /// </summary>
        public const int TOLERANCE = 20;

        /// <summary>
        /// 受け入れる画像ファイルの拡張子
        /// </summary>
        public static readonly string[] PICT_FILE_EXTENSIONS = new string[] { "jpg", "jpeg", "png", "gif", "bmp" };

        /// <summary>
        /// 画像の裏に設定する色
        /// </summary>
        public static readonly Color IMAGE_BACK_COLOR = Color.White;
    }
}
