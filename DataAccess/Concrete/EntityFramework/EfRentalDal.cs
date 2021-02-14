using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal:EfEntityRepositoryBase<Rental,CarContext>,IRentalDal
    {
        public List<RentCarDetailDto> GetRentCarDetails()
        {
            using (CarContext context=new CarContext())
            {
                var result = from r in context.Rentals
                    join c in context.Cars on r.CarId equals c.Id
                    join b in context.Brands on c.BrandId equals b.Id
                    join co in context.Colors on c.ColorId equals co.Id
                    join cos in context.Customers on r.CustomerId equals cos.Id
                    join us in context.Users on cos.UserId equals us.Id
                    select new RentCarDetailDto
                    {
                        id = r.Id,
                        CustomerName = us.FirstName,
                        BrandName = b.Name,
                        RentDate = r.RentDate,
                        ReturnDate = r.ReturnDate
                    };
                return result.ToList();
            }
        }
    }
}
