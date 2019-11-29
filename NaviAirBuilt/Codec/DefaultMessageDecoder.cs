using LinnerToolkit.Core.Codec;
using Mina.Core.Buffer;
using Mina.Core.Session;
using Mina.Filter.Codec;
using Mina.Filter.Codec.Demux;
using System;
using System.Collections.Generic;

namespace NaviAirBuilt.Codec
{
    public class DefaultMessageDecoder : IMessageDecoder
    {
        protected Dictionary<IoSession, IDataDecoder> _innerDecoderDic;

        protected Type dataDecoderType;

        public DefaultMessageDecoder(IDataDecoder innerDecoder)
        {
            dataDecoderType = innerDecoder.GetType();
            _innerDecoderDic = new Dictionary<IoSession, IDataDecoder>();
        }

        protected IDataDecoder CreateDataDecoder()
        {
            return dataDecoderType.Assembly.CreateInstance(dataDecoderType.FullName) as IDataDecoder;
        }

        public MessageDecoderResult Decodable(IoSession session, IoBuffer input)
        {
            if (!_innerDecoderDic.ContainsKey(session))
                _innerDecoderDic.Add(session, CreateDataDecoder());

            return _innerDecoderDic[session].Decodable(input.Remaining) ? MessageDecoderResult.OK : MessageDecoderResult.NeedData;
        }

        public MessageDecoderResult Decode(IoSession session, IoBuffer input, IProtocolDecoderOutput output)
        {
            byte[] remaining = new byte[input.Remaining];
            input.Get(remaining, 0, remaining.Length);

            var data = _innerDecoderDic[session].Decode(remaining);
            if (data != null)
            {
                output.Write(data);
                return MessageDecoderResult.OK;
            }

            return MessageDecoderResult.NeedData;
        }

        public void FinishDecode(IoSession session, IProtocolDecoderOutput output)
        {
        }
    }
}
