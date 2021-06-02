using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCC_Trainer
{
    public static class API
    {
        public static readonly int PipeBufferSize = 256;
    }

    public enum MessageType
    {
        Success = 1,
        Failure = 2,
        Esam = 3
    }

}
