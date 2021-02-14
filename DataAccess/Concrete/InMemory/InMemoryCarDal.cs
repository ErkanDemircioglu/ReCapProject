using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal:ICarDal
    {
        private List<Car> _cars;

        public InMemoryCarDal()
        {
                _cars=new List<Car>
                {
                    new Car{Id = 1,BrandId = 1,ColorId = 1,DailyPrice = 1000,ModelYear = 2010,Description = "Mavi"},
                    new Car{Id = 2,BrandId = 1,ColorId = 2,DailyPrice = 2000,ModelYear = 2005,Description = "Mavi"},
                    new Car{Id = 3,BrandId = 2,ColorId = 2,DailyPrice = 1500,ModelYear = 2016,Description = "Siyah Üstü Açık"},
                    new Car{Id = 4,BrandId = 3,ColorId = 3,DailyPrice = 2500,ModelYear = 2014,Description = "Beyaz"},
                    new Car{Id = 5,BrandId = 3,ColorId = 3,DailyPrice = 500,ModelYear = 1998,Description = "Kırmızı"},

                };
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Update(Car car)
        {
            Car carToUpadate = _cars.SingleOrDefault(c => c.Id == car.Id);
            carToUpadate.BrandId = car.BrandId;
            carToUpadate.ColorId = car.ColorId;
            carToUpadate.DailyPrice = car.DailyPrice;
            carToUpadate.ModelYear = car.ModelYear;
            carToUpadate.Description = car.Description;
            
        }

        public void Delete(Car car)
        {
            Car carToDelete = _cars.SingleOrDefault(c => c.Id == car.Id);
            _cars.Remove(carToDelete);
        }

        public List<CarDetailDto> GetCarDetails()
        {
            throw new NotImplementedException();
        }

        public Car GetById(int id)
        {
            return _cars.SingleOrDefault(c => c.Id == id);
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<CarDetailDto> GetByCarsAll()
        {
            throw new NotImplementedException();
        }
    }
}
