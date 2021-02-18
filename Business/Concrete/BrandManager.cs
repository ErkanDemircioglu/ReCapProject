using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Constants;
using Core.Utilities;
using Core.Utilities.Results;
using Brand = Entities.Concrete.Brand;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public IResult Add(Brand brand)
        {
            if (brand.Name.Length >= 2)
            {
                _brandDal.Add(brand);
                return new SuccessResult(Messages.Added);
            }
            else
            {
                return new ErrorResult();
            }

        }

        public IResult Delete(Brand brand)
        {
            _brandDal.Delete(brand);
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<Brand>  Get(int id)
        {
            return new SuccessDataResult<Brand>(_brandDal.Get(b => b.Id == id));
        }

        public IDataResult<List<Brand>>  GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll()); 
        }

        public IResult Update(Brand brand)
        {
            _brandDal.Update(brand);
            return new SuccessResult(Messages.Updated);
        }

        public IDataResult<Brand>  GetByBrand(string marka)
        {
            var result= _brandDal.GetAll(b=>b.Name.Contains(marka)).FirstOrDefault();
            if (result!=null)
            {
                return new SuccessDataResult<Brand>(result);
            }
            return new ErrorDataResult<Brand>(Messages.InvalidBrand);
        }

        public IDataResult<List<Brand>>  GetByBrand2(string marka)
        {
            var result= _brandDal.GetAll(b => b.Name.Contains(marka));
           
            if (result.Count>0)
            {
                return new SuccessDataResult<List<Brand>>(result,Messages.List);
            }
            return new ErrorDataResult<List<Brand>>(Messages.InvalidBrand);
        }
    }
}
