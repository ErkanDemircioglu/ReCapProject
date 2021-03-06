﻿using Core.Utilities;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarImageService
    {

        IDataResult<List<CarImage>> GetAll();
        IDataResult<CarImage> Get(int id);
        IResult Delete(CarImage carImage);
        IResult UploadImage(int id, IFormFile objectFile,string path);
        IResult Update(CarImage carImage,IFormFile files,string path);
    }
}
