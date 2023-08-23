using SIPSorceryMedia.Abstractions;

namespace SIPSorceryVideoNegotiationTest
{
    public class MultiFormatVideoSource : IVideoSource
    {
        private const int H264_FORMAT_ID = 100;
        private const int VP8_FORMAT_ID = 96;
        private static readonly List<VideoFormat> SupportedVideoFormats = new List<VideoFormat>
        {
            new VideoFormat(VideoCodecsEnum.VP8, VP8_FORMAT_ID, VideoFormat.DEFAULT_CLOCK_RATE),
            new VideoFormat(VideoCodecsEnum.H264, H264_FORMAT_ID, VideoFormat.DEFAULT_CLOCK_RATE)
        };

        public event EncodedSampleDelegate OnVideoSourceEncodedSample;

        public event SourceErrorDelegate OnVideoSourceError;

        public event RawVideoSampleDelegate OnVideoSourceRawSample;

        public event RawVideoSampleFasterDelegate OnVideoSourceRawSampleFaster;

        public Task CloseVideo() => Task.CompletedTask;

        public void ExternalVideoSourceRawSample(uint durationMilliseconds, int width, int height, byte[] sample, VideoPixelFormatsEnum pixelFormat)
        {
            //
        }

        public void ExternalVideoSourceRawSampleFaster(uint durationMilliseconds, RawImage rawImage)
        {
            //
        }

        public void ForceKeyFrame()
        {
            //
        }

        public List<VideoFormat> GetVideoSourceFormats() => SupportedVideoFormats;

        public bool HasEncodedVideoSubscribers() => false;

        public bool IsVideoSourcePaused() => true;

        public Task PauseVideo() => Task.CompletedTask;

        public void RestrictFormats(Func<VideoFormat, bool> filter)
        {
            //
        }

        public Task ResumeVideo() => Task.CompletedTask;

        public void SetVideoSourceFormat(VideoFormat videoFormat)
        {
            Console.WriteLine($"{nameof(MultiFormatVideoSource)} Video Source Format Id: {videoFormat.FormatID}");
        }

        public Task StartVideo() => Task.CompletedTask;
    }
}
