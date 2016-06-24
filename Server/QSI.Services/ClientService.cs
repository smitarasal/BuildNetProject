using AutoMapper;
using Newtonsoft.Json;
using QSI.Data;
using QSI.Data.Spec;
using QSI.Domain;
using QSI.Services.Spec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSI.Services
{
    [ServiceBehavior]
    public class ClientService : IClientService
    {
        IClientRepository _clientRepository;
        IClientServersRepository _clientServersRepository;

        public ClientService()
        {
            if (_clientRepository == null)
                _clientRepository = new ClientRepository();

            if (_clientServersRepository == null)
                _clientServersRepository = new ClientServersRepository();
        }

        public ClientService(IClientRepository clientRepository, IClientServersRepository clientServersRepository)
        {
            _clientRepository = clientRepository;
            _clientServersRepository = clientServersRepository;                        
        }


        public ClientResponse GetAuthorizedClients(string Id)
        {
            var clientId = Guid.Parse(Id);
            var validClientLst = _clientRepository.GetWhere(m => m.Id == clientId);
            var validClient = validClientLst.FirstOrDefault();

            ClientResponse response = new ClientResponse();

            if (validClient != null)
            {
                ClientDto clientsDto = Mapper.Map<Client, ClientDto>(validClient);
                response.ClientDetails = JsonConvert.SerializeObject(clientsDto);

            }
            else
            {
                ErrorObject obj = new ErrorObject { Message = "Invalid Client Id.", Status = "Failed" };
                response.ClientDetails = JsonConvert.SerializeObject(obj);
            }
            return response;

        }



        public ClientResponse GetAllClients()
        {

            List<ClientDto> LstClientDto = new List<ClientDto>();
            ClientResponse response = new ClientResponse();
            var clientLst = _clientRepository.GetAll();
            var clients = clientLst.ToList();

            if (clients.Count() > 0)
            {
                foreach (var item in clients)
                {
                    ClientDto clientsDto = Mapper.Map<Client, ClientDto>(item);
                    LstClientDto.Add(clientsDto);
                }

                response.ClientDetails = JsonConvert.SerializeObject(LstClientDto);
            }
            else
            {
                ErrorObject obj = new ErrorObject { Message = "No User data found.", Status = "Failed" };
                response.ClientDetails = JsonConvert.SerializeObject(obj);
            }
            return response;
        }



        public ClientResponse SaveClient(ClientDto clientDto)
        {
            clientDto.Id = Guid.NewGuid();
            clientDto.CreatedAt = DateTime.Now;
            clientDto.ModifiedAt = DateTime.Now;

            ClientResponse response = new ClientResponse();

            Client newClient = Mapper.Map<ClientDto, Client>(clientDto);
            if (newClient.ClientServers != null && newClient.ClientServers.Count > 0)
            {
                foreach (var item in newClient.ClientServers)
                {
                    item.Id = Guid.NewGuid();
                }
            }
            var client = _clientRepository.Insert(newClient);
            _clientRepository.Save();

            if (client == null)
            {
                ErrorObject obj = new ErrorObject { Message = "Error in processing request.", Status = "Failed" };
                response.ClientDetails = JsonConvert.SerializeObject(obj);
            }
            else
            {
                ErrorObject obj = new ErrorObject { Message = "Information Saved Successfully", Status = "Success" };
                response.ClientDetails = JsonConvert.SerializeObject(clientDto.Id);
            }
            return response;
         }

        public ClientResponse DeleteClient(string Id)
        {
            ClientResponse response = new ClientResponse();
            var clientId = Guid.Parse(Id);
            var client = _clientRepository.GetWhere(m => m.Id == clientId).FirstOrDefault();

            if (client != null)
            {
                _clientRepository.Delete(client);
                _clientRepository.Save();
                ErrorObject obj = new ErrorObject { Message = "Client Deleted Successfully.", Status = "Success" };
                response.ClientDetails = JsonConvert.SerializeObject(obj);
            }
            else
            {
                ErrorObject obj = new ErrorObject { Message = "Error in processing request.", Status = "Failed" };
                response.ClientDetails = JsonConvert.SerializeObject(obj);
            }
            return response;
        }


        public ClientResponse UpdateClient(ClientDto clientDto)
        {
            ClientResponse response = new ClientResponse();
            Client result = null;
            try
            {
             
                var clientServers = _clientServersRepository.GetWhere(m => m.ClientId == clientDto.Id);
                if (clientServers.Count() > 0 && clientServers != null)
                {
                    foreach (var item in clientServers)
                    {

                        _clientServersRepository.Delete(item);

                    }
                }
                _clientServersRepository.Save();

                if (clientDto.ClientServers != null && clientDto.ClientServers.Count > 0)
                {
                    foreach (var item in clientDto.ClientServers)
                    {
                        item.ClientId = clientDto.Id;
                    }
                }
                var client = Mapper.Map<ClientDto, Client>(clientDto);
                foreach (var item in client.ClientServers)
                {
                    item.Id = Guid.NewGuid();
                    _clientServersRepository.Insert(item);
                    _clientServersRepository.Save();
                }
                client.ModifiedAt = DateTime.Now;
                result = _clientRepository.Update(client);
                _clientRepository.Save();
            }
            catch (Exception ex)
            {

                ErrorObject obj = new ErrorObject { Message = "Error in processing request.", Status = "Failed" };
                response.ClientDetails = JsonConvert.SerializeObject(obj);
            }
            //      _clientRepository.Save();

            if (result == null)
            {
                ErrorObject obj = new ErrorObject { Message = "Error in processing request.", Status = "Failed" };
                response.ClientDetails = JsonConvert.SerializeObject(obj);
            }
            else
            {
                ErrorObject obj = new ErrorObject { Message = "Client updated Successfully", Status = "Success" };
                response.ClientDetails = JsonConvert.SerializeObject(obj);
            }

            return response;
        }

    }
}
