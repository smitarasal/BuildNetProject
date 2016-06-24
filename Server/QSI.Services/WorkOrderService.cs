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
    public class WorkOrderService : IWorkOrderService
    {
        IWorkOrderRepository _workOrderRepository;

        public WorkOrderService()
        {
            if (_workOrderRepository == null)
                _workOrderRepository = new WorkOrderRepository();
        }


        public WorkOrderService(IWorkOrderRepository workOrderRepository)
        {
            _workOrderRepository = workOrderRepository;
        }


        public WorkOrderResponse GetWorkOrderByUserID(string UserId)
        {
            var userId = Guid.Parse(UserId);
            var validUserOrderLst = _workOrderRepository.GetWhere(m => m.UserId == userId);
            var validUserOrder = validUserOrderLst.ToList();

            WorkOrderResponse response = new WorkOrderResponse();

            if (validUserOrder != null)
            {
                List<WorkOrderDto> workOrderDto = Mapper.Map<List<WorkOrder>, List<WorkOrderDto>>(validUserOrder);
                response.WorkOrderDetails = JsonConvert.SerializeObject(workOrderDto);

            }
            else
            {
                ErrorObject obj = new ErrorObject { Message = "Invalid UserId. Data not found.", Status = "Failed" };
                response.WorkOrderDetails = JsonConvert.SerializeObject(obj);


            }
            return response;
        }


        public WorkOrderResponse SaveWorkOrder(WorkOrderDto workOrderDto)
        {
            workOrderDto.Id = Guid.NewGuid();
            workOrderDto.CreatedOn = DateTime.Now;

            WorkOrderResponse response = new WorkOrderResponse();


            WorkOrder newWorkOrder = Mapper.Map<WorkOrderDto, WorkOrder>(workOrderDto);
            var workOrder = _workOrderRepository.Insert(newWorkOrder);
            _workOrderRepository.Save();

            if (workOrder == null)
            {
                ErrorObject obj = new ErrorObject { Message = "Error in processing request.", Status = "Failed" };
                response.WorkOrderDetails = JsonConvert.SerializeObject(obj);
            }
            else
            {

                ErrorObject obj = new ErrorObject { Message = "Information Saved Successfully", Status = "Success" };
                response.WorkOrderDetails = JsonConvert.SerializeObject(workOrderDto.Id);
            }

            return response;

        }


        public WorkOrderResponse UpdateWorkOrder(WorkOrderDto workOrderDto)
        {
            WorkOrderResponse response = new WorkOrderResponse();

            WorkOrder newWorkOrder = Mapper.Map<WorkOrderDto, WorkOrder>(workOrderDto);

            var result = _workOrderRepository.Update(newWorkOrder);
            _workOrderRepository.Save();
            if (result == null)
            {

                ErrorObject obj = new ErrorObject { Message = "Error in processing request.", Status = "Failed" };
                response.WorkOrderDetails = JsonConvert.SerializeObject(obj);
            }
            else
            {
                ErrorObject obj = new ErrorObject { Message = "Work Orders Updated Successfully", Status = "Success" };
                response.WorkOrderDetails = JsonConvert.SerializeObject(obj);
            }


            return response;

        }

        public WorkOrderResponse DeleteWorkOrder(string Id)
        {
            WorkOrderResponse response = new WorkOrderResponse();

            var workOrderId = Guid.Parse(Id);
            var workOrder = _workOrderRepository.GetWhere(m => m.Id == workOrderId).FirstOrDefault();
            if (workOrder != null)
            {
                _workOrderRepository.Delete(workOrder);
                _workOrderRepository.Save();
                ErrorObject obj = new ErrorObject { Message = "Work Orders Deleted Successfully", Status = "Success" };
                response.WorkOrderDetails = JsonConvert.SerializeObject(obj);
            }
            else
            {
                ErrorObject obj = new ErrorObject { Message = "Error in processing request.", Status = "Failed" };
                response.WorkOrderDetails = JsonConvert.SerializeObject(obj);
            }

            return response;
        }
    }
}
