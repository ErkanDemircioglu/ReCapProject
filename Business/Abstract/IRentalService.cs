using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IDataResult<List<Rental>> GetAll();
        IDataResult<Rental> Get(int id);
        IResult Add(Rental rental);
        IResult Update(Rental rental);
        IResult Delete(Rental rental);
        IResult RentACar(int carId, int userId);
        IResult ReturnCar(int rentId);
        IDataResult<List<RentCarDetailDto>> GetRentCarDetail();
    }
}
