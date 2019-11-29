using LinnerToolkit.Core.Codec;

namespace NaviAirBuilt.Codec
{
    public class DataEncoder  :DataEncoderBase
    {
        public override byte[] EncodeToByte(IEncodeData data)
        {
            if (data is EnvironmentDeviceData environmentDeviceData)
            {
                return data.ToBytes();
            }

            return base.EncodeToByte(data);
        }

    }
}

