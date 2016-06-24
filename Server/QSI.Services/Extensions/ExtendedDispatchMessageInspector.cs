using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;


namespace QSI.Services.Extensions
{
    /// <summary>
    /// Contains logic to inspect message after receiving it from client and
    /// before sending it to another operation.
    /// </summary>

    public class ExtendedDispatchMessageInspector : IDispatchMessageInspector, IEndpointBehavior
    {        

      
    
        public ExtendedDispatchMessageInspector()
        {
          
        }

        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        /// <param name="userService">User Service</param>
        
   

        /// <summary>
        /// Inspects message after receiving request.
        /// </summary>
        /// <param name="request">Message Request</param>
        /// <param name="channel">Client Channel Object</param>
        /// <param name="instanceContext">Instance Context</param>
        /// <returns>Object</returns>
        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, 
            System.ServiceModel.IClientChannel channel, System.ServiceModel.InstanceContext instanceContext)
        {
            int tokenIndex = request.Headers.FindHeader("UserToken", "");
            int electionIndex = request.Headers.FindHeader("ElectionStateId", "");
            int applicationNameIndex = request.Headers.FindHeader("Application", "");

            //If the headerIndex is -1, then the token was missing from the header
            if (tokenIndex != -1)
            {
                string applicationName = string.Empty;

                if (applicationNameIndex != -1)
                {
                    applicationName = request.Headers.GetHeader<string>(applicationNameIndex);
                }

                string userToken = request.Headers.GetHeader<string>(tokenIndex);
                string electionId = Guid.Empty.ToString();

                //we do not block the top level IF statement because
                //we need the ExtendedOperationContext "User" to be set if the UserToken exist
                if (electionIndex != -1)
                {
                    electionId = request.Headers.GetHeader<string>(electionIndex);
                }

                Guid token = new Guid(userToken);
                Guid electionStateId = new Guid(electionId);

                    
                
            }
            else
            {
                if (request.Headers.Action.Contains("LoginUser"))
                {
                    //proceed
                }
                else
                {
                    //Logging.LogService.Fatal(LogEvents.Layout.LayoutAuthorizationLogEvents.UserTokenMissing, new AuthorizationEventData());
                }
            }

            return null;
        }
        

        /// <summary>
        /// Inspects message before sending it to another operation.
        /// </summary>
        /// <param name="reply">Message Reply</param>
        /// <param name="correlationState">Object correlationState</param>
        /// <returns></returns>

        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            //Clean up the OperationContext
            OperationContext.Current.Extensions.Remove(ExtendedOperationContext.Current);
        }



        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
            throw new NotImplementedException();
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            throw new NotImplementedException();
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            throw new NotImplementedException();
        }

        public void Validate(ServiceEndpoint endpoint)
        {
            throw new NotImplementedException();
        }
    }
}
