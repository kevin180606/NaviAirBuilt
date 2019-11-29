using LinnerToolkit.Core.Codec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaviAirBuilt.Codec
{
    public class EnvironmentDeviceData : IDecodeData, IEncodeData
    {
        public byte[] ToBytes()
        {
            return new byte[] { 0xAA, 0x55, 0, 1, 0, 0x0D, 0x0A };
        }
    }
}
