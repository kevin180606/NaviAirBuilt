using LinnerToolkit.Core.Codec;
using Mina.Filter.Codec.Demux;

namespace NaviAirBuilt.Codec
{
    public  class DefaultDemuxingProtocolCodecFactory: DemuxingProtocolCodecFactory
    {
        public DefaultDemuxingProtocolCodecFactory(IDataDecoder dataDecoder, IDataEncoder dataEncoder)
        {
            if (dataDecoder != null)
                AddMessageDecoder(new DefaultMessageDecoder(dataDecoder));

            if (dataEncoder != null)
                AddMessageEncoder(new DefaultMessageEncoder(dataEncoder));
        }

        public DefaultDemuxingProtocolCodecFactory(IDataDecoder dataDecoder) : this(dataDecoder, null) { }

        public DefaultDemuxingProtocolCodecFactory(IDataEncoder dataEncoder) : this(null, dataEncoder) { }
    }
}
