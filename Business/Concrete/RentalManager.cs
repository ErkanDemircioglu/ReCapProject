using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        private IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }
        [CacheAspect]
        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.List);
        }
        [CacheAspect]
        public IDataResult<Rental> Get(int id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.Id == id), Messages.List);
        }
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Add(Rental rental)
        {
            if (rental.CarId == 5)
            {
                return new ErrorResult(Messages.InvalidCar);
            }
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentACar);
        }
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult();
        }
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult();
        }
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult RentACar(int carId, int userId)
        {
            var kontrol = _rentalDal.GetAll(r => r.CarId == carId && r.ReturnDate!=null);
            if (kontrol.Count > 0)
            {
                return new ErrorResult(Messages.NotRentCar);
            }
            _rentalDal.Add(new Rental
            {
                CarId = carId,
                CustomerId = userId,
                RentDate = Convert.ToDateTime(DateTime.Today.ToShortDateString())
            });
            return new SuccessResult(Messages.RentACar);
        }
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult ReturnCar(int rentId)
        {
            var result = _rentalDal.Get(r => r.Id == rentId);

            _rentalDal.Update(new Rental
            {
                Id = rentId,
                CarId = result.CarId,
                CustomerId = result.CustomerId,
                RentDate = result.RentDate,
                ReturnDate = Convert.ToDateTime(DateTime.Now.ToShortDateString())
            });
            return new SuccessResult(Messages.Updated);
        }
        [CacheAspect]
        public IDataResult<List<RentCarDetailDto>> GetRentCarDetail()
        {
            return new SuccessDataResult<List<RentCarDetailDto>>(_rentalDal.GetRentCarDetails(),Messages.List);
        }
    }
}
