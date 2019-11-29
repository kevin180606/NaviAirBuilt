using LinnerToolkit.Core.Codec;
using Mina.Core.Buffer;
using Mina.Core.Session;
using Mina.Filter.Codec;
using Mina.Filter.Codec.Demux;
using System;

namespace NaviAirBuilt.Codec
{
    public class DefaultMessageEncoder : IMessageEncoder<IEncodeData>
    {
        protected IDataEncoder _innerEncoder;

        public DefaultMessageEncoder(IDataEncoder innterEncoder)
        {
            _innerEncoder = innterEncoder;
        }

        public void Encode(IoSession session, object message, IProtocolEncoderOutput output)
        {
            IEncodeData encodeData = message as IEncodeData;
            if (encodeData != null)
                Encode(session, encodeData, output);
        }

        public void Encode(IoSession session, IEncodeData message, IProtocolEncoderOutput output)
        {
            IoBuffer buffer = null;

            Byte[] data = _innerEncoder.EncodeToByte(message);
            if (data != null)
            {
                buffer = IoBuffer.Allocate(data.Length);
                buffer.Put(data);
            }
            else
            {
                String s = _innerEncoder.EncodeToString(message);
                if (!string.IsNullOrEmpty(s))
                {
                    buffer = IoBuffer.Allocate(s.Length);
                    buffer.PutString(s);
                }
            }

            if (buffer != null)
            {
                buffer.Flip();
                output.Write(buffer);
            }
        }
    }
}
