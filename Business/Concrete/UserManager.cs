﻿using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Core.Utilities;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class UserManager:IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(),Messages.List);
        }

        public IDataResult<User> Get(int id)
        {
            return new SuccessDataResult<User>(_userDal.Get(u=>u.Id==id),Messages.List);
        }

        public IResult Add(User user)
        {
            if (user.FirstName.Length<3)
            {
                return new ErrorResult(Messages.UserNameMessage);
            }

            _userDal.Add(user);
            return new SuccessResult(Messages.Added);
        }

        public IResult Update(User user)
        {
            if (user.FirstName.Length<3)
            {
                return new ErrorResult(Messages.UserNameMessage);
            }
            _userDal.Update(user);
            return new SuccessResult(Messages.Updated);
        }

        public IResult Delete(User user)
        {
            if (user.Id==0 )
            {
                return new ErrorResult(Messages.ExistUser);
            }
            _userDal.Delete(user);
            return new SuccessResult(Messages.Deleted);
        }
    }
}
