using OpenCvSharp;
using System.Drawing.Imaging;
using System.Drawing;
using OpenCvSharp.Extensions;

namespace DiplomaProject.BLL
{
    public class Utilities
    {
        /*public static void ExtractFrame(string videoPath, string outputImagePath, double timeInSeconds = 0)
        {
            using var capture = new OpenCvSharp.VideoCapture(videoPath);
            if (!capture.IsOpened())
                throw new Exception("Не удалось открыть видео");

            double fps = capture.Fps;
            int frameNumber = (int)(fps * timeInSeconds);

            capture.Set(OpenCvSharp.VideoCaptureProperties.PosFrames, frameNumber);
            using var frame = new OpenCvSharp.Mat();
            capture.Read(frame);

            if (frame.Empty())
                throw new Exception("Не удалось извлечь кадр из видео");

            Cv2.ImWrite(outputImagePath, frame);
        }*/
        public static void ExtractFrame(string videoPath, string outputImagePath, double timeInSeconds = 0)
        {
            using var capture = new OpenCvSharp.VideoCapture(videoPath);
            if (!capture.IsOpened())
                throw new Exception("Не удалось открыть видео");

            // Перемещаемся по времени (в миллисекундах)
            capture.Set(VideoCaptureProperties.PosMsec, timeInSeconds * 1000);

            using var frame = new Mat();
            if (!capture.Read(frame) || frame.Empty())
                throw new Exception("Не удалось извлечь кадр из видео");

            Cv2.ImWrite(outputImagePath, frame);
        }
    }
    public class FrameResponse
    {
        public int Id { get; set; }
        public string FrameUrl { get; set; }
    }
}
