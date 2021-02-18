﻿using Entities.Concrete;
using System;
using System.Collections.Generic;

using System.Text;
using Core.Utilities;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IColorService
    {
        IDataResult<List<Color>>  GetAll();
        IDataResult<Color>  Get(int id);
        IResult Add(Color color);
        IResult Update(Color color);
        IResult Delete(Color color);
    }
}
