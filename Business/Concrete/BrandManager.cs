using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public void Add(Brand brand)
        {
            if (brand.Name.Length >= 2)
            {
                _brandDal.Add(brand);
            }
            else
            {
                throw new Exception("Araba ismi 2 karakter olamaz");
            }

        }

        public void Delete(Brand brand)
        {
            _brandDal.Delete(brand);
        }

        public Brand Get(int id)
        {
            return _brandDal.Get(b => b.Id == id);
        }

        public List<Brand> GetAll()
        {
            return _brandDal.GetAll();
        }

        public void Update(Brand brand)
        {
            _brandDal.Update(brand);
        }

        public Brand GetByBrand(string marka)
        {
            return _brandDal.GetAll(b=>b.Name.Contains(marka)).FirstOrDefault();
        }

        public List<Brand> GetByBrand2(string marka)
        {
            return _brandDal.GetAll(b => b.Name.Contains(marka));
        }
    }
}
