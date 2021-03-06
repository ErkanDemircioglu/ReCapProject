﻿using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities;
using Core.Utilities.Business;
using Core.Utilities.FileHelpers;
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
        [CacheRemoveAspect("ICarImageService.Get")]
        public IResult Delete(CarImage carImage)
        {
            _carImageDal.Delete(carImage);
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<CarImage> Get(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(c => c.Id == id));
        }
        [CacheAspect]
        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());

        }
        [CacheRemoveAspect("ICarImageService.Get")]
        public IResult Update(CarImage carImage,IFormFile files,string path)
        {
            IResult result = BusinessRules.Run(CheckPhotoCount(carImage.CarId));
            if (result!=null)
            {
                return result;
            }

            CarImage updatedCarImage = new CarImage();
            updatedCarImage.Id = carImage.Id;
            updatedCarImage.CarId = carImage.CarId;
            updatedCarImage.Date = DateTime.Now;
            updatedCarImage.ImagePath = FileHelper.Update(files, path);
                
            _carImageDal.Update(updatedCarImage);
            return new SuccessResult();
        }

        public IResult UploadImage(int id, IFormFile objectFile,string path)
        {
         
            IResult result = BusinessRules.Run(CheckPhotoCount(id));
            if (result != null)
            {
                return new ErrorResult();
            }
            CarImage carImage = new CarImage();
            carImage.CarId = id;
            carImage.Date = DateTime.Now;
            carImage.ImagePath = FileHelper.Add(objectFile, path);
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
