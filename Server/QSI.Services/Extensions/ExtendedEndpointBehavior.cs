

using System;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;



namespace QSI.Services.Extensions
{
    /// <summary>
    /// Endpoint behavior is used to extend run-time behavior for an endpoint 
    /// in either a service / client application.
    /// </summary>
    public class ExtendedEndpointBehavior : BehaviorExtensionElement, IEndpointBehavior
    {
        /// <summary>
        /// Adding Binding Parameters
        /// </summary>
        /// <param name="endpoint">ServiceEndPoint endpoint</param>
        /// <param name="bindingParameters">BindingParameterCollection bindingParameters</param>
        /// <returns></returns>
        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }


        /// <summary>
        /// Apply Binding Parameters
        /// </summary>
        /// <param name="endpoint">ServiceEndPoint endpoint</param>
        /// <param name="clientRuntime">ClientRuntime clientRuntime</param>
        /// <returns></returns>
        public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
        }


        /// <summary>
        /// Apply Dispatch Behavior
        /// </summary>
        /// <param name="endpoint">ServiceEndPoint endpoint</param>
        /// <param name="endpointDispatcher">EndpointDispatcher endpointDispatcher</param>
        /// <returns></returns>
        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new ExtendedDispatchMessageInspector());
        }


        /// <summary>
        /// Validate ServiceEndpoint
        /// </summary>
        /// <param name="endpoint">ServiceEndPoint endpoint</param>
        /// <returns></returns>
        public void Validate(ServiceEndpoint endpoint)
        {
        }


        /// <summary>
        /// Over;ride Behavior Type
        /// </summary>
        /// <returns>Type of ExtendedEndpointBehavior</returns>
        public override Type BehaviorType
        {
            get
            {
                return typeof (ExtendedEndpointBehavior);
            }
        }


        /// <summary>
        /// Override CreateBehavior() method
        /// </summary>
        /// <returns>An object of ExtendedEndpointBehavior</returns>
        protected override object CreateBehavior()
        {
            return new ExtendedEndpointBehavior();
        }
    }
}