using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace RSIClientSide.Handler
{
    public class MacAddressMessageInspector : IClientMessageInspector
    {
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
           
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            var nextActorHeader = MessageHeader.CreateHeader(
                "mac",
                "http://example.com/",
                "0A-00-27-00-00-09",
                false,
                "http://schemas.xmlsoap.org/soap/actor/next"
                );
            request.Headers.Add(nextActorHeader);
            return null;
        }
    }
}
