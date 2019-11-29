using LinnerToolkit.Core.Buffer;
using LinnerToolkit.Core.Codec;
using LinnerToolkit.MindBody.Core.Codec;

namespace NaviAirBuilt.Codec
{
    public   class DataDecoder : DataDecoderBase
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public DataDecoder()
        {
            BYTE_HEADER = new byte[] { 0xAA, 0x55 };
            BYTE_END = new byte[] { 0x0D, 0x0A };
            MinDecodableLength = 7;
        }
        /// <summary>
        /// 解析数据
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        protected override IDecodeData Decode(DataBuffer buffer)
        {
            if (CheckHeader(buffer, BYTE_HEADER))
            {
                byte[] bytes = new byte[MinDecodableLength];
                buffer.Get(bytes, 0, bytes.Length);

                if (CheckEnd(bytes, BYTE_END))
                {
                   return new EnvironmentDeviceData { };               

                }
            }

            return null;
        }
    }
}
