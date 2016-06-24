using AutoMapper;
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
  public  class ImageService :IImageService
    {

            IImageRepository _imageRepository;
            public ImageService()
            {
                _imageRepository = new ImageRepository();
            }

            public ImageService(IImageRepository imageRepository)
            {
                _imageRepository = imageRepository;
            }

        public void SaveAttributesPicture(AttributesPictureDto attributesPictureDto)
        {
         
                attributesPictureDto.Id = Guid.NewGuid();
                attributesPictureDto.CreatedAt =  DateTime.Now;
                attributesPictureDto.ModifiedAt = DateTime.Now;

                ClientResponse response = new ClientResponse();

                Detection detection = Mapper.Map<AttributesPictureDto, Detection>(attributesPictureDto);
                if (detection.DetectionImages != null && detection.DetectionImages.Count > 0)
                {
                    foreach (var item in detection.DetectionImages)
                    {
                        item.Id = Guid.NewGuid();
                        item.CreatedAt = DateTime.Now;
                        item.ModifiedAt = DateTime.Now;
                    }
                }
                var serverDetection = _imageRepository.Insert(detection);
                _imageRepository.Save();

                if (serverDetection == null)
                {
                    throw new WebFaultException<string>("Insert Failed", System.Net.HttpStatusCode.BadRequest);
                }
                else
                {
                    throw new WebFaultException<string>("Saved successfully", System.Net.HttpStatusCode.OK);
                }
         
           // return response;
        }
    }
}
