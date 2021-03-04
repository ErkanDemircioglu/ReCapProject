using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _cardal;


        public CarManager(ICarDal cardal)
        {
            _cardal = cardal;
       
        }

        [CacheAspect]
        [PerformanceAspect(10)]

        public IDataResult<List<Car>>  GetAll()
        {
            //if (DateTime.Now.DayOfWeek==DayOfWeek.Wednesday)
            //{
            //    return new ErrorDataResult<List<Car>>(Messages.MaintenanceDay);
            //}
            return new SuccessDataResult<List<Car>>(_cardal.GetAll(),Messages.List);
        }
        [CacheAspect]
        public IDataResult<Car>  Get(int id)
        {
            if (id>0)
            {
                return new ErrorDataResult<Car>(Messages.OutOfHours);
            }
            return new SuccessDataResult<Car>(_cardal.Get(c => c.Id == id),Messages.List);
        }
        [CacheRemoveAspect("ICarService.Get")]
        [SecuredOperation("Car.Add,admin")]
        [ValidationAspect(typeof(CarValidator))]
        [PerformanceAspect(5)]
        [TransactionScopeAspect]
        public IResult Add(Car car)
        {
       
                _cardal.Add(car);
                return new SuccessResult(Messages.Added);

        }
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Update(Car car)
        {
            _cardal.Update(car);
           return  new SuccessResult(Messages.Updated);
        }
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Delete(Car car)
        {
            _cardal.Delete(car);
            return  new SuccessResult(Messages.Deleted);
        }
        [CacheAspect]
        public IDataResult<List<Car>>  GetCarsByBrandId(int brandId)
        {
            
            return new SuccessDataResult<List<Car>>(_cardal.GetAll(c => c.BrandId == brandId).ToList(),Messages.List);
        }
        [CacheAspect]
        public IDataResult<List<Car>>  GetCarsByColorId(int colorId)
        {
            
            return new SuccessDataResult<List<Car>>(_cardal.GetAll(c => c.ColorId == colorId).ToList(),Messages.List);
        }
        [CacheAspect]
        public IDataResult<List<CarDetailDto>>  GetCarDetails()
        {

     
            return new SuccessDataResult<List<CarDetailDto>>(_cardal.GetCarDetails(),Messages.List);
        }

   
    }
}
