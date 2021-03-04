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
using Core.Aspects.Autofac.Caching;

namespace Business.Concrete
{
    public class ColorManager:IColorService
    {
        IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }
        [CacheAspect]
        public IDataResult<Color>  Get(int id)
        {
            return new SuccessDataResult<Color>( _colorDal.Get(c => c.Id == id));
        }
        [CacheRemoveAspect("IColorService.Get")]
        public IResult Add(Color color)
        {
            _colorDal.Add(color);
            return new SuccessResult(Messages.Added);
        }
        [CacheRemoveAspect("IColorService.Get")]
        public IResult Delete(Color color)
        {
            _colorDal.Delete(color);
            return new SuccessResult(Messages.Deleted);
        }
        [CacheRemoveAspect("IColorService.Get")]
        public IDataResult< List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll()); 
        }
        [CacheRemoveAspect("IColorService.Get")]
        public IResult Update(Color color)
        {
            _colorDal.Update(color);
            return new SuccessResult(Messages.Updated);
        }
        [CacheAspect]
        public IDataResult<Color>  GetByColor(string renk)
        {
            return new SuccessDataResult<Color>(_colorDal.GetAll(c => c.Name.Contains(renk)).FirstOrDefault());
        }
    }
}
