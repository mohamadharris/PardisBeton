using Common.Utilities;
using Microsoft.AspNetCore.Http;
using Repository.EF.Base;
using Repository.EF.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Base
{
    public class BLBase<TEntity, TKey> where TEntity : class
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IBaseRepository<TEntity, TKey> _repository;
        protected readonly FileUtility _fileUtility;


        public BLBase(IUnitOfWork unitOfWork, FileUtility fileUtility)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<TEntity, TKey>();
            _fileUtility = fileUtility;
        }

        //To upload images from Summernote Editor using AJAX method from controllers 
        public async Task<string> UploadImage(IFormFile file, string subDirectory)
        {           
            var filePath = await _fileUtility.UploadFileAsync(file,subDirectory, null);

            return filePath;
        }



    }
}
