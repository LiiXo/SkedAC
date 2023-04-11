using System.Diagnostics;
using Ionic.Zip;
using System.IO;

namespace SkedAC
{
    internal class Recorder
    {
        public bool _isRecording = false;
        private Process _process = null;

        public void StartRecording()
        {
            Rectangle screenBounds = Screen.PrimaryScreen.Bounds;

            string saveFilePath = "files/data.mp4";

            string ffmpegArgs = $"-f gdigrab -framerate 30 -offset_x {screenBounds.Left} -offset_y {screenBounds.Top} -video_size {screenBounds.Width}x{screenBounds.Height} -i desktop -y -c:v libx264 -preset ultrafast -crf 0 -pix_fmt yuv420p \"{saveFilePath}\"";

            _process = new Process();
            _process.StartInfo.FileName = "ffmpeg.exe";
            _process.StartInfo.Arguments = ffmpegArgs;
            _process.StartInfo.CreateNoWindow = true;
            _process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            _process.StartInfo.UseShellExecute = false;
            _process.StartInfo.RedirectStandardOutput = true;
            _process.StartInfo.RedirectStandardInput = true;

            _process.Start();
            _isRecording = true;
        }

        public void StopRecording()
        {
            _process.StandardInput.Write("q");
            _process.StandardInput.Close();
            _process.WaitForExit();
            _process.Close();
            _isRecording = false;

            DateTime currentDateTime = DateTime.Now;
   
            string filePath = "files/data.mp4";

            string zipPath = $"files/{currentDateTime.ToString("yyyy-MM-dd_HH-mm-ss")}.zip";
            string password = "SkedVerif1";

            using (ZipFile zip = new ZipFile())
            {
                zip.Password = password;
                zip.AddFile(filePath);
                zip.Save(zipPath);
            }

            File.Delete(filePath);
        }
    }
}
