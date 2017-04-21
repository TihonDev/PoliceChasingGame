namespace PCGClassLibrary.Managers
{
    using System;
    using System.Media;
    using PCGClassLibrary.Interfaces;

    public class SoundManager : ISoundManager
    {
        private SoundPlayer gameSoundsPlayer;

        public SoundManager(SoundPlayer soundsPlayer)
        {
            this.gameSoundsPlayer = soundsPlayer;
        }

        public void PlaySound(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentException("Parameter \"filename\" cannot have null or empty value.");
            }

            var dotWavIndex = filename.IndexOf(".wav");
            if (dotWavIndex < 1)
            {
                throw new ArgumentException("Parameter \"filename\" should be in the following format: \"filename.wav\"");
            }

            var filePath = "..\\..\\..\\Multimedia\\" + filename;
            this.gameSoundsPlayer.SoundLocation = filePath;
            if (filename.IndexOf("main") == -1)
            {
                this.gameSoundsPlayer.Play();
            }
            else
            {
                this.gameSoundsPlayer.PlayLooping();
            }
        }
    }
}
