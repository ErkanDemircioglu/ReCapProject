using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Business.Abstract;
using Business.Constants;
using Core.Utilities;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _cardal;


        public CarManager(ICarDal cardal)
        {
            _cardal = cardal;
       
        }

        public IDataResult<List<Car>>  GetAll()
        {
            //if (DateTime.Now.DayOfWeek==DayOfWeek.Wednesday)
            //{
            //    return new ErrorDataResult<List<Car>>(Messages.MaintenanceDay);
            //}
            return new SuccessDataResult<List<Car>>(_cardal.GetAll(),Messages.List);
        }

        public IDataResult<Car>  Get(int id)
        {
            if (DateTime.Now.Hour==18)
            {
                return new ErrorDataResult<Car>(Messages.OutOfHours);
            }
            return new SuccessDataResult<Car>(_cardal.Get(c => c.Id == id),Messages.List);
        }

        public IResult Add(Car car)
        {
            if (car.DailyPrice > 0)
            {
                _cardal.Add(car);
                return new SuccessResult(Messages.Added);
            }
           
                return new ErrorResult(Messages.LowPrice);
             
            

        }

        public IResult Update(Car car)
        {
            _cardal.Update(car);
           return  new SuccessResult(Messages.Updated);
        }

        public IResult Delete(Car car)
        {
            _cardal.Delete(car);
            return  new SuccessResult(Messages.Deleted);
        }

        public IDataResult<List<Car>>  GetCarsByBrandId(int brandId)
        {
            
            return new SuccessDataResult<List<Car>>(_cardal.GetAll(c => c.BrandId == brandId).ToList(),Messages.List);
        }

        public IDataResult<List<Car>>  GetCarsByColorId(int colorId)
        {
            
            return new SuccessDataResult<List<Car>>(_cardal.GetAll(c => c.ColorId == colorId).ToList(),Messages.List);
        }

        public IDataResult<List<CarDetailDto>>  GetCarDetails()
        {

     
            return new SuccessDataResult<List<CarDetailDto>>(_cardal.GetCarDetails(),Messages.List);
        }

   
    }
}
