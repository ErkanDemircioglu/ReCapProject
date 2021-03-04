﻿using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CustomerManager:ICustomerService
    {
        private ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }
        [CacheAspect]
        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll(),Messages.List);
        }
        [CacheAspect]
        public IDataResult<Customer> Get(int id)
        {
            return new SuccessDataResult<Customer>(_customerDal.Get(c=>c.Id==id));
        }
        [CacheRemoveAspect("ICustomerService.Get")]
        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Add(Customer customer)
        {
     
            _customerDal.Add(customer);
            return new SuccessResult(Messages.Added);
        }
        [CacheRemoveAspect("ICustomerService.Get")]
        public IResult Update(Customer customer)
        {
            throw new NotImplementedException();
        }
        [CacheRemoveAspect("ICustomerService.Get")]
        public IResult Delete(Customer customer)
        {
       
                _customerDal.Delete(customer);
                return new SuccessResult(Messages.Deleted);
   
 
        }
    }
}
