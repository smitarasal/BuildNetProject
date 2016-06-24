using Newtonsoft.Json;
using QSI.Data;
using QSI.Data.Spec;
using QSI.Domain;
using QSI.Services.Spec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace QSI.Services
{
    [ServiceBehavior]
    public class SharingService : ISharingService
    {
        IUserManagementRepository _userManagementRepository = new UserManagementRepository();
        ISharingRepository _sharingRepository = new SharingRepository();
        IEmailService _emailService;

        public SharingService()
        {
            _emailService = new EmailService();
        }
        
        public void ShareWidgets(SharingDto sharingDto)
        {

                sharingDto.Id = Guid.NewGuid();
                sharingDto.CreatedAt = DateTime.Now;
                sharingDto.ModifiedAt = DateTime.Now;

                EmailAddressCollection emailAddressCollection = sharingDto.EmailAddresses;

                if (emailAddressCollection != null)
                {
                    List<string> lstEmailAddresses = emailAddressCollection.EmailAddress;

                    if (lstEmailAddresses != null && lstEmailAddresses.Count > 0)
                    {
                        foreach (var item in lstEmailAddresses)
                        {
                            var validUser = _userManagementRepository.GetWhere(m => m.EmailAddress == item).FirstOrDefault();

                            if (validUser != null)
                            {
                                ShareWidget shareWidget = new ShareWidget();
                                shareWidget.Id = Guid.NewGuid();
                                shareWidget.SharedById = sharingDto.UserId;
                                shareWidget.SharedToId = validUser.Id;
                                shareWidget.Keys = sharingDto.Keys;
                                shareWidget.Value = sharingDto.Value;
                                shareWidget.CreatedAt = sharingDto.CreatedAt;
                                shareWidget.ModifiedAt = sharingDto.ModifiedAt;
                                shareWidget.CreatedBy = sharingDto.CreatedBy;
                                shareWidget.ModifiedBy = sharingDto.ModifiedBy;

                                _sharingRepository.Insert(shareWidget);

                                _emailService.EmailBookMark(sharingDto.Value, item, string.Empty);
                            }

                        }

                        _sharingRepository.Save();


                        throw new WebFaultException<string>("Saved successfully", System.Net.HttpStatusCode.OK);
                    }
                }
        }


        public void  DeleteWidgets(string UserId, string Key)
        {
            try
            {
                var userId = Guid.Parse(UserId);
                var shareWidgets = _sharingRepository.GetWhere(m => m.SharedToId == userId && m.Keys == Key);
                var shareWidgetsList = shareWidgets.ToList();

                if (shareWidgetsList != null && shareWidgetsList.Count > 0)
                {
                    foreach (var item in shareWidgetsList)
                    {
                        _sharingRepository.Delete(item);
                        _sharingRepository.Save();
                    }

                    throw new WebFaultException<string>("Widgets deleted successfully", System.Net.HttpStatusCode.OK);
                }
                else
                {
                    throw new WebFaultException<string>("No Widgets Found", System.Net.HttpStatusCode.BadRequest);
                }

            }
            catch (Exception ex)
            {
                if(ex.Message.ToString()=="OK")
                    throw new WebFaultException<string>("Widgets deleted successfully", System.Net.HttpStatusCode.OK);
                else if(ex.Message.ToString()=="Bad Request")
                    throw new WebFaultException<string>("No Widgets Found", System.Net.HttpStatusCode.BadRequest);
               else
                  throw new WebFaultException<string>("Error in Processing Request", System.Net.HttpStatusCode.InternalServerError);
            }
        }

    }
}
