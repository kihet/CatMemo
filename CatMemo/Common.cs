using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace CatMemo.Components
{
    /// <summary>
    /// 共通関数クラス
    /// </summary>
    public static class Common
    {
        /// <summary>
        /// ファイルの種類が画像であるかどうか
        /// </summary>
        /// <param name="fileName">拡張子入りのファイル名</param>
        /// <returns>画像であればtrue そうでなければfalse</returns>
        public static bool IsPictureFile(string fileName)
        => Const.PICT_FILE_EXTENSIONS.Contains(Path.GetExtension(fileName).Replace(".", "").ToLower());

        /// <summary>
        /// 画像の不透過箇所を一色に塗りつぶす
        /// </summary>
        /// <param name="bmp">塗りつぶす画像</param>
        /// <returns>塗りつぶした画像</returns>
        public static Bitmap FillImage(Bitmap bmp, Color fillColor)
        {
            // 画像の座標を一つずつ取得し、アルファ値が0でなければ不透過色で塗りつぶす
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color pixelColor = bmp.GetPixel(x, y);
                    if (pixelColor.A > 0)
                    {
                        bmp.SetPixel(x, y, fillColor);
                    }
                }
            }
            return bmp;
        }

        /// <summary>
        /// カンマ区切りのバイト配列で構成された文字列から画像を取得
        /// </summary>
        /// <param name="binaryText">カンマ区切りのバイト配列で構成された文字列</param>
        /// <returns>取得した画像</returns>

        public static Image GetImageFromCSVString(string binaryText)
        {
            ImageConverter imgconv = new ImageConverter();
            return (Image)imgconv.ConvertFrom(binaryText.Split(',').Select(num => byte.Parse(num)).ToArray());
        }

        /// <summary>
        /// 透過漏れの箇所を透過色に変換したBitmapを取得
        /// </summary>
        /// <param name="source">透過漏れしている画像</param>
        /// <param name="transparentColor">設定している透過色</param>
        /// <returns>透過漏れ箇所が透過色に変換された画像</returns>
        public static Bitmap RemoveTransparencyEdges(Bitmap source, Color transparentColor)
        {
            Bitmap result = new Bitmap(source.Width, source.Height);
            for (int y = 0; y < source.Height; y++)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    Color pixel = source.GetPixel(x, y);

                    // 透過漏れ箇所を完全透過にし、そうでない箇所を保持する
                    if (IsCloseToTransparentColor(pixel, transparentColor) || pixel.A < Const.ALPHA_THRESHOLD)
                    {
                        result.SetPixel(x, y, transparentColor);
                    }
                    else
                    {
                        result.SetPixel(x, y, pixel);
                    }
                }
            }
            return result;
        }
        public static List<Bitmap> ExtractFrames(Image gifImage)
        {
            var frames = new List<Bitmap>();
            var frameCount = gifImage.GetFrameCount(FrameDimension.Time);

            for (int i = 0; i < frameCount; i++)
            {
                gifImage.SelectActiveFrame(FrameDimension.Time, i);
                frames.Add(new Bitmap(gifImage));
            }

            return frames;
        }

        public static Image RebuildGif(List<Bitmap> frames, int delay)
        {
            using (var stream = new MemoryStream())
            {
                var gifEncoder = ImageCodecInfo.GetImageEncoders().First(c => c.MimeType == "image/gif");
                var encoderParams = new EncoderParameters(1);

                // 最初のフレームを保存
                encoderParams.Param[0] = new EncoderParameter(Encoder.SaveFlag, (long)EncoderValue.MultiFrame);
                frames[0].Save(stream, gifEncoder, encoderParams);

                // 残りのフレームを追加
                encoderParams.Param[0] = new EncoderParameter(Encoder.SaveFlag, (long)EncoderValue.FrameDimensionTime);
                for (int i = 1; i < frames.Count; i++)
                {
                    frames[0].SaveAdd(frames[i], encoderParams);
                }

                // GIFを終了
                encoderParams.Param[0] = new EncoderParameter(Encoder.SaveFlag, (long)EncoderValue.Flush);
                frames[0].SaveAdd(encoderParams);

                // GIFをストリームから読み込む
                return Image.FromStream(new MemoryStream(stream.ToArray()));
            }
        }



        /// <summary>
        /// 取得した色が透過色の閾値内に収まっているか
        /// </summary>
        /// <param name="pixel">取得した色</param>
        /// <param name="transparentColor">透過色</param>
        /// <returns>取得した色が透過色の閾値内に収まっている場合はtrue そうでない場合はfalse</returns>
        public static bool IsCloseToTransparentColor(Color pixel, Color transparentColor)
        {
            int diffR = Math.Abs(pixel.R - transparentColor.R);
            int diffG = Math.Abs(pixel.G - transparentColor.G);
            int diffB = Math.Abs(pixel.B - transparentColor.B);

            return (diffR + diffG + diffB) <= Const.TOLERANCE;
        }

    }
}
