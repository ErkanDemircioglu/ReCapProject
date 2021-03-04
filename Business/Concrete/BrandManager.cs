using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities;
using Core.Utilities.Results;
using Brand = Entities.Concrete.Brand;
using Core.Aspects.Autofac.Caching;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }
        [CacheRemoveAspect("IBrandService.Get")]
        [ValidationAspect(typeof(BrandValidator))]
        public IResult Add(Brand brand)
        {
    
                return new SuccessResult(Messages.Added);
   
        }
        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Delete(Brand brand)
        {
            _brandDal.Delete(brand);
            return new SuccessResult(Messages.Deleted);
        }
        [CacheAspect]
        public IDataResult<Brand>  Get(int id)
        {
            return new SuccessDataResult<Brand>(_brandDal.Get(b => b.Id == id));
        }
        [CacheAspect]
        public IDataResult<List<Brand>>  GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll()); 
        }
        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Update(Brand brand)
        {
            _brandDal.Update(brand);
            return new SuccessResult(Messages.Updated);
        }
        [CacheAspect]
        public IDataResult<Brand>  GetByBrand(string marka)
        {
            var result= _brandDal.GetAll(b=>b.Name.Contains(marka)).FirstOrDefault();
            if (result!=null)
            {
                return new SuccessDataResult<Brand>(result);
            }
            return new ErrorDataResult<Brand>(Messages.InvalidBrand);
        }
        [CacheAspect]
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
