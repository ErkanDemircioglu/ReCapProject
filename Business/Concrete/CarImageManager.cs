using Business.Abstract;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
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

        public IResult Add(CarImage carImage)
        {
            IResult result = BusinessRules.Run(QuantityCarPhoto(carImage.CarId));
            if (result!=null)
            {
                return result;
            }

           _carImageDal.Add(ChangePhotoName(carImage));
            return new SuccessResult();
        }

        public IDataResult<List<CarImage>> CarPhotosAll(int id)
        {
            var result = _carImageDal.GetAll(c => c.CarId == id);
            if (result.Count == 0)
            {
                return new ErrorDataResult<List<CarImage>>();
            }
           return new SuccessDataResult<List<CarImage>>(result);
        }

        public IResult Delete(CarImage carImage)
        {
            _carImageDal.Delete(carImage);
            return new SuccessResult();
        }

        public IDataResult<CarImage> Get(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(c => c.Id == id));
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());

        }

        public IResult Update(CarImage carImage)
        {
            _carImageDal.Update(ChangePhotoName(carImage));
            return new SuccessResult();
        }

        private CarImage ChangePhotoName(CarImage carImage)
        {
            CarImage returnCarImage = new CarImage();
            string photoName = string.Empty;
            string photoExtension = carImage.ImagePath;
            //dosya isminin ilk kez gönderilen CarImage daki ImagePath e yüklendiğini ve uzantısını aldığımızı varsayıyoruz
            if (photoExtension.ToLower() == ".jpg" || photoExtension.ToLower() == ".png")
            {
                photoName = Guid.NewGuid() + photoExtension;
                returnCarImage.ImagePath = "~photo/" + photoName;
                returnCarImage.CarId = carImage.CarId;
                returnCarImage.Date = DateTime.Now;
                return returnCarImage;
            }
            else
            {
                return carImage;
            }

        }

        private IResult QuantityCarPhoto(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId).Count;
            if (result>=5)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }
       
    }
}
