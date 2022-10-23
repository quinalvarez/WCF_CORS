using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Web;

namespace MyExample
{
    public class CORSEnablingBehavior : BehaviorExtensionElement, IEndpointBehavior 
    {
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters) { }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime) { }

        public void ApplyDispatchBehavior(ServiceEndpoint enpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new CORSHeaderInjectingMessageInspector());
        }

        public void Validate(ServiceEndpoint enpoint) { }

        public override Type BehaviorType { get { return typeof(CORSEnablingBehavior); } }

        protected override object CreateBehavior() { return new CORSEnablingBehavior(); }

        private class CORSHeaderInjectingMessageInspector : IDispatchMessageInspector
        {
            public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext) { return null; }

            private static IDictionary<string, string> _headersToInject = new Dictionary<string, string>
            {
                { "Access-Control-Allow-Origin" ,"http://localhost:3000" },
                { "Access-Control-Request-Method", "POST,GET,PUT,DELETE,OPTIONS" },
                { "Access-Control-Allow-Headers", "X-Requested-With, Content-Type" }
            };

            public void BeforeSendReply(ref Message reply, object correlationState)
            {
                var httpHeader = reply.Properties["httpResponse"] as HttpResponseMessageProperty;
                foreach (var item in _headersToInject)
                    httpHeader.Headers.Add(item.Key, item.Value);
            }
        }
    }
}