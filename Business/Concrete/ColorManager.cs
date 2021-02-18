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

namespace Business.Concrete
{
    public class ColorManager:IColorService
    {
        IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        public IDataResult<Color>  Get(int id)
        {
            return new SuccessDataResult<Color>( _colorDal.Get(c => c.Id == id));
        }

        public IResult Add(Color color)
        {
            _colorDal.Add(color);
            return new SuccessResult(Messages.Added);
        }

        public IResult Delete(Color color)
        {
            _colorDal.Delete(color);
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult< List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll()); 
        }

        public IResult Update(Color color)
        {
            _colorDal.Update(color);
            return new SuccessResult(Messages.Updated);
        }

        public IDataResult<Color>  GetByColor(string renk)
        {
            return new SuccessDataResult<Color>(_colorDal.GetAll(c => c.Name.Contains(renk)).FirstOrDefault());
        }
    }
}
