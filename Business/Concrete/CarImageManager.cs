using Business.Abstract;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        
        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        public IResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<CarImage> Get(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(c => c.Id == id));
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());

        }

        public IResult UploadImage(int id,string photoName)
        {
            IResult result = BusinessRules.Run(CheckPhotoCount(id));
            if (result!=null)
            {
                return new ErrorResult();
            }
            CarImage carImage = new CarImage();
            carImage.CarId = id;
            carImage.Date = DateTime.Now;
            carImage.ImagePath = photoName;
            _carImageDal.Add(carImage);
            return new SuccessResult();

        }
        private IResult CheckPhotoCount(int id)
        {
            var result = _carImageDal.GetAll(c => c.CarId == id).Count;
            if (result>5)
            {
                return new ErrorResult("");
            }
            return new SuccessResult();
        }
    }
}
