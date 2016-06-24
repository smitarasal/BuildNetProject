/****************************** Module Header ******************************\
Module Name:  UnityServiceBehavior

Copyright (c) 2012-2013 Hart InterCivic, Inc.
All Rights Reserved

This file contains the class UnityServiceBehavior.
It is to create Custom Service Behavior and override the ApplyDispatchBehavior method 
in order to use the Custom Instance Provider.
\***************************************************************************/

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

//TODO:  Comment Code below, add short description above and wrap code under 140 characters per line

namespace QSI.Services
{
    /// <summary>
    /// It is to create Custom Service Behavior and override the ApplyDispatchBehavior method 
    /// in order to use the Custom Instance Provider.
    /// </summary>
    public class ServiceBehavior :Attribute, IServiceBehavior
    {
             /// <summary>
        /// Constructor to initialize unity container.
        /// </summary>
        public ServiceBehavior()
        {
         }


        /// <summary>
        /// Provides the ability to inspect the service host and the service description to confirm that the service can run successfully.
        /// </summary>
        /// <param name="serviceDescription">The service description.</param>
        /// <param name="serviceHostBase">The service host that is currently being constructed.</param>
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }


        /// <summary>
        /// Add the binding parameters
        /// </summary>
        /// <param name="serviceDescription">ServiceDescription serviceDescription</param>
        /// <param name="serviceHostBase">ServiceHostBase serviceHostBase</param>
        /// <param name="endpoints">Collection of service endpoints</param>
        /// <param name="bindingParameters">BindingParameterCollection bindingParameters</param>
        /// <returns></returns>
        public void AddBindingParameters(
            ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase,
            Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection bindingParameters)
        {
            AutoMapBootStrap.InitializeMap();
           
        }


        /// <summary>
        /// To apply the dispatch behavior
        /// </summary>
        /// <param name="serviceDescription">ServiceDescription serviceDescription</param>
        /// <param name="serviceHostBase">ServiceHostBase serviceHostBase</param>
        /// <returns></returns>
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher channelDispatcher in serviceHostBase.ChannelDispatchers)
            {
                foreach (EndpointDispatcher endpointDispatcher in channelDispatcher.Endpoints)
                {
                    if (endpointDispatcher.ContractName != "IMetadataExchange")
                    {
                        string contractName = endpointDispatcher.ContractName;
                        ServiceEndpoint serviceEndpoint = serviceDescription.Endpoints.FirstOrDefault(e => e.Contract.Name == contractName);
                        if (serviceEndpoint != null)
                        {
                      
                        }
                    }
                }
            }
        }
    }
}