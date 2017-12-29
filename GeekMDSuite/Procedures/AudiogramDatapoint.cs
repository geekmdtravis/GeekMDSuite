﻿namespace GeekMDSuite.Procedures
{
    public class AudiogramDatapoint : IAudiogramDatapoint
    {
        public static AudiogramDatapoint Build(int value) => new AudiogramDatapoint(value);
        
        internal AudiogramDatapoint(int value)
        {
            Value = value;
        }
        
        public int Value { get; set; }

        public override string ToString() => $"{Value}dB";

        public AudiogramDatapoint()
        {
            
        }
    }
}