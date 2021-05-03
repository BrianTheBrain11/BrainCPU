using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brain
{
    class AudioCore
    {
        ushort audioMemPointer;
        
        byte[] audioMemory = new byte[1024];
        
        void AudioInitialize()
        {
            audioMemPointer = 0x00;
        }
        
        void AudioLoop()
        {
            if (audioMemory[audioMemPointer])
            {
                audioMemPointer = 0x00;
                
                //check memory flag for audio stop/swap, stop or load audio memory if so
                
                return;
            }
            PlayAudio(audioMemory[audioMemPointer]);
            audioMemPointer++;
        }
    }
}
